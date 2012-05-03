using System;
using System.Linq;
using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using IUDICO.DisciplineManagement.Controllers;
using NUnit.Framework;

namespace IUDICO.UnitTests.CurriculumManagement.NUnit
{
    using System.IO;
    using System.Web;

    using IUDICO.Common.Models.Shared.DisciplineManagement;
    using IUDICO.DisciplineManagement.Models;

    using Moq;

    [TestFixture]
    public class DisciplineTests : BaseTestFixture
    {
        #region PrivateHelpers

        private User GetUser(string userName)
        {
            return UserService.GetUsers(u => u.Username == userName).FirstOrDefault();
        }

        #endregion

        #region Discipline tests

        [Test]
        public void AddDiscipline()
        {
            var expectedItems = this.DataPreparer.GetDisciplines();
            var controller = this.GetController<DisciplineController>();

            // add disciplines
            expectedItems.ForEach(item => controller.Create(item));

            // then add "special" discipline
            controller.Create(new Discipline { IsValid = false, Owner = "ozo", IsDeleted = true });

            expectedItems = this.DataPreparer.GetDisciplines();
            var allItems = this.DisciplineStorage.GetDisciplines()
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
                .ForEach(i => AdvAssert.AreEqual(expectedItems[i], this.DisciplineStorage.GetDiscipline(ids[i])));
        }

        [Test]
        public void GetDisciplines()
        {
            var ids = this.DataPreparer.CreateDisciplinesSet2().Take(2);
            var expectedItems = this.DataPreparer.GetDisciplines();

            // Tests GetDisciplines(IEnumerable<int> ids)
            AdvAssert.AreEqual(expectedItems.Take(2).ToList(), this.DisciplineStorage.GetDisciplines(ids));

            // Tests GetDisciplines()
            AdvAssert.AreEqual(expectedItems, this.DisciplineStorage.GetDisciplines());

            // Tests GetDisciplines(Func<Discipline, bool>)
            AdvAssert.AreEqual(
                expectedItems.Where(item => item.Owner == Users.Panza).ToList(), 
                this.DisciplineStorage.GetDisciplines(item => item.Owner == Users.Panza));

            // Tests GetDisciplinesByGroupId(groupId)
            var groupId = this.UserService.GetGroup(1).Id;
            AdvAssert.AreEqual(
                new[] { expectedItems[0], expectedItems[2] }, this.DisciplineStorage.GetDisciplinesByGroupId(groupId));

            // Tests GetDisciplines(User owner)
            this.tests.SetCurrentUser(Users.Ozo);
            Func<Discipline> expectedItemF =
                () => new Discipline { Name = "Discipline5", Owner = Users.Ozo, IsValid = true };
            this.DisciplineStorage.AddDiscipline(expectedItemF());
            var currentUser = this.UserService.GetCurrentUser();
            var actualItems = this.DisciplineStorage.GetDisciplines(currentUser);
            AdvAssert.AreEqual(new[] { expectedItemF() }, actualItems);
        }

        [Test]
        public void UpdateDiscipline()
        {
            var ids = this.DataPreparer.CreateDisciplinesSet1();
            var controller = this.GetController<DisciplineController>();
            Func<Discipline> newDisciplineF =
                () =>
                new Discipline
                    {
                       Name = "NewDiscipline", Owner = Users.Ozo, IsValid = false, IsDeleted = true, Id = ids[1] 
                    };
            controller.Edit(ids[1], newDisciplineF());

            var expected = newDisciplineF();
            var actual = this.DisciplineStorage.GetDiscipline(ids[1]);
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
            var controller = this.GetController<DisciplineController>();

            // delete 1 item
            var result = controller.DeleteItem(disciplineIds[1]);
            Assert.AreEqual(null, this.DisciplineStorage.GetDiscipline(disciplineIds[1]));
            Assert.AreEqual(disciplineIds.Count - 1, this.DisciplineStorage.GetDisciplines().Count);
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = true"));

            // check curriculums also deleted
            Assert.AreEqual(0, this.CurriculumStorage.GetCurriculums(c => c.DisciplineRef == disciplineIds[1]).Count);

            // check stages also deleted
            Assert.AreEqual(0, this.DisciplineStorage.GetChapters(item => item.DisciplineRef == disciplineIds[1]).Count);

            // error deletion
            result = controller.DeleteItem(disciplineIds[1]);
            Assert.AreEqual(null, this.DisciplineStorage.GetDiscipline(disciplineIds[1]));
            Assert.AreEqual(disciplineIds.Count - 1, this.DisciplineStorage.GetDisciplines().Count);
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = false"));

            // delete items
            disciplineIds.RemoveAt(1);
            controller.DeleteItems(disciplineIds.ToArray());
            Assert.AreEqual(0, this.DisciplineStorage.GetDisciplines().Count);

            // error deletion many items
            result = controller.DeleteItems(disciplineIds.ToArray());
            disciplineIds.ForEach(i => Assert.AreEqual(null, this.DisciplineStorage.GetDiscipline(i)));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = false"));
        }

