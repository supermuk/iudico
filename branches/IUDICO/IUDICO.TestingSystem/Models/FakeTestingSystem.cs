using System;
using System.Collections.Generic;
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
            var results = new List<AttemptResult>
                              {
                                  new AttemptResult(0, user, theme, CompletionStatus.Unknown, AttemptStatus.Suspended,
                                                    SuccessStatus.Unknown, 0.21f),
                                  new AttemptResult(1, user, theme, CompletionStatus.NotAttempted, AttemptStatus.Active,
                                                    SuccessStatus.Unknown, null),
                                  new AttemptResult(2, user, theme, CompletionStatus.Completed, AttemptStatus.Completed,
                                                    SuccessStatus.Passed, 0.98f),
                                  new AttemptResult(3, user, theme, CompletionStatus.Incomplete, AttemptStatus.Completed,
                                                    SuccessStatus.Failed, 0.04f)
                              };

            return results;
        }

        public IEnumerable<AttemptResult> GetAllAttempts()
        {
            List<AttemptResult> results = new List<AttemptResult>();
            Theme a = new Theme();

            results.Add(new AttemptResult(0, new User { Name = "name1", Id = new Guid("1") }, new Theme { Name = "theme1", Id = 1 }, CompletionStatus.Unknown, AttemptStatus.Suspended, SuccessStatus.Unknown, 0.21f));
            results.Add(new AttemptResult(1, new User { Name = "name2", Id = new Guid("2") }, new Theme { Name = "theme2", Id = 2 }, CompletionStatus.NotAttempted, AttemptStatus.Active, SuccessStatus.Unknown, null));
            results.Add(new AttemptResult(2, new User { Name = "name3", Id = new Guid("3") }, new Theme { Name = "theme3", Id = 3 }, CompletionStatus.Completed, AttemptStatus.Completed, SuccessStatus.Passed, 0.98f));
            results.Add(new AttemptResult(3, new User { Name = "name4", Id = new Guid("4") }, new Theme { Name = "theme4", Id = 4}, CompletionStatus.Incomplete, AttemptStatus.Completed, SuccessStatus.Failed, 0.04f));

            return results;
        }

        public IEnumerable<AnswerResult> GetAnswers(AttemptResult attempt)
        {
            List<AnswerResult> results = new List<AnswerResult>();
            results.Add(new AnswerResult(0, 0, attempt, 0, "100", InteractionType.Numeric, null));
            results.Add(new AnswerResult(0, 1, attempt, null, "0", InteractionType.TrueFalse, null));
            results.Add(new AnswerResult(0, 2, attempt, 10, "5", InteractionType.Numeric, null));
            results.Add(new AnswerResult(0, 3, attempt, 1, "1", InteractionType.TrueFalse, null));
            results.Add(new AnswerResult(0, 4, attempt, null, "0", InteractionType.TrueFalse, 1));

            return results;

        }

        public IEnumerable<ActionLink> BuildLink(int themeId)
        {
            List<ActionLink> results = new List<ActionLink>();
            results.Add(new ActionLink("thema1",1));
            results.Add(new ActionLink("thema1",2));
            results.Add(new ActionLink("thema1",3));
            results.Add(new ActionLink("thema2",1));
            results.Add(new ActionLink("thema2",2));
            return results;
        }

        #endregion
    }
}