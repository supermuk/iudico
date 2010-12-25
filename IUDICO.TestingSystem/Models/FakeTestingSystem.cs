using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Models;

namespace IUDICO.TestingSystem.Models
{
    public class FakeTestingSystem : ITestingService
    {
        #region ITestingSystem interface implementation

        public IEnumerable<AttemptResult> GetResults(User user, Theme theme)
        {
            List<AttemptResult> results = new List<AttemptResult>();
            results.Add(new AttemptResult(0, user, theme, CompletionStatus.Unknown, AttemptStatus.Suspended, SuccessStatus.Unknown, 0.21f));
            results.Add(new AttemptResult(1, user, theme, CompletionStatus.NotAttempted, AttemptStatus.Active, SuccessStatus.Unknown, null));
            results.Add(new AttemptResult(2, user, theme, CompletionStatus.Completed, AttemptStatus.Completed, SuccessStatus.Passed, 0.98f));
            results.Add(new AttemptResult(3, user, theme, CompletionStatus.Incomplete, AttemptStatus.Completed, SuccessStatus.Failed, 0.04f));

            return results;
        }

        #endregion
    }
}