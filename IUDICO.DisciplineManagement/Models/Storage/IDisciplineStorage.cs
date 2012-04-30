using System;
using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;

namespace IUDICO.DisciplineManagement.Models.Storage
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDisciplineStorage
    {
        #region External methods

        User GetCurrentUser();
        IList<Course> GetCourses();
        Course GetCourse(int id);
        IList<Group> GetGroups();
        IList<Curriculum> GetCurriculums(Func<Curriculum, bool> predicate);

        #endregion

        #region Discipline methods

        /// <summary>
        /// Gets the disciplines.
        /// </summary>
        /// <returns></returns>
        IList<Discipline> GetDisciplines();

        /// <summary>
        /// Gets the disciplines.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IList<Discipline> GetDisciplines(Func<Discipline, bool> predicate);

        /// <summary>
        /// Gets the disciplines.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        IList<Discipline> GetDisciplines(IEnumerable<int> ids);

        /// <summary>
        /// Gets the disciplines owned by user or shared to him
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IList<Discipline> GetDisciplines(User user);

        /// <summary>
        /// Gets the discipline.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        Discipline GetDiscipline(int id);

        /// <summary>
        /// Gets the disciplines which have discipline assignments binded to group
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <returns></returns>
        IList<Discipline> GetDisciplinesByGroupId(int groupId);

        /// <summary>
        /// Adds the discipline.
        /// </summary>
        /// <param name="discipline">The discipline.</param>
        /// <returns></returns>
        int AddDiscipline(Discipline discipline);

        /// <summary>
        /// Updates the discipline.
        /// </summary>
        /// <param name="discipline">The discipline.</param>
        void UpdateDiscipline(Discipline discipline);

        /// <summary>
        /// Deletes the discipline.
        /// </summary>
        /// <param name="id">The id.</param>
        void DeleteDiscipline(int id);

        /// <summary>
        /// Deletes the disciplines.
        /// </summary>
        /// <param name="ids">The ids.</param>
        void DeleteDisciplines(IEnumerable<int> ids);

        /// <summary>
        /// Makes disciplines connected with topics connected with courses invalid.
        /// </summary>
        /// <param name="courseId"></param>
        void MakeDisciplinesInvalid(int courseId);

        #endregion

        #region SharedDisciplines

        IList<ShareUser> GetDisciplineSharedUsers(int disciplineId);

        IList<ShareUser> GetDisciplineNotSharedUsers(int disciplineId);

        void UpdateDisciplineSharing(int disciplineId, IEnumerable<Guid> sharewith);

        #endregion

        #region Chapter methods

        /// <summary>
        /// Gets the chapter.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        Chapter GetChapter(int id);

        /// <summary>
        /// Gets the chapters.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IList<Chapter> GetChapters(Func<Chapter, bool> predicate);

        // IEnumerable<Chapter> GetChapters(int disciplineId);

        /// <summary>
        /// Gets the chapters.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        IList<Chapter> GetChapters(IEnumerable<int> ids);

        /// <summary>
        /// Adds the chapter.
        /// </summary>
        /// <param name="chapter">The chapter.</param>
        /// <returns></returns>
        int AddChapter(Chapter chapter);

        /// <summary>
        /// Updates the chapter.
        /// </summary>
        /// <param name="chapter">The chapter.</param>
        void UpdateChapter(Chapter chapter);

        /// <summary>
        /// Deletes the chapter.
        /// </summary>
        /// <param name="id">The id.</param>
        void DeleteChapter(int id);

        /// <summary>
        /// Deletes the chapters.
        /// </summary>
        /// <param name="ids">The ids.</param>
        void DeleteChapters(IEnumerable<int> ids);

        #endregion

        #region Topic methods

        /// <summary>
        /// Gets the topic.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        Topic GetTopic(int id);

        /// <summary>
        /// Gets the topics.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IList<Topic> GetTopics(Func<Topic, bool> predicate);

        /// <summary>
        /// Gets the topics.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        IList<Topic> GetTopics(IEnumerable<int> ids);

        IList<Topic> GetTopicsByDisciplineId(int disciplineId);

        /// <summary>
        /// Gets the topics by group id.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <returns></returns>
        IList<Topic> GetTopicsByGroupId(int groupId);

        /// <summary>
        /// Gets the topics by course id.
        /// </summary>
        /// <param name="courseId">The course id.</param>
        /// <returns></returns>
        IList<Topic> GetTopicsByCourseId(int courseId);

        /// <summary>
        /// Gets the topics owned by user.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        IList<Topic> GetTopicsOwnedByUser(User owner);

        /// <summary>
        /// Adds the topic.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <returns></returns>
        int AddTopic(Topic topic);

        /// <summary>
        /// Updates the topic.
        /// </summary>
        /// <param name="topic">The topic.</param>
        void UpdateTopic(Topic topic);

        /// <summary>
        /// Deletes the topic.
        /// </summary>
        /// <param name="id">The id.</param>
        void DeleteTopic(int id);

        /// <summary>
        /// Deletes the topics.
        /// </summary>
        /// <param name="ids">The ids.</param>
        void DeleteTopics(IEnumerable<int> ids);

        /// <summary>
        /// Topics up.
        /// </summary>
        /// <param name="topicId">The topic id.</param>
        /// <returns></returns>
        Topic TopicUp(int topicId);

        /// <summary>
        /// Topics down.
        /// </summary>
        /// <param name="topicId">The topic id.</param>
        /// <returns></returns>
        Topic TopicDown(int topicId);

        #endregion

        #region TopicType methods

        /// <summary>
        /// Gets the type of the topic.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        TopicType GetTopicType(int id);

        /// <summary>
        /// Gets the topic types.
        /// </summary>
        /// <returns></returns>
        IList<TopicType> GetTopicTypes();

        /// <summary>
        /// Gets the theory topic types.
        /// </summary>
        /// <returns></returns>
        IList<TopicType> GetTheoryTopicTypes();

        /// <summary>
        /// Gets the test topic types.
        /// </summary>
        /// <returns></returns>
        IList<TopicType> GetTestTopicTypes();

        #endregion
    }
}
