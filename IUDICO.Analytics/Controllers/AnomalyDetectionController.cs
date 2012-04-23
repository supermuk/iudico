using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Analytics.Models.Storage;
using IUDICO.Analytics.Models;
using IUDICO.Analytics.Models.AnomalyDetectionAlg;
using IUDICO.Analytics.Models.DecisionTrees;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.Analytics.Controllers
{
    public class AnomalyDetectionController : PluginController
    {
        private readonly IAnalyticsStorage _Storage;

        public AnomalyDetectionController(IAnalyticsStorage analyticsStorage)
        {
            _Storage = analyticsStorage;
        }


        //
        // GET: /AnomalyDetection/

        [Allow(Role = Role.Teacher)]
        public ActionResult Index()
        {
            return View(_Storage.AvailebleTopics());
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult SelectGroup(int topicId)
        {
            var availableGroups = _Storage.AvailebleGroups(topicId);
            HttpContext.Session["TopicId"] = topicId;
            ViewData["ShowError"] = null;
            return View(availableGroups);
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult TrainTopic(int groupId)
        {
            int topicId = (int)HttpContext.Session["TopicId"];
            var studentsAndMarks = _Storage.GetStudentListForTraining(topicId, groupId);
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
            int topicId = (int)HttpContext.Session["TopicId"];
            var studentsAndMarks = _Storage.GetAllStudentListForTraining(topicId);
            TrainingSet[] tr;

            try
            {
                tr = TrainingSetsCreator.generateTrainingSets(studentsAndMarks, tsNormal, tsAnomalies);
            }
            catch (Exception ex)
            {
                HttpContext.Session["ShowError"] = true;
                return RedirectToAction("TrainTopic", "AnomalyDetection", new { id = (int)HttpContext.Session["TopicId"] });
            }

            ViewData["SkillTags"] = _Storage.GetTags();
            return View(AnomalyDetectionAlgorithm.runAlg(studentsAndMarks, tr[0], tr[1], tr[2]));
        }
    }
}
