using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models.ViewDataClasses;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumAssignmentTimelineController : CurriculumBaseController
    {
        public CurriculumAssignmentTimelineController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index(int curriculumAssignmentId)
        {
            try
            {
                CurriculumAssignment curriculumAssignment = Storage.GetCurriculumAssignment(curriculumAssignmentId);
                Curriculum curriculum = Storage.GetCurriculum(curriculumAssignment.CurriculumRef);
                Group group = Storage.GetGroup(curriculumAssignment.UserGroupRef);
                var timelines = Storage.GetCurriculumAssignmentTimelines(curriculumAssignmentId);

                ViewData["Group"] = group;
                ViewData["Curriculum"] = curriculum;
                return View(timelines);
            }
            catch (Exception e)
            {
                throw e;
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
                Curriculum curriculum = Storage.GetCurriculum(curriculumAssignment.CurriculumRef);
                Group group = Storage.GetGroup(curriculumAssignment.UserGroupRef);
                CreateCurriculumAssignmentTimelineModel createTimelineModel = new CreateCurriculumAssignmentTimelineModel(new Timeline());

                ViewData["GroupName"] = group.Name;
                ViewData["CurriculumName"] = curriculum.Name;
                return View(createTimelineModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int curriculumAssignmentId, CreateCurriculumAssignmentTimelineModel createTimelineModel)
        {
            try
            {
                Timeline timeline = createTimelineModel.Timeline;
                timeline.CurriculumAssignmentRef = curriculumAssignmentId;
                timeline.StageRef = null;

                AddValidationErrorsToModelState(Validator.ValidateCurriculumAssignmentTimeline(timeline).Errors);

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

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int timelineId)
        {
            try
            {
                LoadValidationErrors();

                Timeline timeline = Storage.GetTimeline(timelineId);
                CurriculumAssignment curriculumAssignment = Storage.GetCurriculumAssignment(timeline.CurriculumAssignmentRef);
                Curriculum curriculum = Storage.GetCurriculum(curriculumAssignment.CurriculumRef);
                Group group = Storage.GetGroup(curriculumAssignment.UserGroupRef);
                CreateCurriculumAssignmentTimelineModel editTimelineModel = new CreateCurriculumAssignmentTimelineModel(timeline);

                ViewData["GroupName"] = group.Name;
                ViewData["CurriculumName"] = curriculum.Name;
                Session["CurriculumAssignmentId"] = timeline.CurriculumAssignmentRef;
                return View(editTimelineModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int timelineId, CreateCurriculumAssignmentTimelineModel editTimelineModel)
        {
            try
            {
                Timeline timeline = editTimelineModel.Timeline;
                timeline.Id = timelineId;

                AddValidationErrorsToModelState(Validator.ValidateCurriculumAssignmentTimeline(timeline).Errors);

                if (ModelState.IsValid)
                {
                    Storage.UpdateTimeline(timeline);

                    return RedirectToRoute("CurriculumAssignmentTimelines", new { action = "Index", CurriculumAssignmentId = Session["CurriculumAssignmentId"] });
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
    }
}
