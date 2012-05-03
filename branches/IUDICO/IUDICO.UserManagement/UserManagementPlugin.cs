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
    using IUDICO.Common;
    using IUDICO.Common.Models.Notifications;
    using IUDICO.Common.Models.Shared.Statistics;

    public class UserManagementPlugin : IWindsorInstaller, IPlugin
    {
        protected IWindsorContainer container;
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            this.container = container;

            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name)),
                Component.For<IPlugin>().Instance(this).LifeStyle.Is(LifestyleType.Singleton),
                ////Component.For<IPlugin>().ImplementedBy<UserManagementPlugin>().LifeStyle.Is(LifestyleType.Singleton),
                ////Component.For<IUserStorage>().ImplementedBy<CachedUserStorage>().LifeStyle.Is(LifestyleType.Singleton),
                Component.For<IUserStorage>().ImplementedBy<DatabaseUserStorage>().LifeStyle.Is(LifestyleType.Singleton),
                Component.For<IUserService>().ImplementedBy<UserService>().LifeStyle.Is(LifestyleType.Singleton),
                Component.For<MembershipProvider>().ImplementedBy<OpenIdMembershipProvider>(),
                Component.For<RoleProvider>().ImplementedBy<OpenIdRoleProvider>());
        }

        #endregion

        #region IPlugin Members

        public string GetName()
        {
            return "UserManagement";
        }

        public IEnumerable<Action> BuildActions()
        {
            return new[]
                       {
                           new Action(Localization.GetMessage("GetUsers"), "User/Index"),
                           new Action(Localization.GetMessage("GetGroups"), "Group/Index"),
                           new Action(Localization.GetMessage("Register"), "Account/Register"),
                           new Action(Localization.GetMessage("ForgotPassword"), "Account/Forgot"),
                           new Action(Localization.GetMessage("Login"), "Account/Login"),
                       };
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new[]
                       {
                           new MenuItem(Localization.GetMessage("Users"), "User", "Index"),
                           new MenuItem(Localization.GetMessage("Groups"), "Group", "Index")
                       };
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Account", "Account/{action}", new { controller = "Account" });

            routes.MapRoute(
                "Group", "Group/{action}", new { controller = "Group" });

            routes.MapRoute(
                "User", "User/{action}", new { controller = "User" });
        }

        public void Update(string evt, params object[] data)
        {
            if (evt == TestingNotifications.TestCompleted)
            {
                var attemptResult = (AttemptResult)data[0];
                this.container.Resolve<IUserStorage>().UpdateUserAverage(attemptResult);
            }
        }

        public void Setup(IWindsorContainer container)
        {
        }

        #endregion
    }
}