        [Test]
        public void ShareDiscipline()
        {
            var ids = this.DataPreparer.CreateDisciplinesSet1();
            var controller = GetController<DisciplineController>();
            // before sharing
            var expectedSharedUsers = new List<User>();
            var expectedNotSharedUsers = new List<User> { this.GetUser(Users.Ozo) };
            var actualSharedUsers = this.DisciplineStorage.GetDisciplineSharedUsers(ids[0]);
            var actualNotSharedUsers = this.DisciplineStorage.GetDisciplineNotSharedUsers(ids[0]);
            AdvAssert.AreEqual(expectedSharedUsers, actualSharedUsers);
            AdvAssert.AreEqual(expectedNotSharedUsers, actualNotSharedUsers);

            // after sharing
            expectedSharedUsers = new List<User> { this.GetUser(Users.Ozo) };
            expectedNotSharedUsers = new List<User>();
            var result = controller.Share(ids[0], expectedSharedUsers.Select(u => u.Id).ToList());
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = true"));
            actualSharedUsers = this.DisciplineStorage.GetDisciplineSharedUsers(ids[0]);
            actualNotSharedUsers = this.DisciplineStorage.GetDisciplineNotSharedUsers(ids[0]);
            AdvAssert.AreEqual(expectedSharedUsers, actualSharedUsers);
            AdvAssert.AreEqual(expectedNotSharedUsers, actualNotSharedUsers);

            // after unsharing
            expectedSharedUsers = new List<User>();
            expectedNotSharedUsers = new List<User> { this.GetUser(Users.Ozo) };
            result = controller.Share(ids[0], null);
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = true"));
            actualSharedUsers = this.DisciplineStorage.GetDisciplineSharedUsers(ids[0]);
            actualNotSharedUsers = this.DisciplineStorage.GetDisciplineNotSharedUsers(ids[0]);
            AdvAssert.AreEqual(expectedSharedUsers, actualSharedUsers);
            AdvAssert.AreEqual(expectedNotSharedUsers, actualNotSharedUsers);
        }

        [Test]
        public void ImportExportDiscipline()
        {
            this.DataPreparer.CreateDisciplinesSet4();
            var controller = GetController<DisciplineController>();
            var filePath = controller.Export(this.DataPreparer.DisciplineIds[0]);

            var fileUpload = new Mock<HttpPostedFileBase>();
            fileUpload.SetupGet(item => item.FileName).Returns(filePath.FileName);
            fileUpload.Setup(item => item.SaveAs(It.IsAny<string>()))
                .Callback((string path) => File.Copy(fileUpload.Object.FileName, path));
            controller.Import(string.Empty, fileUpload.Object);

            var expectedDiscipline = DisciplineStorage.GetDiscipline(this.DataPreparer.DisciplineIds[0]);
            var actualDiscipline = DisciplineStorage.GetDiscipline(this.DataPreparer.DisciplineIds.Last() + 1);
            AdvAssert.AreEqual(expectedDiscipline, actualDiscipline);

            var expectedChapters = DisciplineStorage.GetChapters(c => c.DisciplineRef == expectedDiscipline.Id);
            var actualChapters = DisciplineStorage.GetChapters(c => c.DisciplineRef == actualDiscipline.Id);
            AdvAssert.AreEqual(expectedChapters, actualChapters, false);

            var expectedTopics = expectedChapters
                .SelectMany(c => DisciplineStorage.GetTopics(t => t.ChapterRef == c.Id))
                .ToList();
            var actualTopics = actualChapters
                .SelectMany(c => DisciplineStorage.GetTopics(t => t.ChapterRef == c.Id))
                .ToList();
            AdvAssert.AreEqual(expectedTopics, actualTopics, false);
        }

