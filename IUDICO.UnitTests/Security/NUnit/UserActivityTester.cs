namespace IUDICO.UnitTests.Security.NUnit
{
    using System;
    using System.Linq;

    using global::NUnit.Framework;

    using IUDICO.Common.Models.Shared;
    using IUDICO.Security.ViewModels.UserActivity;

    [TestFixture]
    internal class UserActivityTester : SecurityTester
    {
        public User CreateUser(string id, string name, bool approved)
        {
            return new User { UserId = id, Name = name, IsApproved = approved };
        }
        public void AddUserStats(OverallViewModel model, User user, int totalRequests,
            int todayRequests, DateTime lastActivity)
        {
            model.AddUserStats(user, totalRequests, todayRequests, lastActivity);
        }

        //author: Фай Роман
        [Test]
        public void GetOverallNumberOfRequestsTest()
        {
            var overallViewModel = new OverallViewModel("1");
            // create users
            var tempUser1 = CreateUser("user1", "User1", true);
            var tempUser2 = CreateUser("user2", "User2", true );

            // must be zero
            Assert.AreEqual(overallViewModel.GetOverallNumberOfRequests(), 0);

            //add their stats
            AddUserStats(overallViewModel, tempUser1, 120, 70, DateTime.Today);
            AddUserStats(overallViewModel, tempUser2, 60, 60, DateTime.Today);

            // check if all requests are summed
            Assert.AreEqual(overallViewModel.GetOverallNumberOfRequests(), 180);
        }

        //author: Фай Роман
        [Test]
        public void GetOverallNumberOfRequestsForTodayTest()
        {
            var overallViewModel = new OverallViewModel();
            //create users
            var tempUser1 = CreateUser("User1", "User1", true );
            var tempUser2 = CreateUser("User2", "User2", true );

            // must be zero
            Assert.AreEqual(overallViewModel.GetOverallNumberOfRequestsForToday(), 0);

            //add their stats
            AddUserStats(overallViewModel, tempUser1, 120, 70, DateTime.Today);
            AddUserStats(overallViewModel, tempUser2, 60, 60, DateTime.Today);

            // check if today requests are summed
            Assert.AreEqual(130, overallViewModel.GetOverallNumberOfRequestsForToday());
        }

        //author: Фай Роман
        [Test]
        public void AddUserStatsTest()
        {
            var overallViewModel = new OverallViewModel("1");
            //create user
            var tempUser = CreateUser("User1", "User1", true );

            // must be zero
            Assert.AreEqual(overallViewModel.GetOverallNumberOfRequests(), 0);

            //add stats
            AddUserStats(overallViewModel, tempUser, 120, 70, DateTime.Today);

            Assert.True(overallViewModel.GetStats().Count(s => s.User.UserId == "User1") == 1);
            Assert.AreEqual(overallViewModel.GetOverallNumberOfRequests(), 120);
        }

        //author: Фай Роман
        [Test]
        public void GetStatsTest()
        {
            var overallViewModel = new OverallViewModel("1");
            //create user
            var tempUser = CreateUser("user1", "User1", true );

            //add stats
            AddUserStats(overallViewModel, tempUser, 120, 70, DateTime.Today);

            Assert.True(overallViewModel.GetStats().Count(s => s.User.UserId == "user1") == 1);
            Assert.True(overallViewModel.GetStats().Count(s => s.User.UserId == "user15") == 0);
        }

        //author: Фай Роман
        [Test]
        public void GetUserActivityTest()
        {
            var overallViewModel = new OverallViewModel("1");
            //create users
            var tempUser1 = CreateUser("user1", "User1", true );
            var tempUser2 = CreateUser("user2", "User2", true );

            //add their stats
            AddUserStats(overallViewModel, tempUser1, 120, 70, DateTime.Today);
            AddUserStats(overallViewModel, tempUser2, 60, 60, DateTime.Today);

            Assert.True(overallViewModel.GetUserActivity("user1").Sum(s => s.TotalNumberOfRequests) == 120);
        }

        //author: Фай Роман
        [Test]
        public void GetUserActivityForTodayTest()
        {
            var overallViewModel = new OverallViewModel("1");
            //create users
            var tempUser1 = CreateUser("user1", "User1", true );
            var tempUser2 = CreateUser("user2", "User2", true );

            //add their stats
            AddUserStats(overallViewModel, tempUser1, 120, 70, DateTime.Today);
            AddUserStats(overallViewModel, tempUser2, 60, 60, DateTime.Today);

            Assert.True(overallViewModel.GetUserActivityForToday("user1").Sum(s => s.TodayNumberOfRequests) == 70);
        }
    }
}