using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CourseManagement.Models;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CourseManagement.Controllers
{
    using IUDICO.Common;
   
    public class CourseController : CourseBaseController
    {
        private readonly ICourseStorage storage;
        private readonly IUserService userService;

        public CourseController(ICourseStorage courseStorage)
        {
            this.storage = courseStorage;
            this.userService = LmsService.FindService<IUserService>();
        }

        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public ActionResult Index()
        {
            var userId = this.userService.GetUsers().Single(i => i.Username == this.User.Identity.Name).Id;

            var sharedCourses = this.storage.GetCourses(userId);
            var courses = this.storage.GetCourses(User.Identity.Name);
            var currentUser = this.userService.GetCurrentUser();
            var all = sharedCourses.Union(courses);

            var owners = all.Select(i => i.Owner).Distinct().ToList();

            var users = new List<User>();
            foreach (var owner in owners)
            {
                var copy = owner;
                users.AddRange(this.userService.GetUsers(i => i.Username == copy));
            }
            var dic = users.ToDictionary(i => i.Username, j => j.Username);


            var viewCourses = all.Select(i => new ViewCourseModel
            {
                Id = i.Id,
                Name = i.Name,
                Created = i.Created,
                Updated = i.Updated,
                Locked = i.Locked ?? false,
                Shared = i.Owner != currentUser.Username,
                OwnerName = dic.ContainsKey(i.Owner) ? dic[i.Owner] : i.Owner,
                UpdatedByName = i.UpdatedBy
            });
            return View(viewCourses.OrderByDescending(i => i.Updated).AsEnumerable());
        }

        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public ActionResult Create()
        {
            return PartialView("Create", new Course());
        }

        [HttpPost]
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public JsonResult Create(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    course.Owner = this.userService.GetCurrentUser().Username;
                    var courseId = this.storage.AddCourse(course);

                    var model = this.ToViewCourseModel(course);

                    return Json(new { success = true, courseId = courseId, courseRow = PartialViewAsString("CourseRow", model) });
                }

                return Json(new { success = false, courseId = 0, html = PartialViewAsString("Create", course) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, html = ex.Message });
            }

        }

        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public ActionResult Edit(int courseId)
        {
            var course = this.storage.GetCourse(courseId);

            return PartialView("Edit", course);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public ActionResult Edit(int courseId, Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.storage.UpdateCourse(courseId, course);
                    
                    var model = this.ToViewCourseModel(course);

                    return Json(new { success = true, courseId = courseId, courseRow = PartialViewAsString("CourseRow", model) });
                }

                return Json(new { success = false, courseId = courseId, html = PartialViewAsString("Edit", course) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, html = ex.Message });
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public string Share(int courseId)
        {
            var course = this.storage.GetCourse(courseId);
            var allUsers = this.userService.GetUsers().Where(i => i.Username != course.Owner).Where(i => i.Roles.Contains(Role.CourseCreator) | i.Roles.Contains(Role.Teacher));
            var courseUsers = this.storage.GetCourseUsers(courseId);

            var model =
                allUsers.Select(
                    i =>
                    new ShareUser
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Roles = i.Roles.Select(j => j.ToString()).ToArray(),
                        Shared = courseUsers.Count(j => j.Id == i.Id) > 0
                    });

            return PartialViewAsString("ShareUserList", model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public JsonResult Share(int courseId, IEnumerable<Guid> sharewith)
        {
            try
            {
                this.storage.UpdateCourseUsers(courseId, sharewith);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }

        }

        [HttpDelete]
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public JsonResult Delete(int courseId)
        {
            try
            {
                var course = this.storage.GetCourse(courseId);
                var currentUser = this.userService.GetCurrentUser();
                if (currentUser.Username != course.Owner)
                {
                    this.storage.UpdateCourseUsers(
                        courseId,
                        this.storage.GetCourseUsers(courseId).Where(i => i.Username != currentUser.Username).Select(
                            i => i.Id));
                }
                else
                {
                    this.storage.DeleteCourse(courseId);
                }

                return Json(new { success = true, id = courseId });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public JsonResult Delete(List<int> courseIds)
        {
            try
            {
                this.storage.DeleteCourses(courseIds);

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public ActionResult Publish(int courseId)
        {
            var path = this.storage.Export(courseId);
            this.storage.Import(path, this.userService.GetCurrentUser().Username);

            return RedirectToAction("Index");
        }

        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public FilePathResult Export(int courseId)
        {
            var path = this.storage.Export(courseId);

            return new FilePathResult(path, "application/octet-stream") { FileDownloadName = this.storage.GetCourse(courseId).Name + ".zip" };
        }

        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public ActionResult Import()
        {
            ViewData["validateResults"] = new List<string>();

            return View();
        }

        public ActionResult Parse(int courseId)
        {
            this.storage.Parse(courseId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public ActionResult Import(string action, HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                ViewData["validateResults"] = new List<string>();

                return View();
            }

            if (action == Localization.GetMessage("Validate"))
            {
                return this.Validate(fileUpload);
            }

            var path = HttpContext.Request.PhysicalApplicationPath;

            path = Path.Combine(path, @"Data\WorkFolder");
            path = Path.Combine(path, Guid.NewGuid().ToString());

            Directory.CreateDirectory(path);

            path = Path.Combine(path, fileUpload.FileName.Split('\\').Last());

            fileUpload.SaveAs(path);

            this.storage.Import(path, this.userService.GetCurrentUser().Username);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
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

        protected ViewCourseModel ToViewCourseModel(Course course)
        {
            return new ViewCourseModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    Created = course.Created,
                    Updated = course.Updated,
                    Locked = course.Locked ?? false,
                    Shared = false,
                    OwnerName = this.userService.GetCurrentUser().Name,
                    UpdatedByName = this.userService.GetCurrentUser().Name
                };
        }

    }
}