         [Test]
         public void MakeDisciplineInvalid() {
             this.DataPreparer.CreateDisciplinesSet4();
             var disciplineId = this.DataPreparer.DisciplineIds[0];             
             this.DisciplineStorage.MakeDisciplinesInvalid(1);
             var chapterIds = this.DisciplineStorage.GetTopicsByCourseId(1).Select(item => item.ChapterRef);
             var disciplineIds = this.DisciplineStorage.GetChapters(chapterIds).Select(item => item.DisciplineRef).ToList();
             disciplineIds.ForEach(id => Assert.IsFalse(this.DisciplineStorage.GetDiscipline(id).IsValid));
         }

        #endregion

        #region Chapter tests

        [Test]
        public void AddChapter()
        {
            this.DataPreparer.CreateDisciplinesSet2();
            var expectedItems = this.DataPreparer.GetChapters();
            var controller = this.GetController<ChapterController>();

            expectedItems.ForEach(item => controller.Create(item, item.DisciplineRef));

            expectedItems = this.DataPreparer.GetChapters();
            var actualItems = this.DataPreparer.DisciplineIds.SelectMany(id => this.DisciplineStorage.GetChapters(item => item.DisciplineRef == id))
                .OrderBy(item => item.Id)
                .ToList();
            AdvAssert.AreEqual(expectedItems, actualItems);

            // check authomatically added curriculum chapters
            actualItems.ForEach(
                item =>
                Assert.AreEqual(1, this.CurriculumStorage.GetCurriculumChapters(c => c.ChapterRef == item.Id).Count));
        }

        [Test]
        public void GetChapter()
        {
            var chapterIds = this.DataPreparer.CreateDisciplinesSet3();
            var expectedItems = this.DataPreparer.GetChapters();
            expectedItems.Select((item, i) => i).ToList()
                .ForEach(i => AdvAssert.AreEqual(expectedItems[i], this.DisciplineStorage.GetChapter(chapterIds[i])));
        }

        [Test]
        public void GetChapters()
        {
            var chapterIds = this.DataPreparer.CreateDisciplinesSet3().Take(2).ToList();
            var expectedItems = this.DataPreparer.GetChapters();

            // Tests GetChapters(IEnumerable<int> ids)
            AdvAssert.AreEqual(expectedItems.Take(2).ToList(), this.DisciplineStorage.GetChapters(chapterIds));

            // Tests GetChapters(Func<Chapter, bool>)
            AdvAssert.AreEqual(
                expectedItems.Where(item => item.Name == expectedItems[0].Name).ToList(), 
                this.DisciplineStorage.GetChapters(item => item.Name == expectedItems[0].Name));
        }

        [Test]
        public void UpdateChapter()
        {
            var controller = this.GetController<ChapterController>();
            var chapterIds = this.DataPreparer.CreateDisciplinesSet3();

            Func<Chapter> newDisciplineF =
                () => new Chapter { Name = "NewChapter", IsDeleted = true, Id = chapterIds[1], DisciplineRef = 100, };
            controller.Edit(chapterIds[1], newDisciplineF());

            var expected = newDisciplineF();
            var actual = this.DisciplineStorage.GetChapter(chapterIds[1]);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(false, actual.IsDeleted);
            Assert.AreEqual(chapterIds[1], actual.Id);
            Assert.AreEqual(this.DataPreparer.GetChapters()[1].DisciplineRef, actual.DisciplineRef);
        }

