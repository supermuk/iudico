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
using IUDICO.Common.Models.Caching;

namespace IUDICO.CurriculumManagement
{
    public class CurriculumManagementPlugin : IWindsorInstaller, IPlugin
    {
        static ICurriculumStorage _CurriculumStorage { get; set; }

        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name)),
                Component.For<IPlugin>().ImplementedBy<CurriculumManagementPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ICurriculumStorage>().ImplementedBy<CachedCurriculumStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ICurriculumStorage>().ImplementedBy<DatabaseCurriculumStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ICurriculumService>().ImplementedBy<CurriculumService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
            );

            // temporary hack
            _CurriculumStorage = new CachedCurriculumStorage(new DatabaseCurriculumStorage(container.Resolve<ILmsService>()), container.Resolve<ICacheProvider>());
            //_CurriculumStorage = container.Resolve<ICurriculumStorage>();
        }

        #endregion

        #region IPlugin Members
        public string GetName()
        {
            return Localization.getMessage("CurriculumManagement");
        }

        public IEnumerable<Action> BuildActions()
        {
            return new[]
            {
                new Action(Localization.getMessage("CurriculumManagement"), "Curriculum/Index")
            };
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new[]
            {
                new MenuItem(Localization.getMessage("Curriculums"), "Curriculum", "Index")
            };
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute(
                "Curriculum",
                "Curriculum/{CurriculumId}/{action}",
                new { controller = "Curriculum" }
            );

            routes.MapRoute(
                "Curriculums",
                "Curriculum/{action}",
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
            switch (evt)
            {
                case DisciplineNotifications.DisciplineDeleting:
                    //delete corresponding Curriculums
                    var disciplineId = ((Discipline)data[0]).Id;
                    var curriculumIds = _CurriculumStorage.GetCurriculums(c => c.DisciplineRef == disciplineId).Select(item => item.Id);
                    _CurriculumStorage.DeleteCurriculums(curriculumIds);
                    break;
                case DisciplineNotifications.ChapterCreated:
                    //add corresponding CurriculumChapters
                    var chapter = (Chapter)data[0];
                    var curriculums = _CurriculumStorage.GetCurriculums(c => c.DisciplineRef == chapter.DisciplineRef);
                    curriculums
                        .Select(curriculum => new CurriculumChapter
                            {
                                ChapterRef = chapter.Id,
                                CurriculumRef = curriculum.Id
                            })
                        .ToList()
                        .ForEach(item => _CurriculumStorage.AddCurriculumChapter(item));
                    break;
                case DisciplineNotifications.ChapterDeleting:
                    //delete corresponding curriculum chapters
                    var chapterId = ((Chapter)data[0]).Id;
                    var curriculumChapterIds = _CurriculumStorage.GetCurriculumChapters(item => item.ChapterRef == chapterId).Select(item => item.Id);
                    _CurriculumStorage.DeleteCurriculumChapters(curriculumChapterIds);
                    break;
                case DisciplineNotifications.TopicCreated:
                    //add corresponding curriculum chapter topics.
                    var topic = (Topic)data[0];
                    var curriculumChapters = _CurriculumStorage.GetCurriculumChapters(item => item.ChapterRef == topic.ChapterRef);
                    curriculumChapters
                        .Select(curriculumChapter => new CurriculumChapterTopic
                            {
                                CurriculumChapterRef = curriculumChapter.Id,
                                TopicRef = topic.Id,
                                MaxScore = Constants.DefaultTopicMaxScore,
                                BlockTopicAtTesting = false,
                                BlockCurriculumAtTesting = false
                            })
                        .ToList()
                        .ForEach(item => _CurriculumStorage.AddCurriculumChapterTopic(item));
                    break;
                case DisciplineNotifications.TopicDeleting:
                    //delete corresponding curriculum chapter topics
                    var topicId = ((Topic)data[0]).Id;
                    var curriculumChapterTopicIds = _CurriculumStorage.GetCurriculumChapterTopics(item => item.TopicRef == topicId)
                        .Select(item => item.Id)
                        .ToList();
                    _CurriculumStorage.DeleteCurriculumChapterTopics(curriculumChapterTopicIds);
                    break;
                case UserNotifications.GroupDelete:
                    //delete connected Curriculums:
                    var groupId = ((Group)data[0]).Id;
                    //curriculumStorage.MakeCurriculumsInvalid(groupId);
                    curriculumIds = _CurriculumStorage.GetCurriculums(c => c.UserGroupRef == groupId).Select(item => item.Id);
                    _CurriculumStorage.DeleteCurriculums(curriculumIds);
                    break;
            }
        }

        public void Setup(IWindsorContainer container)
        {

        }

        #endregion
    }
}