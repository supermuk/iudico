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
        // protected ICurriculumStorage _CurriculumStorage { get; set; }
        protected IWindsorContainer container;

        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name)),
                Component.For<IPlugin>().Instance(this).LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ICurriculumStorage>().ImplementedBy<CachedCurriculumStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ICurriculumStorage>().ImplementedBy<DatabaseCurriculumStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ICurriculumService>().ImplementedBy<CurriculumService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton));

            // temporary hack
            this.container = container;
            // _LmsService = container.Resolve<ILmsService>();
            // _CurriculumStorage = new CachedCurriculumStorage(new DatabaseCurriculumStorage(container.Resolve<ILmsService>()), container.Resolve<ICacheProvider>());
            // _CurriculumStorage = container.Resolve<ICurriculumStorage>();
        }

        #endregion

        #region IPlugin Members
        public string GetName()
        {
            return Localization.GetMessage("CurriculumManagement");
        }

        public IEnumerable<Action> BuildActions()
        {
            return new[]
            {
                new Action(Localization.GetMessage("CurriculumManagement"), "Curriculum/Index")
            };
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new[]
            {
                new MenuItem(Localization.GetMessage("Curriculums"), "Curriculum", "Index")
            };
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute(
                "Curriculum",
                "Curriculum/{CurriculumId}/{action}",
                new { controller = "Curriculum" });

            routes.MapRoute(
                "Curriculums",
                "Curriculum/{action}",
                new { controller = "Curriculum" });

            routes.MapRoute(
                "CurriculumChapter",
                "CurriculumChapter/{CurriculumChapterId}/{action}",
                new { controller = "CurriculumChapter" });

            routes.MapRoute(
                "CurriculumChapters",
                "Curriculum/{CurriculumId}/CurriculumChapter/{action}",
                new { controller = "CurriculumChapter" });

            routes.MapRoute(
                "CurriculumChapterTopic",
                "CurriculumChapterTopic/{CurriculumChapterTopicId}/{action}",
                new { controller = "CurriculumChapterTopic" });

            routes.MapRoute(
                "CurriculumChapterTopics",
                "CurriculumChapter/{CurriculumChapterId}/CurriculumChapterTopic/{action}",
                new { controller = "CurriculumChapterTopic" });
        }

        public void Update(string evt, params object[] data)
        {
            var curriculumStorage = this.container.Resolve<ICurriculumStorage>();

            switch (evt)
            {  
                case DisciplineNotifications.DisciplineIsValidChange:
                    // makes corresponding Curriculums invalid
                    var disciplineId = ((Discipline)data[0]).Id;
                    var curriculumIds = curriculumStorage.GetCurriculums(c => c.DisciplineRef == disciplineId).Select(item => item.Id);
                    curriculumStorage.ChangeCurriculumsIsValid(curriculumIds, ((Discipline)data[0]).IsValid);
                    break;
                case DisciplineNotifications.DisciplineDeleting:
                    // delete corresponding Curriculums
                    disciplineId = ((Discipline)data[0]).Id;
                    curriculumIds = curriculumStorage.GetCurriculums(c => c.DisciplineRef == disciplineId).Select(item => item.Id);
                    curriculumStorage.DeleteCurriculums(curriculumIds);
                    break;
                case DisciplineNotifications.ChapterCreated:
                    // add corresponding CurriculumChapters
                    var chapter = (Chapter)data[0];
                    var curriculums = curriculumStorage.GetCurriculums(c => c.DisciplineRef == chapter.DisciplineRef);
                    curriculums
                        .Select(curriculum => new CurriculumChapter
                            {
                                ChapterRef = chapter.Id,
                                CurriculumRef = curriculum.Id
                            })
                        .ToList()
                        .ForEach(item => curriculumStorage.AddCurriculumChapter(item));
                    break;
                case DisciplineNotifications.ChapterDeleting:
                    // delete corresponding curriculum chapters
                    var chapterId = ((Chapter)data[0]).Id;
                    var curriculumChapterIds = curriculumStorage.GetCurriculumChapters(item => item.ChapterRef == chapterId).Select(item => item.Id);
                    curriculumStorage.DeleteCurriculumChapters(curriculumChapterIds);
                    break;
                case DisciplineNotifications.TopicCreated:
                    // add corresponding curriculum chapter topics.
                    var topic = (Topic)data[0];
                    var curriculumChapters = curriculumStorage.GetCurriculumChapters(item => item.ChapterRef == topic.ChapterRef);
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
                        .ForEach(item => curriculumStorage.AddCurriculumChapterTopic(item));
                    break;
                case DisciplineNotifications.TopicDeleting:
                    // delete corresponding curriculum chapter topics
                    var topicId = ((Topic)data[0]).Id;
                    var curriculumChapterTopicIds = curriculumStorage.GetCurriculumChapterTopics(item => item.TopicRef == topicId)
                        .Select(item => item.Id)
                        .ToList();
                    curriculumStorage.DeleteCurriculumChapterTopics(curriculumChapterTopicIds);
                    break;
                case UserNotifications.GroupDelete:
                    // make connected Curriculums invalid:
                    var groupId = ((Group)data[0]).Id;
                    curriculumIds = curriculumStorage.GetCurriculums(c => c.UserGroupRef == groupId).Select(item => item.Id);
                    curriculumStorage.ChangeCurriculumsIsValid(curriculumIds, false);
                    break;
            }
        }

        public void Setup(IWindsorContainer container)
        {

        }

        #endregion
    }
}