        [Test]
        public void DeleteChapters()
        {
            var controller = this.GetController<ChapterController>();
            this.DataPreparer.CreateDisciplinesSet4();
            var chapterIds = this.DataPreparer.ChapterIds;

            // delete 1 item
            var result = controller.DeleteItem(chapterIds[1]);
            Assert.AreEqual(null, this.DisciplineStorage.GetChapter(chapterIds[1]));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = true"));

            // check curriculumChapters also deleted
            Assert.AreEqual(0, this.CurriculumStorage.GetCurriculumChapters(c => c.ChapterRef == chapterIds[1]).Count);

            // error deletion
            result = controller.DeleteItem(chapterIds[1]);
            Assert.AreEqual(null, this.DisciplineStorage.GetChapter(chapterIds[1]));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = false"));

            // delete items
            chapterIds.RemoveAt(1);
            result = controller.DeleteItems(chapterIds.ToArray());
            chapterIds.ForEach(i => Assert.AreEqual(null, this.DisciplineStorage.GetChapter(i)));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = true"));

            // error deletion many items
            result = controller.DeleteItems(chapterIds.ToArray());
            chapterIds.ForEach(i => Assert.AreEqual(null, this.DisciplineStorage.GetChapter(i)));
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
            var controller = this.GetController<TopicController>();

            expectedItems.ForEach(item => controller.Create(item.ToCreateModel()));

            expectedItems = this.DataPreparer.GetTopics();
            var actualItems = this.DataPreparer.ChapterIds.SelectMany(id => this.DisciplineStorage.GetTopics(item => item.ChapterRef == id))
                .OrderBy(item => item.Id)
                .ToList();
            AdvAssert.AreEqual(expectedItems, actualItems);
            Assert.AreNotEqual(expectedItems.Last().SortOrder, actualItems.Last().SortOrder);

            // check authomatically added curriculum chapter topics
            actualItems.ForEach(
                item =>
                Assert.AreEqual(1, this.CurriculumStorage.GetCurriculumChapterTopics(c => c.TopicRef == item.Id).Count));

            var chapterId = this.DataPreparer.ChapterIds[0];

            // add bad topic
            controller = this.GetController<TopicController>();
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
            Assert.AreEqual(expectedItems.Count, this.DataPreparer.ChapterIds.Sum(id => this.DisciplineStorage.GetTopics(item => item.ChapterRef == id).Count));

            // add bad topic
            controller = this.GetController<TopicController>();
            badTopic = this.DataPreparer.GetTopics()[0];
            badTopic.Name = Enumerable.Repeat('A', 60).ToString();
            controller.Create(badTopic.ToCreateModel());
            Assert.AreEqual(false, controller.ModelState.IsValid);
            Assert.AreEqual(expectedItems.Count, this.DataPreparer.ChapterIds.Sum(id => this.DisciplineStorage.GetTopics(item => item.ChapterRef == id).Count));
        }

        [Test]
        public void GetTopic()
        {
            var topicIds = this.DataPreparer.CreateDisciplinesSet4();
            var expectedItems = this.DataPreparer.GetTopics();
            expectedItems.Select((item, i) => i).ToList()
                .ForEach(i => AdvAssert.AreEqual(expectedItems[i], this.DisciplineStorage.GetTopic(topicIds[i])));
        }

        [Test]
        public void GetTopics()
        {
            var topicIds = this.DataPreparer.CreateDisciplinesSet4().Take(2).ToList();
            var expectedItems = this.DataPreparer.GetTopics();
            var topicsPerDiscipline = expectedItems.Count / 3;

            // Tests GetTopics(IEnumerable<int> ids)
            AdvAssert.AreEqual(expectedItems.Take(2).ToList(), this.DisciplineStorage.GetTopics(topicIds));

            // Tests GetTopics(Func<Discipline, bool>)
            AdvAssert.AreEqual(
                expectedItems.Where(item => item.Name == expectedItems[0].Name).ToList(), 
                this.DisciplineStorage.GetTopics(item => item.Name == expectedItems[0].Name));

            // Tests GetTopicsByCourseId()
            var courseId = this.CourseService.GetCourse(2).Id;
            AdvAssert.AreEqual(
                expectedItems.Where(item => item.TestCourseRef == courseId || item.TheoryCourseRef == courseId).ToList(), 
                this.DisciplineStorage.GetTopicsByCourseId(courseId));

            // Tests GetTopicsByDisciplineId()
            // 1/3 of added topics belongs to first discipline and first stage
            var disciplineId = this.DisciplineStorage.GetDisciplines().First().Id;
            var items = expectedItems.OrderBy(item => item.ChapterRef).Take(topicsPerDiscipline).ToList();
            AdvAssert.AreEqual(items, this.DisciplineStorage.GetTopicsByDisciplineId(disciplineId));

            // Tests GetTopicsByDisciplineId()
            // first and third discipline connected to group1
            var groupId = this.UserService.GetGroup(1).Id;
            items = expectedItems.OrderBy(item => item.ChapterRef).ToList();
            items = items.Take(topicsPerDiscipline).Union(items.Skip(2 * topicsPerDiscipline).Take(topicsPerDiscipline)).ToList();
            AdvAssert.AreEqual(items, this.DisciplineStorage.GetTopicsByGroupId(groupId));

            // Tests GetTopicsOwnedByUser()
            // add discipline owned by another guy.
            var user = this.UserService.GetCurrentUser();
            this.tests.SetCurrentUser(Users.Ozo);
            disciplineId = this.DisciplineStorage.AddDiscipline(new Discipline { Name = "Discipline5", Owner = Users.Ozo });
            var chapterId = this.DisciplineStorage.AddChapter(new Chapter { Name = "Chapter", DisciplineRef = disciplineId });
            this.DisciplineStorage.AddTopic(new Topic { Name = "Topic", ChapterRef = chapterId });

            items = expectedItems.OrderBy(item => item.ChapterRef).ToList();
            AdvAssert.AreEqual(items, this.DisciplineStorage.GetTopicsOwnedByUser(user));
        }

