using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using System;
using IUDICO.Common.Models.Shared.DisciplineManagement;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    public class DatabaseCurriculumStorage : ICurriculumStorage
    {
        protected readonly ILmsService LmsService;
        protected readonly LinqLogger Logger;

        protected virtual IDataContext GetDbContext()
        {
            var db = new DBDataContext();

#if DEBUG
            db.Log = this.Logger;
#endif

            return db;
        }

        public DatabaseCurriculumStorage(ILmsService lmsService, LinqLogger logger)
        {
            this.LmsService = lmsService;
            this.Logger = logger;
        }

        public DatabaseCurriculumStorage(ILmsService lmsService)
        {
            this.LmsService = lmsService;
        }

        #region IStorageInterface Members

        #region External methods

        public User GetCurrentUser()
        {
            return this.LmsService.FindService<IUserService>().GetCurrentUser();
        }

        public IList<Course> GetCourses()
        {
            return this.LmsService.FindService<ICourseService>().GetCourses(this.GetCurrentUser()).ToList();
        }

        public Course GetCourse(int id)
        {
            return this.LmsService.FindService<ICourseService>().GetCourse(id);
        }

        public Group GetGroup(int id)
        {
            return this.LmsService.FindService<IUserService>().GetGroup(id);
        }

        public IList<Group> GetGroups()
        {
            return this.LmsService.FindService<IUserService>().GetGroups().ToList();
        }

        public IList<Group> GetGroupsByUser(User user)
        {
            return this.LmsService.FindService<IUserService>().GetGroupsByUser(user).ToList();
        }

        public IList<Chapter> GetChapters(int disciplineId)
        {
            return this.LmsService.FindService<IDisciplineService>().GetChapters(disciplineId);
        }

        public IList<Topic> GetTopicsByChapterId(int chapterId)
        {
            return this.LmsService.FindService<IDisciplineService>().GetTopicsByChapterId(chapterId);
        }

        public IList<Discipline> GetDisciplines(User user)
        {
            return this.LmsService.FindService<IDisciplineService>().GetDisciplines(user);
        }

        public Discipline GetDiscipline(int id)
        {
            return this.LmsService.FindService<IDisciplineService>().GetDiscipline(id);
        }

        public Chapter GetChapter(int id)
        {
            return this.LmsService.FindService<IDisciplineService>().GetChapter(id);
        }

        public Topic GetTopic(int id)
        {
            return this.LmsService.FindService<IDisciplineService>().GetTopic(id);
        }

        #endregion

        #region Curriculum methods

        private static Curriculum GetCurriculum(IDataContext db, int id)
        {
            return db.Curriculums.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Curriculum GetCurriculum(int id)
        {
            return GetCurriculum(this.GetDbContext(), id);
        }

        public IList<Curriculum> GetCurriculums()
        {
            return GetCurriculums(item => true);
        }

        public IList<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            return this.GetDbContext().Curriculums.Where(item => ids.Contains(item.Id) && !item.IsDeleted).ToList();
        }

        public IList<Curriculum> GetCurriculums(User user)
        {
            var disciplines = this.GetDisciplines(this.GetCurrentUser());
            return disciplines.SelectMany(discipline => GetCurriculums(c => c.DisciplineRef == discipline.Id))
                .OrderBy(item => item.DisciplineRef)
                .ToList();
        }

        public IList<Curriculum> GetCurriculums(Func<Curriculum, bool> predicate)
        {
            return this.GetDbContext().Curriculums.Where(item => !item.IsDeleted).Where(predicate).ToList();
        }

        private bool IsCurriculumValid(Curriculum curriculum)
        {
            if (this.GetGroup(curriculum.UserGroupRef) == null)
            {
                return false;
            }

            if (!this.GetDiscipline(curriculum.DisciplineRef).IsValid)
            {
                return false;
            }

            var curriculumChapters = this.GetCurriculumChapters(item => item.CurriculumRef == curriculum.Id);

            foreach (var curriculumChapter in curriculumChapters)
            {
                if (curriculumChapter.StartDate < curriculum.StartDate || curriculumChapter.EndDate > curriculum.EndDate)
                {
                    return false;
                }

                var curriculumChapterTopics = this.GetCurriculumChapterTopics(item => item.CurriculumChapterRef == curriculumChapter.Id);

                foreach (var curriculumChapterTopic in curriculumChapterTopics)
                {
                    if (curriculumChapter.StartDate > curriculumChapterTopic.TestStartDate
                        || curriculumChapter.StartDate > curriculumChapterTopic.TheoryStartDate
                        || curriculumChapter.EndDate < curriculumChapterTopic.TheoryEndDate
                        || curriculumChapter.EndDate < curriculumChapterTopic.TestEndDate)
                    {
                        return false;
                    }
                }
                foreach (var curriculumChapterTopic in curriculumChapterTopics)
                {
                    if (curriculum.StartDate > curriculumChapterTopic.TestStartDate
                        || curriculum.StartDate > curriculumChapterTopic.TheoryStartDate
                        || curriculum.EndDate < curriculumChapterTopic.TheoryEndDate
                        || curriculum.EndDate < curriculumChapterTopic.TestEndDate)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int AddCurriculum(Curriculum curriculum)
        {
            var db = this.GetDbContext();
            curriculum.IsDeleted = false;
            curriculum.IsValid = true;

            db.Curriculums.InsertOnSubmit(curriculum);
            db.SubmitChanges();

            // add corresponding curriculum chapters
            var chapters = this.GetChapters(curriculum.DisciplineRef);
            foreach (var chapter in chapters)
            {
                var curriculumChapter = new CurriculumChapter
                {
                    ChapterRef = chapter.Id,
                    CurriculumRef = curriculum.Id
                };
                this.AddCurriculumChapter(curriculumChapter);
            }

            return curriculum.Id;
        }

        public void UpdateCurriculum(Curriculum curriculum)
        {
            var db = this.GetDbContext();
            var oldCurriculum = GetCurriculum(db, curriculum.Id);

            oldCurriculum.UserGroupRef = curriculum.UserGroupRef;
            oldCurriculum.StartDate = curriculum.StartDate;
            oldCurriculum.EndDate = curriculum.EndDate;            

            db.SubmitChanges();
            oldCurriculum.IsValid = this.IsCurriculumValid(curriculum);
            db.SubmitChanges();
        }

        public void DeleteCurriculum(int id)
        {
            var db = this.GetDbContext();
            var curriculum = GetCurriculum(db, id);

            // delete corresponding curriculum chapters
            var curriculumChapterIds = this.GetCurriculumChapters(item => item.CurriculumRef == id).Select(item => item.Id).ToList();
            this.DeleteCurriculumChapters(curriculumChapterIds);

            curriculum.IsDeleted = true;
            db.SubmitChanges();
        }

        public void DeleteCurriculums(IEnumerable<int> ids)
        {
            ids.ForEach(this.DeleteCurriculum);
        }

        public IList<TopicDescription> GetTopicDescriptions(User user)
        {
            var result = new List<TopicDescription>();
            var groups = this.GetGroupsByUser(user).ToList(); // get groups for user
            var dateNow = DateTime.Now;
            var curriculums = groups
                .SelectMany(group => GetCurriculums(c => c.UserGroupRef == group.Id))
                .Where(curriculum => dateNow.Between(curriculum.StartDate, curriculum.EndDate) && curriculum.IsValid)
                .ToList();
            var curriculumChapters = curriculums
                .SelectMany(curriculum => this.GetCurriculumChapters(item => item.CurriculumRef == curriculum.Id))
                .Where(curriculumChapter => dateNow.Between(curriculumChapter.StartDate, curriculumChapter.EndDate))
                .ToList();
            var curriculumChapterTopics = curriculumChapters
                .SelectMany(curriculumChapter => this.GetCurriculumChapterTopics(item => item.CurriculumChapterRef == curriculumChapter.Id));
            // .Where(curriculumChapterTopic => dateNow.Between(curriculumChapterTopic.StartDate, curriculumChapterTopic.EndDate))
            // .ToList();

            var blockedCurriculumIds = new List<int>();
            foreach (var curriculumChapterTopic in curriculumChapterTopics)
            {
                TopicDescription testTopicDescription = null;
                TopicDescription theoryTopicDescription = null;
                if (curriculumChapterTopic.Topic.TestCourseRef.HasValue && dateNow.Between(curriculumChapterTopic.TestStartDate, curriculumChapterTopic.TestEndDate))
                {
                    testTopicDescription = new TopicDescription
                    {
                        CourseId = curriculumChapterTopic.Topic.TestCourseRef,
                        CurriculumChapterTopicId = curriculumChapterTopic.Id,
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
                        CourseId = curriculumChapterTopic.Topic.TheoryCourseRef, 
                        CurriculumChapterTopicId = curriculumChapterTopic.Id, 
                        Topic = curriculumChapterTopic.Topic, // ???
                        TopicType = Converter.ToTopicType(curriculumChapterTopic.Topic.TheoryTopicType), // ???
                        TopicPart = TopicPart.Theory, 
                        Chapter = curriculumChapterTopic.CurriculumChapter.Chapter, // ???
                        Discipline = curriculumChapterTopic.CurriculumChapter.Curriculum.Discipline, // ???
                        Curriculum = curriculumChapterTopic.CurriculumChapter.Curriculum, // ???
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
                        blockedCurriculumIds.Add(curriculumChapterTopic.CurriculumChapter.CurriculumRef); // ???
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

        public IEnumerable<TopicDescription> GetTopicDescriptionsByTopics(IEnumerable<Topic> topics, User user)
        {
            var ids = topics.Select(t => t.Id);
            
            return this.GetTopicDescriptions(user).Where(t => ids.Contains(t.Topic.Id)).AsEnumerable();
        }

        public void ChangeCurriculumsIsValid(IEnumerable<int> curriculumIds, bool isValid)
        {
            var db = this.GetDbContext();
            var curriculums = db.Curriculums.Where(item => curriculumIds.Contains(item.Id));

            foreach (var curriculum in curriculums)
            {
                curriculum.IsValid = isValid;
            }

            db.SubmitChanges();
        }

        #endregion

        #region CurriculumChapters methods

        private static CurriculumChapter GetCurriculumChapter(IDataContext db, int id)
        {
            return db.CurriculumChapters.SingleOrDefault(item => !item.IsDeleted && item.Id == id);
        }

        public CurriculumChapter GetCurriculumChapter(int id)
        {
            return GetCurriculumChapter(this.GetDbContext(), id);
        }

        public IList<CurriculumChapter> GetCurriculumChapters(Func<CurriculumChapter, bool> predicate)
        {
            return this.GetDbContext().CurriculumChapters.Where(item => !item.IsDeleted).Where(predicate).ToList();
        }

        public int AddCurriculumChapter(CurriculumChapter curriculumChapter)
        {
            var db = this.GetDbContext();

            curriculumChapter.IsDeleted = false;

            db.CurriculumChapters.InsertOnSubmit(curriculumChapter);
            
            var curriculum = GetCurriculum(db, curriculumChapter.CurriculumRef);

            if (curriculumChapter.StartDate < curriculum.StartDate || curriculumChapter.EndDate > curriculum.EndDate)
            {
                curriculum.IsValid = false;
            }

            db.SubmitChanges();
            
            // add corresponding curriculum chapter topics
            var topics = this.GetTopicsByChapterId(curriculumChapter.ChapterRef);

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

                this.AddCurriculumChapterTopic(curriculumChapterTopic);
            }

            return curriculumChapter.Id;
        }


        public void UpdateCurriculumChapter(CurriculumChapter curriculumChapter)
        {
            var db = this.GetDbContext();
            var oldCurriculumChapter = GetCurriculumChapter(db, curriculumChapter.Id);

            oldCurriculumChapter.StartDate = curriculumChapter.StartDate;
            oldCurriculumChapter.EndDate = curriculumChapter.EndDate;
            
            db.SubmitChanges();
            
            var curriculum = GetCurriculum(db, oldCurriculumChapter.CurriculumRef);
            curriculum.IsValid = this.IsCurriculumValid(curriculum);

            db.SubmitChanges();
        }

        public void DeleteCurriculumChapter(int id)
        {
            var db = this.GetDbContext();
            var curriculumChapter = GetCurriculumChapter(db, id);

            // delete corresponding curriculum chapter topics
            var curriculumChapterTopicIds = this.GetCurriculumChapterTopics(item => item.CurriculumChapterRef == id)
                .Select(item => item.Id)
                .ToList();
            this.DeleteCurriculumChapterTopics(curriculumChapterTopicIds);

            curriculumChapter.IsDeleted = true;
            db.SubmitChanges();

            var curriculum = curriculumChapter.Curriculum;
            curriculum.IsValid = this.IsCurriculumValid(curriculum);
            db.SubmitChanges();
        }

        public void DeleteCurriculumChapters(IEnumerable<int> ids)
        {
            ids.ForEach(this.DeleteCurriculumChapter);
        }

        #endregion

        #region CurriculumChapterTopic methods

        private static CurriculumChapterTopic GetCurriculumChapterTopic(IDataContext db, int id)
        {
            return db.CurriculumChapterTopics.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public CurriculumChapterTopic GetCurriculumChapterTopic(int id)
        {
            return GetCurriculumChapterTopic(this.GetDbContext(), id);
        }

        public IList<CurriculumChapterTopic> GetCurriculumChapterTopics(Func<CurriculumChapterTopic, bool> predicate)
        {
            return this.GetDbContext().CurriculumChapterTopics.Where(item => !item.IsDeleted).Where(predicate).ToList();
        }

        public int AddCurriculumChapterTopic(CurriculumChapterTopic curriculumChapterTopic)
        {
            var db = this.GetDbContext();

            curriculumChapterTopic.IsDeleted = false;

            db.CurriculumChapterTopics.InsertOnSubmit(curriculumChapterTopic);
            var curriculum = GetCurriculum(db, GetCurriculumChapter(db, curriculumChapterTopic.CurriculumChapterRef).CurriculumRef);

            if (curriculum.StartDate > curriculumChapterTopic.TestStartDate
                || curriculum.StartDate > curriculumChapterTopic.TheoryStartDate
                || curriculum.EndDate < curriculumChapterTopic.TheoryEndDate
                || curriculum.EndDate < curriculumChapterTopic.TestEndDate)
            {
                curriculum.IsValid = false;
            }

            db.SubmitChanges();
            return curriculumChapterTopic.Id;
        }

        public void UpdateCurriculumChapterTopic(CurriculumChapterTopic curriculumChapterTopic)
        {
            var db = this.GetDbContext();
            var oldTopicAssignment = GetCurriculumChapterTopic(db, curriculumChapterTopic.Id);

            oldTopicAssignment.MaxScore = curriculumChapterTopic.MaxScore;
            oldTopicAssignment.BlockCurriculumAtTesting = curriculumChapterTopic.BlockCurriculumAtTesting;
            oldTopicAssignment.BlockTopicAtTesting = curriculumChapterTopic.BlockTopicAtTesting;
            oldTopicAssignment.TheoryStartDate = curriculumChapterTopic.TheoryStartDate;
            oldTopicAssignment.TheoryEndDate = curriculumChapterTopic.TheoryEndDate;
            oldTopicAssignment.TestStartDate = curriculumChapterTopic.TestStartDate;
            oldTopicAssignment.TestEndDate = curriculumChapterTopic.TestEndDate;            

            db.SubmitChanges();
            var curriculumChapter = GetCurriculumChapter(db, oldTopicAssignment.CurriculumChapterRef);
            var curriculum = GetCurriculum(db, curriculumChapter.CurriculumRef);
            curriculum.IsValid = this.IsCurriculumValid(curriculum);
            db.SubmitChanges();
        }

        public void DeleteCurriculumChapterTopic(int id)
        {
            var db = this.GetDbContext();
            var curriculumChapterTopic = GetCurriculumChapterTopic(db, id);

            curriculumChapterTopic.IsDeleted = true;

            db.SubmitChanges();
            
            var curriculum = curriculumChapterTopic.CurriculumChapter.Curriculum;
            curriculum.IsValid = this.IsCurriculumValid(curriculum);
            db.SubmitChanges();
        }

        public void DeleteCurriculumChapterTopics(IEnumerable<int> ids)
        {
            ids.ForEach(this.DeleteCurriculumChapterTopic);
        }

        public bool CanPassCurriculumChapterTopic(User user, CurriculumChapterTopic curriculumChapterTopic, TopicTypeEnum topicType)
        {
            // TODO: implement in more sophisticated and performance-proof maner

            var descriptions = this.GetTopicDescriptions(user);

            var selectedDescriptions =
                descriptions.Where(desc => desc.CurriculumChapterTopicId == curriculumChapterTopic.Id
                                           && desc.TopicType == topicType);

            return selectedDescriptions.Count() == 1;
        }

        public IEnumerable<CurriculumChapterTopic> GetCurriculumChapterTopicsByCurriculumId(int curriculumId)
        {
            return this.GetCurriculumChapters(item => item.CurriculumRef == curriculumId).SelectMany(item => this.GetCurriculumChapterTopics(item2 => item2.CurriculumChapterRef == item.Id));
        }

        #endregion

        #endregion
    }
}