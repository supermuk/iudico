using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models;
using IUDICO.CurriculumManagement.Models.Storage;

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
                var timelines = Storage.GetTimelines(curriculumAssignmentId);

                if (timelines != null && group != null && curriculumAssignment != null)
                {
                    ViewData["Group"] = group;
                    ViewData["CurriculumAssignment"] = curriculumAssignment;
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

        [HttpGet]
        public ActionResult Create(int curriculumAssignmentId)
        {
            try
            {
                var operations = Storage.GetOperations();

                CreateTimelineModel createTimelineModel = new CreateTimelineModel()
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
        public ActionResult Create(int curriculumAssignmentId, CreateTimelineModel createTimelineModel)
        {
            try
            {
                Timeline timeline = createTimelineModel.Timeline;

                timeline.CurriculumAssignmentRef = curriculumAssignmentId;
                timeline.OperationRef = createTimelineModel.OperationId;
                Storage.AddTimeline(timeline);

                return RedirectToAction("Index", new { CurriculumAssignmentId = curriculumAssignmentId });
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

                CreateTimelineModel editTimelineModel = new CreateTimelineModel()
                {
                    Operations = from item in operations
                                 select new SelectListItem { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false },
                    Timeline = Storage.GetTimeline(timelineId),
                    OperationId = Storage.GetTimeline(timelineId).OperationRef
                };

                return View(editTimelineModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Edit(int timelineId, CreateTimelineModel editTimelineModel)
        {
            try
            {
                Timeline timeline = editTimelineModel.Timeline;
                timeline.Id = timelineId;
                timeline.OperationRef = editTimelineModel.OperationId;

                Storage.UpdateTimeline(timeline);

                return RedirectToAction("Index");//чому
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
