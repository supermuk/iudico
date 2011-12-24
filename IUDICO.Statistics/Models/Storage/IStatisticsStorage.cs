using System;
using System.Collections.Generic;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Models.Storage
{
    public interface IStatisticsStorage
    {
        IEnumerable<Group> GetAllGroups();

        double GetTotalForUserCurriculum(Guid userId, int curriculumId);
        int GetCurriculumIdByThemeId(int themeId);

        void SaveManualResult(Guid userId, int themeId, double score);


        Group GetGroupById(int id);

        IEnumerable<Curriculum> GetCurrilulumsByGroupId(int id);
    }
}
