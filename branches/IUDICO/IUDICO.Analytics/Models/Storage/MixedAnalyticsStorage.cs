using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Analytics.Models.ViewDataClasses;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Shared.Statistics;

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

        #region GetRecommended Topics by User Perfomance

        protected IEnumerable<TopicFeature> GetTopicFeatures(Func<TopicFeature, bool> predicate)
        {
            return _Db.TopicFeatures.Where(predicate).Select(tf => new { Feature = tf.Feature, Topic = tf.Topic, TopicFeature = tf }).AsEnumerable().Select(a => a.TopicFeature);
        }

        protected IEnumerable<TopicFeature> GetTopicFeaturesAvailableForUser(User user)
        {
            var topics = GetTopicsAvailableForUser(user).Select(t => t.Topic.Id);

            return _Db.TopicFeatures.Where(tf => topics.Contains(tf.TopicId));
        }

        protected Dictionary<int, double> GetUserPerfomance(User user)
        {
            var features = new Dictionary<int, double>();
            var count = new Dictionary<int, int>();

            var attempts = GetResults(user).GroupBy(a => a.User).Select(g => g.First()).
                    ToDictionary(a => a.Topic.Id, a => a);
            var topicFeatures =
                GetTopicFeatures(f => attempts.Keys.Contains(f.TopicId)).
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
                GetTopicFeaturesAvailableForUser(user).GroupBy(t => t.TopicId)
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

        #region Get Recommended Topics by Score

        public IEnumerable<TopicStat> GetRecommendedItems(User user, IEnumerable<TopicSimilarity> similarItems)
        {
            var userScores = user.UserTopicScores;
            var ratedTopics = userScores.Select(s => s.TopicId);
            var scores = new Dictionary<int, double>();
            var totalSim = new Dictionary<int, double>();

            foreach (var userScore in userScores)
            {
                foreach (var similarItem in GetSimilarItems(similarItems, userScore.TopicId))
                {
                    if (ratedTopics.Contains(similarItem.Topic2Id))
                    {
                        continue;
                    }

                    scores[similarItem.Topic2Id] += similarItem.Similarity*userScore.Score;
                    totalSim[similarItem.Topic2Id] += similarItem.Similarity;
                }
            }

            var rankings = scores.Select(s => new TopicStat(s.Key, s.Value/totalSim[s.Key])).ToList();

            rankings.Sort();

            return rankings;
        }

        protected IEnumerable<TopicSimilarity> GetSimilarItems(IEnumerable<TopicSimilarity> similarItems, int topicId)
        {
            var v1 = similarItems.Where(t => t.Topic1Id == topicId);
            var v2 = similarItems.Where(t => t.Topic2Id == topicId).Select(t => new TopicSimilarity(t.Topic2Id, t.Topic1Id, t.Similarity));

            return v1.Concat(v2);
        }

        public IEnumerable<TopicSimilarity> CalculateSimilarItems(IEnumerable<Topic> topics, int n)
        {
            return topics.SelectMany(t => TopMatches(t, topics, n));
        }

        protected IEnumerable<TopicSimilarity> TopMatches(Topic topic, IEnumerable<Topic> topics, int n)
        {
            var topicStats = topics.Where(t => t.Id != topic.Id).Select(t => new TopicSimilarity(topic.Id, t.Id, PearsonDistance(topic, t))).ToList();
            
            topicStats.Sort();

            return topicStats.Take(n);
        }

        protected double PearsonDistance(Topic t1, Topic t2)
        {
            var t1s = t1.UserTopicScores.ToDictionary(t => t.UserId, t => t.Score);
            var t2s = t2.UserTopicScores.ToDictionary(t => t.UserId, t => t.Score);

            var commonUsers = t1s.Select(t => t.Key).Intersect(t2s.Select(t => t.Key));
            var n = commonUsers.Count();

            var t1c = t1s.Where(t => commonUsers.Contains(t.Key)).Select(t => 1.0 * t.Value);
            var t2c = t2s.Where(t => commonUsers.Contains(t.Key)).Select(t => 1.0 * t.Value);

            var sum1 = t1c.Sum();
            var sum2 = t2c.Sum();

            var sum1Sq = t1c.Sum(x => x * x);
            var sum2Sq = t2c.Sum(x => x * x);

            var pSum = commonUsers.Sum(user => (t1s[user]*t2s[user]));

            var num = pSum - (sum1 * sum2 / n);
            var den = Math.Sqrt((sum1Sq - Math.Pow(sum1, 2)/n)*(sum2Sq - Math.Pow(sum2, 2)/n));

            return den == 0 ? 0 : num/den;
        }

        #endregion

        #region Get Topic Validity Score

        public GroupTopicStat GetGroupTopicStatistic(Topic topic, Group group)
        {
            var results = GetResults(topic);

            var groupResults = results.Where(r => r.User.GroupUsers.Select(g => g.GroupRef)
                                      .Contains(group.Id))
                                      .Select(r => new UserRating(r.User, r.Score.ToPercents().Value))
                                      .ToList();
            
            var usersParticipated = groupResults.Select(g => g.User);
            var n = usersParticipated.Count();

            if (n == 0)
            {
                return new GroupTopicStat(0, 0);
            }

            var usersIds = usersParticipated.Select(u => u.Id);

            var groupRatings = group.GroupUsers
                                    .Select(gu => new UserRating(gu.User, 1.0 * gu.User.TestsSum / gu.User.TestsTotal))
                                    .Where(gu => usersIds.Contains(gu.User.Id))
                                    .ToList();

            groupResults.Sort();
            groupRatings.Sort();

            var ratResults = groupResults.Select((r, i) => new { User = r.User, Index = i }).ToDictionary(a => a.User, a => a.Index);
            var ratRatings = groupRatings.Select((r, i) => new { User = r.User, Index = i }).ToDictionary(a => a.User, a => a.Index);

            var ratingDifference = 1.0 * usersParticipated.Sum(u => Math.Abs(ratResults[u] - ratRatings[u]));
            var ratingMax = 2*((n + 1)/2)*(n/2);
            var ratingNormalized = ratingDifference/ratingMax;

            var diffResults = groupResults.ToDictionary(a => a.User, a => a.Score);
            var diffRatings = groupRatings.ToDictionary(a => a.User, a => a.Score);

            var scoreDifference = 1.0 * usersParticipated.Sum(u => Math.Abs(diffResults[u] - diffRatings[u]));
            var scoreMax = n*100;
            var scoreNormalized = scoreDifference / scoreMax;

            return new GroupTopicStat(ratingNormalized, scoreNormalized);
        }

        #endregion

        #endregion

        #region Helper Methods

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

        public void EditTopics(int id, IEnumerable<int> topics)
        {
            var allTopics = _Db.TopicFeatures.Where(tf => tf.FeatureId == id).AsEnumerable();
            var deletedTopics = allTopics.Where(tf => !topics.Contains(tf.TopicId));
            var addTopics = topics.Where(i => !allTopics.Select(t => t.TopicId).Contains(i));

            _Db.TopicFeatures.DeleteAllOnSubmit(deletedTopics);

            foreach (var topic in addTopics)
            {
                var tf = new TopicFeature { FeatureId = id, TopicId = topic };

                _Db.TopicFeatures.InsertOnSubmit(tf);
            }

            _Db.SubmitChanges();
        }

        public ViewFeatureDetails GetFeatureDetailsWithTopics(int id)
        {
            var feature = GetFeatureDetails(id);
            var featureTopicsIds = feature.Topics.Select(t => t.Id);
            feature.AvailableTopics = GetTopics().Where(t => !featureTopicsIds.Contains(t.Id));

            return feature;
        }

        #endregion

        #region Other Service Methods

        protected IEnumerable<Topic> GetTopics()
        {
            return _LmsService.FindService<IDisciplineService>().GetTopics();
        }

        protected IEnumerable<TopicDescription> GetTopicsAvailableForUser(User user)
        {
            return _LmsService.FindService<ICurriculumService>().GetTopicDescriptions(user);
        }

        protected IEnumerable<AttemptResult> GetResults(User user)
        {
            return _LmsService.FindService<ITestingService>().GetResults(user);
        }

        protected IEnumerable<AttemptResult> GetResults(Topic topic)
        {
            return _LmsService.FindService<ITestingService>().GetResults(topic);
        }

        #endregion
    }
}