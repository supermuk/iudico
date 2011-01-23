using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Shared.Statistics
{
    /// <summary>
    /// Represents result of one user's attempt on one theme(course).
    /// </summary>
    public class AttemptResult
    {
        #region Public Properties

        /// <summary>
        /// Identifier of attempt, this result is related to.
        /// </summary>
        public int AttemptId { get; protected set; }
        /// <summary>
        /// User, attempt result is for.
        /// </summary>
        public User User { get; protected set; }
        /// <summary>
        /// Theme, attempt result is for.
        /// </summary>
        public Theme Theme { get; protected set; }

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
        /// Score user got in result of one(this) attempt.
        /// </summary>
        public Score Score { get; protected set; }
        
	    #endregion

        #region Constructors

        public AttemptResult(int attemptId, User user, Theme theme, 
            CompletionStatus completionStatus, AttemptStatus attemptStatus, 
            SuccessStatus successStatus, float? scaledScore)
        {
            AttemptId = attemptId;
            User = user;
            Theme = theme;
            
            CompletionStatus = completionStatus;
            AttemptStatus = attemptStatus;
            SuccessStatus = successStatus;
            Score = new Score(scaledScore);
        }

        #endregion
    }
}
