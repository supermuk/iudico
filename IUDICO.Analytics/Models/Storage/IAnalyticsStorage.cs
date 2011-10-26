using System.Collections.Generic;
using IUDICO.Common.Models;

namespace IUDICO.Analytics.Models.Storage
{
    public interface IAnalyticsStorage
    {
        #region Analytics methods

        void RefreshState();

        IEnumerable<ForecastingTree> GetAllForecastingTrees();
        IEnumerable<ForecastingTree> GetForecastingTrees(System.Guid UserRef);

        #endregion
    }
}