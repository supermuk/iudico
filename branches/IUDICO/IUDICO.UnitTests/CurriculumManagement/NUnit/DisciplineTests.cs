using System;
using System.Linq;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.DisciplineManagement.Controllers;
using IUDICO.DisciplineManagement.Models;
using NUnit.Framework;

namespace IUDICO.UnitTests.CurriculumManagement.NUnit
{
    [TestFixture]
    public class DisciplineTests : BaseTestFixture
    {
        #region PrivateHelpers

        #endregion

        #region Discipline tests

        [Test]
        public void AddDiscipline()
        {
            var expectedItems = this.DataPreparer.GetDisciplines();
            var controller = GetController<DisciplineController>();

            // add disciplines
            expectedItems.ForEach(item => controller.Create(item));
            // then add "special" discipline
            controller.Create(new Discipline { IsValid = false, Owner = "ozo", IsDeleted = true });

            expectedItems = this.DataPreparer.GetDisciplines();
            var allItems = this.Storage.GetDisciplines()
                .OrderBy(item => item.Id)
                .ToList();
            var actualItems = allItems
                .Take(expectedItems.Count).ToList();
            var actualItem = allItems.Last();

            AdvAssert.AreEqual(expectedItems, actualItems);
            Assert.AreEqual(this.UserService.GetCurrentUser().Username, actualItem.Owner);
            Assert.AreEqual(false, actualItem.IsDeleted);
            Assert.AreEqual(true, actualItem.IsValid);
        }

        [Test]
        public void GetDiscipline()
        {
            var ids = this.DataPreparer.CreateDisciplinesSet1();
            var expectedItems = this.DataPreparer.GetDisciplines();
            expectedItems.Select((item, i) => i).ToList()
                .ForEach(i => AdvAssert.AreEqual(expectedItems[i], this.Storage.GetDiscipline(ids[i])));
        }

        [Test]
        public void GetDisciplines()
        {
            var ids = this.DataPreparer.CreateDisciplinesSet2().Take(2);
            var expectedItems = this.DataPreparer.GetDisciplines();

            // Tests GetDisciplines(IEnumerable<int> ids)
            AdvAssert.AreEqual(expectedItems.Take(2).ToList(), this.Storage.GetDisciplines(ids));

            // Tests GetDisciplines()
            AdvAssert.AreEqual(expectedItems, this.Storage.GetDisciplines());

            // Tests GetDisciplines(Func<Discipline, bool>)
            AdvAssert.AreEqual(
                expectedItems.Where(item => item.Owner == Users.Panza).ToList(),
                this.Storage.GetDisciplines(item => item.Owner == Users.Panza));

            // Tests GetDisciplinesByGroupId(groupId)
            var groupId = this.UserService.GetGroup(1).Id;
            AdvAssert.AreEqual(
                new[] { expectedItems[0], expectedItems[2] }, this.Storage.GetDisciplinesByGroupId(groupId));

            // Tests GetDisciplines(User owner)
            this.tests.SetCurrentUser(Users.Ozo);
            Func<Discipline> expectedItemF = () => new Discipline { Name = "Discipline5", Owner = Users.Ozo, IsValid = true };
            this.Storage.AddDiscipline(expectedItemF());
            var currentUser = this.UserService.GetCurrentUser();
            var actualItems = this.Storage.GetDisciplines(currentUser);
            AdvAssert.AreEqual(new[] { expectedItemF() }, actualItems);
        }

        [Test]
        public void UpdateDiscipline()
        {
            var ids = this.DataPreparer.CreateDisciplinesSet1();
            var controller = GetController<DisciplineController>();
            Func<Discipline> newDisciplineF = () => new Discipline
            {
                Name = "NewDiscipline",
                Owner = Users.Ozo,
                IsValid = false,
                IsDeleted = true,
                Id = ids[1]
            };
            controller.Edit(ids[1], newDisciplineF());

            var expected = newDisciplineF();
            var actual = this.Storage.GetDiscipline(ids[1]);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(Users.Panza, actual.Owner);
            Assert.AreEqual(true, actual.IsValid);
            Assert.AreEqual(false, actual.IsDeleted);
        }

