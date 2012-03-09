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
        private readonly ILmsService _lmsService;

        protected virtual IDataContext GetDbContext()
        {
            return new DBDataContext();
        }

        public DatabaseDisciplineStorage(ILmsService lmsService)
        {
            _lmsService = lmsService;
        }

        #region IStorageInterface Members

        #region External methods

        public User GetCurrentUser()
        {
            return _lmsService.FindService<IUserService>().GetCurrentUser();
        }

        public IList<Course> GetCourses()
        {
            return _lmsService.FindService<ICourseService>().GetCourses(GetCurrentUser()).ToList();
        }

        public Course GetCourse(int id)
        {
            return _lmsService.FindService<ICourseService>().GetCourse(id);
        }

        public Group GetGroup(int id)
        {
            return _lmsService.FindService<IUserService>().GetGroup(id);
        }

        public IList<Group> GetGroups()
        {
            return _lmsService.FindService<IUserService>().GetGroups().ToList();
        }

        public IList<Group> GetGroupsByUser(User user)
        {
            return _lmsService.FindService<IUserService>().GetGroupsByUser(user).ToList();
        }

        public IList<Curriculum> GetCurriculumsByGroupId(int groupId)
        {
            return _lmsService.FindService<ICurriculumService>().GetCurriculumsByGroupId(groupId);
        }


        #endregion

        #region Discipline methods

        private static Discipline GetDiscipline(IDataContext db, int id)
        {
            return db.Disciplines.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Discipline GetDiscipline(int id)
        {
            return GetDiscipline(GetDbContext(), id);
        }

        public IList<Discipline> GetDisciplines(Func<Discipline, bool> predicate)
        {
            return GetDbContext().Disciplines.Where(item => !item.IsDeleted).Where(predicate).ToList();
        }

        //public IEnumerable<Discipline> GetDisciplines()
        //{
        //    return GetDbContext().Disciplines.Where(item => !item.IsDeleted).ToList();
        //}

        //public IEnumerable<Discipline> GetDisciplines(User owner)
        //{
        //    return GetDbContext().Disciplines.Where(item => !item.IsDeleted && item.Owner == owner.Username).ToList();
        //}

        public IList<Discipline> GetDisciplines(IEnumerable<int> ids)
        {
            return GetDisciplines(item => ids.Contains(item.Id));
        }

        public IList<Discipline> GetDisciplinesByGroupId(int groupId)
        {
            return GetCurriculumsByGroupId(groupId).Select(item => GetDiscipline(item.DisciplineRef)).ToList();
        }

        //TODO:what the fuckin method?
        public IList<Discipline> GetDisciplinesWithTopicsOwnedByUser(User user)
        {
            //IEnumerable<int> courseIds = GetCoursesOwnedByUser(user)
            //    .Select(item => item.Id)
            //    .ToList();
            //return GetDisciplines(user) //?
            //    .Where(item => GetTopicsByDisciplineId(item.Id)
            //                 .Any(topic => courseIds.Contains(topic.CourseRef ?? Constants.NoCourseId)))
            //                 .ToList();

            return null;
        }

        public int AddDiscipline(Discipline discipline)
        {
            var db = GetDbContext();

            discipline.Created = DateTime.Now;
            discipline.Updated = DateTime.Now;
            discipline.IsDeleted = false;
            discipline.IsValid = true;
            discipline.Owner = GetCurrentUser().Username;

            db.Disciplines.InsertOnSubmit(discipline);
            db.SubmitChanges();

            _lmsService.Inform(DisciplineNotifications.DisciplineCreated, discipline);

            return discipline.Id;
        }

        public void UpdateDiscipline(Discipline discipline)
        {
            var db = GetDbContext();
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
            _lmsService.Inform(DisciplineNotifications.DisciplineEdited, data);
        }

        public void DeleteDiscipline(int id)
        {
            var db = GetDbContext();
            var discipline = GetDiscipline(db, id);

            //delete chapters
            var chapterIds = GetChapters(item => item.DisciplineRef == id).Select(item => item.Id);
            DeleteChapters(chapterIds);

            _lmsService.Inform(DisciplineNotifications.DisciplineDeleting, discipline);

            discipline.IsDeleted = true;
            db.SubmitChanges();
            _lmsService.Inform(DisciplineNotifications.DisciplineDeleted, discipline);
        }

        public void DeleteDisciplines(IEnumerable<int> ids)
        {
            ids.ForEach(DeleteDiscipline);
        }

        //public void MakeDisciplineInvalid(int courseId)
        //{
        //    var db = GetDbContext();
        //    var topicIds = GetTopicsByCourseId(courseId).Select(item => item.Id);
        //    var disciplines = db.Disciplines.Where(item => topicIds.Contains(item.Id) && !item.IsDeleted);
        //    foreach (Discipline discipline in disciplines)
        //    {
        //        discipline.IsValid = false;
        //    }
        //    db.SubmitChanges();
        //}

        #endregion

        #region Chapter methods

        private static Chapter GetChapter(IDataContext db, int id)
        {
            return db.Chapters.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Chapter GetChapter(int id)
        {
            return GetChapter(GetDbContext(), id);
        }

        public IList<Chapter> GetChapters(Func<Chapter, bool> predicate)
        {
            return GetDbContext().Chapters.Where(item => !item.IsDeleted).Where(predicate).ToList();
        }

        //public IList<Chapter> GetChapters(int disciplineId)
        //{
        //    return GetDbContext().Chapters.Where(item => item.DisciplineRef == disciplineId && !item.IsDeleted).ToList();
        //}

        public IList<Chapter> GetChapters(IEnumerable<int> ids)
        {
            return GetChapters(item => ids.Contains(item.Id));
        }

        public int AddChapter(Chapter chapter)
        {
            var db = GetDbContext();
            chapter.Created = DateTime.Now;
            chapter.Updated = DateTime.Now;
            chapter.IsDeleted = false;

            db.Chapters.InsertOnSubmit(chapter);
            db.SubmitChanges();

            _lmsService.Inform(DisciplineNotifications.ChapterCreated, chapter);
            return chapter.Id;
        }

        public void UpdateChapter(Chapter chapter)
        {
            var db = GetDbContext();
            Chapter oldChapter = GetChapter(db, chapter.Id);

            oldChapter.Name = chapter.Name;
            oldChapter.Updated = DateTime.Now;

            db.SubmitChanges();
        }

        public void DeleteChapter(int id)
        {
            var db = GetDbContext();
            var chapter = GetChapter(db, id);

            //delete corresponding topics
            var topicIds = GetTopics(item => item.ChapterRef == id).Select(item => item.Id);
            DeleteTopics(topicIds);

            _lmsService.Inform(DisciplineNotifications.ChapterDeleting, chapter);
            chapter.IsDeleted = true;
            db.SubmitChanges();
        }

        public void DeleteChapters(IEnumerable<int> ids)
        {
            ids.ForEach(DeleteChapter);
        }

        #endregion

        #region Topic methods

        private static Topic GetTopic(IDataContext db, int id)
        {
            return db.Topics.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Topic GetTopic(int id)
        {
            return GetTopic(GetDbContext(), id);
        }

        public IList<Topic> GetTopics()
        {
            return GetDbContext().Topics.Where(item => !item.IsDeleted).OrderBy(item => item.SortOrder).ToList();
        }

        public IList<Topic> GetTopics(IEnumerable<int> ids)
        {
            return GetTopics(GetDbContext(), item => ids.Contains(item.Id));
        }

        private static IList<Topic> GetTopics(IDataContext db, Func<Topic, bool> predicate)
        {
            return db.Topics.Where(item => !item.IsDeleted).Where(predicate).OrderBy(item => item.SortOrder).ToList();
        }

        //private static IList<Topic> GetTopicsByChapterId(IDataContext db, int chapterId)
        //{
        //    return GetTopics(db, item => item.ChapterRef == chapterId);
        //}

        public IList<Topic> GetTopics(Func<Topic, bool> predicate)
        {
            return GetTopics(GetDbContext(), predicate).ToList();
        }

        //public IEnumerable<Topic> GetTopicsByChapterId(int chapterId)
        //{
        //    return GetTopicsByChapterId(GetDbContext(), chapterId);
        //}

        public IList<Topic> GetTopicsByDisciplineId(int disciplineId)
        {
            return GetChapters(item => item.DisciplineRef == disciplineId).SelectMany(item => GetTopics(item2 => item2.ChapterRef == item.Id)).ToList();
        }

        public IList<Topic> GetTopicsByGroupId(int groupId)
        {
            return GetDisciplinesByGroupId(groupId).SelectMany(item => GetTopicsByDisciplineId(item.Id)).ToList();
        }

        public IList<Topic> GetTopicsOwnedByUser(User owner)
        {
            return GetDisciplines(item => item.Owner == owner.Username).SelectMany(item => GetTopicsByDisciplineId(item.Id)).ToList();
        }

        public IList<Topic> GetTopicsByCourseId(int courseId)
        {
            return GetDbContext().Topics.Where(item => (item.TestCourseRef == courseId || item.TheoryCourseRef == courseId) && !item.IsDeleted).ToList();
        }

        public IList<Group> GetGroupsAssignedToTopic(int topicId)
        {
            return GetTopic(topicId)
                .Chapter
                .Discipline
                .Curriculums
                .Select(item => GetGroup(item.UserGroupRef))
                .ToList();
        }

        public int AddTopic(Topic topic)
        {
            var db = GetDbContext();

            topic.Created = DateTime.Now;
            topic.Updated = DateTime.Now;
            topic.IsDeleted = false;
            db.Topics.InsertOnSubmit(topic);
            db.SubmitChanges();

            topic.SortOrder = topic.Id;
            UpdateTopic(topic);

            _lmsService.Inform(DisciplineNotifications.TopicCreated, topic);
            return topic.Id;
        }

        public void UpdateTopic(Topic topic)
        {
            var db = GetDbContext();
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

            data[0] = oldTopic;
            data[1] = updatingTopic;
            _lmsService.Inform(DisciplineNotifications.TopicEdited, data);
        }

        public void DeleteTopic(int id)
        {
            var db = GetDbContext();
            var topic = GetTopic(db, id);

            _lmsService.Inform(DisciplineNotifications.TopicDeleting, topic);

            topic.IsDeleted = true;
            db.SubmitChanges();

            _lmsService.Inform(DisciplineNotifications.TopicDeleted, topic);
        }

        public void DeleteTopics(IEnumerable<int> ids)
        {
            ids.ForEach(DeleteTopic);
        }

        public Topic TopicUp(int topicId)
        {
            var db = GetDbContext();
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
            var db = GetDbContext();
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
            return GetDbContext().TopicTypes.SingleOrDefault(item => item.Id == id);
        }

        public IList<TopicType> GetTopicTypes()
        {
            return GetDbContext().TopicTypes.ToList();
        }

        public IList<TopicType> GetTheoryTopicTypes()
        {
            return GetDbContext().TopicTypes.ToList()
                .Where(item => Converter.ToTopicType(item) == TopicTypeEnum.Theory).ToList();
        }

        public IList<TopicType> GetTestTopicTypes()
        {
            return GetDbContext().TopicTypes.ToList()
                .Where(item => Converter.ToTopicType(item) == TopicTypeEnum.Test ||
                Converter.ToTopicType(item) == TopicTypeEnum.TestWithoutCourse).ToList();
        }

        #endregion

        #endregion
    }
}