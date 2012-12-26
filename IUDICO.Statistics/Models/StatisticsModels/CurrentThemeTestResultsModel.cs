using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.Statistics.Models.StatisticsModels
{
    public class CurrentTopicTestResultsModel
    {
        private readonly AttemptResult attempt;
        private readonly IudicoCourseInfo courseInfo;
        private readonly IEnumerable<AnswerResult> userAnswers;
        private readonly bool hasNoData;

        public CurrentTopicTestResultsModel(int curriculumChapterTopicId, TopicTypeEnum topicType, ILmsService lmsService)
        {
            var currenUser = lmsService.FindService<IUserService>().GetCurrentUser();
            var curriculumChapterTopic = lmsService.FindService<ICurriculumService>().GetCurriculumChapterTopicById(curriculumChapterTopicId);
            if (currenUser != null & curriculumChapterTopic != null)
            {
                var attemptResults = lmsService.FindService<ITestingService>().GetResults(currenUser, curriculumChapterTopic, topicType).ToList();
                if (attemptResults.Any())
                {
                    // hotfix: added checking of Course id
                    this.attempt =
                        attemptResults.FirstOrDefault(x => x.CurriculumChapterTopic.Topic.TestCourseRef == x.IudicoCourseRef);
                    if (this.attempt != null)
                    {
                        this.courseInfo =
                            lmsService.FindService<ICourseService>().GetCourseInfo(this.attempt.IudicoCourseRef);
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
            return this.GetPercentScore() >= this.attempt.CurriculumChapterTopic.ThresholdOfSuccess ? "passed" : "failed";
        }

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
            double maxScore = 0;
            foreach (var node in this.courseInfo.NodesInfo)
            {
                if (this.userAnswers.Any(answer => int.Parse(answer.PrimaryResourceFromManifest.Replace(".html", string.Empty)) == node.Id))
                {
                    maxScore += node.MaxScore;
                }
            }
            return maxScore;
        }

        public double GetPercentScore()
        {
            double rawScore = this.GetScore();
            double maxScore = this.GetMaxScore();

            if (maxScore == 0)
                return 0;

            return rawScore / maxScore * 100;            
        }

        public string GetUserAnswer(AnswerResult answerResult)
        {
            return answerResult.LearnerResponse != null ? Uri.UnescapeDataString(answerResult.LearnerResponse.ToString()) : string.Empty;
        }

        public DateTime DateTimeStarted()
        {
            return this.attempt.StartTime ?? DateTime.MinValue;
        }

        public DateTime DateTimeFinished()
        {
            return this.attempt.FinishTime ?? DateTime.MinValue;
        }

        public bool NoData()
        {
            return this.hasNoData;
        }
    }
}