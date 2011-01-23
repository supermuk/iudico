using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Shared.Statistics
{
    public class AnswerResult
    {
        #region Public Properties
        /// <summary>
        /// Identifier activity attemp
        /// </summary>
        public int ActivityAttempId { get; protected set; }
        /// <summary>
        /// Identifier of interaction
        /// </summary>
        public int InteractionId { get; protected set; }
        /// <summary>
        /// Represents result of one user's attempt on one theme(course).
        /// </summary>
        public AttemptResult _AttempResult { get; protected set; }
        /// <summary>
        /// Response user's
        /// </summary>
        public object LearnerResponse { get; protected set; }
        /// <summary>
        /// Correct response
        /// </summary>
        public string CorrectResponse { get; protected set; }
        /// <summary>
        /// type of response
        /// </summary>
        public InteractionType? LearnerResponseType { get; protected set; }
        /// <summary>
        /// Score user got in result for attempt.
        /// </summary>
        public float? ScaledScore { get; protected set; }
        #endregion

        #region Constructors
        public AnswerResult(int activityAttempId, int interactionId, AttemptResult _attempResult,
            object learnerResponse, string correctResponse, InteractionType? learnerResponseType, float? scaledScore)
        {
            this.ActivityAttempId = activityAttempId;
            this.InteractionId = interactionId;
            this._AttempResult = _attempResult;
            this.LearnerResponse = learnerResponse;
            this.CorrectResponse = correctResponse;
            this.LearnerResponseType = learnerResponseType;
            this.ScaledScore = scaledScore;
        }
        #endregion
    }
}
