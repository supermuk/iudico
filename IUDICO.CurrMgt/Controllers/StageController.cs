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
                    ViewData["CurriculumId"] = Storage.GetCurriculum(curriculumId).Name;
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
                    throw new Exception("Cannot create record");
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
                stage.Curriculum = Storage.GetCurriculum(curriculumId);

                int? id = Storage.AddStage(stage);

                if (id != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("Cannot create record");
                }
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
                    return View(stage);
                }
                else
                {
                    throw new Exception("Cannot edit record");
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
                if (Storage.UpdateStage(stageId, stage))
                {
                    return RedirectToAction("Index", new { CurriculumId = stage.CurriculumRef });
                }
                else
                {
                    throw new Exception("Cannot update record");
                }
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        [HttpDelete]
        public JsonResult Delete(int stageId)
        {
            try
            {
                if (Storage.DeleteStage(stageId))
                {
                    //return RedirectToAction("Index", new { CurriculumId = Storage.GetStage(stageId).CurriculumRef });
                    return Json(new { success = true });
                }
                else
                {
                    //throw new Exception("Cannot delete record");
                    return Json(new { success = false });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false });
                //return ErrorView(e);
            }
        }

        [HttpPost]
        public JsonResult Delete(int[] stageIds)
        {
            try
            {
                if (Storage.DeleteStages(stageIds.AsEnumerable()))
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch
            {
                return Json(new { success = false });
            }
        }
    }
}
