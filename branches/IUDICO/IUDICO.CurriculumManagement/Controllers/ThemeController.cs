using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.CurriculumManagement.Models.Storage;

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
        private readonly ICurriculumStorage _storage;

        public ThemeController(ICurriculumStorage curriculumStorage)
        {
            _storage = curriculumStorage;
        }

        private ActionResult ErrorView(Exception e)
        {
            var currentControllerName = (string)RouteData.Values["controller"];
            var currentActionName = (string)RouteData.Values["action"];

            return View("Error", new HandleErrorInfo(e, currentControllerName, currentActionName));
        }

        public ActionResult Index(int stageId)
        {
            try
            {
                var themes = _storage.GetThemes(stageId);
                Stage stage = _storage.GetStage(stageId);
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
                var courseManager = LmsService.FindService<ICourseService>();

                var model = new ThemeModel()
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
                var theme = new Theme { CourseRef = model.CourseId, StageRef = model.StageId };
                var course = LmsService.FindService<ICourseService>().GetCourse(model.CourseId);
                
                _storage.AddTheme(theme, course);

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
                var theme = _storage.GetTheme(themeId);

                if (theme != null)
                {
                    var model = new ThemeModel()
                    {
                        StageId = theme.StageRef,
                        ThemeId = themeId,
                        Courses = from course in LmsService.FindService<ICourseService>().GetCourses()
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
                var theme = _storage.GetTheme(model.ThemeId);
                theme.CourseRef = model.CourseId;
                var course = LmsService.FindService<ICourseService>().GetCourse(model.CourseId);

                _storage.UpdateTheme(theme, course);

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
                _storage.DeleteTheme(themeId);

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
                _storage.DeleteThemes(themeIds);

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
                var theme = _storage.ThemeUp(themeId);

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
                var theme = _storage.ThemeDown(themeId);

                return RedirectToAction("Index", new { StageId = theme.StageRef });
            }
            catch (Exception e)
            {
                return ErrorView(e);
            }
        }
    }
}
