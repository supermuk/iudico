using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using System.Collections.Generic;
using Castle.Windsor;

namespace IUDICO.Search
{
    public class SearchPluginn : IWindsorInstaller, IPlugin
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
        public string GetName()
        {
            return "Search";
        }

        public IEnumerable<Action> BuildActions()
        {
            return new Action[] {};
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new MenuItem[] { };
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute(
                "Search",
                "Search/{action}",
                new { controller = "Search" }
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