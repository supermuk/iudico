using System.Web.Mvc;
using System.Collections.Generic;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Controllers;
using IUDICO.Statistics.Models.Storage;
using IUDICO.Common.Models.Attributes;
using IUDICO.Statistics.Models.StatisticsModels;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Controllers
{
    public class StatsController : PluginController
    {
        private readonly IStatisticsProxy proxy;

        public StatsController(IStatisticsProxy statsStorage)
        {
            this.proxy = statsStorage;
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index()
        {
            var groups = this.proxy.GetAllGroups();

            return View(groups);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult SelectDisciplines(int id)
        {
            ViewData["Group"] = LmsService.FindService<IUserService>().GetGroup(id).Name;
            var curriculums = this.proxy.GetCurrilulumsByGroupId(id);
            HttpContext.Session["SelectedGroupId"] = id;
            return View(curriculums);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult ShowDisciplineStatistic(int[] selectedCurriculumId)
        {
            var selectedGroup = LmsService.FindService<IUserService>().GetGroup((int)HttpContext.Session["SelectedGroupId"]);
            ViewData["selectGroupName"] = selectedGroup.Name;
            IEnumerable<User> users = LmsService.FindService<IUserService>().GetUsersByGroup(selectedGroup);
            var srp = new SpecializedResultProxy();
            AllSpecializedResults allSpecRes = srp.GetResults(users, selectedCurriculumId, LmsService);

            return View(allSpecRes);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult TopicsInfo(int curriculumId)
        {
            var model = new TopicInfoModel((int)HttpContext.Session["SelectedGroupId"], curriculumId, LmsService);

            HttpContext.Session["Attempts"] = model.AllAttempts;

            return View(model);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult TopicTestResults(long attemptId)
        {
            var model = new TopicTestResultsModel(attemptId, (List<AttemptResult>)HttpContext.Session["Attempts"], LmsService);
            return View(model);
        }

        [Allow(Role = Role.Student)]
        [HttpGet]
        public ActionResult CurrentTopicTestResults(int curriculumChapterTopicId, TopicTypeEnum topicType)
        {
            var model = new CurrentTopicTestResultsModel(curriculumChapterTopicId, topicType, LmsService);
            return View(model);
        }
    }
}
