using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models.Plugin;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models;
using System.Web.Routing;
using IUDICO.Security.Models.Storages;
using IUDICO.Security.Models.Storages.Database;
using Action = IUDICO.Common.Models.Action;

namespace IUDICO.Security
{
    public class SecurityPlugin : IPlugin, IWindsorInstaller
    {
        private const String SECURITY_PLUGIN_NAME = "SecurityPlugin";

        #region IWindsorInstaller

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name)),
                Component.For<IPlugin>().ImplementedBy<SecurityPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ISecurityService>().ImplementedBy<SecurityService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<IBanStorage>().ImplementedBy<DatabaseBanStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ISecurityStorage>().ImplementedBy<DatabaseSecurityStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
            );
        }
        #endregion

        #region IPlugin

        public string GetName()
        {
            return Localization.getMessage(Localization.Keys.SECURITY_PLUGIN);
        }

        public IEnumerable<Action> BuildActions()
        {
            return new Action[]
            {
                new Action(Localization.getMessage(Localization.Keys.SECURITY), "Security/Index")
            };
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new MenuItem[]
            {
                new MenuItem(Localization.getMessage(Localization.Keys.SECURITY), "Security", "Index")
            };
        }

        public void Setup(IWindsorContainer container)
        {
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Ban",
                "Ban/{action}",
                new { controller = "Ban" });

            routes.MapRoute(
                "Security",
                "Security/{action}",
                new { controller = "Security" });
        }
        public void Update(string evt, params object[] data)
        {
        }
        #endregion
    }
}