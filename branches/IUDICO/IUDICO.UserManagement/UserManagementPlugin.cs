using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
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
                Component.For<IPlugin>().ImplementedBy<UserManagementPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<IUserService>().ImplementedBy<DatabaseUserStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
            );

            //HttpContext.Current.Application["UMStorage"] = container.Resolve<DatabaseUserManagement>();// UMStorageFactory.CreateStorage(UMStorageType.Database);
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