using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Shared.Statistics
{
    public class AttemptResult
    {
        #region Public Properties

        public int AttemptID { get; protected set; }
        public Guid UserID { get; protected set; }
        public int CourseID { get; protected set; }

        public CompletionStatus CompleationStatus { get; protected set; }
        public AttemptStatus AttemptStatus { get; protected set; }
        public SuccessStatus SuccessStatus { get; protected set; }
        public Score Score { get; protected set; }
        
	    #endregion

        #region Constructors

        public AttemptResult(int attemptID, Guid userID, int courseID, 
            CompletionStatus completionStatus, AttemptStatus attemptStatus, 
            SuccessStatus successStatus, float? scaledScore)
        {
            this.AttemptID = attemptID;
            this.UserID = userID;
            this.CourseID = courseID;
            
            this.CompleationStatus = completionStatus;
            this.AttemptStatus = attemptStatus;
            this.SuccessStatus = successStatus;
            this.Score = new Score(scaledScore);
        }

        #endregion
    }
}
