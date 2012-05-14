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
                var chapters = Storage.GetChapters(item => item.DisciplineRef == parentId);

                var partialViews = chapters.Select(chapter => PartialViewAsString("ChapterRow", chapter)).ToArray();

                return Json(new { success = true, items = partialViews });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int disciplineId)
        {
            return PartialView("Create", new Chapter());
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult Create(Chapter chapter, int disciplineId)
        {
            try
            {
                chapter.DisciplineRef = disciplineId;

                if (ModelState.IsValid)
                {
                    Storage.AddChapter(chapter);

                    return Json(new { success = true, disciplineId = chapter.DisciplineRef, chapterRow = PartialViewAsString("ChapterRow", chapter) });
                }

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
                    chapter = Storage.UpdateChapter(chapter);

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
