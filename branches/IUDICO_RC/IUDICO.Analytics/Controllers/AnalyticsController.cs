using System.Web.Mvc;
using IUDICO.Analytics.Models.Storage;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.Analytics.Controllers
{
    public class AnalyticsController : PluginController
    {
        private readonly IAnalyticsStorage storage;

        public AnalyticsController(IAnalyticsStorage analyticsStorage)
        {
            this.storage = analyticsStorage;
        }

        // GET: /Analytics/

        [Allow(Role = Role.Admin | Role.Teacher)]
        public ActionResult Index()
        {
            var query = this.storage.GetAllForecastingTrees();

            return View(query);
        }
    }
}