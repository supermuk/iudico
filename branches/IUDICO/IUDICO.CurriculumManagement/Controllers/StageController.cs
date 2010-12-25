using System;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.Storage;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class StageController : CurriculumBaseController
    {
        private readonly ICurriculumStorage _Storage;

        public StageController(ICurriculumStorage curriculumStorage)
        {
            _Storage = curriculumStorage;
        }

        private ActionResult ErrorView(Exception e)
        {
            var currentControllerName = (string)RouteData.Values["controller"];
            var currentActionName = (string)RouteData.Values["action"];

            return View("Error", new HandleErrorInfo(e, currentControllerName, currentActionName));
        }

        public ActionResult Index(int curriculumId)
        {
            try
            {
                var stages = _Storage.GetStages(curriculumId);

                if (stages != null)
                {
                    ViewData["CurriculumName"] = _Storage.GetCurriculum(curriculumId).Name;
                    return View(stages);
                }
                else
                {
                    throw new Exception("Cannot read records");
                }
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        [HttpGet]
        public ActionResult Create(int curriculumId)
        {
            try
            {
                var curriculum = _Storage.GetCurriculum(curriculumId);

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
                return ErrorView(e);
            }
        }

        [HttpPost]
        public ActionResult Create(int curriculumId, Stage stage)
        {
            try
            {
                stage.Curriculum = _Storage.GetCurriculum(curriculumId);

                _Storage.AddStage(stage);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        [HttpGet]
        public ActionResult Edit(int stageId)
        {
            try
            {
                var stage = _Storage.GetStage(stageId);

                if (stage != null)
                {
                    HttpContext.Application["CurriculumId"] = stage.CurriculumRef;
                    return View(stage);
                }
                else
                {
                    throw new Exception("Stage doesn't exist!");
                }
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        [HttpPost]
        public ActionResult Edit(int stageId, Stage stage)
        {
            try
            {
                stage.Id = stageId;
                _Storage.UpdateStage(stage);

                return RedirectToRoute("Stages", new { action = "Index", CurriculumId = HttpContext.Application["CurriculumId"] });
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        [HttpPost]
        public JsonResult DeleteItem(int stageId)
        {
            try
            {
                _Storage.DeleteStage(stageId);

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
                _Storage.DeleteStages(stageIds);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
    }
}
