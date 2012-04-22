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
            //User teacherUser = _LmsService.FindService<IUserService>().GetCurrentUser();
            //return _LmsService.FindService<ICurriculumService>().GetTopicsOwnedByUser(teacherUser);
            Topic t = new Topic();
            t.Id = 99999;
            t.Name = "Test topic";
            List<Topic> res = new List<Topic>();
            res.Add(t);
            return res;
        }

        public IEnumerable<Group> AvailebleGroups(int topicId)
        {
            //User teacherUser = _LmsService.FindService<IUserService>().GetCurrentUser();
            //return _LmsService.FindService<ICurriculumService>().GetTopicsOwnedByUser(teacherUser);
            Group i41 = new Group();
            i41.Id = 1;
            i41.Name = "PMI 41";
            Group i42 = new Group();
            i42.Id = 2;
            i42.Name = "PMI 42";
            Group i43 = new Group();
            i43.Id = 3;
            i43.Name = "PMI 43";
            List<Group> res = new List<Group>();
            res.Add(i41);
            res.Add(i42);
            res.Add(i43);
            return res;
        }

        public IEnumerable<KeyValuePair<User,double[]>> GetAllStudentListForTraining(int topicId)
        {
            //var listOfGroups = _LmsService.FindService<ICurriculumService>().GetGroupsAssignedToTopic(topicId);
            //IEnumerable<User> studentsFromSelectedGroups = new List<User>();
            //foreach (var group in listOfGroups)
            //{
            //    studentsFromSelectedGroups = studentsFromSelectedGroups.Union(_LmsService.FindService<IUserService>().GetUsersByGroup(group));
            //}
            List<KeyValuePair<User, double[]>> result = new List<KeyValuePair<User, double[]>>();
            return result.Concat(GetStudentListForPMI41()).Concat(GetStudentListForPMI42()).Concat(GetStudentListForPMI43());
            //foreach (var student in studentsFromSelectedGroups)
                //{
                //    AttemptResult att = new AttemptResult(1,student,null,new CompletionStatus(),new AttemptStatus(),new SuccessStatus(),null,null,rnd.Next(1,20)/100);
                //    result.Add(new KeyValuePair<User,AttemptResult>(student,att));
                //}
            //return result;
        }

        public IEnumerable<KeyValuePair<User, double[]>> GetStudentListForTraining(int topicId, int groupId)
        {
            //var listOfGroups = _LmsService.FindService<ICurriculumService>().GetGroupsAssignedToTopic(topicId);
            //IEnumerable<User> studentsFromSelectedGroups = new List<User>();
            //foreach (var group in listOfGroups)
            //{
            //    studentsFromSelectedGroups = studentsFromSelectedGroups.Union(_LmsService.FindService<IUserService>().GetUsersByGroup(group));
            //}
            List<KeyValuePair<User, double[]>> result = new List<KeyValuePair<User, double[]>>();
            switch (groupId)
            {
                case 1:
                    return result.Concat(GetStudentListForPMI41());
                    break;
                case 2:
                    return result.Concat(GetStudentListForPMI42());
                    break;
                case 3:
                    return result.Concat(GetStudentListForPMI43());
                    break;
                default:
                    throw new Exception();
            }
            //foreach (var student in studentsFromSelectedGroups)
            //{
            //    AttemptResult att = new AttemptResult(1,student,null,new CompletionStatus(),new AttemptStatus(),new SuccessStatus(),null,null,rnd.Next(1,20)/100);
            //    result.Add(new KeyValuePair<User,AttemptResult>(student,att));
            //}
            return result;
        }

        public IEnumerable<KeyValuePair<User, double[]>> GetStudentListForPMI43()
        {
            List<KeyValuePair<User, double[]>> result = new List<KeyValuePair<User, double[]>>();
            Random rnd = new Random(System.Environment.TickCount);

            #region PMI-43
            User user = new User();
            user.OpenId = "1";
            int score = 6;
            user.Name = "Остап Андрусів(08i301)";
            double[] coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "2";
            score = 9;
            user.Name = "Назар Врублевський(08i302)";
            coef = new double[] { 450, 3 - rnd.Next(10, 100) / 100.0, 2 - rnd.Next(10, 100) / 100.0, 4 - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "3";
            score = 4;
            user.Name = "Адмайкін Максим(08i303)";
            coef = new double[] { 350, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "4";
            score = 6;
            user.Name = "Михайло Тис(08i304)";
            coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "5";
            score = 5;
            user.Name = "Оля Іванків(08i305)";
            coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "6";
            score = 7;
            user.Name = "Юрій Ожирко(08i306)";
            coef = new double[] { 450, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "7";
            score = 5;
            user.Name = "Тарас Бехта(08i307)";
            coef = new double[] { 300, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "8";
            score = 9;
            user.Name = "Василь Бодак(08i308)";
            coef = new double[] { 100, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "9";
            score = 7;
            user.Name = "Василь Багряк(08i309)";
            coef = new double[] { 350, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "10";
            score = 9;
            user.Name = "Максим Гула(08i310)";
            coef = new double[] { 150, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "11";
            score = 6;
            user.Name = "Назар Качмарик(08i311)";
            coef = new double[] { 350, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "12";
            score = 7;
            user.Name = "Мирослав Голуб(08i312)";
            coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "13";
            score = 9;
            user.Name = "Павло Мартиник(08i313)";
            coef = new double[] { 550, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "14";
            score = 6;
            user.Name = "08i314 08i314(08i314)";
            coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "15";
            score = 7;
            user.Name = "08i315 Горячий(08i315)";
            coef = new double[] { 450, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef)); user = new User();

            user = new User();
            user.OpenId = "16";
            score = 4;
            user.Name = "Данило Савчак(08i316)";
            coef = new double[] { 250, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "17";
            score = 8;
            user.Name = "Ярослав Пиріг(08i317)";
            coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));
            user = new User();

            user = new User();
            user.OpenId = "18";
            score = 7;
            user.Name = "Ірина Харів(08i318)";
            coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "19";
            score = 7;
            user.Name = "Ярослав Мота(08i319)";
            coef = new double[] { 350, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "20";
            score = 7;
            user.Name = "08i320 Федорович(08i320)";
            coef = new double[] { 450, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));
            #endregion

            return result;
        }

        public IEnumerable<KeyValuePair<User, double[]>> GetStudentListForPMI42()
        {
            List<KeyValuePair<User, double[]>> result = new List<KeyValuePair<User, double[]>>();
            Random rnd = new Random(System.Environment.TickCount);

            #region PMI-42
            User user = new User();
            user.OpenId = "21";
            int score = 7;
            user.Name = "Олег Булатовський(08i201)";
            double[] coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "22";
            score = 8;
            user.Name = "Василь Ванівський(08i202)";
            coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "23";
            score = 4;
            user.Name = "Boзняк Максим(08i203)";
            coef = new double[] { 300, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "24";
            score = 6;
            user.Name = "Юра Гой(08i204)";
            coef = new double[] { 350, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "25";
            score = 5;
            user.Name = "Остап Демків(08i205)";
            coef = new double[] { 300, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "26";
            score = 7;
            user.Name = "Юра Дерев`янко(08i206)";
            coef = new double[] { 300, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "27";
            score = 4;
            user.Name = "Роман Дроботiй(08i207)";
            coef = new double[] { 200, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "28";
            score = 9;
            user.Name = "Дубик Петро(08i208)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "29";
            score = 7;
            user.Name = "Віталій Засадний(08i209)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "30";
            score = 5;
            user.Name = "08i210 Андрусишин(08i210)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "31";
            score = 5;
            user.Name = "Тарас Кміть(08i211)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "32";
            score = 6;
            user.Name = "Роман Коваль(08i212)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "33";
            score = 8;
            user.Name = "Маріана Кушла(08i213)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "34";
            score = 7;
            user.Name = "Юрій Ладанівський(08i214)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "35";
            score = 6;
            user.Name = "Leskiv Andriy(08i215)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "36";
            score = 4;
            user.Name = "Юра Лучків(08i216)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "37";
            score = 6;
            user.Name = "Mamchur Andriy(08i217)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "38";
            score = 5;
            user.Name = "Христина Мандибур(08i218)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "39";
            score = 7;
            user.Name = "Андрiй Протасов(08i219)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "40";
            score = 7;
            user.Name = "08i220 08i220(08i220)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "41";
            score = 7;
            user.Name = "Стадник Роман(08i221)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "42";
            score = 5;
            user.Name = "Андрій Столбовой(08i222)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "43";
            score = 5;
            user.Name = "Ігор Сторянський(08i223)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "44";
            score = 6;
            user.Name = "Фатич Михайло(08i224)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            #endregion

            return result;
        }

        public IEnumerable<KeyValuePair<User, double[]>> GetStudentListForPMI41()
        {
            List<KeyValuePair<User, double[]>> result = new List<KeyValuePair<User, double[]>>();
            Random rnd = new Random(System.Environment.TickCount);

            #region PMI-41
            User user = new User();
            user.OpenId = "45";
            int score = 8;
            user.Name = "Роман Баїк(08i101)";
            double[] coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "46";
            score = 8;
            user.Name = "Катерина Бугай(08i102)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "47";
            score = 7;
            user.Name = "Олексій Гелей(08i103)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "48";
            score = 9;
            user.Name = "Карпунь Богдан(08i104)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "49";
            score = 9;
            user.Name = "08i105 08i105(08i105)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "50";
            score = 7;
            user.Name = "Олександр Козачук(08i106)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "51";
            score = 5;
            user.Name = "Аня Кітчак(08i107)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "52";
            score = 9;
            user.Name = "Кравець Роман(08i108)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "53";
            score = 8;
            user.Name = "Андрій Крупич(08i109)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "54";
            score = 9;
            user.Name = "Літинський Ростислав(08i110)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "55";
            score = 6;
            user.Name = "Христина Макар(08i111)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "56";
            score = 7;
            user.Name = "Юрко Тимчук(08i112)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "57";
            score = 9;
            user.Name = "Oleg Papirnyk(08i113)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "58";
            score = 8;
            user.Name = "Віталій Нобіс(08i114)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "59";
            score = 7;
            user.Name = "Taras Pelenyo(08i115)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "60";
            score = 5;
            user.Name = "08i116 08i116(08i116)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "61";
            score = 8;
            user.Name = "Стадник Богдан(08i117)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "62";
            score = 5;
            user.Name = "Andriy Pachva(08i118)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "63";
            score = 8;
            user.Name = "Фай Роман(08i119)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "64";
            score = 7;
            user.Name = "Мар'яна Хлєбик(08i120)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "65";
            score = 7;
            user.Name = "08i121 08i121(08i121)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "66";
            score = 7;
            user.Name = "Руслан Івать(08i122)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "67";
            score = 6;
            user.Name = "Андрій Сташко(08i123)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "68";
            score = 7;
            user.Name = "08i124 08i124(08i124)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "69";
            score = 8;
            user.Name = "Ігор Михалевич(08i125)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));
            #endregion

            return result;
        }

        #endregion
    }
}