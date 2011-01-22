using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using IUDICO.Common.Models;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CourseManagement.Controllers
{
    public class CourseController : CourseBaseController
    {
        private readonly ICourseStorage _storage;

        public CourseController(ICourseStorage courseStorage)
        {
            _storage = courseStorage;
        }

        [Allow(Role = Role.Student)]
        public ActionResult Index()
        {
            IUserService userService = LmsService.FindService<IUserService>();
            Guid userId = userService.GetUsers().Single(i => i.Username == User.Identity.Name).Id;
            var courses = _storage.GetCourses(userId);
            
            return View(courses.Union(_storage.GetCourses(User.Identity.Name)));
        }

        [Allow(Role = Role.Student)]
        public ActionResult Create()
        {
            IUserService userService = LmsService.FindService<IUserService>();
            var allUsers = userService.GetUsers().Where(i => i.Username != User.Identity.Name);
            ViewData["AllUsers"] = allUsers;

            return View();
        }

        [HttpPost]
        [Allow(Role = Role.Student)]
        public ActionResult Create(Course course, IEnumerable<Guid> sharewith)
        {
            course.Owner = User.Identity.Name;
            var id = _storage.AddCourse(course);
            _storage.UpdateCourseUsers(id, sharewith);

            return RedirectToAction("Index");
        }

        [Allow(Role = Role.Student)]
        public ActionResult Edit(int courseId)
        {
            IUserService userService = LmsService.FindService<IUserService>();
            var course = _storage.GetCourse(courseId);

            var allUsers = userService.GetUsers().Where(i => i.Username != course.Owner);
            var courseUsers = allUsers.Where(i => _storage.GetCourseUsers(courseId).Any(j => j.Id == i.Id));

            ViewData["AllUsers"] = allUsers.Except(courseUsers);
            ViewData["SharedUsers"] = courseUsers;
            return View(course);
        }

        [HttpPost]
        [Allow(Role = Role.Student)]
        public ActionResult Edit(int courseId, Course course, IEnumerable<Guid> sharewith)
        {
            _storage.UpdateCourse(courseId, course);
            _storage.UpdateCourseUsers(courseId, sharewith);
          
            return RedirectToAction("Index");
        }

        [HttpDelete]
        [Allow(Role = Role.Student)]
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
        [Allow(Role = Role.Student)]
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

        [Allow(Role = Role.Student)]
        public FilePathResult Export(int courseId)
        {
            var path = _storage.Export(courseId);
            
            return new FilePathResult(path, "application/octet-stream") { FileDownloadName = _storage.GetCourse(courseId).Name + ".zip" };
        }

        [Allow(Role = Role.Student)]
        public ActionResult Import()
        {
            ViewData["validateResults"] = new List<string>();
            
            return View();
        }

        [HttpPost]
        [Allow(Role = Role.Student)]
        public ActionResult Import(string action, HttpPostedFileBase fileUpload)
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
        [Allow(Role = Role.Student)]
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
