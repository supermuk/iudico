using System;
using System.Collections.Generic;
using System.Web.Mvc;
using IUDICO.Common;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Attributes;
using IUDICO.DisciplineManagement.Models;
using IUDICO.DisciplineManagement.Models.Storage;
using System.Linq;
using System.Web;
using System.IO;
using IUDICO.DisciplineManagement.Models.ViewDataClasses;
using IUDICO.DisciplineManagement.Helpers;

namespace IUDICO.DisciplineManagement.Controllers
{
    public class DisciplineController : DisciplineBaseController
    {
       private bool completeDownloading = true;
       private int countOfCourses = 0;
       private int countValidCourses = 0;
       private int countInvalidCourses = 0;
 

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

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Import()
        {
           return PartialView();
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Import(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return View();
            }

            var importer = new ImportExportDiscipline(Storage);
            
            importer.Import(file);

            return RedirectToAction("Index");
        }


        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Import(string path)
        {
           if (path == string.Empty)
           {
              return View();
           }

           var importer = new ImportExportDiscipline(Storage);

           importer.Import(path);

           return RedirectToAction("Index");
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ContentResult UploadFiles()
        {
          this.completeDownloading = true;
          this.countOfCourses = 0;
          this.countValidCourses = 0;
          this.countInvalidCourses = 0;

           var r = new List<ViewDataUploadFilesResult>();

           string savedFileName = string.Empty;

           if (Request.Files.Count > 1)
           {
                return Content("{\"message\":\"" + "You must select one file" + "\"}", "application/json");
           }

           foreach (string file in Request.Files)
           {
              HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
              if (hpf.ContentLength == 0)
                 continue;

              if (Path.GetExtension(hpf.FileName) != ".zip")
              {
                 return Content("{\"message\":\"" + "You must select .zip file" + "\"}", "application/json");
              }

              savedFileName = Path.Combine(Server.MapPath("~/Data/Disciplines"), Path.GetFileName(hpf.FileName));
              hpf.SaveAs(savedFileName); // Save the file

              r.Add(new ViewDataUploadFilesResult()
              {
                 Name = hpf.FileName,
                 Length = hpf.ContentLength,
                 Type = hpf.ContentType
              });

              var importer = new ImportExportDiscipline(Storage);

              //importer.Validate(savedFileName, ref this.countOfCourses, ref this.countValidCourses, ref this.countInvalidCourses);
              if(importer.Validate(savedFileName))
              {
                 importer.Import(savedFileName);
              }

           }

           //// Returns json
           //return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + /*"\",\"path\":\""+ savedFileName.ToString() +*/ "\"}", "application/json");
           return Content("{\"message\":\"" + "Uploading complete" + "\"}", "application/json");
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ContentResult ValidationInfo()
        {
           //this.completeDownloading = true;
           //this.countCourses = 0;
           //this.countValidCourses = 0;
           //this.countInvalidCourses = 0;

           return Content("{\"message\":\"" + string.Format("{0}: {1}, {2}: {3}, {4}: {5}", Localization.GetMessage("countOfCourses"), this.countOfCourses, Localization.GetMessage("countValidCourses"), this.countValidCourses, Localization.GetMessage("countInvalidCourses"), this.countInvalidCourses) + "\"}", "application/json");
           
        }
    }
}
