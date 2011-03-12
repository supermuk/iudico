using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.ViewDataClasses;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class StageTimelineController : CurriculumBaseController
    {
        public StageTimelineController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

        }

        [Allow(Role = Role.Teacher)]
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
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int timelineId)
        {
            try
            {
                LoadValidationErrors();

                Timeline timeline = Storage.GetTimeline(timelineId);

                CreateStageTimelineModel editStageTimelineModel = new CreateStageTimelineModel(timeline,
                    Storage.GetStages(Storage.GetCurriculumAssignment(timeline.CurriculumAssignmentRef).CurriculumRef), (int)timeline.StageRef);

                Session["CurriculumAssignmentId"] = timeline.CurriculumAssignmentRef;
                return View(editStageTimelineModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int timelineId, CreateStageTimelineModel editStageTimelineModel)
        {
            try
            {
                Timeline timeline = editStageTimelineModel.Timeline;

                timeline.CurriculumAssignmentRef = Storage.GetCurriculumAssignment(Storage.GetTimeline(timelineId).CurriculumAssignmentRef).CurriculumRef;
                timeline.StageRef = editStageTimelineModel.StageId;
                timeline.Id = timelineId;

                AddValidationErrorsToModelState(Validator.ValidateStageTimeline(timeline).Errors);

                if (ModelState.IsValid)
                {
                    Storage.UpdateTimeline(timeline);

                    return RedirectToRoute("StageTimelines", new { action = "Index", CurriculumAssignmentId = Session["CurriculumAssignmentId"] });
                }
                else
                {
                    SaveValidationErrors();

                    return RedirectToAction("Edit");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
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
        [Allow(Role = Role.Teacher)]
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
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int curriculumAssignmentId)
        {
            try
            {
                LoadValidationErrors();

                CurriculumAssignment curriculumAssignment = Storage.GetCurriculumAssignment(curriculumAssignmentId);

                CreateStageTimelineModel createTimelineModel = new CreateStageTimelineModel(new Timeline(),
                    Storage.GetStages(curriculumAssignment.CurriculumRef), 0);

                return View(createTimelineModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int curriculumAssignmentId, CreateStageTimelineModel createTimelineModel)
        {
            try
            {
                Timeline timeline = createTimelineModel.Timeline;
                timeline.CurriculumAssignmentRef = curriculumAssignmentId;
                timeline.StageRef = createTimelineModel.StageId;

                AddValidationErrorsToModelState(Validator.ValidateStageTimeline(timeline).Errors);

                if (ModelState.IsValid)
                {
                    Storage.AddTimeline(timeline);

                    return RedirectToAction("Index", new { CurriculumAssignmentId = curriculumAssignmentId });
                }
                else
                {
                    SaveValidationErrors();

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
