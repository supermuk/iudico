using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using WebEditor.Models;
using WebEditor.Models.Storage;

namespace WebEditor.Controllers
{
    public class CourseController : BaseController
    {
        protected IStorageInterface Storage;

        protected override void Initialize(RequestContext requestContext)
        {
            StorageFactory factory = new StorageFactory();
            Storage = factory.CreateStorage(StorageType.Mixed);

            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            var courses = Storage.GetCourses();

            return View(courses);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            Storage.AddCourse(course);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int courseId)
        {
            try
            {
                Course course = Storage.GetCourse(courseId);
                return View(course);
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(int courseId, Course course)
        {
            try
            {
                Storage.UpdateCourse(courseId, course);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpDelete]
        public ActionResult Delete(int courseId)
        {
            try
            {
                Storage.DeleteCourse(courseId);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
