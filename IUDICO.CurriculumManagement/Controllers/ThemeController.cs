using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models;
using IUDICO.CurriculumManagement.Models.ViewDataClasses;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class ThemeController : CurriculumBaseController
    {
        public ThemeController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index(int stageId)
        {
            try
            {
                var themes = Storage.GetThemesByStageId(stageId);
                Stage stage = Storage.GetStage(stageId);

                ViewData["CurriculumName"] = stage.Curriculum.Name;
                ViewData["StageName"] = stage.Name;
                ViewData["CurriculumId"] = stage.CurriculumRef;
                return View(themes);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int stageId)
        {
            try
            {
                LoadValidationErrors();

                Stage stage = Storage.GetStage(stageId);
                var model = new CreateThemeModel(stageId, Storage.GetCourses(), 0, Storage.GetThemeTypes(), 0, "");

                ViewData["CurriculumName"] = stage.Curriculum.Name;
                ViewData["StageName"] = stage.Name;
                return View(model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int stageId, CreateThemeModel model)
        {
            try
            {
                Theme theme = new Theme
                {
                    CourseRef = model.CourseId == Constants.NoCourseId ? (int?)null : model.CourseId,
                    StageRef = model.StageId,
                    ThemeTypeRef = model.ThemeTypeId,
                    Name = model.ThemeName
                };

                AddValidationErrorsToModelState(Validator.ValidateTheme(theme).Errors);

                if (ModelState.IsValid)
                {
                    Storage.AddTheme(theme);

                    return RedirectToAction("Index", new { StageId = model.StageId });
                }
                else
                {
                    SaveValidationErrors();

                    return RedirectToAction("Create");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int themeId)
        {
            try
            {
                LoadValidationErrors();

                var theme = Storage.GetTheme(themeId);
                var model = new CreateThemeModel(theme.StageRef, Storage.GetCourses(), theme.CourseRef,
                    Storage.GetThemeTypes(), theme.ThemeTypeRef, theme.Name);

                ViewData["CurriculumName"] = theme.Stage.Curriculum.Name;
                ViewData["StageName"] = theme.Stage.Name;
                ViewData["ThemeName"] = theme.Name;
                return View(model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int themeId, CreateThemeModel model)
        {
            try
            {
                Theme theme = Storage.GetTheme(themeId);
                theme.CourseRef = model.CourseId;
                theme.ThemeTypeRef = model.ThemeTypeId;
                theme.Name = model.ThemeName;

                AddValidationErrorsToModelState(Validator.ValidateTheme(theme).Errors);

                if (ModelState.IsValid)
                {
                    Storage.UpdateTheme(theme);

                    return RedirectToRoute("Themes", new { action = "Index", StageId = theme.StageRef });
                }
                else
                {
                    SaveValidationErrors();

                    return RedirectToAction("Edit");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
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
        [Allow(Role = Role.Teacher)]
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

        [Allow(Role = Role.Teacher)]
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

        [Allow(Role = Role.Teacher)]
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