        [Test]
        public void DeleteDisciplines()
        {
            this.DataPreparer.CreateDisciplinesSet3();
            var disciplineIds = this.DataPreparer.DisciplineIds;
            var controller = GetController<DisciplineController>();

            // delete 1 item
            var result = controller.DeleteItem(disciplineIds[1]);
            Assert.AreEqual(null, this.Storage.GetDiscipline(disciplineIds[1]));
            Assert.AreEqual(disciplineIds.Count - 1, this.Storage.GetDisciplines().Count);
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = true"));
            // check curriculums also deleted
            Assert.AreEqual(0, this.CurriculumStorage.GetCurriculums(c => c.DisciplineRef == disciplineIds[1]).Count);
            // check stages also deleted
            Assert.AreEqual(0, this.Storage.GetChapters(item => item.DisciplineRef == disciplineIds[1]).Count);

            // error deletion
            result = controller.DeleteItem(disciplineIds[1]);
            Assert.AreEqual(null, this.Storage.GetDiscipline(disciplineIds[1]));
            Assert.AreEqual(disciplineIds.Count - 1, this.Storage.GetDisciplines().Count);
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = false"));

            // delete items
            disciplineIds.RemoveAt(1);
            controller.DeleteItems(disciplineIds.ToArray());
            Assert.AreEqual(0, this.Storage.GetDisciplines().Count);

            // error deletion many items
            result = controller.DeleteItems(disciplineIds.ToArray());
            disciplineIds.ForEach(i => Assert.AreEqual(null, this.Storage.GetDiscipline(i)));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = false"));
        }

/*        [Test]
        public void MakeDisciplineInvalid()
        {
            Discipline discipline = new Discipline { Name = "Discipline1" };
            var id = _Storage.AddDiscipline(discipline);
            Chapter chapter = new Chapter { Discipline = discipline, Name = "Chapter1" };
            _Storage.AddChapter(chapter);
            Topic topic = new Topic { Name = "Topic1", Chapter = chapter, TopicType = _Storage.GetTopicType(1), CourseRef = 1 };
            _Storage.AddTopic(topic);
            _Storage.MakeDisciplineInvalid(id);
            Assert.AreEqual(false, _Storage.GetDiscipline(id).IsValid);
        }*/

        #endregion

        #region Chapter tests

        [Test]
        public void AddChapter()
        {
            this.DataPreparer.CreateDisciplinesSet2();
            var expectedItems = this.DataPreparer.GetChapters();
            var controller = GetController<ChapterController>();

            expectedItems.ForEach(item => controller.Create(item, item.DisciplineRef));

            expectedItems = this.DataPreparer.GetChapters();
            var actualItems = this.DataPreparer.DisciplineIds.SelectMany(id => this.Storage.GetChapters(item => item.DisciplineRef == id))
                .OrderBy(item => item.Id)
                .ToList();
            AdvAssert.AreEqual(expectedItems, actualItems);

            // check authomatically added curriculum chapters
            actualItems.ForEach(item => Assert.AreEqual(1, this.CurriculumStorage.GetCurriculumChapters(c => c.ChapterRef == item.Id).Count));
        }

        [Test]
        public void GetChapter()
        {
            var chapterIds = this.DataPreparer.CreateDisciplinesSet3();
            var expectedItems = this.DataPreparer.GetChapters();
            expectedItems.Select((item, i) => i).ToList()
                .ForEach(i => AdvAssert.AreEqual(expectedItems[i], this.Storage.GetChapter(chapterIds[i])));
        }

        [Test]
        public void GetChapters()
        {
            var chapterIds = this.DataPreparer.CreateDisciplinesSet3().Take(2).ToList();
            var expectedItems = this.DataPreparer.GetChapters();

            // Tests GetChapters(IEnumerable<int> ids)
            AdvAssert.AreEqual(expectedItems.Take(2).ToList(), this.Storage.GetChapters(chapterIds));

            // Tests GetChapters(Func<Chapter, bool>)
            AdvAssert.AreEqual(
                expectedItems.Where(item => item.Name == expectedItems[0].Name).ToList(),
                this.Storage.GetChapters(item => item.Name == expectedItems[0].Name));
        }

