using System.Web.Mvc;
using IUDICO.Analytics.Models.Storage;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Controllers
{
    public class FeaturesController : PluginController
    {
        private readonly IAnalyticsStorage _Storage;

        public FeaturesController(IAnalyticsStorage analyticsStorage)
        {
            _Storage = analyticsStorage;
        }

        //
        // GET: /Features/

        [Allow(Role = Role.Admin)]
        public ActionResult Index()
        {
            return View(_Storage.GetFeatures());
        }

        //
        // GET: /Features/Details/5

        [Allow(Role = Role.Admin)]
        public ActionResult Details(int id)
        {
            return View(_Storage.GetFeatureDetails(id));
        }

        //
        // GET: /Features/Create

        [Allow(Role = Role.Admin)]
        public ActionResult Create()
        {
            var feature = new Feature();

            return View(feature);
        }

        //
        // POST: /Features/Create

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult Create(Feature feature)
        {
            if (ModelState.IsValid)
            {
                _Storage.CreateFeature(feature);

                return RedirectToAction("Index");
            }

            return View(feature);
        }

        //
        // GET: /Features/Edit/5

        [Allow(Role = Role.Admin)]
        public ActionResult Edit(int id)
        {
            return View(_Storage.GetFeature(id));
        }

        //
        // POST: /Features/Edit/5

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult Edit(int id, Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return View(feature);
            }

            _Storage.EditFeature(id, feature);

            return RedirectToAction("Index");
        }

        //
        // DELETE: /Features/Delete/5

        [HttpDelete]
        [Allow(Role = Role.Admin)]
        public ActionResult Delete(int id)
        {
            try
            {
                _Storage.DeleteFeature(id);

                return Json(new {status = true});
            }
            catch
            {
                return Json(new {status = false});
            }
        }

        [Allow(Role = Role.Admin)]
        public ActionResult AddTopic(int id)
        {
            var features =_Storage.GetFeatureDetailsWithTopics(id);

            return View(features);
        }
    }
}