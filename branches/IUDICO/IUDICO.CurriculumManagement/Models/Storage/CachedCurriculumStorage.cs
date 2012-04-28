using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Shared.DisciplineManagement;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    public class CachedCurriculumStorage : ICurriculumStorage
    {
        private readonly ICurriculumStorage storage;
        private readonly ICacheProvider cacheProvider;
        private readonly object lockObject = new object();

        public CachedCurriculumStorage(ICurriculumStorage storage, ICacheProvider cachePrvoider)
        {
            this.storage = storage;
            this.cacheProvider = cachePrvoider;
        }
        
        #region External methods

        public User GetCurrentUser()
        {
            return this.storage.GetCurrentUser();
        }

        public IList<Course> GetCourses()
        {
            return this.storage.GetCourses();
        }

        public Course GetCourse(int id)
        {
            return this.storage.GetCourse(id);
        }

        public Group GetGroup(int id)
        {
            return this.storage.GetGroup(id);
        }

        public IList<Group> GetGroups()
        {
            return this.storage.GetGroups();
        }

        public IList<Group> GetGroupsByUser(User user)
        {
            return this.storage.GetGroupsByUser(user);
        }

        public Discipline GetDiscipline(int id)
        {
            return this.storage.GetDiscipline(id);
        }

        public IList<Discipline> GetDisciplines(User user)
        {
            return this.storage.GetDisciplines(user);
        }

        public Chapter GetChapter(int id)
        {
            return this.storage.GetChapter(id);
        }

        public Topic GetTopic(int id)
        {
            return this.storage.GetTopic(id);
        }

        #endregion

        #region Curriculum methods

        public IList<Curriculum> GetCurriculums()
        {
            return this.cacheProvider.Get<IList<Curriculum>>("curriculums", @lockObject, () => this.storage.GetCurriculums(), DateTime.Now.AddDays(1), "curriculums");
        }

        public IList<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            return this.cacheProvider.Get<IList<Curriculum>>("curriculums-" + string.Join("-", ids.ToArray()), @lockObject, () => this.storage.GetCurriculums(ids), DateTime.Now.AddDays(1), "curriculums");
        }

        public IList<Curriculum> GetCurriculums(User user)
        {
            return this.cacheProvider.Get<IList<Curriculum>>("curriculums-user-" + user.Id, @lockObject, () => this.storage.GetCurriculums(user), DateTime.Now.AddDays(1), "curriculums");
        }

        public Curriculum GetCurriculum(int curriculumId)
        {
            return this.cacheProvider.Get<Curriculum>("curriculum-" + curriculumId, this.lockObject, () => this.storage.GetCurriculum(curriculumId), DateTime.Now.AddDays(1), "curriculum-" + curriculumId);
        }

        public IList<Curriculum> GetCurriculums(Func<Curriculum, bool> predicate)
        {
            return this.storage.GetCurriculums(predicate);
        }

        public int AddCurriculum(Curriculum curriculum)
        {
            var id = this.storage.AddCurriculum(curriculum);

            this.cacheProvider.Invalidate("curriculums", "curriculum-" + id);

            return id;
        }

        public void UpdateCurriculum(Curriculum curriculum)
        {
            this.storage.UpdateCurriculum(curriculum);

            this.cacheProvider.Invalidate("curriculums", "curriculum-" + curriculum.Id);
        }

        public void DeleteCurriculum(int id)
        {
            this.storage.DeleteCurriculum(id);

            this.cacheProvider.Invalidate("curriculums", "curriculum-" + id);
        }

        public void DeleteCurriculums(IEnumerable<int> ids)
        {
            this.storage.DeleteCurriculums(ids);

            this.cacheProvider.Invalidate(ids.Select(i => "curriculum-" + ids).ToArray());
            this.cacheProvider.Invalidate("curriculums");
        }

        public IList<TopicDescription> GetTopicDescriptions(User user)
        {
            return this.storage.GetTopicDescriptions(user);
        }

        public IEnumerable<TopicDescription> GetTopicDescriptionsByTopics(IEnumerable<Topic> topics, User user)
        {
            return this.storage.GetTopicDescriptionsByTopics(topics, user);
        }

        public CurriculumChapter GetCurriculumChapter(int id)
        {
            return this.cacheProvider.Get<CurriculumChapter>("curriculumchapter-" + id, @lockObject, () => this.storage.GetCurriculumChapter(id), DateTime.Now.AddDays(1), "curriculumchapter-" + id);
        }

        public IList<CurriculumChapter> GetCurriculumChapters(Func<CurriculumChapter, bool> predicate)
        {
            return this.storage.GetCurriculumChapters(predicate);
        }

        public int AddCurriculumChapter(CurriculumChapter curriculumChapter)
        {
            var id = this.storage.AddCurriculumChapter(curriculumChapter);

            this.cacheProvider.Invalidate("curriculumchapter-" + id, "curriculumchapters");

            return id;
        }

        public void UpdateCurriculumChapter(CurriculumChapter curriculumChapter)
        {
            this.storage.UpdateCurriculumChapter(curriculumChapter);

            this.cacheProvider.Invalidate("curriculumchapter-" + curriculumChapter.Id, "curriculumchapters");
        }

        public void DeleteCurriculumChapter(int id)
        {
            this.storage.DeleteCurriculumChapter(id);

            this.cacheProvider.Invalidate("curriculumchapter-" + id, "curriculumchapters");
        }

        public void DeleteCurriculumChapters(IEnumerable<int> ids)
        {
            this.storage.DeleteCurriculumChapters(ids);

            this.cacheProvider.Invalidate(ids.Select(id => "curriculumchapter-" + id).ToArray());
            this.cacheProvider.Invalidate("curriculumchapters");
        }

        public CurriculumChapterTopic GetCurriculumChapterTopic(int id)
        {
            return this.cacheProvider.Get<CurriculumChapterTopic>("curriculumchaptertopic-" + id, @lockObject, () => this.storage.GetCurriculumChapterTopic(id), DateTime.Now.AddDays(1), "curriculumchaptertopic-" + id);
        }

        public IList<CurriculumChapterTopic> GetCurriculumChapterTopics(Func<CurriculumChapterTopic, bool> predicate)
        {
            return this.storage.GetCurriculumChapterTopics(predicate);
        }

        public int AddCurriculumChapterTopic(CurriculumChapterTopic curriculumChapterTopic)
        {
            var id = this.storage.AddCurriculumChapterTopic(curriculumChapterTopic);

            this.cacheProvider.Invalidate("curriculumchaptertopics");

            return id;
        }

        public void UpdateCurriculumChapterTopic(CurriculumChapterTopic curriculumChapterTopic)
        {
            this.storage.UpdateCurriculumChapterTopic(curriculumChapterTopic);

            this.cacheProvider.Invalidate("curriculumchaptertopics", "curriculumchaptertopic-" + curriculumChapterTopic.Id);
        }

        public void DeleteCurriculumChapterTopic(int id)
        {
            this.storage.DeleteCurriculumChapterTopic(id);

            this.cacheProvider.Invalidate("curriculumchaptertopics", "curriculumchaptertopic-" + id);
        }

        public void DeleteCurriculumChapterTopics(IEnumerable<int> ids)
        {
            this.storage.DeleteCurriculumChapterTopics(ids);

            this.cacheProvider.Invalidate(ids.Select(id => "curriculumchaptertopic-" + id).ToArray());
            this.cacheProvider.Invalidate("curriculumchaptertopics");
        }

        public bool CanPassCurriculumChapterTopic(User user, CurriculumChapterTopic topic, TopicTypeEnum topicType)
        {
            return this.storage.CanPassCurriculumChapterTopic(user, topic, topicType);
        }

        public IEnumerable<CurriculumChapterTopic> GetCurriculumChapterTopicsByCurriculumId(int curriculumId)
        {
            return this.storage.GetCurriculumChapterTopicsByCurriculumId(curriculumId);
        }

        #endregion
    }
}