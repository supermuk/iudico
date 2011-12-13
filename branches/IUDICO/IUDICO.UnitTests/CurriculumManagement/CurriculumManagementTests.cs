using System;
using System.Data.Linq;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Services;
using IUDICO.UserManagement.Models.Storage;
using Moq;
using Moq.Protected;
using IUDICO.CurriculumManagement.Models;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.Common.Models.Shared;

namespace IUDICO.UnitTests.CurriculumManagement
{
    public class CurriculumManagementTests
    {
        #region Protected members

        protected static CurriculumManagementTests _Instance;

        #endregion

        #region Public properties

        private Mock<IDataContext> _MockDataContext
        {
            get;
            set;
        }

        private Mock<ILmsService> _MockLmsService
        {
            get;
            set;
        }

        private Mock<MixedCurriculumStorage> _MockStorage
        {
            get;
            set;
        }

        public IDataContext DataContext
        {
            get { return _MockDataContext.Object; }
        }

        public ILmsService LmsService
        {
            get { return _MockLmsService.Object; }
        }

        public ICurriculumStorage Storage
        {
            get { return _MockStorage.Object; }
        }

        //public Mock<ITable> Users
        //{
        //    get;
        //    protected set;
        //}

        //public Mock<ITable> Groups
        //{
        //    get;
        //    protected set;
        //}

        //public Mock<ITable> GroupUsers
        //{
        //    get;
        //    protected set;
        //}

        //public Mock<ITable> Curriculums
        //{
        //    get;
        //    protected set;
        //}

        #endregion

        private CurriculumManagementTests()
        {
            _MockDataContext = new Mock<IDataContext>();
            _MockLmsService = new Mock<ILmsService>();
            _MockStorage = new Mock<MixedCurriculumStorage>(_MockLmsService.Object);
            _MockStorage.Protected().Setup<IDataContext>("GetDbContext").Returns(_MockDataContext.Object);

            //Users = new Mock<ITable>();
            //Groups = new Mock<ITable>();
            //GroupUsers = new Mock<ITable>();
            //Curriculums = new Mock<ITable>();

            Setup();
        }

        public static CurriculumManagementTests GetInstance()
        {
            return _Instance ?? (_Instance = new CurriculumManagementTests());
        }

        private void Setup()
        {
            var mockCourseData = new[]{
                new Course{Id=1, Name="Course", Owner="panza" }
            };

            var mockUserData = new[] {
                new User {Id = Guid.NewGuid(), Username = "panza", Email = "ipetrovych@gmail.com", Password = "", },
            };

            var mockGroupData = new[] {
                new Group {Id = 1, Name = "PMI51", Deleted = false},new Group {Id = 2, Name = "PMI31", Deleted = false}
            };

            var mockGroupUserData = new[] {
                new GroupUser {GroupRef = 1, UserRef = mockUserData[0].Id}
            };

            //_MockDataContext.SetupGet(c => c.Courses).Returns(new MemoryTable<Course>(mockCourseData));
            //_MockDataContext.SetupGet(c => c.Users).Returns(new MemoryTable<User>(mockUserData));
            //_MockDataContext.SetupGet(c => c.Groups).Returns(new MemoryTable<Group>(mockGroupData));
            //_MockDataContext.SetupGet(c => c.GroupUsers).Returns(new MemoryTable<GroupUser>(mockGroupUserData));

            //_MockLmsService.Setup(l => l.GetDataContext()).Returns(_MockDataContext.Object);
            Mock<IUserService> userService = new Mock<IUserService>();
            Mock<ICourseService> courseService = new Mock<ICourseService>();
            _MockLmsService.Setup(l => l.FindService<IUserService>()).Returns(userService.Object);
            _MockLmsService.Setup(l => l.FindService<ICourseService>()).Returns(courseService.Object);

            //тут можна засетапити ті зовнішні методи які викликаються в курікулум стораджі
            userService.Setup(s => s.GetCurrentUser()).Returns(mockUserData[0]);
            userService.Setup(s => s.GetGroups()).Returns(mockGroupData);
            userService.Setup(s => s.GetGroup(1)).Returns(mockGroupData[0]);
            userService.Setup(s => s.GetGroup(2)).Returns(mockGroupData[1]);
            userService.Setup(s => s.GetUsers()).Returns(mockUserData);
            userService.Setup(s => s.GetGroupsByUser(It.IsAny<User>())).Returns(new[] { mockGroupData[0] });

            ClearTables();
        }

        public void ClearTables()
        {
            var mockThemeTypes = new[] {
                new ThemeType {Id=1, Name="Test"},
                new ThemeType {Id=2, Name="Theory"},
                new ThemeType {Id=3, Name="TestWithoutCourse"}
            };

            _MockDataContext.SetupGet(c => c.Curriculums).Returns(new MemoryTable<Curriculum>("Id"));
            _MockDataContext.SetupGet(c => c.Stages).Returns(new MemoryTable<Stage>("Id"));
            _MockDataContext.SetupGet(c => c.Themes).Returns(new MemoryTable<Theme>("Id"));
            _MockDataContext.SetupGet(c => c.ThemeAssignments).Returns(new MemoryTable<ThemeAssignment>("Id"));
            _MockDataContext.SetupGet(c => c.CurriculumAssignments).Returns(new MemoryTable<CurriculumAssignment>("Id"));
            _MockDataContext.SetupGet(c => c.Timelines).Returns(new MemoryTable<Timeline>("Id"));
            _MockDataContext.SetupGet(c => c.ThemeTypes).Returns(new MemoryTable<ThemeType>(mockThemeTypes, "Id"));
        }
    }
}
