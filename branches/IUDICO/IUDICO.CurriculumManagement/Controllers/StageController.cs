using System;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.Storage;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class StageController : CurriculumBaseController
    {
        public StageController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

        }

        public ActionResult Index(int curriculumId)
        {
            try
            {
                var stages = Storage.GetStages(curriculumId);

                if (stages != null)
                {
                    ViewData["CurriculumName"] = Storage.GetCurriculum(curriculumId).Name;
                    return View(stages);
                }
                else
                {
                    throw new Exception("Cannot read records");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult Create(int curriculumId)
        {
            try
            {
                var curriculum = Storage.GetCurriculum(curriculumId);

                if (curriculum != null)
                {
                    return View();
                }
                else
                {
                    throw new Exception("Curriculum doesn't exist!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Create(int curriculumId, Stage stage)
        {
            try
            {
                stage.CurriculumRef = curriculumId;

                Storage.AddStage(stage);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult Edit(int stageId)
        {
            try
            {
                var stage = Storage.GetStage(stageId);

                if (stage != null)
                {
                    HttpContext.Session["CurriculumId"] = stage.CurriculumRef;
                    return View(stage);
                }
                else
                {
                    throw new Exception("Stage doesn't exist!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Edit(int stageId, Stage stage)
        {
            try
            {
                stage.Id = stageId;
                Storage.UpdateStage(stage);

                return RedirectToRoute("Stages", new { action = "Index", CurriculumId = HttpContext.Session["CurriculumId"] });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
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
