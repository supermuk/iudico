using System.Collections.Generic;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Models.Storage
{
    public interface IStatisticsProxy
    {
        #region Methods for statistic

        IEnumerable<Group> GetAllGroups();
        IEnumerable<Curriculum> GetCurrilulumsByGroupId(int groupId);

        #endregion
    }
}
