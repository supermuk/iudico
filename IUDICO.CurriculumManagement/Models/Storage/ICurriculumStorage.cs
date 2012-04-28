using System;
using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Shared.DisciplineManagement;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICurriculumStorage
    {
        #region External methods

        User GetCurrentUser();
        IList<Course> GetCourses();
        Course GetCourse(int id);
        Group GetGroup(int id);
        IList<Group> GetGroups();
        IList<Group> GetGroupsByUser(User user);
        Discipline GetDiscipline(int id);
        IList<Discipline> GetDisciplines(User user);
        Chapter GetChapter(int id);
        Topic GetTopic(int id);

        #endregion

        #region Curriculum methods

        Curriculum GetCurriculum(int curriculumId);
        IList<Curriculum> GetCurriculums(IEnumerable<int> ids);
        IList<Curriculum> GetCurriculums(Func<Curriculum, bool> predicate);
        IList<Curriculum> GetCurriculums();
        IList<Curriculum> GetCurriculums(User user);
        int AddCurriculum(Curriculum curriculum);
        void UpdateCurriculum(Curriculum curriculum);
        void DeleteCurriculum(int id);
        void DeleteCurriculums(IEnumerable<int> ids);
        // void MakeCurriculumsInvalid(int groupId);

        /// <summary>
        /// Gets the topic descriptions available for user for current date.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IList<TopicDescription> GetTopicDescriptions(User user);
        IEnumerable<TopicDescription> GetTopicDescriptionsByTopics(IEnumerable<Topic> topics, User user);

        #endregion

        #region CurriculumChapter methods

        CurriculumChapter GetCurriculumChapter(int id);

        IList<CurriculumChapter> GetCurriculumChapters(Func<CurriculumChapter, bool> predicate); 

        int AddCurriculumChapter(CurriculumChapter curriculumChapter);

        void UpdateCurriculumChapter(CurriculumChapter curriculumChapter);

        void DeleteCurriculumChapter(int id);

        void DeleteCurriculumChapters(IEnumerable<int> ids);

        #endregion

        #region CurriculumChapterTopic methods

        CurriculumChapterTopic GetCurriculumChapterTopic(int id);

        IList<CurriculumChapterTopic> GetCurriculumChapterTopics(Func<CurriculumChapterTopic, bool> predicate); 

        int AddCurriculumChapterTopic(CurriculumChapterTopic curriculumChapterTopic);

        void UpdateCurriculumChapterTopic(CurriculumChapterTopic curriculumChapterTopic);

        void DeleteCurriculumChapterTopic(int id);

        void DeleteCurriculumChapterTopics(IEnumerable<int> ids);

        bool CanPassCurriculumChapterTopic(User user, CurriculumChapterTopic topic, TopicTypeEnum topicType);

        IEnumerable<CurriculumChapterTopic> GetCurriculumChapterTopicsByCurriculumId(int curriculumId);
        
        #endregion
    }
}
