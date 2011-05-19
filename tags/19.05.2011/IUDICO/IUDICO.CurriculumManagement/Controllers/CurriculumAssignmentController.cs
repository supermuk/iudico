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
                LoadValidationErrors();

                IEnumerable<Group> groups = Storage.GetNotAssignedGroups(curriculumId);
                Curriculum curriculum = Storage.GetCurriculum(curriculumId);
                CreateCurriculumAssignmentModel createAssignmentModel = new CreateCurriculumAssignmentModel(groups, 0);

                ViewData["CurriculumName"] = curriculum.Name;
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
                CurriculumAssignment curriculumAssignment = new CurriculumAssignment();
                curriculumAssignment.UserGroupRef = createAssignmentModel.GroupId;
                curriculumAssignment.CurriculumRef = curriculumId;

                AddValidationErrorsToModelState(Validator.ValidateCurriculumAssignment(curriculumAssignment).Errors);

                if (ModelState.IsValid)
                {
                    Storage.AddCurriculumAssignment(curriculumAssignment);

                    return RedirectToAction("Index");
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
        public ActionResult Edit(int curriculumAssignmentId)
        {
            try
            {
                LoadValidationErrors();

                CurriculumAssignment curriculumAssignment = Storage.GetCurriculumAssignment(curriculumAssignmentId);
                int curriculumId = curriculumAssignment.CurriculumRef;
                Curriculum curriculum = Storage.GetCurriculum(curriculumId);
                int assignmentGroupId = curriculumAssignment.UserGroupRef;
                IEnumerable<Group> groups = Storage.GetNotAssignedGroupsWithCurrentGroup(curriculumId, assignmentGroupId);
                CreateCurriculumAssignmentModel editAssignmentModel = new CreateCurriculumAssignmentModel(groups, assignmentGroupId);

                Session["CurriculumId"] = curriculumId;
                ViewData["CurriculumName"] = curriculum.Name;
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
                CurriculumAssignment curriculumAssignment = new CurriculumAssignment();
                curriculumAssignment.UserGroupRef = editAssignmentModel.GroupId;
                curriculumAssignment.Id = curriculumAssignmentId;

                AddValidationErrorsToModelState(Validator.ValidateCurriculumAssignment(curriculumAssignment).Errors);

                if (ModelState.IsValid)
                {
                    Storage.UpdateCurriculumAssignment(curriculumAssignment);

                    return RedirectToRoute("CurriculumAssignments", new { action = "Index", CurriculumId = Session["CurriculumId"] });
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