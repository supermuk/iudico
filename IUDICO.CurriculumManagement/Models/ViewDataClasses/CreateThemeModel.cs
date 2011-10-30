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
        public List<SelectListItem> Courses { get; set; }
        public List<SelectListItem> ThemeTypes { get; set; }
        public int CourseId { get; set; }
        public int StageId { get; set; }
        public int ThemeTypeId { get; set; }
        public string ThemeName { get; set; }

        public CreateThemeModel()
        {
        }

        public CreateThemeModel(int stageId, IEnumerable<Course> courses, int? courseId, IEnumerable<ThemeType> themeTypes, int themeTypeId, string themeName)
        {
            StageId = stageId;
            Courses = courses
                    .Select(item => new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = false
                    })
                    .ToList();
            Courses.Insert(0, new SelectListItem()
            {
                Text = Localization.getMessage("No course"),
                Value = Constants.NoCourseId.ToString(),
                Selected = false
            });

            ThemeTypes = themeTypes
                        .Select(item => new SelectListItem
                        {
                            Text = Converters.ConvertToString(item),
                            Value = item.Id.ToString(),
                            Selected = false
                        })
                        .ToList();
            CourseId = courseId ?? Constants.NoCourseId;
            ThemeTypeId = themeTypeId;
            ThemeName = themeName;
        }
    }
}