using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.UserManagement.Models.Storage;
using IUDICO.CurriculumManagement.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using IUDICO.UnitTests.Fakes;

namespace IUDICO.UnitTests
{
    [TestClass]
    public class CurriculumStorageTest
    {
        ICurriculumStorage storage { get; set; }
        ILmsService lmsService { get; set; }
        DBDataContext context { get; set; } 

        private void ClearDb()
        {
            context = lmsService.GetDbDataContext();
            context.CurriculumAssignments.DeleteAllOnSubmit(context.CurriculumAssignments);
            context.Themes.DeleteAllOnSubmit(context.Themes);
            context.Stages.DeleteAllOnSubmit(context.Stages);
            context.Curriculums.DeleteAllOnSubmit(context.Curriculums);
            context.CurriculumAssignments.DeleteAllOnSubmit(context.CurriculumAssignments);
            context.SubmitChanges();
        }

        private void InitializeDb()
        {
            context.ThemeTypes.InsertOnSubmit(new ThemeType { Id = 1, Name = "Test" });
            context.ThemeTypes.InsertOnSubmit(new ThemeType { Id = 2, Name = "Theory" });

            context.Courses.InsertOnSubmit(new Course { Name = "Course", Created = DateTime.Now, Deleted = false, Updated = DateTime.Now });
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
                //context.DeleteDatabase();
            }
        }

        #region CurriculumController tests

