using System;
using System.Collections.Generic;
using System.Linq;

using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.Statistics.Models.StatisticsModels
{
    /// <summary>
    /// This class contain all information that need for displaying of Stats/TopicTestResults page
    /// </summary>
    public class TopicTestResultsModel
    {

        #region Fields

        /// <summary>
        /// attempt to show
        /// </summary>
        private readonly AttemptResult attempt;

        private readonly IudicoCourseInfo courseInfo;

        /// <summary>
        /// user answers for this attempt
        /// </summary>
        private readonly IEnumerable<AnswerResult> userAnswers;

        /// <summary>
        /// if there is no data to show - true
        /// else - false
        /// </summary>
        private readonly bool hasNoData;

        #endregion

        #region Constructor

        /// <summary>
        /// constructor that get all information from Testing System
        /// </summary>
        /// <param name="attemptId">id of attempt to show</param>
        /// <param name="attList">list of attempts from Session Context</param>
        /// <param name="lmsService">ILmsService for conection to Testing System</param>
        public TopicTestResultsModel(long attemptId, IEnumerable<AttemptResult> attList, ILmsService lmsService)
        {
            if (attemptId != -1)
            {
                this.attempt = attList.First(c => c.AttemptId == attemptId);
                if (this.attempt != null)
                {
                    this.userAnswers = lmsService.FindService<ITestingService>().GetAnswers(this.attempt);
                    this.courseInfo =
                            lmsService.FindService<ICourseService>().GetCourseInfo(this.attempt.IudicoCourseRef);

                    this.hasNoData = this.userAnswers == null;
                }
                else
                {
                    this.hasNoData = true;
                }                
            }
            else
                this.hasNoData = true;
        }

        #endregion

        #region Get methods

        /// <summary>
        /// Return user answers for current attempt
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AnswerResult> GetUserAnswers()
        {
            return this.userAnswers;
        }

        /// <summary>
        /// Return name of current user
        /// </summary>
        /// <returns></returns>
        public string GetUserName()
        {
            return this.attempt != null ? this.attempt.User.Name : string.Empty;
        }

        /// <summary>
        /// Return name of current topic
        /// </summary>
        /// <returns></returns>
        public string GetTopicName()
        {
            return this.attempt != null ? this.attempt.CurriculumChapterTopic.Topic.Name : string.Empty;
        }

        /// <summary>
        /// Return success status of current attempt
        /// </summary>
        /// <returns></returns>
        public string GetSuccessStatus()
        {
            return this.attempt != null ? this.attempt.SuccessStatus.ToString() : string.Empty;
        }

        /// <summary>
        /// Return summary score for current attempt
        /// </summary>
        /// <returns></returns>
        public double GetScore()
        {
            double totalRawScore = 0;
            foreach (var answer in this.userAnswers)
            {
                if (answer.RawScore.HasValue)
                {
                    totalRawScore += answer.RawScore.Value;
                }
            }
            return totalRawScore;
        }

        public double GetMaxScore()
        {
            return courseInfo.OverallMaxScore;
        }

        public double GetPercentScore()
        {
            double rawScore = this.GetScore();

            double maxScore = this.GetMaxScore();

            if (maxScore == 0)
                return 0;

            return rawScore / maxScore * 100;          
        }

        /// <summary>
        /// Return user answer from AnswerResult
        /// </summary>
        /// <param name="answerResult">AnswerResult for question</param>
        /// <returns></returns>
        public string GetUserAnswer(AnswerResult answerResult)
        {
            return answerResult.LearnerResponse != null ? answerResult.LearnerResponse.ToString() : string.Empty;
        }

        /// <summary>
        /// Return user score from AnswerResult
        /// </summary>
        /// <param name="answerResult">AnswerResult for question</param>
        /// <returns></returns>
        public double GetUserScoreForAnswer(AnswerResult answerResult)
        {
            if (answerResult.RawScore != null)
            {
                if (answerResult.RawScore.HasValue)
                    return answerResult.RawScore.Value;
            }

            return 0;
        }

        /// <summary>
        /// Return maximum score from AnswerResult
        /// </summary>
        /// <param name="answerResult">AnswerResult for question</param>
        /// <returns></returns>
        public double GetMaxScoreForAnswer(AnswerResult answerResult)
        {
            if (answerResult.MaxScore != null)
            {
                if (answerResult.MaxScore.HasValue)
                    return answerResult.MaxScore.Value;
            }

            return 0;
        }

        /// <summary>
        /// Return object that determine if there is data to show 
        /// </summary>
        /// <returns></returns>
        public bool NoData()
        {
            return this.hasNoData;
        }

        #endregion
    }
}