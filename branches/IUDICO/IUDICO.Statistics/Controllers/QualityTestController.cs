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
        public ActionResult SelectDiscipline()
        {
            IndexModel model = new IndexModel(LmsService);
            HttpContext.Session["TeacherUserName"] = model.GetTeacherUserName();
            return View(model);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult SelectTopic(long selectDisciplineId)
        {
            SelectTopicModel model = new SelectTopicModel(LmsService, selectDisciplineId, (String)HttpContext.Session["TeacherUserName"]);
            HttpContext.Session["DisciplineName"] = model.GetDisciplineName();
            return View(model);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult SelectGroups(int selectTopicId)
        {
            SelectGroupsModel model = new SelectGroupsModel(LmsService, selectTopicId, (String)HttpContext.Session["TeacherUserName"],
                (String)HttpContext.Session["DisciplineName"]);
            HttpContext.Session["TopicName"] = model.GetTopicName();
            HttpContext.Session["TopicId"] = selectTopicId;
            return View(model);
        }

        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult ShowQualityTest(int[] selectGroupIds)
        {
            ShowQualityTestModel model = new ShowQualityTestModel(LmsService, selectGroupIds,
                (String)HttpContext.Session["DisciplineName"], (int)HttpContext.Session["TopicId"]);
            return View(model);
        }
    }
}
