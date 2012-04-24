using System;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Models;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.DisciplineManagement;
using IUDICO.DisciplineManagement.Models;
using IUDICO.DisciplineManagement.Models.Storage;
using IUDICO.LMS.Models;
using Moq;
using Moq.Protected;

using CurriculumModels = IUDICO.CurriculumManagement.Models;
using DisciplineModels = IUDICO.DisciplineManagement.Models;
using UserModels = IUDICO.UserManagement.Models;
using CourseModels = IUDICO.CourseManagement.Models;
using Castle.Windsor;
using IUDICO.CurriculumManagement;

namespace IUDICO.UnitTests.CurriculumManagement
{
    public class DisciplineCurriculumTestEngine
    {
        #region Protected members

        protected static DisciplineCurriculumTestEngine _Instance;

        #endregion

        #region Private Properties

        private Mock<CurriculumManagementPlugin> _MockCurriculumManagementPlugin { get; set; }

        private Mock<DisciplineManagementPlugin> _MockDisciplineManagementPlugin { get; set; }

        private Mock<CurriculumModels.IDataContext> _MockCurriculumDataContext { get; set; }

        private Mock<DisciplineModels.IDataContext> _MockDisciplineDataContext { get; set; }

        private Mock<LmsService> _MockLmsService { get; set; }

        private Mock<IUserService> _MockUserService { get; set; }

        private Mock<ICourseService> _MockCourseService { get; set; }

        private Mock<DatabaseCurriculumStorage> _MockCurriculumStorage { get; set; }

        private Mock<DatabaseDisciplineStorage> _MockDisciplineStorage { get; set; }

        private Mock<IWindsorContainer> _MockWindsorContainer { get; set; }

        #endregion

        #region Public properties

        public CurriculumManagementPlugin CurriculumManagementPlugin
        {
            get { return _MockCurriculumManagementPlugin.Object; }
        }

        public DisciplineManagementPlugin DisciplineManagementPlugin
        {
            get { return _MockDisciplineManagementPlugin.Object; }
        }

        public CurriculumModels.IDataContext CurriculumDataContext
        {
            get { return _MockCurriculumDataContext.Object; }
        }

        public DisciplineModels.IDataContext DisciplineDataContext
        {
            get { return _MockDisciplineDataContext.Object; }
        }

        public ILmsService LmsService
        {
            get { return _MockLmsService.Object; }
        }

        public ICourseService CourseService
        {
            get { return _MockCourseService.Object; }
        }

        public IUserService UserService
        {
            get { return _MockUserService.Object; }
        }

        public ICurriculumStorage CurriculumStorage
        {
            get { return _MockCurriculumStorage.Object; }
        }

        public IDisciplineStorage DisciplineStorage
        {
            get { return _MockDisciplineStorage.Object; }
        }

        public IWindsorContainer WindsorContainer
        {
            get { return _MockWindsorContainer.Object; }
        }

        public DataPreparer DataPreparer { get; private set; }

        #endregion

        private DisciplineCurriculumTestEngine()
        {
            //Constructors
            _MockCurriculumManagementPlugin = new Mock<CurriculumManagementPlugin>();
            _MockDisciplineManagementPlugin = new Mock<DisciplineManagementPlugin>();

            _MockCurriculumDataContext = new Mock<CurriculumModels.IDataContext>();
            _MockDisciplineDataContext = new Mock<DisciplineModels.IDataContext>();

            _MockWindsorContainer = new Mock<IWindsorContainer>();
            _MockLmsService = new Mock<LmsService>(WindsorContainer);
            _MockUserService = new Mock<IUserService>();
            _MockCourseService = new Mock<ICourseService>();

            _MockCurriculumStorage = new Mock<DatabaseCurriculumStorage>(LmsService);
            _MockDisciplineStorage = new Mock<DatabaseDisciplineStorage>(LmsService);

            //Setup links
            _MockCurriculumStorage.Protected().Setup<CurriculumModels.IDataContext>("GetDbContext").Returns(CurriculumDataContext);
            _MockDisciplineStorage.Protected().Setup<DisciplineModels.IDataContext>("GetDbContext").Returns(DisciplineDataContext);

            _MockWindsorContainer.Setup(l => l.Resolve<IUserService>()).Returns(_MockUserService.Object);
            _MockWindsorContainer.Setup(l => l.Resolve<ICourseService>()).Returns(_MockCourseService.Object);
            _MockWindsorContainer.Setup(l => l.Resolve<IDisciplineService>()).Returns(new DisciplineService(DisciplineStorage));
            _MockWindsorContainer.Setup(l => l.Resolve<ICurriculumService>()).Returns(new CurriculumService(CurriculumStorage));
            _MockWindsorContainer.Setup(l => l.Resolve<ICurriculumStorage>()).Returns(CurriculumStorage);
            _MockWindsorContainer.Setup(l => l.Resolve<IDisciplineStorage>()).Returns(DisciplineStorage);
            _MockWindsorContainer.Setup(c => c.ResolveAll<IPlugin>())
                .Returns(new IPlugin[]
                {
                    CurriculumManagementPlugin,
                    DisciplineManagementPlugin
                });
            CurriculumManagementPlugin.Install(WindsorContainer, null);
            DisciplineManagementPlugin.Install(WindsorContainer, null);

            DataPreparer = new DataPreparer(DisciplineStorage, CurriculumStorage, LmsService);

            //_MockCurriculumManagementPlugin.Protected().SetupGet<ICurriculumStorage>(p => p._CurriculumStorage).Returns(CurriculumStorage);
            //_MockDisciplineManagementPlugin.Protected().SetupGet<IDisciplineStorage>(p => p._DisciplineStorage).Returns(DisciplineStorage);
            //can be replace with: (butt call it in Setup())

            SetupData();
        }

