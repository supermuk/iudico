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
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());

            List<Stage> stages = new List<Stage>();
            stages.Add(new Stage() { Name = "Stage1", CurriculumRef = curriculumId });
            stages.Add(new Stage() { Name = "Stage2", CurriculumRef = curriculumId });
            stages.Add(new Stage() { Name = "Stage3", CurriculumRef = curriculumId });
            stages.Add(new Stage() { Name = "Stage4", CurriculumRef = curriculumId });

            //Test AddStage() and GetStage()
            List<int> ids = new List<int>();
            stages.ForEach(item => ids.Add(storage.AddStage(item)));

            stages.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(stages[i], storage.GetStage(ids[i])));
        }

        [TestMethod]
        public void GetStageTest()
        {
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());

            List<Stage> stages = new List<Stage>();
            stages.Add(new Stage() { Name = "Stage1", CurriculumRef = curriculumId });
            stages.Add(new Stage() { Name = "Stage2", CurriculumRef = curriculumId });
            stages.Add(new Stage() { Name = "Stage3", CurriculumRef = curriculumId });
            stages.Add(new Stage() { Name = "Stage4", CurriculumRef = curriculumId });
            stages.Add(new Stage() { Name = "Stage5", CurriculumRef = curriculumId });
            List<int> ids = new List<int>();
            stages.ForEach(item => ids.Add(storage.AddStage(item)));

            //Test GetStages()
            AdvAssert.AreEqual(stages, storage.GetStages(curriculumId).ToList());

            //Test GetStage()
            stages.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(stages[i], storage.GetStage(ids[i])));
        }

        [TestMethod]
        public void UpdateStageTest()
        {
            //Test UpdateStage()
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());

            Stage stage = new Stage { Name = "Stage", CurriculumRef = curriculumId };
            int stageId = storage.AddStage(stage);
            stage.Name = "UpdatedStage";
            storage.UpdateStage(stage);

            AdvAssert.AreEqual(stage, storage.GetStage(stageId));

            stage.Name = "SecondlyUpdatedStage";
            Assert.AreNotEqual(stage.Name, storage.GetStage(stageId).Name);
        }

        [TestMethod]
        public void DeleteStageTest()
        {
            //Test DeleteStage()
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            int stageId = storage.AddStage(Utils.GetDefaultStage(curriculumId));
            storage.DeleteStage(stageId);
            Assert.AreEqual(null, storage.GetStage(stageId));

            //Test DeleteStages(), delete not all items
            List<int> ids = new List<int>();
            ids.Add(storage.AddStage(Utils.GetDefaultStage(curriculumId)));
            ids.Add(storage.AddStage(Utils.GetDefaultStage(curriculumId)));
            ids.Add(storage.AddStage(Utils.GetDefaultStage(curriculumId)));
            storage.DeleteStages(ids.GetSpecificItems(0, 2));

            Assert.AreEqual(null, storage.GetStage(ids[0]));
            Assert.AreNotEqual(null, storage.GetStage(ids[1]));
            Assert.AreEqual(null, storage.GetStage(ids[2]));
        }

        #endregion

        #region ThemeMethods

        [TestMethod]
        public void AddThemeTest()
        {
            ICourseService courseService = lmsService.FindService<ICourseService>();
            List<Course> courses = courseService.GetCourses().ToList();
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            int stageId = storage.AddStage(Utils.GetDefaultStage(curriculumId));

            List<Theme> themes = new List<Theme>();
            themes.Add(Utils.GetDefaultTheme(stageId,courses[0].Id));
            themes.Add(Utils.GetDefaultTheme(stageId, courses[1].Id));
            themes.Add(Utils.GetDefaultTheme(stageId, courses[2].Id));

            //Test AddTheme() and GetTheme()
            List<int> ids = new List<int>();
            themes.ForEach(item => ids.Add(storage.AddTheme(item)));

            themes.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(themes[i], storage.GetTheme(ids[i])));
        }

        [TestMethod]
        public void GetThemeTests()
        {
            ICourseService courseService = lmsService.FindService<ICourseService>();
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            List<Course> courses = courseService.GetCourses().ToList();
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            int stageOneId = storage.AddStage(Utils.GetDefaultStage(curriculumId));
            int stageTwoId = storage.AddStage(Utils.GetDefaultStage(curriculumId));

            List<Theme> themes = new List<Theme>();
            themes.Add(Utils.GetDefaultTheme(stageOneId, courses[0].Id));
            themes.Add(Utils.GetDefaultTheme(stageOneId, courses[1].Id));
            themes.Add(Utils.GetDefaultTheme(stageTwoId, courses[2].Id));

            List<int> ids = new List<int>();
            themes.ForEach(item => ids.Add(storage.AddTheme(item)));
            
            //Test GetThemesByStageId()
            AdvAssert.AreEqual(themes.GetSpecificItems(0,1), storage.GetThemesByStageId(stageOneId).ToList());

            //Test GetThemesByCourseId()
            AdvAssert.AreEqual(themes.GetSpecificItems(2), storage.GetThemesByCourseId(courses[2].Id).ToList());

            //Test GetThemesByCurriculumId()
            AdvAssert.AreEqual(themes, storage.GetThemesByCurriculumId(curriculumId).ToList());

            //Test GetThemesByGroupId()
            int curriculumAssignmentId = storage.AddCurriculumAssignment(Utils.GetDefaultCurriculumAssignment(curriculumId, groups[0].Id));
            AdvAssert.AreEqual(themes, storage.GetThemesByGroupId(groups[0].Id).ToList());
        }

        [TestMethod]
        public void UpdateThemeTests()
        {
            //Test UpdateTheme()
            ICourseService courseService = lmsService.FindService<ICourseService>();
            List<Course> courses = courseService.GetCourses().ToList();
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            int stageId = storage.AddStage(Utils.GetDefaultStage(curriculumId));
            Theme theme = new Theme() { Name = "Theme", StageRef = stageId, CourseRef = courses[0].Id, ThemeTypeRef = 1 };
            int id = storage.AddTheme(theme);
            theme.Name = "UpdatedTheme";
            storage.UpdateTheme(theme);

            AdvAssert.AreEqual(theme, storage.GetTheme(id));

            theme.Name = "SecondlyUpdatedTheme";
            Assert.AreNotEqual(theme.Name, storage.GetTheme(id).Name);
        }

        [TestMethod]
        public void DeleteThemeTest()
        {
            //Test DeleteTheme()
            ICourseService courseService = lmsService.FindService<ICourseService>();
            List<Course> courses = courseService.GetCourses().ToList();
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            int stageId = storage.AddStage(Utils.GetDefaultStage(curriculumId));
            int id = storage.AddTheme(Utils.GetDefaultTheme(stageId, courses[0].Id));
            storage.DeleteTheme(id);
            Assert.AreEqual(null, storage.GetTheme(id));

            //Test DeleteThemes(), delete not all items
            List<int> ids = new List<int>();
            ids.Add(storage.AddTheme(Utils.GetDefaultTheme(stageId,courses[0].Id)));
            ids.Add(storage.AddTheme(Utils.GetDefaultTheme(stageId, courses[1].Id)));
            ids.Add(storage.AddTheme(Utils.GetDefaultTheme(stageId, courses[2].Id)));
            storage.DeleteThemes(ids.GetSpecificItems(0, 2));

            Assert.AreEqual(null, storage.GetTheme(ids[0]));
            Assert.AreNotEqual(null, storage.GetTheme(ids[1]));
            Assert.AreEqual(null, storage.GetTheme(ids[2]));
        }

        [TestMethod]
        public void ThemeUpTest()
        {
            ICourseService courseService = lmsService.FindService<ICourseService>();
            List<Course> courses = courseService.GetCourses().ToList();
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            int stageId = storage.AddStage(Utils.GetDefaultStage(curriculumId));

            List<Theme> themes = new List<Theme>();
            Theme themeOne = new Theme() { Name = "ThemeOne", ThemeTypeRef = 1, CourseRef = courses[0].Id, StageRef = stageId };
            Theme themeTwo = new Theme() { Name = "ThemeTwo", ThemeTypeRef = 1, CourseRef = courses[0].Id, StageRef = stageId };

            List<int> ids = new List<int>();
            themes.ForEach(item => ids.Add(storage.AddTheme(item)));

            Assert.AreEqual(ids[1], storage.GetTheme(ids[1]).SortOrder);
            storage.ThemeUp(ids[1]);
            Assert.AreEqual(ids[0], storage.GetTheme(ids[1]).SortOrder);
        }

        [TestMethod]
        public void ThemeDownTest()
        {
            ICourseService courseService = lmsService.FindService<ICourseService>();
            List<Course> courses = courseService.GetCourses().ToList();
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            int stageId = storage.AddStage(Utils.GetDefaultStage(curriculumId));

            List<Theme> themes = new List<Theme>();
            Theme themeOne = new Theme() { Name = "ThemeOne", ThemeTypeRef = 1, CourseRef = courses[0].Id, StageRef = stageId };
            Theme themeTwo = new Theme() { Name = "ThemeTwo", ThemeTypeRef = 1, CourseRef = courses[0].Id, StageRef = stageId };

            List<int> ids = new List<int>();
            themes.ForEach(item => ids.Add(storage.AddTheme(item)));

            Assert.AreEqual(ids[0], storage.GetTheme(ids[0]).SortOrder);
            storage.ThemeDown(ids[0]);
            Assert.AreEqual(ids[1], storage.GetTheme(ids[0]).SortOrder);
        }

        #endregion

        #region CurriculumAssignmentMethods

        [TestMethod]
        public void AddCurriculumAssignmentTest()
        {
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            List<CurriculumAssignment> curriculumAssignments = new List<CurriculumAssignment>();
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            curriculumAssignments.Add(new CurriculumAssignment() { CurriculumRef = curriculumId , UserGroupRef = groups[0].Id});
            curriculumAssignments.Add(new CurriculumAssignment() { CurriculumRef = curriculumId , UserGroupRef = groups[0].Id });
            curriculumAssignments.Add(new CurriculumAssignment() { CurriculumRef = curriculumId , UserGroupRef = groups[0].Id });
            curriculumAssignments.Add(new CurriculumAssignment() { CurriculumRef = curriculumId , UserGroupRef = groups[0].Id });

            //Test AddCurriculumAssignment() and GetCurriculumAssingment()
            List<int> ids = new List<int>();
            curriculumAssignments.ForEach(item => ids.Add(storage.AddCurriculumAssignment(item)));

            curriculumAssignments.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculumAssignments[i], storage.GetCurriculumAssignment(ids[i])));
        }

        [TestMethod]
        public void GetCurriculumAssignmentTests()
        {
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            int curriculumOneId = storage.AddCurriculum(new Curriculum { Name = "Curriculum1" });
            int curriculumTwoId = storage.AddCurriculum(new Curriculum { Name = "Curriculum2" });
            List<CurriculumAssignment> curriculumAssingmnets = new List<CurriculumAssignment>();
            List<int> ids = new List<int>();

            curriculumAssingmnets.Add(new CurriculumAssignment() { CurriculumRef = curriculumOneId, UserGroupRef = groups[0].Id });
            curriculumAssingmnets.Add(new CurriculumAssignment() { CurriculumRef = curriculumTwoId, UserGroupRef = groups[1].Id });
            curriculumAssingmnets.ForEach(item => ids.Add(storage.AddCurriculumAssignment(item)));

            //Test GetCurriculumAssingnments()
            AdvAssert.AreEqual(curriculumAssingmnets, storage.GetCurriculumAssignments().ToList());

            //Test GetCurriculumAssignment()
            curriculumAssingmnets.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculumAssingmnets[i], storage.GetCurriculumAssignment(ids[i])));

            //Test GetCurriculumAssignmnetsByCurriculumId()
            AdvAssert.AreEqual(curriculumAssingmnets.GetSpecificItems(0), storage.GetCurriculumAssignmnetsByCurriculumId(curriculumOneId).ToList());

            //Test GetCurriculumAssignmentByGroupId()
            AdvAssert.AreEqual(curriculumAssingmnets.GetSpecificItems(1), storage.GetCurriculumAssignmentsByGroupId(groups[1].Id).ToList());
        }

        [TestMethod]
        public void UpdateCurriculumAssignmentTests()
        {
            //Test UpdateCurriculumAssignment()
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();

            CurriculumAssignment curriculumAssignment = new CurriculumAssignment(){ CurriculumRef = curriculumId, UserGroupRef = groups[0].Id };
            int curriculumAssignmentId = storage.AddCurriculumAssignment(curriculumAssignment);
            AdvAssert.AreEqual(curriculumAssignment, storage.GetCurriculumAssignment(curriculumAssignmentId));

            curriculumAssignment.UserGroupRef = groups[1].Id;
            storage.UpdateCurriculumAssignment(curriculumAssignment);
            AdvAssert.AreEqual(curriculumAssignment, storage.GetCurriculumAssignment(curriculumAssignmentId));

            //Secondary update
            curriculumAssignment.UserGroupRef = groups[2].Id;
            Assert.AreNotEqual(curriculumAssignment.UserGroupRef, storage.GetCurriculumAssignment(curriculumAssignmentId).UserGroupRef);
        }

        [TestMethod]
        public void DeleteCurriculumAssignmentTest()
        {
            //Test DeleteCurriculumAssignment()
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            
            int curriculumAssignmentId = storage.AddCurriculumAssignment(Utils.GetDefaultCurriculumAssignment(curriculumId,groups[0].Id));
            storage.DeleteCurriculumAssignment(curriculumAssignmentId);
            Assert.AreEqual(null, storage.GetCurriculumAssignment(curriculumAssignmentId));

            //Test DeleteCurriculumAssignments(), delete not all items
            List<int> ids = new List<int>();
            ids.Add(storage.AddCurriculumAssignment(new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id }));
            ids.Add(storage.AddCurriculumAssignment(new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id }));
            ids.Add(storage.AddCurriculumAssignment(new CurriculumAssignment() { CurriculumRef = curriculumId, UserGroupRef = groups[0].Id }));
            storage.DeleteCurriculums(ids.GetSpecificItems(0, 2));

            Assert.AreEqual(null, storage.GetCurriculumAssignment(ids[0]));
            Assert.AreNotEqual(null, storage.GetCurriculumAssignment(ids[1]));
            Assert.AreEqual(null, storage.GetCurriculumAssignment(ids[2]));
        }

        #endregion

        #region TimelineMethods

        [TestMethod]
        public void AddTimelineTest()
        {
            //Test AddTimeline() and GetTimeline()
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            int curriculumAssignmentId = storage.AddCurriculumAssignment(Utils.GetDefaultCurriculumAssignment(curriculumId, groups[0].Id));

            List<Timeline> timelines = new List<Timeline>();
            timelines.Add(new Timeline() { CurriculumAssignmentRef = curriculumAssignmentId, StartDate = DateTime.Now, EndDate = DateTime.Now });
            timelines.Add(new Timeline() { CurriculumAssignmentRef = curriculumAssignmentId, StartDate = DateTime.Now, EndDate = DateTime.Now });
            timelines.Add(new Timeline() { CurriculumAssignmentRef = curriculumAssignmentId, StartDate = DateTime.Now, EndDate = DateTime.Now });
            timelines.Add(new Timeline() { CurriculumAssignmentRef = curriculumAssignmentId, StartDate = DateTime.Now, EndDate = DateTime.Now });

            List<int> ids = new List<int>();
            timelines.ForEach(item => ids.Add(storage.AddTimeline(item)));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], storage.GetTimeline(ids[i])));
        }

        [TestMethod]
        public void GetTimelineTests()
        {
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            int stageId = storage.AddStage(Utils.GetDefaultStage(curriculumId));
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            int curriculumAssignmentId = storage.AddCurriculumAssignment(Utils.GetDefaultCurriculumAssignment(curriculumId, groups[0].Id));

            List<Timeline> timelines = new List<Timeline>();
            timelines.Add(new Timeline() { CurriculumAssignmentRef = curriculumAssignmentId, StartDate = DateTime.Now, EndDate = DateTime.Now, StageRef = stageId});
            timelines.Add(new Timeline() { CurriculumAssignmentRef = curriculumAssignmentId, StartDate = DateTime.Now, EndDate = DateTime.Now});
            timelines.Add(new Timeline() { CurriculumAssignmentRef = curriculumAssignmentId, StartDate = DateTime.Now, EndDate = DateTime.Now, StageRef = stageId});
            List<int> ids = new List<int>();
            timelines.ForEach(item => ids.Add(storage.AddTimeline(item)));

            //Test GetCurriculumAssignmentTimelines()
            AdvAssert.AreEqual(timelines.GetSpecificItems(0, 2), storage.GetCurriculumAssignmentTimelines(curriculumAssignmentId).ToList());

            //Test GetStageTimelinesByStageId()
            AdvAssert.AreEqual(timelines.GetSpecificItems(1), storage.GetStageTimelinesByStageId(stageId).ToList());

            //Test GetStageTimelines()
            AdvAssert.AreEqual(timelines.GetSpecificItems(1), storage.GetStageTimelines(stageId,curriculumAssignmentId).ToList());
        }

        [TestMethod]
        public void UpdateTimelineTest()
        {
            //Test UpdateTimeline()
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            int curriculumAssignmentId = storage.AddCurriculumAssignment(Utils.GetDefaultCurriculumAssignment(curriculumId, groups[0].Id));

            Timeline timeline = new Timeline { CurriculumAssignmentRef = curriculumAssignmentId, StartDate = DateTime.Now, EndDate = DateTime.Now };
            int timelineId = storage.AddTimeline(timeline);
            timeline.StartDate = DateTime.Now;
            storage.UpdateTimeline(timeline);

            AdvAssert.AreEqual(timeline, storage.GetTimeline(timelineId));

            //Secondary update
            timeline.StartDate = DateTime.Now;
            Assert.AreNotEqual(timeline.StartDate, storage.GetTimeline(timelineId).StartDate);
        }

        [TestMethod]
        public void DeleteTimelineTest()
        {
            //Test DeleteTimeline()
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            int curriculumAssignmentId = storage.AddCurriculumAssignment(Utils.GetDefaultCurriculumAssignment(curriculumId, groups[0].Id));

            Timeline timeline = new Timeline { CurriculumAssignmentRef = curriculumAssignmentId, StartDate = DateTime.Now, EndDate = DateTime.Now };
            int timelineId = storage.AddTimeline(timeline);
            storage.DeleteTimeline(timelineId);
            Assert.AreEqual(null, storage.GetTimeline(timelineId));

            //Test DeleteTimelines(), delete not all items
            List<int> ids = new List<int>();
            ids.Add(storage.AddTimeline(new Timeline() { CurriculumAssignmentRef = curriculumAssignmentId, StartDate = DateTime.Now, EndDate = DateTime.Now }));
            ids.Add(storage.AddTimeline(new Timeline() { CurriculumAssignmentRef = curriculumAssignmentId, StartDate = DateTime.Now, EndDate = DateTime.Now }));
            ids.Add(storage.AddTimeline(new Timeline() { CurriculumAssignmentRef = curriculumAssignmentId, StartDate = DateTime.Now, EndDate = DateTime.Now }));
            storage.DeleteTimelines(ids.GetSpecificItems(0, 2));

            Assert.AreEqual(null, storage.GetTimeline(ids[0]));
            Assert.AreNotEqual(null, storage.GetTimeline(ids[1]));
            Assert.AreEqual(null, storage.GetTimeline(ids[2]));
        }

        #endregion

        #region GroupMethods

        [TestMethod]
        public void GetGroupTests()
        {
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            List<CurriculumAssignment> curriculumAssignments = new List<CurriculumAssignment>();
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            curriculumAssignments.Add(Utils.GetDefaultCurriculumAssignment(curriculumId, groups[0].Id));
            curriculumAssignments.Add(Utils.GetDefaultCurriculumAssignment(curriculumId, groups[1].Id));
            curriculumAssignments.Add(Utils.GetDefaultCurriculumAssignment(curriculumId, groups[0].Id));

            //Test GetAssignedGroups()
            AdvAssert.AreEqual(groups.GetSpecificItems(0, 1), storage.GetAssignedGroups(curriculumId).ToList());

            //Test GetNotAssignedGroups()
            AdvAssert.AreEqual(groups.GetSpecificItems(2), storage.GetNotAssignedGroups(curriculumId).ToList());

            //Test GetNotAssignedGroupsWithCurrentGroup()
            AdvAssert.AreEqual(groups.GetSpecificItems(1,2), storage.GetNotAssignedGroupsWithCurrentGroup(curriculumId,groups[1].Id).ToList());
        }

        #endregion

        #region ThemeAssignmentMethods

        [TestMethod]
        public void AddThemeAssignmentTest()
        {
            //Test AddThemeAssignment and GetThemeAssignment()
            ICourseService courseService = lmsService.FindService<ICourseService>();
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            List<Course> courses = courseService.GetCourses().ToList();
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            int stageId = storage.AddStage(Utils.GetDefaultStage(curriculumId));
            int themeId = storage.AddTheme(new Theme() { Name = "Theme", CourseRef = courses[0].Id, StageRef = stageId, ThemeTypeRef = 1 });
            int curriculumAssignmentId = storage.AddCurriculumAssignment(Utils.GetDefaultCurriculumAssignment(curriculumId, groups[0].Id));

            List<ThemeAssignment> themeAssignments = new List<ThemeAssignment>();
            themeAssignments.Add(new ThemeAssignment() { CurriculumAssignmentRef = curriculumAssignmentId, MaxScore = 1, IsDeleted = false, ThemeRef = themeId});
            themeAssignments.Add(new ThemeAssignment() { CurriculumAssignmentRef = curriculumAssignmentId, MaxScore = 1, IsDeleted = false, ThemeRef = themeId });
            themeAssignments.Add(new ThemeAssignment() { CurriculumAssignmentRef = curriculumAssignmentId, MaxScore = 1, IsDeleted = false, ThemeRef = themeId });
            themeAssignments.Add(new ThemeAssignment() { CurriculumAssignmentRef = curriculumAssignmentId, MaxScore = 1, IsDeleted = false, ThemeRef = themeId });

            List<int> ids = new List<int>();
            themeAssignments.ForEach(item => ids.Add(storage.AddThemeAssignment(item)));

            themeAssignments.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(themeAssignments[i], storage.GetThemeAssignment(ids[i])));
        }

        [TestMethod]
        public void GetThemeAssignmentTest()
        {
            ICourseService courseService = lmsService.FindService<ICourseService>();
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            List<Course> courses = courseService.GetCourses().ToList();
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            int stageId = storage.AddStage(Utils.GetDefaultStage(curriculumId));

            int themeOneId = storage.AddTheme(new Theme() { Name = "Theme", CourseRef = courses[0].Id, StageRef = stageId, ThemeTypeRef = 1 });
            int themeTwoId = storage.AddTheme(new Theme() { Name = "Theme", CourseRef = courses[1].Id, StageRef = stageId, ThemeTypeRef = 1 });

            int curriculumAssignmentOneId = storage.AddCurriculumAssignment(Utils.GetDefaultCurriculumAssignment(curriculumId, groups[0].Id));
            int curriculumAssignmentTwoId = storage.AddCurriculumAssignment(Utils.GetDefaultCurriculumAssignment(curriculumId, groups[1].Id));

            List<ThemeAssignment> themeAssignments = new List<ThemeAssignment>();
            themeAssignments.Add(new ThemeAssignment() { CurriculumAssignmentRef = curriculumAssignmentOneId, MaxScore = 1, IsDeleted = false, ThemeRef = themeTwoId });
            themeAssignments.Add(new ThemeAssignment() { CurriculumAssignmentRef = curriculumAssignmentOneId, MaxScore = 1, IsDeleted = false, ThemeRef = themeTwoId });
            themeAssignments.Add(new ThemeAssignment() { CurriculumAssignmentRef = curriculumAssignmentTwoId, MaxScore = 1, IsDeleted = false, ThemeRef = themeOneId });
            themeAssignments.Add(new ThemeAssignment() { CurriculumAssignmentRef = curriculumAssignmentTwoId, MaxScore = 1, IsDeleted = false, ThemeRef = themeOneId });
            themeAssignments.ForEach(item => storage.AddThemeAssignment(item));

            //Test GetThemeAssignmentsByCurriculumAssignmentId()
            AdvAssert.AreEqual(themeAssignments.GetSpecificItems(2, 3), storage.GetThemeAssignmentsByCurriculumAssignmentId(curriculumAssignmentTwoId).ToList());

            //Test GetThemeAssignmentsByThemeId()
            AdvAssert.AreEqual(themeAssignments.GetSpecificItems(0, 1), storage.GetThemeAssignmentsByThemeId(themeTwoId).ToList());
        }

        [TestMethod]
        public void UpdateThemeAssignmentTest()
        {
            //Test UpdateThemeAssignment()
            ICourseService courseService = lmsService.FindService<ICourseService>();
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            List<Course> courses = courseService.GetCourses().ToList();
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            int stageId = storage.AddStage(Utils.GetDefaultStage(curriculumId));
            int themeId = storage.AddTheme(new Theme() { Name = "Theme", CourseRef = courses[0].Id, StageRef = stageId, ThemeTypeRef = 1 });
            int curriculumAssignmentId = storage.AddCurriculumAssignment(Utils.GetDefaultCurriculumAssignment(curriculumId, groups[0].Id));

            ThemeAssignment themeAssignment = new ThemeAssignment() { ThemeRef = themeId, MaxScore = 100, IsDeleted = false, CurriculumAssignmentRef = curriculumAssignmentId };
            int themeAssignmentId = storage.AddThemeAssignment(themeAssignment);
            themeAssignment.MaxScore = 1;
            storage.UpdateThemeAssignment(themeAssignment);

            AdvAssert.AreEqual(themeAssignment, storage.GetThemeAssignment(themeAssignmentId));

            //Secondary update
            themeAssignment.MaxScore = 50;
            Assert.AreNotEqual(themeAssignment.MaxScore, storage.GetThemeAssignment(themeAssignmentId).MaxScore);
        }

        [TestMethod]
        public void DeleteThemeAssignmentTest()
        {
            //Test DeleteThemeAssignments(), delete not all items
            ICourseService courseService = lmsService.FindService<ICourseService>();
            IUserService userService = lmsService.FindService<IUserService>();
            List<Group> groups = userService.GetGroups().ToList();
            List<Course> courses = courseService.GetCourses().ToList();
            int curriculumId = storage.AddCurriculum(Utils.GetDefaultCurriculum());
            int stageId = storage.AddStage(Utils.GetDefaultStage(curriculumId));
            int themeId = storage.AddTheme(new Theme() { Name = "Theme", CourseRef = courses[0].Id, StageRef = stageId, ThemeTypeRef = 1 });
            int curriculumAssignmentId = storage.AddCurriculumAssignment(Utils.GetDefaultCurriculumAssignment(curriculumId, groups[0].Id));

            List<int> ids = new List<int>();
            ids.Add(storage.AddThemeAssignment(new ThemeAssignment() { CurriculumAssignmentRef = curriculumAssignmentId, IsDeleted = false, MaxScore = 100, ThemeRef = themeId }));
            ids.Add(storage.AddThemeAssignment(new ThemeAssignment() { CurriculumAssignmentRef = curriculumAssignmentId, IsDeleted = false, MaxScore = 100, ThemeRef = themeId }));
            ids.Add(storage.AddThemeAssignment(new ThemeAssignment() { CurriculumAssignmentRef = curriculumAssignmentId, IsDeleted = false, MaxScore = 100, ThemeRef = themeId }));
            storage.DeleteThemeAssignments(ids.GetSpecificItems(0, 2));

            Assert.AreEqual(null, storage.GetThemeAssignment(ids[0]));
            Assert.AreNotEqual(null, storage.GetThemeAssignment(ids[1]));
            Assert.AreEqual(null, storage.GetThemeAssignment(ids[2]));
        }

        #endregion
    }
}
