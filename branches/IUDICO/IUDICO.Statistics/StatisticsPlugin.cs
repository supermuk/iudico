using System.Collections.Generic;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.Statistics.Models.Storage;
using IUDICO.Statistics.Models;
using Castle.Windsor;
using IUDICO.Common;

namespace IUDICO.Statistics
{
    public class StatisticsPlugin : IWindsorInstaller, IPlugin
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name)),
                Component.For<IPlugin>().ImplementedBy<StatisticsPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<IStatisticsProxy>().ImplementedBy<StatisticsProxy>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<IStatisticsService>().ImplementedBy<StatisticsService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton));
        }

        #endregion

        #region IPlugin Members
        public string GetName()
        {
            return "Statistics";
        }

        public IEnumerable<Action> BuildActions()
        {
            return new[]
                {
                    new Action(Localization.GetMessage("GetStats"), "Stats/Index"),
                    new Action(Localization.GetMessage("QualityTest"), "QualityTest/SelectDiscipline")
                };
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new[] { new MenuItem(Localization.GetMessage("Statistics"), "Stats", "Index") };
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute(
                "Statistics",
                "Stats/{action}",
                new { controller = "Stats", action = "Index" });
        }

        public void Update(string evt, params object[] data)
        {
            // handle events
        }

        #endregion
    }
}