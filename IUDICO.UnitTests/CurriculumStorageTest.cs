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
        static DBDataContext context { get; set; } //??? чи так треба , чи мож є якийсь інший спосіб добавляти групи

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            fakeLmsService = new FakeLmsService();
            context = fakeLmsService.GetDbDataContext(); //???
            //clear all tables before test runs!
            //context.Curriculums.DeleteAllOnSubmit(context.Curriculums);
            //context.SubmitChanges();
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
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int id = storage.AddCurriculum(curriculum);
            AdvAssert.AreEqual(curriculum, storage.GetCurriculum(id));

            storage.DeleteCurriculum(id);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Curriculum curriculumOne = new Curriculum { Name = "FirstTestCurr" };
            Curriculum curriculumTwo = new Curriculum { Name = "SecondTestCurr" };
            int curriculumOneId = storage.AddCurriculum(curriculumOne);
            int curriculumTwoId = storage.AddCurriculum(curriculumTwo);
            int expected = 2;
            int actual = storage.GetCurriculums().ToList().Count;
            Assert.AreEqual(expected,actual);

            storage.DeleteCurriculum(curriculumOneId);
            storage.DeleteCurriculum(curriculumTwoId);
        }
        
        [TestMethod]
        public void TestMethod3()
        {
            Curriculum curriculum = new Curriculum { Name = "CurriculumToUpdate" };
            int id = storage.AddCurriculum(curriculum);
            curriculum.Name = "UpdatedCurriculum";
            AdvAssert.AreEqual(curriculum, storage.GetCurriculum(id));

            storage.DeleteCurriculum(id);
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
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stage = new Stage { Name = "StageToAdd", CurriculumRef = curriculumId };
            int id = storage.AddStage(stage);
            AdvAssert.AreEqual(storage.GetStage(id), stage);

            storage.DeleteStage(id);
            storage.DeleteCurriculum(curriculumId);
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

            storage.DeleteStage(stageId);
            storage.DeleteCurriculum(curriculumId);
        }

        [TestMethod]
        public void TestMethod7()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);
            
            Stage stage = new Stage() { Name = "StageForUpdate",CurriculumRef = curriculumId};
            int id = storage.AddStage(stage);
            stage.Name = "UpdatedStage";
            storage.UpdateStage(stage);

            AdvAssert.AreEqual(stage, storage.GetStage(id));

            storage.DeleteStage(id);
            storage.DeleteCurriculum(curriculumId);
        }

        [TestMethod]
        public void TestMethod8()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stage = new Stage() { Name = "StageForDelete", CurriculumRef = curriculumId };
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

            storage.DeleteCurriculum(curriculumId);
        }

        #endregion

        #region ThemeMethods

        //як розуміти метод private Theme GetTheme(int id, DBDataContext db)???

        [TestMethod]
        public void TestMethod9()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stage = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int stageId = storage.AddStage(stage);

            Theme theme = new Theme() { Name = "theme", ThemeTypeRef = 1, StageRef = stageId };
            int id = storage.AddTheme(theme);
            AdvAssert.AreEqual(theme, storage.GetTheme(id));

            storage.DeleteTheme(id);
            storage.DeleteStage(stageId);
            storage.DeleteCurriculum(curriculumId);
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

            storage.DeleteTheme(theme1Id);
            storage.DeleteTheme(theme2Id);
            storage.DeleteStage(stageOneId);
            storage.DeleteStage(stageTwoId);
            storage.DeleteCurriculum(curriculumId);
        }

        [TestMethod]
        public void TestMethod11()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stageOne = new Stage() { Name = "StageOne", CurriculumRef = curriculumId };
            int stageOneId = storage.AddStage(stageOne);

            Course course = new Course() { Name = "Course", Created = DateTime.Now, Deleted = false, Updated = DateTime.Now };
            context.Courses.InsertOnSubmit(course);
            context.SubmitChanges();

            Theme theme = new Theme() { Name = "Theme", StageRef = stageOneId, ThemeTypeRef = 1, CourseRef = course.Id };
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

            context.Courses.DeleteOnSubmit(course);
            context.SubmitChanges();
            storage.DeleteTheme(stageOneId);
            storage.DeleteTheme(curriculumId);
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

            storage.DeleteCurriculumAssignment(curriculumAssignmentTwoId);
            storage.DeleteCurriculumAssignment(curriculumTwoId);
            storage.DeleteCurriculumAssignment(curriculumAssignmentId);
            storage.DeleteCurriculum(curriculumId);
            context.Groups.DeleteOnSubmit(group);
            context.SubmitChanges();
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

            context.Groups.DeleteOnSubmit(groupTwo);
            context.SubmitChanges();
            storage.DeleteCurriculumAssignment(curriculumAssignmentId);
            context.Groups.DeleteOnSubmit(group);
            context.SubmitChanges();
            storage.DeleteCurriculum(curriculumId);
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

            context.Groups.DeleteOnSubmit(group);
            context.SubmitChanges();
            storage.DeleteCurriculum(curriculumId);

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

            storage.DeleteTimeline(timelineTwoId);
            storage.DeleteTimeline(timelineOneId);
            storage.DeleteCurriculumAssignment(curriculumAssignmentId);
            context.Groups.DeleteOnSubmit(group);
            context.SubmitChanges();
            storage.DeleteCurriculum(curriculumId);
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

            storage.DeleteTimeline(timelineId);
            storage.DeleteTimeline(curriculumAssignmentId);
            storage.DeleteStage(stageId);
            context.Groups.DeleteOnSubmit(group);
            context.SubmitChanges();
            storage.DeleteCurriculum(curriculumId);
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

            Timeline timeline = new Timeline() { StartDate = DateTime.Now, EndDate = DateTime.Now, CurriculumAssignmentRef = curriculumAssignmentId, StageRef = null };
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

            storage.DeleteTimeline(curriculumAssignmentId);
            context.Groups.DeleteOnSubmit(group);
            context.SubmitChanges();
            storage.DeleteCurriculum(curriculumId);
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
            context.SubmitChanges();
            context.Groups.InsertOnSubmit(groupTwo);
            context.SubmitChanges();
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

            storage.DeleteCurriculumAssignment(curriculumAssingmentId);
            context.Groups.DeleteOnSubmit(groupOne);
            context.SubmitChanges();
            context.Groups.DeleteOnSubmit(groupTwo);
            context.SubmitChanges();
            context.Groups.DeleteOnSubmit(groupThree);
            context.SubmitChanges();
            storage.DeleteCurriculum(curriculumId);
        }

        #endregion

        #region ThemeAssignmentMethods

        [TestMethod]
        public void TestMethod19()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Group group = new Group() { Name = "Group" };
            context.Groups.InsertOnSubmit(group);
            context.SubmitChanges();
            int groupId = group.Id;

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groupId };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);

            Stage stage = new Stage(){ Name = "Stage", CurriculumRef = curriculumId};
            int stageId = storage.AddStage(stage);

            Theme theme = new Theme(){ Name = "Theme", ThemeTypeRef = 1, StageRef = stageId };
            int themeId = storage.AddTheme(theme);

            ThemeAssignment themeAssignment = new ThemeAssignment()
            {
                CurriculumAssignmentRef = curriculumAssignmentId,
                MaxScore = 1,
                ThemeRef = themeId
            };

            int themeAssignmentId = storage.AddThemeAssignment(themeAssignment);
            AdvAssert.AreEqual(themeAssignment, storage.GetThemeAssignment(themeAssignmentId));

            themeAssignment.MaxScore = 3;
            storage.UpdateThemeAssignment(themeAssignment);
            AdvAssert.AreEqual(themeAssignment,storage.GetThemeAssignment(themeAssignmentId));

            List<ThemeAssignment> themeAssignments = new List<ThemeAssignment>();
            themeAssignments.Add(themeAssignment);
            AdvAssert.AreEqual(themeAssignments,storage.GetThemeAssignmentsByThemeId(themeId).ToList());

            themeAssignments.Clear();
            themeAssignments.Add(themeAssignment);
            AdvAssert.AreEqual(themeAssignments, storage.GetThemeAssignmentsByCurriculumAssignmentId(curriculumAssignmentId).ToList());

            context.ThemeAssignments.DeleteOnSubmit(themeAssignment);
            context.SubmitChanges();
            storage.DeleteTheme(themeId);
            storage.DeleteStage(stageId);
            storage.DeleteCurriculumAssignment(curriculumAssignmentId);
            context.Groups.DeleteOnSubmit(group);
            context.SubmitChanges();
            storage.DeleteCurriculum(curriculumId);
        }
        
        #endregion
    }
}
