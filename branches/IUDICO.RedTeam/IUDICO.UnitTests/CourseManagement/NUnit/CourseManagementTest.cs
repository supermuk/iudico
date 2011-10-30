using System;
using System.Data.Linq;
using System.Linq;
using System.Xml.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Services;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.UserManagement.Models.Storage;
using Moq;
using Moq.Protected;

namespace IUDICO.UnitTests.CourseManagement.NUnit
{
    public class CourseManagementTest
    {
        #region Protected members

        protected static CourseManagementTest _Instance;

        #endregion

        #region Public properties

        public Mock<IDataContext> MockDataContext
        {
            get;
            protected set;
        }

        public Mock<ILmsService> MockLmsService
        {
            get;
            protected set;
        }

        public Mock<DatabaseUserStorage> MockStorage
        {
            get;
            protected set;
        }

        public IDataContext DataContext
        {
            get { return MockDataContext.Object; }
        }

        public ILmsService LmsService
        {
            get { return MockLmsService.Object; }
        }

        public IUserStorage Storage
        {
            get { return MockStorage.Object; }
        }

        public Mock<ITable> Users
        {
            get;
            protected set;
        }
       
        public Mock<ITable> Courses
        {
            get;
            protected set;
        }

        public Mock<ITable> CourseUsers
        {
            get;
            protected set;
        }

        public Mock<ITable> Nodes
        {
            get;
            protected set;
        }

        public Mock<ITable> NodeResources
        {
            get;
            protected set;
        }

        #endregion

        private CourseManagementTest()
        {
            MockDataContext = new Mock<IDataContext>();
            MockLmsService = new Mock<ILmsService>();
            MockStorage = new Mock<DatabaseUserStorage>(MockLmsService.Object);

            //Storage = new DatabaseUserStorage(MockLmsService.Object);

            Users = new Mock<ITable>();
            Courses = new Mock<ITable>();
            CourseUsers = new Mock<ITable>();
            Nodes = new Mock<ITable>();
            NodeResources = new Mock<ITable>();

            Setup();
            SetupTables();
        }

        public static CourseManagementTest GetInstance()
        {
            return _Instance ?? (_Instance = new CourseManagementTest());
        }

        public void Setup()
        {
            MockLmsService.Setup(l => l.GetIDataContext()).Returns(MockDataContext.Object);
            MockStorage.Protected().Setup<IDataContext>("GetDbContext").Returns(MockDataContext.Object);

        }

        public void SetupTables()
        {
            var mockUserData = new[]
                                   {
                                       new User
                                           {
                                               Id = Guid.NewGuid(), 
                                               Username = "lex", 
                                               Password = Storage.EncryptPassword("lex"),
                                           },
                                       new User
                                           {
                                               Id = Guid.NewGuid(), 
                                               Username = "user1", 
                                               Password = Storage.EncryptPassword("user1"),
                                           },
                                       new User
                                           {
                                               Id = Guid.NewGuid(), 
                                               Username = "user2", 
                                               Password = Storage.EncryptPassword("user2"),
                                           },
                                   };

            var mockCourseData = new[]
                                     {
                                         new Course
                                             {
                                                 Id = 1,
                                                 Name = "Some course",
                                                 Owner = "lex",
                                                 Created = System.DateTime.Now,
                                                 Updated = System.DateTime.Now,
                                                 CourseUsers = new EntitySet<CourseUser>(),
                                                 Deleted = false,
                                                 Locked = null,
                                                 Nodes = new EntitySet<Node>(),
                                             },
                                         new Course
                                             {
                                                 CourseUsers = new EntitySet<CourseUser>(),
                                                 Created = System.DateTime.Now,
                                                 Deleted = false,
                                                 Id = 2,
                                                 Locked = null,
                                                 Name = "#2",
                                                 Nodes = new EntitySet<Node>(),
                                                 Owner = "lex",
                                                 Updated = System.DateTime.Now

                                                 },
                                        new Course
                                             {
                                                 CourseUsers = new EntitySet<CourseUser>(),
                                                 Created = System.DateTime.Now,
                                                 Deleted = true,
                                                 Id = 7,
                                                 Locked = null,
                                                 Name = "#7",
                                                 Nodes = new EntitySet<Node>(),
                                                 Owner = "lex",
                                                 Updated = System.DateTime.Now

                                                 },
                                         new Course
                                                     {
                                                         CourseUsers = new EntitySet<CourseUser>(),
                                                         Created = System.DateTime.Now,
                                                         Deleted = false,
                                                         Id = 3,
                                                         Locked = null,
                                                         Name = "#3",
                                                         Nodes = new EntitySet<Node>(),
                                                         Owner = "user1",
                                                         Updated = System.DateTime.Now
                                                     },
                                         new Course
                                                         {
                                                             CourseUsers = new EntitySet<CourseUser>(),
                                                             Created = System.DateTime.Now,
                                                             Deleted = true,
                                                             Id = 4,
                                                             Locked = null,
                                                             Name = "#4",
                                                             Nodes =  new EntitySet<Node>(),
                                                             Owner = "user2",
                                                             Updated = System.DateTime.Now
                                                         }
                                     };
            var mockCourseUserData = new[]
                                   {
                                       new CourseUser
                                           {
                                               Course = mockCourseData[0], 
                                               CourseRef = mockCourseData[0].Id, 
                                               UserRef = mockUserData[0].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[1], 
                                               CourseRef = mockCourseData[1].Id, 
                                               UserRef = mockUserData[0].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[2], 
                                               CourseRef = mockCourseData[2].Id, 
                                               UserRef = mockUserData[0].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[3], 
                                               CourseRef = mockCourseData[3].Id, 
                                               UserRef = mockUserData[1].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[4], 
                                               CourseRef = mockCourseData[4].Id, 
                                               UserRef = mockUserData[2].Id
                                           }
                                   };
            var mockNodeData = new[]
                                   {
                                       new Node
                                           {
                                               Course = mockCourseData[0],
                                               CourseId = mockCourseData[0].Id,
                                               Id = 0,
                                               IsFolder = false,
                                               Name = "Node for some cource"
                                           }
                                   };
            var mockNodeResourceData = new[]
                                   {
                                       new NodeResource
                                           {
                                               Id = 0,
                                               Name = "something))"
                                           }
                                   };
            
            var mockUsers = new MockableTable<User>(Users.Object, mockUserData.AsQueryable());
            var mockCourses = new MockableTable<Course>(Courses.Object, mockCourseData.AsQueryable());
            var mockCourseUsers = new MockableTable<CourseUser>(CourseUsers.Object, mockCourseUserData.AsQueryable());
            var mockNodes = new MockableTable<Node>(Nodes.Object, mockNodeData.AsQueryable());
            var mockNodeResources = new MockableTable<NodeResource>(NodeResources.Object, mockNodeResourceData.AsQueryable());

            MockDataContext.SetupGet(c => c.Users).Returns(mockUsers);
            MockDataContext.SetupGet(c => c.Courses).Returns(mockCourses);
            MockDataContext.SetupGet(c => c.CourseUsers).Returns(mockCourseUsers);
            MockDataContext.SetupGet(c => c.Nodes).Returns(mockNodes);
            MockDataContext.SetupGet(c => c.NodeResources).Returns(mockNodeResources);
        }
    }
}

