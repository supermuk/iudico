using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEditor.Controllers
{
    public class StageController : BaseController
    {
        public ActionResult Index(int curriculumId)
        {
            var stages = Storage.GetStages(curriculumId);

            if (stages != null)
            {
                return View(stages);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
