using System;
using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;

namespace IUDICO.Common.Models.Services
{
    public interface ICurriculumService : IService
    {
        #region Discipline methods

        //IEnumerable<Discipline> GetDisciplines();
        IEnumerable<Discipline> GetDisciplines(IEnumerable<int> ids);
        Discipline GetDiscipline(int id);
        /// <summary>
        /// Gets disciplines which have topics owned by user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IEnumerable<Discipline> GetDisciplinesWithTopicsOwnedByUser(User user);
        IEnumerable<Discipline> GetDisciplines();
        IEnumerable<Discipline> GetDisciplines(User user);

        #endregion

        #region Chapter methods

        IEnumerable<Chapter> GetChapters(int disciplineId);
        IEnumerable<Chapter> GetChapters(IEnumerable<int> ids);
        Chapter GetChapter(int id);

        #endregion

        #region Topic methods

        IEnumerable<Topic> GetTopicsByChapterId(int chapterId);
        IEnumerable<Topic> GetTopicsByDisciplineId(int disciplineId);
        IEnumerable<Topic> GetTopics(IEnumerable<int> ids);
        IEnumerable<Topic> GetTopics();
        /// <summary>
        /// Gets the topics available for user for current date.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IEnumerable<TopicDescription> GetTopicsAvailableForUser(User user);
        /// <summary>
        /// Gets groups assigned through discipline assignments to topic.
        /// </summary>
        /// <param name="topicId">The topic id.</param>
        /// <returns></returns>
        IEnumerable<Group> GetGroupsAssignedToTopic(int topicId);
        /// <summary>
        /// Gets the topics owned by user.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        IEnumerable<Topic> GetTopicsOwnedByUser(User owner);
        Topic GetTopic(int id);

        #endregion

        #region Assignment methods

        IEnumerable<Discipline> GetDisciplinesByGroupId(int groupId);
        IEnumerable<Topic> GetTopicsByGroupId(int groupId);

        #endregion
    }
}
