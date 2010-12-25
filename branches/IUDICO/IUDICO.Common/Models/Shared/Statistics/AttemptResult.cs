using System;

namespace IUDICO.Common.Models.Shared.Statistics
{
    public class AttemptResult
    {
        #region Public Properties

        public int AttemptId { get; protected set; }
        public Guid UserId { get; protected set; }
        public int CourseId { get; protected set; }

        public CompletionStatus CompleationStatus { get; protected set; }
        public AttemptStatus AttemptStatus { get; protected set; }
        public SuccessStatus SuccessStatus { get; protected set; }
        public Score Score { get; protected set; }
        
	    #endregion

        #region Constructors

        public AttemptResult(int attemptId, Guid userId, int courseId, 
            CompletionStatus completionStatus, AttemptStatus attemptStatus, 
            SuccessStatus successStatus, float? scaledScore)
        {
            AttemptId = attemptId;
            UserId = userId;
            CourseId = courseId;
            
            CompleationStatus = completionStatus;
            AttemptStatus = attemptStatus;
            SuccessStatus = successStatus;
            Score = new Score(scaledScore);
        }

        #endregion
    }
}
