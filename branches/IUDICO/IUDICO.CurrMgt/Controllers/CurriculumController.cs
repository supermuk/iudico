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
        private ActionResult ErrorView(Exception e)
        {
            string currentControllerName = (string)RouteData.Values["controller"];
            string currentActionName = (string)RouteData.Values["action"];
            return View("Error", new HandleErrorInfo(e, currentControllerName, currentActionName));
        }

        public ActionResult Index()
        {
            try
            {
                var curriculums = Storage.GetCurriculums();

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
                if (Storage.AddCurriculum(curriculum) != null)
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
        public ActionResult Edit(int curriculumId)
        {
            try
            {
                Curriculum curriculum = Storage.GetCurriculum(curriculumId);

                if (curriculum != null)
                {
                    return View(curriculum);
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

        [HttpPost]
        public ActionResult Edit(int curriculumId, Curriculum curriculum)
        {
            try
            {
                if (Storage.UpdateCurriculum(curriculumId, curriculum))
                {
                    return RedirectToAction("Index");
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
        public JsonResult Delete(int curriculumId)
        {
            try
            {
                if (Storage.DeleteCurriculum(curriculumId))
                {
                    return Json(new { success = true, id = curriculumId });
                    //return RedirectToAction("Index");
                }
                else
                {
                    return Json(new { success = false });
                    //throw new Exception("Cannot delete record");
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false });
                //return ErrorView(e);
            }
        }

        [HttpPost]
        public JsonResult Delete(int[] curriculumIds)
        {
            try
            {
                bool result = Storage.DeleteCurriculums(curriculumIds.AsEnumerable());

                return Json(new { success = result });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }
    }
}
