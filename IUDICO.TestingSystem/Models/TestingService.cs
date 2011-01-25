using System;
using System.Collections.Generic;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Models;
using System.Web;

namespace IUDICO.TestingSystem.Models
{
    public class TestingService : ITestingService
    {
        #region ITestingService interface implementation

        public IEnumerable<AttemptResult> GetResults(User user, Theme theme)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttemptResult> GetAllAttempts()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<AnswerResult> GetAnswers(AttemptResult attempt)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}