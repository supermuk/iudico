using System;
using System.Collections.Generic;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.TestingSystem.Models
{
    public class TestingService : ITestingService
    {
        #region ITestingService interface implementation

        public IEnumerable<AttemptResult> GetResults(Guid userID, int courseID)
        {
            throw new NotImplementedException();
            return new List<AttemptResult>();
        }

        #endregion
    }
}