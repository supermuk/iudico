using System;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.UserManagement.Models.Storage;
using Moq;
using Moq.Protected;

namespace IUDICO.UnitTests.CurriculumManagement
{
    public class CurriculumManagementTests
    {
        #region Protected members

        protected static CurriculumManagementTests _Instance;

        #endregion

        #region Public properties

        private Mock<IDataContext> _MockDataContext { get; set; }

        private Mock<IUDICO.UserManagement.Models.IDataContext> _MockUserDataContext { get; set; }

        private Mock<IUDICO.CourseManagement.Models.IDataContext> _MockCourseDataContext { get; set; }

        private Mock<ILmsService> _MockLmsService { get; set; }

        private Mock<DatabaseCurriculumStorage> _MockStorage { get; set; }

        private Mock<DatabaseUserStorage> _MockUserStorage { get; set; }

        private Mock<MixedCourseStorage> _MockCourseStorage { get; set; }

        public IDataContext DataContext
        {
            get { return _MockDataContext.Object; }
        }

        public IUDICO.UserManagement.Models.IDataContext UserDataContext
        {
            get { return _MockUserDataContext.Object; }
        }

        public IUDICO.CourseManagement.Models.IDataContext CourseDataContext
        {
            get { return _MockCourseDataContext.Object; }
        }

        public ILmsService LmsService
        {
            get { return _MockLmsService.Object; }
        }

        public ICurriculumStorage Storage
        {
            get { return _MockStorage.Object; }
        }

        public IUserStorage UserStorage
        {
            get { return _MockUserStorage.Object; }
        }

        public ICourseStorage CourseStorage
        {
            get { return _MockCourseStorage.Object; }
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

        //public Mock<ITable> Disciplines
        //{
        //    get;
        //    protected set;
        //}

        #endregion

        private CurriculumManagementTests()
        {
            _MockDataContext = new Mock<IDataContext>();
            _MockUserDataContext = new Mock<IUDICO.UserManagement.Models.IDataContext>();
            _MockCourseDataContext = new Mock<IUDICO.CourseManagement.Models.IDataContext>();
            _MockLmsService = new Mock<ILmsService>();
            _MockStorage = new Mock<DatabaseCurriculumStorage>(_MockLmsService.Object);
            _MockUserStorage = new Mock<DatabaseUserStorage>(_MockLmsService.Object);
            _MockCourseStorage = new Mock<MixedCourseStorage>(_MockLmsService.Object);
            _MockStorage.Protected().Setup<IDataContext>("GetDbContext").Returns(_MockDataContext.Object);
            _MockUserStorage.Protected().Setup<IUDICO.UserManagement.Models.IDataContext>("GetDbContext").Returns(
                _MockUserDataContext.Object);
            _MockCourseStorage.Protected().Setup<IUDICO.CourseManagement.Models.IDataContext>("GetDbContext").Returns(
                _MockCourseDataContext.Object);

            //Users = new Mock<ITable>();
            //Groups = new Mock<ITable>();
            //GroupUsers = new Mock<ITable>();
            //Disciplines = new Mock<ITable>();

            Setup();
        }

        public static CurriculumManagementTests GetInstance()
        {
            return _Instance ?? (_Instance = new CurriculumManagementTests());
        }

        private void Setup()
        {
            var mockCourseData = new[]
                                     {
                                         new Course {Id = 1, Name = "Course", Owner = "panza"}
                                     };

            var mockUserData = new[]
                                   {
                                       new User
                                           {
                                               Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                                               Username = "panza",
                                               Email = "ipetrovych@gmail.com",
                                               Password = "",
                                           },
                                   };

            var mockGroupData = new[]
                                    {
                                        new Group {Id = 1, Name = "PMI51", Deleted = false},
                                        new Group {Id = 2, Name = "PMI31", Deleted = false}
                                    };

            var mockGroupUserData = new[]
                                        {
                                            new GroupUser {GroupRef = 1, UserRef = mockUserData[0].Id}
                                        };

            _MockCourseDataContext.SetupGet(c => c.Courses).Returns(new MemoryTable<Course>(mockCourseData));
            _MockUserDataContext.SetupGet(c => c.Users).Returns(new MemoryTable<User>(mockUserData));
            _MockUserDataContext.SetupGet(c => c.Groups).Returns(new MemoryTable<Group>(mockGroupData));
            _MockUserDataContext.SetupGet(c => c.GroupUsers).Returns(new MemoryTable<GroupUser>(mockGroupUserData));

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
            userService.Setup(s => s.GetGroupsByUser(It.IsAny<User>())).Returns(new[] {mockGroupData[0]});

            var mockUserScoresData = new[]
                                          {
                                              new UserTopicScore {Score = 0, TopicId = 1, UserId = mockUserData[0].Id}
                                          };

            _MockDataContext.SetupGet(c => c.UserTopicScores).Returns(new MemoryTable<UserTopicScore>(mockUserScoresData));

            ClearTables();
        }

        public void ClearTables()
        {
            var mockTopicTypes = new[]
                                     {
                                         new TopicType {Id = 1, Name = "Test"},
                                         new TopicType {Id = 2, Name = "Theory"},
                                         new TopicType {Id = 3, Name = "TestWithoutCourse"}
                                     };

            

            _MockDataContext.SetupGet(c => c.Disciplines).Returns(new MemoryTable<Discipline>("Id"));
            _MockDataContext.SetupGet(c => c.Chapters).Returns(new MemoryTable<Chapter>("Id"));
            _MockDataContext.SetupGet(c => c.Topics).Returns(new MemoryTable<Topic>("Id"));
            _MockDataContext.SetupGet(c => c.Curriculums).Returns(new MemoryTable<Curriculum>("Id"));
            _MockDataContext.SetupGet(c => c.TopicTypes).Returns(new MemoryTable<TopicType>(mockTopicTypes, "Id"));
        }
    }
}