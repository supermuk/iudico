using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models.ViewDataClasses;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumAssignmentTimelineController : CurriculumBaseController
    {
        public CurriculumAssignmentTimelineController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

        }

        public ActionResult Index(int curriculumAssignmentId)
        {
            try
            {
                CurriculumAssignment curriculumAssignment = Storage.GetCurriculumAssignment(curriculumAssignmentId);
                Group group = Storage.GetGroup(curriculumAssignment.UserGroupRef);
                var timelines = Storage.GetCurriculumAssignmentTimelines(curriculumAssignmentId);

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
        public ActionResult Create(int curriculumAssignmentId)
        {
            try
            {
                var operations = Storage.GetOperations();

                CreateCurriculumAssignmentTimelineModel createTimelineModel = new CreateCurriculumAssignmentTimelineModel()
                {
                    Operations = from item in operations
                                 select new SelectListItem { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false },
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
        public ActionResult Create(int curriculumAssignmentId, CreateCurriculumAssignmentTimelineModel createTimelineModel)
        {
            try
            {
                if (ModelState.IsValid && Validator.ValidateTimeline(createTimelineModel.Timeline).IsValid)
                {
                    Timeline timeline = createTimelineModel.Timeline;
                    timeline.CurriculumAssignmentRef = curriculumAssignmentId;
                    timeline.OperationRef = createTimelineModel.OperationId;
                    timeline.StageRef = null;
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

        [HttpGet]
        public ActionResult Edit(int timelineId)
        {
            try
            {
                var operations = Storage.GetOperations();
                Timeline timeline = Storage.GetTimeline(timelineId);

                EditCurriculumAssignmentTimelineModel editTimelineModel = new EditCurriculumAssignmentTimelineModel()
                {
                    Operations = from item in operations
                                 select new SelectListItem { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false },
                    Timeline = timeline,
                    OperationId = timeline.OperationRef
                };

                Session["CurriculumAssignmentId"] = timeline.CurriculumAssignmentRef;
                return View(editTimelineModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Edit(int timelineId, EditCurriculumAssignmentTimelineModel editTimelineModel)
        {
            try
            {
                if (ModelState.IsValid && Validator.ValidateTimeline(editTimelineModel.Timeline).IsValid)
                {
                    Timeline timeline = editTimelineModel.Timeline;
                    timeline.Id = timelineId;
                    timeline.OperationRef = editTimelineModel.OperationId;
                    Storage.UpdateTimeline(timeline);

                    return RedirectToRoute("CurriculumAssignmentTimelines", new { action = "Index", CurriculumAssignmentId = Session["CurriculumAssignmentId"] });
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
    }
}
