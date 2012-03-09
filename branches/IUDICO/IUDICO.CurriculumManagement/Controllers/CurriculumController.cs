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
        public ActionResult Index()
        {
            var curriculums = Storage.GetCurriculums(Storage.GetCurrentUser());

            return View
            (
                curriculums
                    .Select(item => new ViewCurriculumModel
                    {
                        Id = item.Id,
                        GroupName = Storage.GetGroup(item.UserGroupRef).Name,
                        DisciplineName = Storage.GetDiscipline(item.DisciplineRef).Name,
                        StartDate = Converter.ToString(item.StartDate),
                        EndDate = Converter.ToString(item.EndDate),
                    })
            );
        }

        //[Allow(Role = Role.Teacher)]
        //public ActionResult Index(int disciplineId)
        //{
        //    var discipline = Storage.GetDiscipline(disciplineId);
        //    var curriculums = Storage.GetCurriculumsByDisciplineId(disciplineId);

        //    ViewData["DisciplineName"] = discipline.Name;
        //    return View
        //    (
        //        curriculums
        //            .Select(item => new ViewCurriculumModel
        //            {
        //                Id = item.Id,
        //                GroupName = Storage.GetGroup(item.UserGroupRef).Name,
        //                StartDate = Converter.ToString(item.StartDate),
        //                EndDate = Converter.ToString(item.EndDate),
        //            })
        //    );
        //}

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create()
        {
            LoadValidationErrors();

            var groups = Storage.GetGroups();
            var disciplines = Storage.GetDisciplines(Storage.GetCurrentUser());
            var model = new CreateCurriculumModel(groups, 0, disciplines, 0, null, null);

            return View(model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(CreateCurriculumModel model)
        {
            var curriculum = new Curriculum()
            {
                UserGroupRef = model.GroupId,
                DisciplineRef = model.DisciplineId,
                StartDate = model.SetDate ? model.StartDate : (DateTime?)null,
                EndDate = model.SetDate ? model.EndDate : (DateTime?)null
            };

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

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int curriculumId)
        {
            LoadValidationErrors();

            var curriculum = Storage.GetCurriculum(curriculumId);
            //var discipline = Storage.GetDiscipline(disciplineId);
            var groupId = curriculum.UserGroupRef;
            //var groups = Storage.GetNotAssignedGroupsWithCurrentGroup(disciplineId, groupId);

            var groups = Storage.GetGroups();
            //var disciplines = Storage.GetDisciplines(Storage.GetCurrentUser());
            var model = new EditCurriculumModel(groups, groupId, curriculum.StartDate, curriculum.EndDate);

            ViewData["DisciplineName"] = Storage.GetDiscipline(curriculum.DisciplineRef).Name;
            return View(model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int curriculumId, EditCurriculumModel model)
        {
            var curriculum = Storage.GetCurriculum(curriculumId);
            var oldGroup = curriculum.UserGroupRef;
            curriculum.UserGroupRef = model.GroupId;
            curriculum.StartDate = model.SetDate ? model.StartDate : (DateTime?)null;
            curriculum.EndDate = model.SetDate ? model.EndDate : (DateTime?)null;

            AddValidationErrorsToModelState(Validator.ValidateCurriculum(curriculum, oldGroup).Errors);

            if (ModelState.IsValid)
            {
                Storage.UpdateCurriculum(curriculum);
                return RedirectToRoute("Curriculums", new { action = "Index" });
            }
            SaveValidationErrors();
            return RedirectToAction("Edit");
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