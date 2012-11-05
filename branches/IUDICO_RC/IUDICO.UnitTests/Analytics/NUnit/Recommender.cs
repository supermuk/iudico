using System;
using System.Linq;

using IUDICO.Common.Models.Shared;

using NUnit.Framework;

namespace IUDICO.UnitTests.Analytics.NUnit
{
    [TestFixture]
    public class Recommender
    {
        private AnalyticsTests tests = AnalyticsTests.GetInstance();

        [SetUp]
        public virtual void SetupTest()
        {
            AnalyticsTests.Reset();
            this.tests = AnalyticsTests.GetInstance();
        }

        [Test]
        public void GetTopicScores()
        {
            var topicScores = this.tests.Storage.GetTopicScores();

            foreach (var topicScore in topicScores)
            {
                Assert.IsTrue(topicScore.Value.Count() == this.tests.DataContext.TopicScores.Count(t => t.TopicId == topicScore.Key.Id));
                Assert.IsTrue(topicScore.Value.Sum(t => t.Score) == this.tests.DataContext.TopicScores.Where(t => t.TopicId == topicScore.Key.Id).Sum(t => t.Score));
            }
        }

        [Test]
        public void GetUserScores()
        {
            var userScore = this.tests.Storage.GetUserScores().First();

            Assert.IsTrue(userScore.Value.Count() == this.tests.DataContext.UserScores.Count(t => t.UserId == this.tests.UserId));
            Assert.IsTrue(userScore.Value.Sum(t => t.Score) == this.tests.DataContext.UserScores.Where(t => t.UserId == this.tests.UserId).Sum(t => t.Score));
        }

        [Test]
        public void GetTopicTagScores()
        {
            var topicTagScores = this.tests.Storage.TestGetTopicTagScores(this.tests.DisciplineService.GetTopic(1));
            var results = this.tests.TestingService.GetResults(this.tests.DisciplineService.GetTopic(1));


            Assert.IsTrue(topicTagScores.First().Score == results.Sum(r => r.Score.ToPercents()) / results.Count());
        }

        [Test]
        public void GetUserTagScores()
        {
            var userTagScores = this.tests.Storage.TestGetUserTagScores(this.tests.UserService.GetCurrentUser());
            var results = this.tests.TestingService.GetResults(this.tests.UserService.GetCurrentUser());

            Assert.IsTrue(userTagScores.First().Score == results.Sum(r => r.Score.ToPercents()) / results.Count());
        }

        [Test]
        public void UpdateTopicScores()
        {
            var topicScores = this.tests.Storage.GetTopicScores();
            var ts = topicScores.ToDictionary(t => t.Key.Id, t => t.Value.Sum(v => v.Score));

            this.tests.Storage.UpdateAllTopicScores();

            topicScores = this.tests.Storage.GetTopicScores();

            foreach (var topicScore in topicScores)
            {
                Assert.IsTrue(topicScore.Value.Count() == this.tests.DataContext.TopicScores.Count(t => t.TopicId == topicScore.Key.Id));
                Assert.IsTrue(topicScore.Value.Sum(t => t.Score) == this.tests.DataContext.TopicScores.Where(t => t.TopicId == topicScore.Key.Id).Sum(t => t.Score));
                Assert.IsTrue(topicScore.Value.Sum(t => t.Score) != ts[topicScore.Key.Id]);
            }
        }

        [Test]
        public void UpdateUserScores()
        {
            var userScores = this.tests.Storage.GetUserScores();
            var us = userScores.ToDictionary(t => t.Key.Id, t => t.Value.Sum(v => v.Score));

            this.tests.Storage.UpdateAllUserScores();

            userScores = this.tests.Storage.GetUserScores();

            foreach (var userScore in userScores)
            {
                Assert.IsTrue(userScore.Value.Count() == this.tests.DataContext.UserScores.Count(t => t.UserId == userScore.Key.Id));
                Assert.IsTrue(userScore.Value.Sum(t => t.Score) == this.tests.DataContext.UserScores.Where(t => t.UserId == userScore.Key.Id).Sum(t => t.Score));
                Assert.IsTrue(userScore.Value.Sum(t => t.Score) != us[userScore.Key.Id]);
            }
        }

        [Test]
        public void GetTopicTags()
        {
            var topicTags = this.tests.Storage.TestGetTopicTags(t => t.TopicId == 1);

            Assert.IsTrue(topicTags.Count() == this.tests.DataContext.TopicTags.Count(t => t.TopicId == 1));
        }

        [Test]
        public void CustomDistance()
        {
            var user = new User { Id = this.tests.UserId };
            var topic = new Topic { Id = 1 };

            var distance = this.tests.Storage.TestCustomDistance(user, topic);

            Assert.IsTrue(Math.Abs(distance + 4100) < 0.001);
        }

        [Test]
        public void GetRecommenderTopics()
        {
            
        }
    }
}
