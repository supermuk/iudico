using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using WebEditor.Models;

namespace WebEditor.Controllers
{
    public class CourseController : BaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            var courses = db.Courses;

            return View(courses);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            db.AddCourse(course);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int courseId)
        {
            try
            {
                Course course = db.GetCourse(courseId);
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
                db.UpdateCourse(courseId, course);
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
                db.RemoveCourse(courseId);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
