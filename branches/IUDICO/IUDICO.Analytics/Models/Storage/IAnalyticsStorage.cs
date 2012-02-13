using System;
using System.Collections.Generic;
using System.Web.Mvc;
using IUDICO.Analytics.Models.ViewDataClasses;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Models.Storage
{
    public interface IAnalyticsStorage
    {
        #region Analytics methods

        void RefreshState();

        IEnumerable<ForecastingTree> GetAllForecastingTrees();
        IEnumerable<ForecastingTree> GetForecastingTrees(Guid UserRef);
        IEnumerable<TopicStat> GetRecommendedTopics(User user);

        #endregion

        IEnumerable<Feature> GetFeatures();
        ViewFeatureDetails GetFeatureDetails(int id);
        ViewFeatureDetails GetFeatureDetailsWithTopics(int id);
        void CreateFeature(Feature feature);
        Feature GetFeature(int id);
        void EditFeature(int id, Feature feature);
        void DeleteFeature(int id);
    }
}