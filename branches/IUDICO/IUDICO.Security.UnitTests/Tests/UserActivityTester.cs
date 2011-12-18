using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.Common.Models.Shared;
using IUDICO.Security.ViewModels.UserActivity;
using NUnit.Framework;

namespace IUDICO.Security.UnitTests.Tests
{
    [TestFixture]
    class UserActivityTester : SecurityTester
    {
        [Test]
        public void GetOverallNumberOfRequestsTest()
        {
            var overallViewModel = new OverallViewModel("1");

            var tempUser1 = new User {UserId = "user1",Name = "User1", IsApproved = true};

            var tempUser2 = new User {UserId = "user2",Name = "User2", IsApproved = true };

            //Must be zero;
            Assert.AreEqual(overallViewModel.GetOverallNumberOfRequests(), 0);

            overallViewModel.AddUserStats(tempUser1, 120, 70, DateTime.Today);

            overallViewModel.AddUserStats(tempUser2, 60, 60, DateTime.Today);

            //Check if request of all users are summed;
            Assert.AreEqual(overallViewModel.GetOverallNumberOfRequests(), 180);
        }


        [Test]
        public void GetOverallNumberOfRequestsForTodayTest()
        {
            var overallViewModel = new OverallViewModel("1");

            var tempUser1 = new User { UserId = "User1", Name = "User1", IsApproved = true };

            var tempUser2 = new User { UserId = "User2", Name = "User2", IsApproved = true };

            Assert.AreEqual(overallViewModel.GetOverallNumberOfRequestsForToday(),0);

            overallViewModel.AddUserStats(tempUser1, 120, 70, DateTime.Today);

            overallViewModel.AddUserStats(tempUser2, 60, 60, DateTime.Today);

            Assert.AreEqual(130,overallViewModel.GetOverallNumberOfRequestsForToday());
        }

        [Test]
        public void AddUserStatsTest()
        {
            var overallViewModel = new OverallViewModel("1");

            var tempUser = new User {UserId = "User1", Name = "User1", IsApproved = true};

            Assert.AreEqual(overallViewModel.GetOverallNumberOfRequests(), 0);

            overallViewModel.AddUserStats(tempUser, 120, 70, DateTime.Today);

            Assert.True(overallViewModel.GetStats().Count(s => s.User.UserId == "User1") == 1);

            Assert.AreEqual(overallViewModel.GetOverallNumberOfRequests(),120);
        }

        [Test]
        public void GetStatsTest()
        {
            var overallViewModel = new OverallViewModel("1");

            var tempUser = new User { UserId = "user1", Name = "User1", IsApproved = true };

            overallViewModel.AddUserStats(tempUser, 120, 70, DateTime.Today);

            Assert.True(overallViewModel.GetStats().Count(s => s.User.UserId == "user1")==1);

            Assert.True(overallViewModel.GetStats().Count(s => s.User.UserId == "user15") == 0);
        }
    }
}
