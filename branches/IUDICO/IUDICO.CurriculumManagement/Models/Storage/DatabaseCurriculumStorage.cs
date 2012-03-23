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
        private readonly ILmsService _lmsService;

        protected virtual IDataContext GetDbContext()
        {
            return new DBDataContext();
        }

        public DatabaseCurriculumStorage(ILmsService lmsService)
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

        public IList<Chapter> GetChapters(int disciplineId)
        {
            return _lmsService.FindService<IDisciplineService>().GetChapters(disciplineId);
        }

        public IList<Topic> GetTopicsByChapterId(int chapterId)
        {
            return _lmsService.FindService<IDisciplineService>().GetTopicsByChapterId(chapterId);
        }

        public IList<Discipline> GetDisciplines(User user)
        {
            return _lmsService.FindService<IDisciplineService>().GetDisciplines(user);
        }

        public Discipline GetDiscipline(int id)
        {
            return _lmsService.FindService<IDisciplineService>().GetDiscipline(id);
        }

        public Chapter GetChapter(int id)
        {
            return _lmsService.FindService<IDisciplineService>().GetChapter(id);
        }

        public Topic GetTopic(int id)
        {
            return _lmsService.FindService<IDisciplineService>().GetTopic(id);
        }

        #endregion

        #region Curriculum methods

        private static Curriculum GetCurriculum(IDataContext db, int id)
        {
            return db.Curriculums.SingleOrDefault(item => item.Id == id && !item.IsDeleted);
        }

        public Curriculum GetCurriculum(int id)
        {
            return GetCurriculum(GetDbContext(), id);
        }

        public IList<Curriculum> GetCurriculums()
        {
            return GetDbContext().Curriculums.Where(item => !item.IsDeleted).ToList();
        }

        public IList<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            return GetDbContext().Curriculums.Where(item => ids.Contains(item.Id) && !item.IsDeleted).ToList();
        }

        public IList<Curriculum> GetCurriculums(User user)
        {
            var disciplines = GetDisciplines(GetCurrentUser());
            return disciplines.SelectMany(discipline => GetCurriculumsByDisciplineId(discipline.Id))
                .OrderBy(item => item.DisciplineRef)
                .ToList();
        }

        public IList<Curriculum> GetCurriculumsByDisciplineId(int disciplineId)
        {
            return GetDbContext().Curriculums.Where(item => item.DisciplineRef == disciplineId && !item.IsDeleted).ToList();
        }

        private static IList<Curriculum> GetCurriculumsByGroupId(IDataContext db, int groupId)
        {
            return db.Curriculums.Where(item => item.UserGroupRef == groupId && !item.IsDeleted).ToList();
        }

        public IList<Curriculum> GetCurriculumsByGroupId(int groupId)
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

        public IList<TopicDescription> GetTopicDescriptions(User user)
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
                        CourseId = curriculumChapterTopic.Topic.TestCourseRef,
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

        private static CurriculumChapter GetCurriculumChapter(IDataContext db, int id)
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

        public void DeleteCurriculumChapters(IEnumerable<int> ids)
        {
            ids.ForEach(DeleteCurriculumChapter);
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
            ids.ForEach(DeleteCurriculumChapterTopic);
        }

        #endregion

        #region Group methods

        public IList<Group> GetAssignedGroups(int disciplineId)
        {
            return GetCurriculumsByDisciplineId(disciplineId).Select(item => GetGroup(item.UserGroupRef)).ToList();
        }

        public IList<Group> GetNotAssignedGroups(int disciplineId)
        {
            var assignedGroupIds = GetAssignedGroups(disciplineId).Select(item => item.Id);
            return GetGroups().Where(item => !assignedGroupIds.Contains(item.Id)).ToList();
        }

        public IList<Group> GetNotAssignedGroupsWithCurrentGroup(int disciplineId, int currentGroupId)
        {
            var groups = GetNotAssignedGroups(disciplineId);
            groups.Add(GetGroup(currentGroupId));
            return groups;
        }

        #endregion

        #endregion
    }
}