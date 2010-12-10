using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models.Plugin;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement
{
    public class UserManagementPlugin : IWindsorInstaller, IPlugin
    {
        #region IWindsorInstaller Members

        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name)),
                //Component.For<IPlugin>().ImplementedBy<CourseManagmentPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                //Component.For<ICourseManagment>().ImplementedBy<MixedCourseStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
                Component.For<DatabaseUMStorage>().ImplementedBy<DatabaseUMStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
            );

            HttpContext.Current.Application["UMStorage"] = container.Resolve<DatabaseUMStorage>();// UMStorageFactory.CreateStorage(UMStorageType.Database);
        }

        #endregion

        #region IPlugin Members

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute(
                "Account",
                "Account/{action}",
                new { controller = "Account" }
            );

            routes.MapRoute(
                "Group",
                "Group/{action}",
                new { controller = "Group" }
            );

            routes.MapRoute(
                "Role",
                "Role/{action}",
                new { controller = "Role" }
            );
        }

        public void Update(string evt, params object[] data)
        {
            // handle events
        }

        #endregion
    }
}