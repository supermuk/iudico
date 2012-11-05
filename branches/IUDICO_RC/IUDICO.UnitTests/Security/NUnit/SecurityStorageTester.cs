namespace IUDICO.UnitTests.Security.NUnit
{
    using System;
    using System.Linq;

    using global::NUnit.Framework;

    using IUDICO.Common.Models.Shared;

    internal class SecurityStorageTester : SecurityTester
    {
        [Test]
        public void CreateUserActivity()
        {
            var userActivity = new UserActivity
                {
                    Request = string.Empty, 
                    RequestStartTime = DateTime.Now, 
                    RequestEndTime = DateTime.Now, 
                    RequestLength = 0, 
                    ResponseLength = 1
                };

            this.SecurityStorage.CreateUserActivity(userActivity);

            Assert.AreEqual(1, this.SecurityStorage.GetUserActivities().Count());
            Assert.True(this.SecurityStorage.GetUserActivities().Contains(userActivity));
        }
    }
}