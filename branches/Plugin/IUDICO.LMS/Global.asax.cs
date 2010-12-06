using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using IUDICO.Common.Models.Services;
using IUDICO.LMS.IoC;
using IUDICO.LMS.Models;

namespace IUDICO.LMS
{
    public class MvcApplication : HttpApplication, IContainerAccessor
    {
        static IWindsorContainer container;

        public IWindsorContainer Container
        {
            get { return container; }
        }

        protected void Application_Start()
        {
            InitializeWindsor();

            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyResourceProvider());
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            IControllerFactory controllerFactory = Container.Resolve<IControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_End()
        {
            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }

        protected void RegisterRoutes(RouteCollection routes)
        {
            IService[] services = Container.ResolveAll<IService>();
            foreach (IService service in services)
            {
                service.RegisterRoutes(routes);
            }
            Container.Release(services);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void InitializeWindsor()
        {
            container = new WindsorContainer()
                .Register(Component.For<ILmsService>().ImplementedBy<LmsService>().LifeStyle.Singleton.DependsOn())
                .Install(FromAssembly.This(),
                         FromAssembly.InDirectory(new AssemblyFilter(Server.MapPath("/Plugins"), "IUDICO.*.dll")));
        }
    }
}