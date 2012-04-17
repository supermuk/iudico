using System.Collections.Generic;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
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

        public IEnumerable<AttemptResult> GetResults(User user, CurriculumChapterTopic curriculumChapterTopic)
        {
            return MlcProxy.GetResults(user, curriculumChapterTopic);
        }

        public IEnumerable<AttemptResult> GetResults(User user)
        {
            return MlcProxy.GetResults(user);
        }

        public IEnumerable<AttemptResult> GetResults(CurriculumChapterTopic curriculumChapterTopic)
        {
            return MlcProxy.GetResults(curriculumChapterTopic);
        }

        public IEnumerable<AttemptResult> GetResults(Topic topic)
        {
            return MlcProxy.GetResults(topic);
        }

        public IEnumerable<AttemptResult> GetAllAttempts()
        {
            return MlcProxy.GetResults();
        }

        public IEnumerable<AnswerResult> GetAnswers(AttemptResult attempt)
        {
            return MlcProxy.GetAnswers(attempt);
        }

        #endregion
    }
}