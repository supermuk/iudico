// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestingService.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.TestingSystem.Models
{
    public class TestingService : ITestingService
    {
        protected readonly IMlcProxy MlcProxy;

        public TestingService(IMlcProxy proxy)
        {
            this.MlcProxy = proxy;
        }

        #region ITestingService interface implementation

       public AttemptResult GetResult(long attemptId)
        {
            return this.MlcProxy.GetResult(attemptId);
        }

        public IEnumerable<AttemptResult> GetResults(User user, CurriculumChapterTopic curriculumChapterTopic)
        {
            return this.MlcProxy.GetResults(user, curriculumChapterTopic);
        }

        public IEnumerable<AttemptResult> GetResults(
            User user, CurriculumChapterTopic curriculumChapterTopic, TopicTypeEnum topicType)
        {
            return this.MlcProxy.GetResults(user, curriculumChapterTopic, topicType);
        }

        public IEnumerable<AttemptResult> GetResults(User user)
        {
            return this.MlcProxy.GetResults(user);
        }

        public IEnumerable<AttemptResult> GetResults(CurriculumChapterTopic curriculumChapterTopic)
        {
            return this.MlcProxy.GetResults(curriculumChapterTopic);
        }

        public IEnumerable<AttemptResult> GetResults(Topic topic)
        {
            return this.MlcProxy.GetResults(topic);
        }

        public IEnumerable<AttemptResult> GetResults()
        {
            return this.MlcProxy.GetResults();
        }

        public IEnumerable<AnswerResult> GetAnswers(AttemptResult attempt)
        {
            return this.MlcProxy.GetAnswers(attempt);
        }

        #endregion
    }
}