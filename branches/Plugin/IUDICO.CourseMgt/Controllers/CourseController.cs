using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using IUDICO.Common.Models;

namespace IUDICO.CourseMgt.Controllers
{
    public class CourseController : CourseBaseController
    {
        public ActionResult Index()
        {
            var courses = Storage.GetCourses();

            if (courses != null)
            {
                return View(courses);
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            int? id = Storage.AddCourse(course);
            
            if (id != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Edit(int courseId)
        {
            Course course = Storage.GetCourse(courseId);

            if (course != null)
            {
                return View(course);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(int courseId, Course course)
        {
            bool result = Storage.UpdateCourse(courseId, course);
            
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpDelete]
        public JsonResult Delete(int courseId)
        {
            bool result = Storage.DeleteCourse(courseId);
            
            if (result)
            {
                return Json(new { success = true, id = courseId });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public JsonResult Delete(int[] courseIds)
        {
            try
            {
                bool result = Storage.DeleteCourses(new List<int>(courseIds));
                return Json(new { success = result });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        public FilePathResult Export(int courseId)
        {
            string path = Storage.Export(courseId);
            return new FilePathResult(path, "application/octet-stream") { FileDownloadName = Storage.GetCourse(courseId).Name + ".zip" };
        }

        public ActionResult Import()
        {
            ViewData["validateResults"] = new List<string>();
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase fileUpload)
        {
            try
            {
                string path = HttpContext.Request.PhysicalApplicationPath;
                path = Path.Combine(path, @"Data\WorkFolder");
                path = Path.Combine(path, Guid.NewGuid().ToString());
                Directory.CreateDirectory(path);
                path = Path.Combine(path, fileUpload.FileName.Split('\\').Last());
                fileUpload.SaveAs(path);

                Storage.Import(path);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Validate(HttpPostedFileBase fileUpload)
        {
            try
            {
                string path = HttpContext.Request.PhysicalApplicationPath;
                path = Path.Combine(path, @"Data\WorkFolder");
                path = Path.Combine(path, Guid.NewGuid().ToString());
                Directory.CreateDirectory(path);
                path = Path.Combine(path, fileUpload.FileName.Split('\\').Last());
                fileUpload.SaveAs(path);
                ViewData["validateResults"] = Helpers.PackageValidator.Validate(path);
                return View("Import");

            }
            catch
            {
                return View("Error");
            }
        }
    }
}
