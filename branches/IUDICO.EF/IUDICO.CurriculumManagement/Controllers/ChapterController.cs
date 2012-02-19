using System;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class ChapterController : CurriculumBaseController
    {
        public ChapterController(ICurriculumStorage disciplineStorage)
            : base(disciplineStorage)
        {
            
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index(int disciplineId)
        {
            try
            {
                var chapters = Storage.GetChapters(disciplineId);

                ViewData["DisciplineName"] = Storage.GetDiscipline(disciplineId).Name;
                return View(chapters);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int disciplineId)
        {
            try
            {
                var discipline = Storage.GetDiscipline(disciplineId);

                ViewData["DisciplineName"] = discipline.Name;
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int disciplineId, Chapter chapter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    chapter.DisciplineRef = disciplineId;
                    Storage.AddChapter(chapter);

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(chapter);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int chapterId)
        {
            try
            {
                var chapter = Storage.GetChapter(chapterId);

                ViewData["DisciplineName"] = chapter.Discipline.Name;
                HttpContext.Session["DisciplineId"] = chapter.DisciplineRef;
                return View(chapter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int chapterId, Chapter chapter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    chapter.Id = chapterId;
                    Storage.UpdateChapter(chapter);

                    return RedirectToRoute("Chapters", new { action = "Index", DisciplineId = HttpContext.Session["DisciplineId"] });
                }
                else
                {
                    return View(chapter);
                }
            }
            catch (Exception e)
            {
                throw e;
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
