using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.CourseManagement.Models;
using IUDICO.Common.Models;
using System.Collections.Generic;
using Action = IUDICO.Common.Models.Action;
using IUDICO.Common.Models.Notifications;

namespace IUDICO.CourseManagement
{
    public class CourseManagementPlugin : IWindsorInstaller, IPlugin
    {
        IWindsorContainer container;

        ICourseStorage courseStorage
        {
            get
            {
                return container.Resolve<ICourseStorage>();
            }
        }

        #region IPlugin Members
        public IEnumerable<Action> BuildActions(Role role)
        {
            var actions = new List<Action>();

            actions.Add(new Action("Get Courses", "Course/Index"));

            return actions;
        }

        public void BuildMenu(Menu menu)
        {
            menu.Add(new MenuItem("Course", "Course", "Index"));
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Node",
                "Course/{CourseID}/Node/{NodeID}/{action}",
                new { controller = "Node", CourseID = 0 }
            );

            routes.MapRoute(
                "Nodes",
                "Course/{CourseID}/Node/{action}",
                new { controller = "Node", action = "Index", CourseID = 0 }
            );

            routes.MapRoute(
                "Course",
                "Course/{CourseID}/{action}",
                new { controller = "Course" }
            );

            routes.MapRoute(
                "Courses",
                "Course/{action}",
                new { controller = "Course", action = "Index" }
            );
        }

        public void Update(string evt, params object[] data)
        {
            // handle appropriate events
            switch(evt)
            {
                case UserNotifications.UserDelete:
                var user = (User)data[0];
                //courseStorage.DeleteCourseUsers(user.Id);
                break;
            }
        }
        #endregion

        #region IWindsorInstaller Members
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name)),
                Component.For<IPlugin>().ImplementedBy<CourseManagementPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ICourseStorage>().ImplementedBy<MixedCourseStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ICourseService>().ImplementedBy<CourseService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
            );
            
            //courseStorage = container.Resolve<ICourseStorage>();
            this.container = container;
        }
        #endregion
    }
}