using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using IUDICO.Common.Models;
using IUDICO.CurrMgt.Controllers;

namespace IUDICO.CurrMgt.Controllers
{
    public class StageController : CurriculumBaseController
    {
        public ActionResult Index(int curriculumId)
        {
            var stages = Storage.GetStages(curriculumId);

            if (stages != null)
            {
                return View(stages);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Create(int curriculumId)
        {
            var curriculum = Storage.GetCurriculum(curriculumId);

            if (curriculum != null)
            {
                return View();
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Create(int curriculumId, Stage stage)
        {
            stage.Curriculum = Storage.GetCurriculum(curriculumId);

            int? id = Storage.AddStage(stage);

            if (id != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(stage);
            }
        }

        [HttpGet]
        public ActionResult Edit(int stageId)
        {
            Stage stage = Storage.GetStage(stageId);

            if (stage == null)
                return Redirect("Curriculum");
            else
                return View(stage);
        }

        [HttpPost]
        public ActionResult Edit(int stageId, Stage stage)
        {
            if (Storage.UpdateStage(stageId, stage))
                return RedirectToRoute("Stages", new { CurriculumId = stage.CurriculumRef, action = "Index" });
            else
                return View(stage);
        }

        [HttpDelete]
        public ActionResult Delete(int stageId)
        {
            if (Storage.DeleteStage(stageId))
                return RedirectToRoute("Stages", new { CurriculumId = Storage.GetStage(stageId).CurriculumRef, action = "Index" });
            else
                return View("Error");
        }

        [HttpPost]
        public ActionResult Delete(int[] stageIds)
        {
            if (Storage.DeleteStages(stageIds.AsEnumerable()))
                return Json(new { success = true });
            else
                return Json(new { success = false });
        }
    }
}
