using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Controllers;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumAssignmentController : CurriculumBaseController
    {
        public CurriculumAssignmentController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

        }

        public class CreateModel //!!!
        {
            public IEnumerable<SelectListItem> Groups { get; set; }
            public int GroupId { get; set; }
        }

        public ActionResult Index(int curriculumId)
        {
            try
            {
                HttpContext.Session["CurriculumId"] = curriculumId;

                var assingmentsGroups = Storage.GetAssignmentGroups(curriculumId);

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
                IEnumerable<Group> groups = Storage.GetAllNotAssignmentGroups((int)HttpContext.Session["CurriculumId"]);

                CreateModel createModel = new CreateModel()
                {
                    Groups = from item in groups
                             select new SelectListItem { Text = item.Name.ToString(), Value = item.Id.ToString(), Selected = false }
                };

                return View(createModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Create(CreateModel createModel)
        {
            try
            {
                CurriculumAssignment NewCurrAssignment = new CurriculumAssignment();
                NewCurrAssignment.UserGroupRef = createModel.GroupId;
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

                var timelines = Storage.GetTimeline((int)HttpContext.Session["CurriculumId"], groupId);

                var courseManager = LmsService.FindService<IUDICO.Common.Models.Services.IUserService>();

                ViewData["GroupName"] = courseManager.GetGroup(groupId).Name;

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
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult CreateTimeline(Timeline timeline)//!!!
        {
            try
            {
                timeline.CurriculumAssignmentRef = (Storage.GetCurrAssignmentForCurriculumForGroup((int)HttpContext.Session["CurriculumId"],
                                                                                                  ((int)HttpContext.Session["GroupId"]))).Id;
                Storage.AddTimeline(timeline);

                return RedirectToRoute("Timelines");

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public JsonResult DeleteItem(int groupId)
        {
            try
            {
                int CurrAssingmentId = Storage.GetCurrAssignmentForCurriculumForGroup((int)HttpContext.Session["CurriculumId"], groupId).Id;
                Storage.DeleteCurriculumAssignment(CurrAssingmentId);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteItems(int[] groupIds)
        {
            try
            {
                int i = 0;
                while (i != groupIds.Length)
                {
                    int CurrAssingmentId = Storage.GetCurrAssignmentForCurriculumForGroup((int)HttpContext.Session["CurriculumId"], groupIds[i]).Id;
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

        public ActionResult EditTimelineForStages()
        {
            try
            {
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

        public ActionResult EditStageTimeline(int StageId)
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}