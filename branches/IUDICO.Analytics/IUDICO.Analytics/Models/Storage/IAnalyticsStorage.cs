using System;
using System.Collections.Generic;
using System.Web.Mvc;
using IUDICO.Analytics.Models.ViewDataClasses;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Shared.Statistics;

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
        void EditTopics(int id, IEnumerable<int> topics);

        #region Anomaly detection

        IEnumerable<Topic> AvailebleTopics();
        IEnumerable<Group> AvailebleGroups(int topicId);
        IEnumerable<KeyValuePair<User, double[]>> GetAllStudentListForTraining(int topicId);
        IEnumerable<KeyValuePair<User, double[]>> GetStudentListForTraining(int topicId, int groupId);

        #endregion
    }
}