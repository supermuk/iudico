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

        public class CreateModel
        {
            public IEnumerable<SelectListItem> Groups { get; set; }
            public int GroupId { get; set; }
        }

        public ActionResult Index(int curriculumId)
        {
            try
            {
                HttpContext.Application["CurriculumId"] = curriculumId;

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
                IEnumerable<Group> groups = Storage.GetAllNotAssignmentGroups((int)HttpContext.Application["CurriculumId"]);

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
                NewCurrAssignment.CurriculumRef = (int)HttpContext.Application["CurriculumId"];

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
                HttpContext.Application["GroupId"] = groupId;

                var timelines = Storage.GetTimeline((int)HttpContext.Application["CurriculumId"], groupId);

                ViewData["GroupName"] = "PMI"; //Storage.get GetGroup(groupId).Name;

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
        public ActionResult CreateTimeline(Timeline timeline)
        {
            try
            {
                timeline.CurriculumAssignmentRef = (Storage.GetCurrAssignmentForCurriculumForGroup((int)HttpContext.Application["CurriculumId"],
                                                                                                  (int)HttpContext.Application["GroupId"])).Id;
                Storage.AddTimeline(timeline);

                return RedirectToRoute("Timelines");

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //public ActionResult EditTimelineForStages()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public JsonResult DeleteItem(int groupId)
        //{
        //    try
        //    {
        //        Storage.DeleteCurriculum(groupId);

        //        return Json(new { success = true });
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { success = false, message = e.Message });
        //    }
        //}

        //[HttpPost]
        //public JsonResult DeleteItems(int[] groupIds)
        //{
        //    try
        //    {
        //        Storage.DeleteCurriculums(groupIds);

        //        return Json(new { success = true });
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { success = false, message = e.Message });
        //    }
        //}
    }
}