        [Test]
        public void UpdateTopic()
        {
            var controller = this.GetController<TopicController>();
            var topicIds = this.DataPreparer.CreateDisciplinesSet4();

            Func<Topic> newTopicF = () => new Topic
            {
                SortOrder = 3,
                TestCourseRef = null,
                TestTopicTypeRef = null,
                TheoryCourseRef = this.CourseService.GetCourse(2).Id,
                TheoryTopicTypeRef = this.DisciplineStorage.GetTheoryTopicTypes().First(item => item.ToTopicTypeEnum() == TopicTypeEnum.Theory).Id,
                Name = "NewTopic",
                ChapterRef = 100,
                IsDeleted = true
            };
            controller.Edit(topicIds[0], newTopicF().ToCreateModel());
            var expected = newTopicF();
            var actual = this.DisciplineStorage.GetTopic(topicIds[0]);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.TestCourseRef, actual.TestCourseRef);
            Assert.AreEqual(expected.TestTopicTypeRef, actual.TestTopicTypeRef);
            Assert.AreEqual(expected.TheoryCourseRef, actual.TheoryCourseRef);
            Assert.AreEqual(expected.TheoryTopicTypeRef, actual.TheoryTopicTypeRef);
            Assert.AreEqual(false, actual.IsDeleted);
            Assert.AreEqual(topicIds[0], actual.Id);
            Assert.AreEqual(this.DataPreparer.GetTopics()[0].ChapterRef, actual.ChapterRef);

            var wrongTopic = newTopicF();
            wrongTopic.Id = topicIds[1];
            wrongTopic.TheoryCourseRef = null;
            wrongTopic.ChapterRef = this.DataPreparer.GetTopics()[1].ChapterRef;

            this.DisciplineStorage.UpdateTopic(wrongTopic);
            Assert.AreEqual(false, this.DisciplineStorage.GetDiscipline(this.DisciplineStorage.GetChapter(wrongTopic.ChapterRef).DisciplineRef).IsValid);
            
            wrongTopic = newTopicF();
            wrongTopic.Id = topicIds[0];
            wrongTopic.TheoryCourseRef = this.CourseService.GetCourse(4).Id;
            wrongTopic.ChapterRef = this.DataPreparer.GetTopics()[0].ChapterRef;

            this.DisciplineStorage.UpdateTopic(wrongTopic);
            Assert.AreEqual(false, this.DisciplineStorage.GetDiscipline(this.DisciplineStorage.GetChapter(wrongTopic.ChapterRef).DisciplineRef).IsValid);

            wrongTopic = newTopicF();
            wrongTopic.Id = topicIds[0];
            wrongTopic.TheoryCourseRef = this.CourseService.GetCourse(3).Id;
            wrongTopic.ChapterRef = this.DataPreparer.GetTopics()[0].ChapterRef;

            this.DisciplineStorage.UpdateTopic(wrongTopic);
            Assert.AreEqual(true, this.DisciplineStorage.GetDiscipline(this.DisciplineStorage.GetChapter(wrongTopic.ChapterRef).DisciplineRef).IsValid);

