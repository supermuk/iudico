using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models.Plugin;

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
                                        .Named(c.Implementation.Name))//,
                //Component.For<IPlugin>().ImplementedBy<CourseManagmentPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                //Component.For<ICourseManagment>().ImplementedBy<MixedCourseStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
            );
        }

        #endregion

        #region IPlugin Members

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