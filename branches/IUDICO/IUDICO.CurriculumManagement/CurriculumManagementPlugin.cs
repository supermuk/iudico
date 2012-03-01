using System.Collections.Generic;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Models.Storage;
using Action = IUDICO.Common.Models.Action;
using IUDICO.CurriculumManagement.Models;

using System.Linq;
using Castle.Windsor;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common;

namespace IUDICO.CurriculumManagement
{
    public class CurriculumManagementPlugin : IWindsorInstaller, IPlugin
    {
        static ICurriculumStorage curriculumStorage { get; set; }

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
                Component.For<ICurriculumStorage>().ImplementedBy<DatabaseCurriculumStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ICurriculumService>().ImplementedBy<CurriculumService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
            );

            curriculumStorage = container.Resolve<ICurriculumStorage>();
        }

        #endregion

        #region IPlugin Members
        public string GetName()
        {
            return Localization.getMessage("CurriculumManagement");
        }

        public IEnumerable<Action> BuildActions()
        {
            return new Action[]
            {
                new Action(Localization.getMessage("CurriculumManagement"), "Discipline/Index")
            };
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new MenuItem[]
            {
                new MenuItem(Localization.getMessage("Curriculums"), "Discipline", "Index")
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
                "Curriculum",
                "Curriculum/{CurriculumId}/{action}",
                new { controller = "Curriculum" }
            );

            routes.MapRoute(
                "Curriculums",
                "Discipline/{DisciplineId}/Curriculum/{action}",
                new { controller = "Curriculum" }
            );

            routes.MapRoute(
                "CurriculumChapter",
                "CurriculumChapter/{CurriculumChapterId}/{action}",
                new { controller = "CurriculumChapter" }
            );

            routes.MapRoute(
                "CurriculumChapters",
                "Curriculum/{CurriculumId}/CurriculumChapter/{action}",
                new { controller = "CurriculumChapter" }
            );

            routes.MapRoute(
                "CurriculumChapterTopic",
                "CurriculumChapterTopic/{CurriculumChapterTopicId}/{action}",
                new { controller = "CurriculumChapterTopic" }
            );

            routes.MapRoute(
                "CurriculumChapterTopics",
                "CurriculumChapter/{CurriculumChapterId}/CurriculumChapterTopic/{action}",
                new { controller = "CurriculumChapterTopic" }
            );
        }

        public void Update(string evt, params object[] data)
        {
            if (evt == UserNotifications.CourseDelete)
            {
                //delete connected Topics
                int courseId = ((Course)data[0]).Id;
                //curriculumStorage.MakeDisciplineInvalid(courseId);
                var topicIds = curriculumStorage.GetTopicsByCourseId(courseId).Select(item => item.Id);
                curriculumStorage.DeleteTopics(topicIds);
            }
            else if (evt == UserNotifications.GroupDelete)
            {
                //delete connected Curriculums
                int groupId = ((Group)data[0]).Id;
                //curriculumStorage.MakeCurriculumsInvalid(groupId);
                var curriculumIds = curriculumStorage.GetCurriculumsByGroupId(groupId).Select(item => item.Id);
                curriculumStorage.DeleteCurriculums(curriculumIds);
            }
            else if (evt == UserNotifications.UserDelete)
            {
                //delete connected Disciplines(Curriculums)
                var disciplineIds = curriculumStorage.GetDisciplines((User)data[0]).Select(item => item.Id);
                curriculumStorage.DeleteDisciplines(disciplineIds);
            }
        }

        public void Setup(IWindsorContainer container)
        {

        }

        #endregion
    }
}