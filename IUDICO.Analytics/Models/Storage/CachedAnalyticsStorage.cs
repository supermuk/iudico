using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models.Shared;
using IUDICO.Analytics.Models.ViewDataClasses;

namespace IUDICO.Analytics.Models.Storage
{
    public class CachedAnalyticsStorage : IAnalyticsStorage
    {
        private readonly IAnalyticsStorage storage;
        private readonly ICacheProvider cacheProvider;
        private readonly object lockObject = new object();
        
        public CachedAnalyticsStorage(IAnalyticsStorage storage, ICacheProvider cacheProvider)
        {
            this.storage = storage;
            this.cacheProvider = cacheProvider;
        }
        /*
        public void RefreshState()
        {
            throw new NotImplementedException();
        }
        */
        public IEnumerable<ForecastingTree> GetAllForecastingTrees()
        {
            return this.cacheProvider.Get("forecastingtrees", @lockObject, () => this.storage.GetAllForecastingTrees().ToList(), DateTime.Now.AddDays(1), "forecastingtrees");
        }

        public IEnumerable<ForecastingTree> GetForecastingTrees(Guid userRef)
        {
            return this.cacheProvider.Get("forecastingtrees-" + userRef, @lockObject, () => this.storage.GetAllForecastingTrees().Where(f => f.UserRef == userRef).ToList(), DateTime.Now.AddDays(1), "forecastingtrees");
        }

        public IEnumerable<TopicStat> GetRecommenderTopics(User user)
        {
            return this.storage.GetRecommenderTopics(user);

            // return this.cacheProvider.Get("recommendertopics-" + user.Username, @lockObject, () => this.storage.GetRecommenderTopics(user), DateTime.Now.AddDays(1), "recommendertopics-" + user.Username);
        }

        public IEnumerable<TopicStat> GetRecommenderTopics(User user, int amount)
        {
            return this.GetRecommenderTopics(user, amount);

            // return _cacheProvider.Get<IEnumerable<TopicStat>>("recommendertopics-" + user.Username + "", @lockObject, () => _storage.GetRecommenderTopics(user), DateTime.Now.AddDays(1), "recommendertopics-" + user.Username);
        }

        public IEnumerable<Tag> GetTags()
        {
            return this.cacheProvider.Get("tags", @lockObject, () => this.storage.GetTags().ToList(), DateTime.Now.AddDays(1), "tags");
        }

        public Tag GetTag(int id)
        {
            return this.cacheProvider.Get("tag-" + id, @lockObject, () => this.storage.GetTag(id), DateTime.Now.AddDays(1), "tag-" + id);    
        }

        public ViewTagDetails GetTagDetails(int id)
        {
            return this.cacheProvider.Get("tagdetails-" + id, @lockObject, () => this.storage.GetTagDetails(id), DateTime.Now.AddDays(1), "tagdetails-" + id, "tag-" + id);
        }

        public ViewTagDetails GetTagDetailsWithTopics(int id)
        {
            return this.cacheProvider.Get("tagdetails-full-" + id, @lockObject, () => this.storage.GetTagDetailsWithTopics(id), DateTime.Now.AddDays(1), "tagdetails-" + id, "tag-" + id);
        }

        public void CreateTag(Tag feature)
        {
            this.storage.CreateTag(feature);

            this.cacheProvider.Invalidate("tags");
        }

        public void EditTag(int id, Tag feature)
        {
            this.storage.EditTag(id, feature);
            
            this.cacheProvider.Invalidate("tags", "tag-" + id);
        }

        public void DeleteTag(int id)
        {
            this.storage.DeleteTag(id);

            this.cacheProvider.Invalidate("tags", "tag-" + id);
        }

        public void EditTags(int id, IEnumerable<int> topics)
        {
            this.storage.EditTags(id, topics);

            this.cacheProvider.Invalidate("tags", "tag-" + id);
        }

        public Dictionary<Topic, IEnumerable<TopicScore>> GetTopicScores()
        {
            return this.storage.GetTopicScores(); 
        }

        public Dictionary<User, IEnumerable<UserScore>> GetUserScores()
        {
            return this.storage.GetUserScores();
        }

        public void UpdateUserScores(Guid id)
        {
            this.storage.UpdateUserScores(id);

            this.cacheProvider.Invalidate("userscores");
        }

        public void UpdateTopicScores(int id)
        {
            this.storage.UpdateTopicScores(id);

            this.cacheProvider.Invalidate("topicscores");
        }

        public void UpdateAllUserScores()
        {
            this.storage.UpdateAllUserScores();

            this.cacheProvider.Invalidate("userscores");
        }

        public void UpdateAllTopicScores()
        {
            this.storage.UpdateAllTopicScores();

            this.cacheProvider.Invalidate("topicscores");
        }

        #region Anomaly detection

        public IEnumerable<Topic> AvailebleTopics()
        {
            return this.storage.AvailebleTopics();
        }

        public IEnumerable<Group> AvailableGroups(int topicId)
        {
            return this.storage.AvailableGroups(topicId);
        }

        public IEnumerable<KeyValuePair<User, double[]>> GetAllStudentListForTraining(int topicId)
        {
            return this.storage.GetAllStudentListForTraining(topicId);
        }

        public IEnumerable<KeyValuePair<User, double[]>> GetStudentListForTraining(int topicId, int groupId)
        {
            return this.storage.GetStudentListForTraining(topicId, groupId);
        }

        #endregion

        #region Quality
        public double GaussianDistribution(Topic topic)
        {
            return this.storage.GaussianDistribution(topic);
        }
        public double GetTopicTagStatistic(Topic topic)
        {
            return this.storage.GetTopicTagStatistic(topic);
        }
        public double GetCorrTopicStatistic(Topic topic, IEnumerable<Group> groups)
        {
            return this.storage.GetCorrTopicStatistic(topic, groups);
        }
        public double GetDiffTopicStatistic(Topic topic, IEnumerable<Group> groups)
        {
            return this.storage.GetDiffTopicStatistic(topic, groups);
        }
        #endregion
    }
}