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
using IUDICO.Common.Models.Shared;
using System.Linq;
using IUDICO.Statistics.ViewModels;

namespace IUDICO.Statistics.Controllers
{
    public class StatsController : PluginController
    {
        protected readonly IStatisticsProxy Proxy;

        protected readonly IStatisticsStorage Storage;

        public StatsController(IStatisticsProxy statisticsProxy, IStatisticsStorage statisticsStorage)
        {
            Proxy = statisticsProxy;
            Storage = statisticsStorage;
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index()
        {
            var groups = Storage.GetAllGroups().Select(group => new GroupViewModel(group.Id, group.Name));

            return View(groups);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult SelectCurriculums(int id)
        {
            // TODO: add group with id exists validation
            var groupName = Storage.GetGroupById(id).Name;
            var curriculumViewModels = Storage.GetCurrilulumsByGroupId(id).Select(curr => new CurriculumViewModel(curr.Id, curr.Name, curr.Created));

            var selectCurriculumsViewModel = new SelectCurriculumsViewModel(groupName, curriculumViewModels);

            HttpContext.Session["SelectedGroupId"] = id;

            return View(selectCurriculumsViewModel);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult ShowCurriculumStatistic(int[] selectCurriculumId)
        {
            var selectedGroup = LmsService.FindService<IUserService>().GetGroup((int)HttpContext.Session["SelectedGroupId"]);
            ViewData["selectGroupName"] = selectedGroup.Name;
            IEnumerable<User> users = LmsService.FindService<IUserService>().GetUsersByGroup(selectedGroup);
            SpecializedResultProxy srp = new SpecializedResultProxy();
            AllSpecializedResults allSpecRes = srp.GetResults(users, selectCurriculumId, LmsService);

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

        [Allow(Role = Role.Teacher)]
        [HttpGet]
        public JsonResult GetTotalUserCurriculum(Guid userId, int curriculumId)
        {
            try
            {
                var total = Storage.GetTotalForUserCurriculum(userId, curriculumId);

                return Json(new { isSuccess = true, total = total }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { isSuccess = false, message = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public JsonResult SaveManualResult(Guid userId, int themeId, double score)
        {
            try
            {
                Storage.SaveManualResult(userId, themeId, score);

                var curriculumId = Storage.GetCurriculumIdByThemeId(themeId);
                var total = Storage.GetTotalForUserCurriculum(userId, curriculumId);

                return Json(new { isSuccess = true, total = total });
            }
            catch (Exception exception)
            {
                return Json(new { isSuccess = false, message = exception.Message });
            }
        }
    }
}
