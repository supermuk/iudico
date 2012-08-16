// -----------------------------------------------------------------------
// <copyright file="AttemptResultTests.cs" company="">
// 
// </copyright>
// -----------------------------------------------------------------------

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    using System;

    using IUDICO.Common.Models.Shared;
    using IUDICO.Common.Models.Shared.DisciplineManagement;
    using IUDICO.Common.Models.Shared.Statistics;

    using global::NUnit.Framework;

    /// <summary>
    /// Bunch of test for <see cref="AttemptResult"/>.
    /// </summary>
    [TestFixture]
    public class AttemptResultTests
    {
        [Test]
        public void CreateAttemptResultWithDefaultConstructor()
        {
            var attemptResult = new AttemptResult();

            Assert.AreEqual(0, attemptResult.AttemptId);
            Assert.AreEqual(AttemptStatus.Active, attemptResult.AttemptStatus);
            Assert.AreEqual(CompletionStatus.Unknown, attemptResult.CompletionStatus);
            Assert.AreEqual(null, attemptResult.CurriculumChapterTopic);
            Assert.AreEqual(null, attemptResult.FinishTime);
            Assert.AreEqual(null, attemptResult.Score);
            Assert.AreEqual(null, attemptResult.StartTime);
            Assert.AreEqual(SuccessStatus.Unknown, attemptResult.SuccessStatus);
            Assert.AreEqual(0, (int)attemptResult.TopicType);
            Assert.AreEqual(null, attemptResult.User);
        }

        [Test]
        public void CreateAttemptResultWithParameters()
        {
            const long AttemptId = 12312;
            var user = new User();
            var curriculumChapterTopic = new CurriculumChapterTopic();
            const TopicTypeEnum TopicTypeEnum = TopicTypeEnum.TestWithoutCourse;
            const CompletionStatus CompletionStatus = CompletionStatus.Incomplete;
            const AttemptStatus AttemptStatus = AttemptStatus.Completed;
            const SuccessStatus SuccessStatus = SuccessStatus.Passed;
            DateTime? startTime = new DateTime(32478932);
            DateTime? finishTime = new DateTime(189041324);
            float? score = 0.22f;
            float? minScore = 0;
            float? maxScore = 50;
            float? rawScore = 11;

            var attemptResult = new AttemptResult(
                AttemptId,
                user,
                curriculumChapterTopic,
                TopicTypeEnum,
                CompletionStatus,
                AttemptStatus,
                SuccessStatus,
                startTime,
                finishTime,
                minScore,
                maxScore,
                rawScore,
                score);

            Assert.AreEqual(AttemptId, attemptResult.AttemptId);
            Assert.AreEqual(user, attemptResult.User);
            Assert.AreEqual(curriculumChapterTopic, attemptResult.CurriculumChapterTopic);
            Assert.AreEqual(TopicTypeEnum, attemptResult.TopicType);
            Assert.AreEqual(CompletionStatus, attemptResult.CompletionStatus);
            Assert.AreEqual(AttemptStatus, attemptResult.AttemptStatus);
            Assert.AreEqual(SuccessStatus, attemptResult.SuccessStatus);
            Assert.AreEqual(startTime, attemptResult.StartTime);
            Assert.AreEqual(finishTime, attemptResult.FinishTime);
            Assert.AreEqual(score, attemptResult.Score.ScaledScore);
        }
    }
}
