using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Analytics.Models.Quality;
using IUDICO.Analytics.Models.Storage;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Controllers
{
    public class QualityController : PluginController
    {
        private readonly IAnalyticsStorage storage;

        public QualityController(IAnalyticsStorage analyticsStorage)
        {
            this.storage = analyticsStorage;
        }

        [Allow(Role = Role.Admin | Role.Teacher)]
        public ActionResult Index()
        {
            IEnumerable<Discipline> allowedDisciplines;
            allowedDisciplines = LmsService.FindService<IDisciplineService>().GetDisciplines();

            return View(allowedDisciplines);
        }

        [Allow(Role = Role.Admin | Role.Teacher)]
        [HttpPost]
        public ActionResult ShowDiscipline(long selectDisciplineId)
        {
            IEnumerable<Topic> temp_allowedTopics = LmsService.FindService<IDisciplineService>().GetTopicsByDisciplineId((int)selectDisciplineId);
            var disciplineName = LmsService.FindService<IDisciplineService>().GetDiscipline((int)selectDisciplineId).Name;
            var groups = LmsService.FindService<IUserService>().GetGroups();
            double disciplineQuality = 0;
            var allowedTopics = new List<KeyValuePair<Topic, double>>();
            if (temp_allowedTopics != null & temp_allowedTopics.Count() != 0)
            {
                double tempDisciplineQuality = 0;
                var temp = new List<KeyValuePair<Topic, double>>();
                foreach (var topic in temp_allowedTopics)
                {
                    List<double> quality = new List<double>();
                    quality.Add(this.storage.GetScoreRatingTopicStatistic(topic, groups));
                    quality.Add(this.storage.GaussianDistribution(topic));
                    tempDisciplineQuality += quality.Sum() / quality.Count;
                    temp.Add(new KeyValuePair<Topic, double>(topic, quality.Sum() / quality.Count));
                }
                disciplineQuality = tempDisciplineQuality / temp.Count;
                allowedTopics = temp;
            }

            DisciplineModel model = new DisciplineModel(allowedTopics, disciplineName, disciplineQuality);
            return View(model);
        }

    }
}
