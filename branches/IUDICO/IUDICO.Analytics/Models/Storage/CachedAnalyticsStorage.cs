using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models.Shared;
using IUDICO.Analytics.Models.ViewDataClasses;

namespace IUDICO.Analytics.Models.Storage
{
    public class CachedAnalyticsStorage: IAnalyticsStorage
    {
        private readonly IAnalyticsStorage _storage;
        private readonly ICacheProvider _cacheProvider;
        private readonly object lockObject = new object();

        public CachedAnalyticsStorage(IAnalyticsStorage storage, ICacheProvider cacheProvider)
        {
            _storage = storage;
            _cacheProvider = cacheProvider;
        }
        /*
        public void RefreshState()
        {
            throw new NotImplementedException();
        }
        */
        public IEnumerable<ForecastingTree> GetAllForecastingTrees()
        {
            return _cacheProvider.Get<IEnumerable<ForecastingTree>>("forecastingtrees", @lockObject, () => _storage.GetAllForecastingTrees(), DateTime.Now.AddDays(1), "forecastingtrees");
        }

        public IEnumerable<ForecastingTree> GetForecastingTrees(Guid userRef)
        {
            return _cacheProvider.Get<IEnumerable<ForecastingTree>>("forecastingtrees-" + userRef, @lockObject, () => _storage.GetAllForecastingTrees().Where(f => f.UserRef == userRef), DateTime.Now.AddDays(1), "forecastingtrees");
        }

        public IEnumerable<TopicStat> GetRecommenderTopics(User user)
        {
            return _cacheProvider.Get<IEnumerable<TopicStat>>("recommendertopics-" + user.Username, @lockObject, () => _storage.GetRecommenderTopics(user), DateTime.Now.AddDays(1), "recommendertopics-" + user.Username);
        }

        public IEnumerable<TopicStat> GetRecommenderTopics(User user, int amount)
        {
            return GetRecommenderTopics(user).Take(amount);

            //return _cacheProvider.Get<IEnumerable<TopicStat>>("recommendertopics-" + user.Username + "", @lockObject, () => _storage.GetRecommenderTopics(user), DateTime.Now.AddDays(1), "recommendertopics-" + user.Username);
        }

        public IEnumerable<Tag> GetTags()
        {
            return _cacheProvider.Get<IEnumerable<Tag>>("tags", @lockObject, () => _storage.GetTags(), DateTime.Now.AddDays(1), "tags");
        }

        public Tag GetTag(int id)
        {
            return _cacheProvider.Get<Tag>("tag-" + id, @lockObject, () => _storage.GetTag(id), DateTime.Now.AddDays(1), "tag-" + id);    
        }

        public ViewTagDetails GetTagDetails(int id)
        {
            return _cacheProvider.Get<ViewTagDetails>("tagdetails-" + id, @lockObject, () => _storage.GetTagDetails(id), DateTime.Now.AddDays(1), "tagdetails-" + id, "tag-" + id);
        }

        public ViewTagDetails GetTagDetailsWithTopics(int id)
        {
            return _cacheProvider.Get<ViewTagDetails>("tagdetails-full-" + id, @lockObject, () => _storage.GetTagDetailsWithTopics(id), DateTime.Now.AddDays(1), "tagdetails-" + id, "tag-" + id);
        }

        public void CreateTag(Tag feature)
        {
            _storage.CreateTag(feature);

            _cacheProvider.Invalidate("tags");
        }

        public void EditTag(int id, Tag feature)
        {
            _storage.EditTag(id, feature);
            
            _cacheProvider.Invalidate("tags", "tag-" + id);
        }

        public void DeleteTag(int id)
        {
            _storage.DeleteTag(id);

            _cacheProvider.Invalidate("tags", "tag-" + id);
        }

        public void EditTags(int id, IEnumerable<int> topics)
        {
            _storage.EditTags(id, topics);

            _cacheProvider.Invalidate("tags", "tag-" + id);
        }

        public Dictionary<int, IEnumerable<TopicScore>> GetTopicScores()
        {
            return _cacheProvider.Get<Dictionary<int, IEnumerable<TopicScore>>>("topicscores", @lockObject, () => _storage.GetTopicScores(), DateTime.Now.AddDays(1), "topicscores"); 
        }

        public Dictionary<Guid, IEnumerable<UserScore>> GetUserScores()
        {
            return _cacheProvider.Get<Dictionary<Guid, IEnumerable<UserScore>>>("userscores", @lockObject, () => _storage.GetUserScores(), DateTime.Now.AddDays(1), "userscores");
        }

        public void UpdateUserScores(Guid id)
        {
            _cacheProvider.Invalidate("userscores");
        }

        public void UpdateTopicScores(int id)
        {
            _cacheProvider.Invalidate("topicscores");
        }
    }
}