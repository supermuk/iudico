using System.Collections.Generic;

namespace IUDICO.Common.Models.Services
{
    public interface ICurriculumService : IService
    {
        #region Helper methods

        void RefreshState();

        #endregion

        #region Curriculum methods

        IEnumerable<Curriculum> GetCurriculums();
        IEnumerable<Curriculum> GetCurriculums(IEnumerable<int> ids);
        Curriculum GetCurriculum(int id);

        #endregion

        #region Stage methods

        IEnumerable<Stage> GetStages(int curriculumId);
        IEnumerable<Stage> GetStages(IEnumerable<int> ids);
        Stage GetStage(int id);

        #endregion

        #region Theme methods

        IEnumerable<Theme> GetThemes(int stageId);
        IEnumerable<Theme> GetThemes(IEnumerable<int> ids);
        Theme GetTheme(int id);

        #endregion

        #region Assignment methods

        IEnumerable<Group> GetGroups();
        Group GetGroup(int curriculumId);
        IEnumerable<Timeline> GetTimelines();

        #endregion
    }
}
