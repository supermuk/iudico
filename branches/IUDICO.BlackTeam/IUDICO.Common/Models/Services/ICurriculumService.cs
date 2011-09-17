using System.Collections.Generic;
using IUDICO.Common.Models.Shared.CurriculumManagement;

namespace IUDICO.Common.Models.Services
{
    public interface ICurriculumService : IService
    {
        #region Curriculum methods

        IEnumerable<Curriculum> GetCurriculums();
        IEnumerable<Curriculum> GetCurriculums(IEnumerable<int> ids);
        Curriculum GetCurriculum(int id);
        /// <summary>
        /// Gets curriculums which have themes owned by user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IEnumerable<Curriculum> GetCurriculumsWithThemesOwnedByUser(User user);

        #endregion

        #region Stage methods

        IEnumerable<Stage> GetStages(int curriculumId);
        IEnumerable<Stage> GetStages(IEnumerable<int> ids);
        Stage GetStage(int id);

        #endregion

        #region Theme methods

        IEnumerable<Theme> GetThemesByStageId(int stageId);
        IEnumerable<Theme> GetThemesByCurriculumId(int curriculumId);
        IEnumerable<Theme> GetThemes(IEnumerable<int> ids);
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
        Theme GetTheme(int id);

        #endregion

        #region Assignment methods

        IEnumerable<Curriculum> GetCurriculumsByGroupId(int groupId);
        IEnumerable<Theme> GetThemesByGroupId(int groupId);

        #endregion
    }
}
