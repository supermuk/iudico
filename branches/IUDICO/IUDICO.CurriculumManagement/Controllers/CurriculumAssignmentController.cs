using System;
using System.Web.Mvc;
using IUDICO.CurriculumManagement.Models.Storage;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumAssignmentController : CurriculumBaseController
    {
        public CurriculumAssignmentController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

        }

        public ActionResult Index(int curriculumId) //переписати щоб було по айдішці
        {
            try
            {
                var groups = Storage.GetGroups();


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
                throw e;
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
                var timelines = Storage.GetTimelines();

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
                throw e;
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
