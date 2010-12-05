using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    public interface ICurriculumStorage
    {
        #region Curriculum methods

        IEnumerable<Curriculum> GetCurriculums();
        Curriculum GetCurriculum(int id);
        int? AddCurriculum(Curriculum curriculum);
        bool UpdateCurriculum(int id, Curriculum curriculum);
        bool DeleteCurriculum(int id);
        bool DeleteCurriculums(IEnumerable<int> ids);

        #endregion

        #region Stage methods

        IEnumerable<Stage> GetStages(int curriculumId);
        int? AddStage(Stage stage);
        Stage GetStage(int id);
        bool UpdateStage(int id, Stage stage);
        bool DeleteStage(int id);
        bool DeleteStages(IEnumerable<int> ids);

        #endregion

        #region Theme methods

        IEnumerable<Theme> GetThemes(int stageId);
        Theme GetTheme(int id);
        int? AddTheme(Theme theme);
        bool UpdateTheme(int id, Theme theme);
        bool ThemeUp(int themeId);
        bool ThemeDown(int themeId);

        #endregion

        #region Temporary

        Course GetCourse(int id);
        List<Course> GetCourses();

        #endregion
    }
}