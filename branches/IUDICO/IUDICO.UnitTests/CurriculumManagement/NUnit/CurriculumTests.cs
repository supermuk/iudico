using System;
using System.Collections.Generic;
using NUnit.Framework;
using IUDICO.Common.Models.Services;
using System.Linq;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Shared;

namespace IUDICO.UnitTests.CurriculumManagement.NUnit
{
    [TestFixture]
    public class DisciplineTests
    {
        protected CurriculumManagementTests _Tests = CurriculumManagementTests.GetInstance();
        protected ICurriculumStorage _Storage
        {
            get
            {
                return _Tests.Storage;
            }
        }
        protected List<Discipline> CreateDefaultData()
        {
            var disciplines = new List<Discipline>()
            {
                new Discipline() { Name = "Discipline1" },
                new Discipline() { Name = "Discipline2" },
                new Discipline() { Name = "Discipline3" },
                new Discipline() { Name = "Discipline4" }
            };
            return disciplines;
        }

        [SetUp]
        public void InitializeTest()
        {
            _Tests.ClearTables();
        }

        #region DisciplineMethodsTests
        [Test]
        public void AddDiscipline()
        {
            List<Discipline> disciplines = CreateDefaultData();
            var ids = disciplines.Select(item => _Storage.AddDiscipline(item)).ToList();
            disciplines.Select((item, index) => index).ToList()
                .ForEach(index => AdvAssert.AreEqual(disciplines[index], _Storage.GetDiscipline(ids[index])));
            try
            {
                _Storage.AddDiscipline(new Discipline());
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.AddDiscipline(new Discipline { });
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetDiscipline()
        {
            List<Discipline> disciplines = CreateDefaultData();
            var ids = disciplines.Select(item => _Storage.AddDiscipline(item)).ToList();
            disciplines.Select((item, i) => i).ToList()
                .ForEach(i => AdvAssert.AreEqual(disciplines[i], _Storage.GetDiscipline(ids[i])));
            #region WhyDoesItWork
            Discipline cur = _Storage.GetDiscipline(0);
            Assert.AreEqual(null, cur);
            Discipline disciplineWithExistesId = new Discipline { Name = "ExistedDiscipline", Id = ids[0] };
            _Storage.AddDiscipline(disciplineWithExistesId);
            _Storage.GetDiscipline(ids[0]);
            #endregion
            try
            {
                _Storage.GetDiscipline(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                disciplineWithExistesId = new Discipline { Name = "ExistedDiscipline", Id = ids[0] };
                _Storage.AddDiscipline(disciplineWithExistesId);
                _Storage.GetDiscipline(ids[0]);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetDisciplines()
        {
            User user = new User { Id = Guid.NewGuid(), Username = "user1" };
            List<Discipline> disciplines = CreateDefaultData();
            var ids = disciplines.Select(item => _Storage.AddDiscipline(item)).ToList();
            _Storage.GetDiscipline(ids[3]).Owner = user.Username;
            //Tests GetDisciplines(IEnumerable<int> ids)
            Assert.AreEqual(disciplines, _Storage.GetDisciplines(ids));
            //Tests GetDisciplines()
            Assert.AreEqual(disciplines, _Storage.GetDisciplines());
            //Tests GetDisciplines(User owner)
            AdvAssert.AreEqual(disciplines[3], _Storage.GetDisciplines(user).First());
            List<int> empty = new List<int>();
            try
            {
                _Storage.GetDisciplines(empty);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetDisciplinesByGroupId()
        {
            List<Discipline> disciplines = CreateDefaultData();
            disciplines.ForEach(item => _Storage.AddDiscipline(item));
            Group group = new Group { Id = 1, Name = "Group1" };
            var curriculums = disciplines.Select(item => new Curriculum { Discipline = item, UserGroupRef = group.Id })
                .ToList();
            curriculums.ForEach(i => _Storage.AddCurriculum(i));
            Assert.AreEqual(disciplines, _Storage.GetDisciplinesByGroupId(group.Id).ToList());
        }
        [Test]
        public void UpdateDiscipline()
        {
            Discipline discipline = new Discipline { Id = 1, Name = "Discipline1" };
            _Storage.AddDiscipline(discipline);
            discipline.Name = "UpdatedDiscipline";
            _Storage.UpdateDiscipline(discipline);
            var actualDiscipline = _Storage.GetDiscipline(discipline.Id);
            AdvAssert.AreEqual(discipline, actualDiscipline);            
            try
            {
                _Storage.UpdateDiscipline(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void DeleteCurriculium()
        {
            List<Discipline> disciplines = CreateDefaultData();
            var ids = disciplines.Select(item => _Storage.AddDiscipline(item)).ToList();
            _Storage.DeleteDiscipline(ids[0]);
            Assert.AreEqual(null, _Storage.GetDiscipline(ids[0]));
            Assert.AreNotEqual(null, _Storage.GetDiscipline(ids[1]));
            ids.RemoveAt(0);
            _Storage.DeleteDisciplines(ids);
            try
            {
                ids.ForEach(i => _Storage.GetDiscipline(i));
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.DeleteDiscipline(ids[0]);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.DeleteDiscipline(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void MakeDisciplineInvalid()
        {
            Discipline discipline = new Discipline() { Name = "Discipline1" };
            var id = _Storage.AddDiscipline(discipline);
            Chapter chapter = new Chapter() { Discipline = discipline, Name = "Chapter1" };
            _Storage.AddChapter(chapter);
            Topic topic = new Topic() { Name = "Topic1", Chapter = chapter, TopicType = _Storage.GetTopicType(1), CourseRef = 1 };
            _Storage.AddTopic(topic);
            _Storage.MakeDisciplineInvalid(id);
            Assert.AreEqual(false, _Storage.GetDiscipline(id).IsValid);
        }
        #endregion

        #region ChapterMethodsTests
        [Test]
        public void AddChapter()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter { Name = "Chapter", Discipline = item }).ToList();
            var ids = chapters.Select(item => _Storage.AddChapter(item)).ToList();
            ids.Select((item, i) => i).ToList().ForEach(item => AdvAssert.AreEqual(chapters[item], _Storage.GetChapter(ids[item])));
            try
            {
                _Storage.AddChapter(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.AddChapter(new Chapter { });
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetChapter()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter { Name = "Chapter", Discipline = item }).ToList();
            var ids = chapters.Select(item => _Storage.AddChapter(item)).ToList();
            ids.Select((item, i) => i).ToList().ForEach(item => AdvAssert.AreEqual(chapters[item], _Storage.GetChapter(ids[item])));
            try
            {
                _Storage.GetChapter(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.AddChapter(new Chapter { Name = "ExistedChapter", Discipline = disciplines[0], Id = ids[0] });
                _Storage.GetChapter(ids[0]);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetChapters()
        {
            var disciplines = CreateDefaultData();
            var curIds = disciplines.Select(item => _Storage.AddDiscipline(item));
            var chapters = disciplines.Select(item => new Chapter { Name = "Chapter", Discipline = item }).ToList();
            var ids = chapters.Select(item => _Storage.AddChapter(item)).ToList();
            AdvAssert.AreEqual(chapters.ToArray(), _Storage.GetChapters(ids).ToArray());
            AdvAssert.AreEqual(chapters[0], _Storage.GetChapters(disciplines[0].Id).First());
            try
            {
                ids.Clear();
                _Storage.GetChapters(ids);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.DeleteDiscipline(disciplines[0].Id);
                _Storage.GetChapters(disciplines[0].Id);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void UpdateChapter()
        {
            Discipline curric = new Discipline() { Name = "Discipline1", Id = 1 };
            Chapter chapter = new Chapter { Name = "Chapter1", Discipline = curric, Id = 1 };
            _Storage.AddChapter(chapter);
            chapter.Name = "ChangedName";
            _Storage.UpdateChapter(chapter);
            AdvAssert.AreEqual(chapter, _Storage.GetChapter(1));
            try
            {
                _Storage.UpdateChapter(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }            
        }
        [Test]
        public void DeleteChapter()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter { Name = "Chapter", Discipline = item }).ToList();
            var ids = chapters.Select(item => _Storage.AddChapter(item)).ToList();
            _Storage.DeleteChapter(ids[0]);
            Assert.AreEqual(null, _Storage.GetChapter(ids[0]));
            try
            {
                _Storage.DeleteChapter(ids[0]);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void DeleteChapters()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter { Name = "Chapter", Discipline = item }).ToList();
            var ids = chapters.Select(item => _Storage.AddChapter(item)).ToList();
            Chapter notDeleted = new Chapter { Name = "NotDeletedChapter", Discipline = disciplines[0] };
            var id = _Storage.AddChapter(notDeleted);
            _Storage.DeleteChapters(ids);
            Assert.AreEqual(0, _Storage.GetChapters(ids).ToArray().Count());
            AdvAssert.AreEqual(notDeleted, _Storage.GetChapter(id));
            try
            {
                _Storage.DeleteChapters(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.DeleteChapters(ids);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void DeleteChapterIfDisciplineIsDeleted()
        {
            Discipline discipline = new Discipline { Name = "Discipline" };
            var currId = _Storage.AddDiscipline(discipline);
            Chapter chapter = new Chapter { Name = "Chapter", Discipline = discipline };
            var chapterId = _Storage.AddChapter(chapter);
            _Storage.DeleteDiscipline(currId);
            Assert.AreEqual(null, _Storage.GetChapter(chapterId));
        }
        #endregion

        #region TopicMethodsTests
        [Test]
        public void AddTopic()
        {
            Discipline cur = new Discipline() { Name = "Discipline" };
            _Storage.AddDiscipline(cur);
            Curriculum as1 = new Curriculum() { Discipline = cur, UserGroupRef = 1 };
            _Storage.AddCurriculum(as1);
            Chapter st = new Chapter() { Name = "Chapter", Discipline = cur };
            _Storage.AddChapter(st);
            Topic topic = new Topic() { Name = "Topic", Chapter = st, TopicType = _Storage.GetTopicType(1) };
            int id = _Storage.AddTopic(topic);
            AdvAssert.AreEqual(topic, _Storage.GetTopic(id));
            Assert.AreEqual(1, _Storage.GetTopicAssignmentsByTopicId(id).Count());
            try
            {
                Topic topic1 = new Topic { Name = "Topic", Chapter = st, TopicType = _Storage.GetTopicType(1) };
                _Storage.AddTopic(topic);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.AddTopic(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetTopic()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group group = userService.GetGroup(1);

            var disciplines = CreateDefaultData();

            var chapters = disciplines.Select(item => new Chapter { Name = "Chapter", Discipline = item }).ToList();
            var topics = chapters.Select(item => new Topic { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(1) }).ToList();
            var ids = topics.Select(item => _Storage.AddTopic(item)).ToList();
            topics.Select((item, i) => i).
                ToList().ForEach(item => AdvAssert.AreEqual(topics[item], _Storage.GetTopic(ids[item])));
            try
            {
                Topic existed = new Topic { Name = "Topic", Chapter = chapters[0], TopicType = _Storage.GetTopicType(1), Id = ids[0] };
                _Storage.AddTopic(existed);
                _Storage.GetTopic(ids[0]);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.GetTopic(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetTopics()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter { Name = "Chapter", Discipline = item })
                .ToList();
            var topics = chapters.Select(item => new Topic { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(1) })
                .ToList();
            var ids = topics.Select(item => _Storage.AddTopic(item))
                .ToList();
            AdvAssert.AreEqual(topics.ToArray(), _Storage.GetTopics(ids).ToArray());
            try
            {
                _Storage.GetTopics(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetTopicsByChapterId()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter { Name = "Chapter", Discipline = item })
                .ToList();
            var topics = chapters.Select(item => new Topic { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(1) })
                .ToList();
            topics.ForEach(item => _Storage.AddTopic(item));
            AdvAssert.AreEqual(topics[0], _Storage.GetTopicsByChapterId(topics[0].Chapter.Id).First());
            try
            {
                _Storage.DeleteTopic(topics[0].Id);
                _Storage.GetTopicsByChapterId(topics[0].Chapter.Id);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetTopicsByDisciplineId()
        {
            var disciplines = CreateDefaultData();
            disciplines.ForEach(item => _Storage.AddDiscipline(item));
            var chapters = disciplines.Select(item => new Chapter { Name = "Chapter", Discipline = item })
                .ToList();
            chapters.ForEach(item => _Storage.AddChapter(item));
            var topics = chapters.Select(item => new Topic { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(1) })
                .ToList();
            topics.Add(new Topic() { Name = "Topic", Chapter = chapters[0], TopicType = _Storage.GetTopicType(1) });
            topics.ForEach(item => _Storage.AddTopic(item));
            List<Topic> expected = new List<Topic>() { topics[0], topics[topics.Count - 1] };
            AdvAssert.AreEqual(expected, _Storage.GetTopicsByDisciplineId(disciplines[0].Id)
                .ToList());
            try
            {
                _Storage.DeleteDiscipline(disciplines[0].Id);
                _Storage.GetTopicsByDisciplineId(disciplines[0].Id);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetTopicsByGroupId()
        {
            List<Discipline> disciplines = CreateDefaultData();
            disciplines.ForEach(item => _Storage.AddDiscipline(item));

            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group group = userService.GetGroup(1);

            var curriculums = disciplines.Select(item => new Curriculum { Discipline = item, UserGroupRef = group.Id })
                .ToList();
            curriculums.ForEach(i => _Storage.AddCurriculum(i));

            var chapters = disciplines.Select(item => new Chapter { Name = "Chapter", Discipline = item })
                .ToList();
            chapters.ForEach(item => _Storage.AddChapter(item));

            var topics = chapters.Select(item => new Topic { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(1) })
                .ToList();
            topics.ForEach(item => _Storage.AddTopic(item));

            AdvAssert.AreEqual(topics, _Storage.GetTopicsByGroupId(group.Id).ToList());
        }
        [Test]
        public void GetTopicsByCourseId()
        {
            Discipline cur = new Discipline() { Name = "Discipline", Id = 1 };
            _Storage.AddDiscipline(cur);

            Chapter chapter = new Chapter() { Name = "Chapter", Discipline = cur, Id = 1 };
            _Storage.AddChapter(chapter);

            Course course = new Course() { Name = "Course", Id = 1 };
            Topic topic = new Topic() { Name = "Topic", Chapter = chapter, TopicType = _Storage.GetTopicType(1), Id = 1, CourseRef = course.Id };
            Topic topic1 = new Topic() { Name = "Topic1", Chapter = chapter, TopicType = _Storage.GetTopicType(1), Id = 2, CourseRef = course.Id };
            _Storage.AddTopic(topic);
            AdvAssert.AreEqual(topic, _Storage.GetTopicsByCourseId(course.Id).First());
            _Storage.AddTopic(topic1);
            List<Topic> expected = new List<Topic>() { topic, topic1 };
            AdvAssert.AreEqual(expected, _Storage.GetTopicsByCourseId(course.Id).ToList());
            try
            {
                _Storage.DeleteTopic(topic.Id);
                _Storage.DeleteTopic(topic1.Id);
                _Storage.GetTopicsByCourseId(course.Id);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetGroupsAssignedToTopic()
        {
            Discipline cur = new Discipline() { Name = "Discipline" };
            Discipline cur1 = new Discipline() { Name = "Discipline1" };

            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr1 = userService.GetGroup(2);
            Group gr2 = userService.GetGroup(1);

            _Storage.AddDiscipline(cur);
            _Storage.AddDiscipline(cur1);

            Curriculum ass = new Curriculum() { Discipline = cur, UserGroupRef = gr1.Id, Id = 1 };
            Curriculum ass1 = new Curriculum() { Discipline = cur1, UserGroupRef = gr2.Id, Id = 2 };
            _Storage.AddCurriculum(ass);
            _Storage.AddCurriculum(ass1);

            Chapter chapter = new Chapter() { Name = "Chapter", Discipline = cur };
            Chapter chapter1 = new Chapter() { Name = "Chapter1", Discipline = cur1 };
            _Storage.AddChapter(chapter);
            _Storage.AddChapter(chapter1);

            Topic topic = new Topic() { Name = "Topic", Chapter = chapter, TopicType = _Storage.GetTopicType(1) };
            Topic topic1 = new Topic() { Name = "Topic1", Chapter = chapter1, TopicType = _Storage.GetTopicType(1) };
            var id = _Storage.AddTopic(topic);
            var id1 = _Storage.AddTopic(topic1);

            Assert.AreEqual(gr1.Id, _Storage.GetGroupsAssignedToTopic(id).First().Id);
            Assert.AreEqual(gr2.Id, _Storage.GetGroupsAssignedToTopic(id1).First().Id);
            _Storage.DeleteTopic(id1);
            try
            {
                _Storage.GetGroupsAssignedToTopic(id1);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
            try
            {
                _Storage.DeleteCurriculum(1);
                _Storage.GetGroupsAssignedToTopic(id);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void GetTopicsAvailableForUser()
        {
            Discipline curr = new Discipline() { Name = "Discipline" };
            Discipline curr1 = new Discipline() { Name = "Discipline1" };
            _Storage.AddDiscipline(curr);
            _Storage.AddDiscipline(curr1);

            DateTime dtStart = new DateTime(2011, 11, 11, 0, 0, 0);
            DateTime dtIn = new DateTime(2040, 11, 11, 0, 0, 0);
            DateTime dtOf = new DateTime(2011, 11, 12, 0, 0, 0);
            Curriculum as1 = new Curriculum() { Discipline = curr, UserGroupRef = 1 };
            Curriculum as2 = new Curriculum() { Discipline = curr1, UserGroupRef = 1 };
            _Storage.AddCurriculum(as1);
            _Storage.AddCurriculum(as2);

            Timeline tml = new Timeline() { Curriculum = as1, StartDate = dtStart, EndDate = dtIn };
            Timeline tml1 = new Timeline() { Curriculum = as2, StartDate = dtStart, EndDate = dtOf };
            _Storage.AddTimeline(tml);
            _Storage.AddTimeline(tml1);

            Chapter st = new Chapter() { Name = "Chapter1", Discipline = curr };
            Chapter st1 = new Chapter() { Name = "Chapter2", Discipline = curr1 };
            _Storage.AddChapter(st);
            _Storage.AddChapter(st1);

            Topic th1 = new Topic() { Name = "Topic1", Chapter = st, TopicType = _Storage.GetTopicType(1) };
            Topic th2 = new Topic() { Name = "Topic2", Chapter = st1, TopicType = _Storage.GetTopicType(1) };
            _Storage.AddTopic(th1);
            _Storage.AddTopic(th2);

            List<TopicDescription> result = new List<TopicDescription> { new TopicDescription() { Topic = th1, Chapter = st, Discipline = curr,
                Timelines = new List<Timeline>() { tml } } };
            IUserService serv = _Tests.LmsService.FindService<IUserService>();
            User us = serv.GetUsers().First();
            AdvAssert.AreEqual(result, _Storage.GetTopicsAvailableForUser(us));

            Timeline tml2 = new Timeline() { ChapterRef = st.Id, Curriculum = as1, StartDate = dtStart, EndDate = new DateTime(2011, 12, 9, 0, 0, 0) };
            _Storage.AddTimeline(tml2);
            result.Clear();
            AdvAssert.AreEqual(result, _Storage.GetTopicsAvailableForUser(us));
            try
            {
                User notExistedUser = new User() { Id = Guid.NewGuid(), Username = "mad", Email = "none@gmail.com", Password = "" };
                _Storage.GetTopicsAvailableForUser(notExistedUser);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void UpdateTopic()
        {
            Discipline cur = new Discipline() { Name = "Discipline" };
            _Storage.AddDiscipline(cur);

            Curriculum as1 = new Curriculum() { Discipline = cur, UserGroupRef = 1 };
            _Storage.AddCurriculum(as1);

            Chapter st = new Chapter() { Name = "Chapter", Discipline = cur };
            _Storage.AddChapter(st);

            Topic topic = new Topic() { Name = "Topic", Chapter = st, TopicType = _Storage.GetTopicType(1) };
            int id = _Storage.AddTopic(topic);
            topic.Name = "UpdatedName";
            _Storage.UpdateTopic(topic);
            AdvAssert.AreEqual(topic, _Storage.GetTopic(id));            
            
            try
            {
                _Storage.UpdateTopic(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void DeleteTopic()
        {
            Discipline cur = new Discipline() { Name = "Discipline" };
            _Storage.AddDiscipline(cur);
            Curriculum as1 = new Curriculum() { Discipline = cur, UserGroupRef = 1 };
            _Storage.AddCurriculum(as1);
            Chapter st = new Chapter() { Name = "Chapter", Discipline = cur };
            _Storage.AddChapter(st);
            Topic topic = new Topic() { Name = "Topic", Chapter = st, TopicType = _Storage.GetTopicType(1) };
            int id = _Storage.AddTopic(topic);
            _Storage.DeleteTopic(id);
            Assert.AreEqual(null, _Storage.GetTopic(id));
            Assert.AreEqual(0, _Storage.GetTopicAssignmentsByTopicId(id).Count());
            try
            {
                _Storage.DeleteTopic(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void DeleteTopics()
        {
            List<Discipline> cur = CreateDefaultData();
            cur.ForEach(item => _Storage.AddDiscipline(item));

            List<Chapter> chapters = cur.Select(item => new Chapter() { Name = "Chapter", Discipline = item })
                .ToList();
            chapters.ForEach(item => _Storage.AddChapter(item));

            List<Topic> topics = chapters.Select(item => new Topic() { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(1) })
                .ToList();
            var ids = topics.Select(item => _Storage.AddTopic(item));

            Topic last = new Topic() { Name = "LastTopic", Chapter = chapters[0], TopicType = _Storage.GetTopicType(1) };
            var id = _Storage.AddTopic(last);

            _Storage.DeleteTopics(ids);
            AdvAssert.AreEqual(last, _Storage.GetTopic(id));
            try
            {
                _Storage.GetTopics(ids);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void TopicUp()
        {
            List<Discipline> cur = CreateDefaultData();
            cur.ForEach(item => _Storage.AddDiscipline(item));

            List<Chapter> chapters = cur.Select(item => new Chapter() { Name = "Chapter", Discipline = item })
                .ToList();
            chapters.ForEach(item => _Storage.AddChapter(item));

            List<Topic> topics = chapters.Select(item => new Topic() { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(1) })
                .ToList();
            Topic topic = new Topic() { Name = "Topic1", Chapter = chapters[0], TopicType = _Storage.GetTopicType(1) };
            var ids = topics.Select(item => _Storage.AddTopic(item))
                .ToList();
            var id = _Storage.AddTopic(topic);
            topic = _Storage.GetTopicsByChapterId(topic.Chapter.Id).ToList()[1];

            _Storage.TopicUp(id);
            AdvAssert.AreEqual(topic, _Storage.GetTopicsByChapterId(topic.Chapter.Id).ToList()[0]);

            _Storage.TopicUp(id);
            AdvAssert.AreEqual(topic, _Storage.GetTopicsByChapterId(topic.Chapter.Id).ToList()[0]);
            try
            {
                _Storage.TopicUp(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void TopicDown()
        {
            List<Discipline> cur = CreateDefaultData();
            cur.ForEach(item => _Storage.AddDiscipline(item));

            List<Chapter> chapters = cur.Select(item => new Chapter() { Name = "Chapter", Discipline = item })
                .ToList();
            chapters.ForEach(item => _Storage.AddChapter(item));

            List<Topic> topics = chapters.Select(item => new Topic() { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(1) })
                .ToList();
            Topic topic = new Topic() { Name = "Topic1", Chapter = chapters[0], TopicType = _Storage.GetTopicType(1) };
            var ids = topics.Select(item => _Storage.AddTopic(item))
                .ToList();
            var id = _Storage.AddTopic(topic);

            _Storage.TopicUp(id);
            topic = _Storage.GetTopicsByChapterId(topic.Chapter.Id).ToList()[0];

            _Storage.TopicDown(id);
            AdvAssert.AreEqual(topic, _Storage.GetTopicsByChapterId(topic.Chapter.Id).ToList()[1]);
            try
            {
                _Storage.TopicDown(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }
        [Test]
        public void DeletingTopicWhenChapterDeleted()
        {
            Discipline cur = new Discipline() { Name = "Discipline" };
            _Storage.AddDiscipline(cur);
            Curriculum as1 = new Curriculum() { Discipline = cur, UserGroupRef = 1 };
            _Storage.AddCurriculum(as1);
            Chapter st = new Chapter() { Name = "Chapter", Discipline = cur };
            var chapterId = _Storage.AddChapter(st);
            Topic topic = new Topic() { Name = "Topic", Chapter = st, TopicType = _Storage.GetTopicType(1) };
            int id = _Storage.AddTopic(topic);
            _Storage.DeleteChapter(chapterId);
            Assert.AreEqual(null, _Storage.GetTopic(id));
        }
        #endregion

        #region CurriculumMethods
        [Test]
        public void AddCurriculum()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);

            var disciplines = CreateDefaultData();
            disciplines.ForEach(item => _Storage.AddDiscipline(item));

            var curriculums = disciplines.Select(item => new Curriculum { Discipline = item, UserGroupRef = gr.Id }).ToList();

            var timelines = curriculums.Select(item => new Timeline()
            {
                Curriculum = item,
                StartDate = new DateTime(2011, 1, 1),
                EndDate = new DateTime(2011, 1, 31)
            }).ToList();
            timelines.ForEach(item => _Storage.AddTimeline(item));

            var chapters = disciplines.Select(item => new Chapter() { Discipline = item, Name = "Chapter" }).ToList();
            var idsSt = chapters.Select(item => _Storage.AddChapter(item)).ToList();

            List<Timeline> chapterTimeline = new List<Timeline>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                chapterTimeline.Add(new Timeline()
                {
                    Curriculum = curriculums[i],
                    ChapterRef = idsSt[i],
                    StartDate = new DateTime(2011, 1, 1 + i * 2),
                    EndDate = new DateTime(2011, 1, 4 + i * 2)
                });
            }
            chapterTimeline.ForEach(item => _Storage.AddTimeline(item));

            var ids = curriculums.Select(item => _Storage.AddCurriculum(item)).ToList();

            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculums[i], _Storage.GetCurriculum(ids[i])));

            try
            {
                _Storage.AddCurriculum(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.AddCurriculum(new Curriculum());
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void UpdateCurriculum()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);

            var disciplines = CreateDefaultData();
            disciplines.ForEach(item => _Storage.AddDiscipline(item));

            var curriculums = disciplines.Select(item => new Curriculum { Discipline = item, UserGroupRef = gr.Id }).ToList();
            var ids = curriculums.Select(item => _Storage.AddCurriculum(item)).ToList();

            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculums[i], _Storage.GetCurriculum(ids[i])));

            var timelines = curriculums.Select(item => new Timeline()
            {
                Curriculum = item,
                StartDate = new DateTime(2011, 12, 1),
                EndDate = new DateTime(2011, 12, 31)
            }).ToList();
            timelines.ForEach(item => _Storage.AddTimeline(item));

            var chapters = disciplines.Select(item => new Chapter() { Discipline = item, Name = "Chapter" }).ToList();
            var idsSt = chapters.Select(item => _Storage.AddChapter(item)).ToList();

            List<Timeline> chapterTimeline = new List<Timeline>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                chapterTimeline.Add(new Timeline()
                {
                    Curriculum = curriculums[i],
                    ChapterRef = idsSt[i],
                    StartDate = new DateTime(2011, 12, 1 + i * 3),
                    EndDate = new DateTime(2011, 12, 4 + i * 3)
                });
            }
            chapterTimeline.ForEach(item => _Storage.AddTimeline(item));

            var topic = chapters.Select(item => new Topic() { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(2) }).ToList();
            topic.ForEach(item => _Storage.AddTopic(item));

            List<TopicAssignment> topicAssignments = new List<TopicAssignment>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                topicAssignments.Add(new TopicAssignment()
                {
                    Curriculum = curriculums[i],
                    Topic = topic[i],
                    MaxScore = i * 5
                });
            }
            topicAssignments.ForEach(item => _Storage.AddTopicAssignment(item));

            curriculums.ForEach(item => _Storage.UpdateCurriculum(item));

            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculums[i], _Storage.GetCurriculum(ids[i])));

            try
            {
                _Storage.UpdateCurriculum(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.UpdateCurriculum(new Curriculum());
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void DeleteCurriculum()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);

            var disciplines = CreateDefaultData();
            disciplines.ForEach(item => _Storage.AddDiscipline(item));

            var curriculums = disciplines.Select(item => new Curriculum { Discipline = item, UserGroupRef = gr.Id }).ToList();

            var timelines = curriculums.Select(item => new Timeline()
            {
                Curriculum = item,
                StartDate = new DateTime(2011, 5, 1),
                EndDate = new DateTime(2011, 5, 31)
            }).ToList();
            var idsT = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            var chapters = disciplines.Select(item => new Chapter() { Discipline = item, Name = "Chapter" }).ToList();
            var idsSt = chapters.Select(item => _Storage.AddChapter(item)).ToList();

            List<Timeline> chapterTimeline = new List<Timeline>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                chapterTimeline.Add(new Timeline()
                {
                    Curriculum = curriculums[i],
                    ChapterRef = idsSt[i],
                    StartDate = new DateTime(2011, 5, 1 + i * 4),
                    EndDate = new DateTime(2011, 5, 4 + i * 4)
                });
            }
            var idsStT = chapterTimeline.Select(item => _Storage.AddTimeline(item)).ToList();

            var topic = chapters.Select(item => new Topic() { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(2) }).ToList();
            topic.ForEach(item => _Storage.AddTopic(item));

            List<TopicAssignment> topicAssignments = new List<TopicAssignment>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                topicAssignments.Add(new TopicAssignment()
                {
                    Curriculum = curriculums[i],
                    Topic = topic[i],
                    MaxScore = i * 5
                });
            }
            var idsThA = topicAssignments.Select(item => _Storage.AddTopicAssignment(item)).ToList();


            var ids = curriculums.Select(item => _Storage.AddCurriculum(item)).ToList();

            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculums[i], _Storage.GetCurriculum(ids[i])));


            curriculums.ForEach(item => _Storage.DeleteCurriculum(item.Id));


            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => Assert.AreEqual(null, _Storage.GetCurriculum(ids[i])));

            Assert.AreEqual(0, _Storage.GetChapterTimelinesByCurriculumId(1).ToList().Count());
            for (int i = 0; i < chapterTimeline.Count; ++i)
            {
                for (int j = 0; j < _Storage.GetChapterTimelinesByCurriculumId(ids[i]).ToList().Count; ++j)
                {
                    Assert.AreEqual(null, _Storage.GetChapterTimelinesByCurriculumId(ids[i]).ToList()[j]);
                }
            }

            //topicAssignments.Select((item, i) => i)
            //    .ToList()
            //    .ForEach(i => Assert.AreEqual(null, _Storage.GetTopicAssignment(idsThA[i])));

            try
            {
                _Storage.DeleteCurriculum(ids.Max() + 1);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void DeleteCurriculums()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);

            var disciplines = CreateDefaultData();
            disciplines.ForEach(item => _Storage.AddDiscipline(item));

            var curriculums = disciplines.Select(item => new Curriculum { Discipline = item, UserGroupRef = gr.Id }).ToList();

            var timelines = curriculums.Select(item => new Timeline()
            {
                Curriculum = item,
                StartDate = new DateTime(2011, 5, 1),
                EndDate = new DateTime(2011, 5, 31)
            }).ToList();
            var idsT = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            var chapters = disciplines.Select(item => new Chapter() { Discipline = item, Name = "Chapter" }).ToList();
            var idsSt = chapters.Select(item => _Storage.AddChapter(item)).ToList();

            List<Timeline> chapterTimeline = new List<Timeline>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                chapterTimeline.Add(new Timeline()
                {
                    Curriculum = curriculums[i],
                    ChapterRef = idsSt[i],
                    StartDate = new DateTime(2011, 5, 1 + i * 4),
                    EndDate = new DateTime(2011, 5, 4 + i * 4)
                });
            }
            var idsStT = chapterTimeline.Select(item => _Storage.AddTimeline(item)).ToList();

            var topic = chapters.Select(item => new Topic() { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(2) }).ToList();
            topic.ForEach(item => _Storage.AddTopic(item));

            List<TopicAssignment> topicAssignments = new List<TopicAssignment>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                topicAssignments.Add(new TopicAssignment()
                {
                    Curriculum = curriculums[i],
                    Topic = topic[i],
                    MaxScore = i * 5
                });
            }
            var idsThA = topicAssignments.Select(item => _Storage.AddTopicAssignment(item)).ToList();


            var ids = curriculums.Select(item => _Storage.AddCurriculum(item)).ToList();

            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculums[i], _Storage.GetCurriculum(ids[i])));


            _Storage.DeleteCurriculums(ids);


            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => Assert.AreEqual(null, _Storage.GetCurriculum(ids[i])));

            for (int i = 0; i < chapterTimeline.Count; ++i)
            {
                for (int j = 0; j < _Storage.GetChapterTimelinesByCurriculumId(ids[i]).ToList().Count; ++j)
                {
                    Assert.AreEqual(null, _Storage.GetChapterTimelinesByCurriculumId(ids[i]).ToList()[j]);
                }
            }
            try
            {
                _Storage.DeleteCurriculums(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void MakeCurriculumsInvalid()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);
            int groupId = gr.Id;

            var disciplines = CreateDefaultData();
            disciplines.ForEach(item => _Storage.AddDiscipline(item));

            var curriculums = disciplines.Select(item => new Curriculum { Discipline = item, UserGroupRef = gr.Id }).ToList();

            var timelines = curriculums.Select(item => new Timeline()
            {
                Curriculum = item,
                StartDate = new DateTime(2011, 5, 1),
                EndDate = new DateTime(2011, 5, 31)
            }).ToList();
            var idsT = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            var chapters = disciplines.Select(item => new Chapter() { Discipline = item, Name = "Chapter" }).ToList();
            var idsSt = chapters.Select(item => _Storage.AddChapter(item)).ToList();

            List<Timeline> chapterTimeline = new List<Timeline>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                chapterTimeline.Add(new Timeline()
                {
                    Curriculum = curriculums[i],
                    ChapterRef = idsSt[i],
                    StartDate = new DateTime(2011, 7, 1 + i * 5),
                    EndDate = new DateTime(2011, 7, 4 + i * 5)
                });
            }
            var idsStT = chapterTimeline.Select(item => _Storage.AddTimeline(item)).ToList();

            var topic = chapters.Select(item => new Topic() { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(2) }).ToList();
            topic.ForEach(item => _Storage.AddTopic(item));

            List<TopicAssignment> topicAssignments = new List<TopicAssignment>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                topicAssignments.Add(new TopicAssignment()
                {
                    Curriculum = curriculums[i],
                    Topic = topic[i],
                    MaxScore = i * 5
                });
            }
            var idsThA = topicAssignments.Select(item => _Storage.AddTopicAssignment(item)).ToList();


            var ids = curriculums.Select(item => _Storage.AddCurriculum(item)).ToList();

            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculums[i], _Storage.GetCurriculum(ids[i])));


            _Storage.MakeCurriculumsInvalid(groupId);


            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => Assert.AreEqual(false, _Storage.GetCurriculum(ids[i]).IsValid));

            try
            {
                _Storage.MakeCurriculumsInvalid(0);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetCurriculum()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);

            Discipline cur = new Discipline() { Name = "Discipline" };
            _Storage.AddDiscipline(cur);

            Curriculum curAss = new Curriculum() { Discipline = cur, UserGroupRef = gr.Id };
            int curAssId = _Storage.AddCurriculum(curAss);

            AdvAssert.AreEqual(curAss, _Storage.GetCurriculum(curAssId));

            try
            {
                _Storage.GetCurriculum(0);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetCurriculums()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);

            Discipline cur = new Discipline() { Name = "Discipline" };
            _Storage.AddDiscipline(cur);

            List<Curriculum> curAss = new List<Curriculum>();
            curAss.Add(new Curriculum() { Discipline = cur, UserGroupRef = gr.Id });

            var curAssId = curAss.Select(item => _Storage.AddCurriculum(item)).ToList();

            Assert.AreEqual(curAss, _Storage.GetCurriculums(curAssId));

            try
            {
                _Storage.GetCurriculums(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetDisciplineAssignmnetsByDisciplineId()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);

            Discipline cur = new Discipline() { Name = "Discipline" };
            var curId = _Storage.AddDiscipline(cur);

            List<Curriculum> curAss = new List<Curriculum>();
            curAss.Add(new Curriculum() { Discipline = cur, UserGroupRef = gr.Id });
            curAss.ForEach(item => _Storage.AddCurriculum(item));

            Assert.AreEqual(curAss, _Storage.GetDisciplineAssignmnetsByDisciplineId(curId).ToList());

            try
            {
                _Storage.GetDisciplineAssignmnetsByDisciplineId(curId + 1);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetCurriculumsByGroupId()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);
            int groupId = gr.Id;

            Discipline cur = new Discipline() { Name = "Discipline" };
            _Storage.AddDiscipline(cur);

            List<Curriculum> curAss = new List<Curriculum>();
            curAss.Add(new Curriculum() { Discipline = cur, UserGroupRef = gr.Id });
            curAss.ForEach(item => _Storage.AddCurriculum(item));

            Assert.AreEqual(curAss, _Storage.GetCurriculumsByGroupId(groupId).ToList());

            try
            {
                _Storage.GetDisciplineAssignmnetsByDisciplineId(groupId + 1);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetDisciplineASsignments()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);
            Group group = userService.GetGroup(1);

            Discipline cur = new Discipline() { Name = "Discipline" };
            _Storage.AddDiscipline(cur);

            List<Curriculum> curAss = new List<Curriculum>();
            curAss.Add(new Curriculum() { Discipline = cur, UserGroupRef = gr.Id });
            curAss.Add(new Curriculum() { Discipline = cur, UserGroupRef = group.Id });
            curAss.Add(new Curriculum() { Discipline = cur, UserGroupRef = gr.Id });
            curAss.ForEach(item => _Storage.AddCurriculum(item));

            Assert.AreEqual(curAss, _Storage.GetCurriculums().ToList());

        }
        #endregion

        #region TimelineMethods
        [Test]
        public void AddTimeline()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select(item => new Curriculum { Discipline = item }).ToList();
            var timelines = curriculum.Select(item => new Timeline { Curriculum = item }).ToList();

            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimeline(ids[i])));

            try
            {
                _Storage.AddTimeline(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.AddTimeline(new Timeline { });
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetTimeline()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum { Discipline = item }).ToList();
            var timelines = curriculum.Select(item => new Timeline { Curriculum = item }).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));
            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimeline(timelines[i].Id)));

            try
            {
                _Storage.GetTimeline(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetCurriculumTimelines()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum { Discipline = item, Id = 1 }).ToList();
            var timelines = curriculum.Select(item => new Timeline { Curriculum = item }).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));
            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetCurriculumTimelines(timelines[i].Curriculum.Id).ToList()[i]));

            try
            {
                _Storage.GetCurriculumTimelines(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetChapterTimelinesByCurriculumId()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum { Discipline = item, Id = 1 }).ToList();
            var timelines = curriculum.Select(item => new Timeline { Curriculum = item, ChapterRef = 1 }).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetChapterTimelinesByCurriculumId(timelines[i].Curriculum.Id).ToList()[i]));

            try
            {
                _Storage.GetChapterTimelinesByCurriculumId(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetChapterTimelinesByChapterId()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum { Discipline = item }).ToList();
            var timelines = curriculum.Select(item => new Timeline { Curriculum = item, ChapterRef = 1 }).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetChapterTimelinesByChapterId(timelines[i].ChapterRef.Value).ToList()[i]));

            try
            {
                _Storage.GetChapterTimelinesByChapterId(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetChapterTimelines()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum { Discipline = item, Id = 1 }).ToList();
            var timelines = curriculum.Select(item => new Timeline { Curriculum = item, ChapterRef = 1 }).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetChapterTimelines(timelines[i].ChapterRef.Value, timelines[i].Curriculum.Id).ToList()[i]));

            try
            {
                _Storage.GetChapterTimelines(0, 0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.GetChapterTimelines(0, timelines[0].Curriculum.Id);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.GetChapterTimelines(timelines[0].ChapterRef.Value, 0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetTimelines()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum { Discipline = item }).ToList();
            var timelines = curriculum.Select(item => new Timeline { Curriculum = item, Id = 1 }).ToList();

            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();
            Assert.AreEqual(timelines.Count, ids.Count);
            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimelines(ids).ToList()[i]));

            try
            {
                _Storage.GetTimelines(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void UpdateTimeline()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum { Discipline = item }).ToList();
            var timelines = curriculum.Select(item => new Timeline { Curriculum = item, Id = 1 }).ToList();

            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimeline(ids[i])));

            timelines.ForEach(item => item.EndDate = System.DateTime.Now);

            timelines.ForEach(item => _Storage.UpdateTimeline(item));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimeline(ids[i])));

            try
            {
                _Storage.UpdateTimeline(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.UpdateTimeline(new Timeline());
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void DeleteTimeline()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum { Discipline = item }).ToList();
            List<Timeline> timelines = new List<Timeline>();
            for (int i = 0; i < 4; ++i)
            {
                timelines.Add(new Timeline() { Curriculum = curriculum[i], Id = i });
            }
            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            timelines.ForEach(item => _Storage.DeleteTimeline(item.Id));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => Assert.AreEqual(null, _Storage.GetTimeline(ids[i])));

            try
            {
                _Storage.DeleteTimeline(5);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void DeleteTimelines()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum { Discipline = item }).ToList();
            List<Timeline> timelines = new List<Timeline>();
            for (int i = 0; i < 4; ++i)
            {
                timelines.Add(new Timeline() { Curriculum = curriculum[i], Id = i });
            }
            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            _Storage.DeleteTimelines(ids);

            timelines.Select((item, i) => i)
                    .ToList()
                    .ForEach(i => Assert.AreEqual(null, _Storage.GetTimeline(ids[i])));

            try
            {
                _Storage.DeleteTimelines(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        #endregion

        #region TopicAssignmentMethods
        [Test]
        public void AddTopicAssignment()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter() { Discipline = item, Id = 1, Name = "chapter" }).ToList();
            var topic = chapters.Select(item => new Topic() { Chapter = item, Name = "topic" }).ToList();
            var topicassignment = topic.Select(item => new TopicAssignment() { Topic = item }).ToList();
            for (int i = 0; i < topicassignment.Count; ++i)
            {
                topicassignment[i].Id = i;
            }

            var ids = topicassignment.Select(item => _Storage.AddTopicAssignment(item)).ToList();

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(topicassignment[i], _Storage.GetTopicAssignment(ids[i])));

            try
            {
                _Storage.AddTopicAssignment(new TopicAssignment());
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.AddTopicAssignment(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetTopicAssignment()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter() { Discipline = item, Id = 1, Name = "chapter" }).ToList();
            var topic = chapters.Select(item => new Topic() { Chapter = item, Name = "topic" }).ToList();
            var topicassignment = topic.Select(item => new TopicAssignment() { Topic = item }).ToList();
            for (int i = 0; i < topicassignment.Count; ++i)
            {
                topicassignment[i].Id = i;
            }

            topicassignment.ForEach(item => _Storage.AddTopicAssignment(item));

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(topicassignment[i], _Storage.GetTopicAssignment(topicassignment[i].Id)));

            try
            {
                _Storage.GetTopicAssignment(5);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetTopicAssignmentsByCurriculumId()
        {
            var disciplines = CreateDefaultData();
            var disciplinesassignment = disciplines.Select(item => new Curriculum() { Discipline = item, Id = 1 }).ToList();
            var chapters = disciplines.Select(item => new Chapter() { Discipline = item, Id = 1, Name = "chapter" }).ToList();
            var topic = chapters.Select(item => new Topic() { Chapter = item, Name = "topic" }).ToList();
            var topicassignment = topic.Select(item => new TopicAssignment() { Topic = item }).ToList();
            for (int i = 0; i < topicassignment.Count; ++i)
            {
                topicassignment[i].Curriculum = disciplinesassignment[i];
            }
            topicassignment.ForEach(item => _Storage.AddTopicAssignment(item));

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(topicassignment[i], _Storage.GetTopicAssignmentsByCurriculumId(topicassignment[i].Curriculum.Id).ToList()[i]));

            try
            {
                _Storage.GetTopicAssignmentsByCurriculumId(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetTopicAssignmentsByTopicId()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter() { Discipline = item, Name = "chapter" }).ToList();
            var topic = chapters.Select(item => new Topic() { Chapter = item, Name = "topic", Id = 1 }).ToList();

            var topicassignment = topic.Select(item => new TopicAssignment() { Topic = item }).ToList();

            topicassignment.ForEach(item => _Storage.AddTopicAssignment(item));

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(topicassignment[i], _Storage.GetTopicAssignmentsByTopicId(topicassignment[i].Topic.Id).ToList()[i]));

            try
            {
                _Storage.GetTopicAssignmentsByTopicId(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void GetTopicAssignments()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter() { Discipline = item, Name = "chapter" }).ToList();
            var topic = chapters.Select(item => new Topic() { Chapter = item, Name = "topic", Id = 1 }).ToList();

            var topicassignment = topic.Select(item => new TopicAssignment() { Topic = item }).ToList();

            var ids = topicassignment.Select(item => _Storage.AddTopicAssignment(item)).ToList();

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(topicassignment[i], _Storage.GetTopicAssignments(ids).ToList()[i]));

            try
            {
                _Storage.GetTopicAssignments(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void UpdateTopicAssignment()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter() { Discipline = item, Name = "chapter" }).ToList();
            var topic = chapters.Select(item => new Topic() { Chapter = item, Name = "topic", Id = 1 }).ToList();

            var topicassignment = topic.Select(item => new TopicAssignment() { Topic = item }).ToList();
            for (int i = 0; i < topicassignment.Count; ++i)
            {
                topicassignment[i].MaxScore = 20 * i;
                topicassignment[i].Id = i;
            }
            topicassignment.ForEach(item => _Storage.AddTopicAssignment(item));

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(topicassignment[i], _Storage.GetTopicAssignment(topicassignment[i].Id)));


            topicassignment.ForEach(item => item.MaxScore = 0);
            topicassignment.ForEach(item => _Storage.UpdateTopicAssignment(item));

            topicassignment.Select((item, i) => i)
               .ToList()
               .ForEach(i => AdvAssert.AreEqual(topicassignment[i], _Storage.GetTopicAssignment(topicassignment[i].Id)));

            try
            {
                _Storage.UpdateTopicAssignment(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.UpdateTopicAssignment(new TopicAssignment());
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }
        [Test]
        public void DeleteTopicAssignments()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter() { Discipline = item, Name = "chapter" }).ToList();
            var topic = chapters.Select(item => new Topic() { Chapter = item, Name = "topic", Id = 1 }).ToList();

            var topicassignment = topic.Select(item => new TopicAssignment() { Topic = item }).ToList();
            for (int i = 0; i < topicassignment.Count; ++i)
            {
                topicassignment[i].Id = i;
            }
            var ids = topicassignment.Select(item => _Storage.AddTopicAssignment(item)).ToList();

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(topicassignment[i], _Storage.GetTopicAssignment(ids[i])));

            _Storage.DeleteTopicAssignments(ids);

            topicassignment.Select((item, i) => i)
               .ToList()
               .ForEach(i => Assert.AreEqual(null, _Storage.GetTopicAssignment(ids[i])));

        }
        #endregion

        #region ReactionToDeleting
        [Test]
        public void DeletingGroup()
        {
            Group gr = _Tests.UserStorage.GetGroup(1);
            Discipline cur = new Discipline() { Name = "Discipline" };
            var id = _Storage.AddDiscipline(cur);

            Curriculum ass = new Curriculum() { Discipline = cur, UserGroupRef = gr.Id };
            _Storage.AddCurriculum(ass);

            _Tests.UserStorage.DeleteGroup(gr.Id);
            cur.IsValid = false;
            _Storage.UpdateDiscipline(cur);
            Discipline c = new Discipline();

            Assert.AreEqual(false, _Storage.GetDiscipline(id).IsValid);
        }
        [Test]
        public void DeletingUser()
        {
            Guid g = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            User us = _Tests.UserStorage.GetUser(item => item.Id == g);
            Discipline cur = new Discipline() { Name = "Discipline", Owner = us.Username };
            var id = _Storage.AddDiscipline(cur);

            _Tests.UserStorage.DeleteUser(item => item.Id == us.Id);
            Assert.AreEqual(false, _Storage.GetDiscipline(id).IsValid);
        }
        [Test]
        public void DeleteCourse()
        {
            Course course = _Tests.CourseStorage.GetCourse(1);
            Discipline cur = new Discipline() { Name = "Discipline" };
            var currId= _Storage.AddDiscipline(cur);
            Curriculum as1 = new Curriculum() { Discipline = cur, UserGroupRef = 1 };
            _Storage.AddCurriculum(as1);
            Chapter st = new Chapter() { Name = "Chapter", Discipline = cur };
            var chapterId = _Storage.AddChapter(st);
            Topic topic = new Topic() { Name = "Topic", Chapter = st, TopicType = _Storage.GetTopicType(1), CourseRef=course.Id };
            _Storage.AddTopic(topic);
            _Tests.CourseStorage.DeleteCourse(course.Id);
            Assert.AreEqual(false, _Storage.GetDiscipline(currId).IsValid);
        }
        #endregion
    }
}
