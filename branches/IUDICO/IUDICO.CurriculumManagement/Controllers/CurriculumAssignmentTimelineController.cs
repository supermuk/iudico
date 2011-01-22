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
                //HttpContext.Session["GroupId"] = groupId;

                CurriculumAssignment curriculumAssignment = Storage.GetCurriculumAssignment(curriculumAssignmentId);
                Group group = Storage.GetGroup(curriculumAssignment.UserGroupRef);
                var timelines = Storage.GetTimelines(curriculumAssignmentId); //GetTimelines((int)HttpContext.Session["CurriculumId"], groupId);

                //ViewData["GroupName"] = Storage.GetGroup(groupId).Name;

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

                timeline.CurriculumAssignmentRef = curriculumAssignmentId;// (Storage.GetCurriculumAssignmentByCurriculumIdByGroupId((int)HttpContext.Session["CurriculumId"],
                                                                                                   //((int)HttpContext.Session["GroupId"]))).Id;
                timeline.OperationRef = createTimelineModel.OperationId;
                Storage.AddTimeline(timeline);

                return RedirectToAction("Index", new { CurriculumAssignmentId = curriculumAssignmentId });
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
