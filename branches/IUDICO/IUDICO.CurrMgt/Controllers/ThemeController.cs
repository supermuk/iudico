using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;

namespace IUDICO.CurrMgt.Controllers
{
    public class ThemeModel
    {
        public IEnumerable<SelectListItem> Courses { get; set; }
        public IEnumerable<Theme> Themes { get; set; }
        public int CourseId { get; set; }
        public int StageId { get; set; }
        public int ThemeId { get; set; }
    }

    public class ThemeController : CurrBaseController
    {
        public ActionResult Index(int stageId)
        {
            var themes = Storage.GetThemes(stageId);
            ThemeModel model = new ThemeModel() { Themes = themes, StageId = stageId };

            if (themes != null)
            {
                return View(model);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Create(int stageId)
        {
            try
            {
                ThemeModel model = new ThemeModel();
                model.StageId = stageId;
                model.Courses = from course in Storage.GetCourses()
                                select new SelectListItem { Text = course.Name, Value = course.Id.ToString(), Selected = false };

                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Create(int stageId, int courseId)
        {
            Theme theme = new Theme() { CourseRef = courseId, StageRef = stageId };
            int? id = Storage.AddTheme(theme);

            if (id != null)
            {
                return RedirectToAction("Index", new { StageId = stageId });
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Edit(int themeId, int stageId)
        {
            try
            {
                ThemeModel model = new ThemeModel();
                model.ThemeId = themeId;
                model.StageId = stageId;
                model.Courses = from course in Storage.GetCourses()
                                select new SelectListItem { Text = course.Name, Value = course.Id.ToString(), Selected = false };

                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(int themeId, int stageId, int courseId)
        {
            Theme theme = new Theme() { CourseRef = courseId, StageRef = stageId };
            bool result = Storage.UpdateTheme(themeId, theme);

            if (result)
            {
                return RedirectToAction("Index", new { StageId = stageId });
            }
            else
            {
                return View("Error");
            }
            return null;
        }

        public ActionResult ThemeUp(int themeId, int stageId)
        {
            bool result = Storage.ThemeUp(themeId);

            if (result)
            {
                return RedirectToAction("Index", new { StageId = stageId });
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult ThemeDown(int themeId, int stageId)
        {
            bool result = Storage.ThemeDown(themeId);

            if (result)
            {
                return RedirectToAction("Index", new { StageId = stageId });
            }
            else
            {
                return View("Error");
            }
        }
    }
}
