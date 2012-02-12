using System.Collections.Generic;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICurriculumStorage
    {
        #region External methods

        //void RefreshState();
        User GetCurrentUser();
        IEnumerable<Course> GetCourses();
        Course GetCourse(int id);
        Group GetGroup(int id);
        IEnumerable<Group> GetGroups();
        IEnumerable<Group> GetGroupsByUser(User user);
        IEnumerable<Course> GetCoursesOwnedByUser(User user);

        #endregion

        #region Discipline methods

        IEnumerable<Discipline> GetDisciplines();
        IEnumerable<Discipline> GetDisciplines(User owner);
        IEnumerable<Discipline> GetDisciplines(IEnumerable<int> ids);
        Discipline GetDiscipline(int id);
        /// <summary>
        /// Gets the disciplines which have discipline assignments binded to group
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <returns></returns>
        IEnumerable<Discipline> GetDisciplinesByGroupId(int groupId);
        /// <summary>
        /// Gets disciplines which have topics owned by user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IEnumerable<Discipline> GetDisciplinesWithTopicsOwnedByUser(User user);
        int AddDiscipline(Discipline discipline);
        void UpdateDiscipline(Discipline discipline);
        void DeleteDiscipline(int id);
        void DeleteDisciplines(IEnumerable<int> ids);
        void MakeDisciplineInvalid(int courseId);

        #endregion

        #region Chapter methods

        IEnumerable<Chapter> GetChapters(int disciplineId);
        IEnumerable<Chapter> GetChapters(IEnumerable<int> ids);
        int AddChapter(Chapter chapter);
        Chapter GetChapter(int id);
        void UpdateChapter(Chapter chapter);
        void DeleteChapter(int id);
        void DeleteChapters(IEnumerable<int> ids);

        #endregion

        #region Topic methods

        Topic GetTopic(int id);
        IEnumerable<Topic> GetTopics(IEnumerable<int> ids);
        IEnumerable<Topic> GetTopicsByChapterId(int chapterId);
        IEnumerable<Topic> GetTopicsByDisciplineId(int disciplineId);
        IEnumerable<Topic> GetTopicsByGroupId(int groupId);
        IEnumerable<Topic> GetTopicsByCourseId(int courseId);
        /// <summary>
        /// Gets the topics owned by user.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        IEnumerable<Topic> GetTopicsOwnedByUser(User owner);
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
        int AddTopic(Topic topic);
        void UpdateTopic(Topic topic);
        void DeleteTopic(int id);
        void DeleteTopics(IEnumerable<int> ids);
        Topic TopicUp(int topicId);
        Topic TopicDown(int topicId);

        #endregion

        #region TopicType methods

        TopicType GetTopicType(int id);
        IEnumerable<TopicType> GetTopicTypes();

        #endregion

        #region Curriculum methods

        Curriculum GetCurriculum(int curriculumId);
        IEnumerable<Curriculum> GetCurriculums(IEnumerable<int> ids);
        IEnumerable<Curriculum> GetDisciplineAssignmnetsByDisciplineId(int disciplineId);
        IEnumerable<Curriculum> GetCurriculumsByGroupId(int groupId);
        IEnumerable<Curriculum> GetCurriculums();
        int AddCurriculum(Curriculum curriculum);
        void UpdateCurriculum(Curriculum curriculum);
        void DeleteCurriculum(int id);
        void DeleteCurriculums(IEnumerable<int> ids);
        void MakeCurriculumsInvalid(int groupId);

        #endregion

        #region TopicAssignment methods

        TopicAssignment GetTopicAssignment(int topicAssignmentId);
        IEnumerable<TopicAssignment> GetTopicAssignmentsByCurriculumId(int curriculumId);
        IEnumerable<TopicAssignment> GetTopicAssignmentsByTopicId(int topicId);
        IEnumerable<TopicAssignment> GetTopicAssignments(IEnumerable<int> ids);
        int AddTopicAssignment(TopicAssignment topicAssignment);
        void UpdateTopicAssignment(TopicAssignment topicAssignment);
        void DeleteTopicAssignments(IEnumerable<int> ids);

        #endregion

        #region Timeline methods

        Timeline GetTimeline(int TimelineId);
        IEnumerable<Timeline> GetTimelines(IEnumerable<int> timelineIds);
        IEnumerable<Timeline> GetCurriculumTimelines(int curriculumId);
        IEnumerable<Timeline> GetChapterTimelinesByCurriculumId(int curriculumId);
        IEnumerable<Timeline> GetChapterTimelinesByChapterId(int chapterId);
        IEnumerable<Timeline> GetChapterTimelines(int chapterId, int curriculumId);
        int AddTimeline(Timeline timeline);
        void UpdateTimeline(Timeline timeline);
        void DeleteTimeline(int timelineId);
        void DeleteTimelines(IEnumerable<int> timelineIds);

        #endregion

        #region Group methods

        IEnumerable<Group> GetAssignedGroups(int disciplineId);
        IEnumerable<Group> GetNotAssignedGroups(int disciplineId);
        /// <summary>
        /// Gets not assigned groups for discipline including current group.
        /// </summary>
        /// <param name="disciplineId">The discipline id.</param>
        /// <param name="currentGroupId">The current group id.</param>
        /// <returns></returns>
        IEnumerable<Group> GetNotAssignedGroupsWithCurrentGroup(int disciplineId, int currentGroupId);

        #endregion
    }
}