            wrongTopic = newTopicF();
            wrongTopic.Id = topicIds[0];
            wrongTopic.TheoryCourseRef = null;
            wrongTopic.TestCourseRef = this.CourseService.GetCourse(4).Id;
            wrongTopic.ChapterRef = this.DataPreparer.GetTopics()[0].ChapterRef;

            this.DisciplineStorage.UpdateTopic(wrongTopic);
            Assert.AreEqual(false, this.DisciplineStorage.GetDiscipline(this.DisciplineStorage.GetChapter(wrongTopic.ChapterRef).DisciplineRef).IsValid);
        }

        [Test]
        public void DeleteTopics()
        {
            var controller = this.GetController<TopicController>();
            var topicIds = this.DataPreparer.CreateDisciplinesSet4();

            // delete 1 item
            var result = controller.DeleteItem(topicIds[1]);
            Assert.AreEqual(null, this.DisciplineStorage.GetTopic(topicIds[1]));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = true"));

            // check curriculumChapterTopics also deleted
            Assert.AreEqual(0, this.CurriculumStorage.GetCurriculumChapterTopics(c => c.TopicRef == topicIds[1]).Count);

            // error deletion
            result = controller.DeleteItem(topicIds[1]);
            Assert.AreEqual(null, this.DisciplineStorage.GetTopic(topicIds[1]));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = false"));

            // delete items
            topicIds.RemoveAt(1);
            result = controller.DeleteItems(topicIds.ToArray());
            topicIds.ForEach(i => Assert.AreEqual(null, this.DisciplineStorage.GetTopic(i)));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = true"));

            // error deletion many items
            result = controller.DeleteItems(topicIds.ToArray());
            topicIds.ForEach(i => Assert.AreEqual(null, this.DisciplineStorage.GetTopic(i)));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = false"));
        }

        [Test]
        public void TopicUp()
        {
            var controller = this.GetController<TopicController>();
            this.DataPreparer.CreateDisciplinesSet4();
            var topics = this.DisciplineStorage.GetTopics(item => item.ChapterRef == this.DataPreparer.ChapterIds[0])
                .OrderBy(item => item.SortOrder)
                .ToList();
            var topic1 = topics[0];
            var topic2 = topics[1];
            var oldSortOrder1 = topic1.SortOrder;
            var oldSortOrder2 = topic2.SortOrder;

            controller.TopicUp(topic1.Id);
            Assert.AreEqual(oldSortOrder1, this.DisciplineStorage.GetTopic(topic1.Id).SortOrder);
            Assert.AreEqual(oldSortOrder2, this.DisciplineStorage.GetTopic(topic2.Id).SortOrder);

            controller.TopicUp(topic2.Id);
            Assert.AreEqual(oldSortOrder2, this.DisciplineStorage.GetTopic(topic1.Id).SortOrder);
            Assert.AreEqual(oldSortOrder1, this.DisciplineStorage.GetTopic(topic2.Id).SortOrder);
        }

        [Test]
        public void TopicDown()
        {
            var controller = this.GetController<TopicController>();
            this.DataPreparer.CreateDisciplinesSet4();
            var topics = this.DisciplineStorage.GetTopics(item => item.ChapterRef == this.DataPreparer.ChapterIds[0])
                .OrderBy(item => item.SortOrder)
                .ToList();
            var topic1 = topics[topics.Count - 2];
            var topic2 = topics[topics.Count - 1];
            var oldSortOrder1 = topic1.SortOrder;
            var oldSortOrder2 = topic2.SortOrder;

            controller.TopicDown(topic2.Id);
            Assert.AreEqual(oldSortOrder1, this.DisciplineStorage.GetTopic(topic1.Id).SortOrder);
            Assert.AreEqual(oldSortOrder2, this.DisciplineStorage.GetTopic(topic2.Id).SortOrder);

            controller.TopicDown(topic1.Id);
            Assert.AreEqual(oldSortOrder2, this.DisciplineStorage.GetTopic(topic1.Id).SortOrder);
            Assert.AreEqual(oldSortOrder1, this.DisciplineStorage.GetTopic(topic2.Id).SortOrder);
        }

        #endregion
    }
}