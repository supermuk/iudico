using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.Common.Models;

namespace IUDICO.UnitTests
{
    public static class Utils
    {
        public static Curriculum GetDefaultCurriculum()
        {
            return new Curriculum() { Name = "Curriculum" };
        }

        public static Stage GetDefaultStage(int curriculumId)
        {
            return new Stage() { CurriculumRef = curriculumId, Name = "Stage" };
        }

        public static Theme GetDefaultTheme(int stageId, int courseId)
        {
            return new Theme() { StageRef = stageId, CourseRef = courseId, Name = "Theme", ThemeTypeRef = 1 };
        }
    }
}
