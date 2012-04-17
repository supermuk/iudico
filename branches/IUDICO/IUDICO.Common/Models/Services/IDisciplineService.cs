using System.Collections.Generic;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Common.Models.Services
{
    public interface IDisciplineService : IService
    {
        #region Discipline methods

        IList<Discipline> GetDisciplines(IEnumerable<int> ids);
        Discipline GetDiscipline(int id);
        /// <summary>
        /// Gets disciplines which have topics owned by user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IList<Discipline> GetDisciplinesWithTopicsOwnedByUser(User user);
        IList<Discipline> GetDisciplines();
        /// <summary>
        /// Gets the disciplines owned by user or shared to him.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IList<Discipline> GetDisciplines(User user);

        #endregion

        #region Chapter methods

        IList<Chapter> GetChapters(int disciplineId);
        IList<Chapter> GetChapters(IEnumerable<int> ids);
        Chapter GetChapter(int id);

        #endregion

        #region Topic methods

        IList<Topic> GetTopicsByChapterId(int chapterId);
        IList<Topic> GetTopicsByDisciplineId(int disciplineId);
        IList<Topic> GetTopics(IEnumerable<int> ids);
        IList<Topic> GetTopics();
        /// <summary>
        /// Gets groups assigned through discipline assignments to topic.
        /// </summary>
        /// <param name="topicId">The topic id.</param>
        /// <returns></returns>
        IList<Group> GetGroupsAssignedToTopic(int topicId);

        Topic GetTopic(int id);

        #endregion

        #region Topic Type methods

        TopicType GetTopicType(int id);
        IList<TopicType> GetTopicTypes();
        IList<TopicType> GetTheoryTopicTypes();
        IList<TopicType> GetTestTopicTypes();

        #endregion

        #region Group methods

        IEnumerable<Discipline> GetDisciplinesByGroupId(int groupId);
        IEnumerable<Topic> GetTopicsByGroupId(int groupId);

        #endregion
    }
}
