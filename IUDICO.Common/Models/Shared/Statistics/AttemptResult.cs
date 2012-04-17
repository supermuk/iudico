using System;
using IUDICO.Common.Models.Shared.DisciplineManagement;

namespace IUDICO.Common.Models.Shared.Statistics
{
    /// <summary>
    /// Represents result of one user's attempt on one topic(course).
    /// </summary>
    public class AttemptResult
    {
        #region Public Properties

        /// <summary>
        /// Identifier of attempt, this result is related to.
        /// </summary>
        public long AttemptId { get; protected set; }
        
        /// <summary>
        /// User, attempt result is for.
        /// </summary>
        public User User { get; protected set; }
        
        /// <summary>
        /// CurriculumChapterTopic, attempt result is for.
        /// </summary>
        public CurriculumChapterTopic CurriculumChapterTopic { get; protected set; }

        /// <summary>
        /// Type of referenced topic.
        /// </summary>
        public TopicTypeEnum TopicType { get; protected set; }

        /// <summary>
        /// Completion status - SCORM related.
        /// Indicates whether attempt is completed or incompleted.
        /// </summary>
        public CompletionStatus CompletionStatus { get; protected set; }
       
        /// <summary>
        /// Attempt status - represents in general what is status of attempt.
        /// </summary>
        public AttemptStatus AttemptStatus { get; protected set; }
        
        /// <summary>
        /// Success status - SCORM related.
        /// Indicates whether attempt was passed or failed.
        /// </summary>
        public SuccessStatus SuccessStatus { get; protected set; }

        /// <summary>
        /// Receives attempt beginning start timestamp.
        /// May be null.
        /// </summary>
        public DateTime? StartTime { get; protected set; }

        /// <summary>
        /// Score user got in result of one(this) attempt.
        /// </summary>
        public Score Score { get; protected set; }
        
	    #endregion

        #region Constructors

        public AttemptResult(long attemptId, User user, CurriculumChapterTopic curriculumChapterTopic,
            TopicTypeEnum topicType, CompletionStatus completionStatus, AttemptStatus attemptStatus, 
            SuccessStatus successStatus, DateTime? startTime, float? scaledScore)
        {
            this.AttemptId = attemptId;
            this.User = user;
            this.CurriculumChapterTopic = curriculumChapterTopic;
            this.TopicType = topicType;

            this.CompletionStatus = completionStatus;
            this.AttemptStatus = attemptStatus;
            this.SuccessStatus = successStatus;
            this.StartTime = startTime;
            this.Score = new Score(scaledScore);
        }

        #endregion
    }
}
