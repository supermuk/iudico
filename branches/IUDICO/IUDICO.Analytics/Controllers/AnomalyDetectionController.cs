using System;
using System.Web.Mvc;
using IUDICO.Analytics.Models.Storage;
using IUDICO.Analytics.Models.AnomalyDetectionAlg;
using IUDICO.Analytics.Models.DecisionTrees;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.Analytics.Controllers
{
    public class AnomalyDetectionController : PluginController
    {
        private readonly IAnalyticsStorage storage;

        public AnomalyDetectionController(IAnalyticsStorage analyticsStorage)
        {
            this.storage = analyticsStorage;
        }

        // GET: /AnomalyDetection/

        [Allow(Role = Role.Teacher)]
        public ActionResult Index()
        {
            return View(this.storage.AvailebleTopics());
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult SelectGroup(int topicId)
        {
            var availableGroups = this.storage.AvailableGroups(topicId);
            
            HttpContext.Session["TopicId"] = topicId;

            ViewData["ShowError"] = null;

            return View(availableGroups);
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult TrainTopic(int groupId)
        {
            var topicId = (int)HttpContext.Session["TopicId"];
            var studentsAndMarks = this.storage.GetStudentListForTraining(topicId, groupId);

            ViewData["ShowError"] = null;

            if (HttpContext.Session["ShowError"] != null)
            {
                HttpContext.Session["ShowError"] = null;
                ViewData["ShowError"] = true;
            }

            return View(studentsAndMarks);
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult TrainAlg(string[] tsNormal, string[] tsAnomalies)
        {
            var topicId = (int)HttpContext.Session["TopicId"];
            var studentsAndMarks = this.storage.GetAllStudentListForTraining(topicId);
            TrainingSet[] tr;

            try
            {
                tr = TrainingSetsCreator.GenerateTrainingSets(studentsAndMarks, tsNormal, tsAnomalies);
            }
            catch (Exception)
            {
                HttpContext.Session["ShowError"] = true;
                return RedirectToAction("TrainTopic", "AnomalyDetection", new { id = (int)HttpContext.Session["TopicId"] });
            }

            ViewData["SkillTags"] = this.storage.GetTags();
            return View(AnomalyDetectionAlgorithm.RunAlg(studentsAndMarks, tr[0], tr[1], tr[2]));
        }
    }
}
