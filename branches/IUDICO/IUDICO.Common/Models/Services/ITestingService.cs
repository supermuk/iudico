using System;
using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.Common.Models.Services
{
    public interface ITestingService : IService
    {
        /// <summary>
        /// Gets result with specified <paramref name="attemptId"/>.
        /// </summary>
        /// <param name="attemptId">Long integer value representing id of AttemptItem.</param>
        /// <returns><see cref="AttemptResult"/> value with specified <paramref name="attemptId"/>.
        /// If no attempt with specified <paramref name="attemptId"/> found - <value>null</value> is returned.
        /// In case there were somehow more than one <see cref="AttemptResult"/> with specified <paramref name="attemptId"/> - <see cref="InvalidOperationException"/> would be thrown.</returns>
        AttemptResult GetResult(long attemptId);

        /// <summary>
        /// Gets results of attempts on specified <paramref name="curriculumChapterTopic"/> for specified <paramref name="user"/>.
        /// It is important to understand there can be no attempts or be a few,
        /// so appropriate number of results would be returned.
        /// </summary>
        /// <param name="user"><see cref="User"/> object, represents user for which attempt results are returned.</param>
        /// <param name="curriculumChapterTopic"><see cref="CurriculumChapterTopic"/> object, represents curriculum chapter topic, for which attempt results are returned.</param>
        /// <returns><see cref="IEnumerable{T}"/> collection of <see cref="AttemptResult"/> objects. Can return zero or more attempt results.
        /// Zero count means user has not attempted relative courses(from topic) yet.</returns>
        IEnumerable<AttemptResult> GetResults(User user, CurriculumChapterTopic curriculumChapterTopic);

        /// <summary>
        /// Gets results of attempts on specified <paramref name="curriculumChapterTopic"/> for specified <paramref name="user"/>.
        /// It is important to understand there can be no attempts or be a few,
        /// so appropriate number of results would be returned.
        /// </summary>
        /// <param name="user"><see cref="User"/> object, represents user for which attempt results are returned.</param>
        /// <param name="curriculumChapterTopic"><see cref="CurriculumChapterTopic"/> object, represents curriculum chapter topic, for which attempt results are returned.</param>
        /// <param name="topicType"><see cref="TopicTypeEnum"/> enum value representing part/type of topic, results would be filtered by.</param>
        /// <returns><see cref="IEnumerable{T}"/> collection of <see cref="AttemptResult"/> objects. Can return zero or more attempt results.
        /// Zero count means user has not attempted relative course(from topic) yet.</returns>
        IEnumerable<AttemptResult> GetResults(User user, CurriculumChapterTopic curriculumChapterTopic, TopicTypeEnum topicType);

        /// <summary>
        /// Gets results of all attempts for specified <paramref name="user"/>.
        /// It is important to understand there can be no attempts or be a few,
        /// so appropriate number of results would be returned.
        /// </summary>
        /// <param name="user"><see cref="User"/> object, represents user for which attempt results are returned.</param>
        /// <returns><see cref="IEnumerable{T}"/> collection of <see cref="AttemptResult"/> objects. Can return zero or more attempt results.
        /// Zero count means user has not attempted any topic yet.</returns>
        IEnumerable<AttemptResult> GetResults(User user);

        /// <summary>
        /// Gets results of attempts for all users on specified <paramref name="curriculumChapterTopic"/>.
        /// It is important to understand there can be no attempts or be a few,
        /// so appropriate number of results would be returned.
        /// </summary>
        /// <param name="curriculumChapterTopic"><see cref="CurriculumChapterTopic"/> object, represents curriculum chapter topic, for which attempt results are returned.</param>
        /// <returns><see cref="IEnumerable{T}"/> collection of <see cref="AttemptResult"/> objects. Can return zero or more attempt results.
        /// Zero count means no user has attempted relative <paramref name="curriculumChapterTopic"/> yet.</returns>
        IEnumerable<AttemptResult> GetResults(CurriculumChapterTopic curriculumChapterTopic);

        /// <summary>
        /// Gets results of attempts for all users on specified <paramref name="topic"/>.
        /// It is important to understand there can be no attempts or be a few,
        /// so appropriate number of results would be returned.
        /// </summary>
        /// <param name="topic"><see cref="Topic"/> object, represents topic, for which attempt results are returned.</param>
        /// <returns><see cref="IEnumerable{T}"/> collection of <see cref="AttemptResult"/> objects. Can return zero or more attempt results.
        /// Zero count means no user has attempted relative <paramref name="topic"/> yet.</returns>
        IEnumerable<AttemptResult> GetResults(Topic topic);

        /// <summary>
        /// Gets results of all attempts in the system.
        /// </summary>
        /// <returns>><see cref="IEnumerable{T}"/> collection of <see cref="AttemptResult"/></returns>
        IEnumerable<AttemptResult> GetResults();

        /// <summary>
        /// Gets answers for specified <paramref name="attempt"/>.
        /// </summary>
        /// <param name="attempt">Attempt, for which answers are returned.</param>
        /// <returns><see cref="IEnumerable{T}"/> collection of <see cref="AnswerResult"/></returns>
        IEnumerable<AnswerResult> GetAnswers(AttemptResult attempt);
    }
}
