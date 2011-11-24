using System.Collections.Generic;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.Statistics.Models.Storage;
using IUDICO.Statistics.Models;
using Castle.Windsor;
namespace IUDICO.Statistics
{
    public class StatisticsPlugin : IWindsorInstaller, IPlugin
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
                Component.For<IPlugin>().ImplementedBy<StatisticsPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<IStatisticsProxy>().ImplementedBy<StatisticsProxy>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<IStatisticsService>().ImplementedBy<StatisticsService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
            );
        }

        #endregion

        #region IPlugin Members
        public string GetName()
        {
            return IUDICO.Statistics.Localization.getMessage("Statistics");
        }

        public IEnumerable<Action> BuildActions(Role role)
        {
            var actions = new List<Action>();

            actions.Add(new Action(IUDICO.Statistics.Localization.getMessage("GetStats"), "Stats/Index"));
            actions.Add(new Action(IUDICO.Statistics.Localization.getMessage("QualityTest"), "QualityTest/SelectCurriculum"));
            return actions;
        }

        public void BuildMenu(Menu menu)
        {
            menu.Add(new MenuItem(IUDICO.Statistics.Localization.getMessage("Statistics"), "Stats", "Index"));
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute(
                "Statistics",
                "Stats/{action}",
                new { controller = "Stats", action = "Index" }
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