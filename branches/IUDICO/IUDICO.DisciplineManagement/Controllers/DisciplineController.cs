﻿using System;
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
using IUDICO.DisciplineManagement.Models.ViewDataClasses;

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
            var model = disciplines.Select(item => item.ToViewDisciplineModel(Validator.GetValidationError(item)));
            return View(model);
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult Create(Discipline discipline)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Storage.AddDiscipline(discipline);
                    return Json(
                        new
                        {
                            success = true,
                            disciplineRow = PartialViewAsString(
                                "DisciplineRow", 
                                discipline.ToViewDisciplineModel(Validator.GetValidationError(discipline)))
                        });
                }

                return Json(new { success = false, html = PartialViewAsString("Create", discipline) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, html = ex.Message });
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int disciplineId)
        {
            var discipline = Storage.GetDiscipline(disciplineId);

            return PartialView(discipline);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int disciplineId, Discipline discipline)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    discipline.Id = disciplineId;
                    discipline = Storage.UpdateDiscipline(discipline);

                    return Json(
                        new
                        {
                            success = true,
                            disciplineId = disciplineId,
                            disciplineRow =
                                PartialViewAsString(
                                    "DisciplineRow",
                                    discipline.ToViewDisciplineModel(Validator.GetValidationError(discipline)))
                        });
                }
                return Json(new { success = false, chapterId = disciplineId, html = PartialViewAsString("Edit", discipline) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, html = ex.Message });
            }
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
            var importer = new ImportExportDiscipline(Storage);
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
            var importer = new ImportExportDiscipline(Storage);
            importer.Import(fileUpload);

            return RedirectToAction("Index");
        }
    }
}
