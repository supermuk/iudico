﻿using System.Web.Mvc;
using IUDICO.Common.Controllers;
using System.Collections.Generic;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models;

namespace IUDICO.LMS.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        /*private ILmsService _lmsService;

        public HomeController(ILmsService lmsService)
        {
            _lmsService = lmsService;
        }*/

        public ActionResult Index()
        {
            return View(new Dictionary<IPlugin, IEnumerable<Action>>(MvcApplication.Actions));
        }
    }
}
