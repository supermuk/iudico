using System;
using System.Data.Linq;
using System.Linq;
using System.Xml.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Services;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.UserManagement.Models;
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

        private Mock<IDataContext> MockDataContext
        {
            get;
            set;
        }

        private Mock<ILmsService> MockLmsService
        {
            get;
            set;
        }

        private Mock<MixedCourseStorage> MockStorage
        {
            get;
            set;
        }

        private Mock<UserService> MockUserServise { get; set; }

        public IDataContext DataContext
        {
            get { return MockDataContext.Object; }
        }

        public ILmsService LmsService
        {
            get { return MockLmsService.Object; }
        }

        public ICourseStorage Storage
        {
            get { return MockStorage.Object; }
        }

        //public Mock<ITable> Users
        //{
        //    get;
        //    set;
        //}

        //public Mock<ITable> Courses
        //{
        //    get;
        //    set;
        //}

        //public Mock<ITable> CourseUsers
        //{
        //    get;
        //    set;
        //}

        //public Mock<ITable> Nodes
        //{
        //    get;
        //    set;
        //}

        //public Mock<ITable> NodeResources
        //{
        //    get;
        //    set;
        //}

        #endregion

        private CourseManagementTest()
        {
            MockDataContext = new Mock<IDataContext>();
            MockLmsService = new Mock<ILmsService>();
            MockStorage = new Mock<MixedCourseStorage>(MockLmsService.Object);
            MockUserServise = new Mock<UserService>();
            //Users = new Mock<ITable>();
            //Courses = new Mock<ITable>();
            //CourseUsers = new Mock<ITable>();
            //Nodes = new Mock<ITable>();
            //NodeResources = new Mock<ITable>();

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
            MockLmsService.Setup(l => l.FindService<IUserService>().GetCurrentUser()).Returns(new User { Username = "lex" });
 //           MockUserServise.Setup(l => l.GetCurrentUser()).Returns(new User {Name = "lex"});
        }

        public void SetupTables()
        {
            var mockUserData = new[]
                                   {
                                       new User
                                           {
                                               Id = Guid.NewGuid(), 
                                               Username = "lex", 
                                           },
                                       new User
                                           {
                                               Id = new Guid("11345200-abe8-4f60-90c8-0d43c5f6c0f6"), 
                                               Username = "user1", 
                                           },
                                       new User
                                           {
                                               Id = new Guid("22345200-abe8-4f60-90c8-0d43c5f6c0f6"), 
                                               Username = "user2", 
                                           },
                                       new User
                                           {
                                               Id = Guid.NewGuid(), 
                                               Username = "user3", 
                                           },
                                       new User
                                           {
                                               Id = Guid.NewGuid(), 
                                               Username = "user4", 
                                           },
                                           new User
                                           {
                                               Id = new Guid("55345200-abe8-4f60-90c8-0d43c5f6c0f6"), 
                                               Username = "user5", 
                                           },
                                       new User
                                           {
                                               Id = new Guid("66345200-abe8-4f60-90c8-0d43c5f6c0f6"), 
                                               Username = "user6", 
                                           },
                                   };

            MockDataContext.SetupGet(s => s.Users).Returns(new MemoryTable<User>(mockUserData));

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
                                                 Owner = "admin",
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
                                                             Owner = "lex",
                                                             Updated = System.DateTime.Now
                                                         }
                                     };

            MockDataContext.SetupGet(s => s.Courses).Returns(new MemoryTable<Course>(mockCourseData));

            var mockCourseUserData = new[]
                                   {
                                       new CourseUser
                                           {
                                               Course = mockCourseData[0], 
                                               CourseRef = mockCourseData[0].Id, 
                                               UserRef = mockUserData[1].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[0], 
                                               CourseRef = mockCourseData[0].Id, 
                                               UserRef = mockUserData[2].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[0], 
                                               CourseRef = mockCourseData[0].Id, 
                                               UserRef = mockUserData[3].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[1], 
                                               CourseRef = mockCourseData[1].Id, 
                                               UserRef = mockUserData[4].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[2], 
                                               CourseRef = mockCourseData[2].Id, 
                                               UserRef = mockUserData[5].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[3], 
                                               CourseRef = mockCourseData[3].Id, 
                                               UserRef = mockUserData[2].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[4], 
                                               CourseRef = mockCourseData[4].Id, 
                                               UserRef = mockUserData[2].Id
                                           }
                                   };
            MockDataContext.SetupGet(s => s.CourseUsers).Returns(new MemoryTable<CourseUser>(mockCourseUserData));

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
            MockDataContext.SetupGet(s => s.Nodes).Returns(new MemoryTable<Node>(mockNodeData));

            var mockNodeResourceData = new[]
                                   {
                                       new NodeResource
                                           {
                                               Id = 0,
                                               Name = "something))"
                                           }
                                   };
            MockDataContext.SetupGet(s => s.NodeResources).Returns(new MemoryTable<NodeResource>(mockNodeResourceData));


            //var mockUsers = new MockableTable<User>(Users.Object, mockUserData.AsQueryable());
            //var mockCourses = new MockableTable<Course>(Courses.Object, mockCourseData.AsQueryable());
            //var mockCourseUsers = new MockableTable<CourseUser>(CourseUsers.Object, mockCourseUserData.AsQueryable());
            //var mockNodes = new MockableTable<Node>(Nodes.Object, mockNodeData.AsQueryable());
            //var mockNodeResources = new MockableTable<NodeResource>(NodeResources.Object, mockNodeResourceData.AsQueryable());

            //MockDataContext.SetupGet(c => c.Users).Returns(mockUsers);
            //MockDataContext.SetupGet(c => c.Courses).Returns(mockCourses);
            //MockDataContext.SetupGet(c => c.CourseUsers).Returns(mockCourseUsers);
            //MockDataContext.SetupGet(c => c.Nodes).Returns(mockNodes);
            //MockDataContext.SetupGet(c => c.NodeResources).Returns(mockNodeResources);
        }
    }
}

