using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class StageTimelineController : CurriculumBaseController
    {
        public StageTimelineController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

        }

        public ActionResult Index(int curriculumAssignmentId)
        {
            try
            {
                var timelines = Storage.GetStageTimelines(curriculumAssignmentId)
                                .Select(item => new ViewStageTimelineModel
                                {
                                    Id = item.Id,
                                    StartDate = item.StartDate,
                                    EndDate = item.EndDate,
                                    OperationName = item.Operation.Name,
                                    StageName = Storage.GetStage((int)item.StageRef).Name ?? "[Stage not exist]"
                                });

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

        //public ActionResult EditStageTimeline(int stageId)
        //{
        //    try
        //    {
        //        HttpContext.Session["StageId"] = stageId;

        //        var timelines = Storage.GetTimelines(stageId, (int)HttpContext.Session["CurriculumId"], (int)HttpContext.Session["GroupId"]);

        //        ViewData["StageName"] = Storage.GetStage(stageId).Name;

        //        if (timelines != null)
        //        {
        //            return View(timelines);
        //        }
        //        else
        //        {
        //            throw new Exception("Cannot read records");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}



        [HttpPost]
        public JsonResult DeleteItem(int timelineId)
        {
            try
            {
                Storage.DeleteTimeline(timelineId);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteItems(int[] timelineIds)
        {
            try
            {
                Storage.DeleteTimelines(timelineIds);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        //[HttpGet]
        //public ActionResult Create(int curriculumAssignmentId)
        //{
        //    try
        //    {
        //        var operations = Storage.GetOperations();

        //        CreateTimelineModel createTimelineModel = new CreateTimelineModel()
        //        {
        //            Operations = from item in operations
        //                         select new SelectListItem { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false },
        //            Timeline = new Timeline()
        //        };

        //        return View(createTimelineModel);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //[HttpPost]
        //public ActionResult Create(CreateTimelineModel createTimelineModel)
        //{
        //    try
        //    {
        //        Timeline timeline = createTimelineModel.Timeline;

        //        timeline.CurriculumAssignmentRef = (Storage.GetCurriculumAssignmentByCurriculumIdByGroupId((int)HttpContext.Session["CurriculumId"],
        //                                                                                          ((int)HttpContext.Session["GroupId"]))).Id;
        //        timeline.OperationRef = createTimelineModel.OperationId;
        //        timeline.StageRef = (int)HttpContext.Session["StageId"];

        //        Storage.AddTimeline(timeline);

        //        return RedirectToAction("EditStageTimeline", new { StageId = (int)HttpContext.Session["StageId"] });
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
    }
}
