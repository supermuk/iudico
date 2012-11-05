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
            tests = UserManagementTests.Update();
            var user = new User { Username = "name", Email = "mail1@mail.com", Password = "123" };
            this.tests.Storage.CreateUser(user);

            var gg = this.tests.Storage.GetUser("name").Id;
            this.tests.Storage.ActivateUser(gg);

            this.tests.ChangeCurrentUser("name");
            var userId = this.tests.Storage.GetCurrentUser().Id;

            this.tests.Storage.RateTopic(5, 1);
            //Assert.AreEqual(1, this.tests.MockDataContext.Object.UserTopicRatings.Count());

            Assert.IsTrue(this.tests.MockDataContext.Object.UserTopicRatings.Any(r => r.Rating == 5));
            Assert.IsTrue(this.tests.MockDataContext.Object.UserTopicRatings.Any(r => r.TopicId == 1));
            Assert.IsTrue(this.tests.MockDataContext.Object.UserTopicRatings.Any(r => r.UserId == userId));
            //Assert.IsTrue(this.tests.MockDataContext.Object.UserTopicRatings.Any(r => r.Rating == 5 && r.TopicId == 1 && r.UserId == userId));
            this.tests.ChangeCurrentUser("panza");
            this.tests.Storage.DeleteUser(u => u.Username == "name");

        }

        [Test]
        public void UpdateUserAverage()
        {
            tests = UserManagementTests.Update();
            var user = new User { Username = "name1", Email = "mail1@mail.com", Password = "123" };
            this.tests.Storage.CreateUser(user);
            
            var attemptResult = new AttemptResult
                {
                    AttemptId = 1,
                    AttemptStatus = AttemptStatus.Completed,
                    CompletionStatus = CompletionStatus.Completed,
                    FinishTime = DateTime.Now,
                    StartTime = DateTime.Now,
                    Score = new Score(0, 50, 45, 0.9F),
                    SuccessStatus = SuccessStatus.Passed,
                    TopicType = TopicTypeEnum.Test,
                    User = this.tests.Storage.GetUser(user.Username)
                };

            this.tests.Storage.UpdateUserAverage(attemptResult);

            var testUser = this.tests.Storage.GetUser(user.Username);

            Assert.IsTrue(testUser.TestsTotal == 1);
            Assert.IsTrue(testUser.TestsSum == 90);

            this.tests.Storage.DeleteUser(u => u.Username == "name1");
        }
    }
}
