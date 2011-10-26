using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models.Attributes;
using IUDICO.Analytics.Models.ViewDataClasses;
using IUDICO.Analytics.Models.Storage;
using IUDICO.Analytics.Models;

namespace IUDICO.Analytics.Controllers
{
    public class AnalyticsController : PluginController
    {
        //
        // GET: /Analytics/

        [Allow(Role = Role.Teacher)]
        public ActionResult Index()
        {
            IAnalyticsStorage Storage;
            Storage = new MixedAnalyticsStorage(LmsService);
            IEnumerable<ForecastingTree> query = Storage.GetAllForecastingTrees();
            return View(query);
        }

    }
}
