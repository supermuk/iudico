using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;

namespace IUDICO.DisciplineManagement.Models.Storage
{
    public class CachedDisciplineStorage : IDisciplineStorage
    {
        private readonly IDisciplineStorage storage;
        private readonly ICacheProvider cacheProvider;
        private readonly object lockObject = new object();

        public CachedDisciplineStorage(IDisciplineStorage storage, ICacheProvider cachePrvoider)
        {
            this.storage = storage;
            this.cacheProvider = cachePrvoider;
        }

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

        public IList<Group> GetGroups()
        {
            return this.storage.GetGroups();
        }

        public IList<Curriculum> GetCurriculums(Func<Curriculum, bool> predicate)
        {
            return this.storage.GetCurriculums(predicate);
        }

        public void MakeDisciplinesInvalid(int courseId)
        {
            this.storage.MakeDisciplinesInvalid(courseId);

            var topics = this.storage.GetTopicsByCourseId(courseId);
            var chapters = topics.Select(item => this.storage.GetChapter(item.ChapterRef));
            var disciplineIds = chapters.Select(item => item.DisciplineRef).Distinct();

            this.cacheProvider.Invalidate("disciplines");
            this.cacheProvider.Invalidate(disciplineIds.Select(id => "discipline-" + id).ToArray());
        }

        public IList<Discipline> GetDisciplines()
        {
            return this.cacheProvider.Get(
                "disciplines", this.lockObject, () => this.storage.GetDisciplines(), DateTime.Now.AddDays(1), "disciplines");
        }

        public IList<Discipline> GetDisciplines(Func<Discipline, bool> predicate)
        {
            return this.storage.GetDisciplines().Where(predicate).ToList();
        }

        public IList<Discipline> GetDisciplines(IEnumerable<int> ids)
        {
            return this.storage.GetDisciplines(ids);
        }

        public IList<Discipline> GetDisciplines(User user)
        {
            return this.storage.GetDisciplines(user);
        }

        public Discipline GetDiscipline(int id)
        {
            return this.cacheProvider.Get("discipline-" + id, this.lockObject, () => this.storage.GetDiscipline(id), DateTime.Now.AddDays(1), "discipline-" + id);
        }

        public IList<Discipline> GetDisciplinesByGroupId(int groupId)
        {
            return this.cacheProvider.Get(
                "disciplines-" + groupId,
                this.lockObject,
                () => this.storage.GetDisciplines(),
                DateTime.Now.AddDays(1),
                "disciplines");
        }

        public int AddDiscipline(Discipline discipline)
        {
            var id = this.storage.AddDiscipline(discipline);

            this.cacheProvider.Invalidate("disciplines");

            return id;
        }

        public void UpdateDiscipline(Discipline discipline)
        {
            this.storage.UpdateDiscipline(discipline);

            this.cacheProvider.Invalidate("disciplines", "discipline-" + discipline.Id);
        }

        public void DeleteDiscipline(int id)
        {
            this.storage.DeleteDiscipline(id);

            this.cacheProvider.Invalidate("disciplines", "discipline-" + id);
        }

        public void DeleteDisciplines(IEnumerable<int> ids)
        {
            this.storage.DeleteDisciplines(ids);

            this.cacheProvider.Invalidate("disciplines");
            this.cacheProvider.Invalidate(ids.Select(id => "discipline-" + id).ToArray());
        }

        public IList<ShareUser> GetDisciplineSharedUsers(int disciplineId)
        {
            return this.cacheProvider.Get(
                "discipline-sharedusers-" + disciplineId,
                @lockObject,
                () => this.storage.GetDisciplineSharedUsers(disciplineId),
                DateTime.Now.AddDays(1),
                "discipline-sharedusers-" + disciplineId,
                "discpline-" + disciplineId,
                "users");
        }

        public IList<ShareUser> GetDisciplineNotSharedUsers(int disciplineId)
        {
            return this.cacheProvider.Get(
                "discipline-notsharedusers-" + disciplineId,
                @lockObject,
                () => this.storage.GetDisciplineNotSharedUsers(disciplineId),
                DateTime.Now.AddDays(1),
                "discipline-sharedusers-" + disciplineId,
                "discpline-" + disciplineId,
                "users");
        }

        public void UpdateDisciplineSharing(int disciplineId, IEnumerable<Guid> sharewith)
        {
            this.storage.UpdateDisciplineSharing(disciplineId, sharewith);

            this.cacheProvider.Invalidate("discipline-sharedusers", "discipline-sharedusers-" + disciplineId);
        }

        public Chapter GetChapter(int id)
        {
            return this.cacheProvider.Get(
                "chapter-" + id, this.lockObject, () => this.storage.GetChapter(id), DateTime.Now.AddDays(1), "chapter-" + id);
        }

        public IList<Chapter> GetChapters(Func<Chapter, bool> predicate)
        {
            return this.storage.GetChapters(predicate);
        }

        public IList<Chapter> GetChapters(IEnumerable<int> ids)
        {
            return this.storage.GetChapters(ids);
        }

