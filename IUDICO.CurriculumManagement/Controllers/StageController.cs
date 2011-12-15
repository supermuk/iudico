using System;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class StageController : CurriculumBaseController
    {
        public StageController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {
            
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index(int curriculumId)
        {
            try
            {
                var stages = Storage.GetStages(curriculumId);

                ViewData["CurriculumName"] = Storage.GetCurriculum(curriculumId).Name;
                return View(stages);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int curriculumId)
        {
            try
            {
                var curriculum = Storage.GetCurriculum(curriculumId);

                ViewData["CurriculumName"] = curriculum.Name;
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int curriculumId, Stage stage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    stage.CurriculumRef = curriculumId;
                    Storage.AddStage(stage);

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(stage);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int stageId)
        {
            try
            {
                var stage = Storage.GetStage(stageId);

                ViewData["CurriculumName"] = stage.Curriculum.Name;
                HttpContext.Session["CurriculumId"] = stage.CurriculumRef;
                return View(stage);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int stageId, Stage stage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    stage.Id = stageId;
                    Storage.UpdateStage(stage);

                    return RedirectToRoute("Stages", new { action = "Index", CurriculumId = HttpContext.Session["CurriculumId"] });
                }
                else
                {
                    return View(stage);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult DeleteItem(int stageId)
        {
            try
            {
                Storage.DeleteStage(stageId);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult DeleteItems(int[] stageIds)
        {
            try
            {
                Storage.DeleteStages(stageIds);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
    }
}
