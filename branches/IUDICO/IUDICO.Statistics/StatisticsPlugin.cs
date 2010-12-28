using System.Collections.Generic;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.Statistics.Models.Storage;
using IUDICO.Statistics.Models;
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
                Component.For<IStatisticsStorage>().ImplementedBy<StatisticsStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<IStatisticsService>().ImplementedBy<StatisticsService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
            );
        }

        #endregion

        #region IPlugin Members

        public IEnumerable<Action> BuildActions(Role role)
        {
            return new List<Action>();
        }

        public void BuildMenu(Menu menu)
        {

        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute(
                "Statistics",
                "Statistics/{action}",
                new { controller = "Statistics" }
            );

        }

        public void Update(string evt, params object[] data)
        {
            // handle events
        }

        #endregion
    }
}