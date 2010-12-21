using System.Web.Mvc;
using IUDICO.Common.Controllers;
using System.Collections.Generic;
using IUDICO.Common.Models;
using System;

namespace IUDICO.LMS.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public HomeController()
        {

        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
