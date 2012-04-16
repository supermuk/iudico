using System;
using System.Linq;
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

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult ViewChapters(int parentId)
        {
            try
            {
                ViewData["DisciplineId"] = parentId;

                var chapters = Storage.GetChapters(item => item.DisciplineRef == parentId);

                var partialViews = chapters.Select(chapter => PartialViewAsString("ChapterRow", chapter)).ToArray();

                return Json(new { success = true, items = partialViews });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
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

            return PartialView("Create", new Chapter());
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult Create(Chapter chapter, int DisciplineRef)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    chapter.DisciplineRef = DisciplineRef;
                    Storage.AddChapter(chapter);

                    return Json(new { success = true, disciplineId = chapter.DisciplineRef, chapterRow = PartialViewAsString("ChapterRow", chapter) });
                }

                SaveValidationErrors();

                return Json(new { success = false, disciplineId = chapter.DisciplineRef, html = PartialViewAsString("Create", chapter) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, html = ex.Message });
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int chapterId)
        {
            var chapter = Storage.GetChapter(chapterId);

            ViewData["DisciplineName"] = chapter.Discipline.Name;
            Session["DisciplineId"] = chapter.DisciplineRef;

            return PartialView("Edit", chapter);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult Edit(int chapterId, Chapter chapter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    chapter.Id = chapterId;
                    Storage.UpdateChapter(chapter);

                    return Json(new { success = true, chapterId = chapterId, chapterRow = PartialViewAsString("ChapterRow", chapter) });
                }
                return Json(new { success = false, chapterId = chapterId, html = PartialViewAsString("Edit", chapter) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, html = ex.Message });
            }

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
