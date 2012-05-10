using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.TestingSystem.ViewModels;

using NUnit.Framework;

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    [TestFixture]
    internal class PlayModelTests
    {
        [Test]
        [TestCase(0, 0, TopicTypeEnum.Test, null)]
        [TestCase(1234, 12, TopicTypeEnum.Theory, "")]
        [TestCase(12, 13123, TopicTypeEnum.TestWithoutCourse, "dasfo asidf  adsf")]
        [TestCase(12, 23, TopicTypeEnum.Test, "Hello 123")]
        public void PlayModelPropertiesTest(int attemptId, int curriculumChapterTopicId, TopicTypeEnum topicType, string topicName)
        {
            var playModel = new PlayModel
                {
                    AttemptId = attemptId,
                    CurriculumChapterTopicId = curriculumChapterTopicId,
                    TopicType = topicType,
                    TopicName = topicName
                };
            
            Assert.AreEqual(attemptId, playModel.AttemptId);
            Assert.AreEqual(curriculumChapterTopicId, playModel.CurriculumChapterTopicId);
            Assert.AreEqual(topicType, playModel.TopicType);
            Assert.AreEqual(topicName, playModel.TopicName);
        }
    }
}