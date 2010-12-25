using System;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.TemplateMetadata;
using IUDICO.LMS.IoC;
using IUDICO.LMS.Models;
using IUDICO.Common.Models.Plugin;
using System.Collections.Generic;
using Action = IUDICO.Common.Models.Action;
using System.Reflection;

namespace IUDICO.LMS
{
    public class MvcApplication : HttpApplication, IContainerAccessor
    {
        static IWindsorContainer _Container;

        public static Menu Menu { get; protected set; }
        public static IEnumerable<Action> Actions { get; protected set; }

        public IWindsorContainer Container
        {
            get { return _Container; }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var actions = new List<Action>();
            var plugins = Container.ResolveAll<IPlugin>();

            foreach (var plugin in plugins)
            {
                // TODO: add proper role
                actions.AddRange(plugin.BuildActions(new Role()));
            }

            Actions = actions;
        }

        protected void Application_Start()
        {
            AppDomain.CurrentDomain.AppendPrivatePath(Server.MapPath("/Plugins"));
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            InitializeWindsor();

            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyResourceProvider());
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
            LoadPluginData();

            var controllerFactory = Container.Resolve<IControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            ModelMetadataProviders.Current = new FieldTemplateMetadataProvider();
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
    }
}