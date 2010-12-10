using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Controllers;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class StageController : CurriculumBaseController
    {
        private ActionResult ErrorView(Exception e)
        {
            string currentControllerName = (string)RouteData.Values["controller"];
            string currentActionName = (string)RouteData.Values["action"];
            return View("Error", new HandleErrorInfo(e, currentControllerName, currentActionName));
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
                return ErrorView(e);
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
                return ErrorView(e);
            }
        }

        [HttpPost]
        public ActionResult Create(int curriculumId, Stage stage)
        {
            try
            {
                throw new NotImplementedException();
                //stage.Curriculum = Storage.GetCurriculum(curriculumId);

                Storage.AddStage(stage);

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
                Stage stage = Storage.GetStage(stageId);

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
                Storage.UpdateStage(stage);

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
