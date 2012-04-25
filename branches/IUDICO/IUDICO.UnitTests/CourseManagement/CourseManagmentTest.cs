namespace IUDICO.UnitTests.CourseManagement
{
    using System;
    using System.Configuration;
    using System.Data.Linq;
    using System.IO;
    using System.Web;

    using IUDICO.Common.Models;
    using IUDICO.Common.Models.Services;
    using IUDICO.Common.Models.Shared;
    using IUDICO.CourseManagement.Models;
    using IUDICO.CourseManagement.Models.ManifestModels;
    using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
    using IUDICO.CourseManagement.Models.Storage;
    using IUDICO.UserManagement.Models.Storage;

    using Moq;
    using Moq.Protected;

    public class CourseManagementTest
    {
        #region Protected members

        protected static CourseManagementTest instance;

        private readonly User[] mockUserData = new[]
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

        private Mock<IDataContext> _MockDataContext { get; set; }

        private Mock<IUDICO.UserManagement.Models.IDataContext> _MockUserDataContext { get; set; }


        private Mock<ILmsService> _MockLmsService { get; set; }

        private Mock<DatabaseUserStorage> _MockUserStorage { get; set; }

        private Mock<MixedCourseStorage> _MockStorage { get; set; }

        private Mock<MixedCourseStorageProtectedMethodTestClass> _MockStorageProtectedMethodTestClass { get; set; }

        private Mock<HttpPostedFileBase> _HttpPostedFileBase { get; set; }

        #endregion

        #region Public properties

        public IDataContext DataContext
        {
            get { return this._MockDataContext.Object; }
        }

        public ILmsService LmsService
        {
            get { return this._MockLmsService.Object; }
        }

        public ICourseStorage Storage
        {
            get { return this._MockStorage.Object; }
        }

        public MixedCourseStorageProtectedMethodTestClass StorageForTestingProtectedMethods
        {
            get { return this._MockStorageProtectedMethodTestClass.Object; }
        }

        public string _CourseStoragePath { get; protected set; }

        public Mock<ITable> Users { get; protected set; }

        public HttpPostedFileBase HttpPostedFileBase
        {
            get { return this._HttpPostedFileBase.Object; }
        }

        public Mock<ITable> Courses { get; protected set; }

        public Mock<ITable> CourseUsers { get; protected set; }

        public Mock<ITable> Nodes { get; protected set; }

        public Mock<ITable> NodeResources { get; protected set; }

        #endregion

        private CourseManagementTest()
        {
            this._MockDataContext = new Mock<IDataContext>();
            this._MockUserDataContext = new Mock<IUDICO.UserManagement.Models.IDataContext>();
            this._MockLmsService = new Mock<ILmsService>();
            this._MockStorage = new Mock<MixedCourseStorage>(this._MockLmsService.Object);
            this._MockUserStorage = new Mock<DatabaseUserStorage>(this._MockLmsService.Object);
            this._MockStorage.Protected().Setup<IDataContext>("GetDbContext").Returns(this._MockDataContext.Object);
            this._MockUserStorage.Protected().Setup<IUDICO.UserManagement.Models.IDataContext>("GetDbContext").Returns(
                this._MockUserDataContext.Object);
            this._HttpPostedFileBase = new Mock<HttpPostedFileBase>();
            this._HttpPostedFileBase.SetupGet(i => i.FileName).Returns("file");
            this._CourseStoragePath = Path.Combine(
                ConfigurationManager.AppSettings.Get("RootTestFolder"), @"CourseManagement\Data");

            this._MockStorageProtectedMethodTestClass = new Mock<MixedCourseStorageProtectedMethodTestClass>();
            this._MockStorageProtectedMethodTestClass.Protected().Setup<IDataContext>("GetDbContext").Returns(
                this._MockDataContext.Object);

            this.Setup();
        }

        public static CourseManagementTest GetInstance()
        {
            return instance ?? (instance = new CourseManagementTest());
        }

        public void Setup()
        {
            this._MockStorage.CallBase = true;
            this._MockStorage.Protected().Setup<string>("GetCoursesPath").Returns(this._CourseStoragePath);
            this._MockStorage.Setup(c => c.GetTemplatesPath()).Returns(
                Path.Combine(this._CourseStoragePath, "Templates"));
            this._MockUserDataContext.SetupGet(c => c.Users).Returns(new MemoryTable<User>(this.mockUserData));

            Mock<IUserService> userService = new Mock<IUserService>();
            this._MockLmsService.Setup(l => l.FindService<IUserService>()).Returns(userService.Object);

            userService.Setup(s => s.GetCurrentUser()).Returns(this.mockUserData[0]);
            userService.Setup(s => s.GetUsers()).Returns(this.mockUserData);


            this.ClearTables();
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
                                                 Created = DateTime.Now,
                                                 Updated = DateTime.Now,
                                                 CourseUsers = new EntitySet<CourseUser>(),
                                                 Deleted = false,
                                                 Locked = null,
                                                 Nodes = new EntitySet<Node>(),
                                             },
                                         new Course
                                             {
                                                 CourseUsers = new EntitySet<CourseUser>(),
                                                 Created = DateTime.Now,
                                                 Deleted = false,
                                                 Id = 2,
                                                 Locked = true,
                                                 Name = "#2",
                                                 Nodes = new EntitySet<Node>(),
                                                 Owner = "admin",
                                                 Updated = DateTime.Now
                                             },
                                         new Course
                                             {
                                                 CourseUsers = new EntitySet<CourseUser>(),
                                                 Created = DateTime.Now,
                                                 Deleted = false,
                                                 Id = 7,
                                                 Locked = null,
                                                 Name = "#7",
                                                 Nodes = new EntitySet<Node>(),
                                                 Owner = "lex",
                                                 Updated = DateTime.Now
                                             },
                                         new Course
                                             {
                                                 CourseUsers = new EntitySet<CourseUser>(),
                                                 Created = DateTime.Now,
                                                 Deleted = false,
                                                 Id = 3,
                                                 Locked = null,
                                                 Name = "#3",
                                                 Nodes = new EntitySet<Node>(),
                                                 Owner = "user1",
                                                 Updated = DateTime.Now
                                             },
                                         new Course
                                             {
                                                 CourseUsers = new EntitySet<CourseUser>(),
                                                 Created = DateTime.Now,
                                                 Deleted = true,
                                                 Id = 4,
                                                 Locked = null,
                                                 Name = "#4",
                                                 Nodes = new EntitySet<Node>(),
                                                 Owner = "lex",
                                                 Updated = DateTime.Now
                                             }
                                     };
            var mockCourseUserData = new[]
                                         {
                                             new CourseUser
                                                 {
                                                     Course = mockCourseData[0],
                                                     CourseRef = mockCourseData[0].Id,
                                                     UserRef = this.mockUserData[1].Id
                                                 },
                                             new CourseUser
                                                 {
                                                     Course = mockCourseData[0],
                                                     CourseRef = mockCourseData[0].Id,
                                                     UserRef = this.mockUserData[2].Id
                                                 },
                                             new CourseUser
                                                 {
                                                     Course = mockCourseData[0],
                                                     CourseRef = mockCourseData[0].Id,
                                                     UserRef = this.mockUserData[3].Id
                                                 },
                                             new CourseUser
                                                 {
                                                     Course = mockCourseData[1],
                                                     CourseRef = mockCourseData[1].Id,
                                                     UserRef = this.mockUserData[4].Id
                                                 },
                                             new CourseUser
                                                 {
                                                     Course = mockCourseData[2],
                                                     CourseRef = mockCourseData[2].Id,
                                                     UserRef = this.mockUserData[5].Id
                                                 },
                                             new CourseUser
                                                 {
                                                     Course = mockCourseData[3],
                                                     CourseRef = mockCourseData[3].Id,
                                                     UserRef = this.mockUserData[2].Id
                                                 },
                                             new CourseUser
                                                 {
                                                     Course = mockCourseData[4],
                                                     CourseRef = mockCourseData[4].Id,
                                                     UserRef = this.mockUserData[2].Id
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


            this._MockDataContext.SetupGet(c => c.Courses).Returns(new MemoryTable<Course>(mockCourseData));
            this._MockDataContext.SetupGet(c => c.Nodes).Returns(new MemoryTable<Node>(mockNodeData));
            this._MockDataContext.SetupGet(c => c.NodeResources).Returns(new MemoryTable<NodeResource>(mockNodeResourceData));
            this._MockDataContext.SetupGet(c => c.CourseUsers).Returns(new MemoryTable<CourseUser>(mockCourseUserData));
        }
    }

    public class MixedCourseStorageProtectedMethodTestClass : MixedCourseStorage
    {
        public MixedCourseStorageProtectedMethodTestClass(ILmsService ilmsService) : base(ilmsService)
        {
        }

        public Item AddSubItemsTest(Item parentItem, Node parentNode, int courseId, ManifestManager helper, ref Manifest manifest)
        {
            return this.AddSubItems(parentItem, parentNode, courseId, helper, ref manifest);
        }
    }
}