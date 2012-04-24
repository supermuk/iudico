using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;

namespace IUDICO.DisciplineManagement.Models.Storage
{
    public class CachedDisciplineStorage: IDisciplineStorage
    {
        private readonly IDisciplineStorage _storage;
        private readonly ICacheProvider _cacheProvider;
        private readonly object lockObject = new object();

        public CachedDisciplineStorage(IDisciplineStorage storage, ICacheProvider cachePrvoider)
        {
            _storage = storage;
            _cacheProvider = cachePrvoider;
        }

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

        public IList<Curriculum> GetCurriculums(Func<Curriculum, bool> predicate)
        {
            return _storage.GetCurriculums(predicate);
        }

        public IList<Discipline> GetDisciplines()
        {
            return _cacheProvider.Get("disciplines", @lockObject,
                                                               () => _storage.GetDisciplines(), DateTime.Now.AddDays(1),
                                                               "disciplines");
        }

        public IList<Discipline> GetDisciplines(Func<Discipline, bool> predicate)
        {
            return _storage.GetDisciplines().Where(predicate).ToList();
        }

        public IList<Discipline> GetDisciplines(IEnumerable<int> ids)
        {
            return _storage.GetDisciplines(ids);
        }

        public IList<Discipline> GetDisciplines(User user)
        {
            return _storage.GetDisciplines(user);
        }

        public Discipline GetDiscipline(int id)
        {
            return _cacheProvider.Get("discipline-" + id, @lockObject, () => _storage.GetDiscipline(id), DateTime.Now.AddDays(1), "discipline-" + id);
        }

        public IList<Discipline> GetDisciplinesByGroupId(int groupId)
        {
            return _cacheProvider.Get("disciplines-" + groupId, @lockObject,
                                                               () => _storage.GetDisciplines(), DateTime.Now.AddDays(1),
                                                               "disciplines");
        }

        public int AddDiscipline(Discipline discipline)
        {
            var id = _storage.AddDiscipline(discipline);

            _cacheProvider.Invalidate("disciplines");

            return id;
        }

        public void UpdateDiscipline(Discipline discipline)
        {
            _storage.UpdateDiscipline(discipline);

            _cacheProvider.Invalidate("disciplines", "discipline-" + discipline.Id);
        }

        public void DeleteDiscipline(int id)
        {
            _storage.DeleteDiscipline(id);

            _cacheProvider.Invalidate("disciplines", "discipline-" + id);
        }

        public void DeleteDisciplines(IEnumerable<int> ids)
        {
            _storage.DeleteDisciplines(ids);

            _cacheProvider.Invalidate("disciplines");
            _cacheProvider.Invalidate(ids.Select(id => "discipline-" + id).ToArray());
        }

        public IList<ShareUser> GetDisciplineSharedUsers(int disciplineId)
        {
            return _cacheProvider.Get("discipline-sharedusers-" + disciplineId, @lockObject,
                                      () => _storage.GetDisciplineSharedUsers(disciplineId), DateTime.Now.AddDays(1), "discipline-sharedusers-" + disciplineId, "discpline-" + disciplineId, "users");
        }

        public IList<ShareUser> GetDisciplineNotSharedUsers(int disciplineId)
        {
            return _cacheProvider.Get("discipline-notsharedusers-" + disciplineId, @lockObject,
                                      () => _storage.GetDisciplineNotSharedUsers(disciplineId), DateTime.Now.AddDays(1), "discipline-sharedusers-" + disciplineId, "discpline-" + disciplineId, "users");
        }

        public void UpdateDisciplineSharing(int disciplineId, IEnumerable<Guid> sharewith)
        {
            _storage.UpdateDisciplineSharing(disciplineId, sharewith);

            _cacheProvider.Invalidate("discipline-sharedusers", "discipline-sharedusers-" + disciplineId);
        }

        public Chapter GetChapter(int id)
        {
            return _cacheProvider.Get("chapter-" + id, @lockObject, () => _storage.GetChapter(id),
                                      DateTime.Now.AddDays(1), "chapter-" + id);
        }

        public IList<Chapter> GetChapters(Func<Chapter, bool> predicate)
        {
            return _storage.GetChapters(predicate);
        }

        public IList<Chapter> GetChapters(IEnumerable<int> ids)
        {
            return _storage.GetChapters(ids);
        }

        public int AddChapter(Chapter chapter)
        {
            var id = _storage.AddChapter(chapter);
            
            _cacheProvider.Invalidate("chapters");

            return id;
        }

