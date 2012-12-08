using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Shared.DisciplineManagement;

namespace IUDICO.DisciplineManagement.Models.Storage
{
    public class DatabaseDisciplineStorage : IDisciplineStorage
    {
        private readonly ILmsService lmsService;
        private readonly LinqLogger logger;

        protected virtual IDataContext GetDbContext()
        {
            var context = new DBDataContext();
            // context.DeferredLoadingEnabled = false;
#if DEBUG
            context.Log = this.logger;
#endif

            return context; // new DBDataContext();
        }

        public DatabaseDisciplineStorage(ILmsService lmsService, LinqLogger logger)
        {
            this.lmsService = lmsService;
            this.logger = logger;
        }

        public DatabaseDisciplineStorage(ILmsService lmsService)
        {
            this.lmsService = lmsService;
        }

        #region IStorageInterface Members

        #region External methods

        private IEnumerable<User> GetUsersAvailableForSharing()
        {
            var currentUser = this.GetCurrentUser();
            return this.lmsService.FindService<IUserService>()
                .GetUsers(user => user.Roles.Contains(Role.Teacher))
                .Where(user => user.Id != currentUser.Id)
                .ToList();
        }

        private IEnumerable<User> GetUsers(List<Guid> ids)
        {
            return this.lmsService.FindService<IUserService>().GetUsers(item => ids.Contains(item.Id)).ToList();
        }

        public virtual User GetCurrentUser()
        {
            return this.lmsService.FindService<IUserService>().GetCurrentUser();
        }

        public IList<Course> GetCourses()
        {
            return this.lmsService.FindService<ICourseService>().GetCourses(this.GetCurrentUser()).ToList();
        }

        public void Import(string path, string courseName)
        {
            this.lmsService.FindService<ICourseService>().Import(path, courseName, this.GetCurrentUser().Username);
        }

        public void Unlock(int id)
        {
            this.lmsService.FindService<ICourseService>().Unlock(id);
        }

        public string Export(int id)
        {
            return lmsService.FindService<ICourseService>().Export(id);
        }

        public Course GetCourse(int id)
        {
            return this.lmsService.FindService<ICourseService>().GetCourse(id);
        }

        public Group GetGroup(int id)
        {
            return this.lmsService.FindService<IUserService>().GetGroup(id);
        }

        public IList<Group> GetGroups()
        {
            return this.lmsService.FindService<IUserService>().GetGroups().ToList();
        }

        public IList<Curriculum> GetCurriculums(Func<Curriculum, bool> predicate)
        {
            return this.lmsService.FindService<ICurriculumService>().GetCurriculums(predicate);
        }

        #endregion

        #region Discipline methods

