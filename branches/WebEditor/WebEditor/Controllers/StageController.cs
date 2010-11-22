using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEditor.Controllers
{
    public class StageController : BaseController
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(int curriculumId)
        {
            var courses = Storage.GetStages(curriculumId);

            if (courses != null)
            {
                return View(courses);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
