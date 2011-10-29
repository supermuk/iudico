using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Controllers;
using System.Web.Mvc;

namespace IUDICO.Security.Controllers
{
    public class BanController : PluginController
    {
        public ActionResult Index()
        {
            return View();
        }


    }
}