using System;
using System.Collections.Generic;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.TestingSystem.Models
{
    public class FakeTestingSystem : ITestingService
    {
        #region ITestingSystem interface implementation
        
        public IEnumerable<AttemptResult> GetResults(Guid userId, int courseId)
        {
            var results = new List<AttemptResult>
                              {
                                  new AttemptResult(0, userId, courseId, CompletionStatus.Unknown,
                                                    AttemptStatus.Suspended, SuccessStatus.Unknown, 0.21f),
                                  new AttemptResult(1, userId, courseId, CompletionStatus.NotAttempted,
                                                    AttemptStatus.Active, SuccessStatus.Unknown, null),
                                  new AttemptResult(2, userId, courseId, CompletionStatus.Completed,
                                                    AttemptStatus.Completed, SuccessStatus.Passed, 0.98f),
                                  new AttemptResult(3, userId, courseId, CompletionStatus.Incomplete,
                                                    AttemptStatus.Completed, SuccessStatus.Failed, 0.04f)
                              };

            return results;
        }

        #endregion

        #region Helpers

        private Guid GetUserId()
        {
            return Guid.NewGuid();
        }

        #endregion
    }
}