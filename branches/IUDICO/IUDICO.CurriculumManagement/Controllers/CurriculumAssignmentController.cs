using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Controllers;
using IUDICO.CurriculumManagement.Models;


namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumAssignmentController : CurriculumBaseController
    {
        public CurriculumAssignmentController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

        }

        public ActionResult Index(int curriculumId)
        {
            try
            {
                HttpContext.Session["CurriculumId"] = curriculumId;

                var assingmentsGroups = Storage.GetAssignmentedGroups(curriculumId);

                if (assingmentsGroups != null)
                {
                    return View(assingmentsGroups);
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
        public ActionResult Create() 
        {
            try
            {
                IEnumerable<Group> groups = Storage.GetAllNotAssignmentedGroups((int)HttpContext.Session["CurriculumId"]);

                CreateAssignmentModel createAssignmentModel = new CreateAssignmentModel()
                {
                    Groups = from item in groups
                             select new SelectListItem { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false }
                };

                return View(createAssignmentModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Create(CreateAssignmentModel createAssignmentModel)
        {
            try
            {
                CurriculumAssignment NewCurrAssignment = new CurriculumAssignment();
                NewCurrAssignment.UserGroupRef = createAssignmentModel.GroupId;
                NewCurrAssignment.CurriculumRef = (int)HttpContext.Session["CurriculumId"];

                Storage.AddCurriculumAssignment(NewCurrAssignment);

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult EditTimeline(int groupId)
        {
            try
            {
                HttpContext.Session["GroupId"] = groupId;

                var timelines = Storage.GetTimelines((int)HttpContext.Session["CurriculumId"], groupId);

                ViewData["GroupName"] = Storage.GetGroup(groupId).Name;
                
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

        [HttpGet]
        public ActionResult CreateTimeline()
        {
            try
            {
                var operations = Storage.GetOperations();

                CreateTimelineModel createTimelineModel = new CreateTimelineModel()
                {
                    Operations = from item in operations
                                 select new SelectListItem { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false },
                    timeline = new Timeline()
                };

                return View(createTimelineModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult CreateTimeline(CreateTimelineModel createTimelineModel)
        {
            try
            {
                Timeline timeline = createTimelineModel.timeline;

                timeline.CurriculumAssignmentRef = (Storage.GetCurriculumAssignmentByCurriculumIdByGroupId((int)HttpContext.Session["CurriculumId"],
                                                                                                  ((int)HttpContext.Session["GroupId"]))).Id;
                timeline.OperationRef = createTimelineModel.OperationId;
                Storage.AddTimeline(timeline);

                return RedirectToAction("EditTimeline", new { GroupId = (int)HttpContext.Session["GroupId"] });

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public JsonResult DeleteAssignmentItem(int groupId)
        {
            try
            {
                int CurrAssingmentId = Storage.GetCurriculumAssignmentByCurriculumIdByGroupId((int)HttpContext.Session["CurriculumId"], groupId).Id;
                Storage.DeleteCurriculumAssignment(CurrAssingmentId);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteAssignmentItems(int[] groupIds)
        {
            try
            {
                int i = 0;
                while (i != groupIds.Length)
                {
                    int CurrAssingmentId = Storage.GetCurriculumAssignmentByCurriculumIdByGroupId((int)HttpContext.Session["CurriculumId"], groupIds[i]).Id;
                    Storage.DeleteCurriculumAssignment(CurrAssingmentId);
                    i++;
                }
                
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        public ActionResult EditTimelineForStages(int groupId)
        {
            try
            {
                HttpContext.Session["GroupId"] = groupId;

                var stages = Storage.GetStages((int)HttpContext.Session["CurriculumId"]);
                
                if (stages != null)
                {
                    return View(stages);
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

        public ActionResult EditStageTimeline(int stageId)
        {
            try
            {
                HttpContext.Session["StageId"] = stageId;

                var timelines = Storage.GetTimelines(stageId, (int)HttpContext.Session["CurriculumId"], (int)HttpContext.Session["GroupId"]);

                ViewData["StageName"] = Storage.GetStage(stageId).Name;

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

        [HttpPost]
        public JsonResult DeleteTimelineItem(int timelineId)
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
        public JsonResult DeleteTimelineItems(int[] timelineIds)
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

        [HttpPost]
        public JsonResult DeleteStageTimelineItem(int timelineId)
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
        public JsonResult DeleteStageTimelineItems(int[] timelineIds)
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
        public ActionResult CreateStageTimeline()
        {
            try
            {
                var operations = Storage.GetOperations();

                CreateTimelineModel createTimelineModel = new CreateTimelineModel()
                {
                    Operations = from item in operations
                                 select new SelectListItem { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false },
                    timeline = new Timeline()
                };

                return View(createTimelineModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult CreateStageTimeline(CreateTimelineModel createTimelineModel)
        {
            try
            {
                Timeline timeline = createTimelineModel.timeline;

                timeline.CurriculumAssignmentRef = (Storage.GetCurriculumAssignmentByCurriculumIdByGroupId((int)HttpContext.Session["CurriculumId"],
                                                                                                  ((int)HttpContext.Session["GroupId"]))).Id;
                timeline.OperationRef = createTimelineModel.OperationId;
                timeline.StageRef = (int)HttpContext.Session["StageId"];
                
                Storage.AddTimeline(timeline);

                return RedirectToAction("EditStageTimeline", new { StageId = (int)HttpContext.Session["StageId"] });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}