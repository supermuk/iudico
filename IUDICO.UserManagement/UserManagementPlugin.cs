using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Auth;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement
{
    public class UserManagementPlugin : IWindsorInstaller, IPlugin
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name)),
                Component.For<IPlugin>().ImplementedBy<UserManagementPlugin>().LifeStyle.Is(LifestyleType.Singleton),
                Component.For<IUserStorage>().ImplementedBy<DatabaseUserStorage>().LifeStyle.Is(LifestyleType.Singleton),
                Component.For<IUserService>().ImplementedBy<UserService>().LifeStyle.Is(LifestyleType.Singleton),
                Component.For<MembershipProvider>().ImplementedBy<OpenIdMembershipProvider>(),
                Component.For<RoleProvider>().ImplementedBy<OpenIdRoleProvider>()
                );

            //HttpContext.Current.Application["UMStorage"] = container.Resolve<DatabaseUserManagement>();// UMStorageFactory.CreateStorage(UMStorageType.Database);
        }

        #endregion

        #region IPlugin Members

        public string GetName()
        {
            return Localization.getMessage("UserManagement");
        }

        public IEnumerable<Action> BuildActions()
        {
            return new[]
                       {
                           new Action(Localization.getMessage("GetUsers"), "User/Index"),
                           new Action(Localization.getMessage("GetGroups"), "Group/Index"),
                           new Action(Localization.getMessage("Register"), "Account/Register"),
                           new Action(Localization.getMessage("ForgotPassword"), "Account/Forgot"),
                           new Action(Localization.getMessage("Login"), "Account/Login"),
                       };
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new[]
                       {
                           new MenuItem(Localization.getMessage("Users"), "User", "Index"),
                           new MenuItem(Localization.getMessage("Groups"), "Group", "Index")
                       };
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Account",
                "Account/{action}",
                new {controller = "Account"}
                );

            routes.MapRoute(
                "Group",
                "Group/{action}",
                new {controller = "Group"}
                );

            routes.MapRoute(
                "User",
                "User/{action}",
                new {controller = "User"}
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