using System;
using System.Linq;
using System.Web.Mvc;
using IUDICO.Analytics.Models.Storage;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models.Services;

namespace IUDICO.Analytics.Controllers
{
    public class RecommenderController : PluginController
    {
        private readonly IAnalyticsStorage _Storage;

        public RecommenderController(IAnalyticsStorage analyticsStorage)
        {
            _Storage = analyticsStorage;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopicScores()
        {
            return View(_Storage.GetTopicScores());
        }

        public ActionResult UserScores()
        {
            return View(_Storage.GetUserScores());
        }

        public ActionResult UpdateUser(Guid id)
        {
            _Storage.UpdateUserScores(id);

            return RedirectToAction("UserScores");
        }

        public ActionResult UpdateTopic(int id)
        {
            _Storage.UpdateTopicScores(id);

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

            var topics = _Storage.GetRecommenderTopics(user);
            var topicDescriptions = LmsService.FindService<ICurriculumService>().GetTopicDescriptionsByTopics(topics.Select(t => t.Topic), user);

            return PartialView(topicDescriptions);
        }
    }
}
