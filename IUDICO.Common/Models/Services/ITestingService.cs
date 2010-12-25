using IUDICO.Common.Models.Shared.Statistics;
using System.Collections.Generic;
using System;

namespace IUDICO.Common.Models.Services
{
    public interface ITestingService : IService
    {
        IEnumerable<AttemptResult> GetResults(Guid userId, int courseId);
    }
}
