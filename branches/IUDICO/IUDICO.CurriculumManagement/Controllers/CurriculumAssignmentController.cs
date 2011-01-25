using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Controllers;
using IUDICO.CurriculumManagement.Models;
using IUDICO.CurriculumManagement.Models.ViewDataClasses;
using IUDICO.Common.Models.Attributes;


namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumAssignmentController : CurriculumBaseController
    {
        public CurriculumAssignmentController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index(int curriculumId)
        {
            try
            {
                Curriculum curriculum = Storage.GetCurriculum(curriculumId);
                var curriculumAssignments = Storage.GetCurriculumAssignmnetsByCurriculumId(curriculumId);

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
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int curriculumId)
        {
            try
            {
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
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int curriculumId, CreateCurriculumAssignmentModel createAssignmentModel)
        {
            try
            {
                if (ModelState.IsValid && Validator.ValidateCurriculumAssignment(createAssignmentModel.GroupId).IsValid)
                {
                    CurriculumAssignment newCurriculumAssignment = new CurriculumAssignment();
                    newCurriculumAssignment.UserGroupRef = createAssignmentModel.GroupId;
                    newCurriculumAssignment.CurriculumRef = curriculumId;

                    int curriculumAssingnmentId = Storage.AddCurriculumAssignment(newCurriculumAssignment);

                    return RedirectToAction("Index");
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
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int curriculumAssignmentId)
        {
            try
            {
                int curriculumId = Storage.GetCurriculumAssignment(curriculumAssignmentId).CurriculumRef;

                IEnumerable<Group> groups = Storage.GetNotAssignedGroups(curriculumId);

                //add current group
                int assignmentGroupId = Storage.GetCurriculumAssignment(curriculumAssignmentId).UserGroupRef;
                List<Group> assignmentedGroup = new List<Group>();
                assignmentedGroup.Add(Storage.GetGroup(assignmentGroupId));

                groups = groups.Concat(assignmentedGroup as IEnumerable<Group>);
 
                CreateCurriculumAssignmentModel editAssignmentModel = new CreateCurriculumAssignmentModel()
                {
                    Groups = from item in groups
                             select new SelectListItem { Text = item.Name, Value = item.Id.ToString(), Selected = false },
                    GroupId = assignmentGroupId
                };

                Session["CurriculumId"] = curriculumId;
                return View(editAssignmentModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int curriculumAssignmentId, CreateCurriculumAssignmentModel editAssignmentModel)
        {
            try
            {
                if (ModelState.IsValid && Validator.ValidateCurriculumAssignment(editAssignmentModel.GroupId).IsValid)
                {
                    CurriculumAssignment curriculumAssignment = new CurriculumAssignment();
                    curriculumAssignment.UserGroupRef = editAssignmentModel.GroupId;
                    curriculumAssignment.Id = curriculumAssignmentId;
                    Storage.UpdateCurriculumAssignment(curriculumAssignment);

                    return RedirectToRoute("CurriculumAssignments", new { action = "Index", CurriculumId = Session["CurriculumId"] });
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
        [Allow(Role = Role.Teacher)]
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
        [Allow(Role = Role.Teacher)]
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
    }
}