using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Models.StatisticsModels
{
    public class CurrentTopicTestResultsModel
    {
        private AttemptResult _Attempt;
        private IEnumerable<AnswerResult> _UserAnswers;
        private bool _NoData;
        public CurrentTopicTestResultsModel(int topicId, ILmsService lmsService)
        {
            User currenUser = lmsService.FindService<IUserService>().GetCurrentUser();
            Topic topic = lmsService.FindService<ICurriculumService>().GetTopic(topicId);
            if (currenUser != null & topic != null)
            {
                IEnumerable<AttemptResult> attemptResults = lmsService.FindService<ITestingService>().GetResults(currenUser, topic);
                if (attemptResults != null & attemptResults.Count() >= 1)
                {

                    _Attempt = attemptResults.Last();
                    if (_Attempt != null)
                    {
                        _UserAnswers = lmsService.FindService<ITestingService>().GetAnswers(_Attempt);
                        if (_UserAnswers != null)
                            _NoData = false;
                        else
                            _NoData = true;
                    }
                }
                else
                    _NoData = true;
            }
            else
                _NoData = true;
        }
        public IEnumerable<AnswerResult> GetUserAnswers()
        {
            return this._UserAnswers;
        }
        public String GetUserName()
        {
            if (this._Attempt != null)
                return this._Attempt.User.Username;
            else
                return "";
        }
        public String GetTopicName()
        {
            if (this._Attempt != null)
                return this._Attempt.Topic.Name;
            else
                return "";
        }
        public String GetSuccessStatus()
        {
            if (this._Attempt != null)
                return this._Attempt.SuccessStatus.ToString();
            else
                return "";
        }
        public String GetScore()
        {
            if (this._Attempt != null)
            {
                if (this._Attempt.Score.ToPercents().HasValue == true)
                    return Math.Round((double)this._Attempt.Score.ToPercents(), 2).ToString();
            }
            return "";
        }
        public String GetUserAnswer(AnswerResult answerResult)
        {
            if (answerResult.LearnerResponse != null)
                return Uri.UnescapeDataString(answerResult.LearnerResponse.ToString());
            else
                return "";
        }
        public bool NoData()
        {
            return this._NoData;
        }
    }
}