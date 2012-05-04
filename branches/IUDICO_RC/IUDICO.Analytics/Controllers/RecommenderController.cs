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

        [Allow(Role = Role.Admin)]
        public ActionResult Index()
        {
            return View();
        }

        [Allow(Role = Role.Admin)]
        public ActionResult TopicScores()
        {
            return View(this.storage.GetTopicScores());
        }

        [Allow(Role = Role.Admin)]
        public ActionResult UserScores()
        {
            return View(this.storage.GetUserScores());
        }

        [Allow(Role = Role.Admin)]
        public ActionResult UpdateUser(Guid id)
        {
            this.storage.UpdateUserScores(id);

            return RedirectToAction("UserScores");
        }

        [Allow(Role = Role.Admin)]
        public ActionResult UpdateAllUsers()
        {
            this.storage.UpdateAllUserScores();

            return RedirectToAction("UserScores");
        }

        [Allow(Role = Role.Admin)]
        public ActionResult UpdateTopic(int id)
        {
            this.storage.UpdateTopicScores(id);

            return RedirectToAction("TopicScores");
        }

        [Allow(Role = Role.Admin)]
        public ActionResult UpdateAllTopics()
        {
            this.storage.UpdateAllTopicScores();

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

            var roles = LmsService.FindService<IUserService>().GetCurrentUserRoles();

            if (!roles.Contains(Role.Student))
            {
                return new EmptyResult();
            }

            var topics = this.storage.GetRecommenderTopics(user);

            return PartialView(topics);
        }
    }
}