        [Test]
        public void UpdateChapter()
        {
            var controller = GetController<ChapterController>();
            var chapterIds = this.DataPreparer.CreateDisciplinesSet3();

            Func<Chapter> newDisciplineF = () => new Chapter
            {
                Name = "NewChapter",
                IsDeleted = true,
                Id = chapterIds[1],
                DisciplineRef = 100,
            };
            controller.Edit(chapterIds[1], newDisciplineF());

            var expected = newDisciplineF();
            var actual = this.Storage.GetChapter(chapterIds[1]);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(false, actual.IsDeleted);
            Assert.AreEqual(chapterIds[1], actual.Id);
            Assert.AreEqual(this.DataPreparer.GetChapters()[1].DisciplineRef, actual.DisciplineRef);
        }

        [Test]
        public void DeleteChapters()
        {
            var controller = GetController<ChapterController>();
            var chapterIds = this.DataPreparer.CreateDisciplinesSet3();

            // delete 1 item
            var result = controller.DeleteItem(chapterIds[1]);
            Assert.AreEqual(null, this.Storage.GetChapter(chapterIds[1]));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = true"));
            // check curriculumChapters also deleted
            Assert.AreEqual(0, this.CurriculumStorage.GetCurriculumChapters(c => c.ChapterRef == chapterIds[1]).Count);

            // error deletion
            result = controller.DeleteItem(chapterIds[1]);
            Assert.AreEqual(null, this.Storage.GetChapter(chapterIds[1]));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = false"));

            // delete items
            chapterIds.RemoveAt(1);
            result = controller.DeleteItems(chapterIds.ToArray());
            chapterIds.ForEach(i => Assert.AreEqual(null, this.Storage.GetChapter(i)));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = true"));

            // error deletion many items
            result = controller.DeleteItems(chapterIds.ToArray());
            chapterIds.ForEach(i => Assert.AreEqual(null, this.Storage.GetChapter(i)));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = false"));
        }

        #endregion

        #region Topic tests

        [Test]
        public void AddTopic()
        {
            this.DataPreparer.CreateDisciplinesSet3();
            var expectedItems = this.DataPreparer.GetTopics();
            expectedItems[0].IsDeleted = true; // for test
            var controller = GetController<TopicController>();

            expectedItems.ForEach(item => controller.Create(item.ToCreateModel()));

            expectedItems = this.DataPreparer.GetTopics();
            var actualItems = this.DataPreparer.ChapterIds.SelectMany(id => this.Storage.GetTopics(item => item.ChapterRef == id))
                .OrderBy(item => item.Id)
                .ToList();
            AdvAssert.AreEqual(expectedItems, actualItems);
            Assert.AreNotEqual(expectedItems.Last().SortOrder, actualItems.Last().SortOrder);

            // check authomatically added curriculum chapter topics
            actualItems.ForEach(item => Assert.AreEqual(1, this.CurriculumStorage.GetCurriculumChapterTopics(c => c.TopicRef == item.Id).Count));

            var chapterId = this.DataPreparer.ChapterIds[0];
            // add bad topic
            controller = GetController<TopicController>();
            var badTopic = new Topic
            {
                TestCourseRef = null,
                TestTopicTypeRef = null,
                TheoryCourseRef = null,
                TheoryTopicTypeRef = null,
                Name = "BadTopic",
                ChapterRef = chapterId,
            };
            controller.Create(badTopic.ToCreateModel());
            Assert.AreEqual(false, controller.ModelState.IsValid);
            Assert.AreEqual(expectedItems.Count, this.DataPreparer.ChapterIds.Sum(id => this.Storage.GetTopics(item => item.ChapterRef == id).Count));

            // add bad topic
            controller = GetController<TopicController>();
            badTopic = this.DataPreparer.GetTopics()[0];
            badTopic.Name = Enumerable.Repeat('A', 60).ToString();
            controller.Create(badTopic.ToCreateModel());
            Assert.AreEqual(false, controller.ModelState.IsValid);
            Assert.AreEqual(expectedItems.Count, this.DataPreparer.ChapterIds.Sum(id => this.Storage.GetTopics(item => item.ChapterRef == id).Count));
        }

