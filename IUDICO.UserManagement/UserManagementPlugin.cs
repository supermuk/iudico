using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.UserManagement.Models.Auth;
using IUDICO.UserManagement.Models.Storage;
using IUDICO.UserManagement.Models;
using Castle.Windsor;

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
                Component.For<IUserStorage>().ImplementedBy<DatabaseUserStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<IUserService>().ImplementedBy<UserService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<MembershipProvider>().ImplementedBy<OpenIdMembershipProvider>(),
                Component.For<RoleProvider>().ImplementedBy<OpenIdRoleProvider>()

            );

            //HttpContext.Current.Application["UMStorage"] = container.Resolve<DatabaseUserManagement>();// UMStorageFactory.CreateStorage(UMStorageType.Database);
        }

        #endregion

        #region IPlugin Members

        public IEnumerable<Action> BuildActions(Role role)
        {
            return new List<Action>();
        }

        public void BuildMenu(Menu menu)
        {
            menu.Add(new MenuItem("Account", "Account", "Index"));
            menu.Add(new MenuItem("User", "User", "Index"));
            menu.Add(new MenuItem("Group", "Group", "Index"));
        }

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
                "User",
                "User/{action}",
                new { controller = "User" }
            );
        }

        public void Update(string evt, params object[] data)
        {
            // handle events
        }

        public void Setup(IWindsorContainer container)
        {

        }

        #endregion
    }
}