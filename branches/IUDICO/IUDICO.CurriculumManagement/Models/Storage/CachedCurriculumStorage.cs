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
    public class CachedCurriculumStorage: ICurriculumStorage
    {
        private readonly ICurriculumStorage _storage;
        private readonly ICacheProvider _cacheProvider;
        private readonly object lockObject = new object();

        public CachedCurriculumStorage(ICurriculumStorage storage, ICacheProvider cachePrvoider)
        {
            _storage = storage;
            _cacheProvider = cachePrvoider;
        }
        
        #region External methods

        public User GetCurrentUser()
        {
            return _storage.GetCurrentUser();
        }

        public IList<Course> GetCourses()
        {
            return _storage.GetCourses();
        }

        public Course GetCourse(int id)
        {
            return _storage.GetCourse(id);
        }

        public Group GetGroup(int id)
        {
            return _storage.GetGroup(id);
        }

        public IList<Group> GetGroups()
        {
            return _storage.GetGroups();
        }

        public IList<Group> GetGroupsByUser(User user)
        {
            return _storage.GetGroupsByUser(user);
        }

        public Discipline GetDiscipline(int id)
        {
            return _storage.GetDiscipline(id);
        }

        public IList<Discipline> GetDisciplines(User user)
        {
            return _storage.GetDisciplines(user);
        }

        public Chapter GetChapter(int id)
        {
            return _storage.GetChapter(id);
        }

        public Topic GetTopic(int id)
        {
            return _storage.GetTopic(id);
        }

        #endregion

        #region Curriculum methods

        public IList<Curriculum> GetCurriculums()
        {
            return _cacheProvider.Get<IList<Curriculum>>("curriculums", @lockObject, () => _storage.GetCurriculums(), DateTime.Now.AddDays(1), "curriculums");
        }

        public IList<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            return _cacheProvider.Get<IList<Curriculum>>("curriculums-" + string.Join("-", ids.ToArray()), @lockObject, () => _storage.GetCurriculums(ids), DateTime.Now.AddDays(1), "curriculums");
        }

        public IList<Curriculum> GetCurriculums(User user)
        {
            return _cacheProvider.Get<IList<Curriculum>>("curriculums-user-" + user.Id, @lockObject, () => _storage.GetCurriculums(user), DateTime.Now.AddDays(1), "curriculums");
        }

        public Curriculum GetCurriculum(int curriculumId)
        {
            return _cacheProvider.Get<Curriculum>("curriculum-" + curriculumId, lockObject, () => _storage.GetCurriculum(curriculumId), DateTime.Now.AddDays(1), "curriculum-" + curriculumId);
        }

        public IList<Curriculum> GetCurriculums(Func<Curriculum, bool> predicate)
        {
            return _storage.GetCurriculums(predicate);
        }

        public int AddCurriculum(Curriculum curriculum)
        {
            var id = _storage.AddCurriculum(curriculum);

            _cacheProvider.Invalidate("curriculums", "curriculum-" + id);

            return id;
        }

        public void UpdateCurriculum(Curriculum curriculum)
        {
            _storage.UpdateCurriculum(curriculum);

            _cacheProvider.Invalidate("curriculums", "curriculum-" + curriculum.Id);
        }

        public void DeleteCurriculum(int id)
        {
            _storage.DeleteCurriculum(id);

            _cacheProvider.Invalidate("curriculums", "curriculum-" + id);
        }

        public void DeleteCurriculums(IEnumerable<int> ids)
        {
            _storage.DeleteCurriculums(ids);

            _cacheProvider.Invalidate(ids.Select(i => "curriculum-" + ids).ToArray());
            _cacheProvider.Invalidate("curriculums");
        }

        public IList<TopicDescription> GetTopicDescriptions(User user)
        {
            return _storage.GetTopicDescriptions(user);
        }

        public IEnumerable<TopicDescription> GetTopicDescriptionsByTopics(IEnumerable<Topic> topics, User user)
        {
            return _storage.GetTopicDescriptionsByTopics(topics, user);
        }

        public CurriculumChapter GetCurriculumChapter(int id)
        {
            return _cacheProvider.Get<CurriculumChapter>("curriculumchapter-" + id, @lockObject, () => _storage.GetCurriculumChapter(id), DateTime.Now.AddDays(1), "curriculumchapter-" + id);
        }

        public IList<CurriculumChapter> GetCurriculumChapters(Func<CurriculumChapter, bool> predicate)
        {
            return _storage.GetCurriculumChapters(predicate);
        }

        public int AddCurriculumChapter(CurriculumChapter curriculumChapter)
        {
            var id = _storage.AddCurriculumChapter(curriculumChapter);

            _cacheProvider.Invalidate("curriculumchapter-" + id, "curriculumchapters");

            return id;
        }

        public void UpdateCurriculumChapter(CurriculumChapter curriculumChapter)
        {
            _storage.UpdateCurriculumChapter(curriculumChapter);

            _cacheProvider.Invalidate("curriculumchapter-" + curriculumChapter.Id, "curriculumchapters");
        }

        public void DeleteCurriculumChapter(int id)
        {
            _storage.DeleteCurriculumChapter(id);

            _cacheProvider.Invalidate("curriculumchapter-" + id, "curriculumchapters");
        }

        public void DeleteCurriculumChapters(IEnumerable<int> ids)
        {
            _storage.DeleteCurriculumChapters(ids);

            _cacheProvider.Invalidate(ids.Select(id => "curriculumchapter-" + id).ToArray());
            _cacheProvider.Invalidate("curriculumchapters");
        }

        public CurriculumChapterTopic GetCurriculumChapterTopic(int id)
        {
            return _cacheProvider.Get<CurriculumChapterTopic>("curriculumchaptertopic-" + id, @lockObject, () => _storage.GetCurriculumChapterTopic(id), DateTime.Now.AddDays(1), "curriculumchaptertopic-" + id);
        }

        public IList<CurriculumChapterTopic> GetCurriculumChapterTopics(Func<CurriculumChapterTopic, bool> predicate)
        {
            return _storage.GetCurriculumChapterTopics(predicate);
        }

        public int AddCurriculumChapterTopic(CurriculumChapterTopic curriculumChapterTopic)
        {
            var id = _storage.AddCurriculumChapterTopic(curriculumChapterTopic);

            _cacheProvider.Invalidate("curriculumchaptertopics");

            return id;
        }

        public void UpdateCurriculumChapterTopic(CurriculumChapterTopic curriculumChapterTopic)
        {
            _storage.UpdateCurriculumChapterTopic(curriculumChapterTopic);

            _cacheProvider.Invalidate("curriculumchaptertopics", "curriculumchaptertopic-" + curriculumChapterTopic.Id);
        }

        public void DeleteCurriculumChapterTopic(int id)
        {
            _storage.DeleteCurriculumChapterTopic(id);

            _cacheProvider.Invalidate("curriculumchaptertopics", "curriculumchaptertopic-" + id);
        }

        public void DeleteCurriculumChapterTopics(IEnumerable<int> ids)
        {
            _storage.DeleteCurriculumChapterTopics(ids);

            _cacheProvider.Invalidate(ids.Select(id => "curriculumchaptertopic-" + id).ToArray());
            _cacheProvider.Invalidate("curriculumchaptertopics");
        }

        public bool CanPassCurriculumChapterTopic(User user, CurriculumChapterTopic topic, TopicTypeEnum topicType)
        {
            return _storage.CanPassCurriculumChapterTopic(user, topic, topicType);
        }
        #endregion
    }
}