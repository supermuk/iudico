using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using IUDICO.Analytics.Models;
using IUDICO.Analytics.Models.Storage;
using IUDICO.Common;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;

namespace IUDICO.Analytics
{
    public class AnalyticsPlugin : IWindsorInstaller, IPlugin
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
                Component.For<IPlugin>().ImplementedBy<AnalyticsPlugin>().LifeStyle.Is(LifestyleType.Singleton),
                Component.For<IAnalyticsStorage>().ImplementedBy<CachedAnalyticsStorage>().LifeStyle.Is(LifestyleType.Singleton),
                Component.For<IAnalyticsStorage>().ImplementedBy<MixedAnalyticsStorage>().LifeStyle.Is(LifestyleType.Singleton),
                Component.For<IAnalyticsService>().ImplementedBy<AnalyticsService>().LifeStyle.Is(LifestyleType.Singleton));
        }

        #endregion

        #region IPlugin Members

        public string GetName()
        {
            return "Analytics";
        }

        public IEnumerable<Action> BuildActions()
        {
            return new[]
                       {
                           ////new Action(Localization.GetMessage("Analytics"), "Stats/Index"),
                           new Action(Localization.GetMessage("Tags"), "Tags/Index"),
                           new Action(Localization.GetMessage("Recommender"), "Recommender/Index"),
                           new Action(Localization.GetMessage("AnomalyDetection"), "AnomalyDetection/Index")
                       };
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new[]
                       {
                        new MenuItem(Localization.GetMessage("Analytics"), "Analytics", "Index"),
                       };
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Analytics",
                "Analytics/{action}",
                new { controller = "Analytics", action = "Index" });

            routes.MapRoute(
                "Tags",
                "Tags/{action}",
                new { controller = "Tags", action = "Index" });

            routes.MapRoute(
                "Recommender",
                "Recommender/{action}",
                new { controller = "Recommender", action = "Index" });
            routes.MapRoute(
                "AnomalyDetection",
                "AnomalyDetection/{action}",
                new { controller = "AnomalyDetection", action = "Index" });
            routes.MapRoute(
                "Quality",
                "Quality/{action}",
                new { controller = "Quality", action = "Index" });
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