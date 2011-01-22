using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class ThemeController : CurriculumBaseController
    {
        public ThemeController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

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
                throw e;
            }
        }

        [HttpGet]
        public ActionResult Create(int stageId)
        {
            try
            {
                var model = new CreateThemeModel()
                {
                    StageId = stageId,
                    Courses = from course in Storage.GetCourses()
                              select new SelectListItem { Text = course.Name, Value = course.Id.ToString(), Selected = false },
                    ThemeTypes = from themeType in Storage.GetThemeTypes()
                                 select new SelectListItem { Text = themeType.Name, Value = themeType.Id.ToString(), Selected = false }
                };

                return View(model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Create(CreateThemeModel model)
        {
            try
            {
                var theme = new Theme { CourseRef = model.CourseId, StageRef = model.StageId, ThemeTypeRef = model.ThemeTypeId };
                var course = Storage.GetCourse(model.CourseId);

                Storage.AddTheme(theme, course);

                return RedirectToAction("Index", new { StageId = model.StageId });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult Edit(int themeId)
        {
            try
            {
                var theme = Storage.GetTheme(themeId);

                if (theme != null)
                {
                    var model = new CreateThemeModel()
                    {
                        StageId = theme.StageRef,
                        ThemeId = themeId,
                        Courses = from course in Storage.GetCourses()
                                  select new SelectListItem
                                  {
                                      Text = course.Name,
                                      Value = course.Id.ToString(),
                                      Selected = course.Id == theme.CourseRef ? true : false
                                  },
                        ThemeTypes = from themeType in Storage.GetThemeTypes()
                                     select new SelectListItem
                                     {
                                         Text = themeType.Name,
                                         Value = themeType.Id.ToString(),
                                         Selected = themeType.Id == theme.ThemeTypeRef ? true : false
                                     }
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
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Edit(CreateThemeModel model)
        {
            try
            {
                var theme = Storage.GetTheme(model.ThemeId);
                theme.CourseRef = model.CourseId;
                theme.ThemeTypeRef = model.ThemeTypeId;
                var course = Storage.GetCourse(model.CourseId);

                Storage.UpdateTheme(theme, course);

                return RedirectToRoute("Themes", new { action = "Index", StageId = theme.StageRef });
            }
            catch (Exception e)
            {
                throw e;
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
                var theme = Storage.ThemeUp(themeId);

                return RedirectToRoute("Themes", new { Action = "Index", StageId = theme.StageRef });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult ThemeDown(int themeId)
        {
            try
            {
                var theme = Storage.ThemeDown(themeId);

                return RedirectToRoute("Themes", new { Action = "Index", StageId = theme.StageRef });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
