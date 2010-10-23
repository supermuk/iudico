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

        //
        // GET: /Course/

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

    }
}
