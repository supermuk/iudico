using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEditor.Controllers
{
    public class ThemeController : BaseController
    {

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(int stageId)
        {
            return View();
        }

    }
}
