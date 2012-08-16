using System;
using IUDICO.Common.Models.Shared.DisciplineManagement;

namespace IUDICO.Common.Models.Shared.Statistics
{
    /// <summary>
    /// Represents result of one user's attempt on one topic(course).
    /// Topic(course) are defined by pair: <see cref="CurriculumChapterTopic"/> and <see cref="TopicType"/>.
    /// </summary>
    public class AttemptResult
    {
        #region Public Properties

        /// <summary>
        /// Identifier of attempt, this result is related to.
        /// </summary>
        public long AttemptId { get; set; }

        /// <summary>
        /// User, attempt result is for.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// CurriculumChapterTopic, attempt result is for.
        /// </summary>
        public CurriculumChapterTopic CurriculumChapterTopic { get; set; }

        /// <summary>
        /// Type of referenced topic.
        /// </summary>
        public TopicTypeEnum TopicType { get; set; }

        /// <summary>
        /// Completion status - SCORM related.
        /// Indicates whether attempt is completed or incompleted.
        /// </summary>
        public CompletionStatus CompletionStatus { get; set; }

        /// <summary>
        /// Attempt status - represents in general what is status of attempt.
        /// </summary>
        public AttemptStatus AttemptStatus { get; set; }

        /// <summary>
        /// Success status - SCORM related.
        /// Indicates whether attempt was passed or failed.
        /// </summary>
        public SuccessStatus SuccessStatus { get; set; }

        /// <summary>
        /// Receives attempt beginning start timestamp.
        /// May be null.
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Receives attempt finish timestamp.
        /// May be null.
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// Score user got in result of one(this) attempt.
        /// </summary>
        public Score Score = new Score();

        #endregion

        #region Constructors

        public AttemptResult(long attemptId, User user, CurriculumChapterTopic curriculumChapterTopic, TopicTypeEnum topicType, CompletionStatus completionStatus, AttemptStatus attemptStatus, SuccessStatus successStatus, DateTime? startTime, DateTime? finishTime, float? minScore, float? maxScore, float? rawScore, float? scaledScore)
        {
            this.AttemptId = attemptId;
            this.User = user;
            this.CurriculumChapterTopic = curriculumChapterTopic;
            this.TopicType = topicType;

            this.CompletionStatus = completionStatus;
            this.AttemptStatus = attemptStatus;
            this.SuccessStatus = successStatus;
            this.StartTime = startTime;
            this.FinishTime = finishTime;
            this.Score = new Score(minScore, maxScore, rawScore, scaledScore);
        }

        public AttemptResult()
        {
        }

        #endregion
    }
}