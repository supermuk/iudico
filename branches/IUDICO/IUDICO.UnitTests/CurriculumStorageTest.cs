using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;

namespace IUDICO.UnitTests
{
    [TestClass]
    public class CurriculumStorageTest
    {
        MixedCurriculumStorage storage { get; set; }
        static ILmsService fakeLmsService { get; set; }
        static DBDataContext context { get;set; } //??? чи так треба , чи мож є якийсь інший спосіб добавляти групи

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            fakeLmsService = new FakeLmsService();
            context = fakeLmsService.GetDbDataContext(); //???
            //clear all tables before test runs!
            context.Curriculums.DeleteAllOnSubmit(context.Curriculums);
            context.SubmitChanges();
            //...
        }

        [TestInitialize()]
        public void InitializeTest()
        {
            storage = new MixedCurriculumStorage(fakeLmsService);
        }

        #region CurriculumTestMethods

        [TestMethod]
        public void TestMethod1()
        {
            Curriculum curriculum = new Curriculum
            {
                Name = "Bob",
                IsDeleted = false,
            };
            int id = storage.AddCurriculum(curriculum);
            AdvAssert.AreEqual(curriculum, storage.GetCurriculum(id));
            //storage.GetCurriculum(1);
            //викликає Exception оскільки нема такого запису,треба шось підмутити
            //питання - якщо нема такого,то шо вертати??
        }

        [TestMethod]
        public void TestMethod2()
        {
            Curriculum curriculum1 = new Curriculum { Name = "FirstTestCurr" };
            Curriculum curriculum2 = new Curriculum { Name = "SecondTestCurr" };
            storage.AddCurriculum(curriculum1);
            storage.AddCurriculum(curriculum2);
            int expected = 2;
            int actual = storage.GetCurriculums().ToList().Count;
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Curriculum curriculum = new Curriculum { Name = "CurriculumToUpdate" };
            int id = storage.AddCurriculum(curriculum);
            curriculum.Name = "UpdatedCurriculum";
            AdvAssert.AreEqual(curriculum, storage.GetCurriculum(id));
        }

