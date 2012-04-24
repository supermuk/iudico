using System;
using System.Web.Mvc;
using IUDICO.Analytics.Models.Storage;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models;

namespace IUDICO.Analytics.Controllers
{
    using System.Linq;

    using IUDICO.Common.Models.Services;

    public class RecommenderController : PluginController
    {
        private readonly IAnalyticsStorage storage;

        public RecommenderController(IAnalyticsStorage analyticsStorage)
        {
            this.storage = analyticsStorage;
        }

        [Allow(Role = Role.Student)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopicScores()
        {
            return View(this.storage.GetTopicScores());
        }

        public ActionResult UserScores()
        {
            return View(this.storage.GetUserScores());
        }

        public ActionResult UpdateUser(Guid id)
        {
            this.storage.UpdateUserScores(id);

            return RedirectToAction("UserScores");
        }

        public ActionResult UpdateTopic(int id)
        {
            this.storage.UpdateTopicScores(id);

            return RedirectToAction("TopicScores");
        }

        [ChildActionOnly]
        public ActionResult RecommendedTopics()
        {
            var user = LmsService.FindService<IUserService>().GetCurrentUser();

            if (user == null)
            {
                return new EmptyResult();
            }

            var topics = this.storage.GetRecommenderTopics(user);
            var topicDescriptions = LmsService.FindService<ICurriculumService>().GetTopicDescriptionsByTopics(topics.Select(t => t.Topic), user);

            return PartialView(topicDescriptions);
        }
    }
}
