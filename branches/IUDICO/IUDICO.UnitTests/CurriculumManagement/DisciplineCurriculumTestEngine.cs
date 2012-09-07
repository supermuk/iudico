using System.Linq;

using Castle.Windsor;

using IUDICO.Common.Models;
using IUDICO.Common.Models.Caching.Provider;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement;
using IUDICO.CurriculumManagement.Models;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.DisciplineManagement;
using IUDICO.DisciplineManagement.Models;
using IUDICO.DisciplineManagement.Models.Storage;
using IUDICO.LMS.Models;

using Moq;
using Moq.Protected;
using CourseModels = IUDICO.CourseManagement.Models;
using CurriculumModels = IUDICO.CurriculumManagement.Models;
using DisciplineModels = IUDICO.DisciplineManagement.Models;
using UserModels = IUDICO.UserManagement.Models;
using System;
using System.Collections.Generic;

namespace IUDICO.UnitTests.CurriculumManagement
{
    using IDataContext = CurriculumModels.IDataContext;

    public class DisciplineCurriculumTestEngine
    {
        #region Static members

        private static DisciplineCurriculumTestEngine instance;

        #endregion

        #region Private Properties

        private Mock<CurriculumManagementPlugin> MockCurriculumManagementPlugin { get; set; }

        private Mock<DisciplineManagementPlugin> MockDisciplineManagementPlugin { get; set; }

        private Mock<IDataContext> MockCurriculumDataContext { get; set; }

        private Mock<DisciplineManagement.Models.IDataContext> MockDisciplineDataContext { get; set; }

        private Mock<LmsService> MockLmsService { get; set; }

        private Mock<IUserService> MockUserService { get; set; }

        private Mock<ICourseService> MockCourseService { get; set; }

        private Mock<CachedCurriculumStorage> MockCurriculumStorage { get; set; }

        private Mock<DatabaseCurriculumStorage> MockDbCurriculumStorage { get; set; }

        private Mock<CachedDisciplineStorage> MockDisciplineStorage { get; set; }

        private Mock<DatabaseDisciplineStorage> MockDbDisciplineStorage { get; set; }

        private Mock<IWindsorContainer> MockWindsorContainer { get; set; }

        #endregion

        #region Public properties

        public CurriculumManagementPlugin CurriculumManagementPlugin
        {
            get
            {
                return this.MockCurriculumManagementPlugin.Object;
            }
        }

        public DisciplineManagementPlugin DisciplineManagementPlugin
        {
            get
            {
                return this.MockDisciplineManagementPlugin.Object;
            }
        }

        public IDataContext CurriculumDataContext
        {
            get
            {
                return this.MockCurriculumDataContext.Object;
            }
        }

        public DisciplineManagement.Models.IDataContext DisciplineDataContext
        {
            get
            {
                return this.MockDisciplineDataContext.Object;
            }
        }

        public ILmsService LmsService
        {
            get
            {
                return this.MockLmsService.Object;
            }
        }

        public ICourseService CourseService
        {
            get
            {
                return this.MockCourseService.Object;
            }
        }

        public IUserService UserService
        {
            get
            {
                return this.MockUserService.Object;
            }
        }

        public ICurriculumStorage CurriculumStorage
        {
            get
            {
                return this.MockCurriculumStorage.Object;
            }
        }

        public IDisciplineStorage DisciplineStorage
        {
            get
            {
                return this.MockDisciplineStorage.Object;
            }
        }

        public IWindsorContainer WindsorContainer
        {
            get
            {
                return this.MockWindsorContainer.Object;
            }
        }

        public DataPreparer DataPreparer { get; private set; }

        #endregion

