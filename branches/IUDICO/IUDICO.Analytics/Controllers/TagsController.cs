using System.Linq;
using System.Web.Mvc;
using IUDICO.Analytics.Models.Storage;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Controllers
{
    public class TagsController : PluginController
    {
        private readonly IAnalyticsStorage storage;

        public TagsController(IAnalyticsStorage analyticsStorage)
        {
            this.storage = analyticsStorage;
        }

        // GET: /Features/

        [Allow(Role = Role.Admin)]
        public ActionResult Index()
        {
            return View(this.storage.GetTags());
        }

        // GET: /Features/Details/5

        [Allow(Role = Role.Admin)]
        public ActionResult Details(int id)
        {
            return View(this.storage.GetTagDetails(id));
        }

        // GET: /Features/Create

        [Allow(Role = Role.Admin)]
        public ActionResult Create()
        {
            var tag = new Tag();

            return View(tag);
        }

        // POST: /Features/Create

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                this.storage.CreateTag(tag);

                return RedirectToAction("Index");
            }

            return View(tag);
        }

        // GET: /Features/Edit/5

        [Allow(Role = Role.Admin)]
        public ActionResult Edit(int id)
        {
            return View(this.storage.GetTag(id));
        }

        // POST: /Features/Edit/5

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult Edit(int id, Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View(tag);
            }

            this.storage.EditTag(id, tag);

            return RedirectToAction("Index");
        }

        // DELETE: /Features/Delete/5

        [HttpDelete]
        [Allow(Role = Role.Admin)]
        public ActionResult Delete(int id)
        {
            try
            {
                this.storage.DeleteTag(id);

                return Json(new { status = true });
            }
            catch
            {
                return Json(new { status = false });
            }
        }

        [Allow(Role = Role.Admin)]
        public ActionResult EditTopics(int id)
        {
            var features = this.storage.GetTagDetailsWithTopics(id);

            return View(features);
        }

        [Allow(Role = Role.Admin)]
        [HttpPost]
        public ActionResult EditTopics(int id, FormCollection form)
        {
            var topics = string.IsNullOrEmpty(form["topics"]) ? Enumerable.Empty<int>() : form["topics"].Split(',').Select(i => int.Parse(i));

            this.storage.EditTags(id, topics);

            return RedirectToAction("Index");
        }
    }
}