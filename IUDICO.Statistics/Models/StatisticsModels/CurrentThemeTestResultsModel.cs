using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.Statistics.Models.StatisticsModels
{
    public class CurrentTopicTestResultsModel
    {
        private readonly AttemptResult attempt;
        private readonly IEnumerable<AnswerResult> userAnswers;
        private readonly bool hasNoData;

        public CurrentTopicTestResultsModel(int curriculumChapterTopicId, TopicTypeEnum topicType, ILmsService lmsService)
        {
            var currenUser = lmsService.FindService<IUserService>().GetCurrentUser();
            var curriculumChapterTopic = lmsService.FindService<ICurriculumService>().GetCurriculumChapterTopicById(curriculumChapterTopicId);
            if (currenUser != null & curriculumChapterTopic != null)
            {
                var attemptResults = lmsService.FindService<ITestingService>().GetResults(currenUser, curriculumChapterTopic, topicType).ToList();
                if (attemptResults.Count() >= 1)
                {

                    this.attempt = attemptResults.Last();
                    if (this.attempt != null)
                    {
                        this.userAnswers = lmsService.FindService<ITestingService>().GetAnswers(this.attempt);
                        
                        this.hasNoData = this.userAnswers == null;
                    }
                }
                else
                    this.hasNoData = true;
            }
            else
                this.hasNoData = true;
        }

        public IEnumerable<AnswerResult> GetUserAnswers()
        {
            return this.userAnswers;
        }

        public string GetUserName()
        {
            return this.attempt != null ? this.attempt.User.Username : string.Empty;
        }

        public string GetTopicName()
        {
            return this.attempt != null ? this.attempt.CurriculumChapterTopic.Topic.Name : string.Empty;
        }

        public string GetSuccessStatus()
        {
            return this.attempt != null ? this.attempt.SuccessStatus.ToString() : string.Empty;
        }

        public string GetScore()
        {
            if (this.attempt != null)
            {
                if (this.attempt.Score.ToPercents().HasValue == true)
                    return Math.Round((double)this.attempt.Score.ToPercents(), 2).ToString();
            }

            return string.Empty;
        }
        public string GetUserAnswer(AnswerResult answerResult)
        {
            return answerResult.LearnerResponse != null ? Uri.UnescapeDataString(answerResult.LearnerResponse.ToString()) : string.Empty;
        }

        public bool NoData()
        {
            return this.hasNoData;
        }
    }
}