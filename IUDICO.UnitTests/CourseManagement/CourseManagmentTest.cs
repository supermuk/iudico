using System;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.CourseManagement.Models.ManifestModels;
using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.UserManagement.Models.Storage;
using Moq;
using Moq.Protected;
using IUDICO.CourseManagement.Models;

namespace IUDICO.UnitTests.CourseManagement.NUnit
{
    public class CourseManagementTest
    {
        #region Protected members

        protected static CourseManagementTest _Instance;

        User[] _MockUserData = new[]
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
                                               Id = new Guid("33345200-abe8-4f60-90c8-0d43c5f6c0f6"), 
                                               Username = "user3", 
                                           },
                                       new User
                                           {
                                               Id = new Guid("44345200-abe8-4f60-90c8-0d43c5f6c0f6"), 
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
        #endregion

        #region Private properties
        private Mock<IUDICO.CourseManagement.Models.IDataContext> _MockDataContext
        {
            get;
            set;
        }

        private Mock<IUDICO.UserManagement.Models.IDataContext> _MockUserDataContext
        {
            get;
            set;
        }


        private Mock<ILmsService> _MockLmsService
        {
            get;
            set;
        }

        private Mock<DatabaseUserStorage> _MockUserStorage
        {
            get;
            set;
        }

        private Mock<MixedCourseStorage> _MockStorage
        {
            get;
            set;
        }

        private Mock<MixedCourseStorageProtectedMethodTestClass> _MockStorageProtectedMethodTestClass
        {
            get;
            set;
        }

        private Mock<HttpPostedFileBase> _HttpPostedFileBase
        {
            get;
            set;
        }

        #endregion 

        #region Public properties

        public IDataContext DataContext
        {
            get { return _MockDataContext.Object; }
        }

        public ILmsService LmsService
        {
            get { return _MockLmsService.Object; }
        }

        public ICourseStorage Storage
        {
            get { return _MockStorage.Object; }
        }

        public MixedCourseStorageProtectedMethodTestClass StorageForTestingProtectedMethods
        {
            get { return _MockStorageProtectedMethodTestClass.Object; }
        }

        public Mock<ITable> Users
        {
            get;
            protected set;
        }

        public HttpPostedFileBase HttpPostedFileBase
        {
            get { return _HttpPostedFileBase.Object; }
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
            _MockDataContext = new Mock<IDataContext>();
            _MockUserDataContext = new Mock<IUDICO.UserManagement.Models.IDataContext>();
            _MockLmsService = new Mock<ILmsService>();
            _MockStorage = new Mock<MixedCourseStorage>(_MockLmsService.Object);
            _MockUserStorage = new Mock<DatabaseUserStorage>(_MockLmsService.Object);
            _MockStorage.Protected().Setup<IDataContext>("GetDbContext").Returns(_MockDataContext.Object);
            _MockUserStorage.Protected().Setup<IUDICO.UserManagement.Models.IDataContext>("GetDbContext").Returns(_MockUserDataContext.Object);
            _HttpPostedFileBase = new Mock<HttpPostedFileBase>();
            _HttpPostedFileBase.SetupGet(i => i.FileName).Returns("file");

            _MockStorageProtectedMethodTestClass = new Mock<MixedCourseStorageProtectedMethodTestClass>();
            _MockStorageProtectedMethodTestClass.Protected().Setup<IDataContext>("GetDbContext").Returns(_MockDataContext.Object);

            Setup();
        }

        public static CourseManagementTest GetInstance()
        {
            return _Instance ?? (_Instance = new CourseManagementTest());
        }

        public void Setup()
        {

   //         _MockStorage.Setup(i => i.GetCoursePath(It.IsAny<int>())).Returns(@"d:\Tests\Data\Courses");
            _MockStorage.CallBase = true;
            _MockStorage.Protected().Setup<string>("GetCoursesPath").Returns(@"d:\Tests\Data\Courses");
            _MockUserDataContext.SetupGet(c => c.Users).Returns(new MemoryTable<User>(_MockUserData));

            Mock<IUserService> userService = new Mock<IUserService>();
            _MockLmsService.Setup(l => l.FindService<IUserService>()).Returns(userService.Object);

            userService.Setup(s => s.GetCurrentUser()).Returns(_MockUserData[0]);
            userService.Setup(s => s.GetUsers()).Returns(_MockUserData);


            ClearTables();
        }

        public void ClearTables()
        {

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
                                                 Locked = true,
                                                 Name = "#2",
                                                 Nodes = new EntitySet<Node>(),
                                                 Owner = "admin",
                                                 Updated = System.DateTime.Now

                                                 },
                                        new Course
                                             {
                                                 CourseUsers = new EntitySet<CourseUser>(),
                                                 Created = System.DateTime.Now,
                                                 Deleted = false,
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
            var mockCourseUserData = new[]
                                   {
                                       new CourseUser
                                           {
                                               Course = mockCourseData[0], 
                                               CourseRef = mockCourseData[0].Id, 
                                               UserRef = _MockUserData[1].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[0], 
                                               CourseRef = mockCourseData[0].Id, 
                                               UserRef = _MockUserData[2].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[0], 
                                               CourseRef = mockCourseData[0].Id, 
                                               UserRef = _MockUserData[3].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[1], 
                                               CourseRef = mockCourseData[1].Id, 
                                               UserRef = _MockUserData[4].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[2], 
                                               CourseRef = mockCourseData[2].Id, 
                                               UserRef = _MockUserData[5].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[3], 
                                               CourseRef = mockCourseData[3].Id, 
                                               UserRef = _MockUserData[2].Id
                                           },
                                       new CourseUser
                                           {
                                               Course = mockCourseData[4], 
                                               CourseRef = mockCourseData[4].Id, 
                                               UserRef = _MockUserData[2].Id
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
                                               Name = "Node0 for some cource",
                                               NodeResources = new EntitySet<NodeResource>(),
                                               
                                           },

                                        new Node
                                           {
                                               Course = mockCourseData[1],
                                               CourseId = mockCourseData[1].Id,
                                               Id = 1,
                                               IsFolder = true,
                                               Name = "Node1 for some cource",
                                               NodeResources = new EntitySet<NodeResource>(),
                                           },
                                        new Node
                                           {
                                               Course = mockCourseData[2],
                                               CourseId = mockCourseData[2].Id,
                                               Id = 2,
                                               IsFolder = true,
                                               Name = "Node2 for some cource",
                                               NodeResources = new EntitySet<NodeResource>(),
                                           }
                                   };

            var mockNodeResourceData = new[]
                                   {
                                       new NodeResource
                                           {
                                               Id = 0,
                                               Name = "NodeResorces0",
                                               Node = mockNodeData[0],
                                               NodeId = mockNodeData[0].Id,
                                               Path = "somePath0"
                                           },
                                        new NodeResource
                                           {
                                               Id = 1,
                                               Name = "NodeResorces1",
                                               Node = mockNodeData[0],
                                               NodeId = mockNodeData[0].Id,
                                               Path = "somePath1"
                                           },
                                        new NodeResource
                                           {
                                               Id = 3,
                                               Name = "NodeResorces1",
                                               Node = mockNodeData[1],
                                               NodeId = mockNodeData[1].Id,
                                               Path = "somePath2"
                                           },
                                        new NodeResource
                                        {
                                            Id = 2,
                                            Name = "NodeResorces1",
                                            Node = mockNodeData[1],
                                            NodeId = mockNodeData[1].Id,
                                            Path = "somePath3"
                                        }
                                   };
            mockNodeData[0].NodeResources.Add(mockNodeResourceData[0]);
            mockNodeData[0].NodeResources.Add(mockNodeResourceData[1]);
            mockNodeData[1].NodeResources.Add(mockNodeResourceData[2]);
            mockNodeData[1].NodeResources.Add(mockNodeResourceData[3]);


            _MockDataContext.SetupGet(c => c.Courses).Returns(new MemoryTable<Course>(mockCourseData));
            _MockDataContext.SetupGet(c => c.Nodes).Returns(new MemoryTable<Node>(mockNodeData));
            _MockDataContext.SetupGet(c => c.NodeResources).Returns(new MemoryTable<NodeResource>(mockNodeResourceData));
            _MockDataContext.SetupGet(c => c.CourseUsers).Returns(new MemoryTable<CourseUser>(mockCourseUserData));
        }

    }

    public class MixedCourseStorageProtectedMethodTestClass: MixedCourseStorage
    {
        public MixedCourseStorageProtectedMethodTestClass(ILmsService iLmsService):base(iLmsService){}

        public Item AddSubItemsTest(Item parentItem, Node parentNode, int courseId, ManifestManager helper, ref Manifest manifest)
        {
            return AddSubItems(parentItem,parentNode,courseId, helper, ref manifest);
        }
    }
}

