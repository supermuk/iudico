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
using IUDICO.Common.Models.Caching.Provider;

namespace IUDICO.UnitTests.DataGenerator.Fakes
{
	public class FakeCourseStorage
	{
        #region Protected members

        protected static FakeCourseStorage instance;
		  protected int counter = 0;
        private readonly User[] mockUserData = new[]
            {
                new User { Id = Guid.NewGuid(), Username = "lex", }, 
                new User { Id = new Guid("11345200-abe8-4f60-90c8-0d43c5f6c0f6"), Username = "user1", }, 
                new User { Id = new Guid("22345200-abe8-4f60-90c8-0d43c5f6c0f6"), Username = "user2", }, 
                new User { Id = new Guid("33345200-abe8-4f60-90c8-0d43c5f6c0f6"), Username = "user3", }, 
                new User { Id = new Guid("44345200-abe8-4f60-90c8-0d43c5f6c0f6"), Username = "user4", }, 
                new User { Id = new Guid("55345200-abe8-4f60-90c8-0d43c5f6c0f6"), Username = "user5", }, 
                new User { Id = new Guid("66345200-abe8-4f60-90c8-0d43c5f6c0f6"), Username = "user6", }, 
            };

        #endregion

        #region Private properties

        private Mock<IDataContext> _MockDataContext { get; set; }

        private Mock<IUDICO.UserManagement.Models.IDataContext> _MockUserDataContext { get; set; }

        private Mock<ILmsService> _MockLmsService { get; set; }

        private Mock<DatabaseUserStorage> _MockUserStorage { get; set; }

        private Mock<CachedCourseStorage> _MockStorage { get; set; }

        private Mock<MixedCourseStorage> _MockDatabaseStorage { get; set; }

//        private Mock<MixedCourseStorageProtectedMethodTestClass> _MockStorageProtectedMethodTestClass { get; set; }

        private Mock<HttpPostedFileBase> _HttpPostedFileBase { get; set; }

        #endregion

        #region Public properties

        public IDataContext DataContext
        {
            get
            {
                return this._MockDataContext.Object;
            }
        }

        public ILmsService LmsService
        {
            get
            {
                return this._MockLmsService.Object;
            }
        }

        public ICourseStorage Storage
        {
            get
            {
                return this._MockStorage.Object;
            }
        }

		  //public MixedCourseStorageProtectedMethodTestClass StorageForTestingProtectedMethods
		  //{
		  //    get
		  //    {
		  //        return this._MockStorageProtectedMethodTestClass.Object;
		  //    }
		  //}

        public string _CourseStoragePath { get; protected set; }

        public Mock<ITable> Users { get; protected set; }

        public HttpPostedFileBase HttpPostedFileBase
        {
            get
            {
                return this._HttpPostedFileBase.Object;
            }
        }

        public Mock<ITable> Courses { get; protected set; }

        public Mock<ITable> CourseUsers { get; protected set; }

        public Mock<ITable> Nodes { get; protected set; }

        public Mock<ITable> NodeResources { get; protected set; }

        #endregion

        private FakeCourseStorage()
        {
            this._MockDataContext = new Mock<IDataContext>();
            this._MockUserDataContext = new Mock<IUDICO.UserManagement.Models.IDataContext>();
            this._MockLmsService = new Mock<ILmsService>();

            var mockCacheProvider = new Mock<HttpCache>();
            this._MockDatabaseStorage = new Mock<MixedCourseStorage>(this._MockLmsService.Object);
            this._MockStorage = new Mock<CachedCourseStorage>(this._MockDatabaseStorage.Object, mockCacheProvider.Object);

            this._MockUserStorage = new Mock<DatabaseUserStorage>(this._MockLmsService.Object);
            this._MockDatabaseStorage.Protected().Setup<IDataContext>("GetDbContext").Returns(this._MockDataContext.Object);
            this._MockUserStorage.Protected().Setup<IUDICO.UserManagement.Models.IDataContext>("GetDbContext").Returns(
                this._MockUserDataContext.Object);
            this._HttpPostedFileBase = new Mock<HttpPostedFileBase>();
            this._HttpPostedFileBase.SetupGet(i => i.FileName).Returns("file");
				//this._CourseStoragePath = Path.Combine(
				//    ConfigurationManager.AppSettings.Get("RootTestFolder"), @"CourseManagement\Data");

				this._CourseStoragePath = Path.Combine(
					 ConfigurationManager.AppSettings.Get("PathToIUDICO.UnitTests"), @"IUDICO.UnitTests\DataGenerator\Data");
				//this._MockStorageProtectedMethodTestClass = new Mock<MixedCourseStorageProtectedMethodTestClass>();
				//this._MockStorageProtectedMethodTestClass.Protected().Setup<IDataContext>("GetDbContext").Returns(
				//    this._MockDataContext.Object);

            this.Setup();
        }

        public static FakeCourseStorage GetInstance()
        {
            return instance ?? (instance = new FakeCourseStorage());
        }

        public void Setup()
        {
            this._MockDatabaseStorage.CallBase = true;
            this._MockDatabaseStorage.Protected().Setup<string>("GetCoursesPath").Returns(this._CourseStoragePath);
				this._MockDatabaseStorage.Setup(a => a.GetCoursePath(It.IsAny<int>())).Returns<int>(a => this._CourseStoragePath + @"\Tests\" + (this.counter++).ToString());
			   this._MockDatabaseStorage.Setup(a=>a.Parse(It.IsAny<int>()));
				this._MockDatabaseStorage.Setup(x=>x.GetCourseTempPath(It.IsAny<int>())).Returns<int>(a=> Path.Combine(this._CourseStoragePath,@"Data\WorkFolder",a.ToString()));
            this._MockDatabaseStorage.Setup(c => c.GetTemplatesPath()).Returns(Path.Combine(this._CourseStoragePath, "Templates"));
            this._MockUserDataContext.SetupGet(c => c.Users).Returns(new MemoryTable<User>(this.mockUserData));

            var userService = new Mock<IUserService>();
            this._MockLmsService.Setup(l => l.FindService<IUserService>()).Returns(userService.Object);

            userService.Setup(s => s.GetCurrentUser()).Returns(this.mockUserData[0]);
            userService.Setup(s => s.GetUsers()).Returns(this.mockUserData);

            this.ClearTables();
        }

        public void ClearTables()
        {
            var mockCourseData = new Course[]{};
            var mockCourseUserData = new CourseUser[]{};
            var mockNodeData = new Node[]{};
            var mockNodeResourceData = new NodeResource[]{};

            this._MockDataContext.SetupGet(c => c.Courses).Returns(new MemoryTable<Course>(mockCourseData));
            this._MockDataContext.SetupGet(c => c.Nodes).Returns(new MemoryTable<Node>(mockNodeData));
            this._MockDataContext.SetupGet(c => c.NodeResources).Returns(new MemoryTable<NodeResource>(mockNodeResourceData));
            this._MockDataContext.SetupGet(c => c.CourseUsers).Returns(new MemoryTable<CourseUser>(mockCourseUserData));
        }
    }

	// public class MixedCourseStorageProtectedMethodTestClass : MixedCourseStorage
	// {
	//     public MixedCourseStorageProtectedMethodTestClass(ILmsService ilmsService)
	//         : base(ilmsService)
	//     {
	//     }

	//     public Item AddSubItemsTest(
	//         Item parentItem, Node parentNode, int courseId, ManifestManager helper, ref Manifest manifest)
	//     {
	//         return this.AddSubItems(parentItem, parentNode, courseId, helper, ref manifest);
	//     }
	// }
	//}
}
