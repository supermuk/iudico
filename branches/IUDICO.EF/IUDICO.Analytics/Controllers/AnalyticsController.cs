using System.Collections.Generic;
using System.Web.Mvc;
using IUDICO.Analytics.Models.Storage;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Shared;

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