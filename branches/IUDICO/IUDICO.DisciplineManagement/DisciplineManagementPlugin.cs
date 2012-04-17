using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using Castle.Windsor;
using IUDICO.Common.Models.Notifications;
using IUDICO.DisciplineManagement.Models;
using IUDICO.DisciplineManagement.Models.Storage;

namespace IUDICO.DisciplineManagement
{
    public class DisciplineManagementPlugin : IWindsorInstaller, IPlugin
    {
        static IDisciplineStorage _DisciplineStorage { get; set; }

        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name)),
                Component.For<IPlugin>().ImplementedBy<DisciplineManagementPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<IDisciplineStorage>().ImplementedBy<DatabaseDisciplineStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<IDisciplineService>().ImplementedBy<DisciplineService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
            );

            _DisciplineStorage = container.Resolve<IDisciplineStorage>();
        }

        #endregion

        #region IPlugin Members
        public string GetName()
        {
            return Localization.getMessage("DisciplineManagement");
        }

        public IEnumerable<Action> BuildActions()
        {
            return new[]
            {
                new Action(Localization.getMessage("DisciplineManagement"), "Discipline/Index")
            };
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new[]
            {
                new MenuItem(Localization.getMessage("Disciplines"), "Discipline", "Index")
            };
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute(
                "Discipline",
                "Discipline/{DisciplineID}/{action}",
                new { controller = "Discipline" }
            );

            routes.MapRoute(
                "Disciplines",
                "Discipline/{action}",
                new { controller = "Discipline", action = "Index" }
            );

            routes.MapRoute(
               "Chapter",
               "Chapter/{ChapterId}/{action}",
               new { controller = "Chapter" }
            );

            routes.MapRoute(
                "Chapters",
                "Discipline/{DisciplineId}/Chapter/{action}",
                new { controller = "Chapter" }
            );

            routes.MapRoute(
               "ChapterAction",
               "ChapterAction/{action}",
               new { controller = "Chapter" }
            );

            routes.MapRoute(
               "Topic",
               "Topic/{TopicId}/{action}",
               new { controller = "Topic" }
            );

            routes.MapRoute(
                "Topics",
                "Chapter/{ChapterId}/Topic/{action}",
                new { controller = "Topic" }
            );

            routes.MapRoute(
               "TopicAction",
               "TopicAction/{action}",
               new { controller = "Topic" }
            );
        }

        public void Update(string evt, params object[] data)
        {
            switch (evt)
            {
                case UserNotifications.CourseDelete:
                    //delete connected Topics
                    var courseId = ((Course)data[0]).Id;
                    //curriculumStorage.MakeDisciplineInvalid(courseId);
                    var topicIds = _DisciplineStorage.GetTopicsByCourseId(courseId).Select(item => item.Id);
                    _DisciplineStorage.DeleteTopics(topicIds);
                    break;
                case UserNotifications.UserDelete:
                    //delete connected Disciplines(Curriculums)
                    var disciplineIds = _DisciplineStorage.GetDisciplines((User)data[0]).Select(item => item.Id);
                    _DisciplineStorage.DeleteDisciplines(disciplineIds);
                    break;
            }
        }

        public void Setup(IWindsorContainer container)
        {

        }

        #endregion
    }
}