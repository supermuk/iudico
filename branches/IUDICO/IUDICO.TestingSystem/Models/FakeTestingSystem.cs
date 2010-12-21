using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.TestingSystem.Models
{
    public class FakeTestingSystem : ITestingService
    {
        #region ITestingSystem interface implementation

        public AttemptResult PlayCourse(int courseID)
        {
            AttemptResult result = new AttemptResult(0, getUserID(), courseID, CompletionStatus.Unknown, AttemptStatus.Suspended, SuccessStatus.Unknown, 0.11f);
            return result;
        }

        public IEnumerable<AttemptResult> GetResults(Guid userID, int courseID)
        {
            List<AttemptResult> results = new List<AttemptResult>();
            results.Add(new AttemptResult(0, userID, courseID, CompletionStatus.Unknown, AttemptStatus.Suspended, SuccessStatus.Unknown, 0.21f));
            results.Add(new AttemptResult(1, userID, courseID, CompletionStatus.NotAttempted, AttemptStatus.Active, SuccessStatus.Unknown, null));
            results.Add(new AttemptResult(2, userID, courseID, CompletionStatus.Completed, AttemptStatus.Completed, SuccessStatus.Passed, 0.98f));
            results.Add(new AttemptResult(3, userID, courseID, CompletionStatus.Incomplete, AttemptStatus.Completed, SuccessStatus.Failed, 0.04f));

            return results;
        }

        #endregion

        #region Helpers

        private Guid getUserID()
        {
            return Guid.NewGuid();
        }

        #endregion
    }
}