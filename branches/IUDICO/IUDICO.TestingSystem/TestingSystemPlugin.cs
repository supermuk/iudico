using System.Collections.Generic;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.TestingSystem.Models;

namespace IUDICO.TestingSystem
{
    public class TestingSystemPlugin : IWindsorInstaller, IPlugin
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
                Component.For<IPlugin>().ImplementedBy<TestingSystemPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ITestingService>().ImplementedBy<FakeTestingSystem>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
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
               "Training",
               "Training/{packageId}/{attemptId}",
               new { controller = "Training", action = "Details", attemptId = UrlParameter.Optional },
               new { packageID = @"\d+" });

            routes.MapRoute(
               "Trainings",
               "Training/{action}/{id}",
               new { controller = "Training", action = "Index", id = UrlParameter.Optional }
            );
        }

        public void Update(string evt, params object[] data)
        {
            // handle events
        }

        #endregion
    }
}