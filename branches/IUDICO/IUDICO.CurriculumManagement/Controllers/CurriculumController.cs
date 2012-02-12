using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Controllers;
using IUDICO.CurriculumManagement.Models;
using IUDICO.CurriculumManagement.Models.ViewDataClasses;
using IUDICO.Common.Models.Attributes;


namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumController : CurriculumBaseController
    {
        public CurriculumController(ICurriculumStorage disciplineStorage)
            : base(disciplineStorage)
        {

        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index(int disciplineId)
        {
            try
            {
                Discipline discipline = Storage.GetDiscipline(disciplineId);
                var curriculums = Storage.GetDisciplineAssignmnetsByDisciplineId(disciplineId);

                ViewData["DisciplineName"] = discipline.Name;
                return View
                (
                    from curriculum in curriculums
                    select new ViewCurriculumModel
                    {
                        Id = curriculum.Id,
                        GroupName = Storage.GetGroup(curriculum.UserGroupRef).Name
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
        public ActionResult Create(int disciplineId)
        {
            try
            {
                LoadValidationErrors();

                IEnumerable<Group> groups = Storage.GetNotAssignedGroups(disciplineId);
                Discipline discipline = Storage.GetDiscipline(disciplineId);
                CreateCurriculumModel createAssignmentModel = new CreateCurriculumModel(groups, 0);

                ViewData["DisciplineName"] = discipline.Name;
                return View(createAssignmentModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int disciplineId, CreateCurriculumModel createAssignmentModel)
        {
            try
            {
                Curriculum curriculum = new Curriculum();
                curriculum.UserGroupRef = createAssignmentModel.GroupId;
                curriculum.DisciplineRef = disciplineId;

                AddValidationErrorsToModelState(Validator.ValidateCurriculum(curriculum).Errors);

                if (ModelState.IsValid)
                {
                    Storage.AddCurriculum(curriculum);

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
        public ActionResult Edit(int curriculumId)
        {
            try
            {
                LoadValidationErrors();

                Curriculum curriculum = Storage.GetCurriculum(curriculumId);
                int disciplineId = curriculum.DisciplineRef;
                Discipline discipline = Storage.GetDiscipline(disciplineId);
                int assignmentGroupId = curriculum.UserGroupRef;
                IEnumerable<Group> groups = Storage.GetNotAssignedGroupsWithCurrentGroup(disciplineId, assignmentGroupId);
                CreateCurriculumModel editAssignmentModel = new CreateCurriculumModel(groups, assignmentGroupId);

                Session["DisciplineId"] = disciplineId;
                ViewData["DisciplineName"] = discipline.Name;
                return View(editAssignmentModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int curriculumId, CreateCurriculumModel editAssignmentModel)
        {
            try
            {
                Curriculum curriculum = new Curriculum();
                curriculum.UserGroupRef = editAssignmentModel.GroupId;
                curriculum.Id = curriculumId;

                AddValidationErrorsToModelState(Validator.ValidateCurriculum(curriculum).Errors);

                if (ModelState.IsValid)
                {
                    Storage.UpdateCurriculum(curriculum);

                    return RedirectToRoute("Curriculums", new { action = "Index", DisciplineId = Session["DisciplineId"] });
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
        public JsonResult DeleteItem(int curriculumId)
        {
            try
            {
                Storage.DeleteCurriculum(curriculumId);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult DeleteItems(int[] curriculumIds)
        {
            try
            {
                Storage.DeleteCurriculums(curriculumIds);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
    }
}