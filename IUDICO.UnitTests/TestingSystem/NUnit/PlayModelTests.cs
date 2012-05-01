using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.TestingSystem.ViewModels;

using NUnit.Framework;

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    [TestFixture]
    internal class PlayModelTests
    {
        [Test]
        [TestCase(0, 0, TopicTypeEnum.Test)]
        [TestCase(1234, 12, TopicTypeEnum.Theory)]
        [TestCase(12, 13123, TopicTypeEnum.TestWithoutCourse)]
        [TestCase(12, 23, TopicTypeEnum.Test)]
        public void PlayModelPropertiesTest(int attemptId, int curriculumChapterTopicId, TopicTypeEnum topicType)
        {
            var playModel = new PlayModel
                { AttemptId = attemptId, CurriculumChapterTopicId = curriculumChapterTopicId, TopicType = topicType };
            
            Assert.AreEqual(attemptId, playModel.AttemptId);
            Assert.AreEqual(curriculumChapterTopicId, playModel.CurriculumChapterTopicId);
            Assert.AreEqual(topicType, playModel.TopicType);
        }
    }
}