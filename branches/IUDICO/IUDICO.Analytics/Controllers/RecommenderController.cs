using System;
using System.Web.Mvc;
using IUDICO.Analytics.Models.Storage;
using IUDICO.Common.Controllers;

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
    }
}