        [TestMethod]
        public void TestMethod4()
        {
            Curriculum curriculum = new Curriculum { Name = "CurriculumToDelete" };
            int id = storage.AddCurriculum(curriculum);
            storage.DeleteCurriculum(id);
            //якщо шо - поправити
            try
            {
                storage.GetCurriculum(id);
                Assert.AreEqual(true, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }

        #endregion

        #region StageTestMethods

        [TestMethod]
        public void TestMethod5()
        {
            Stage stage = new Stage { Name = "StageToAdd" };
            int id = storage.AddStage(stage);
            AdvAssert.AreEqual(storage.GetStage(id), stage);
        }

        [TestMethod]
        public void TestMethod6()
        {
            Curriculum curriculum = new Curriculum { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);
            Stage stage = new Stage { Name = "Stage", CurriculumRef = curriculumId};
            int stageId = storage.AddStage(stage);
            List<Stage> listWithStagesForTest = new List<Stage>();
            listWithStagesForTest.Add(stage);
            AdvAssert.AreEqual(listWithStagesForTest, storage.GetStages(curriculumId).ToList());
        }

        [TestMethod]
        public void TestMethod7()
        {
            Stage stage = new Stage() { Name = "StageForUpdate" };
            int id = storage.AddStage(stage);
            stage.Name = "UpdatedStage";
            storage.UpdateStage(stage);

            AdvAssert.AreEqual(stage, storage.GetStage(id));
        }

        [TestMethod]
        public void TestMethod8()
        {
            Stage stage = new Stage() { Name = "StageForDelete" };
            int id = storage.AddStage(stage);
            AdvAssert.AreEqual(storage.GetStage(id), stage);
            storage.DeleteStage(id);

            try
            {
                storage.GetStage(id);
                Assert.AreEqual(true, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true,true);
            }
        }

        #endregion

        #region ThemeMethods

        //як розуміти метод private Theme GetTheme(int id, DBDataContext db)???

        [TestMethod]
        public void TestMethod9()
        {
            Theme theme = new Theme() { Name = "theme" };
            int id = storage.AddTheme(theme);
            AdvAssert.AreEqual(theme, storage.GetTheme(id));
        }

        [TestMethod]
        public void TestMethod10()
        {
            Curriculum curriculum = new Curriculum(){ Name = "Curriculum"};
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stageOne = new Stage() { Name = "StageOne", CurriculumRef = curriculumId };
            int stageOneId = storage.AddStage(stageOne);
            Stage stageTwo = new Stage(){ Name = "StageTwo",CurriculumRef = curriculumId };
            int stageTwoId = storage.AddStage(stageTwo);

            Theme theme1 = new Theme() { Name = "Theme1", StageRef = stageOneId, ThemeTypeRef = 1};
            Theme theme2 = new Theme() { Name = "Theme2", StageRef = stageTwoId, ThemeTypeRef = 2};
            int theme1Id = storage.AddTheme(theme1);
            int theme2Id = storage.AddTheme(theme2);

            List<Theme> themes = new List<Theme>();
            themes.Add(theme1);

            AdvAssert.AreEqual(themes,storage.GetThemesByStageId(stageOneId).ToList());

            themes.Add(theme2);
            AdvAssert.AreEqual(themes, storage.GetThemesByCurriculumId(curriculumId).ToList());
        }

        [TestMethod]
        public void TestMethod11()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stageOne = new Stage() { Name = "StageOne", CurriculumRef = curriculumId };
            int stageOneId = storage.AddStage(stageOne);

            Theme theme = new Theme() { Name = "Theme", StageRef = stageOneId, ThemeTypeRef = 1 };
            int themeId = storage.AddTheme(theme);

            AdvAssert.AreEqual(theme, storage.GetTheme(themeId));

            theme.Name = "NewName";
            storage.UpdateTheme(theme);
            AdvAssert.AreEqual(theme, storage.GetTheme(themeId));

            storage.DeleteTheme(themeId);
            try
            {
                AdvAssert.AreEqual(theme, storage.GetTheme(themeId));
                Assert.AreEqual(true, false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }

        #endregion

        #region CurriculumAssignmentMethods

        [TestMethod]
        public void TestMethod12()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Group group = new Group() { Name = "Group" };
            context.Groups.InsertOnSubmit(group);
            context.SubmitChanges();
            int groupId = group.Id;

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groupId };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);
            AdvAssert.AreEqual(curriculumAssignment,storage.GetCurriculumAssignment(curriculumAssignmentId));

            List<CurriculumAssignment> curriculumAssignments = new List<CurriculumAssignment>();
            curriculumAssignments.Add(curriculumAssignment);

            AdvAssert.AreEqual(curriculumAssignments,storage.GetCurriculumAssignmnetsByCurriculumId(curriculumId).ToList());

            Curriculum curriculumTwo = new Curriculum() { Name = "CurriculumTwo" };
            int curriculumTwoId = storage.AddCurriculum(curriculumTwo);
            CurriculumAssignment curriculumAssignmentTwo = new CurriculumAssignment() { CurriculumRef = curriculumTwoId, UserGroupRef = groupId };
            int curriculumAssignmentTwoId = storage.AddCurriculumAssignment(curriculumAssignmentTwo);
            curriculumAssignments.Add(curriculumAssignmentTwo);

            AdvAssert.AreEqual(curriculumAssignments,storage.GetCurriculumAssignments().ToList());
        }

        [TestMethod]
        public void TestMethod13()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Group group = new Group() { Name = "Group" };
            context.Groups.InsertOnSubmit(group);
            context.SubmitChanges();

