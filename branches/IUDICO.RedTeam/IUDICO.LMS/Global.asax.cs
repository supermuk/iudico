using System;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.TemplateMetadata;
using IUDICO.LMS.IoC;
using IUDICO.LMS.Models;
using IUDICO.Common.Models.Plugin;
using System.Collections.Generic;
using Action = IUDICO.Common.Models.Action;
using System.Reflection;
using IUDICO.LMS.Models.Providers;
using System.Web.Security;
using System.Globalization;
using System.Threading;
using System.Text;

namespace IUDICO.LMS
{
    public class MvcApplication : HttpApplication, IContainerAccessor
    {
        static IWindsorContainer _Container;

        public static Menu Menu { get; protected set; }
        //public static IEnumerable<Action> Actions { get; protected set; }
        public static Dictionary<IPlugin, IEnumerable<Action>> Actions { get; protected set; }

        public IWindsorContainer Container
        {
            get { return _Container; }
        }

        public static IWindsorContainer StaticContainer
        {
            get { return _Container; }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var securityService = Container.Resolve<ISecurityService>();
            var plugins = Container.ResolveAll<IPlugin>();
            //var currentRole = Container.Resolve<IUserService>().GetCurrentUser().Role;
            var currentRole = IUDICO.Common.Models.Role.None;

            lock (Actions)
            {
                Actions.Clear();

                foreach (var plugin in plugins)
                {
                    plugin.Setup(Container);

                    var actions = plugin.BuildActions(currentRole).Where(a => IsAllowed(a, currentRole));

                    if (Actions.ContainsKey(plugin))
                    {
                        Actions[plugin] = actions;
                    }
                    else
                    {
                        Actions.Add(plugin, actions);
                    }
                }
            }
        }

        protected void Application_Start()
        {
            Common.Log4NetLoggerService.InitLogger();
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("log.xml")));

            ViewEngines.Engines.Add(new PluginViewEngine());

            Actions = new Dictionary<IPlugin, IEnumerable<Action>>();

            AppDomain.CurrentDomain.AppendPrivatePath(Server.MapPath("/Plugins"));
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyResourceProvider());
            AreaRegistration.RegisterAllAreas();

            InitializeWindsor();

            LoadProviders();
            LoadPluginData();
            RegisterRoutes(RouteTable.Routes);

            var controllerFactory = Container.Resolve<IControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            ModelMetadataProviders.Current = new FieldTemplateMetadataProvider();
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    var context = HttpContext.Current;
        //    var exception = context.Server.GetLastError();

        //    context.Response.Clear();

        //    if (exception != null)
        //    {
        //        Server.ClearError();
        //        //тут можна залогати ерор.
        //        //context.Response.RedirectToRoute("Default", new { controller = "Home", action = "Error" });
        //    }
        //}

        private void LoadProviders()
        {
            ((IoCMembershipProvider)Membership.Provider).Initialize(Container.Resolve<MembershipProvider>());
            ((IoCRoleProvider)Roles.Provider).Initialize(Container.Resolve<RoleProvider>());
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            /// log error
            throw new NotImplementedException();
        }

        protected void Application_End()
        {
            if (_Container != null)
            {
                _Container.Dispose();
                _Container = null;
            }
        }

        protected void LoadPluginData()
        {
            Menu = new Menu();

            var plugins = Container.ResolveAll<IPlugin>();

            foreach (var plugin in plugins)
            {
                plugin.BuildMenu(Menu);
            }
        }

        protected void RegisterRoutes(RouteCollection routes)
        {
            var plugins = Container.ResolveAll<IPlugin>();

            foreach (var plugin in plugins)
            {
                plugin.RegisterRoutes(routes);
            }

            Container.Release(plugins);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ico/{*pathInfo}");
            routes.IgnoreRoute("Scripts/");
            routes.IgnoreRoute("Content/");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void InitializeWindsor()
        {
            _Container = new WindsorContainer();

            _Container
                .Register(
                    Component.For<IWindsorContainer>().Instance(_Container))
                .Register(
                    Component.For<ILmsService>().ImplementedBy<LmsService>().LifeStyle.Singleton.DependsOn())
                .Install(FromAssembly.This(),
                         FromAssembly.InDirectory(new AssemblyFilter(Server.MapPath("/Plugins"), "IUDICO.*.dll"))
            );
        }

        protected bool IsAllowed(Action fullAction, Role role)
        {
            var parts = fullAction.Link.Split('/');

            var controller = _Container.Resolve<IController>(parts[0] + "controller");
            var action = controller.GetType().GetMethods().Where(m => m.Name == parts[1] && !IsPost(m) && m.GetParameters().Length == 0).FirstOrDefault();

            var attribute = Attribute.GetCustomAttribute(action, typeof(AllowAttribute), false) as AllowAttribute;

            return attribute == null || Roles.Provider.IsUserInRole(HttpContext.Current.User.Identity.Name, attribute.Role.ToString());
        }

        protected bool IsPost(MethodInfo action)
        {
            return Attribute.GetCustomAttribute(action, typeof(HttpPostAttribute), false) != null;
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                CultureInfo ci = (CultureInfo)this.Session["Culture"];
                //Checking first if there is no value in session
                //and set default language
                //this can happen for first user's request
                if (ci == null)
                {
                    //Sets default culture to english invariant
                    string langName = "en-US";
                    //Try to get values from Accept lang HTTP header
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
                //Finally setting culture for each request
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
            LoadPluginData();
            Application_BeginRequest(sender, e);
        }

        void Application_EndRequest(Object Sender, EventArgs e)
        {
                log4net.ILog log = log4net.LogManager.GetLogger(typeof(MvcApplication));
                log.Info(Request.HttpMethod + ": " + Request.Path);
        }
    }
}