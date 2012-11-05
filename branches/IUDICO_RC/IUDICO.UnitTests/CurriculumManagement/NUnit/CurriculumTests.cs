using System;
using System.Linq;

using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Controllers;

using NUnit.Framework;
using IUDICO.Common.Models.Services;
using System.Collections.Generic;

namespace IUDICO.UnitTests.CurriculumManagement.NUnit
{
    [TestFixture]
    public class CurriculumTests : BaseTestFixture
    {
        #region CurriculumMethods

        [Test]
        public void AddCurriculum()
        {
            this.DataPreparer.CreateCurriculumsSet1();
            var expectedItems = this.DataPreparer.GetCurriculums();
            var controller = this.GetController<CurriculumController>();

            // add curriculums
            expectedItems.ForEach(item => controller.Create(item.ToCreateModel()));

            // then add "special" curriculum
            controller.Create(
                new Curriculum
                    {
                        IsValid = false, 
                        IsDeleted = true, 
                        DisciplineRef = this.DataPreparer.DisciplineIds[0], 
                        UserGroupRef = this.UserService.GetGroup(2).Id
                    }.ToCreateModel());

            expectedItems = this.DataPreparer.GetCurriculums();
            var allItems = this.CurriculumStorage.GetCurriculums().OrderBy(item => item.Id).ToList();
            var actualItems = allItems.Take(expectedItems.Count).ToList();
            var actualItem = allItems.Last();

            Assert.AreEqual(expectedItems.Count, actualItems.Count);
            Assert.AreEqual(true, actualItem.IsValid);
            Assert.AreEqual(false, actualItem.IsDeleted);

            // add bad curriculum
            controller = this.GetController<CurriculumController>();
            controller.Create(
                new Curriculum
                    {
                        DisciplineRef = this.DataPreparer.DisciplineIds[1], 
                        UserGroupRef = this.UserService.GetGroup(1).Id, 
                        StartDate = DateTime.Now.AddDays(1), 
                        // bad start date
                        EndDate = DateTime.Now
                    }.ToCreateModel());
            Assert.AreEqual(false, controller.ModelState.IsValid);
            Assert.AreEqual(expectedItems.Count + 1, this.CurriculumStorage.GetCurriculums().Count);

            // add bad curriculum
            controller = this.GetController<CurriculumController>();
            controller.Create(
                new Curriculum
                    {
                        DisciplineRef = this.DataPreparer.DisciplineIds[0], 
                        // curriculum with same group and discipline already exist!
                        UserGroupRef = this.UserService.GetGroup(2).Id, 
                        StartDate = DateTime.Now, 
                        EndDate = DateTime.Now
                    }.ToCreateModel());
            Assert.AreEqual(false, controller.ModelState.IsValid);
            Assert.AreEqual(expectedItems.Count + 1, this.CurriculumStorage.GetCurriculums().Count);

            // add bad curriculum
            controller = this.GetController<CurriculumController>();
            controller.Create(
                new Curriculum
                    {
                        DisciplineRef = -1, 
                        // disciplineId < 0
                        UserGroupRef = this.UserService.GetGroup(2).Id, 
                        StartDate = DateTime.Now, 
                        EndDate = DateTime.Now
                    }.ToCreateModel());
            Assert.AreEqual(false, controller.ModelState.IsValid);
            Assert.AreEqual(expectedItems.Count + 1, this.CurriculumStorage.GetCurriculums().Count);

            // add bad curriculum
            controller = this.GetController<CurriculumController>();
            controller.Create(
                new Curriculum
                    {
                        DisciplineRef = this.DataPreparer.DisciplineIds[0], 
                        UserGroupRef = -1, 
                        // groupId < 0
                        StartDate = DateTime.Now, 
                        EndDate = DateTime.Now
                    }.ToCreateModel());
            Assert.AreEqual(false, controller.ModelState.IsValid);
            Assert.AreEqual(expectedItems.Count + 1, this.CurriculumStorage.GetCurriculums().Count);

            // add bad curriculum
            controller = this.GetController<CurriculumController>();
            controller.Create(
                new Curriculum
                    {
                        DisciplineRef = this.DataPreparer.DisciplineIds[1], 
                        UserGroupRef = this.UserService.GetGroup(1).Id, 
                        StartDate = new DateTime(1100, 1, 1), 
                        // too small date
                        EndDate = new DateTime(2400, 1, 1) // too big date
                    }.ToCreateModel());
            Assert.AreEqual(false, controller.ModelState.IsValid);
            Assert.AreEqual(expectedItems.Count + 1, this.CurriculumStorage.GetCurriculums().Count);

            var curriculum = new Curriculum {
                DisciplineRef = this.DataPreparer.DisciplineIds[0],
                UserGroupRef = this.UserService.GetGroup(1).Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2)
            };
            var curriculumId = this.CurriculumStorage.AddCurriculum(curriculum);
            var discipline = this.DisciplineStorage.GetDiscipline(this.DataPreparer.DisciplineIds[0]);