            int groupId = group.Id;

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groupId };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);
            AdvAssert.AreEqual(curriculumAssignment, storage.GetCurriculumAssignment(curriculumAssignmentId));

            Group groupTwo = new Group() { Name = "GroupTwo" };
            context.Groups.InsertOnSubmit(groupTwo);
            context.SubmitChanges();

            curriculumAssignment.UserGroupRef = groupId;
            storage.UpdateCurriculumAssignment(curriculumAssignment);
            AdvAssert.AreEqual(curriculumAssignment, storage.GetCurriculumAssignment(curriculumAssignmentId));
        }

        [TestMethod]
        public void TestMethod14()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Group group = new Group() { Name = "Group" };
            context.Groups.InsertOnSubmit(group);
            context.SubmitChanges();

            int groupId = group.Id;

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groupId };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);
            AdvAssert.AreEqual(curriculumAssignment, storage.GetCurriculumAssignment(curriculumAssignmentId));
            
            List<CurriculumAssignment> curriculumAssignments = new List<CurriculumAssignment>();
            curriculumAssignments.Add(curriculumAssignment);

            AdvAssert.AreEqual(curriculumAssignments,storage.GetCurriculumAssignmentsByGroupId(groupId).ToList());

            storage.DeleteCurriculumAssignment(curriculumAssignmentId);
            
            try
            {
                AdvAssert.AreEqual(curriculumAssignment, storage.GetCurriculumAssignment(curriculumAssignmentId));
                Assert.AreEqual(true,false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true,true);
            }
        }

        #endregion

        #region TimelineMethods

        [TestMethod]
        public void TestMethod15()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Group group = new Group() { Name = "Group" };
            context.Groups.InsertOnSubmit(group);
            context.SubmitChanges();

            CurriculumAssignment curriculumAssingment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = group.Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssingment);

            Timeline timelineOne = new Timeline() { StartDate = DateTime.Now, EndDate = DateTime.Now, CurriculumAssignmentRef = curriculumAssignmentId };
            int timelineOneId = storage.AddTimeline(timelineOne);
            AdvAssert.AreEqual(timelineOne, storage.GetTimeline(timelineOneId));

            Timeline timelineTwo = new Timeline() { StartDate = DateTime.Now, EndDate = DateTime.Now, CurriculumAssignmentRef = curriculumAssignmentId };
            int timelineTwoId = storage.AddTimeline(timelineTwo);
            List<Timeline> timelines = new List<Timeline>();
            timelines.Add(timelineOne);
            timelines.Add(timelineTwo);

            AdvAssert.AreEqual(timelines,storage.GetTimelines(curriculumAssignmentId).ToList());
        }

        [TestMethod]
        public void TetsMethod16()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Group group = new Group() { Name = "Group" };
            context.Groups.InsertOnSubmit(group);
            context.SubmitChanges();

            CurriculumAssignment curriculumAssingment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = group.Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssingment);

            Stage stage = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int stageId = storage.AddStage(stage);

            Timeline timeline = new Timeline() { StartDate = DateTime.Now, EndDate = DateTime.Now, CurriculumAssignmentRef = curriculumAssignmentId ,StageRef = stageId };
            int timelineId = storage.AddTimeline(timeline);
            
            List<Timeline> stageTimelines = new List<Timeline>();
            stageTimelines.Add(timeline);

            AdvAssert.AreEqual(stageTimelines, storage.GetStageTimelinesByStageId(stageId).ToList());
            AdvAssert.AreEqual(stageTimelines, storage.GetStageTimelinesByCurriculumAssignmentId(curriculumAssignmentId).ToList());
        }

        [TestMethod]
        public void TestMethod17()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Group group = new Group() { Name = "Group" };
            context.Groups.InsertOnSubmit(group);
            context.SubmitChanges();

            CurriculumAssignment curriculumAssingment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = group.Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssingment);

            Timeline timeline = new Timeline() { StartDate = DateTime.Now, EndDate = DateTime.Now, CurriculumAssignmentRef = curriculumAssignmentId};
            int timelineId = storage.AddTimeline(timeline);

            AdvAssert.AreEqual(timeline,storage.GetTimeline(timelineId));
            timeline.StartDate = DateTime.Now;
            timeline.EndDate = DateTime.Now;

            storage.UpdateTimeline(timeline);
            AdvAssert.AreEqual(timeline,storage.GetTimeline(timelineId));

            storage.DeleteTimeline(timelineId);
            
            try
            {
                storage.GetTimeline(timelineId);
                Assert.AreEqual(true,false);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true,true);
            }
        }

        #endregion

        #region GroupMethods

        [TestMethod]
        public void TestMethod18()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Group groupOne = new Group() { Name = "GroupOne" };
            Group groupTwo = new Group() { Name = "GroupTwo" };
            Group groupThree = new Group() { Name = "GroupThree" };

            context.Groups.InsertOnSubmit(groupOne);
            context.Groups.InsertOnSubmit(groupTwo);
            context.Groups.InsertOnSubmit(groupThree);
            context.SubmitChanges();

            int groupOneId = groupOne.Id;
            int groupTwoId = groupTwo.Id;
            int groupThreeId = groupThree.Id;

            CurriculumAssignment curriculumAssingnment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groupOneId };
            int curriculumAssingmentId = storage.AddCurriculumAssignment(curriculumAssingnment);

            List<Group> groups = new List<Group>();
            groups.Add(groupOne);

            AdvAssert.AreEqual(groups, storage.GetAssignedGroups(curriculumId).ToList());

            groups.Clear();
            groups.Add(groupTwo);
            groups.Add(groupThree);
            AdvAssert.AreEqual(groups, storage.GetNotAssignedGroups(curriculumId).ToList());

            groups.Add(groupOne);
            AdvAssert.AreEqual(groups, storage.GetNotAssignedGroups(curriculumId).ToList());
        }

        #endregion
    }
}
