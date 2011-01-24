﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.ViewDataClasses;

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
                var timelines = Storage.GetStageTimelinesByCurriculumAssignmentId(curriculumAssignmentId)
                                .Select(item => new ViewStageTimelineModel
                                {
                                    Id = item.Id,
                                    StartDate = item.StartDate,
                                    EndDate = item.EndDate,
                                    OperationName = item.Operation.Name,
                                    StageName = Storage.GetStage((int)item.StageRef).Name
                                });

                ViewData["Group"] = group;
                ViewData["Curriculum"] = curriculumAssignment.Curriculum;
                return View(timelines);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult Edit(int timelineId)
        {
            try
            {
                Timeline timeline = Storage.GetTimeline(timelineId);

                EditStageTimelineModel editStageTimelineModel = new EditStageTimelineModel()
                {
                    Operations = Storage.GetOperations()
                                .Select(item => new SelectListItem
                                {
                                    Text = item.Name,
                                    Value = item.Id.ToString(),
                                    Selected = false
                                }),
                    Stages = Storage.GetStages(Storage.GetCurriculumAssignment(timeline.CurriculumAssignmentRef).CurriculumRef)
                            .Select(item => new SelectListItem
                            {
                                Text = item.Name,
                                Value = item.Id.ToString(),
                                Selected = false
                            }),
                    Timeline = timeline,
                    OperationId = timeline.OperationRef,
                    StageId = (int)timeline.StageRef
                };

                Session["CurriculumAssignmentId"] = timeline.CurriculumAssignmentRef;
                return View(editStageTimelineModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Edit(int timelineId, EditStageTimelineModel editStageTimelineModel)
        {
            try
            {
                if (ModelState.IsValid && Validator.ValidateTimeline(editStageTimelineModel.Timeline).IsValid)
                {
                    Timeline stageTimeline = editStageTimelineModel.Timeline;

                    stageTimeline.CurriculumAssignmentRef = Storage.GetCurriculumAssignment(Storage.GetTimeline(timelineId).CurriculumAssignmentRef).CurriculumRef;
                    stageTimeline.OperationRef = editStageTimelineModel.OperationId;
                    stageTimeline.StageRef = editStageTimelineModel.StageId;
                    stageTimeline.Id = timelineId;

                    Storage.UpdateTimeline(stageTimeline);

                    return RedirectToRoute("StageTimelines", new { action = "Index", CurriculumAssignmentId = Session["CurriculumAssignmentId"] });
                }
                else
                {
                    return RedirectToAction("Edit");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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
                if (ModelState.IsValid && Validator.ValidateTimeline(createTimelineModel.Timeline).IsValid)
                {
                    Timeline timeline = createTimelineModel.Timeline;
                    timeline.CurriculumAssignmentRef = curriculumAssignmentId;
                    timeline.OperationRef = createTimelineModel.OperationId;
                    timeline.StageRef = createTimelineModel.StageId;
                    Storage.AddTimeline(timeline);

                    return RedirectToAction("Index", new { CurriculumAssignmentId = curriculumAssignmentId });
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
