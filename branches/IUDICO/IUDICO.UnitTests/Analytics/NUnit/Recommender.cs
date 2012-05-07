using System;

using IUDICO.Common.Models.Shared;

using NUnit.Framework;

namespace IUDICO.UnitTests.Analytics.NUnit
{
    [TestFixture]
    public class Recommender
    {
        private AnalyticsTests tests = AnalyticsTests.GetInstance();

        [Test]
        public void DistanceTest()
        {
            var user = new User { Id = this.tests.UserId };
            var topic = new Topic { Id = 1 };

            var distance = this.tests.Storage.TestCustomDistance(user, topic);

            Assert.IsTrue(Math.Abs(distance + 800) < 0.001);
        }
    }
}
