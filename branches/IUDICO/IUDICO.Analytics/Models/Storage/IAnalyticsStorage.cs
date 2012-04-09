using System;
using System.Collections.Generic;
using IUDICO.Analytics.Models.ViewDataClasses;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Models.Storage
{
    public interface IAnalyticsStorage
    {
        #region Analytics methods

        void RefreshState();

        IEnumerable<ForecastingTree> GetAllForecastingTrees();
        IEnumerable<ForecastingTree> GetForecastingTrees(Guid userRef);
        IEnumerable<TopicStat> GetRecommenderTopics(User user);
        IEnumerable<TopicStat> GetRecommenderTopics(User user, int amount);

        #endregion

        IEnumerable<Tag> GetTags();
        Tag GetTag(int id);
        ViewTagDetails GetTagDetails(int id);
        ViewTagDetails GetTagDetailsWithTopics(int id);
        void CreateTag(Tag feature);
        void EditTag(int id, Tag feature);
        void DeleteTag(int id);
        void EditTags(int id, IEnumerable<int> topics);

        Dictionary<int, IEnumerable<TopicScore>> GetTopicScores();
        Dictionary<Guid, IEnumerable<UserScore>> GetUserScores();
        
        void UpdateUserScores(Guid id);
    }
}