using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Shared.Statistics
{
    /// <summary>
    /// Represents answer (correct and user's) per one attempt over the activity.
    /// </summary>
    public class AnswerResult
    {
        #region Public Properties
        
        /// <summary>
        /// Identifier of Attempt performed over Activity (Item in manifest).
        /// </summary>
        public long ActivityAttemptId { get; protected set; }

        /// <summary>
        /// Title of activity. Represents "title" attribute from manifest's item node.
        /// </summary>
        public string ActivityTitle { get; protected set; }

        /// <summary>
        /// Identifier of Interaction.
        /// </summary>
        public long? InteractionId { get; protected set; }
        
        /// <summary>
        /// Represents result of one user's attempt on one theme(course).
        /// </summary>
        public AttemptResult AttemptResult { get; protected set; }
        
        /// <summary>
        /// Represents response (string/decimal/boolean) 
        /// user(SCO in fact) specified while performing attempt over activity.
        /// </summary>
        public object LearnerResponse { get; protected set; }
        
        /// <summary>
        /// String value, represents correct response, which SCO specified
        /// while user was attempting activity.
        /// </summary>
        public string CorrectResponse { get; protected set; }
        
        /// <summary>
        /// Type of learner response.
        /// Consider using this while casting LearnerResponse and comparing it with CorrectResponse.
        /// May be Null. In that case string representation for Learner Response should match best.
        /// </summary>
        public InteractionType? LearnerResponseType { get; protected set; }
        
        /// <summary>
        /// Float Nullable value represents scaled score, calculated while attempting activity.
        /// In most cases null.
        /// </summary>
        public float? ScaledScore { get; protected set; }
        
        #endregion

        #region Constructors

        public AnswerResult(long activityAttempId, string activityTitle, long? interactionId, AttemptResult attempResult,
            object learnerResponse, string correctResponse, InteractionType? learnerResponseType, float? scaledScore)
        {
            this.ActivityAttemptId = activityAttempId;
            this.ActivityTitle = activityTitle;
            this.InteractionId = interactionId;
            this.AttemptResult = attempResult;
            this.LearnerResponse = learnerResponse;
            this.CorrectResponse = correctResponse;
            this.LearnerResponseType = learnerResponseType;
            this.ScaledScore = scaledScore;
        }

        #endregion
    }
}
