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
            try
            {
                var disciplines = Storage.GetDisciplines(Storage.GetCurrentUser());

                return View(disciplines);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create()
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
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(Discipline discipline)
        {
            try
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
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int disciplineId)
        {
            try
            {
                var discipline = Storage.GetDiscipline(disciplineId);

                return View(discipline);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int disciplineId, Discipline discipline)
        {
            try
            {
                discipline.Id = disciplineId;
                Storage.UpdateDiscipline(discipline);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw e;
            }
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
