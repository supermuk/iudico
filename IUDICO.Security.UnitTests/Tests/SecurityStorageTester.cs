using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.UnitTests.Tests
{
    class SecurityStorageTester : SecurityTester
    {
        [Test]
        public void CreateUserActivity()
        {
            var userActivity = new UserActivity
            {
                Request = "",
                RequestStartTime = DateTime.Now,
                RequestEndTime = DateTime.Now,
                RequestLength = 0,
                ResponseLength = 1
            };

            SecurityStorage.CreateUserActivity(userActivity);

            Assert.AreEqual(1, SecurityStorage.GetUserActivities().Count());
        }
    }
}
