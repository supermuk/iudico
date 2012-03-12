﻿using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.TestingSystem.Models
{
    public interface IMlcProxy
    {
        //IEnumerable<Training> GetTrainings(long userKey);
        long GetAttemptId(Theme theme);
        IEnumerable<AttemptResult> GetAllAttempts();
        IEnumerable<AttemptResult> GetResults(User user, Theme theme);
        IEnumerable<AnswerResult> GetAnswers(AttemptResult attemptResult);
        //Training AddPackage(Package package);
        //void DeletePackage(long packId);
    }
}