        public int AddChapter(Chapter chapter)
        {
            var id = this.storage.AddChapter(chapter);
            
            this.cacheProvider.Invalidate("chapters");

            return id;
        }

        public void UpdateChapter(Chapter chapter)
        {
            this.storage.UpdateChapter(chapter);

            this.cacheProvider.Invalidate("chaprters", "chapter-" + chapter.Id);
        }

        public void DeleteChapter(int id)
        {
            this.storage.DeleteChapter(id);

            this.cacheProvider.Invalidate("chaprters", "chapter-" + id);
        }

        public void DeleteChapters(IEnumerable<int> ids)
        {
            this.storage.DeleteChapters(ids);

            this.cacheProvider.Invalidate("chaprters");
            this.cacheProvider.Invalidate(ids.Select(id => "chapter-" + id).ToArray());
        }

        public Topic GetTopic(int id)
        {
            return this.cacheProvider.Get(
                "topic-" + id,
                @lockObject,
                () => this.storage.GetTopic(id),
                DateTime.Now.AddDays(1),
                "topic-" + id,
                "topics-sort");
        }

        public IList<Topic> GetTopics(Func<Topic, bool> predicate)
        {
            return this.storage.GetTopics(predicate);
        }

        public IList<Topic> GetTopics(IEnumerable<int> ids)
        {
            return this.storage.GetTopics(ids);
        }

        public IList<Topic> GetTopicsByDisciplineId(int disciplineId)
        {
            return this.cacheProvider.Get(
                "topics-discipline-" + disciplineId,
                @lockObject,
                () => this.storage.GetTopicsByDisciplineId(disciplineId),
                DateTime.Now.AddDays(1),
                "topics",
                "topics-sort");
        }

        public IList<Topic> GetTopicsByGroupId(int groupId)
        {
            return this.cacheProvider.Get(
                "topics-group-" + groupId,
                @lockObject,
                () => this.storage.GetTopicsByDisciplineId(groupId),
                DateTime.Now.AddDays(1),
                "topics",
                "topics-sort");
        }

        public IList<Topic> GetTopicsByCourseId(int courseId)
        {
            return this.cacheProvider.Get(
                "topics-course-" + courseId,
                @lockObject,
                () => this.storage.GetTopicsByCourseId(courseId),
                DateTime.Now.AddDays(1),
                "topics",
                "topics-sort");
        }

        public IList<Topic> GetTopicsOwnedByUser(User owner)
        {
            return this.cacheProvider.Get(
                "topics-user-" + owner.Username,
                @lockObject,
                () => this.storage.GetTopicsOwnedByUser(owner),
                DateTime.Now.AddDays(1),
                "topics",
                "topics-sort");
        }

        public int AddTopic(Topic topic)
        {
            var id = this.storage.AddTopic(topic);

            this.cacheProvider.Invalidate("topics");

            return id;
        }

        public void UpdateTopic(Topic topic)
        {
            this.storage.UpdateTopic(topic);
            var chapter = this.storage.GetChapter(this.storage.GetTopic(topic.Id).ChapterRef);
            this.cacheProvider.Invalidate("disciplines", "discipline-" + chapter.DisciplineRef);
            
            this.cacheProvider.Invalidate("topics", "topic-" + topic.Id);
        }

        public void DeleteTopic(int id)
        {
            var chapter = this.storage.GetChapter(this.storage.GetTopic(id).ChapterRef);
            this.storage.DeleteTopic(id);            
            this.cacheProvider.Invalidate("disciplines", "discipline-" + chapter.DisciplineRef);

            this.cacheProvider.Invalidate("topics", "topic-" + id);
        }

        public void DeleteTopics(IEnumerable<int> ids)
        {
            this.storage.DeleteTopics(ids);

            this.cacheProvider.Invalidate("topics");
            this.cacheProvider.Invalidate(ids.Select(id => "topic-" + id).ToArray());
        }

        public Topic TopicUp(int topicId)
        {
            var topic = this.storage.TopicUp(topicId);

            this.cacheProvider.Invalidate("topics", "topics-sort");

            return topic;
        }

        public Topic TopicDown(int topicId)
        {
            var topic = this.storage.TopicDown(topicId);

            this.cacheProvider.Invalidate("topics", "topics-sort");

            return topic;
        }

        public TopicType GetTopicType(int id)
        {
            return this.GetTopicTypes().SingleOrDefault(t => t.Id == id);
        }

        public IList<TopicType> GetTopicTypes()
        {
            return this.cacheProvider.Get(
                "topictypes", this.lockObject, () => this.storage.GetTopicTypes(), DateTime.Now.AddDays(1), "topictypes");
        }

        public IList<TopicType> GetTheoryTopicTypes()
        {
            return this.GetTopicTypes()
                .Where(item => item.ToTopicTypeEnum() == TopicTypeEnum.Theory).ToList();
        }

        public IList<TopicType> GetTestTopicTypes()
        {
            return this.GetTopicTypes()
                .Where(item => item.ToTopicTypeEnum() == TopicTypeEnum.Test ||
                item.ToTopicTypeEnum() == TopicTypeEnum.TestWithoutCourse).ToList();
        }
    }
}