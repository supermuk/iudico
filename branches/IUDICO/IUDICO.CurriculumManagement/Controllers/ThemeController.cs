using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class ThemeModel
    {
        public IEnumerable<SelectListItem> Courses { get; set; }
        public IEnumerable<Theme> Themes { get; set; }
        public int CourseId { get; set; }
        public int StageId { get; set; }
        public int ThemeId { get; set; }
    }

    public class ThemeController : CurriculumBaseController
    {
        private ActionResult ErrorView(Exception e)
        {
            string currentControllerName = (string)RouteData.Values["controller"];
            string currentActionName = (string)RouteData.Values["action"];
            return View("Error", new HandleErrorInfo(e, currentControllerName, currentActionName));
        }

        public ActionResult Index(int stageId)
        {
            try
            {
                var themes = Storage.GetThemes(stageId);
                Stage stage = Storage.GetStage(stageId);
                if (themes != null && stage != null)
                {
                    ViewData["StageName"] = stage.Name;
                    ViewData["CurriculumId"] = stage.CurriculumRef;
                    return View(themes);
                }
                else
                {
                    throw new Exception("Stage or theme doesn't exist!");
                }
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        [HttpGet]
        public ActionResult Create(int stageId)
        {
            try
            {
                ICourseManagement courseManager = lmsService.FindService<ICourseManagement>();

                ThemeModel model = new ThemeModel()
                {
                    StageId = stageId,
                    Courses = from course in courseManager.GetCourses()
                              select new SelectListItem { Text = course.Name, Value = course.Id.ToString(), Selected = false }
                };

                return View(model);
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        [HttpPost]
        public ActionResult Create(ThemeModel model)
        {
            try
            {
                Theme theme = new Theme() { CourseRef = model.CourseId, StageRef = model.StageId };
                Course course = lmsService.FindService<ICourseManagement>().GetCourse(model.CourseId);
                
                Storage.AddTheme(theme, course);

                return RedirectToAction("Index", new { StageId = model.StageId });
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        [HttpGet]
        public ActionResult Edit(int themeId)
        {
            try
            {
                Theme theme = Storage.GetTheme(themeId);

                if (theme != null)
                {
                    ThemeModel model = new ThemeModel()
                    {
                        StageId = theme.StageRef,
                        ThemeId = themeId,
                        Courses = from course in lmsService.FindService<ICourseManagement>().GetCourses()
                                  select new SelectListItem { Text = course.Name, Value = course.Id.ToString(), Selected = false }
                    };

                    return View(model);
                }
                else
                {
                    throw new Exception("Theme doesn't exist!");
                }
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        [HttpPost]
        public ActionResult Edit(ThemeModel model)
        {
            try
            {
                Theme theme = Storage.GetTheme(model.ThemeId);
                theme.CourseRef = model.CourseId;
                Course course = lmsService.FindService<ICourseManagement>().GetCourse(model.CourseId);

                Storage.UpdateTheme(theme, course);

                return RedirectToRoute("Themes", new { action = "Index", StageId = theme.StageRef });
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        [HttpPost]
        public JsonResult DeleteItem(int themeId)
        {
            try
            {
                Storage.DeleteTheme(themeId);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteItems(int[] themeIds)
        {
            try
            {
                Storage.DeleteThemes(themeIds);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        public ActionResult ThemeUp(int themeId)
        {
            try
            {
                Theme theme = Storage.ThemeUp(themeId);

                return RedirectToAction("Index", new { StageId = theme.StageRef });
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }

        public ActionResult ThemeDown(int themeId)
        {
            try
            {
                Theme theme = Storage.ThemeDown(themeId);

                return RedirectToAction("Index", new { StageId = theme.StageRef });
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }
    }
}
