using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using IUDICO.Common.Models;
using IUDICO.CourseManagement.Models.Storage;

namespace IUDICO.CourseManagement.Controllers
{
    public class CourseController : CourseBaseController
    {
        private readonly ICourseStorage _storage;

        public CourseController(ICourseStorage courseStorage)
        {
            _storage = courseStorage;
        }

        public ActionResult Index()
        {
            var courses = _storage.GetCourses();
            
            return View(courses);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            var id = _storage.AddCourse(course);
            
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int courseId)
        {
            var course = _storage.GetCourse(courseId);
            
            return View(course);
        }

        [HttpPost]
        public ActionResult Edit(int courseId, Course course)
        {
            _storage.UpdateCourse(courseId, course);
          
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public JsonResult Delete(int courseId)
        {
            try
            {
                _storage.DeleteCourse(courseId);
                
                return Json(new { success = true, id = courseId });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public JsonResult Delete(int[] courseIds)
        {
            try
            {
                _storage.DeleteCourses(new List<int>(courseIds));
                
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        public FilePathResult Export(int courseId)
        {
            var path = _storage.Export(courseId);
            
            return new FilePathResult(path, "application/octet-stream") { FileDownloadName = _storage.GetCourse(courseId).Name + ".zip" };
        }

        public ActionResult Import()
        {
            ViewData["validateResults"] = new List<string>();
            
            return View();
        }

        [HttpPost]
        public ActionResult Import(string  action, HttpPostedFileBase fileUpload)
        {
            if (action == "Validate")
            {
                return Validate(fileUpload);
            }
            var path = HttpContext.Request.PhysicalApplicationPath;

            path = Path.Combine(path, @"Data\WorkFolder");
            path = Path.Combine(path, Guid.NewGuid().ToString());

            Directory.CreateDirectory(path);

            path = Path.Combine(path, fileUpload.FileName.Split('\\').Last());

            fileUpload.SaveAs(path);

            _storage.Import(path);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Validate(HttpPostedFileBase fileUpload)
        {
            var path = HttpContext.Request.PhysicalApplicationPath;

            path = Path.Combine(path, @"Data\WorkFolder");
            path = Path.Combine(path, Guid.NewGuid().ToString());

            Directory.CreateDirectory(path);

            path = Path.Combine(path, fileUpload.FileName.Split('\\').Last());

            fileUpload.SaveAs(path);

            ViewData["validateResults"] = Helpers.PackageValidator.Validate(path);

            return View("Import");
        }
    }
}
