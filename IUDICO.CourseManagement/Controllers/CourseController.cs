using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CourseManagement.Models;
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

        [Allow(Role = Role.Student | Role.Teacher | Role.CourseCreator)]
        public ActionResult Index()
        {
            var userService = LmsService.FindService<IUserService>();

            var userId = userService.GetUsers().Single(i => i.Username == User.Identity.Name).Id;

            var sharedCourses = _Storage.GetCourses(userId);
            var myCourses = _Storage.GetCourses(User.Identity.Name);
            var currentUser = _UserService.GetCurrentUser();
            var all = sharedCourses.Union(myCourses);

            var owners = all.Select(i => i.Owner).Distinct().ToList();

            var users = new List<User>();
            foreach (var owner in owners)
            {
                var copy = owner;
                users.AddRange(_UserService.GetUsers(i => i.Username == copy));
            }
            var dic = users.ToDictionary(i => i.Username, j => j.Username);


            var viewCourses = all.Select(i => new ViewCourseModel
                                                  {
                                                      Id = i.Id,
                                                      Name = i.Name,
                                                      Created = i.Created,
                                                      Updated = i.Updated,
                                                      Locked = i.Locked ?? false,
                                                      Shared = i.Owner != currentUser.UserId,
                                                      OwnerName = dic.ContainsKey(i.Owner) ? dic[i.Owner] : i.Owner
                                                  });
            return View(viewCourses);
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
                    course.Owner = _UserService.GetCurrentUser().Username;
                    var courseId = _Storage.AddCourse(course);

                    return Json(new { success = true, courseId = courseId, courseRow = PartialViewAsString("CourseRow", course) });
                }

                return Json(new { success = false, html = PartialViewAsString("Create", course) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, html = ex.Message});
            }

        }

        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public ActionResult Edit(int courseId)
        {
            var course = _Storage.GetCourse(courseId);

            return PartialView("Edit", course);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public ActionResult Edit(int courseId, Course course)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _Storage.UpdateCourse(courseId, course);

                    return Json(new { success = false, courseId = courseId, courseRow = PartialViewAsString("CourseRow", course) });
                }

                return Json(new { success = false, courseId = courseId, html = PartialViewAsString("Edit", course) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, html = ex.Message});
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public string Share(int courseId)
        {
            var course = _Storage.GetCourse(courseId);
            var allUsers = _UserService.GetUsers().Where(i => i.Username != course.Owner);
            var courseUsers = _Storage.GetCourseUsers(courseId);

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
                _Storage.UpdateCourseUsers(courseId, sharewith);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new {success = false});
            }

        }

        [HttpDelete]
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public JsonResult Delete(int courseId)
        {
            try
            {
                var course = _Storage.GetCourse(courseId);

                if (_UserService.GetCurrentUser().Username != course.Owner)
                {
                    return Json(new { success = false });
                }

                _Storage.DeleteCourse(courseId);

                return Json(new { success = true, id = courseId });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
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

        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public ActionResult Publish(int courseId)
        {
            var path = _Storage.Export(courseId);
            _Storage.Import(path, _UserService.GetCurrentUser().Username);

            return RedirectToAction("Index");
        }

        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public FilePathResult Export(int courseId)
        {
            var path = _Storage.Export(courseId);

            return new FilePathResult(path, "application/octet-stream") { FileDownloadName = _Storage.GetCourse(courseId).Name + ".zip" };
        }

        [Allow(Role = Role.Teacher | Role.CourseCreator)]
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
        [Allow(Role = Role.Teacher | Role.CourseCreator)]
        public ActionResult Import(string action, HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                ViewData["validateResults"] = new List<string>();

                return View();
            }

            if (action == Localization.getMessage("Validate"))
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

    }
}
