using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using System.Reflection;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class CreateThemeModel
    {
        public IEnumerable<SelectListItem> Courses { get; set; }
        public IEnumerable<SelectListItem> ThemeTypes { get; set; }
        public int CourseId { get; set; }
        public int StageId { get; set; }
        public int ThemeTypeId { get; set; }
        public string ThemeName { get; set; }

        public CreateThemeModel()
        {
        }

        public CreateThemeModel(int stageId, IEnumerable<Course> courses, int courseId, IEnumerable<ThemeType> themeTypes, int themeTypeId, string themeName)
        {
            StageId = stageId;
            Courses = courses
                    .Select(item => new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = false
                    });
            ThemeTypes = themeTypes
                        .Select(item => new SelectListItem
                        {
                            Text = item.Name,
                            Value = item.Id.ToString(),
                            Selected = false
                        });
            CourseId = courseId;
            ThemeTypeId = themeTypeId;
            ThemeName = themeName;
        }
    }
}