        // TODO: FatTony: get rid of private static members!
        private static Discipline GetDiscipline(IDataContext db, int id)
        {
            return db.Disciplines.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Discipline GetDiscipline(int id)
        {
            return GetDiscipline(this.GetDbContext(), id);
        }

        public IList<Discipline> GetDisciplines()
        {
            return GetDisciplines(item => true);
        }

        public IList<Discipline> GetDisciplines(Func<Discipline, bool> predicate)
        {
            return this.GetDbContext().Disciplines.Where(item => !item.IsDeleted).Where(predicate).ToList();
        }

        public IList<Discipline> GetDisciplines(User user)
        {
            var sharedDisciplines = this.GetDbContext().SharedDisciplines
                .Where(item => item.UserRef == this.GetCurrentUser().Id)
                .Select(item => item.Discipline)
                .Where(item => !item.IsDeleted);

            return GetDisciplines(item => item.Owner == user.Username)
                .Union(sharedDisciplines)
                .ToList();
        }

        public IList<Discipline> GetDisciplines(IEnumerable<int> ids)
        {
            return GetDisciplines(item => ids.Contains(item.Id));
        }

        public IList<Discipline> GetDisciplinesByGroupId(int groupId)
        {
            return this.GetCurriculums(c => c.UserGroupRef == groupId).Select(item => this.GetDiscipline(item.DisciplineRef)).ToList();
        }

        public int AddDiscipline(Discipline discipline)
        {
            var db = this.GetDbContext();

            discipline.Created = DateTime.Now;
            discipline.Updated = DateTime.Now;
            discipline.IsDeleted = false;
            discipline.IsValid = true;
            discipline.Owner = this.GetCurrentUser().Username;

            db.Disciplines.InsertOnSubmit(discipline);
            db.SubmitChanges();

            this.lmsService.Inform(DisciplineNotifications.DisciplineCreated, discipline);

            return discipline.Id;
        }

        public Discipline UpdateDiscipline(Discipline discipline)
        {
            var db = this.GetDbContext();
            var updatingDiscipline = GetDiscipline(db, discipline.Id);
            var oldDiscipline = new Discipline
            {
                Id = updatingDiscipline.Id
            };

            updatingDiscipline.Name = discipline.Name;
            updatingDiscipline.Updated = DateTime.Now;
            db.SubmitChanges();

            var data = new object[2];
            data[0] = oldDiscipline;
            data[1] = updatingDiscipline;
            this.lmsService.Inform(DisciplineNotifications.DisciplineEdited, data);

            return updatingDiscipline;
        }

        public void DeleteDiscipline(int id)
        {
            var db = this.GetDbContext();
            var discipline = GetDiscipline(db, id);

            // delete chapters
            var chapterIds = this.GetChapters(item => item.DisciplineRef == id).Select(item => item.Id);
            this.DeleteChapters(chapterIds);

            this.lmsService.Inform(DisciplineNotifications.DisciplineDeleting, discipline);

            discipline.IsDeleted = true;
            db.SubmitChanges();
            this.lmsService.Inform(DisciplineNotifications.DisciplineDeleted, discipline);
        }

        public void DeleteDisciplines(IEnumerable<int> ids)
        {
            ids.ForEach(this.DeleteDiscipline);
        }

        private bool IsDisciplineValid(Discipline discipline) {
            var chapters = this.GetChapters(item => item.DisciplineRef == discipline.Id);
            foreach (var chapter in chapters) {
                var topics = this.GetTopics(item => item.ChapterRef == chapter.Id);
                foreach (var topic in topics) {
                    if (topic.TheoryCourseRef == null && topic.TestCourseRef == null) {
                        return false;
                    }
                    else {
                        if (topic.TheoryCourseRef != null) {
                            if (this.GetCourse((int)topic.TheoryCourseRef).Deleted == true) {
                                return false;
                            }
                        }
                        else if ((int)topic.TestCourseRef != -1) {
                            if (this.GetCourse((int)topic.TestCourseRef).Deleted == true) {
                               return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void MakeDisciplinesInvalid(int courseId)
        {
            var db = this.GetDbContext();
            var topics = db.Topics.Where(item => !item.IsDeleted && (item.TestCourseRef == courseId || item.TheoryCourseRef == courseId));
            var chapters = topics.Select(item => db.Chapters.Single(chap => !chap.IsDeleted && chap.Id == item.ChapterRef));
            var disciplineIds = chapters.Select(item => item.DisciplineRef).Distinct();
            foreach (var disciplineId in disciplineIds) {
                var discipline = GetDiscipline(db, disciplineId);
                discipline.IsValid = false;
            }
            db.SubmitChanges();
            foreach (var disciplineId in disciplineIds) {
                this.lmsService.Inform(DisciplineNotifications.DisciplineIsValidChange, GetDiscipline(db, disciplineId));
            }
        }

        #endregion

        #region SharedDisciplines

        public IList<ShareUser> GetDisciplineSharedUsers(int disciplineId)
        {
            var userIds = this.GetDbContext().SharedDisciplines
                .Where(item => item.DisciplineRef == disciplineId)
                .Select(item => item.UserRef)
                .ToList();
            return this.GetUsers(userIds).ToShareUsers(true);
        }

        public IList<ShareUser> GetDisciplineNotSharedUsers(int disciplineId)
        {
            var allUsers = this.GetUsersAvailableForSharing();
            var sharedUserIds = this.GetDisciplineSharedUsers(disciplineId)
                .Select(item => item.Id)
                .ToList();
            return allUsers.Where(user => !sharedUserIds.Contains(user.Id)).ToShareUsers(false);
        }

        public void UpdateDisciplineSharing(int disciplineId, IEnumerable<Guid> sharewith)
        {
            var db = this.GetDbContext();

            // remove old values
            var sharedDisciplines = db.SharedDisciplines.Where(item => item.DisciplineRef == disciplineId);
            db.SharedDisciplines.DeleteAllOnSubmit(sharedDisciplines);
            db.SubmitChanges();

            // add new
            db.SharedDisciplines.InsertAllOnSubmit(
                sharewith.Select(id => new SharedDiscipline
                {
                    DisciplineRef = disciplineId,
                    UserRef = id
                }));
            db.SubmitChanges();
        }

        #endregion

        #region Chapter methods

        private static Chapter GetChapter(IDataContext db, int id)
        {
            return db.Chapters.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Chapter GetChapter(int id)
        {
            return GetChapter(this.GetDbContext(), id);
        }

        public IList<Chapter> GetChapters(Func<Chapter, bool> predicate)
        {
            return this.GetDbContext().Chapters.Where(item => !item.IsDeleted).Where(predicate).ToList();
        }

        public IList<Chapter> GetChapters(IEnumerable<int> ids)
        {
            return this.GetChapters(item => ids.Contains(item.Id));
        }

        public int AddChapter(Chapter chapter)
        {
            var db = this.GetDbContext();
            chapter.Created = DateTime.Now;
            chapter.Updated = DateTime.Now;
            chapter.IsDeleted = false;

            db.Chapters.InsertOnSubmit(chapter);
            db.SubmitChanges();

            this.lmsService.Inform(DisciplineNotifications.ChapterCreated, chapter);
            return chapter.Id;
        }

        public Chapter UpdateChapter(Chapter chapter)
        {
            var db = this.GetDbContext();
            Chapter oldChapter = GetChapter(db, chapter.Id);

            oldChapter.Name = chapter.Name;
            oldChapter.Updated = DateTime.Now;

            db.SubmitChanges();
            return oldChapter;
        }

        public void DeleteChapter(int id)
        {
            var db = this.GetDbContext();
            var chapter = GetChapter(db, id);

            // delete corresponding topics
            var topicIds = GetTopics(item => item.ChapterRef == id).Select(item => item.Id);
            this.DeleteTopics(topicIds);

            this.lmsService.Inform(DisciplineNotifications.ChapterDeleting, chapter);
            chapter.IsDeleted = true;
            db.SubmitChanges();
        }

        public void DeleteChapters(IEnumerable<int> ids)
        {
            ids.ForEach(this.DeleteChapter);
        }

        #endregion

        #region Topic methods

        private static Topic GetTopic(IDataContext db, int id)
        {
            return db.Topics.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Topic GetTopic(int id)
        {
            return GetTopic(this.GetDbContext(), id);
        }

        public IList<Topic> GetTopics()
        {
            return this.GetDbContext().Topics.Where(item => !item.IsDeleted).OrderBy(item => item.SortOrder).ToList();
        }

        public IList<Topic> GetTopics(IEnumerable<int> ids)
        {
            return GetTopics(this.GetDbContext(), item => ids.Contains(item.Id));
        }

        private static IList<Topic> GetTopics(IDataContext db, Func<Topic, bool> predicate)
        {
            return db.Topics.Where(item => !item.IsDeleted).Where(predicate).OrderBy(item => item.SortOrder).ToList();
        }

        public IList<Topic> GetTopics(Func<Topic, bool> predicate)
        {
            return GetTopics(this.GetDbContext(), predicate).ToList();
        }

        public IList<Topic> GetTopicsByDisciplineId(int disciplineId)
        {
            return this.GetChapters(item => item.DisciplineRef == disciplineId).SelectMany(item => this.GetTopics(item2 => item2.ChapterRef == item.Id)).ToList();
        }

        public IList<Topic> GetTopicsByGroupId(int groupId)
        {
            return this.GetDisciplinesByGroupId(groupId).SelectMany(item => this.GetTopicsByDisciplineId(item.Id)).ToList();
        }

        public IList<Topic> GetTopicsOwnedByUser(User owner)
        {
            return GetDisciplines(owner).SelectMany(item => this.GetTopicsByDisciplineId(item.Id)).ToList();
        }

        public IList<Topic> GetTopicsByCourseId(int courseId)
        {
            return this.GetDbContext().Topics.Where(item => (item.TestCourseRef == courseId || item.TheoryCourseRef == courseId) && !item.IsDeleted).ToList();
        }

        public int AddTopic(Topic topic)
        {
            var db = this.GetDbContext();

            topic.Created = DateTime.Now;
            topic.Updated = DateTime.Now;
            topic.IsDeleted = false;
            db.Topics.InsertOnSubmit(topic);
            db.SubmitChanges();

            topic.SortOrder = topic.Id;
            this.UpdateTopic(topic);

            this.lmsService.Inform(DisciplineNotifications.TopicCreated, topic);
            return topic.Id;
        }

        public Topic UpdateTopic(Topic topic)
        {
            var db = this.GetDbContext();
            var data = new object[2];
            var updatingTopic = GetTopic(db, topic.Id);
            var oldTopic = new Topic
            {
                Id = updatingTopic.Id
            };

            updatingTopic.Name = topic.Name;
            updatingTopic.SortOrder = topic.SortOrder;
            updatingTopic.TestCourseRef = topic.TestCourseRef;
            updatingTopic.TheoryCourseRef = topic.TheoryCourseRef;
            updatingTopic.TestTopicTypeRef = topic.TestTopicTypeRef;
            updatingTopic.TheoryTopicTypeRef = topic.TheoryTopicTypeRef;
            updatingTopic.Updated = DateTime.Now;
            db.SubmitChanges();

            var discipline = GetDiscipline(db, GetChapter(db, updatingTopic.ChapterRef).DisciplineRef);
            discipline.IsValid = this.IsDisciplineValid(discipline);
            db.SubmitChanges();

            this.lmsService.Inform(DisciplineNotifications.DisciplineIsValidChange, discipline);
            data[0] = oldTopic;
            data[1] = updatingTopic;
            this.lmsService.Inform(DisciplineNotifications.TopicEdited, data);

            return updatingTopic;
        }

        public void DeleteTopic(int id)
        {
            var db = this.GetDbContext();
            var topic = GetTopic(db, id);

            this.lmsService.Inform(DisciplineNotifications.TopicDeleting, topic);

            topic.IsDeleted = true;
            db.SubmitChanges();

            var discipline = GetDiscipline(db, GetChapter(db, topic.ChapterRef).DisciplineRef);
            discipline.IsValid = this.IsDisciplineValid(discipline);
            db.SubmitChanges();
            this.lmsService.Inform(DisciplineNotifications.DisciplineIsValidChange, discipline);

            this.lmsService.Inform(DisciplineNotifications.TopicDeleted, topic);
        }

        public void DeleteTopics(IEnumerable<int> ids)
        {
            ids.ForEach(this.DeleteTopic);
        }

        public Topic TopicUp(int topicId)
        {
            var db = this.GetDbContext();
            var topic = GetTopic(db, topicId);
            IList<Topic> topics = GetTopics(db, item => item.ChapterRef == topic.ChapterRef).ToList();

            int index = topics.IndexOf(topic);
            if (index != -1 && index != 0)
            {
                int temp = topics[index - 1].SortOrder;
                topics[index - 1].SortOrder = topic.SortOrder;
                topic.SortOrder = temp;

                db.SubmitChanges();
            }

            return topic;
        }

        public Topic TopicDown(int topicId)
        {
            var db = this.GetDbContext();
            var topic = GetTopic(db, topicId);
            IList<Topic> topics = GetTopics(db, item => item.ChapterRef == topic.ChapterRef).ToList();

            int index = topics.IndexOf(topic);
            if (index != -1 && index != topics.Count - 1)
            {
                int temp = topics[index + 1].SortOrder;
                topics[index + 1].SortOrder = topic.SortOrder;
                topic.SortOrder = temp;

                db.SubmitChanges();
            }

            return topic;
        }

        #endregion

        #region TopicType methods

        public TopicType GetTopicType(int id)
        {
            return this.GetDbContext().TopicTypes.SingleOrDefault(item => item.Id == id);
        }

        public IList<TopicType> GetTopicTypes()
        {
            return this.GetDbContext().TopicTypes.ToList();
        }

        public IList<TopicType> GetTheoryTopicTypes()
        {
            return this.GetDbContext().TopicTypes.ToList()
                .Where(item => item.ToTopicTypeEnum() == TopicTypeEnum.Theory).ToList();
        }

        public IList<TopicType> GetTestTopicTypes()
        {
            return this.GetDbContext().TopicTypes.ToList()
                .Where(item => item.ToTopicTypeEnum() == TopicTypeEnum.Test ||
                item.ToTopicTypeEnum() == TopicTypeEnum.TestWithoutCourse).ToList();
        }

        #endregion

        #endregion
    }
}