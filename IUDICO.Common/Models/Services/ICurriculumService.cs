using System;
using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Shared.DisciplineManagement;

namespace IUDICO.Common.Models.Services
{
    public interface ICurriculumService : IService
    {
        IList<Curriculum> GetCurriculums(Func<Curriculum, bool> predicate);

        /// <summary>
        /// Gets the topic descriptions owned by user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IList<TopicDescription> GetTopicDescriptions(User user);

        IEnumerable<TopicDescription> GetTopicDescriptionsByTopics(IEnumerable<Topic> topics, User user);

        /// <summary>
        /// Gets CurriculumChapterTopic by specified <paramref name="curriculumChapterTopicId" /> id.
        /// </summary>
        /// <param name="curriculumChapterTopicId">32-bit Integer value representing CurriculumChapterTopic.Id to get entity by.</param>
        /// <returns>CurriculumChapterTopic value, if one exists with specified Id.</returns>
        CurriculumChapterTopic GetCurriculumChapterTopicById(int curriculumChapterTopicId);

        /// <summary>
        /// Defines if specified <paramref name="user"/> can pass specified <paramref name="curriculumChapterTopic"/> at the moment.
        /// </summary>
        /// <param name="user"><see cref="User"/>, passing allowance is being evaluated for.</param>
        /// <param name="curriculumChapterTopic"><see cref="CurriculumChapterTopic"/>, allowance to pass is being evaluated for.</param>
        /// <param name="topicType"><see cref="TopicTypeEnum"/> value, allowance to pass is being evaluated for.</param>
        /// <returns>Boolean value <value>true</value> in case <paramref name="user"/> can access(pass) <paramref name="curriculumChapterTopic"/> at the moment. Otherwise <value>false</value></returns>
        bool CanPassCurriculumChapterTopic(User user, CurriculumChapterTopic curriculumChapterTopic, TopicTypeEnum topicType);

        /// <summary>
        /// Returns all children of <see cref="Curriculum"/> specified by its <paramref name="curriculumId"/>.
        /// </summary>
        /// <param name="curriculumId">Int32 value representing identifier of <see cref="Curriculum"/></param>
        /// <returns><see cref="IEnumerable{T}"/> collection of <see cref="CurriculumChapterTopic"/> object, which are descendants of specified curriculum.</returns>
        IEnumerable<CurriculumChapterTopic> GetCurriculumChapterTopicsByCurriculumId(int curriculumId);
    }
}
