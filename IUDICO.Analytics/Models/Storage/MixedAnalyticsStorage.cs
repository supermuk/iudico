using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Analytics.Models.ViewDataClasses;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Models.Shared.DisciplineManagement;

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
            //RefreshState();
        }
        /*
        public void RefreshState()
        {
            _Db = new DBDataContext();
        }
        */
        #region Analytics methods

        public IEnumerable<ForecastingTree> GetAllForecastingTrees()
        { 
            return GetDbDataContext().ForecastingTrees.Where(x => x.IsDeleted == false);;
        }

        public IEnumerable<ForecastingTree> GetForecastingTrees(Guid UserRef)
        {
            return GetDbDataContext().ForecastingTrees.Where(x => x.UserRef == UserRef && x.IsDeleted == false);
        }

        /*

        #region GetRecommended Topics by User Perfomance

        

        protected IEnumerable<TopicFeature> GetTopicFeaturesAvailableForUser(User user)
        {
            var topics = GetTopicsAvailableForUser(user).Select(t => t.Topic.Id);

            return _Db.TopicFeatures.Where(tf => topics.Contains(tf.TopicId));
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

        */

        #region Recommender System

        public Dictionary<int, IEnumerable<TopicScore>> GetTopicScores()
        {
            using (var db = GetDbDataContext())
            {
                var topics = this.GetTopics();
                var topicIds = topics.Select(t => t.Id);
                var topicScores = db.TopicScores.Where(s => topicIds.Contains(s.TopicId))
                    .GroupBy(e => e.TopicId)
                    .OrderBy(g => g.Key).ToDictionary(g => g.Key, g => g.AsEnumerable());
            
                return topicScores;
            }
        }

        public Dictionary<Guid, IEnumerable<UserScore>> GetUserScores()
        {
            using (var db = GetDbDataContext())
            {
                var users = this.GetUsers();
                var userIds = users.Select(u => u.Id);
                var userScores = db.UserScores.Where(s => userIds.Contains(s.UserId))
                    .GroupBy(e => e.UserId)
                    .OrderBy(g => g.Key).ToDictionary(g => g.Key, g => g.AsEnumerable());

                return userScores;
            }
        }

        public void UpdateUserScores(Guid id)
        {
            using (var db = GetDbDataContext())
            {
                var user = this.GetUser(id);
                var userTagScores = this.GetUserTagScores(user);

                db.UserScores.DeleteAllOnSubmit(db.UserScores.Where(us => us.UserId == id));
                db.UserScores.InsertAllOnSubmit(userTagScores);

                db.SubmitChanges();
            }
        }

        protected IEnumerable<TopicTag> GetTopicTags(Func<TopicTag, bool> predicate)
        {
            return GetDbDataContext().TopicTags.Where(predicate).Select(tt => new { Tag = tt.Tag, TopicTag = tt }).AsEnumerable().Select(a => a.TopicTag);
        }

        protected IEnumerable<UserScore> GetUserTagScores(User user)
        {
            var tags = new Dictionary<int, float>();
            var count = new Dictionary<int, int>();

            var attempts = GetResults(user).GroupBy(a => new {UserId = a.User.Id, TopicId = a.CurriculumChapterTopic.TopicRef}).Select(g => g.First()).ToDictionary(a => a.CurriculumChapterTopic.TopicRef, a => a);
            var topicTags = GetTopicTags(f => attempts.Keys.Contains(f.TopicId)).GroupBy(t => t.TopicId).ToDictionary(g => g.Key, g => g.Select(t => t.TagId));

            if (attempts.Count() == 0)
            {
                return new List<UserScore>();
            }

            foreach (var topic in topicTags)
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

                foreach (var tag in topic.Value)
                {
                    tags[tag] += (float)score.Value;
                    count[tag]++;
                }
            }

            foreach (var kv in tags)
            {
                tags[kv.Key] /= count[kv.Key];
            }

            return tags.Select(ut => new UserScore { UserId = user.Id, TagId = ut.Key, Score = ut.Value });
        }

        public void UpdateTopicScores(int id)
        {
            using (var db = GetDbDataContext())
            {
                var topic = this.GetTopic(id);
                var topicTagScores = this.GetTopicTagScores(topic);

                db.TopicScores.DeleteAllOnSubmit(db.TopicScores.Where(us => us.TopicId == id));
                db.TopicScores.InsertAllOnSubmit(topicTagScores);

                db.SubmitChanges();
            }
        }

        protected IEnumerable<TopicScore> GetTopicTagScores(Topic topic)
        {
            var topicTags = this.GetTopicTags(t => t.TopicId == topic.Id);
            var attempts = GetResults(topic).GroupBy(a => new { UserId = a.User.Id, TopicId = a.CurriculumChapterTopic.TopicRef }).Select(g => g.First());
            var total = 0.0;
            var count = 0;

            foreach (var attempt in attempts)
            {
                var score = attempt.Score.ToPercents();

                if (score == null)
                {
                    continue;
                }

                total += score.Value;
                count++;
            }

            var average = (float)(total/count);

            var tags = topicTags.Select(t => new TopicScore { TagId = t.TagId, TopicId = t.TopicId, Score = average });

            return tags;
        }

        protected double PearsonDistance(User user, Topic topic)
        {
            var userTagScores = _Db.UserScores.Where(s => s.UserId == user.Id).ToDictionary(s => s.TagId, s => 100 - s.Score);
            var topicTagScores = _Db.TopicScores.Where(s => s.TopicId == topic.Id).ToDictionary(s => s.TagId, s => s.Score);

            var commonTags = userTagScores.Select(t => t.Key).Intersect(topicTagScores.Select(t => t.Key));
            var n = commonTags.Count();

            var userCommonScores = userTagScores.Where(t => commonTags.Contains(t.Key)).Select(t => 1.0 * t.Value);
            var topicCommonScores = topicTagScores.Where(t => commonTags.Contains(t.Key)).Select(t => 1.0 * t.Value);

            var sum1 = userCommonScores.Sum();
            var sum2 = topicCommonScores.Sum();

            var sum1Sq = userCommonScores.Sum(x => x * x);
            var sum2Sq = topicCommonScores.Sum(x => x * x);

            var pSum = commonTags.Sum(tag => (userTagScores[tag] * topicTagScores[tag]));

            var num = pSum - (sum1 * sum2 / n);
            var den = Math.Sqrt((sum1Sq - Math.Pow(sum1, 2) / n) * (sum2Sq - Math.Pow(sum2, 2) / n));

            return den == 0 ? 0 : num / den;
        }

        public IEnumerable<TopicStat> GetRecommenderTopics(User user)
        {
            return GetRecommenderTopics(user, 0);
        }

        public IEnumerable<TopicStat> GetRecommenderTopics(User user, int amount)
        {
            var topics = this.GetTopicsAvailableForUser(user);
            var list = topics.Select(topic => new TopicStat(topic.Topic, this.PearsonDistance(user, topic.Topic))).ToList();

            list.Sort();

            return (amount > 0 ? list.Take(amount) : list);
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

        public IEnumerable<Tag> GetTags()
        {
            return _Db.Tags;
        }

        public ViewTagDetails GetTagDetails(int id)
        {
            var tag = _Db.Tags.SingleOrDefault(f => f.Id == id);
            var topicIds = _Db.TopicTags.Where(t => t.TagId == tag.Id).Select(t => t.TopicId);
            var topics = GetTopics(topicIds);

            return new ViewTagDetails(tag, topics);
        }

        public void CreateTag(Tag tag)
        {
            _Db.Tags.InsertOnSubmit(tag);
            _Db.SubmitChanges();
        }

        public Tag GetTag(int id)
        {
            return _Db.Tags.SingleOrDefault(f => f.Id == id);
        }

        public void EditTag(int id, Tag tag)
        {
            var oldFeature = GetTag(id);

            oldFeature.Name = tag.Name;

            _Db.SubmitChanges();
        }

        public void DeleteTag(int id)
        {
            var containsFeatures = _Db.TopicTags.Where(tf => tf.TagId == id).Any();

            if (containsFeatures)
            {
                throw new Exception("Can't delete feature, which has topics assigned to it");
            }

            _Db.Tags.DeleteOnSubmit(GetTag(id));
        }

        public void EditTags(int id, IEnumerable<int> topics)
        {
            var allTopics = _Db.TopicTags.Where(tf => tf.TagId == id).AsEnumerable();
            var deletedTopics = allTopics.Where(tf => !topics.Contains(tf.TopicId));
            var addTopics = topics.Where(i => !allTopics.Select(t => t.TopicId).Contains(i));

            _Db.TopicTags.DeleteAllOnSubmit(deletedTopics);

            var topticTags = new List<TopicTag>();

            foreach (var topic in addTopics)
            {
                topticTags.Add(new TopicTag { TagId = id, TopicId = topic });
            }

            _Db.TopicTags.InsertAllOnSubmit(topticTags);
            _Db.SubmitChanges();
        }

        public ViewTagDetails GetTagDetailsWithTopics(int id)
        {
            var feature = GetTagDetails(id);
            var featureTopicsIds = feature.Topics.Select(t => t.Id);
            feature.AvailableTopics = GetTopics().Where(t => !featureTopicsIds.Contains(t.Id));

            return feature;
        }

        #endregion

        #region Other Service Methods

        protected IEnumerable<User> GetUsers()
        {
            return _LmsService.FindService<IUserService>().GetUsers();
        }

        protected User GetUser(Guid id)
        {
            return _LmsService.FindService<IUserService>().GetUsers(u => u.Id == id).SingleOrDefault();
        }

        protected Topic GetTopic(int id)
        {
            return _LmsService.FindService<IDisciplineService>().GetTopic(id);
        }

        protected IEnumerable<Topic> GetTopics()
        {
            return _LmsService.FindService<IDisciplineService>().GetTopics();
        }

        protected IEnumerable<Topic> GetTopics(IEnumerable<int> ids)
        {
            return _LmsService.FindService<IDisciplineService>().GetTopics(ids);
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

        #region Anomaly detection

        public IEnumerable<Topic> AvailebleTopics()
        {
            User teacherUser = _LmsService.FindService<IUserService>().GetCurrentUser();
            List<Topic> listOfAvailableTopics = new List<Topic>();

            var ownedDisciplines = _LmsService.FindService<IDisciplineService>()
                .GetDisciplines(item => item.Owner == teacherUser.Username);

            foreach(Discipline discipline in ownedDisciplines)
            {
                listOfAvailableTopics.AddRange(_LmsService.FindService<IDisciplineService>().GetTopicsByDisciplineId(discipline.Id)
                    .Where(item => item.TestTopicTypeRef != null));
            }

            return listOfAvailableTopics;
        }

        public IEnumerable<Group> AvailebleGroups(int topicId)
        {
            List<Group> listOfAvailableGroups = new List<Group>();
           
            var topic = _LmsService.FindService<IDisciplineService>().GetTopic(topicId);
            var groups = _LmsService.FindService<IUserService>().GetGroups();

            foreach (Group group in groups)
            {
                if(_LmsService.FindService<IDisciplineService>()
                    .GetTopicsByGroupId(group.Id).Contains(topic))
                {
                    listOfAvailableGroups.Add(group);
                }
            }

            return listOfAvailableGroups;
        }

        public IEnumerable<KeyValuePair<User,double[]>> GetAllStudentListForTraining(int topicId)
        {

            List<KeyValuePair<User, double[]>> result = new List<KeyValuePair<User, double[]>>();
            List<Group> listOfAvailableGroups = new List<Group>();

            var topic = _LmsService.FindService<IDisciplineService>().GetTopic(topicId);
            var groups = _LmsService.FindService<IUserService>().GetGroups();
            var curriculumChapterTopic = topic.CurriculumChapterTopics.Where(item => item.TopicRef == topic.Id).First();

            foreach (Group group in groups)
            {
                if (_LmsService.FindService<IDisciplineService>()
                    .GetTopicsByGroupId(group.Id).Contains(topic))
                {
                    listOfAvailableGroups.Add(group);
                }
            }

            foreach (Group group in listOfAvailableGroups)
            {
                var students = _LmsService.FindService<IUserService>().GetUsersByGroup(group);

                foreach (User student in students)
                {
                    var value = new double[2 + this.GetTags().Count()];

                    var studentScore = _LmsService.FindService<ITestingService>()
                        .GetResults(student, curriculumChapterTopic)
                        .First(item => item.AttemptStatus == AttemptStatus.Completed);

                    var finishTime = studentScore.FinishTime.HasValue ? studentScore.FinishTime : null;
                    var startTime = studentScore.StartTime.HasValue ? studentScore.StartTime : null;
                    if (finishTime == null || startTime == null)
                    {
                        value[0] = 0;
                    }
                    else
                    {
                        value[0] = finishTime.Value.Subtract(startTime.Value).TotalSeconds;
                    }

                    var score = studentScore.Score.ToPercents();
                    value[1] = (score == null) ? 0 : score.Value;

                    var userScores = this.GetUserTagScores(student);
                    for (int i = 0; i < userScores.Count(); i++)
                    {
                        value[i + 2] = userScores.ElementAt(i).Score;
                    }

                    result.Add(new KeyValuePair<User, double[]>(student, value));
                }
            }
            return result;
        }

        public IEnumerable<KeyValuePair<User, double[]>> GetStudentListForTraining(int topicId, int groupId)
        {
            List<KeyValuePair<User, double[]>> result = new List<KeyValuePair<User, double[]>>();

            var topic = _LmsService.FindService<IDisciplineService>().GetTopic(topicId);
            var curriculumChapterTopic = topic.CurriculumChapterTopics.Where(item => item.TopicRef == topic.Id).First();
            var group = _LmsService.FindService<IUserService>().GetGroup(groupId);
            var students = _LmsService.FindService<IUserService>().GetUsersByGroup(group);

            foreach (User student in students)
            {
                var value = new double[2 + this.GetTags().Count()];
                var studentScore = _LmsService.FindService<ITestingService>()
                    .GetResults(student, curriculumChapterTopic)
                    .First(item => item.AttemptStatus == AttemptStatus.Completed);

                var finishTime = studentScore.FinishTime.HasValue ? studentScore.FinishTime : null;
                var startTime = studentScore.StartTime.HasValue ? studentScore.StartTime : null;
                if (finishTime == null || startTime == null)
                {
                    value[0] = 0;
                }
                else
                {
                    value[0] = finishTime.Value.Subtract(startTime.Value).TotalSeconds;
                }
                
                var score = studentScore.Score.ToPercents();
                value[1] = (score == null) ? 0 : score.Value;

                

                var userScores = this.GetUserTagScores(student);
                for (int i = 0; i < userScores.Count(); i++)
                {
                    value[i + 2] = userScores.ElementAt(i).Score;
                }

                result.Add(new KeyValuePair<User, double[]>(student, value));
            }

            return result;
        }

        #endregion
    }
}