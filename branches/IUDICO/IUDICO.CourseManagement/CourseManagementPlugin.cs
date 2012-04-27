using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.CourseManagement.Models;
using IUDICO.Common.Models;
using System.Collections.Generic;
using Action = IUDICO.Common.Models.Action;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common;

namespace IUDICO.CourseManagement
{
    public class CourseManagementPlugin : IWindsorInstaller, IPlugin
    {
        IWindsorContainer container;

        ICourseStorage CourseStorage
        {
            get
            {
                return this.container.Resolve<ICourseStorage>();
            }
        }

        #region IPlugin Members
        public string GetName()
        {
            return Localization.GetMessage("CourseManagement");
        }

        public IEnumerable<Action> BuildActions()
        {

            return new Action[]
            {
                new Action(Localization.GetMessage("GetCourses"), "Course/Index"),
                new Action(Localization.GetMessage("CreateCourse"), "Course/Create")
            };
            // actions.Add(new Action(Localization.getMessage("EditCourse"), "Course/Index", Role.Teacher));
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new MenuItem[]
            {
                new MenuItem(Localization.GetMessage("Courses"), "Course", "Index")
            };
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Images",
                "Course/{CourseID}/Node/{NodeID}/Images/{FileName}",
                new { controller = "Node", action = "Images", CourseID = 0, FileName = string.Empty });

            routes.MapRoute(
                "Node",
                "Course/{CourseID}/Node/{NodeID}/{action}",
                new { controller = "Node", CourseID = 0 });

            routes.MapRoute(
                "Nodes",
                "Course/{CourseID}/Node/{action}",
                new { controller = "Node", action = "Index", CourseID = 0 });

            routes.MapRoute(
                "Course",
                "Course/{CourseID}/{action}",
                new { controller = "Course" });

            routes.MapRoute(
                "Courses",
                "Course/{action}",
                new { controller = "Course", action = "Index" });
        }

        public void Setup(IWindsorContainer container)
        {

        }

        public void Update(string evt, params object[] data)
        {
            // handle appropriate events
            switch (evt)
            {
                case UserNotifications.UserDelete:
                var user = (User)data[0];
                // courseStorage.DeleteCourseUsers(user.Id);
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
                Component.For<IPlugin>().Instance(this).LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                // Component.For<ICourseStorage>().ImplementedBy<CachedCourseStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ICourseStorage>().ImplementedBy<MixedCourseStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ICourseService>().ImplementedBy<CourseService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton));

            this.container = container;
        }
        #endregion
    }
}