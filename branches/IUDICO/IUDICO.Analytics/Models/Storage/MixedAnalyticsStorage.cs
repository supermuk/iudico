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
        private readonly ILmsService lmsService;
        private DBDataContext db;

        protected DBDataContext GetDbDataContext()
        {
            return new DBDataContext();
        }

        public MixedAnalyticsStorage(ILmsService lmsService)
        {
            this.lmsService = lmsService;
            this.RefreshState();
        }
        
        public void RefreshState()
        {
            this.db = new DBDataContext();
        }
        
        #region Analytics methods

        public IEnumerable<ForecastingTree> GetAllForecastingTrees()
        { 
            return this.GetDbDataContext().ForecastingTrees.Where(x => x.IsDeleted == false);
        }

        public IEnumerable<ForecastingTree> GetForecastingTrees(Guid userRef)
        {
            return this.GetDbDataContext().ForecastingTrees.Where(x => x.UserRef == userRef && x.IsDeleted == false);
        }

        #region Recommender System

        public Dictionary<Topic, IEnumerable<TopicScore>> GetTopicScores()
        {
            using (var d = this.GetDbDataContext())
            {
                var topics = this.GetTopics().ToDictionary(t => t.Id, t => t);

                var topicScores = (from t in topics.Values
                                   join ts in this.db.TopicScores on t.Id equals ts.TopicId into tsj
                                   from j in tsj.DefaultIfEmpty()
                                   group j by t.Id
                                   into grouped 
                                   select new { Topic = grouped.Key, Values = grouped }).OrderBy(g => g.Topic).ToDictionary(g => topics[g.Topic], g => g.Values.Where(f => f != null).ToList().AsEnumerable());
            
                return topicScores;
            }
        }

        public Dictionary<User, IEnumerable<UserScore>> GetUserScores()
        {
            using (var d = this.GetDbDataContext())
            {
                var users = this.GetUsers().ToDictionary(u => u.Id, u => u);

                var userScores = (from u in users.Values
                                  join us in this.db.UserScores on u.Id equals us.UserId into usj
                                  from j in usj.DefaultIfEmpty()
                                  group j by u.Id into grouped
                                  select new { User = grouped.Key, Values = grouped })
                                  .OrderBy(g => g.User).ToDictionary(g => users[g.User], g => g.Values.Where(f => f != null).ToList().AsEnumerable());

                return userScores;
            }
        }

        public void UpdateUserScores(Guid id)
        {
            using (var d = this.GetDbDataContext())
            {
                var user = this.GetUser(id);
                var userTagScores = this.GetUserTagScores(user);

                d.UserScores.DeleteAllOnSubmit(d.UserScores.Where(us => us.UserId == id));
                d.UserScores.InsertAllOnSubmit(userTagScores);

                d.SubmitChanges();
            }
        }

        protected IEnumerable<TopicTag> GetTopicTags(Func<TopicTag, bool> predicate)
        {
            return this.GetDbDataContext().TopicTags.Where(predicate).Select(tt => new { Tag = tt.Tag, TopicTag = tt }).AsEnumerable().Select(a => a.TopicTag);
        }

        protected IEnumerable<UserScore> GetUserTagScores(User user)
        {
            var tags = new Dictionary<int, float>();
            var count = new Dictionary<int, int>();

            var attempts =
                this.GetResults(user).GroupBy(
                    a => new { UserId = a.User.Id, TopicId = a.CurriculumChapterTopic.TopicRef }).Select(g => g.First())
                    .ToDictionary(a => a.CurriculumChapterTopic.TopicRef, a => a);
            
            var topicTags =
                this.GetTopicTags(f => attempts.Keys.Contains(f.TopicId)).GroupBy(t => t.TopicId).ToDictionary(
                    g => g.Key, g => g.Select(t => t.TagId));

            if (!attempts.Any())
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
            using (var d = this.GetDbDataContext())
            {
                var topic = this.GetTopic(id);
                var topicTagScores = this.GetTopicTagScores(topic);

                d.TopicScores.DeleteAllOnSubmit(d.TopicScores.Where(us => us.TopicId == id));
                d.TopicScores.InsertAllOnSubmit(topicTagScores);

                d.SubmitChanges();
            }
        }

        protected IEnumerable<TopicScore> GetTopicTagScores(Topic topic)
        {
            var topicTags = this.GetTopicTags(t => t.TopicId == topic.Id);
            var attempts = this.GetResults(topic).GroupBy(a => new { UserId = a.User.Id, TopicId = a.CurriculumChapterTopic.TopicRef }).Select(g => g.First());
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

            var average = (float)(total / count);

            var tags = topicTags.Select(t => new TopicScore { TagId = t.TagId, TopicId = t.TopicId, Score = average });

            return tags;
        }

        protected double PearsonDistance(User user, Topic topic)
        {
            var userTagScores = this.db.UserScores.Where(s => s.UserId == user.Id).ToDictionary(s => s.TagId, s => 100 - s.Score);
            var topicTagScores = this.db.TopicScores.Where(s => s.TopicId == topic.Id).ToDictionary(s => s.TagId, s => s.Score);

            var commonTags = userTagScores.Select(t => t.Key).Intersect(topicTagScores.Select(t => t.Key));
            var n = commonTags.Count();

            var userCommonScores = userTagScores.Where(t => commonTags.Contains(t.Key)).Select(t => 1.0 * t.Value);
            var topicCommonScores = topicTagScores.Where(t => commonTags.Contains(t.Key)).Select(t => 1.0 * t.Value);

            var sum1 = userCommonScores.Sum();
            var sum2 = topicCommonScores.Sum();

            var sum1Sq = userCommonScores.Sum(x => x * x);
            var sum2Sq = topicCommonScores.Sum(x => x * x);

            var sum = commonTags.Sum(tag => (userTagScores[tag] * topicTagScores[tag]));

            var num = sum - (sum1 * sum2 / n);
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
            var results = this.GetResults(topic);

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
            var ratingMax = 2 * ((n + 1) / 2) * (n / 2);
            var ratingNormalized = ratingDifference / ratingMax;

            var diffResults = groupResults.ToDictionary(a => a.User, a => a.Score);
            var diffRatings = groupRatings.ToDictionary(a => a.User, a => a.Score);

            var scoreDifference = 1.0 * usersParticipated.Sum(u => Math.Abs(diffResults[u] - diffRatings[u]));
            var scoreMax = n * 100;
            var scoreNormalized = scoreDifference / scoreMax;

            return new GroupTopicStat(ratingNormalized, scoreNormalized);
        }
        public double GetTopicStatistic(Topic topic)
        {
            var results = this.GetResults(topic);
            var users = results.Select(r => r.User).ToList();

            // Масив типу : юзер - його теги і їх значення 
            var usersWithTags = new List<UserTags>();
            foreach (var user in users)
            {
                var temp = new UserTags();
                temp.Id = user.Id;
                foreach (var tag in this.GetUserTagScores(user))
                {
                    temp.Tags.Add(tag.TagId, tag.Score);
                }
                usersWithTags.Add(temp);
            }

            // Масив типу : тег - ( значення тегу певного юзера, результат тесту того ж юзера) 
            Dictionary<int, List<KeyValuePair<double, double>>> tagValueScores = new Dictionary<int, List<KeyValuePair<double, double>>>();
            foreach (var userWithTag in usersWithTags)
            {
                foreach (var tag in userWithTag.Tags)
                {
                    var item = new KeyValuePair<double, double>(userWithTag.Tags[tag.Key], results.Where(x => x.User.Id == userWithTag.Id).First().Score.ScaledScore.Value);
                    if (!tagValueScores.Keys.Contains(tag.Key))
                    {
                        tagValueScores.Add(tag.Key, new List<KeyValuePair<double, double>>() { item });
                    }
                    else
                    {
                        tagValueScores[tag.Key].Add(item);
                    }
                }
            }

            int general_amount = 0;
            int success_amount = 0;

            foreach (var tagValueScore in tagValueScores)
            {
                tagValueScore.Value.Sort();
                var prev = tagValueScore.Value.First();

                foreach (var valueScore in tagValueScore.Value)
                {
                    if (Math.Abs(valueScore.Key - prev.Key) < 0.2)
                    {
                        if (Math.Abs(valueScore.Value - prev.Value) < 25)
                        {
                            success_amount++;
                        }
                    }
                    prev = valueScore;
                }

                general_amount++;
            }




            return success_amount / general_amount;
        }
        public double GaussianDistribution(Topic topic)
        {
            var general_result = 0.0;
            List<double> results = this.GetResults(topic).Select(x=>(double)x.Score.ScaledScore).ToList();

            var u = 0.0;
            foreach (var groupResult in results)
            {
                u += groupResult;
            }
            u /= results.Count;
            var variance = 0.0;
            foreach (var groupResult in results)
            {
                variance += Math.Pow(groupResult - u, 2);
            }
            variance /= results.Count;

            var s = 0;
            foreach (var groupResult in results)
            {
                double a = 1 / (Math.Sqrt(2 * Math.PI) * Math.Sqrt(variance));
                double c = -Math.Pow(groupResult - Math.Sqrt(variance), 2);

                double b = Math.Pow(Math.E, c / (2 * variance));
                if (a * b < 0.0005)
                {
                    s++;
                }
            }

            return 1 - (double)s / results.Count;
        }
        public class UserTags
        {
            public Guid Id { get; set; }
            public Dictionary<int, double> Tags { get; set; }
        }
        #endregion

        #endregion

        #region Helper Methods

        public IEnumerable<Tag> GetTags()
        {
            return this.db.Tags;
        }

        public ViewTagDetails GetTagDetails(int id)
        {
            var tag = this.db.Tags.SingleOrDefault(f => f.Id == id);
            var topicIds = this.db.TopicTags.Where(t => t.TagId == tag.Id).Select(t => t.TopicId);
            var topics = GetTopics(topicIds);

            return new ViewTagDetails(tag, topics);
        }

        public void CreateTag(Tag tag)
        {
            this.db.Tags.InsertOnSubmit(tag);
            this.db.SubmitChanges();
        }

        public Tag GetTag(int id)
        {
            return this.db.Tags.SingleOrDefault(f => f.Id == id);
        }

        public void EditTag(int id, Tag tag)
        {
            var oldFeature = this.GetTag(id);

            oldFeature.Name = tag.Name;

            this.db.SubmitChanges();
        }

        public void DeleteTag(int id)
        {
            var containsFeatures = this.db.TopicTags.Any(tf => tf.TagId == id);

            if (containsFeatures)
            {
                throw new Exception("Can't delete tag, which has topics assigned to it");
            }

            this.db.Tags.DeleteOnSubmit(this.GetTag(id));
        }

        public void EditTags(int id, IEnumerable<int> topics)
        {
            var allTopics = this.db.TopicTags.Where(tf => tf.TagId == id).AsEnumerable();
            var deletedTopics = allTopics.Where(tf => !topics.Contains(tf.TopicId));
            var addTopics = topics.Where(i => !allTopics.Select(t => t.TopicId).Contains(i));

            this.db.TopicTags.DeleteAllOnSubmit(deletedTopics);

            var topticTags = addTopics.Select(topic => new TopicTag { TagId = id, TopicId = topic }).ToList();

            this.db.TopicTags.InsertAllOnSubmit(topticTags);
            this.db.SubmitChanges();
        }

        public ViewTagDetails GetTagDetailsWithTopics(int id)
        {
            var feature = this.GetTagDetails(id);
            var featureTopicsIds = feature.Topics.Select(t => t.Id);
            feature.AvailableTopics = this.GetTopics().Where(t => !featureTopicsIds.Contains(t.Id));

            return feature;
        }

        #endregion

        #region Other Service Methods

        protected IEnumerable<User> GetUsers()
        {
            return this.lmsService.FindService<IUserService>().GetUsers();
        }

        protected User GetUser(Guid id)
        {
            return this.lmsService.FindService<IUserService>().GetUsers(u => u.Id == id).SingleOrDefault();
        }

        protected Topic GetTopic(int id)
        {
            return this.lmsService.FindService<IDisciplineService>().GetTopic(id);
        }

        protected IEnumerable<Topic> GetTopics()
        {
            return this.lmsService.FindService<IDisciplineService>().GetTopics();
        }

        protected IEnumerable<Topic> GetTopics(IEnumerable<int> ids)
        {
            return this.lmsService.FindService<IDisciplineService>().GetTopics(ids);
        }

        protected IEnumerable<TopicDescription> GetTopicsAvailableForUser(User user)
        {
            return this.lmsService.FindService<ICurriculumService>().GetTopicDescriptions(user);
        }

        protected IEnumerable<AttemptResult> GetResults(User user)
        {
            return this.lmsService.FindService<ITestingService>().GetResults(user);
        }

        protected IEnumerable<AttemptResult> GetResults(Topic topic)
        {
            return this.lmsService.FindService<ITestingService>().GetResults(topic);
        }

        #endregion

        #region Anomaly detection

        public IEnumerable<Topic> AvailebleTopics()
        {
            var teacherUser = this.lmsService.FindService<IUserService>().GetCurrentUser();
            var listOfAvailableTopics = new List<Topic>();

            var ownedDisciplines = this.lmsService.FindService<IDisciplineService>()
                .GetDisciplines(item => item.Owner == teacherUser.Username);

            foreach (var discipline in ownedDisciplines)
            {
                listOfAvailableTopics.AddRange(this.lmsService.FindService<IDisciplineService>().GetTopicsByDisciplineId(discipline.Id)
                    .Where(item => item.TestTopicTypeRef != null));
            }

            return listOfAvailableTopics;
        }

        public IEnumerable<Group> AvailebleGroups(int topicId)
        {
            var listOfAvailableGroups = new List<Group>();
           
            var topic = this.lmsService.FindService<IDisciplineService>().GetTopic(topicId);
            var groups = this.lmsService.FindService<IUserService>().GetGroups();

            foreach (var group in groups)
            {
                if (this.lmsService.FindService<IDisciplineService>().GetTopicsByGroupId(group.Id).Contains(topic))
                {
                    listOfAvailableGroups.Add(group);
                }
            }

            return listOfAvailableGroups;
        }

        public IEnumerable<KeyValuePair<User, double[]>> GetAllStudentListForTraining(int topicId)
        {

            var result = new List<KeyValuePair<User, double[]>>();
            var listOfAvailableGroups = new List<Group>();

            var topic = this.lmsService.FindService<IDisciplineService>().GetTopic(topicId);
            var groups = this.lmsService.FindService<IUserService>().GetGroups();
            var curriculumChapterTopic = topic.CurriculumChapterTopics.First(item => item.TopicRef == topic.Id);

            foreach (Group group in groups)
            {
                if (this.lmsService.FindService<IDisciplineService>()
                    .GetTopicsByGroupId(group.Id).Contains(topic))
                {
                    listOfAvailableGroups.Add(group);
                }
            }

            foreach (var group in listOfAvailableGroups)
            {
                var students = this.lmsService.FindService<IUserService>().GetUsersByGroup(group);

                foreach (var student in students)
                {
                    var value = new double[2 + this.GetTags().Count()];

                    var studentScore = this.lmsService.FindService<ITestingService>()
                        .GetResults(student, curriculumChapterTopic)
                        .First(item => item.AttemptStatus == AttemptStatus.Completed);

                    var score = studentScore.Score.ToPercents();
                    value[0] = (score == null) ? 0 : score.Value;

                    var finishTime = studentScore.FinishTime.HasValue ? studentScore.FinishTime : null;
                    var startTime = studentScore.StartTime.HasValue ? studentScore.StartTime : null;
                    if (finishTime == null || startTime == null)
                    {
                        value[1] = 0;
                    }
                    else
                    {
                        value[1] = finishTime.Value.Subtract(startTime.Value).TotalSeconds;
                    }

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
            var result = new List<KeyValuePair<User, double[]>>();

            var topic = this.lmsService.FindService<IDisciplineService>().GetTopic(topicId);
            var curriculumChapterTopic = topic.CurriculumChapterTopics.First(item => item.TopicRef == topic.Id);
            var group = this.lmsService.FindService<IUserService>().GetGroup(groupId);
            var students = this.lmsService.FindService<IUserService>().GetUsersByGroup(group);

            foreach (User student in students)
            {
                var value = new double[2 + this.GetTags().Count()];
                var studentScore = this.lmsService.FindService<ITestingService>()
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