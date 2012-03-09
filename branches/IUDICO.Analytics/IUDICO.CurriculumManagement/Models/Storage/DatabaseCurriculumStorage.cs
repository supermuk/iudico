using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using System.Data.Linq;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Notifications;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    public class DatabaseCurriculumStorage : ICurriculumStorage
    {
        private readonly ILmsService _LmsService;

        protected virtual IDataContext GetDbContext()
        {
            return new DBDataContext();
        }

        public DatabaseCurriculumStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
        }

        #region IStorageInterface Members

        #region External methods

        public User GetCurrentUser()
        {
            return _LmsService.FindService<IUserService>().GetCurrentUser();
        }

        public IEnumerable<Course> GetCourses()
        {
            return _LmsService.FindService<ICourseService>().GetCourses(GetCurrentUser());
        }

        public Course GetCourse(int id)
        {
            return _LmsService.FindService<ICourseService>().GetCourse(id);
        }

        public Group GetGroup(int id)
        {
            return _LmsService.FindService<IUserService>().GetGroup(id);
        }

        public IEnumerable<Group> GetGroups()
        {
            return _LmsService.FindService<IUserService>().GetGroups();
        }

        public IEnumerable<Group> GetGroupsByUser(User user)
        {
            return _LmsService.FindService<IUserService>().GetGroupsByUser(user);
        }

        #endregion

        #region Discipline methods

        private Discipline GetDiscipline(IDataContext db, int id)
        {
            return db.Disciplines.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Discipline GetDiscipline(int id)
        {
            return GetDiscipline(GetDbContext(), id);
        }

        public IEnumerable<Discipline> GetDisciplines()
        {
            return GetDbContext().Disciplines.Where(item => !item.IsDeleted).ToList();
        }

        public IEnumerable<Discipline> GetDisciplines(User owner)
        {
            return GetDbContext().Disciplines.Where(item => !item.IsDeleted && item.Owner == owner.Username).ToList();
        }

        public IEnumerable<Discipline> GetDisciplines(IEnumerable<int> ids)
        {
            return GetDbContext().Disciplines.Where(item => ids.Contains(item.Id) && !item.IsDeleted).ToList();
        }

        public IEnumerable<Discipline> GetDisciplinesByGroupId(int groupId)
        {
            return GetCurriculumsByGroupId(groupId).Select(item => item.Discipline).ToList();
        }

        //TODO:what the fuckin method?
        public IEnumerable<Discipline> GetDisciplinesWithTopicsOwnedByUser(User user)
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

            _LmsService.Inform(DisciplineNotifications.DisciplineCreate, discipline);

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

            object[] data = new object[2];
            data[0] = oldDiscipline;
            data[1] = updatingDiscipline;
            _LmsService.Inform(DisciplineNotifications.DisciplineEdit, data);
        }

        public void DeleteDiscipline(int id)
        {
            var db = GetDbContext();
            var discipline = GetDiscipline(db, id);

            //delete corresponding curriculums
            var curriculumIds = GetCurriculumsByDisciplineId(id).Select(item => item.Id);
            DeleteCurriculums(curriculumIds);

            //delete chapters
            var chapterIds = GetChapters(id).Select(item => item.Id);
            DeleteChapters(chapterIds);

            discipline.IsDeleted = true;
            db.SubmitChanges();

            _LmsService.Inform(DisciplineNotifications.DisciplineDelete, discipline);
        }

        public void DeleteDisciplines(IEnumerable<int> ids)
        {
            ids.ForEach(id => DeleteDiscipline(id));
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

        private Chapter GetChapter(IDataContext db, int id)
        {
            return db.Chapters.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Chapter GetChapter(int id)
        {
            return GetChapter(GetDbContext(), id);
        }

        public IEnumerable<Chapter> GetChapters(int disciplineId)
        {
            return GetDbContext().Chapters.Where(item => item.DisciplineRef == disciplineId && !item.IsDeleted).ToList();
        }

        public IEnumerable<Chapter> GetChapters(IEnumerable<int> ids)
        {
            return GetDbContext().Chapters.Where(item => ids.Contains(item.Id) && !item.IsDeleted).ToList();
        }

        public int AddChapter(Chapter chapter)
        {
            var db = GetDbContext();
            chapter.Created = DateTime.Now;
            chapter.Updated = DateTime.Now;
            chapter.IsDeleted = false;

            db.Chapters.InsertOnSubmit(chapter);
            db.SubmitChanges();

            //add corresponding CurriculumChapters
            var curriculums = GetCurriculumsByDisciplineId(chapter.DisciplineRef);
            foreach (var curriculum in curriculums)
            {
                var curriculumChapter = new CurriculumChapter
                {
                    ChapterRef = chapter.Id,
                    CurriculumRef = curriculum.Id
                };
                AddCurriculumChapter(curriculumChapter);
            }

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

            //delete topics
            var topicIds = GetTopicsByChapterId(id).Select(item => item.Id);
            DeleteTopics(topicIds);

            //delete corresponding curriculum chapters
            var curriculumChapterIds = GetCurriculumChaptersByChapterId(id).Select(item => item.Id).ToList();
            DeleteCurriculumChapters(curriculumChapterIds);

            chapter.IsDeleted = true;
            db.SubmitChanges();
        }

        public void DeleteChapters(IEnumerable<int> ids)
        {
            ids.ForEach(DeleteChapter);
        }

        #endregion

        #region Topic methods

        private Topic GetTopic(IDataContext db, int id)
        {
            return db.Topics.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Topic GetTopic(int id)
        {
            return GetTopic(GetDbContext(), id);
        }

        public IEnumerable<Topic> GetTopics()
        {
            return GetDbContext().Topics.Where(item => !item.IsDeleted).OrderBy(item => item.SortOrder).ToList();
        }

        public IEnumerable<Topic> GetTopics(IEnumerable<int> ids)
        {
            return GetDbContext().Topics.Where(item => ids.Contains(item.Id) && !item.IsDeleted).OrderBy(item => item.SortOrder).ToList();
        }

        private IEnumerable<Topic> GetTopicsByChapterId(IDataContext db, int chapterId)
        {
            return db.Topics.Where(item => item.ChapterRef == chapterId && !item.IsDeleted).OrderBy(item => item.SortOrder).ToList();
        }

        public IEnumerable<Topic> GetTopicsByChapterId(int chapterId)
        {
            return GetTopicsByChapterId(GetDbContext(), chapterId);
        }

        public IEnumerable<Topic> GetTopicsByDisciplineId(int disciplineId)
        {
            return GetChapters(GetDiscipline(disciplineId).Id).SelectMany(item => GetTopicsByChapterId(item.Id)).ToList();
        }

        public IEnumerable<Topic> GetTopicsByGroupId(int groupId)
        {
            return GetDisciplinesByGroupId(groupId).SelectMany(item => GetTopicsByDisciplineId(item.Id)).ToList();
        }

        public IEnumerable<Topic> GetTopicsOwnedByUser(User owner)
        {
            return GetDisciplines(owner).SelectMany(item => GetTopicsByDisciplineId(item.Id)).ToList();
        }

        public IEnumerable<Topic> GetTopicsByCourseId(int courseId)
        {
            return GetDbContext().Topics.Where(item => (item.TestCourseRef == courseId || item.TheoryCourseRef == courseId) && !item.IsDeleted).ToList();
        }

        public IEnumerable<TopicDescription> GetTopicsAvailableForUser(User user)
        {
            var result = new List<TopicDescription>();
            var groups = GetGroupsByUser(user).ToList(); //get groups for user
            var dateNow = DateTime.Now;
            var curriculums = groups
                .SelectMany(group => GetCurriculumsByGroupId(group.Id))
                .Where(curriculum => dateNow.Between(curriculum.StartDate, curriculum.EndDate))
                .ToList();
            var curriculumChapters = curriculums
                .SelectMany(curriculum => GetCurriculumChaptersByCurriculumId(curriculum.Id))
                .Where(curriculumChapter => dateNow.Between(curriculumChapter.StartDate, curriculumChapter.EndDate))
                .ToList();
            var curriculumChapterTopics = curriculumChapters
                .SelectMany(curriculumChapter => GetCurriculumChapterTopicsByCurriculumChapterId(curriculumChapter.Id));
            //.Where(curriculumChapterTopic => dateNow.Between(curriculumChapterTopic.StartDate, curriculumChapterTopic.EndDate))
            //.ToList();

            var blockedCurriculumIds = new List<int>();
            foreach (var curriculumChapterTopic in curriculumChapterTopics)
            {
                TopicDescription testTopicDescription = null;
                TopicDescription theoryTopicDescription = null;
                if (curriculumChapterTopic.Topic.TestCourseRef.HasValue && dateNow.Between(curriculumChapterTopic.TestStartDate, curriculumChapterTopic.TestEndDate))
                {
                    testTopicDescription = new TopicDescription
                    {
                        Topic = curriculumChapterTopic.Topic,
                        TopicType = Converter.ToTopicType(curriculumChapterTopic.Topic.TestTopicType),
                        TopicPart = TopicPart.Test,
                        Chapter = curriculumChapterTopic.CurriculumChapter.Chapter,
                        Discipline = curriculumChapterTopic.CurriculumChapter.Curriculum.Discipline,
                        Curriculum = curriculumChapterTopic.CurriculumChapter.Curriculum,
                        StartDate = curriculumChapterTopic.TestStartDate.Value,
                        EndDate = curriculumChapterTopic.TestEndDate.Value
                    };
                }
                if (curriculumChapterTopic.Topic.TheoryCourseRef.HasValue && dateNow.Between(curriculumChapterTopic.TheoryStartDate, curriculumChapterTopic.TheoryEndDate))
                {
                    theoryTopicDescription = new TopicDescription
                    {
                        Topic = curriculumChapterTopic.Topic,//???
                        TopicType = Converter.ToTopicType(curriculumChapterTopic.Topic.TheoryTopicType),//???
                        TopicPart = TopicPart.Theory,
                        Chapter = curriculumChapterTopic.CurriculumChapter.Chapter,//???
                        Discipline = curriculumChapterTopic.CurriculumChapter.Curriculum.Discipline,//???
                        Curriculum = curriculumChapterTopic.CurriculumChapter.Curriculum,//???
                        StartDate = curriculumChapterTopic.TheoryStartDate.Value,
                        EndDate = curriculumChapterTopic.TheoryEndDate.Value
                    };
                }

                var blockTopicAtTesting = false;
                if (testTopicDescription != null)
                {
                    blockTopicAtTesting = curriculumChapterTopic.BlockTopicAtTesting;
                    if (curriculumChapterTopic.BlockCurriculumAtTesting)
                    {
                        blockedCurriculumIds.Add(curriculumChapterTopic.CurriculumChapter.CurriculumRef); //???
                    }
                    result.Add(testTopicDescription);
                }

                if (theoryTopicDescription != null && !blockTopicAtTesting)
                {
                    result.Add(theoryTopicDescription);
                }
            }
            result = result
                .Where(item => !blockedCurriculumIds.Contains(item.Curriculum.Id) || item.TopicPart != TopicPart.Theory)
                    .ToList();

            return result;
        }

        public IEnumerable<Group> GetGroupsAssignedToTopic(int topicId)
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

            //TODO : organizing it like events OnTopicCreated
            //add corresponding curriculum chapter topics.
            var curriculumChapters = GetCurriculumChaptersByChapterId(topic.ChapterRef);
            foreach (var curriculumChapter in curriculumChapters)
            {
                var curriculumChapterTopic = new CurriculumChapterTopic
                {
                    CurriculumChapterRef = curriculumChapter.Id,
                    TopicRef = topic.Id,
                    MaxScore = Constants.DefaultTopicMaxScore,
                    BlockTopicAtTesting = false,
                    BlockCurriculumAtTesting = false
                };
                AddCurriculumChapterTopic(curriculumChapterTopic);
            }

            _LmsService.Inform(DisciplineNotifications.TopicCreate, topic);

            return topic.Id;
        }

        public void UpdateTopic(Topic topic)
        {
            var db = GetDbContext();
            object[] data = new object[2];
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
            _LmsService.Inform(DisciplineNotifications.TopicEdit, data);
        }

        public void DeleteTopic(int id)
        {
            var db = GetDbContext();
            var topic = GetTopic(db, id);

            //delete corresponding curriculum chapter topics
            var curriculumChapterTopicIds = GetCurriculumChapterTopicsByTopicId(id)
                .Select(item => item.Id)
                .ToList();
            DeleteCurriculumChapterTopics(curriculumChapterTopicIds);

            topic.IsDeleted = true;
            db.SubmitChanges();

            _LmsService.Inform(DisciplineNotifications.TopicDelete, topic);
        }

        public void DeleteTopics(IEnumerable<int> ids)
        {
            ids.ForEach(DeleteTopic);
        }

        public Topic TopicUp(int topicId)
        {
            var db = GetDbContext();
            var topic = GetTopic(db, topicId);
            IList<Topic> topics = GetTopicsByChapterId(db, topic.ChapterRef).ToList();

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
            IList<Topic> topics = GetTopicsByChapterId(db, topic.ChapterRef).ToList();

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

        public IEnumerable<TopicType> GetTopicTypes()
        {
            return GetDbContext().TopicTypes.ToList();
        }

        public List<TopicType> GetTheoryTopicTypes()
        {
            return GetDbContext().TopicTypes.ToList()
                .Where(item => Converter.ToTopicType(item) == TopicTypeEnum.Theory).ToList();
        }

        public List<TopicType> GetTestTopicTypes()
        {
            return GetDbContext().TopicTypes.ToList()
                .Where(item => Converter.ToTopicType(item) == TopicTypeEnum.Test ||
                Converter.ToTopicType(item) == TopicTypeEnum.TestWithoutCourse).ToList();
        }

        #endregion

        #region Curriculum methods

        private Curriculum GetCurriculum(IDataContext db, int id)
        {
            return db.Curriculums.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Curriculum GetCurriculum(int id)
        {
            return GetCurriculum(GetDbContext(), id);
        }

        public IEnumerable<Curriculum> GetCurriculums()
        {
            return GetDbContext().Curriculums.Where(item => !item.IsDeleted).ToList();
        }

        public IEnumerable<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            return GetDbContext().Curriculums.Where(item => ids.Contains(item.Id) && !item.IsDeleted).ToList();
        }

        public IEnumerable<Curriculum> GetCurriculumsByDisciplineId(int disciplineId)
        {
            return GetDbContext().Curriculums.Where(item => item.DisciplineRef == disciplineId && !item.IsDeleted).ToList();
        }

        private IEnumerable<Curriculum> GetCurriculumsByGroupId(IDataContext db, int groupId)
        {
            return db.Curriculums.Where(item => item.UserGroupRef == groupId && !item.IsDeleted).ToList();
        }

        public IEnumerable<Curriculum> GetCurriculumsByGroupId(int groupId)
        {
            return GetCurriculumsByGroupId(GetDbContext(), groupId);
        }

        public int AddCurriculum(Curriculum curriculum)
        {
            var db = GetDbContext();
            curriculum.IsDeleted = false;
            curriculum.IsValid = true;

            db.Curriculums.InsertOnSubmit(curriculum);
            db.SubmitChanges();

            //add corresponding curriculum chapters
            var chapters = GetChapters(curriculum.DisciplineRef);
            foreach (var chapter in chapters)
            {
                var curriculumChapter = new CurriculumChapter
                {
                    ChapterRef = chapter.Id,
                    CurriculumRef = curriculum.Id
                };
                AddCurriculumChapter(curriculumChapter);
            }

            return curriculum.Id;
        }

        public void UpdateCurriculum(Curriculum curriculum)
        {
            var db = GetDbContext();
            var oldCurriculum = GetCurriculum(db, curriculum.Id);

            oldCurriculum.UserGroupRef = curriculum.UserGroupRef;
            oldCurriculum.StartDate = curriculum.StartDate;
            oldCurriculum.EndDate = curriculum.EndDate;
            oldCurriculum.IsValid = true;

            db.SubmitChanges();
        }

        public void DeleteCurriculum(int id)
        {
            var db = GetDbContext();
            var curriculum = GetCurriculum(db, id);

            //delete corresponding curriculum chapters
            var curriculumChapterIds = GetCurriculumChaptersByCurriculumId(id).Select(item => item.Id).ToList();
            DeleteCurriculumChapters(curriculumChapterIds);

            curriculum.IsDeleted = true;
            db.SubmitChanges();
        }

        public void DeleteCurriculums(IEnumerable<int> ids)
        {
            ids.ForEach(DeleteCurriculum);
        }

        //public void MakeCurriculumsInvalid(int groupId)
        //{
        //    var db = GetDbContext();
        //    var curriculums = GetCurriculumsByGroupId(db, groupId);
        //    foreach (var curriculum in curriculums)
        //    {
        //        curriculum.IsValid = false;
        //    }
        //    db.SubmitChanges();
        //}

        #endregion

        #region CurriculumChapters methods

        private CurriculumChapter GetCurriculumChapter(IDataContext db, int id)
        {
            return db.CurriculumChapters.SingleOrDefault(item => !item.IsDeleted && item.Id == id);
        }

        public CurriculumChapter GetCurriculumChapter(int id)
        {
            return GetCurriculumChapter(GetDbContext(), id);
        }

        public IList<CurriculumChapter> GetCurriculumChaptersByCurriculumId(int curriculumId)
        {
            return GetDbContext().CurriculumChapters.Where(item => !item.IsDeleted && item.CurriculumRef == curriculumId).ToList();
        }

        public IList<CurriculumChapter> GetCurriculumChaptersByChapterId(int chapterId)
        {
            return GetDbContext().CurriculumChapters.Where(item => !item.IsDeleted && item.ChapterRef == chapterId).ToList();
        }

        public int AddCurriculumChapter(CurriculumChapter curriculumChapter)
        {
            var db = GetDbContext();

            curriculumChapter.IsDeleted = false;

            db.CurriculumChapters.InsertOnSubmit(curriculumChapter);
            db.SubmitChanges();

            //add corresponding curriculum chapter topics
            var topics = GetTopicsByChapterId(curriculumChapter.ChapterRef);
            foreach (var topic in topics)
            {
                var curriculumChapterTopic = new CurriculumChapterTopic
                {
                    CurriculumChapterRef = curriculumChapter.Id,
                    TopicRef = topic.Id,
                    MaxScore = Constants.DefaultTopicMaxScore,
                    BlockTopicAtTesting = false,
                    BlockCurriculumAtTesting = false
                };
                AddCurriculumChapterTopic(curriculumChapterTopic);
            }

            return curriculumChapter.Id;
        }

        public void UpdateCurriculumChapter(CurriculumChapter curriculumChapter)
        {
            var db = GetDbContext();
            var oldCurriculumChapter = GetCurriculumChapter(db, curriculumChapter.Id);

            oldCurriculumChapter.StartDate = curriculumChapter.StartDate;
            oldCurriculumChapter.EndDate = curriculumChapter.EndDate;

            db.SubmitChanges();
        }

        public void DeleteCurriculumChapter(int id)
        {
            var db = GetDbContext();
            var curriculumChapter = GetCurriculumChapter(db, id);

            //delete corresponding curriculum chapter topics
            var curriculumChapterTopicIds = GetCurriculumChapterTopicsByCurriculumChapterId(id)
                .Select(item => item.Id)
                .ToList();
            DeleteCurriculumChapterTopics(curriculumChapterTopicIds);

            curriculumChapter.IsDeleted = true;
            db.SubmitChanges();
        }

        public void DeleteCurriculumChapters(IList<int> ids)
        {
            ids.ForEach(id => DeleteCurriculumChapter(id));
        }

        #endregion

        #region CurriculumChapterTopic methods

        private CurriculumChapterTopic GetCurriculumChapterTopic(IDataContext db, int id)
        {
            return db.CurriculumChapterTopics.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public CurriculumChapterTopic GetCurriculumChapterTopic(int id)
        {
            return GetCurriculumChapterTopic(GetDbContext(), id);
        }

        public IList<CurriculumChapterTopic> GetCurriculumChapterTopicsByCurriculumChapterId(int curriculumChapterId)
        {
            return GetDbContext().CurriculumChapterTopics.Where(item => item.CurriculumChapterRef == curriculumChapterId && !item.IsDeleted).ToList();
        }

        public IList<CurriculumChapterTopic> GetCurriculumChapterTopicsByTopicId(int topicId)
        {
            return GetDbContext().CurriculumChapterTopics.Where(item => item.TopicRef == topicId && !item.IsDeleted).ToList();
        }

        public int AddCurriculumChapterTopic(CurriculumChapterTopic curriculumChapterTopic)
        {
            var db = GetDbContext();

            curriculumChapterTopic.IsDeleted = false;

            db.CurriculumChapterTopics.InsertOnSubmit(curriculumChapterTopic);
            db.SubmitChanges();
            return curriculumChapterTopic.Id;
        }

        public void UpdateCurriculumChapterTopic(CurriculumChapterTopic curriculumChapterTopic)
        {
            var db = GetDbContext();
            var oldTopicAssignment = GetCurriculumChapterTopic(db, curriculumChapterTopic.Id);

            oldTopicAssignment.MaxScore = curriculumChapterTopic.MaxScore;
            oldTopicAssignment.BlockCurriculumAtTesting = curriculumChapterTopic.BlockCurriculumAtTesting;
            oldTopicAssignment.BlockTopicAtTesting = curriculumChapterTopic.BlockTopicAtTesting;
            oldTopicAssignment.TheoryStartDate = curriculumChapterTopic.TheoryStartDate;
            oldTopicAssignment.TheoryEndDate = curriculumChapterTopic.TheoryEndDate;
            oldTopicAssignment.TestStartDate = curriculumChapterTopic.TestStartDate;
            oldTopicAssignment.TestEndDate = curriculumChapterTopic.TestEndDate;

            db.SubmitChanges();
        }

        public void DeleteCurriculumChapterTopic(int id)
        {
            var db = GetDbContext();
            var curriculumChapterTopic = GetCurriculumChapterTopic(db, id);

            curriculumChapterTopic.IsDeleted = true;

            db.SubmitChanges();
        }

        public void DeleteCurriculumChapterTopics(IEnumerable<int> ids)
        {
            ids.ForEach(id => DeleteCurriculumChapterTopic(id));
        }

        #endregion

        #region Group methods

        public IEnumerable<Group> GetAssignedGroups(int disciplineId)
        {
            return GetCurriculumsByDisciplineId(disciplineId).Select(item => GetGroup(item.UserGroupRef));
        }

        public IEnumerable<Group> GetNotAssignedGroups(int disciplineId)
        {
            var assignedGroupIds = GetAssignedGroups(disciplineId).Select(item => item.Id);
            return GetGroups().Where(item => !assignedGroupIds.Contains(item.Id)).Select(item => item);
        }

        public IEnumerable<Group> GetNotAssignedGroupsWithCurrentGroup(int disciplineId, int currentGroupId)
        {
            IEnumerable<Group> groups = GetNotAssignedGroups(disciplineId);

            //add current group
            List<Group> assignedGroup = new List<Group>();
            assignedGroup.Add(GetGroup(currentGroupId));
            groups = groups.Concat(assignedGroup);

            return groups;
        }

        #endregion

        #endregion
    }
}