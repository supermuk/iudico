using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.CourseMgt.Models;

namespace IUDICO.CourseMgt.Models.Storage
{
    public interface IStorage
    {
        #region Curriculum methods

        IEnumerable<Curriculum> GetCurriculums();
        Curriculum GetCurriculum(int id);
        int? AddCurriculum(Curriculum curriculum);
        bool UpdateCurriculum(int id, Curriculum curriculum);
        bool DeleteCurriculum(int id);
        bool DeleteCurriculums(List<int> ids);

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

        #region Course methods

        List<Course> GetCourses();
        Course GetCourse(int id);
        int? AddCourse(Course course);
        bool UpdateCourse(int id, Course course);
        bool DeleteCourse(int id);
        bool DeleteCourses(List<int> ids);
        string Export(int id);
        int? Import(string path);

        #endregion

        #region Node methods

        List<Node> GetNodes(int courseId);
        List<Node> GetNodes(int courseId, int? parentId);
        Node GetNode(int id);
        int? AddNode(Node node);
        bool UpdateNode(int id, Node node);
        bool DeleteNode(int id);
        bool DeleteNodes(List<int> ids);
        int? CreateCopy(Node node, int? parentId, int position);
        String GetNodeContents(int id);

        #endregion
    }
}