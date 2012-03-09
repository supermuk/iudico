using System;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Attributes;
using IUDICO.DisciplineManagement.Models.Storage;

namespace IUDICO.DisciplineManagement.Controllers
{
    public class ChapterController : DisciplineBaseController
    {
        public ChapterController(IDisciplineStorage disciplineStorage)
            : base(disciplineStorage)
        {

        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index(int disciplineId)
        {
            var chapters = Storage.GetChapters(item=>item.DisciplineRef== disciplineId);
            ViewData["DisciplineName"] = Storage.GetDiscipline(disciplineId).Name;
            return View(chapters);
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int disciplineId)
        {
            var discipline = Storage.GetDiscipline(disciplineId);

            ViewData["DisciplineName"] = discipline.Name;
            return View();
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int disciplineId, Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                chapter.DisciplineRef = disciplineId;
                Storage.AddChapter(chapter);

                return RedirectToAction("Index");
            }
            return View(chapter);
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int chapterId)
        {
            var chapter = Storage.GetChapter(chapterId);

            ViewData["DisciplineName"] = chapter.Discipline.Name;
            Session["DisciplineId"] = chapter.DisciplineRef;
            return View(chapter);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int chapterId, Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                chapter.Id = chapterId;
                Storage.UpdateChapter(chapter);

                return RedirectToRoute("Chapters", new { action = "Index", DisciplineId = HttpContext.Session["DisciplineId"] });
            }
            return View(chapter);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult DeleteItem(int chapterId)
        {
            try
            {
                Storage.DeleteChapter(chapterId);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult DeleteItems(int[] chapterIds)
        {
            try
            {
                Storage.DeleteChapters(chapterIds);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
    }
}