        [Test]
        public void GetTopic()
        {
            var topicIds = this.DataPreparer.CreateDisciplinesSet4();
            var expectedItems = this.DataPreparer.GetTopics();
            expectedItems.Select((item, i) => i).ToList()
                .ForEach(i => AdvAssert.AreEqual(expectedItems[i], this.Storage.GetTopic(topicIds[i])));
        }

        [Test]
        public void GetTopics()
        {
            var topicIds = this.DataPreparer.CreateDisciplinesSet4().Take(2).ToList();
            var expectedItems = this.DataPreparer.GetTopics();
            var topicsPerDiscipline = expectedItems.Count / 3;

            // Tests GetTopics(IEnumerable<int> ids)
            AdvAssert.AreEqual(expectedItems.Take(2).ToList(), this.Storage.GetTopics(topicIds));

            // Tests GetTopics(Func<Discipline, bool>)
            AdvAssert.AreEqual(
                expectedItems.Where(item => item.Name == expectedItems[0].Name).ToList(),
                this.Storage.GetTopics(item => item.Name == expectedItems[0].Name));

            // Tests GetTopicsByCourseId()
            var courseId = this.CourseService.GetCourse(2).Id;
            AdvAssert.AreEqual(
                expectedItems.Where(item => item.TestCourseRef == courseId || item.TheoryCourseRef == courseId).ToList(),
                this.Storage.GetTopicsByCourseId(courseId));

            // Tests GetTopicsByDisciplineId()
            // 1/3 of added topics belongs to first discipline and first stage
            var disciplineId = this.Storage.GetDisciplines().First().Id;
            var items = expectedItems.OrderBy(item => item.ChapterRef)
                .Take(topicsPerDiscipline)
                .ToList();
            AdvAssert.AreEqual(items, this.Storage.GetTopicsByDisciplineId(disciplineId));

            // Tests GetTopicsByDisciplineId()
            // first and third discipline connected to group1
            var groupId = this.UserService.GetGroup(1).Id;
            items = expectedItems.OrderBy(item => item.ChapterRef).ToList();
            items = items.Take(topicsPerDiscipline)
                .Union(items.Skip(2 * topicsPerDiscipline).Take(topicsPerDiscipline))
                .ToList();
            AdvAssert.AreEqual(items, this.Storage.GetTopicsByGroupId(groupId));

            // Tests GetTopicsOwnedByUser()
            // add discipline owned by another guy.
            var user = this.UserService.GetCurrentUser();
            this.tests.SetCurrentUser(Users.Ozo);
            disciplineId = this.Storage.AddDiscipline(new Discipline { Name = "Discipline5", Owner = Users.Ozo });
            var chapterId = this.Storage.AddChapter(new Chapter { Name = "Chapter", DisciplineRef = disciplineId });
            this.Storage.AddTopic(new Topic { Name = "Topic", ChapterRef = chapterId });

            items = expectedItems.OrderBy(item => item.ChapterRef).ToList();
            AdvAssert.AreEqual(items, this.Storage.GetTopicsOwnedByUser(user));
        }

/*        [Test]
        public void GetGroupsAssignedToTopic()
        {
            Discipline cur = new Discipline { Name = "Discipline" };
            Discipline cur1 = new Discipline { Name = "Discipline1" };

            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr1 = userService.GetGroup(2);
            Group gr2 = userService.GetGroup(1);

            _Storage.AddDiscipline(cur);
            _Storage.AddDiscipline(cur1);

            Curriculum ass = new Curriculum { Discipline = cur, UserGroupRef = gr1.Id, Id = 1 };
            Curriculum ass1 = new Curriculum { Discipline = cur1, UserGroupRef = gr2.Id, Id = 2 };
            _Storage.AddCurriculum(ass);
            _Storage.AddCurriculum(ass1);

            Chapter chapter = new Chapter { Name = "Chapter", Discipline = cur };
            Chapter chapter1 = new Chapter { Name = "Chapter1", Discipline = cur1 };
            _Storage.AddChapter(chapter);
            _Storage.AddChapter(chapter1);

            Topic topic = new Topic { Name = "Topic", Chapter = chapter, TopicType = _Storage.GetTopicType(1) };
            Topic topic1 = new Topic { Name = "Topic1", Chapter = chapter1, TopicType = _Storage.GetTopicType(1) };
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
            Discipline curr = new Discipline { Name = "Discipline" };
            Discipline curr1 = new Discipline { Name = "Discipline1" };
            _Storage.AddDiscipline(curr);
            _Storage.AddDiscipline(curr1);

            DateTime dtStart = new DateTime(2011, 11, 11, 0, 0, 0);
            DateTime dtIn = new DateTime(2040, 11, 11, 0, 0, 0);
            DateTime dtOf = new DateTime(2011, 11, 12, 0, 0, 0);
            Curriculum as1 = new Curriculum { Discipline = curr, UserGroupRef = 1 };
            Curriculum as2 = new Curriculum { Discipline = curr1, UserGroupRef = 1 };
            _Storage.AddCurriculum(as1);
            _Storage.AddCurriculum(as2);

            Timeline tml = new Timeline { Curriculum = as1, StartDate = dtStart, EndDate = dtIn };
            Timeline tml1 = new Timeline { Curriculum = as2, StartDate = dtStart, EndDate = dtOf };
            _Storage.AddTimeline(tml);
            _Storage.AddTimeline(tml1);

            Chapter st = new Chapter { Name = "Chapter1", Discipline = curr };
            Chapter st1 = new Chapter { Name = "Chapter2", Discipline = curr1 };
            _Storage.AddChapter(st);
            _Storage.AddChapter(st1);

            Topic th1 = new Topic { Name = "Topic1", Chapter = st, TopicType = _Storage.GetTopicType(1) };
            Topic th2 = new Topic { Name = "Topic2", Chapter = st1, TopicType = _Storage.GetTopicType(1) };
            _Storage.AddTopic(th1);
            _Storage.AddTopic(th2);

            List<TopicDescription> result = new List<TopicDescription>
                                                {
                                                    new TopicDescription
                                                        {
                                                            Topic = th1,
                                                            Chapter = st,
                                                            Discipline = curr,
                                                            Timelines = new List<Timeline> {tml}
                                                        }
                                                };
            IUserService serv = _Tests.LmsService.FindService<IUserService>();
            User us = serv.GetUsers().First();
            AdvAssert.AreEqual(result, _Storage.GetTopicsAvailableForUser(us));

            Timeline tml2 = new Timeline
                                {
                                    ChapterRef = st.Id,
                                    Curriculum = as1,
                                    StartDate = dtStart,
                                    EndDate = new DateTime(2011, 12, 9, 0, 0, 0)
                                };
            _Storage.AddTimeline(tml2);
            result.Clear();
            AdvAssert.AreEqual(result, _Storage.GetTopicsAvailableForUser(us));
            try
            {
                User notExistedUser = new User
                                          {
                                              Id = Guid.NewGuid(),
                                              Username = "mad",
                                              Email = "none@gmail.com",
                                              Password = ""
                                          };
                _Storage.GetTopicsAvailableForUser(notExistedUser);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }*/

