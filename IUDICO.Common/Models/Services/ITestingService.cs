using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.Statistics;
using System.Collections.Generic;

namespace IUDICO.Common.Models.Services
{
    public interface ITestingService : IService
    {
        /// <summary>
        /// Gets results of attempts on specified topic for specified user.
        /// It is important to understand there can be no attempts or be a few,
        /// so appropriate number of results would be returned.
        /// </summary>
        /// <param name="user">User value, represents user for which attempt results are returned.</param>
        /// <param name="curriculumChapterTopic">Topic value, represents topic, for which attempt results are returned.</param>
        /// <returns>Collection of AttemptResults objects. Can return zero or more attempt results. Zero count means user has not attempted relative course(from topic) yet.</returns>
        IEnumerable<AttemptResult> GetResults(User user, CurriculumChapterTopic curriculumChapterTopic);

        IEnumerable<AttemptResult> GetResults(User user);

        IEnumerable<AttemptResult> GetResults(CurriculumChapterTopic curriculumChapterTopic);

        IEnumerable<AttemptResult> GetResults(Topic topic);

        /// <summary>
        /// Gets results of all attempts
        /// </summary>
        /// <returns>>Collection of AttemptResults objects</returns>
        IEnumerable<AttemptResult> GetAllAttempts();

        /// <summary>
        /// Gets answers for specified attempt.
        /// </summary>
        /// <param name="attempt">Attempt, for which answers are returned.</param>
        /// <returns>Collection of AnswerResult objects.</returns>
        IEnumerable<AnswerResult> GetAnswers(AttemptResult attempt);
    }
}
