using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.Statistics.Models.StatisticsModels
{
    /// <summary>
    /// This class contain all information that need for displaying of Stats/ThemeTestResults page
    /// </summary>
    public class ThemeTestResultsModel
    {

        #region Fields

        /// <summary>
        /// attempt to show
        /// </summary>
        private readonly AttemptResult Attempt;

        /// <summary>
        /// user answers for this attempt
        /// </summary>
        private readonly IEnumerable<AnswerResult> UserAnswers;

        /// <summary>
        /// if there is no data to show - true
        /// else - false
        /// </summary>
        private readonly bool _NoData;

        #endregion

        #region Constructor

        /// <summary>
        /// constructor that get all information from Testing System
        /// </summary>
        /// <param name="attemptId">id of attempt to show</param>
        /// <param name="attList">list of attempts from Session Context</param>
        /// <param name="lmsService">ILmsService for conection to Testing System</param>
        public ThemeTestResultsModel(long attemptId, IEnumerable<AttemptResult> attList, ILmsService lmsService)
        {
            if (attemptId != -1)
            {
                Attempt = attList.First(c => c.AttemptId == attemptId);
                if (Attempt != null)
                {
                    UserAnswers = lmsService.FindService<ITestingService>().GetAnswers(Attempt);
                    if (UserAnswers != null)
                        _NoData = false;
                    else
                        _NoData = true;
                }
                else
                {
                    _NoData = true;
                }                
            }
            else
                _NoData = true;
        }

        #endregion

        #region Get methods

        /// <summary>
        /// Return user answers for current attempt
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AnswerResult> GetUserAnswers()
        {
            return this.UserAnswers;
        }

        /// <summary>
        /// Return name of current user
        /// </summary>
        /// <returns></returns>
        public String GetUserName()
        {
            if (this.Attempt != null)
                return this.Attempt.User.Name;
            else
                return "";
        }

        /// <summary>
        /// Return name of current theme
        /// </summary>
        /// <returns></returns>
        public String GetThemeName()
        {
            if (this.Attempt != null)
                return this.Attempt.Theme.Name;
            else
                return "";
        }

        /// <summary>
        /// Return success status of current attempt
        /// </summary>
        /// <returns></returns>
        public String GetSuccessStatus()
        {
            if (this.Attempt != null)
                return this.Attempt.SuccessStatus.ToString();
            else
                return "";
        }

        /// <summary>
        /// Return summary score for current attempt
        /// </summary>
        /// <returns></returns>
        public String GetScore()
        {
            if (this.Attempt != null)
            {
                if (this.Attempt.Score.ToPercents().HasValue ==true)
                    return Math.Round((double)this.Attempt.Score.ToPercents(), 2).ToString();
            }
            return "";
        }

        /// <summary>
        /// Return user answer from AnswerResult
        /// </summary>
        /// <param name="answerResult">AnswerResult for question</param>
        /// <returns></returns>
        public String GetUserAnswer(AnswerResult answerResult)
        {
            if (answerResult.LearnerResponse != null)
                return answerResult.LearnerResponse.ToString();
            else
                return "";
        }

        /// <summary>
        /// Return user score from AnswerResult
        /// </summary>
        /// <param name="answerResult">AnswerResult for question</param>
        /// <returns></returns>
        public String GetUserScoreForAnswer(AnswerResult answerResult)
        {
            if (answerResult.ScaledScore != null)
                {
                    if (answerResult.ScaledScore.HasValue == true)
                    return Math.Round((double)answerResult.ScaledScore, 2).ToString();
            }
            return "";
        }

        /// <summary>
        /// Return object that determine if there is data to show 
        /// </summary>
        /// <returns></returns>
        public bool NoData()
        {
            return this._NoData;
        }

        #endregion
    }
}