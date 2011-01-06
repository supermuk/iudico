using System;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Controllers;
using IUDICO.Statistics.Models;
using IUDICO.Statistics.Models.Storage;

namespace IUDICO.Statistics.Controllers
{
    public class StatsController : PluginController
    {
        //Roma
        private readonly IStatisticsProxy _proxy;

        public StatsController(IStatisticsProxy statsStorage)
        {
            _proxy = statsStorage;
        }

        

        public ActionResult Index()
        {
            IEnumerable<Group> groups = _proxy.GetAllGroups();

            return View(groups);
        }

        [HttpPost]
        public ActionResult SelectCurriculums(int id)
        {
            IEnumerable<Curriculum> curriculums = _proxy.GetCurrilulumsByGroupId(id);
            HttpContext.Session["SelectedGroupId"] = id;

            return View(curriculums);
        }

        [HttpPost]
        public ActionResult ShowCurriculumStatistic(int[] selectCurriculumId)
        {
            IEnumerable<User> users = LmsService.FindService<IUserService>().GetUsersByGroup(LmsService.FindService<IUserService>().GetGroup((int)HttpContext.Session["SelectedGroupId"]));
            AllSpecializedResults allSpecializedResults = new AllSpecializedResults(users, selectCurriculumId, LmsService);

            return View(allSpecializedResults);
        }

        // Vitalik
        [HttpPost]
        public ActionResult ThemesInfo(Int32 CurriculumID)
        {
            ThemeInfoModel model = new ThemeInfoModel((int)HttpContext.Session["SelectedGroupId"], CurriculumID, LmsService);
            HttpContext.Session["Attempts"] = model.GetAllAttemts();
            return View(model);
        }
        [HttpPost]
        public ActionResult ThemeTestResaults(Int32 AttemptId)
        {
            ThemeTestResaultsModel model = new ThemeTestResaultsModel(AttemptId, (List<AttemptResult>)HttpContext.Session["Attempts"], LmsService);
            return View(model);
        }
    }
}
