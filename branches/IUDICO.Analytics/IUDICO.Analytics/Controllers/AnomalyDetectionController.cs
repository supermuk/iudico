using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Analytics.Models.Storage;
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
        public ActionResult TrainTopic(int id)
        {
            var studentsAndMarks = _Storage.GetStudentListForTraining(id);
            HttpContext.Session["StudentsAndMarks"] = studentsAndMarks;
            var list = studentsAndMarks.ToList();
            return View(list.GetRange(0, 21));
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult TrainAlg(string[] tsNormal, string[] tsAnomalies)
        {
            var studentsAndMarks = (IEnumerable<KeyValuePair<User, double[]>>)HttpContext.Session["StudentsAndMarks"];
            TrainingSet[] tr = TrainingSetsCreator.generateTrainingSets(studentsAndMarks, tsNormal, tsAnomalies);
            return View(AnomalyDetectionAlgorithm.runAlg(studentsAndMarks, tr[0], tr[1], tr[2]));
        }
    }
}
