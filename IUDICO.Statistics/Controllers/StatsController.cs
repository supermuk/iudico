using System;
using System.Web.Mvc;
using System.Collections.Generic;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Controllers;
using IUDICO.Statistics.Models.Storage;
using IUDICO.Common.Models.Attributes;
using IUDICO.Statistics.Models.StatisticsModels;

namespace IUDICO.Statistics.Controllers
{
    public class StatsController : PluginController
    {
        private readonly IStatisticsProxy _Proxy;

        public StatsController(IStatisticsProxy statsStorage)
        {
            _Proxy = statsStorage;
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index()
        {
            var groups = _Proxy.GetAllGroups();

            return View(groups);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult SelectCurriculums(int id)
        {
            ViewData["Group"] = LmsService.FindService<IUserService>().GetGroup(id).Name;
            var curriculums = _Proxy.GetCurrilulumsByGroupId(id);
            HttpContext.Session["SelectedGroupId"] = id;
            return View(curriculums);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult ShowCurriculumStatistic(int[] selectCurriculumId)
        {
            ViewData["selectGroupName"] = LmsService.FindService<IUserService>().GetGroup((int)HttpContext.Session["SelectedGroupId"]).Name;
            IEnumerable<User> users = LmsService.FindService<IUserService>().GetUsersByGroup(LmsService.FindService<IUserService>().GetGroup((int)HttpContext.Session["SelectedGroupId"]));
            AllSpecializedResults allSpecRes = new AllSpecializedResults();
            SpecializedResultProxy srp = new SpecializedResultProxy();
            allSpecRes = srp.GetResults(users, selectCurriculumId, LmsService);

            return View(allSpecRes);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult ThemesInfo(Int32 curriculumId)
        {
            var model = new ThemeInfoModel((int)HttpContext.Session["SelectedGroupId"], curriculumId, LmsService);

            HttpContext.Session["Attempts"] = model.GetAllAttemts();

            return View(model);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult ThemeTestResults(long attemptId)
        {
            var model = new ThemeTestResultsModel(attemptId, (List<AttemptResult>)HttpContext.Session["Attempts"], LmsService);
            return View(model);
        }

        [Allow(Role = Role.Student)]
        [HttpGet]
        public ActionResult CurrentThemeTestResults(Int32 themeId)
        {
            var model = new CurrentThemeTestResultsModel(themeId, LmsService);
            return View(model);
        }
    }
}