        [TestMethod]
        public void CurriculumControllerTest1()
        {
            CurriculumController controller = new CurriculumController(new MixedCurriculumStorage(lmsService));
            ControllerContext context = new ControllerContext();
            context.RouteData = new RouteData();
            controller.ControllerContext = context;

            List<Curriculum> expectedCurriculums = new List<Curriculum>();
            expectedCurriculums.Add(new Curriculum { Name = "Curriculum1" });
            expectedCurriculums.Add(new Curriculum { Name = "Curriculum2" });
            expectedCurriculums.Add(new Curriculum { Name = "Curriculum3" });

            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(expectedCurriculums[0]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            controller.Create(expectedCurriculums[1]);
            controller.Create(expectedCurriculums[2]);

            controller.RouteData.Values["action"] = "Index";

            List<Curriculum> actualCurriculums = (((ViewResult)controller.Index()).ViewData.Model as IEnumerable<Curriculum>).ToList();
            AdvAssert.AreEqual(expectedCurriculums, actualCurriculums);
        }

        #endregion

        #region CurriculumTestMethods

        [TestMethod]
        public void AddCurriculumTest()
        {
            List<Curriculum> curriculums = new List<Curriculum>();
            curriculums.Add(new Curriculum() { Name = "Curriculum1" });
            curriculums.Add(new Curriculum() { Name = "Curriculum2" });
            curriculums.Add(new Curriculum() { Name = "Curriculum3" });
            curriculums.Add(new Curriculum() { Name = "Curriculum4" });

            //Test AddCurriculum() and GetCurriculum()
            List<int> ids = new List<int>();
            curriculums.ForEach(item => ids.Add(storage.AddCurriculum(item)));

            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculums[i], storage.GetCurriculum(ids[i])));
        }

        [TestMethod]
        public void GetCurriculumTests()
        {
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            List<User> users = userService.GetUsers().ToList();
            List<Curriculum> curriculums = new List<Curriculum>();
            curriculums.Add(new Curriculum() { Name = "Curriculum1" });
            curriculums.Add(new Curriculum() { Name = "Curriculum2" });
            curriculums.Add(new Curriculum() { Name = "Curriculum3" });
            curriculums.Add(new Curriculum() { Name = "Curriculum4" });
            curriculums.Add(new Curriculum() { Name = "Curriculum5" });
            curriculums.ForEach(item => storage.AddCurriculum(item));

            //Test GetCurriculums()
            AdvAssert.AreEqual(curriculums, storage.GetCurriculums().ToList());

            //Test GetCurriculumsByGroupId()
            storage.AddCurriculumAssignment(new CurriculumAssignment() { CurriculumRef = curriculums[0].Id, UserGroupRef = groups[0].Id });
            storage.AddCurriculumAssignment(new CurriculumAssignment() { CurriculumRef = curriculums[0].Id, UserGroupRef = groups[1].Id });
            storage.AddCurriculumAssignment(new CurriculumAssignment() { CurriculumRef = curriculums[1].Id, UserGroupRef = groups[0].Id });

            AdvAssert.AreEqual(curriculums.GetSpecificItems(0, 1), storage.GetCurriculumsByGroupId(groups[0].Id).ToList());

            //Test GetCurriculumsWithThemesOwnedByUser()
            List<int> stageIds = new List<int>();
            stageIds.Add(storage.AddStage(Utils.GetDefaultStage(curriculums[2].Id)));
            stageIds.Add(storage.AddStage(Utils.GetDefaultStage(curriculums[3].Id)));
            stageIds.Add(storage.AddStage(Utils.GetDefaultStage(curriculums[4].Id)));
            storage.AddTheme(Utils.GetDefaultTheme(stageIds[0], 1));
            storage.AddTheme(Utils.GetDefaultTheme(stageIds[0], 2));
            storage.AddTheme(Utils.GetDefaultTheme(stageIds[1], 3));
            storage.AddTheme(Utils.GetDefaultTheme(stageIds[2], 2));

            AdvAssert.AreEqual(curriculums.GetSpecificItems(2, 3), storage.GetCurriculumsWithThemesOwnedByUser(users[0]).ToList());
            AdvAssert.AreEqual(curriculums.GetSpecificItems(2, 4), storage.GetCurriculumsWithThemesOwnedByUser(users[1]).ToList());
        }

        [TestMethod]
        public void UpdateCurriculumTest()
        {
            //Test UpdateCurriculum()
            Curriculum curriculum = new Curriculum { Name = "Curriculum" };
            int id = storage.AddCurriculum(curriculum);
            curriculum.Name = "UpdatedCurriculum";
            storage.UpdateCurriculum(curriculum);

            AdvAssert.AreEqual(curriculum, storage.GetCurriculum(id));

            curriculum.Name = "SecondlyUpdatedCurriculum";
            Assert.AreNotEqual(curriculum.Name, storage.GetCurriculum(id).Name);
        }

        [TestMethod]
        public void DeleteCurriculumTest()
        {
            //Test DeleteCurriculum()
            int id = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            storage.DeleteCurriculum(id);
            Assert.AreEqual(null, storage.GetCurriculum(id));

            //Test DeleteCurriculums(), delete not all items
            List<int> ids = new List<int>();
            ids.Add(storage.AddCurriculum(Utils.GetDefaultCurriculum()));
            ids.Add(storage.AddCurriculum(Utils.GetDefaultCurriculum()));
            ids.Add(storage.AddCurriculum(Utils.GetDefaultCurriculum()));
            storage.DeleteCurriculums(ids.GetSpecificItems(0, 2));

            Assert.AreEqual(null, storage.GetCurriculum(ids[0]));
            Assert.AreNotEqual(null, storage.GetCurriculum(ids[1]));
            Assert.AreEqual(null, storage.GetCurriculum(ids[2]));
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

            AdvAssert.AreEqual(themes, storage.GetThemesByStageId(stageOneId).ToList());

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

            AdvAssert.AreEqual(theme, storage.GetTheme(themeId));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteThemeTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stageOne = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int stageOneId = storage.AddStage(stageOne);

            List<Course> courses = storage.GetCourses().ToList();

            Theme theme = new Theme() { Name = "Theme", StageRef = stageOneId, ThemeTypeRef = 1, CourseRef = courses[0].Id, IsDeleted = false, Updated = DateTime.Now, Created = DateTime.Now };
            int themeId = storage.AddTheme(theme);

            AdvAssert.AreEqual(theme, storage.GetTheme(themeId));

            theme.Name = "UpdatedTheme";
            storage.UpdateTheme(theme); //апдейтить,але не показує. через це не проходить тест і відповідно видалення нижче 
            AdvAssert.AreEqual(theme, storage.GetTheme(themeId));

            storage.DeleteTheme(themeId);
            storage.GetTheme(themeId);
            Assert.AreEqual(true, false);
        }

        [TestMethod]
        public void ThemeUpTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stageOne = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int stageOneId = storage.AddStage(stageOne);

            List<Course> courses = storage.GetCourses().ToList();

            Theme themeOne = new Theme() { Name = "Theme", StageRef = stageOneId, ThemeTypeRef = 1, CourseRef = courses[0].Id, IsDeleted = false, Updated = DateTime.Now, Created = DateTime.Now };
            int themeOneId = storage.AddTheme(themeOne);

            Theme themeTwo = new Theme() { Name = "Theme", StageRef = stageOneId, ThemeTypeRef = 1, CourseRef = courses[0].Id, IsDeleted = false, Updated = DateTime.Now, Created = DateTime.Now };
            int themeTwoId = storage.AddTheme(themeTwo);

            Assert.AreEqual(themeTwoId,storage.GetTheme(themeTwoId).SortOrder);
            int oldSortOrder = storage.GetTheme(themeOneId).SortOrder;

            storage.ThemeUp(themeTwoId);
            Assert.AreEqual(oldSortOrder,storage.GetTheme(themeTwoId).SortOrder);
        }

        [TestMethod]
        public void ThemeDownTest()
        {
            Curriculum curriculum = new Curriculum() { Name = "Curriculum" };
            int curriculumId = storage.AddCurriculum(curriculum);

            Stage stageOne = new Stage() { Name = "Stage", CurriculumRef = curriculumId };
            int stageOneId = storage.AddStage(stageOne);

            List<Course> courses = storage.GetCourses().ToList();

            Theme themeOne = new Theme() { Name = "Theme", StageRef = stageOneId, ThemeTypeRef = 1, CourseRef = courses[0].Id, IsDeleted = false, Updated = DateTime.Now, Created = DateTime.Now };
            int themeOneId = storage.AddTheme(themeOne);

            Theme themeTwo = new Theme() { Name = "Theme", StageRef = stageOneId, ThemeTypeRef = 1, CourseRef = courses[0].Id, IsDeleted = false, Updated = DateTime.Now, Created = DateTime.Now };
            int themeTwoId = storage.AddTheme(themeTwo);

            Assert.AreEqual(themeOneId, storage.GetTheme(themeOneId).SortOrder);
            int oldSortOrder = storage.GetTheme(themeTwoId).SortOrder;

            storage.ThemeDown(themeOneId);
            Assert.AreEqual(oldSortOrder, storage.GetTheme(themeOneId).SortOrder);
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

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);
            AdvAssert.AreEqual(curriculumAssignment, storage.GetCurriculumAssignment(curriculumAssignmentId));

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

            AdvAssert.AreEqual(curriculumAssignments, storage.GetCurriculumAssignmentsByGroupId(groups[0].Id).ToList());

            storage.DeleteCurriculumAssignment(curriculumAssignmentId);
            storage.GetCurriculumAssignment(curriculumAssignmentId);
            Assert.AreEqual(true, false);
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

            AdvAssert.AreEqual(timelines, storage.GetCurriculumAssignmentTimelines(curriculumAssignmentId).ToList());
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

            Timeline timeline = new Timeline() { StartDate = DateTime.Now, EndDate = DateTime.Now, CurriculumAssignmentRef = curriculumAssignmentId, StageRef = stageId };
            int timelineId = storage.AddTimeline(timeline);

            List<Timeline> stageTimelines = new List<Timeline>();
            stageTimelines.Add(timeline);

            AdvAssert.AreEqual(stageTimelines, storage.GetStageTimelinesByStageId(stageId).ToList());
            AdvAssert.AreEqual(stageTimelines, storage.GetStageTimelinesByCurriculumAssignmentId(curriculumAssignmentId).ToList());
            AdvAssert.AreEqual(stageTimelines, storage.GetStageTimelines(stageId, curriculumAssignmentId).ToList());
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

            AdvAssert.AreEqual(timeline, storage.GetTimeline(timelineId));
            timeline.StartDate = DateTime.Now;
            timeline.EndDate = DateTime.Now;

            storage.UpdateTimeline(timeline);
            AdvAssert.AreEqual(timeline, storage.GetTimeline(timelineId));

            storage.DeleteTimeline(timelineId);
            storage.GetTimeline(timelineId);
            Assert.AreEqual(true, false);
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

            AdvAssert.AreEqual(groups, storage.GetAssignedGroups(curriculumId).ToList());

            groups.Clear();
            groups.Add(groups[1]);
            groups.Add(groups[2]);
            AdvAssert.AreEqual(groups, storage.GetNotAssignedGroups(curriculumId).ToList());

            groups.Add(groups[0]);
            AdvAssert.AreEqual(groups, storage.GetNotAssignedGroupsWithCurrentGroup(curriculumId, groupOneId).ToList());
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
            AdvAssert.AreEqual(themeAssignments, storage.GetThemeAssignmentsByThemeId(themeId).ToList());

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
            AdvAssert.AreEqual(themeAssignment, storage.GetThemeAssignment(themeAssignmentId));
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