        [Test]
        public void UpdateTopic()
        {
            var controller = GetController<TopicController>();
            var topicIds = this.DataPreparer.CreateDisciplinesSet4();

            Func<Topic> newTopicF = () => new Topic
            {
                SortOrder = 3,
                TestCourseRef = null,
                TestTopicTypeRef = null,
                TheoryCourseRef = this.CourseService.GetCourse(2).Id,
                TheoryTopicTypeRef = this.Storage.GetTheoryTopicTypes().First(item => item.ToTopicTypeEnum() == TopicTypeEnum.Theory).Id,
                Name = "NewTopic",
                ChapterRef = 100,
                IsDeleted = true
            };
            controller.Edit(topicIds[0], newTopicF().ToCreateModel());

            var expected = newTopicF();
            var actual = this.Storage.GetTopic(topicIds[0]);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.TestCourseRef, actual.TestCourseRef);
            Assert.AreEqual(expected.TestTopicTypeRef, actual.TestTopicTypeRef);
            Assert.AreEqual(expected.TheoryCourseRef, actual.TheoryCourseRef);
            Assert.AreEqual(expected.TheoryTopicTypeRef, actual.TheoryTopicTypeRef);
            Assert.AreEqual(false, actual.IsDeleted);
            Assert.AreEqual(topicIds[0], actual.Id);
            Assert.AreEqual(this.DataPreparer.GetTopics()[0].ChapterRef, actual.ChapterRef);
        }

