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
                //HttpContext.Session["CurriculumId"] = curriculumId;
                Curriculum curriculum = Storage.GetCurriculum(curriculumId);
                var curriculumAssignments = Storage.GetCurriculumAssignmnetsByCurriculumId(curriculumId);


                if (curriculumAssignments != null && curriculum != null)
                {
                    ViewData["CurriculumName"] = curriculum.Name;
                    return View
                    (
                        from curriculumAssignment in curriculumAssignments
                        select new ViewCurriculumAssignmentModel
                        {
                            Id = curriculumAssignment.Id,
                            GroupName = Storage.GetGroup(curriculumAssignment.UserGroupRef).Name
                        }
                    );
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
        public ActionResult Create(int curriculumId)
        {
            try
            {
                //IEnumerable<Group> groups = Storage.GetAllNotAssignmentedGroups((int)HttpContext.Session["CurriculumId"]);
                IEnumerable<Group> groups = Storage.GetNotAssignedGroups(curriculumId);

                CreateCurriculumAssignmentModel createAssignmentModel = new CreateCurriculumAssignmentModel()
                {
                    Groups = from item in groups
                             select new SelectListItem { Text = item.Name, Value = item.Id.ToString(), Selected = false }
                };

                return View(createAssignmentModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Create(int curriculumId, CreateCurriculumAssignmentModel createAssignmentModel)
        {
            try
            {
                CurriculumAssignment newCurriculumAssignment = new CurriculumAssignment();
                newCurriculumAssignment.UserGroupRef = createAssignmentModel.GroupId;
                newCurriculumAssignment.CurriculumRef = curriculumId;//(int)HttpContext.Session["CurriculumId"];

                Storage.AddCurriculumAssignment(newCurriculumAssignment);

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public JsonResult DeleteItem(int curriculumAssignmentId)
        {
            try
            {
                Storage.DeleteCurriculumAssignment(curriculumAssignmentId);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteItems(int[] curriculumAssignmentIds)
        {
            try
            {
                Storage.DeleteCurriculumAssignments(curriculumAssignmentIds);

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
        public ActionResult CreateStageTimeline(CreateTimelineModel createTimelineModel)
        {
            try
            {
                Timeline timeline = createTimelineModel.Timeline;

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