            var chapterId = this.DisciplineStorage.GetChapters(item => item.DisciplineRef == discipline.Id).First().Id;
            var curriculumChapter = new CurriculumChapter() {
                ChapterRef = chapterId,
                CurriculumRef = curriculumId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(4)
            };
            this.CurriculumStorage.AddCurriculumChapter(curriculumChapter);
            curriculum = this.CurriculumStorage.GetCurriculum(curriculumId);
            Assert.IsFalse(curriculum.IsValid);
        }

        [Test]
        public void GetCurriculum() {
            var ids = this.DataPreparer.CreateDisciplinesSet2();
            var curriculums = this.DataPreparer.GetCurriculums();
            AdvAssert.AreEqual(curriculums[0], this.CurriculumStorage.GetCurriculum(ids[0]));
            AdvAssert.AreEqual(curriculums, this.CurriculumStorage.GetCurriculums(ids));
            
            this.tests.SetCurrentUser(Users.Panza);
            var user = this.UserService.GetCurrentUser();
            AdvAssert.AreEqual(curriculums, this.CurriculumStorage.GetCurriculums(user));
        }

        [Test]
        public void GetCurriculumChapterTopicByCurriculumId() {
            var topicIds = this.DataPreparer.CreateDisciplinesSet4();
            var curChapterIds = this.CurriculumStorage.GetCurriculumChapters(item => item.CurriculumRef == this.DataPreparer.CurriculumIds[0]).Select(item => item.Id);
            var curChapterTopicIds = this.CurriculumStorage.GetCurriculumChapterTopics(item => curChapterIds.Contains(item.CurriculumChapterRef)).Select(item => item.Id).ToList();
            var actualCurChaptTopicIds = this.CurriculumStorage.GetCurriculumChapterTopicsByCurriculumId(this.DataPreparer.CurriculumIds[0]).Select(item => item.Id).ToList();
            for (int i = 0; i < curChapterTopicIds.Count; i++) {
                Assert.AreEqual(curChapterTopicIds[i], actualCurChaptTopicIds[i]);
            }
        }
       
