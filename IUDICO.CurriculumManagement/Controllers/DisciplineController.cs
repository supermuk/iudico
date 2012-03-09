using System;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class DisciplineController : CurriculumBaseController
    {
        public DisciplineController(ICurriculumStorage disciplineStorage)
            : base(disciplineStorage)
        {

        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index()
        {
            var disciplines = Storage.GetDisciplines(Storage.GetCurrentUser());
            return View(disciplines);
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(Discipline discipline)
        {

            if (ModelState.IsValid)
            {
                Storage.AddDiscipline(discipline);
                return RedirectToAction("Index");
            }
            else
            {
                return View(discipline);
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int disciplineId)
        {
            var discipline = Storage.GetDiscipline(disciplineId);

            return View(discipline);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int disciplineId, Discipline discipline)
        {
            discipline.Id = disciplineId;
            Storage.UpdateDiscipline(discipline);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult DeleteItem(int disciplineId)
        {
            try
            {
                Storage.DeleteDiscipline(disciplineId);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult DeleteItems(int[] disciplineIds)
        {
            try
            {
                Storage.DeleteDisciplines(disciplineIds);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
    }
}
