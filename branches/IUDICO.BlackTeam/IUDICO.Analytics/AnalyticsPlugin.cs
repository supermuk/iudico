using System.Collections.Generic;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.Analytics.Models;
using Action = IUDICO.Common.Models.Action;


using System.Linq;
using Castle.Windsor;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Action;

namespace IUDICO.Analytics
{
    public class AnalyticsPlugin : IWindsorInstaller, IPlugin
    {

        #region IWindsorInstaller Members

        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            IUDICO.Common.Localization.Initialize();
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name)),
                Component.For<IPlugin>().ImplementedBy<AnalyticsPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<IAnalyticsService>().ImplementedBy<AnalyticsService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
            );
        }

        #endregion

        #region IPlugin Members

        public string GetName()
        {
            return "Analytics";
        }

        public IEnumerable<IAction> BuildActions()
        {
            var actions = new List<IAction>();

            actions.Add(new ActionURL("Analytics", "Stats/Index", Role.Teacher));
            
            return actions;
        }

        public void BuildMenu(Menu menu)
        {
            menu.Add(new MenuItem("Analytics", "Analytics", "Index"));
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute(
                "Analytics",
                "Analytics/{action}",
                new { controller = "Analytics", action = "Index" }
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