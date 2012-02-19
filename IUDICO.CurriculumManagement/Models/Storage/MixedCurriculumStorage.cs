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
    public class MixedCurriculumStorage : ICurriculumStorage
    {
        private readonly ILmsService _LmsService;

        protected virtual IDataContext GetDbContext()
        {
            return new DBDataContext();
        }

        public MixedCurriculumStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
            //RefreshState();
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

        public IEnumerable<Course> GetCoursesOwnedByUser(User user)
        {
            return _LmsService.FindService<ICourseService>().GetCourses(user);
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
            //return _Db.Disciplines.Where(item => ids.Contains(item.Id) && !item.IsDeleted).ToList();
            return GetDbContext().Disciplines.Where(item => ids.Contains(item.Id) && !item.IsDeleted).ToList();
        }

        public IEnumerable<Discipline> GetDisciplinesByGroupId(int groupId)
        {
            return GetCurriculumsByGroupId(groupId).Select(item => item.Discipline).ToList();
        }

        //TODO:what the fuckin method?
        public IEnumerable<Discipline> GetDisciplinesWithTopicsOwnedByUser(User user)
        {
            IEnumerable<int> courseIds = GetCoursesOwnedByUser(user)
                .Select(item => item.Id)
                .ToList();
            return GetDisciplines(user) //?
                .Where(item => GetTopicsByDisciplineId(item.Id)
                             .Any(topic => courseIds.Contains(topic.CourseRef ?? Constants.NoCourseId)))
                             .ToList();
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
            var oldDiscipline = GetDiscipline(db, discipline.Id);
            var newDiscipline = GetDiscipline(db, discipline.Id);

            newDiscipline.Name = discipline.Name;
            newDiscipline.Updated = DateTime.Now;

            db.SubmitChanges();

            object[] data = new object[2];
            data[0] = oldDiscipline;
            data[1] = newDiscipline;

            _LmsService.Inform(DisciplineNotifications.DisciplineEdit, data);
        }

        public void DeleteDiscipline(int id)
        {
            var db = GetDbContext();
            var discipline = GetDiscipline(db, id);

            //delete chapters
            var chapterIds = GetChapters(id).Select(item => item.Id);
            DeleteChapters(chapterIds);

            //delete corresponding Curriculums
            var curriculumIds = GetDisciplineAssignmnetsByDisciplineId(id).Select(item => item.Id);
            DeleteCurriculums(curriculumIds);

            discipline.IsDeleted = true;
            db.SubmitChanges();

            _LmsService.Inform(DisciplineNotifications.DisciplineDelete, discipline);
        }

        public void DeleteDisciplines(IEnumerable<int> ids)
        {
            foreach (int id in ids)
            {
                DeleteDiscipline(id);
            }
        }

        public void MakeDisciplineInvalid(int courseId)
        {
            var db = GetDbContext();
            var topicIds = GetTopicsByCourseId(courseId).Select(item => item.Id);
            var disciplines = db.Disciplines.Where(item => topicIds.Contains(item.Id) && !item.IsDeleted);
            foreach (Discipline discipline in disciplines)
            {
                discipline.IsValid = false;
            }
            db.SubmitChanges();
        }

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

            //delete corresponding ChapterTimelines
            var chapterTimelines = GetChapterTimelinesByChapterId(id);
            foreach (Timeline timeline in chapterTimelines)
            {
                DeleteTimeline(timeline.Id);
            }

            chapter.IsDeleted = true;
            db.SubmitChanges();
        }

        public void DeleteChapters(IEnumerable<int> ids)
        {
            foreach (int id in ids)
            {
                DeleteChapter(id);
            }
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
            var db = GetDbContext();

            return db.Topics.Where(t => !t.IsDeleted);
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

        public Dictionary<Topic, UserTopicScore> GetTopicsWithScoreByChapterId(int chapterId)
        {
            var db = GetDbContext();

            var values =
                (from t in db.Topics
                 where t.ChapterRef == chapterId && !t.IsDeleted
                 join tu in db.UserTopicScores on t.Id equals tu.TopicId into tus
                 from p in tus.DefaultIfEmpty() 
                 select new KeyValuePair<Topic, UserTopicScore>(t, p)).ToDictionary(kv => kv.Key, kv => kv.Value);


            return values;
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
            return GetDbContext().Topics.Where(item => item.CourseRef == courseId && !item.IsDeleted).ToList();
        }

        public IEnumerable<TopicDescription> GetTopicsAvailableForUser(User user)
        {
            IEnumerable<Group> groups = GetGroupsByUser(user).ToList(); //get groups for user
            DateTime dateTime = DateTime.Now;
            List<TopicDescription> result = new List<TopicDescription>();

            var curriculums = groups.SelectMany(group => GetCurriculumsByGroupId(group.Id)) //get discipline assignments
                   .Where(curriculum => GetCurriculumTimelines(curriculum.Id) //select those discipline assignments,
                   .Any(timeline => dateTime.IsIn(timeline))); //for which specified date is in any of the discipline assignment timelines

            foreach (Curriculum curriculum in curriculums)
            {
                //get chapters
                foreach (Chapter chapter in GetChapters(curriculum.DisciplineRef))
                {
                    var chapterTimelines = GetChapterTimelines(chapter.Id, curriculum.Id)
                                        .ToList();
                    //select those chapters, which doesn't have chapter timelines or for which specified date is in any of the chapter timelines
                    if (chapterTimelines.Count() == 0 || chapterTimelines.Any(timeline => dateTime.IsIn(timeline)))
                    {
                        //get topics
                        //result.AddRange(GetTopicsByChapterId(chapter.Id));
                        foreach (var kv in GetTopicsWithScoreByChapterId(chapter.Id))
                        {
                            result.Add(new TopicDescription()
                            {
                                Topic = kv.Key,
                                Chapter = chapter,
                                Discipline = chapter.Discipline,
                                Rating = (kv.Value == null ? 0 : kv.Value.Score),
                                Timelines = chapterTimelines.Count() == 0 ?
                                    GetCurriculumTimelines(curriculum.Id)
                                        .Where(timeline => dateTime.IsIn(timeline)).ToList() :
                                    chapterTimelines
                                        .Where(timeline => dateTime.IsIn(timeline)).ToList()
                            });
                        }
                    }
                };
            }

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
            _LmsService.Inform(DisciplineNotifications.TopicCreate, topic);

            topic.SortOrder = topic.Id;
            UpdateTopic(topic);

            //if it is "Test" then add corresponding TopicAssignments.
            if (Converters.ConvertToTopicType(topic.TopicType) == Enums.TopicType.Test ||
                Converters.ConvertToTopicType(topic.TopicType) == Enums.TopicType.TestWithoutCourse)
            {
                AddTopicAssignments(topic);
            }



            return topic.Id;
        }

        public void UpdateTopic(Topic topic)
        {
            var db = GetDbContext();
            object[] data = new object[2];
            var oldTopic = GetTopic(db, topic.Id);
            var newTopic = oldTopic;

            newTopic.Name = topic.Name;
            newTopic.SortOrder = topic.SortOrder;
            newTopic.CourseRef = topic.CourseRef;
            newTopic.Updated = DateTime.Now;

            if (newTopic.TopicTypeRef != topic.TopicTypeRef)
            {
                newTopic.TopicTypeRef = topic.TopicTypeRef;
                if (Converters.ConvertToTopicType(topic.TopicType) == Enums.TopicType.Test ||
                    Converters.ConvertToTopicType(topic.TopicType) == Enums.TopicType.TestWithoutCourse)
                {
                    AddTopicAssignments(topic);
                }
                else
                {
                    DeleteTopicAssignments(topic);
                }
            }
            db.SubmitChanges();
            data[0] = oldTopic;
            data[1] = newTopic;

            _LmsService.Inform(DisciplineNotifications.TopicEdit, data);
        }

        public void DeleteTopic(int id)
        {
            var db = GetDbContext();
            var topic = GetTopic(db, id);

            //if it is "Test" then delete corresponding TopicAssignments.
            if (Converters.ConvertToTopicType(topic.TopicType) == Enums.TopicType.Test ||
                Converters.ConvertToTopicType(topic.TopicType) == Enums.TopicType.TestWithoutCourse)
            {
                DeleteTopicAssignments(topic);
            }
            topic.IsDeleted = true;
            db.SubmitChanges();

            _LmsService.Inform(DisciplineNotifications.TopicDelete, topic);
        }

        public void DeleteTopics(IEnumerable<int> ids)
        {
            foreach (int id in ids)
            {
                DeleteTopic(id);
            }
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

        /// <summary>
        /// Adds topic assignments for topic.
        /// </summary>
        /// <param name="topic">The topic.</param>
        private void AddTopicAssignments(Topic topic)
        {
            var curriculums = GetDisciplineAssignmnetsByDisciplineId(topic.Chapter.DisciplineRef);
            foreach (Curriculum curriculum in curriculums)
            {
                TopicAssignment newTopicAssignment = new TopicAssignment()
                {
                    CurriculumRef = curriculum.Id,
                    TopicRef = topic.Id,
                    MaxScore = Constants.DefaultTopicMaxScore
                };

                AddTopicAssignment(newTopicAssignment);
            }
        }

        /// <summary>
        /// Deletes topic assignments for topic.
        /// </summary>
        /// <param name="topic">The topic.</param>
        private void DeleteTopicAssignments(Topic topic)
        {
            var topicAssignmentIds = GetTopicAssignmentsByTopicId(topic.Id)
                .Select(item => item.Id);
            DeleteTopicAssignments(topicAssignmentIds);
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

        #endregion

        #region Curriculum methods

        private Curriculum GetCurriculum(IDataContext db, int curriculumId)
        {
            return db.Curriculums.SingleOrDefault(item => item.Id == curriculumId && !item.IsDeleted);
        }

        public Curriculum GetCurriculum(int curriculumId)
        {
            return GetCurriculum(GetDbContext(), curriculumId);
        }

        public IEnumerable<Curriculum> GetCurriculums()
        {
            return GetDbContext().Curriculums.Where(item => !item.IsDeleted).ToList();
        }

        public IEnumerable<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            return GetDbContext().Curriculums.Where(item => ids.Contains(item.Id) && !item.IsDeleted).ToList();
        }

        public IEnumerable<Curriculum> GetDisciplineAssignmnetsByDisciplineId(int disciplineId)
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
            //return GetDbContext().Curriculums.Where(item => item.UserGroupRef == groupId && !item.IsDeleted).ToList();
        }

        public int AddCurriculum(Curriculum curriculum)
        {
            var db = GetDbContext();
            curriculum.IsDeleted = false;
            curriculum.IsValid = true;

            db.Curriculums.InsertOnSubmit(curriculum);
            db.SubmitChanges();

            //add corresponding TopicAssignments
            var topicsInCurrentDiscipline = GetTopicsByDisciplineId(curriculum.DisciplineRef)
                .Where(item => item.TopicTypeRef == (int)Enums.TopicType.Test ||
                    item.TopicTypeRef == (int)Enums.TopicType.TestWithoutCourse);
            foreach (var topic in topicsInCurrentDiscipline)
            {
                TopicAssignment newTopicAssingment = new TopicAssignment()
                {
                    CurriculumRef = curriculum.Id,
                    TopicRef = topic.Id,
                    MaxScore = Constants.DefaultTopicMaxScore
                };

                AddTopicAssignment(newTopicAssingment);
            }

            return curriculum.Id;
        }

        public void UpdateCurriculum(Curriculum curriculum)
        {
            var db = GetDbContext();
            var oldCurriculum = GetCurriculum(db, curriculum.Id);

            oldCurriculum.UserGroupRef = curriculum.UserGroupRef;
            oldCurriculum.IsValid = true;

            db.SubmitChanges();
        }

        public void DeleteCurriculum(int curriculumId)
        {
            var db = GetDbContext();
            var curriculum = GetCurriculum(db, curriculumId);

            //delete corresponding CurriculumTimelines
            var curriculumTimelineIds = GetCurriculumTimelines(curriculumId).Select(item => item.Id);
            DeleteTimelines(curriculumTimelineIds);

            //delete corresponding ChapterTimelines
            var chapterTimelineIds = GetChapterTimelinesByCurriculumId(curriculumId).Select(item => item.Id);
            DeleteTimelines(chapterTimelineIds);

            //delete corresponding TopicAssignments
            var topicAssignmentIds = GetTopicAssignmentsByCurriculumId(curriculumId).Select(item => item.Id);
            DeleteTopicAssignments(topicAssignmentIds);

            curriculum.IsDeleted = true;
            db.SubmitChanges();
        }

        public void DeleteCurriculums(IEnumerable<int> ids)
        {
            foreach (int id in ids)
            {
                DeleteCurriculum(id);
            }
        }

        public void MakeCurriculumsInvalid(int groupId)
        {
            var db = GetDbContext();
            var curriculums = GetCurriculumsByGroupId(db, groupId);
            foreach (var curriculum in curriculums)
            {
                curriculum.IsValid = false;
            }
            db.SubmitChanges();
        }

        #endregion

        #region TopicAssignment methods

        private TopicAssignment GetTopicAssignment(IDataContext db, int topicAssignmentId)
        {
            return db.TopicAssignments.SingleOrDefault(item => item.Id == topicAssignmentId && !item.IsDeleted);
        }

        public TopicAssignment GetTopicAssignment(int topicAssignmentId)
        {
            return GetTopicAssignment(GetDbContext(), topicAssignmentId);
        }

        public IEnumerable<TopicAssignment> GetTopicAssignmentsByCurriculumId(int curriculumId)
        {
            return GetDbContext().TopicAssignments.Where(item => item.CurriculumRef == curriculumId && !item.IsDeleted).ToList();
        }

        public IEnumerable<TopicAssignment> GetTopicAssignmentsByTopicId(int topicId)
        {
            return GetDbContext().TopicAssignments.Where(item => item.TopicRef == topicId && !item.IsDeleted).ToList();
        }

        private IEnumerable<TopicAssignment> GetTopicAssignments(IDataContext db, IEnumerable<int> ids)
        {
            return db.TopicAssignments.Where(item => ids.Contains(item.Id) && !item.IsDeleted).ToList();
        }

        public IEnumerable<TopicAssignment> GetTopicAssignments(IEnumerable<int> ids)
        {
            return GetTopicAssignments(GetDbContext(), ids);
        }

        public int AddTopicAssignment(TopicAssignment topicAssignment)
        {
            var db = GetDbContext();
            db.TopicAssignments.InsertOnSubmit(topicAssignment);
            db.SubmitChanges();

            return topicAssignment.Id;
        }

        public void UpdateTopicAssignment(TopicAssignment topicAssignment)
        {
            var db = GetDbContext();
            var oldTopicAssignment = GetTopicAssignment(db, topicAssignment.Id);

            oldTopicAssignment.MaxScore = topicAssignment.MaxScore;

            db.SubmitChanges();
        }

        public void DeleteTopicAssignments(IEnumerable<int> ids)
        {
            var db = GetDbContext();
            var topicAssignments = GetTopicAssignments(db, ids);

            foreach (TopicAssignment item in topicAssignments)
            {
                item.IsDeleted = true;
            }

            db.SubmitChanges();
        }

        #endregion

        #region Timeline methods

        private IEnumerable<Timeline> GetTimelines(IDataContext db)
        {
            return db.Timelines.Where(item => !item.IsDeleted).ToList();
        }

        private Timeline GetTimeline(IDataContext db, int timelineId)
        {
            return GetTimelines(db).SingleOrDefault(item => item.Id == timelineId);
        }

        public Timeline GetTimeline(int timelineId)
        {
            return GetTimeline(GetDbContext(), timelineId);
        }

        public IEnumerable<Timeline> GetCurriculumTimelines(int curriculumId)
        {
            return GetTimelines(GetDbContext()).Where(item => item.CurriculumRef == curriculumId && item.ChapterRef == null).ToList();
        }

        public IEnumerable<Timeline> GetChapterTimelinesByCurriculumId(int curriculumId)
        {
            return GetTimelines(GetDbContext()).Where(item => item.CurriculumRef == curriculumId && item.ChapterRef != null).ToList();
        }

        public IEnumerable<Timeline> GetChapterTimelinesByChapterId(int chapterId)
        {
            return GetTimelines(GetDbContext()).Where(item => item.ChapterRef == chapterId && item.ChapterRef != null).ToList();
        }

        public IEnumerable<Timeline> GetChapterTimelines(int chapterId, int curriculumId)
        {
            return GetTimelines(GetDbContext()).Where(item => item.ChapterRef == chapterId && item.CurriculumRef == curriculumId
                && item.ChapterRef != null).ToList();
        }

        private IEnumerable<Timeline> GetTimelines(IDataContext db, IEnumerable<int> timelineIds)
        {
            return GetTimelines(db).Where(item => timelineIds.Contains(item.Id)).ToList();
        }

        public IEnumerable<Timeline> GetTimelines(IEnumerable<int> timelineIds)
        {
            return GetTimelines(GetDbContext(), timelineIds);
        }

        public int AddTimeline(Timeline timeline)
        {
            var db = GetDbContext();
            timeline.IsDeleted = false;

            db.Timelines.InsertOnSubmit(timeline);
            db.SubmitChanges();

            return timeline.Id;
        }

        public void UpdateTimeline(Timeline timeline)
        {
            var db = GetDbContext();
            var oldTimeline = GetTimeline(db, timeline.Id);

            oldTimeline.StartDate = timeline.StartDate;
            oldTimeline.EndDate = timeline.EndDate;

            db.SubmitChanges();
        }

        public void DeleteTimeline(int timelineId)
        {
            var db = GetDbContext();
            var timeline = GetTimeline(db, timelineId);

            timeline.IsDeleted = true;

            db.SubmitChanges();
        }

        public void DeleteTimelines(IEnumerable<int> timelineIds)
        {
            var db = GetDbContext();
            var timelines = GetTimelines(db, timelineIds);

            foreach (Timeline timeline in timelines)
            {
                timeline.IsDeleted = true;
            }

            db.SubmitChanges();
        }

        #endregion

        #region Group methods

        public IEnumerable<Group> GetAssignedGroups(int disciplineId)
        {
            return GetDisciplineAssignmnetsByDisciplineId(disciplineId).Select(item => GetGroup(item.UserGroupRef));
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