using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.CurrMgt.Controllers;

namespace IUDICO.CurrMgt.Controllers
{
    public class CurriculumController : CurriculumBaseController
    {
        public ActionResult Index()
        {
            var curriculums = Storage.GetCurriculums();

            if (curriculums != null)
            {
                return View(curriculums);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Curriculum curriculum)
        {
            int? id = Storage.AddCurriculum(curriculum);

            if (id != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Edit(int curriculumId)
        {
            Curriculum curriculum = Storage.GetCurriculum(curriculumId);

            if (curriculum != null)
            {
                return View(curriculum);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(int curriculumId, Curriculum curriculum)
        {
            bool result = Storage.UpdateCurriculum(curriculumId, curriculum);

            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpDelete]
        public ActionResult Delete(int curriculumId)
        {
            bool result = Storage.DeleteCurriculum(curriculumId);

            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public JsonResult Delete(int[] curriculumIds)
        {
            try
            {
                bool result = Storage.DeleteCurriculums(new List<int>(curriculumIds));
                return Json(new { success = result });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }
    }
}
