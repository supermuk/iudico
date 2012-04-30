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
            this.playModel = new PlayModel
                {
                   AttemptId = 12345, CurriculumChapterTopicId = 12, TopicType = TopicTypeEnum.TestWithoutCourse 
                };
        }

        [Test]
        public void PlayModelPropertiesTest()
        {
            this.PlayModelTestsSetUp();
            Assert.AreEqual(this.playModel.AttemptId, 12345);
            Assert.AreEqual(this.playModel.CurriculumChapterTopicId, 12);
            Assert.AreEqual(this.playModel.TopicType, TopicTypeEnum.TestWithoutCourse);
        }
    }
}