        [Test]
        public void DeleteTopics()
        {
            var controller = GetController<TopicController>();
            var topicIds = this.DataPreparer.CreateDisciplinesSet4();

            // delete 1 item
            var result = controller.DeleteItem(topicIds[1]);
            Assert.AreEqual(null, this.Storage.GetTopic(topicIds[1]));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = true"));
            // check curriculumChapterTopics also deleted
            Assert.AreEqual(0, this.CurriculumStorage.GetCurriculumChapterTopics(c => c.TopicRef == topicIds[1]).Count);

            // error deletion
            result = controller.DeleteItem(topicIds[1]);
            Assert.AreEqual(null, this.Storage.GetTopic(topicIds[1]));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = false"));

            // delete items
            topicIds.RemoveAt(1);
            result = controller.DeleteItems(topicIds.ToArray());
            topicIds.ForEach(i => Assert.AreEqual(null, this.Storage.GetTopic(i)));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = true"));

            // error deletion many items
            result = controller.DeleteItems(topicIds.ToArray());
            topicIds.ForEach(i => Assert.AreEqual(null, this.Storage.GetTopic(i)));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = false"));
        }

        [Test]
        public void TopicUp()
        {
            var controller = GetController<TopicController>();
            this.DataPreparer.CreateDisciplinesSet4();
            var topics = this.Storage.GetTopics(item => item.ChapterRef == this.DataPreparer.ChapterIds[0])
                .OrderBy(item => item.SortOrder)
                .ToList();
            var topic1 = topics[0];
            var topic2 = topics[1];
            var oldSortOrder1 = topic1.SortOrder;
            var oldSortOrder2 = topic2.SortOrder;

            controller.TopicUp(topic1.Id);
            Assert.AreEqual(oldSortOrder1, this.Storage.GetTopic(topic1.Id).SortOrder);
            Assert.AreEqual(oldSortOrder2, this.Storage.GetTopic(topic2.Id).SortOrder);

            controller.TopicUp(topic2.Id);
            Assert.AreEqual(oldSortOrder2, this.Storage.GetTopic(topic1.Id).SortOrder);
            Assert.AreEqual(oldSortOrder1, this.Storage.GetTopic(topic2.Id).SortOrder);
        }

        [Test]
        public void TopicDown()
        {
            var controller = GetController<TopicController>();
            this.DataPreparer.CreateDisciplinesSet4();
            var topics = this.Storage.GetTopics(item => item.ChapterRef == this.DataPreparer.ChapterIds[0])
                .OrderBy(item => item.SortOrder)
                .ToList();
            var topic1 = topics[topics.Count - 2];
            var topic2 = topics[topics.Count - 1];
            var oldSortOrder1 = topic1.SortOrder;
            var oldSortOrder2 = topic2.SortOrder;

            controller.TopicDown(topic2.Id);
            Assert.AreEqual(oldSortOrder1, this.Storage.GetTopic(topic1.Id).SortOrder);
            Assert.AreEqual(oldSortOrder2, this.Storage.GetTopic(topic2.Id).SortOrder);

            controller.TopicDown(topic1.Id);
            Assert.AreEqual(oldSortOrder2, this.Storage.GetTopic(topic1.Id).SortOrder);
            Assert.AreEqual(oldSortOrder1, this.Storage.GetTopic(topic2.Id).SortOrder);
        }

        #endregion
    }
}