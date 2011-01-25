using IUDICO.Common.Models;
using IUDICO.TestingSystem.Models.VO;
using System.Collections.Generic;
using System;

namespace IUDICO.TestingSystem.Models
{
    public interface IMlcProxy
    {
        IEnumerable<Training> GetTrainings(long userKey);
        long CreateAttempt(long orgID);
        Training AddPackage(Package package);
        void DeletePackage(long packId);
    }
}
