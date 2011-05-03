using System.Collections.Generic;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared.CurriculumManagement;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICurriculumStorage
    {
        #region External methods

        void RefreshState();
        IEnumerable<Course> GetCourses();
        Course GetCourse(int id);
        Group GetGroup(int id);
        IEnumerable<Group> GetGroups();
        IEnumerable<Group> GetGroupsByUser(User user);
        IEnumerable<Course> GetCoursesOwnedByUser(User user);

        #endregion

        #region Curriculum methods

        IEnumerable<Curriculum> GetCurriculums();
        IEnumerable<Curriculum> GetCurriculums(IEnumerable<int> ids);
        Curriculum GetCurriculum(int id);
        /// <summary>
        /// Gets the curriculums which have curriculum assignments binded to group
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <returns></returns>
        IEnumerable<Curriculum> GetCurriculumsByGroupId(int groupId);
        /// <summary>
        /// Gets curriculums which have themes owned by user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IEnumerable<Curriculum> GetCurriculumsWithThemesOwnedByUser(User user);
        int AddCurriculum(Curriculum curriculum);
        bool UpdateCurriculum(Curriculum curriculum);
        bool DeleteCurriculum(int id);
        void DeleteCurriculums(IEnumerable<int> ids);

        #endregion

        #region Stage methods

        IEnumerable<Stage> GetStages(int curriculumId);
        IEnumerable<Stage> GetStages(IEnumerable<int> ids);
        int AddStage(Stage stage);
        Stage GetStage(int id);
        bool UpdateStage(Stage stage);
        bool DeleteStage(int id);
        void DeleteStages(IEnumerable<int> ids);

        #endregion

        #region Theme methods

        Theme GetTheme(int id);
        IEnumerable<Theme> GetThemes(IEnumerable<int> ids);
        IEnumerable<Theme> GetThemesByStageId(int stageId);
        IEnumerable<Theme> GetThemesByCurriculumId(int curriculumId);
        IEnumerable<Theme> GetThemesByGroupId(int groupId);
        IEnumerable<Theme> GetThemesByCourseId(int courseId);
        /// <summary>
        /// Gets the themes available for user for current date.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IEnumerable<ThemeDescription> GetThemesAvailableForUser(User user);
        /// <summary>
        /// Gets groups assigned through curriculum assignments to theme.
        /// </summary>
        /// <param name="themeId">The theme id.</param>
        /// <returns></returns>
        IEnumerable<Group> GetGroupsAssignedToTheme(int themeId);
        int AddTheme(Theme theme);
        bool UpdateTheme(Theme theme);
        bool DeleteTheme(int id);
        void DeleteThemes(IEnumerable<int> ids);
        Theme ThemeUp(int themeId);
        Theme ThemeDown(int themeId);

        #endregion

        #region ThemeType methods

        IEnumerable<ThemeType> GetThemeTypes();

        #endregion

        #region CurriculumAssignment methods

        CurriculumAssignment GetCurriculumAssignment(int curriculumAssignmentId);
        IEnumerable<CurriculumAssignment> GetCurriculumAssignments(IEnumerable<int> ids);
        IEnumerable<CurriculumAssignment> GetCurriculumAssignmnetsByCurriculumId(int curriculumId);
        IEnumerable<CurriculumAssignment> GetCurriculumAssignmentsByGroupId(int groupId);
        IEnumerable<CurriculumAssignment> GetCurriculumAssignments();
        int AddCurriculumAssignment(CurriculumAssignment curriculumAssignment);
        bool UpdateCurriculumAssignment(CurriculumAssignment curriculumAssignment);
        bool DeleteCurriculumAssignment(int id);
        void DeleteCurriculumAssignments(IEnumerable<int> ids);

        #endregion

        #region ThemeAssignment methods

        ThemeAssignment GetThemeAssignment(int themeAssignmentId);
        IEnumerable<ThemeAssignment> GetThemeAssignmentsByCurriculumAssignmentId(int curriculumAssignmentId);
        IEnumerable<ThemeAssignment> GetThemeAssignmentsByThemeId(int themeId);
        IEnumerable<ThemeAssignment> GetThemeAssignments(IEnumerable<int> ids);
        int AddThemeAssignment(ThemeAssignment themeAssignment);
        bool UpdateThemeAssignment(ThemeAssignment themeAssignment);
        void DeleteThemeAssignments(IEnumerable<int> ids);

        #endregion

        #region Timeline methods

        Timeline GetTimeline(int TimelineId);
        IEnumerable<Timeline> GetTimelines(IEnumerable<int> timelineIds);
        IEnumerable<Timeline> GetCurriculumAssignmentTimelines(int curriculumAssignmentId);
        IEnumerable<Timeline> GetStageTimelinesByCurriculumAssignmentId(int curriculumAssignmentId);
        IEnumerable<Timeline> GetStageTimelinesByStageId(int stageId);
        IEnumerable<Timeline> GetStageTimelines(int stageId, int curriculumAssignmentId);
        int AddTimeline(Timeline timeline);
        bool UpdateTimeline(Timeline timeline);
        bool DeleteTimeline(int timelineId);
        void DeleteTimelines(IEnumerable<int> timelineIds);

        #endregion

        #region Group methods

        IEnumerable<Group> GetAssignedGroups(int curriculumId);
        IEnumerable<Group> GetNotAssignedGroups(int curriculumId);
        /// <summary>
        /// Gets not assigned groups for curriculum including current group.
        /// </summary>
        /// <param name="curriculumId">The curriculum id.</param>
        /// <param name="currentGroupId">The current group id.</param>
        /// <returns></returns>
        IEnumerable<Group> GetNotAssignedGroupsWithCurrentGroup(int curriculumId, int currentGroupId);

        #endregion
    }
}
