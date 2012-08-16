// -----------------------------------------------------------------------
// <copyright file="AnswerResultTests.cs" company="">
// 
// </copyright>
// -----------------------------------------------------------------------

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    using IUDICO.Common.Models.Shared.Statistics;

    using global::NUnit.Framework;

    /// <summary>
    /// Bunch of tests for <see cref="AnswerResult"/>.
    /// </summary>
    [TestFixture]
    public class AnswerResultTests
    {
        [Test]
        public void CreateAnswerResultWithParameters()
        {
            const long ActivityAttemptId = 2349;
            const long ActivityPackageId = 83294;
            const string ActivityTitle = "Some Activity";
            long? interactionId = 12382;
            const CompletionStatus CompletionStatus = CompletionStatus.NotAttempted;
            const SuccessStatus SuccessStatus = SuccessStatus.Failed;
            var attemptResult = new AttemptResult();
            const string LearnerResponse = "C";
            const string CorrectResponse = "-328";
            InteractionType? learnerResponseType = Common.Models.Shared.Statistics.InteractionType.Numeric;
            float? scaledScore = 0.58f;
            float? minScore = 0;
            float? maxScore = 50;
            float? rawScore = 29;


            var answerResult = new AnswerResult(
                ActivityAttemptId,
                ActivityPackageId,
                ActivityTitle,
                interactionId,
                CompletionStatus,
                SuccessStatus,
                attemptResult,
                LearnerResponse,
                CorrectResponse,
                learnerResponseType,
                minScore,
                maxScore,
                rawScore,
                scaledScore);

            Assert.AreEqual(ActivityAttemptId, answerResult.ActivityAttemptId);
            Assert.AreEqual(ActivityPackageId, answerResult.ActivityPackageId);
            Assert.AreEqual(ActivityTitle, answerResult.ActivityTitle);
            Assert.AreEqual(interactionId, answerResult.InteractionId);
            Assert.AreEqual(CompletionStatus, answerResult.CompletionStatus);
            Assert.AreEqual(SuccessStatus, answerResult.SuccessStatus);
            Assert.AreSame(attemptResult, answerResult.AttemptResult);
            Assert.AreEqual(LearnerResponse, answerResult.LearnerResponse);
            Assert.AreEqual(CorrectResponse, answerResult.CorrectResponse);
            Assert.AreEqual(learnerResponseType, answerResult.LearnerResponseType);
            Assert.AreEqual(scaledScore, answerResult.ScaledScore);

        }
    }
}