        private DisciplineCurriculumTestEngine()
        {
            // Constructors
            this.MockCurriculumManagementPlugin = new Mock<CurriculumManagementPlugin>();
            this.MockDisciplineManagementPlugin = new Mock<DisciplineManagementPlugin>();

            this.MockCurriculumDataContext = new Mock<IDataContext>();
            this.MockDisciplineDataContext = new Mock<DisciplineManagement.Models.IDataContext>();

            this.MockWindsorContainer = new Mock<IWindsorContainer>();
            this.MockLmsService = new Mock<LmsService>(this.WindsorContainer);
            this.MockUserService = new Mock<IUserService>();
            this.MockCourseService = new Mock<ICourseService>();

            var mockCacheProvider = new Mock<HttpCache>();
            this.MockDbCurriculumStorage = new Mock<DatabaseCurriculumStorage>(this.LmsService);
            this.MockCurriculumStorage = new Mock<CachedCurriculumStorage>(this.MockDbCurriculumStorage.Object, mockCacheProvider.Object);
            this.MockDbDisciplineStorage = new Mock<DatabaseDisciplineStorage>(this.LmsService);
            this.MockDisciplineStorage = new Mock<CachedDisciplineStorage>(this.MockDbDisciplineStorage.Object, mockCacheProvider.Object);

            // Setup links
            this.MockDbCurriculumStorage.Protected().Setup<IDataContext>("GetDbContext").Returns(
                this.CurriculumDataContext);
            this.MockDbDisciplineStorage.Protected().Setup<DisciplineManagement.Models.IDataContext>("GetDbContext").Returns(this.DisciplineDataContext);

            this.MockWindsorContainer.Setup(l => l.Resolve<IUserService>()).Returns(this.MockUserService.Object);
            this.MockWindsorContainer.Setup(l => l.Resolve<ICourseService>()).Returns(this.MockCourseService.Object);
            this.MockWindsorContainer.Setup(l => l.Resolve<IDisciplineService>()).Returns(
                new DisciplineService(this.DisciplineStorage));
            this.MockWindsorContainer.Setup(l => l.Resolve<ICurriculumService>()).Returns(
                new CurriculumService(this.CurriculumStorage));
            this.MockWindsorContainer.Setup(l => l.Resolve<ICurriculumStorage>()).Returns(this.CurriculumStorage);
            this.MockWindsorContainer.Setup(l => l.Resolve<IDisciplineStorage>()).Returns(this.DisciplineStorage);
            this.MockWindsorContainer.Setup(c => c.ResolveAll<IPlugin>()).Returns(
                new IPlugin[] { this.CurriculumManagementPlugin, this.DisciplineManagementPlugin });
            this.CurriculumManagementPlugin.Install(this.WindsorContainer, null);
            this.DisciplineManagementPlugin.Install(this.WindsorContainer, null);

            this.DataPreparer = new DataPreparer(this.DisciplineStorage, this.CurriculumStorage, this.LmsService);

            // _MockCurriculumManagementPlugin.Protected().SetupGet<ICurriculumStorage>(p => p._CurriculumStorage).Returns(CurriculumStorage);
            // _MockDisciplineManagementPlugin.Protected().SetupGet<IDisciplineStorage>(p => p._DisciplineStorage).Returns(DisciplineStorage);
            // can be replace with: (butt call it in Setup())
            this.SetupData();
        }

        public static DisciplineCurriculumTestEngine GetInstance()
        {
            return instance ?? (instance = new DisciplineCurriculumTestEngine());
        }

        private void SetupData()
        {
            var courses = this.DataPreparer.GetCourses();
            var users = this.DataPreparer.GetUsers();
            var groups = this.DataPreparer.GetGroups();

            this.MockUserService.Setup(s => s.GetCurrentUser()).Returns(users[0]);
            this.MockUserService.Setup(s => s.GetGroups()).Returns(groups);
            this.MockUserService.Setup(s => s.GetGroup(1)).Returns(groups[0]);
            this.MockUserService.Setup(s => s.GetGroup(2)).Returns(groups[1]);
            this.MockUserService.Setup(s => s.GetUsers()).Returns(users);
            this.MockUserService.Setup(s => s.GetUsers(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> pred) => this.UserService.GetUsers().Where(pred));
            this.MockUserService.Setup(s => s.GetGroupsByUser(users[0])).Returns(new[] { groups[0] });
            this.MockUserService.Setup(s => s.GetGroupsByUser(users[1])).Returns(new[] { groups[1] });

            this.MockCourseService.Setup(s => s.GetCourses()).Returns(courses);
            this.MockCourseService.Setup(s => s.GetCourses(users[0])).Returns(courses.ToList().Take(2));
            this.MockCourseService.Setup(s => s.GetCourses(users[1])).Returns(courses.ToList().Skip(2).Take(1));
            this.MockCourseService.Setup(s => s.GetCourse(1)).Returns(courses[0]);
            this.MockCourseService.Setup(s => s.GetCourse(2)).Returns(courses[1]);
            this.MockCourseService.Setup(s => s.GetCourse(3)).Returns(courses[2]);
            this.MockCourseService.Setup(s => s.GetCourse(4)).Returns(courses[3]);

            this.ClearTables();
        }

        public void ClearTables()
        {
            // get back old current user
            this.SetCurrentUser(Users.Panza);

            var mockTopicTypes = this.DataPreparer.GetTopicTypes();

            this.MockDisciplineDataContext.SetupGet(c => c.Disciplines).Returns(new MemoryTable<Discipline>("Id"));
            this.MockDisciplineDataContext.SetupGet(c => c.Chapters).Returns(new MemoryTable<Chapter>("Id"));
            this.MockDisciplineDataContext.SetupGet(c => c.Topics).Returns(new MemoryTable<Topic>("Id"));
            this.MockDisciplineDataContext.SetupGet(c => c.TopicTypes).Returns(
                new MemoryTable<TopicType>(mockTopicTypes, "Id"));
            this.MockDisciplineDataContext.SetupGet(c => c.SharedDisciplines).Returns(
                new MemoryTable<SharedDiscipline>());
            this.MockCurriculumDataContext.SetupGet(c => c.Curriculums).Returns(new MemoryTable<Curriculum>("Id"));
            this.MockCurriculumDataContext.SetupGet(c => c.CurriculumChapters).Returns(
                new MemoryTable<CurriculumChapter>("Id"));
            this.MockCurriculumDataContext.SetupGet(c => c.CurriculumChapterTopics).Returns(
                new MemoryTable<CurriculumChapterTopic>("Id"));
        }

        public void SetCurrentUser(string userName)
        {
            this.MockUserService.Setup(s => s.GetCurrentUser()).Returns(
                this.UserService.GetUsers().First(user => user.Username == userName));
            this.MockDbDisciplineStorage.Setup(x=>x.GetCurrentUser()).Returns(
                 this.UserService.GetUsers().First(user => user.Username == userName));
        }
    }
}