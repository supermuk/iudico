using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Analytics.Models.ViewDataClasses;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Models.Storage
{
    public class MixedAnalyticsStorage : IAnalyticsStorage
    {
        private readonly ILmsService _LmsService;
        private DBDataContext _Db;

        protected DBDataContext GetDbDataContext()
        {
            return new DBDataContext();
        }

        public MixedAnalyticsStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
            RefreshState();
        }

        public void RefreshState()
        {
            _Db = new DBDataContext();
        }

        #region Analytics methods

        public IEnumerable<ForecastingTree> GetAllForecastingTrees()
        {
            IEnumerable<ForecastingTree> query;
            query = _Db.ForecastingTrees.Where(x => x.IsDeleted == false);
            return query;
        }

        public IEnumerable<ForecastingTree> GetForecastingTrees(Guid UserRef)
        {
            IEnumerable<ForecastingTree> query;
            query = _Db.ForecastingTrees.Where(x => x.UserRef == UserRef && x.IsDeleted == false);
            return query;
        }

        #region GetRecommended Topics by Perfomance

        protected Dictionary<int, double> GetUserPerfomance(User user)
        {
            var features = new Dictionary<int, double>();
            var count = new Dictionary<int, int>();

            var attempts =
                _LmsService.FindService<ITestingService>().GetResults(user).GroupBy(a => a.User).Select(g => g.First()).
                    ToDictionary(a => a.Topic.Id, a => a);
            var topicFeatures =
                _LmsService.FindService<ICurriculumService>().GetTopicFeatures(f => attempts.Keys.Contains(f.TopicId)).
                    GroupBy(t => t.TopicId).ToDictionary(g => g.Key, g => g.Select(t => t.FeatureId));

            if (attempts.Count() == 0)
            {
                return features;
            }

            foreach (var topic in topicFeatures)
            {
                if (!attempts.ContainsKey(topic.Key))
                {
                    continue;
                }

                var score = attempts[topic.Key].Score.ToPercents();

                if (score == null)
                {
                    continue;
                }

                foreach (var feature in topic.Value)
                {
                    features[topic.Key] += score.Value;
                    count[topic.Key]++;
                }
            }

            foreach (var feature in features)
            {
                features[feature.Key] /= count[feature.Key];
            }

            return features;
        }

        protected double GetScore(IEnumerable<int> features, Dictionary<int, double> values)
        {
            var similarity = JaccardDistance(features, values.Keys);
            var sum = values.Where(kv => features.Contains(kv.Key)).Sum(kv => kv.Value);

            return similarity*sum;
        }

        public IEnumerable<TopicStat> GetRecommendedTopics(User user)
        {
            var userPerfomance = GetUserPerfomance(user);
            
            var topicFeatures =
                _LmsService.FindService<ICurriculumService>().GetTopicFeaturesAvailableToUser(user).GroupBy(t => t.TopicId)
                    .Select(g => new TopicStat(g.Key, GetScore(g.Select(k => k.FeatureId), userPerfomance))).ToList();

            topicFeatures.Sort();

            return topicFeatures;
        }

        //Jaccard or Tanimoto
        protected double JaccardDistance(IEnumerable<int> t1, IEnumerable<int> t2)
        {
            var common = t1.Intersect(t2).Count();

            return 1.0 - (1.0*common/(t1.Count() + t2.Count() - common));
        }

        #endregion

//        public IEnumerable<int> GetDistances(int topicId)
//        {
//            var attempts = _LmsService.FindService<ITestingService>().GetAllAttempts();
//
//            var topicFeature = topicFeatures.Where(t => t.Topic == topicId).SingleOrDefault();
//
//            if (topicFeature == null)
//            {
//                return null;
//            }
//
//            return topicFeatures.Where(t => t.Topic != topicId).Select(t => HammingDistance(topicFeature.Features, t.Features)).ToList();
//        }

        #endregion

        #region Getter Methods

        public IEnumerable<Feature> GetFeatures()
        {
            return _Db.Features;
        }

        public ViewFeatureDetails GetFeatureDetails(int id)
        {
            return _Db.Features.Where(f => f.Id == id)
                .Select(f => new ViewFeatureDetails(f, f.TopicFeatures.Select(t => t.Topic)))
                .AsEnumerable()
                .SingleOrDefault();
        }

        public void CreateFeature(Feature feature)
        {
            _Db.Features.InsertOnSubmit(feature);
            _Db.SubmitChanges();
        }

        public Feature GetFeature(int id)
        {
            return _Db.Features.SingleOrDefault(f => f.Id == id);
        }

        public void EditFeature(int id, Feature feature)
        {
            var oldFeature = GetFeature(id);

            oldFeature.Name = feature.Name;

            _Db.SubmitChanges();
        }

        public void DeleteFeature(int id)
        {
            var containsFeatures = _Db.TopicFeatures.Where(tf => tf.FeatureId == id).Any();

            if (containsFeatures)
            {
                throw new Exception("Can't delete feature, which has topics assigned to it");
            }

            _Db.Features.DeleteOnSubmit(GetFeature(id));
        }

        public ViewFeatureDetails GetFeatureDetailsWithTopics(int id)
        {
            var feature = GetFeatureDetails(id);
            feature.AvailableTopics = GetTopics().Except(feature.Topics);

            return feature;
        }

        #endregion

        #region Other Service Methods

        protected IEnumerable<Topic> GetTopics()
        {
            return _LmsService.FindService<ICurriculumService>().GetTopics();
        }

        #endregion
    }
}