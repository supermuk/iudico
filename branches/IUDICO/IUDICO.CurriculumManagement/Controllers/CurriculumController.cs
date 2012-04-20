using System;
using System.Linq;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Models.Storage;
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

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create()
        {
            var model = new Curriculum().ToCreateCurriculumModel(Storage, true);

            return View(model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(CreateCurriculumModel model)
        {
            var curriculum = new Curriculum();
            curriculum.UpdateFromModel(model);

            AddValidationErrorsToModelState(Validator.ValidateCurriculum(curriculum).Errors);

            if (ModelState.IsValid)
            {
                Storage.AddCurriculum(curriculum);
                return RedirectToAction("Index");
            }
            model = curriculum.ToCreateCurriculumModel(Storage, true);
            return View(model);
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int curriculumId)
        {
            var curriculum = Storage.GetCurriculum(curriculumId);
            var model = curriculum.ToCreateCurriculumModel(Storage, false);

            ViewData["DisciplineName"] = Storage.GetDiscipline(curriculum.DisciplineRef).Name;
            return View(model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int curriculumId, CreateCurriculumModel model)
        {
            var curriculum = Storage.GetCurriculum(curriculumId);
            curriculum.UpdateFromModel(model);
            curriculum.DisciplineRef = curriculum.DisciplineRef;

            AddValidationErrorsToModelState(Validator.ValidateCurriculum(curriculum).Errors);

            if (ModelState.IsValid)
            {
                Storage.UpdateCurriculum(curriculum);
                return RedirectToRoute("Curriculums", new { action = "Index" });
            }
            model = curriculum.ToCreateCurriculumModel(Storage, false);
            return View(model);
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