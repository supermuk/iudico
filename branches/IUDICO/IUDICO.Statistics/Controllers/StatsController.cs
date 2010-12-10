using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Controllers;

namespace IUDICO.Statistics.Controllers
{
    public class StatsController : PluginController
    {
        //
        // GET: /Stats/

        public ActionResult Index()
        {
            return View();
        }

    }
}
