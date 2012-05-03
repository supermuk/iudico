using System.Linq;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    using System;

    using IUDICO.Common.Models.Shared;
    using IUDICO.Common.Models.Shared.DisciplineManagement;
    using IUDICO.Common.Models.Shared.Statistics;

    [TestFixture]
    public class Analytics
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void RateTopic()
        {
            var userId = this.tests.Storage.GetCurrentUser().Id;

            this.tests.Storage.RateTopic(5, 1);
            Assert.IsTrue(this.tests.MockDataContext.Object.UserTopicRatings.Any(r => r.Rating == 5 && r.TopicId == 1 && r.UserId == userId));
        }

        [Test]
        public void UpdateUserAverage()
        {
            var user = new User { Username = "name1", Email = "mail1@mail.com", Password = "123" };
            this.tests.Storage.CreateUser(user);
            
            var attemptResult = new AttemptResult
                {
                    AttemptId = 1,
                    AttemptStatus = AttemptStatus.Completed,
                    CompletionStatus = CompletionStatus.Completed,
                    FinishTime = DateTime.Now,
                    StartTime = DateTime.Now,
                    Score = new Score(0.9F),
                    SuccessStatus = SuccessStatus.Passed,
                    TopicType = TopicTypeEnum.Test,
                    User = this.tests.Storage.GetUser(user.Username)
                };

            this.tests.Storage.UpdateUserAverage(attemptResult);

            var testUser = this.tests.Storage.GetUser(user.Username);

            Assert.IsTrue(testUser.TestsTotal == 1);
            Assert.IsTrue(testUser.TestsSum == 90);
        }
    }
}
