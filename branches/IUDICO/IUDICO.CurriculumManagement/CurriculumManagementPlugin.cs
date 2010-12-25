using System.Collections.Generic;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.CurriculumManagement.Models.Storage;
using Action = IUDICO.Common.Models.Action;

namespace IUDICO.CurriculumManagement
{
    public class CurriculumManagementPlugin : IWindsorInstaller, IPlugin
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
                Component.For<IPlugin>().ImplementedBy<CurriculumManagementPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ICurriculumService>().ImplementedBy<MixedCurriculumStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
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
                "Curriculum",
                "Curriculum/{CurriculumID}/{action}",
                new { controller = "Curriculum" }
            );

            routes.MapRoute(
                "Curriculums",
                "Curriculum/{action}",
                new { controller = "Curriculum", action = "Index" }
            );

            routes.MapRoute(
               "Stage",
               "Stage/{StageId}/{action}",
               new { controller = "Stage" }
            );

            routes.MapRoute(
                "Stages",
                "Curriculum/{CurriculumId}/Stage/{action}",
                new { controller = "Stage" }
            );

            routes.MapRoute(
               "Theme",
               "Theme/{ThemeId}/{action}",
               new { controller = "Theme" }
            );

            routes.MapRoute(
                "Themes",
                "Stage/{StageId}/Theme/{action}",
                new { controller = "Theme" }
            );
        }

        public void Update(string evt, params object[] data)
        {
            // handle events
        }

        #endregion
    }
}