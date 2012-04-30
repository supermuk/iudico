using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Analytics.Models.Quality;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.Analytics.Controllers
{
    public class QualityController : PluginController
    {
        [Allow(Role = Role.Teacher)]
        public ActionResult Index()
        {
            DisciplineModel model = new DisciplineModel(LmsService);
            return View(model);
        }
        
        [Allow(Role = Role.Teacher)]
        [HttpPost]
        public ActionResult SelectDiscipline(long selectDisciplineId)
        {
            TopicModel model = new TopicModel(LmsService, selectDisciplineId);
            return View(model);
        }

    }
}
