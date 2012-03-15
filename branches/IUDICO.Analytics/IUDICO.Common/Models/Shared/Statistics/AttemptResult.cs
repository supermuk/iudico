using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// Topic, attempt result is for.
        /// </summary>
        public Topic Topic { get; protected set; }

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
        /// Receives attempt finish timestamp.
        /// May be null.
        /// </summary>
        public DateTime? FinishTime { get; protected set; }

        /// <summary>
        /// Score user got in result of one(this) attempt.
        /// </summary>
        public Score Score { get; protected set; }
        
	    #endregion

        #region Constructors

        public AttemptResult(long attemptId, User user, Topic topic, 
            CompletionStatus completionStatus, AttemptStatus attemptStatus,
            SuccessStatus successStatus, DateTime? startTime, DateTime? finishTime,
            float? scaledScore)
        {
            this.AttemptId = attemptId;
            this.User = user;
            this.Topic = topic;
            
            this.CompletionStatus = completionStatus;
            this.AttemptStatus = attemptStatus;
            this.SuccessStatus = successStatus;
            this.StartTime = startTime;
            this.FinishTime = finishTime;
            this.Score = new Score(scaledScore);
        }

        #endregion
    }
}
