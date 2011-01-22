using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.CurriculumManagement.Models.Storage;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class StageTimelineController : CurriculumBaseController
    {
        public StageTimelineController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