        // Tests Update of curriculums, curriculum chapters and curriculum chapter topics
        [Test]
        public void Update() {
            this.DataPreparer.CreateDisciplinesSet4();
            var curriculumController = this.GetController<CurriculumController>();
            var curriculumChapterController = this.GetController<CurriculumChapterController>();
            var curriculumChapterTopicController = this.GetController<CurriculumChapterTopicController>();
            var id = this.DataPreparer.CurriculumIds[0];
            var expected = this.DataPreparer.GetCurriculums()[0];
            expected.StartDate = DateTime.Now;
            expected.EndDate = DateTime.Now.AddDays(1);
            curriculumController.Edit(id, expected.ToCreateModel());
            AdvAssert.AreEqual(expected, this.CurriculumStorage.GetCurriculum(id));

            // Tests invalidation of curriculum because of it's curriculum chapter dates
            var curriculumChapter = this.CurriculumStorage.GetCurriculumChapters(item => item.CurriculumRef == id).First();
            curriculumChapter.StartDate = DateTime.Now;
            curriculumChapter.EndDate = DateTime.Now.AddDays(2);
            curriculumChapterController.Edit(curriculumChapter.Id, curriculumChapter.ToCreateModel());
            curriculumChapter = this.CurriculumStorage.GetCurriculumChapters(item => item.CurriculumRef == id).First();
            Assert.AreEqual(curriculumChapter, this.CurriculumStorage.GetCurriculumChapter(curriculumChapter.Id));
            Assert.IsFalse(this.CurriculumStorage.GetCurriculum(id).IsValid);
            
            // Tests if curriculum becomes valid if curriculum chapter becomes correct
            curriculumChapter.EndDate = DateTime.Now.AddHours(1);
            curriculumChapterController.Edit(curriculumChapter.Id, curriculumChapter.ToCreateModel());
            curriculumChapter = this.CurriculumStorage.GetCurriculumChapters(item => item.CurriculumRef == id).First();
            Assert.AreEqual(curriculumChapter, this.CurriculumStorage.GetCurriculumChapter(curriculumChapter.Id));
            Assert.IsTrue(this.CurriculumStorage.GetCurriculum(id).IsValid);

            // Tests invalidation of curriculum because of it's curriculum chapter topic dates
            var curriculumChapterTopic = this.CurriculumStorage.GetCurriculumChapterTopics(item => item.CurriculumChapterRef == curriculumChapter.Id).First();
            curriculumChapterTopic.TestStartDate = DateTime.Now;
            curriculumChapterTopic.TestEndDate = DateTime.Now.AddDays(2);
            curriculumChapterTopicController.Edit(curriculumChapterTopic.Id, curriculumChapterTopic.ToCreateModel());
            curriculumChapterTopic = this.CurriculumStorage.GetCurriculumChapterTopics(item => item.CurriculumChapterRef == curriculumChapter.Id).First();
            Assert.AreEqual(curriculumChapterTopic, this.CurriculumStorage.GetCurriculumChapterTopic(curriculumChapterTopic.Id));
            Assert.IsFalse(this.CurriculumStorage.GetCurriculum(id).IsValid);

            // Tests if curriculum becomes valid if curriculum chapter topic becomes correct
            curriculumChapterTopic.TestStartDate = DateTime.Now;
            curriculumChapterTopic.TestEndDate = DateTime.Now.AddMinutes(20);
            var result = curriculumController.EditTopic(curriculumChapterTopic.Id, curriculumChapterTopic.ToCreateModel());
            curriculumChapterTopic = this.CurriculumStorage.GetCurriculumChapterTopics(item => item.CurriculumChapterRef == curriculumChapter.Id).First();
            Assert.AreEqual(curriculumChapterTopic, this.CurriculumStorage.GetCurriculumChapterTopic(curriculumChapterTopic.Id));
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = true"));
            Assert.IsTrue(this.CurriculumStorage.GetCurriculum(id).IsValid);

            // Tests invalidation of curriculum because of it's curriculum chapter topic dates are out of curriculums
            curriculumChapter.EndDate = null;
            curriculumChapter.StartDate = null;
            curriculumChapterController.Edit(curriculumChapter.Id, curriculumChapter.ToCreateModel());
            curriculumChapterTopic.TestStartDate = DateTime.Now;
            curriculumChapterTopic.TestEndDate = DateTime.Now.AddDays(2);
            curriculumChapterTopicController.Edit(curriculumChapterTopic.Id, curriculumChapterTopic.ToCreateModel());
            Assert.IsFalse(this.CurriculumStorage.GetCurriculum(id).IsValid);

            // Tests invalidation of curriculum which group is deleted
            expected.UserGroupRef = this.DataPreparer.GetGroups()[2].Id;
            curriculumController.Edit(id, expected.ToCreateModel());
            Assert.IsFalse(this.CurriculumStorage.GetCurriculum(id).IsValid);

            // Tests invalidation of curriculum which discipline is invalid
            expected.UserGroupRef = this.DataPreparer.GetGroups()[2].Id;
            curriculumController.Edit(id, expected.ToCreateModel());
            this.DisciplineStorage.MakeDisciplinesInvalid(this.CourseService.GetCourse(2).Id);
            Assert.IsFalse(this.CurriculumStorage.GetCurriculum(id).IsValid);

            // Tests bad data updates
            expected.EndDate = new DateTime(2400, 1, 1);
            curriculumController.Edit(id, expected.ToCreateModel());
            Assert.IsFalse(curriculumController.ModelState.IsValid);

            curriculumChapter = this.CurriculumStorage.GetCurriculumChapters(item => item.CurriculumRef == id).First();
            curriculumChapter.StartDate = new DateTime(2400, 1, 1);
            curriculumChapter.EndDate = new DateTime(2400, 1, 1);
            curriculumChapterController.Edit(curriculumChapter.Id, curriculumChapter.ToCreateModel());
            Assert.IsFalse(curriculumChapterController.ModelState.IsValid);

            curriculumChapterTopic.TestEndDate = new DateTime(2400, 1, 1);
            curriculumChapterTopicController.Edit(curriculumChapterTopic.Id, curriculumChapterTopic.ToCreateModel());
            Assert.IsFalse(curriculumChapterTopicController.ModelState.IsValid);
            
            curriculumChapterTopic.TestEndDate = new DateTime(2400, 1, 1);
            result = curriculumController.EditTopic(curriculumChapterTopic.Id, curriculumChapterTopic.ToCreateModel());            
            Assert.IsTrue(result.Data.ToString().ToLower().Contains("success = false"));
        }

        #endregion
    }
}