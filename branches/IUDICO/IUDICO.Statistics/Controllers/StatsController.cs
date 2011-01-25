using System;
using System.Web.Mvc;
using System.Collections.Generic;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Controllers;
using IUDICO.Statistics.Models.Storage;
using IUDICO.Common.Models.Attributes;

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
            
                var curriculums = _Proxy.GetCurrilulumsByGroupId(id);

                HttpContext.Session["SelectedGroupId"] = id;

                return View(curriculums);
            
            
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult ShowCurriculumStatistic(int[] selectCurriculumId)
        {
             var users = LmsService.FindService<IUserService>().GetUsersByGroup(LmsService.FindService<IUserService>().GetGroup((int)HttpContext.Session["SelectedGroupId"]));
             var allSpecializedResults = new AllSpecializedResults(users, selectCurriculumId, LmsService);
            
            return View(allSpecializedResults);            
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
        public ActionResult ThemeTestResaults(String attemptUsernameAndTheme)
        {
            var model = new ThemeTestResaultsModel(attemptUsernameAndTheme, (List<AttemptResult>)HttpContext.Session["Attempts"], LmsService);
            return View(model);
        }
    }
}
