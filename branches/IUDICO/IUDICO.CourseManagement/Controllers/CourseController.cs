using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using IUDICO.Common.Models;
using IUDICO.CourseManagement.Models.ManifestModels;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CourseManagement.Controllers
{
    public class CourseController : CourseBaseController
    {
        private readonly ICourseStorage _Storage;
        private readonly IUserService _UserService;

        public CourseController(ICourseStorage courseStorage)
        {
            _Storage = courseStorage;
            _UserService = LmsService.FindService<IUserService>();
        }

        [Allow(Role = Role.Student)]
        public ActionResult Index()
        {
            var userService = LmsService.FindService<IUserService>();
            var userId = userService.GetUsers().Single(i => i.Username == User.Identity.Name).Id;
            var courses = _Storage.GetCourses(userId);
            
            return View(courses.Union(_Storage.GetCourses(User.Identity.Name)));
        }

        [Allow(Role = Role.Student)]
        public ActionResult Create()
        {
            var allUsers = _UserService.GetUsers().Where(i => i.Username != _UserService.GetCurrentUser().Username);
            
            ViewData["AllUsers"] = allUsers;

            return View();
        }

        [HttpPost]
        [Allow(Role = Role.Student)]
        public ActionResult Create(Course course, IEnumerable<Guid> sharewith)
        {
            
            course.Owner = _UserService.GetCurrentUser().Username;
            var id = _Storage.AddCourse(course);
            _Storage.UpdateCourseUsers(id, sharewith);

            return RedirectToAction("Index");
        }

        [Allow(Role = Role.Student)]
        public ActionResult Edit(int courseId)
        {
            var course = _Storage.GetCourse(courseId);

            var allUsers = _UserService.GetUsers().Where(i => i.Username != course.Owner);
            var courseUsers = allUsers.Where(i => _Storage.GetCourseUsers(courseId).Any(j => j.Id == i.Id));

            ViewData["AllUsers"] = allUsers.Except(courseUsers);
            ViewData["SharedUsers"] = courseUsers;

            return View(course);
        }

        [HttpPost]
        [Allow(Role = Role.Student)]
        public ActionResult Edit(int courseId, Course course, IEnumerable<Guid> sharewith)
        {
            _Storage.UpdateCourse(courseId, course);
            _Storage.UpdateCourseUsers(courseId, sharewith);
          
            return RedirectToAction("Index");
        }

        [HttpDelete]
        [Allow(Role = Role.Student)]
        public JsonResult Delete(int courseId)
        {
            try
            {
                _Storage.DeleteCourse(courseId);
                
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
                _Storage.DeleteCourses(new List<int>(courseIds));
                
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
            var path = _Storage.Export(courseId);
            
            return new FilePathResult(path, "application/octet-stream") { FileDownloadName = _Storage.GetCourse(courseId).Name + ".zip" };
        }

        [Allow(Role = Role.Student)]
        public ActionResult Import()
        {
            ViewData["validateResults"] = new List<string>();
            
            return View();
        }

        public ActionResult Parse(int courseId)
        {
            _Storage.Parse(courseId);

            return RedirectToAction("Index");
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

            _Storage.Import(path, _UserService.GetCurrentUser().Username);

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
