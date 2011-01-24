using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models;
using IUDICO.Common.Models;

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
                CurriculumAssignment curriculumAssignment = Storage.GetCurriculumAssignment(curriculumAssignmentId);
                Group group = Storage.GetGroup(curriculumAssignment.UserGroupRef);
                var timelines = Storage.GetStageTimelines(curriculumAssignmentId)
                                .Select(item => new ViewStageTimelineModel
                                {
                                    Id = item.Id,
                                    StartDate = item.StartDate,
                                    EndDate = item.EndDate,
                                    OperationName = item.Operation.Name,
                                    StageName = Storage.GetStage((int)item.StageRef).Name
                                });

                if (timelines != null && group != null && curriculumAssignment != null)
                {
                    ViewData["Group"] = group;
                    ViewData["Curriculum"] = curriculumAssignment.Curriculum;
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

        [HttpGet]
        public ActionResult Create(int curriculumAssignmentId)
        {
            try
            {
                CurriculumAssignment curriculumAssignment = Storage.GetCurriculumAssignment(curriculumAssignmentId);

                CreateStageTimelineModel createTimelineModel = new CreateStageTimelineModel()
                {
                    Operations = Storage.GetOperations()
                                .Select(item => new SelectListItem
                                {
                                    Text = item.Name,
                                    Value = item.Id.ToString(),
                                    Selected = false
                                }),
                    Stages = Storage.GetStages(curriculumAssignment.CurriculumRef)
                            .Select(item => new SelectListItem
                            {
                                Text = item.Name,
                                Value = item.Id.ToString(),
                                Selected = false
                            }),
                    Timeline = new Timeline()
                };

                return View(createTimelineModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Create(int curriculumAssignmentId, CreateStageTimelineModel createTimelineModel)
        {
            try
            {
                Timeline timeline = createTimelineModel.Timeline;
                timeline.CurriculumAssignmentRef = curriculumAssignmentId;
                timeline.OperationRef = createTimelineModel.OperationId;
                timeline.StageRef = createTimelineModel.StageId;
                Storage.AddTimeline(timeline);

                return RedirectToAction("Index", new { CurriculumAssignmentId = curriculumAssignmentId });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
