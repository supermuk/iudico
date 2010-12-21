using System;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.Storage;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumController : CurriculumBaseController
    {
        private readonly ICurriculumStorage _storage;

        public CurriculumController(ICurriculumStorage curriculumStorage)
        {
            _storage = curriculumStorage;
        }

        private ActionResult ErrorView(Exception e)
        {
            var currentControllerName = (string)RouteData.Values["controller"];
            var currentActionName = (string)RouteData.Values["action"];

            Response.Write(e.Message);
            return null;
            //return View("Error", new HandleErrorInfo(e, currentControllerName, currentActionName));
        }

        public ActionResult Index()
        {
            try
            {
                var curriculums = _storage.GetCurriculums();

                if (curriculums != null)
                {
                    return View(curriculums);
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
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        [HttpPost]
        public ActionResult Create(Curriculum curriculum)
        {
            try
            {
                _storage.AddCurriculum(curriculum);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        [HttpGet]
        public ActionResult Edit(int curriculumId)
        {
            try
            {
                var curriculum = _storage.GetCurriculum(curriculumId);

                if (curriculum != null)
                {
                    return View(curriculum);
                }
                else
                {
                    throw new Exception("Curriculum is null.");
                }
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        [HttpPost]
        public ActionResult Edit(int curriculumId, Curriculum curriculum)
        {
            try
            {
                curriculum.Id = curriculumId;
                _storage.UpdateCurriculum(curriculum);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        [HttpPost]
        public JsonResult DeleteItem(int curriculumId)
        {
            try
            {
                _storage.DeleteCurriculum(curriculumId);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteItems(int[] curriculumIds)
        {
            try
            {
                _storage.DeleteCurriculums(curriculumIds);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
    }
}
