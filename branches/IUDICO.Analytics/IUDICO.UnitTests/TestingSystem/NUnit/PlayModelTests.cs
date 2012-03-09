using IUDICO.TestingSystem.Models.VOs;
using NUnit.Framework;

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    [TestFixture]
    internal class PlayModelTests
    {
        private PlayModel playModel;

        [SetUp]
        public void PlayModelTestsSetUp()
        {
            playModel = new PlayModel {AttemptId = 12345, TopicId = 1};
        }

        [Test]
        public void PlayModelPropertiesTest()
        {
            PlayModelTestsSetUp();
            Assert.AreEqual(playModel.AttemptId, 12345);
            Assert.AreEqual(playModel.TopicId, 1);
        }
    }
}