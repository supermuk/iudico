using System;
using System.Collections.Generic;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.Storage;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumAssignmentController : CurriculumBaseController
    {
        private readonly ICurriculumStorage _storage;

        public CurriculumAssignmentController(ICurriculumStorage curriculumStorage)
        {
            _storage = curriculumStorage;
        }

        private ActionResult ErrorView(Exception e)
        {
            var currentControllerName = (string)RouteData.Values["controller"];
            var currentActionName = (string)RouteData.Values["action"];

            return View("Error", new HandleErrorInfo(e, currentControllerName, currentActionName));
        }

        public ActionResult Index(int curriculumId) //переписати щоб було по айдішці
        {
            try
            {
                var groups = _storage.GetGroups();


                if (groups != null)
                {
                    return View(groups);
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

        public ActionResult Add() 
        {
            return View();
        }

        public ActionResult EditTimeline(int groupId) //перенести на індекси
        {
            try
            {
                var timelines = _storage.GetTimelines();

                if (timelines != null)
                {
                    return View(timelines);
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

        //public ActionResult EditTimelineForStages()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public JsonResult DeleteItem(int groupId)
        //{
        //    try
        //    {
        //        Storage.DeleteCurriculum(groupId);

        //        return Json(new { success = true });
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { success = false, message = e.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult DeleteItems(int[] groupIds)
        //{
        //    try
        //    {
        //        Storage.DeleteCurriculums(groupIds);

        //        return Json(new { success = true });
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { success = false, message = e.Message });
        //    }
        //}
    }
}
