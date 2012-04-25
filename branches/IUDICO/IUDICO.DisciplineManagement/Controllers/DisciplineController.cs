using System;
using System.Collections.Generic;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Attributes;
using IUDICO.DisciplineManagement.Models;
using IUDICO.DisciplineManagement.Models.Storage;
using System.Linq;
using System.Web;
using System.IO;

namespace IUDICO.DisciplineManagement.Controllers
{
    public class DisciplineController : DisciplineBaseController
    {
        public DisciplineController(IDisciplineStorage disciplineStorage)
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
            return View(discipline);
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

        [HttpGet]
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public string Share(int disciplineId)
        {
            var users = Storage.GetDisciplineNotSharedUsers(disciplineId)
                .Union(Storage.GetDisciplineSharedUsers(disciplineId))
                .OrderBy(item => item.Name);

            return PartialViewAsString("Share", users);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public JsonResult Share(int disciplineId, List<Guid> sharewith)
        {
            try
            {
                sharewith = sharewith ?? new List<Guid>();
                // AddValidationErrorsToModelState(Validator.ValidateDisciplineSharing(disciplineId, sharewith).Errors);
                if (ModelState.IsValid)
                {
                    Storage.UpdateDisciplineSharing(disciplineId, sharewith);
                    return Json(new { success = true, disciplineId = disciplineId });
                }
                return Json(new { success = true, disciplineId = disciplineId, html = PartialViewAsString("Share", disciplineId) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, disciplineId = disciplineId, html = ex.Message });
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

        [Allow(Role = Role.Teacher)]
        public FilePathResult Export(int disciplineId)
        {
            var importer = new ImportExportDiscipline(Storage, HttpContext.Request.PhysicalApplicationPath);
            var path = importer.Export(disciplineId);

            return new FilePathResult(path, "application/octet-stream") { FileDownloadName = importer.GetFileName(disciplineId) };
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Import(string action, HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                return View();
            }
            var importer = new ImportExportDiscipline(Storage, HttpContext.Request.PhysicalApplicationPath);
            importer.Import(fileUpload);

            return RedirectToAction("Index");
        }
    }
}
