using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Models.Services;
using IUDICO.Statistics.Models.QualityTest;

namespace IUDICO.Statistics.Controllers
{
    public class QualityTestController : PluginController
    {

        [Allow(Role = Role.Teacher)]
        public ActionResult Index()
        {
            IndexModel model = new IndexModel(LmsService);
            HttpContext.Session["TeacherUserName"] = model.GetTeacherUserName();
            return View(model);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult SelectTheme(long selectCurriculumId)
        {
            SelectThemeModel model = new SelectThemeModel(LmsService, selectCurriculumId, (String)HttpContext.Session["TeacherUserName"]);
            HttpContext.Session["CurriculumName"] = model.GetCurriculumName();
            return View(model);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult SelectGroups(int selectThemeId)
        {
            SelectGroupsModel model = new SelectGroupsModel(LmsService, selectThemeId, (String)HttpContext.Session["TeacherUserName"],
                (String)HttpContext.Session["CurriculumName"]);
            HttpContext.Session["ThemeName"] = model.GetThemeName();
            HttpContext.Session["ThemeId"] = selectThemeId;
            return View(model);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult ShowQualityTest(int[] selectGroupIds)
        {
            ShowQualityTestModel model = new ShowQualityTestModel(LmsService, selectGroupIds, (String)HttpContext.Session["TeacherUserName"],
                (String)HttpContext.Session["CurriculumName"], (int)HttpContext.Session["ThemeId"]);
            return View(model);
        }
    }
}