        public void UpdateChapter(Chapter chapter)
        {
            _storage.UpdateChapter(chapter);

            _cacheProvider.Invalidate("chaprters", "chapter-" + chapter.Id);
        }

        public void DeleteChapter(int id)
        {
            _storage.DeleteChapter(id);

            _cacheProvider.Invalidate("chaprters", "chapter-" + id);
        }

        public void DeleteChapters(IEnumerable<int> ids)
        {
            _storage.DeleteChapters(ids);

            _cacheProvider.Invalidate("chaprters");
            _cacheProvider.Invalidate(ids.Select(id => "chapter-" + id).ToArray());
        }

        public Topic GetTopic(int id)
        {
            return _cacheProvider.Get("topic-" + id, @lockObject, () => _storage.GetTopic(id),
                                      DateTime.Now.AddDays(1), "topic-" + id, "topics-sort");
        }

        public IList<Topic> GetTopics(Func<Topic, bool> predicate)
        {
            return _storage.GetTopics(predicate);
        }

        public IList<Topic> GetTopics(IEnumerable<int> ids)
        {
            return _storage.GetTopics(ids);
        }

        public IList<Topic> GetTopicsByDisciplineId(int disciplineId)
        {
            return _cacheProvider.Get("topics-discipline-" + disciplineId, @lockObject,
                                      () => _storage.GetTopicsByDisciplineId(disciplineId), DateTime.Now.AddDays(1), "topics", "topics-sort");
        }

        public IList<Topic> GetTopicsByGroupId(int groupId)
        {
            return _cacheProvider.Get("topics-group-" + groupId, @lockObject,
                                      () => _storage.GetTopicsByDisciplineId(groupId), DateTime.Now.AddDays(1), "topics", "topics-sort");
        }

        public IList<Topic> GetTopicsByCourseId(int courseId)
        {
            return _cacheProvider.Get("topics-course-" + courseId, @lockObject,
                                      () => _storage.GetTopicsByCourseId(courseId), DateTime.Now.AddDays(1), "topics", "topics-sort");
        }

        public IList<Topic> GetTopicsOwnedByUser(User owner)
        {
            return _cacheProvider.Get("topics-user-" + owner.Username, @lockObject,
                                      () => _storage.GetTopicsOwnedByUser(owner), DateTime.Now.AddDays(1), "topics", "topics-sort");
        }

        public IList<Group> GetGroupsAssignedToTopic(int topicId)
        {
            return _storage.GetGroupsAssignedToTopic(topicId);
        }

        public int AddTopic(Topic topic)
        {
            var id = _storage.AddTopic(topic);

            _cacheProvider.Invalidate("topics");

            return id;
        }

        public void UpdateTopic(Topic topic)
        {
            _storage.UpdateTopic(topic);

            _cacheProvider.Invalidate("topics", "topic-" + topic.Id);
        }

        public void DeleteTopic(int id)
        {
            _storage.DeleteTopic(id);

            _cacheProvider.Invalidate("topics", "topic-" + id);
        }

        public void DeleteTopics(IEnumerable<int> ids)
        {
            _storage.DeleteTopics(ids);

            _cacheProvider.Invalidate("topics");
            _cacheProvider.Invalidate(ids.Select(id => "topic-" + id).ToArray());
        }

        public Topic TopicUp(int topicId)
        {
            var topic = _storage.TopicUp(topicId);

            _cacheProvider.Invalidate("topics", "topics-sort");

            return topic;
        }

        public Topic TopicDown(int topicId)
        {
            var topic = _storage.TopicDown(topicId);

            _cacheProvider.Invalidate("topics", "topics-sort");

            return topic;
        }

        public TopicType GetTopicType(int id)
        {
            return GetTopicTypes().SingleOrDefault(t => t.Id == id);
        }

        public IList<TopicType> GetTopicTypes()
        {
            return _cacheProvider.Get("topictypes", @lockObject, () => _storage.GetTopicTypes(), DateTime.Now.AddDays(1),
                                      "topictypes");
        }

        public IList<TopicType> GetTheoryTopicTypes()
        {
            return GetTopicTypes()
                .Where(item => item.ToTopicTypeEnum() == TopicTypeEnum.Theory).ToList();
        }

        public IList<TopicType> GetTestTopicTypes()
        {
            return GetTopicTypes()
                .Where(item => item.ToTopicTypeEnum() == TopicTypeEnum.Test ||
                item.ToTopicTypeEnum() == TopicTypeEnum.TestWithoutCourse).ToList();
        }
    }
}