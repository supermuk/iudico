using System.Collections.Generic;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.Statistics.Models.Storage
{
    public interface IStatisticsStorage
    {
        #region Methods for statistic

        IEnumerable<Group> GetAllGroups();
        IEnumerable<Curriculum> GetCurrilulumsByGroupId(int groupId);
        IEnumerable<Curriculum> GetSelectedCurriclums(System.Int32[] curriculumsId);
        IEnumerable<User> GetUsersBySelectedGroup(Group group);
        IEnumerable<AttemptResult> GetResults(User user, Theme theme);
        List<KeyValuePair<List<Theme>, int>> GetAllThemes(int groupId);

        #endregion
    }
}