        public static DisciplineCurriculumTestEngine GetInstance()
        {
            return _Instance ?? (_Instance = new DisciplineCurriculumTestEngine());
        }

        private void SetupData()
        {
            var courses = DataPreparer.GetCourses();
            var users = DataPreparer.GetUsers();
            var groups = DataPreparer.GetGroups();

            _MockUserService.Setup(s => s.GetCurrentUser()).Returns(users[0]);
            _MockUserService.Setup(s => s.GetGroups()).Returns(groups);
            _MockUserService.Setup(s => s.GetGroup(1)).Returns(groups[0]);
            _MockUserService.Setup(s => s.GetGroup(2)).Returns(groups[1]);
            _MockUserService.Setup(s => s.GetUsers()).Returns(users);
            _MockUserService.Setup(s => s.GetGroupsByUser(users[0])).Returns(new[] { groups[0] });
            _MockUserService.Setup(s => s.GetGroupsByUser(users[1])).Returns(new[] { groups[1] });

            _MockCourseService.Setup(s => s.GetCourses()).Returns(courses);
            _MockCourseService.Setup(s => s.GetCourses(users[0])).Returns(courses.ToList().Take(2));
            _MockCourseService.Setup(s => s.GetCourses(users[1])).Returns(courses.ToList().Skip(2).Take(1));
            _MockCourseService.Setup(s => s.GetCourse(1)).Returns(courses[0]);
            _MockCourseService.Setup(s => s.GetCourse(2)).Returns(courses[1]);
            _MockCourseService.Setup(s => s.GetCourse(3)).Returns(courses[2]);

            ClearTables();
        }

        public void ClearTables()
        {
            //get back old current user
            SetCurrentUser(Users.Panza);

            var mockTopicTypes = DataPreparer.GetTopicTypes();

            _MockDisciplineDataContext.SetupGet(c => c.Disciplines).Returns(new MemoryTable<Discipline>("Id"));
            _MockDisciplineDataContext.SetupGet(c => c.Chapters).Returns(new MemoryTable<Chapter>("Id"));
            _MockDisciplineDataContext.SetupGet(c => c.Topics).Returns(new MemoryTable<Topic>("Id"));
            _MockDisciplineDataContext.SetupGet(c => c.TopicTypes).Returns(new MemoryTable<TopicType>(mockTopicTypes, "Id"));
            _MockDisciplineDataContext.SetupGet(c => c.SharedDisciplines).Returns(new MemoryTable<SharedDiscipline>("Id"));
            _MockCurriculumDataContext.SetupGet(c => c.Curriculums).Returns(new MemoryTable<Curriculum>("Id"));
            _MockCurriculumDataContext.SetupGet(c => c.CurriculumChapters).Returns(new MemoryTable<CurriculumChapter>("Id"));
            _MockCurriculumDataContext.SetupGet(c => c.CurriculumChapterTopics).Returns(new MemoryTable<CurriculumChapterTopic>("Id"));
        }

        public void SetCurrentUser(string userName)
        {
            _MockUserService.Setup(s => s.GetCurrentUser()).Returns(UserService.GetUsers().First(user => user.Username == userName));
        }
    }
}