using System;
using System.Linq;

using IUDICO.Common.Models.Shared;

using NUnit.Framework;

namespace IUDICO.UnitTests.Analytics.NUnit
{
    [TestFixture]
    public class Quality
    {
        private AnalyticsTests tests = AnalyticsTests.GetInstance();

        [Test]
        public void GetTopicTagStatistic()
        {
            var topic = this.tests.DisciplineService.GetTopic(1);
            Assert.AreEqual(this.tests.Storage.GetTopicTagStatistic(topic), 0.5);
        }
        [Test]
        public void Gaussian()
        {
            var topic = this.tests.DisciplineService.GetTopic(1);
            Assert.AreEqual(this.tests.Storage.GaussianDistribution(topic), 0);
        }

        [Test]
        public void GetGroupTopicStatistic()
        {
            /*var topic = this.tests.DisciplineService.GetTopic(1);
            var group = this.tests.
            Assert.AreEqual(this.tests.Storage.GetTopicTagStatistic(topic), 0.5);*/
        }

    }
}
