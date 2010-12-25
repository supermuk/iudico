using System;
using System.Collections.Generic;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Models;

namespace IUDICO.TestingSystem.Models
{
    public class TestingService : ITestingService
    {
        #region ITestingService interface implementation

        public IEnumerable<AttemptResult> GetResults(User user, Theme theme)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}