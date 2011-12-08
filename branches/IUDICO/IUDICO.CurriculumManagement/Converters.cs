using IUDICO.CurriculumManagement.Models.Enums;
using System;
namespace IUDICO.CurriculumManagement
{
    public static class Converters
    {
        public static ThemeType ConvertToThemeType(IUDICO.Common.Models.Shared.ThemeType themeType)
        {
            switch (themeType.Name)
            {
                case "Test":return ThemeType.Test;
                case "Theory":return ThemeType.Theory;
                case "TestWithoutCourse": return ThemeType.TestWithoutCourse;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public static string ConvertToString(IUDICO.Common.Models.Shared.ThemeType themeType)
        {
            ThemeType enumThemeType = ConvertToThemeType(themeType);
            switch (enumThemeType)
            {
                case ThemeType.Test: return Localization.getMessage("ThemeType.Test");
                case ThemeType.Theory: return Localization.getMessage("ThemeType.Theory");
                case ThemeType.TestWithoutCourse: return  Localization.getMessage("ThemeType.TestWithoutCourse");
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}