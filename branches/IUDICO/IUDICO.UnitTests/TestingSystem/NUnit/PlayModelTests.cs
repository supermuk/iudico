using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.TestingSystem.ViewModels;
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
            playModel = new PlayModel {AttemptId = 12345, CurriculumChapterTopicId = 1, TopicType = TopicTypeEnum.TestWithoutCourse};
        }

        [Test]
        public void PlayModelPropertiesTest()
        {
            PlayModelTestsSetUp();
            Assert.AreEqual(playModel.AttemptId, 12345);
            Assert.AreEqual(playModel.CurriculumChapterTopicId, 1);
            Assert.AreSame(playModel.TopicType, TopicTypeEnum.TestWithoutCourse);
        }
    }
}