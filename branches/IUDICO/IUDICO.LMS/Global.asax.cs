using System;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using IUDICO.Common;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.TemplateMetadata;
using IUDICO.LMS.IoC;
using IUDICO.LMS.Models;
using IUDICO.Common.Models.Plugin;
using System.Reflection;
using IUDICO.LMS.Models.Providers;
using System.Web.Security;
using System.Globalization;
using System.Threading;
using IUDICO.Common.Models.Notifications;

namespace IUDICO.LMS
{
    public class MvcApplication : HttpApplication, IContainerAccessor
    {
        static IWindsorContainer container;

        public static IWindsorContainer StaticContainer
        {
            get { return container; }
        }

        // public static Menu Menu { get; protected set; }
        // public static IEnumerable<Action> Actions { get; protected set; }
        // public static Dictionary<IPlugin, IEnumerable<Action>> Actions { get; protected set; }

        public static ILmsService LmsService
        {
            get { return container.Resolve<ILmsService>(); }
        }

        public IWindsorContainer Container
        {
            get { return container; }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            LmsService.Inform(LMSNotifications.ApplicationRequestStart, HttpContext.Current);

            this.Container.Resolve<LinqLogger>().WriteLine("==== Begin request ====");
        }

        protected void Application_Start()
        {
            // AppDomain.CurrentDomain.SetupInformation.PrivateBinPath = "Plugins";
            // AppDomain.CurrentDomain.AppendPrivatePath(Server.MapPath("/Plugins"));
            AppDomain.CurrentDomain.AssemblyResolve += this.CurrentDomain_AssemblyResolve;

            this.InitializeWindsor();

            Log4NetLoggerService.InitLogger();
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("log.xml")));

            ViewEngines.Engines.Add(new PluginViewEngine());
            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyResourceProvider());
            ControllerBuilder.Current.SetControllerFactory(this.Container.Resolve<IControllerFactory>());
            ModelMetadataProviders.Current = new FieldTemplateMetadataProvider();

            this.LoadProviders();
            this.RegisterRoutes(RouteTable.Routes);

            LmsService.Inform(LMSNotifications.ApplicationStart, this.Container.Resolve<ILmsService>());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            var exception = context.Server.GetLastError();

            // context.Response.Clear();
            
            if (exception != null)
            {
                // Server.ClearError();                
                Logger.Instance.Error(this, Request.HttpMethod + ": " + Request.Path);
            }
        }

        private void LoadProviders()
        {
            ((IoCMembershipProvider)Membership.Provider).Initialize(this.Container.Resolve<MembershipProvider>());
            ((IoCRoleProvider)Roles.Provider).Initialize(this.Container.Resolve<RoleProvider>());
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // log error
            throw new NotImplementedException();
        }

        protected void Application_End()
        {
            LmsService.Inform(LMSNotifications.ApplicationStop);

            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }

        protected void RegisterRoutes(RouteCollection routes)
        {
            var plugins = this.Container.ResolveAll<IPlugin>();

            foreach (var plugin in plugins)
            {
                plugin.RegisterRoutes(routes);
            }

            this.Container.Release(plugins);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ico/{*pathInfo}");
            routes.IgnoreRoute("{Scripts}/{*pathInfo}");
            routes.IgnoreRoute("{Content}/{*pathInfo}");
            routes.IgnoreRoute("{Data}/{*pathInfo}");

            routes.IgnoreRoute(
                "{*staticfile}", new { staticfile = @".*\.(jpg|gif|jpeg|png|js|css|htm|html|htc)$" });

            routes.MapRoute(
                "Default", // Route name,
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }); // Parameter defaults
        }

        protected void InitializeWindsor()
        {
            container = new WindsorContainer();

            container
                .Register(
                    Component.For<IWindsorContainer>().Instance(container))
                .Register(Component.For<ILmsService>().ImplementedBy<LmsService>().LifeStyle.Singleton).Install(
                    FromAssembly.This(),
                    FromAssembly.InDirectory(new AssemblyFilter(Server.MapPath("/Plugins"), "IUDICO.*.dll")));

            container.Register(
                Component.For<LinqLogger>().ImplementedBy<LinqLogger>()
                .Parameters(
                    Parameter.ForKey("fileName").Eq(Server.MapPath("/Data/Logs/LINQ/log.txt"))));
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                var ci = (CultureInfo)this.Session["Culture"];
                // Checking first if there is no value in session
                // and set default language
                // this can happen for first user's request
                if (ci == null)
                {
                    // Sets default culture to english invariant
                    string langName = "en-US";
                    // Try to get values from Accept lang HTTP header
                    if (HttpContext.Current.Request.UserLanguages != null &&
                        HttpContext.Current.Request.UserLanguages.Length != 0)
                    {
                        langName = HttpContext.Current.Request.UserLanguages[0].Split(';')[0];
                    }
                    if (langName != "en-US" && langName != "uk-UA")
                    {
                        langName = "en-US";
                    }
                    ci = new CultureInfo(langName);
                    this.Session["Culture"] = ci;
                }
                // Finally setting culture for each request
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }

            this.Application_BeginRequest(sender, e);
        }

        void Application_EndRequest(object sender, EventArgs e)
        {
            this.Container.Resolve<LinqLogger>().WriteLine("==== End request ====");
            LmsService.Inform(LMSNotifications.ApplicationRequestEnd, HttpContext.Current, Request);
        }

        /// <summary>
        /// Allows caching by custom parameters
        /// </summary>
        /// <param name="context"></param>
        /// <param name="custom"></param>
        /// <returns></returns>
        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            switch (custom)
            {
                case "user":
                    var currentUser = this.Container.Resolve<IUserService>().GetCurrentUser();
                    return currentUser != null ? currentUser.Username : string.Empty;
                default:
                    return base.GetVaryByCustomString(context, custom);
            }
        }
    }
}
