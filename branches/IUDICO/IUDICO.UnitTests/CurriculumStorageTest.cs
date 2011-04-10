using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UnitTests
{
    [TestClass]
    public class CurriculumStorageTest
    {
        ICurriculumStorage storage { get; set; }
        ILmsService lmsService { get; set; }
        DBDataContext context { get; set; } //забудь про нього в тестах!

        //1)забери всюди делете.+
        //2)не називай об*єкти curriculumThirtyFour:))+

        private void ClearDb()
        {
            context = lmsService.GetDbDataContext();
            context.CurriculumAssignments.DeleteAllOnSubmit(context.CurriculumAssignments);
            context.Themes.DeleteAllOnSubmit(context.Themes);
            context.Stages.DeleteAllOnSubmit(context.Stages);
            context.Curriculums.DeleteAllOnSubmit(context.Curriculums);
            context.SubmitChanges();
        }

        private void InitializeDb()
        {
            IUserStorage userService = new DatabaseUserStorage(lmsService);

            userService.CreateGroup(new Group() { Name = "TestGroup1" });
            userService.CreateGroup(new Group() { Name = "TestGroup2" });
            userService.CreateGroup(new Group() { Name = "TestGroup3" });
            context.SubmitChanges();
        }

        public CurriculumStorageTest()
        {
            lmsService = new FakeLmsService();
            using (context = lmsService.GetDbDataContext())
            {
                if (context.DatabaseExists())
                {
                    context.DeleteDatabase();
                }
                context.CreateDatabase();
                InitializeDb();
            }
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {

        }

        [TestInitialize()]
        public void InitializeTest()
        {
            storage = new MixedCurriculumStorage(lmsService);
            using (context = lmsService.GetDbDataContext())
            {
                ClearDb();
            }
        }

        ~CurriculumStorageTest()
        {
            using (context = lmsService.GetDbDataContext())
            {
                context.DeleteDatabase();
            }
        }

        #region CurriculumTestMethods

        [TestMethod]
        public void AddCurriculumTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int id = storage.AddCurriculum(curriculum);
            AdvAssert.AreEqual(curriculum, storage.GetCurriculum(id));
        }

        [TestMethod]
        public void GetCurriculumTests()
        {
            Curriculum curriculum1 = new Curriculum { Name = "FirstCurriculum" };
            Curriculum curriculum2 = new Curriculum { Name = "SecondCurriculum" };
            int curriculum1Id = storage.AddCurriculum(curriculum1);
            int curriculum2Id = storage.AddCurriculum(curriculum2);

            IEnumerable<Curriculum> curriculums = storage.GetCurriculums();
            Assert.AreEqual(2, curriculums.ToList().Count);

            AdvAssert.AreEqual(curriculum1, storage.GetCurriculum(curriculum1Id));
            AdvAssert.AreEqual(curriculum2, storage.GetCurriculum(curriculum2Id));

            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculum1Id, UserGroupRef = groups[0].Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);

            List<Curriculum> curriculumList = new List<Curriculum>();
            curriculumList.Add(curriculum1);
            AdvAssert.AreEqual(curriculumList, storage.GetCurriculumsByGroupId(groups[0].Id).ToList());
        }
        
        [TestMethod]
        public void UpdateCurriculumTest()
        {
            Curriculum curriculum = new Curriculum { Name = "Curriculum" };
            int id = storage.AddCurriculum(curriculum);
            curriculum.Name = "UpdatedCurriculum";
            storage.UpdateCurriculum(curriculum); //

            //тут має бути NotEqual, бо ти змінив ім8я тільки в локальній копії, а на бд це не мало б відобразитись-але фіг там
            //відображається!
            AdvAssert.AreEqual(curriculum, storage.GetCurriculum(id));
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void DeleteCurriculumTest()
        {
            Curriculum curriculum = new Curriculum { Name = "Curriculum" };
            int id = storage.AddCurriculum(curriculum);
            storage.DeleteCurriculum(id);
            storage.GetCurriculum(id);
            Assert.AreEqual(true, false);
        }

        #endregion

        #region StageTestMethods

        [TestMethod]
        public void AddStageTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stage = new Stage { Name = "Stage", CurriculumRef = curriculumId };
            int id = storage.AddStage(stage);
            AdvAssert.AreEqual(storage.GetStage(id), stage);
        }

        [TestMethod]
        public void GetStageTest()
        {
            Curriculum curriculum = new Curriculum { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);
            Stage stage = new Stage { Name = "Stage", CurriculumRef = curriculumId };
            int stageId = storage.AddStage(stage);
            List<Stage> listWithStagesForTest = new List<Stage>();
            listWithStagesForTest.Add(stage);
            AdvAssert.AreEqual(listWithStagesForTest, storage.GetStages(curriculumId).ToList());
        }

        [TestMethod]
        public void UpdateStageTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stage = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int id = storage.AddStage(stage);
            stage.Name = "UpdatedStage";
            storage.UpdateStage(stage);

            AdvAssert.AreEqual(stage, storage.GetStage(id));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteStageTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stage = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int id = storage.AddStage(stage);
            AdvAssert.AreEqual(storage.GetStage(id), stage);
            storage.DeleteStage(id);
            storage.GetStage(id);
            Assert.AreEqual(true, false);
        }

        #endregion

        #region ThemeMethods

        //як розуміти метод private Theme GetTheme(int id, DBDataContext db)???

        [TestMethod]
        public void AddThemeTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stage = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int stageId = storage.AddStage(stage);

            Theme theme = new Theme() { Name = "Theme", ThemeTypeRef = 1, StageRef = stageId };
            int id = storage.AddTheme(theme);
            AdvAssert.AreEqual(theme, storage.GetTheme(id));
        }

        [TestMethod]
        public void GetThemeTests()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stageOne = new Stage() { Name = "FirstStage", CurriculumRef = curriculumId };
            int stageOneId = storage.AddStage(stageOne);
            Stage stageTwo = new Stage() { Name = "SecondStage", CurriculumRef = curriculumId };
            int stageTwoId = storage.AddStage(stageTwo);

            Theme theme1 = new Theme() { Name = "FirstTheme", StageRef = stageOneId, ThemeTypeRef = 1 };
            Theme theme2 = new Theme() { Name = "SecondTheme", StageRef = stageTwoId, ThemeTypeRef = 2 };
            int theme1Id = storage.AddTheme(theme1);
            int theme2Id = storage.AddTheme(theme2);

            List<Theme> themes = new List<Theme>();
            themes.Add(theme1);

            AdvAssert.AreEqual(themes,storage.GetThemesByStageId(stageOneId).ToList());

            themes.Add(theme2);
            AdvAssert.AreEqual(themes, storage.GetThemesByCurriculumId(curriculumId).ToList());

            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);

            AdvAssert.AreEqual(themes, storage.GetThemesByGroupId(groups[0].Id).ToList());
        }

        [TestMethod]
        public void UpdateThemeTests()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stage = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int stageId = storage.AddStage(stage);

            Theme theme = new Theme() { Name = "Theme", StageRef = stageId, ThemeTypeRef = 1 };
            int themeId = storage.AddTheme(theme);

            theme.Name = "NewName";
            storage.UpdateTheme(theme);

            AdvAssert.AreEqual(theme,storage.GetTheme(themeId));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteThemeTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stageOne = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int stageOneId = storage.AddStage(stageOne);

            Course course = new Course() { Name = "Course", Created = DateTime.Now, Deleted = false, Updated = DateTime.Now };
            context.Courses.InsertOnSubmit(course);
            context.SubmitChanges();

            Theme theme = new Theme() { Name = "Theme", StageRef = stageOneId, ThemeTypeRef = 1, CourseRef = course.Id, IsDeleted = false, Updated = DateTime.Now, Created = DateTime.Now};
            int themeId = storage.AddTheme(theme);

            AdvAssert.AreEqual(theme, storage.GetTheme(themeId));

            theme.Name = "UpdatedTheme";
            storage.UpdateTheme(theme); //апдейтить,але не показує. через це не проходить тест і відповідно видалення нижче 
            AdvAssert.AreEqual(theme, storage.GetTheme(themeId));

            storage.DeleteTheme(themeId);
            storage.GetTheme(themeId);
            Assert.AreEqual(true, false);
        }

        #endregion
        
        #region CurriculumAssignmentMethods

        [TestMethod]
        public void AddCurriculumAssignmentTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "FirstCurriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id};
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);
            AdvAssert.AreEqual(curriculumAssignment,storage.GetCurriculumAssignment(curriculumAssignmentId));

            List<CurriculumAssignment> curriculumAssignments = new List<CurriculumAssignment>();
            curriculumAssignments.Add(curriculumAssignment);
            AdvAssert.AreEqual(curriculumAssignments, storage.GetCurriculumAssignmnetsByCurriculumId(curriculumId).ToList());

            Curriculum curriculum2 = new Curriculum() { Name = "SecondCurriculum" };
            int curriculum2Id = storage.AddCurriculum(curriculum2);
            CurriculumAssignment curriculumAssignment2 = new CurriculumAssignment() { CurriculumRef = curriculum2Id, UserGroupRef = groups[0].Id };
            storage.AddCurriculumAssignment(curriculumAssignment2);
            curriculumAssignments.Add(curriculumAssignment2);
            AdvAssert.AreEqual(curriculumAssignments, storage.GetCurriculumAssignments().ToList());
        }

        [TestMethod]
        public void GetCurriculumAssignmentTests()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);
            AdvAssert.AreEqual(curriculumAssignment, storage.GetCurriculumAssignment(curriculumAssignmentId));

            List<CurriculumAssignment> curriculumAssignments = new List<CurriculumAssignment>();
            curriculumAssignments.Add(curriculumAssignment);
            
            AdvAssert.AreEqual(curriculumAssignments, storage.GetCurriculumAssignmnetsByCurriculumId(curriculumId).ToList());
            AdvAssert.AreEqual(curriculumAssignments, storage.GetCurriculumAssignmentsByGroupId(groups[0].Id).ToList());
            AdvAssert.AreEqual(curriculumAssignments, storage.GetCurriculumAssignments().ToList());
        }

        [TestMethod]
        public void UpdateCurriculumAssignmentTests()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);
            
            curriculumAssignment.UserGroupRef = groups[1].Id;
            storage.UpdateCurriculumAssignment(curriculumAssignment);
            AdvAssert.AreEqual(curriculumAssignment, storage.GetCurriculumAssignment(curriculumAssignmentId));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteCurriculumAssignmentTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);
            AdvAssert.AreEqual(curriculumAssignment, storage.GetCurriculumAssignment(curriculumAssignmentId));
            
            List<CurriculumAssignment> curriculumAssignments = new List<CurriculumAssignment>();
            curriculumAssignments.Add(curriculumAssignment);

            AdvAssert.AreEqual(curriculumAssignments,storage.GetCurriculumAssignmentsByGroupId(groups[0].Id).ToList());

            storage.DeleteCurriculumAssignment(curriculumAssignmentId);
            storage.GetCurriculumAssignment(curriculumAssignmentId);
            Assert.AreEqual(true,false);
        }

        #endregion

        #region TimelineMethods

        [TestMethod]
        public void AddTimelineTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();

            CurriculumAssignment curriculumAssingment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssingment);

            Timeline timelineOne = new Timeline() { StartDate = DateTime.Now, EndDate = DateTime.Now, CurriculumAssignmentRef = curriculumAssignmentId };
            int timelineOneId = storage.AddTimeline(timelineOne);
            AdvAssert.AreEqual(timelineOne, storage.GetTimeline(timelineOneId));

            Timeline timelineTwo = new Timeline() { StartDate = DateTime.Now, EndDate = DateTime.Now, CurriculumAssignmentRef = curriculumAssignmentId };
            int timelineTwoId = storage.AddTimeline(timelineTwo);
            List<Timeline> timelines = new List<Timeline>();
            timelines.Add(timelineOne);
            timelines.Add(timelineTwo);

            AdvAssert.AreEqual(timelines,storage.GetCurriculumAssignmentTimelines(curriculumAssignmentId).ToList());
        }

        [TestMethod]
        public void GetTimelineTests()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();

            CurriculumAssignment curriculumAssingment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssingment);

            Stage stage = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int stageId = storage.AddStage(stage);

            Timeline timeline = new Timeline() { StartDate = DateTime.Now, EndDate = DateTime.Now, CurriculumAssignmentRef = curriculumAssignmentId ,StageRef = stageId };
            int timelineId = storage.AddTimeline(timeline);
            
            List<Timeline> stageTimelines = new List<Timeline>();
            stageTimelines.Add(timeline);

            AdvAssert.AreEqual(stageTimelines, storage.GetStageTimelinesByStageId(stageId).ToList());
            AdvAssert.AreEqual(stageTimelines, storage.GetStageTimelinesByCurriculumAssignmentId(curriculumAssignmentId).ToList());
            AdvAssert.AreEqual(stageTimelines, storage.GetStageTimelines(stageId,curriculumAssignmentId).ToList());
        }

        [TestMethod]
        public void UpdateTimelineTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();

            CurriculumAssignment curriculumAssingment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssingment);

            Stage stage = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int stageId = storage.AddStage(stage);

            Timeline timeline = new Timeline() { StartDate = DateTime.Now, EndDate = DateTime.Now, CurriculumAssignmentRef = curriculumAssignmentId, StageRef = stageId };
            int timelineId = storage.AddTimeline(timeline);

            timeline.EndDate = DateTime.Now;
            storage.UpdateTimeline(timeline);
            AdvAssert.AreEqual(timeline, storage.GetTimeline(timelineId));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteTimelineTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();

            CurriculumAssignment curriculumAssingment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssingment);

            Timeline timeline = new Timeline() { StartDate = DateTime.Now, EndDate = DateTime.Now, CurriculumAssignmentRef = curriculumAssignmentId, StageRef = null };
            int timelineId = storage.AddTimeline(timeline);

            AdvAssert.AreEqual(timeline,storage.GetTimeline(timelineId));
            timeline.StartDate = DateTime.Now;
            timeline.EndDate = DateTime.Now;

            storage.UpdateTimeline(timeline);
            AdvAssert.AreEqual(timeline,storage.GetTimeline(timelineId));

            storage.DeleteTimeline(timelineId);
            storage.GetTimeline(timelineId);
            Assert.AreEqual(true,false);
        }

        #endregion

        #region GroupMethods

        [TestMethod]
        public void GetGroupTests()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();

            int groupOneId = groups[0].Id;
            int groupTwoId = groups[1].Id;
            int groupThreeId = groups[2].Id;

            CurriculumAssignment curriculumAssingnment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groupOneId };
            int curriculumAssingmentId = storage.AddCurriculumAssignment(curriculumAssingnment);

            List<Group> groupList = new List<Group>();
            groupList.Add(groups[0]);

            //Не проходять через то шо в цих методах використовується методи ,які юзають інші сервіси

            //AdvAssert.AreEqual(groups, storage.GetAssignedGroups(curriculumId).ToList());

            //groups.Clear();
            //groups.Add(groupTwo);
            //groups.Add(groupThree);
            //AdvAssert.AreEqual(groups, storage.GetNotAssignedGroups(curriculumId).ToList());

            //groups.Add(groupOne);
            //AdvAssert.AreEqual(groups, storage.GetNotAssignedGroupsWithCurrentGroup(curriculumId,groupOneId).ToList());
        }

        #endregion

        #region ThemeAssignmentMethods

        [TestMethod]
        public void AddThemeAssignmentTest()
        {
            //Update створює новий ТемАсс замість заміни старого,тому виходить два однакових темАсс,просто з різними Ід
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);
           
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            int groupId = groups[0].Id;

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groupId };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);

            Stage stage = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int stageId = storage.AddStage(stage);

            Theme theme = new Theme() { Name = "Theme", ThemeTypeRef = 1, StageRef = stageId };
            int themeId = storage.AddTheme(theme);

            ThemeAssignment themeAssignment = new ThemeAssignment()
            {
                CurriculumAssignmentRef = curriculumAssignmentId,
                MaxScore = 1,
                ThemeRef = themeId
            };

            int themeAssignmentId = storage.AddThemeAssignment(themeAssignment);
            AdvAssert.AreEqual(themeAssignment, storage.GetThemeAssignment(themeAssignmentId));
        }

        [TestMethod]
        public void GetThemeAssignmentTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            int groupId = groups[0].Id;

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groupId };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);

            Stage stage = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int stageId = storage.AddStage(stage);

            Theme theme = new Theme() { Name = "Theme", ThemeTypeRef = 1, StageRef = stageId };
            int themeId = storage.AddTheme(theme);

            ThemeAssignment themeAssignment = new ThemeAssignment()
            {
                CurriculumAssignmentRef = curriculumAssignmentId,
                MaxScore = 1,
                ThemeRef = themeId
            };
            int themeAssignmentId = storage.AddThemeAssignment(themeAssignment);

            List<ThemeAssignment> themeAssignments = new List<ThemeAssignment>();
            themeAssignments.Add(themeAssignment);
            AdvAssert.AreEqual(themeAssignments,storage.GetThemeAssignmentsByThemeId(themeId).ToList());

            themeAssignments.Clear();
            themeAssignments.Add(themeAssignment);
            AdvAssert.AreEqual(themeAssignments, storage.GetThemeAssignmentsByCurriculumAssignmentId(curriculumAssignmentId).ToList());
        }

        [TestMethod]
        public void UpdateThemeAssignmentTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);

            Stage stage = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int stageId = storage.AddStage(stage);

            Theme theme = new Theme() { Name = "Theme", ThemeTypeRef = 1, StageRef = stageId };
            int themeId = storage.AddTheme(theme);

            ThemeAssignment themeAssignment = new ThemeAssignment()
            {
                CurriculumAssignmentRef = curriculumAssignmentId,
                MaxScore = 1,
                ThemeRef = themeId
            };
            int themeAssignmentId = storage.AddThemeAssignment(themeAssignment);

            themeAssignment.MaxScore = 3;
            storage.UpdateThemeAssignment(themeAssignment);
            AdvAssert.AreEqual(themeAssignment,storage.GetThemeAssignment(themeAssignmentId));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteThemeAssignmentTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);

            Stage stage = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int stageId = storage.AddStage(stage);

            Theme theme = new Theme() { Name = "Theme", ThemeTypeRef = 1, StageRef = stageId };
            int themeId = storage.AddTheme(theme);

            ThemeAssignment themeAssignment = new ThemeAssignment()
            {
                CurriculumAssignmentRef = curriculumAssignmentId,
                MaxScore = 1,
                ThemeRef = themeId
            };
            int themeAssignmentId = storage.AddThemeAssignment(themeAssignment);
            
            var themeAssignmentIds = storage.GetThemeAssignmentsByCurriculumAssignmentId(curriculumAssignmentId).Select(item => item.Id);
            storage.DeleteThemeAssignments(themeAssignmentIds);
            storage.GetThemeAssignments(themeAssignmentIds);
            Assert.AreEqual(true, false);
        }

        #endregion
    }
}
