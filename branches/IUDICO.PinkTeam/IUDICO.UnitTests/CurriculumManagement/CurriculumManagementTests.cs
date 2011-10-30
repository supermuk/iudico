using System;
using System.Data.Linq;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Services;
using IUDICO.UserManagement.Models.Storage;
using Moq;
using Moq.Protected;
using IUDICO.CurriculumManagement.Models.Storage;

namespace IUDICO.UnitTests.CurriculumManagement
{
    public class CurriculumManagementTests
    {
        #region Protected members

        protected static CurriculumManagementTests _Instance;

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

        public Mock<MixedCurriculumStorage> MockStorage
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

        public ICurriculumStorage Storage
        {
            get { return MockStorage.Object; }
        }

        public Mock<ITable> Users
        {
            get;
            protected set;
        }

        public Mock<ITable> Groups
        {
            get;
            protected set;
        }

        public Mock<ITable> GroupUsers
        {
            get;
            protected set;
        }

        public Mock<ITable> Curriculums
        {
            get;
            protected set;
        }

        #endregion

        private CurriculumManagementTests()
        {
            MockDataContext = new Mock<IDataContext>();
            MockLmsService = new Mock<ILmsService>();
            MockStorage = new Mock<MixedCurriculumStorage>(MockLmsService.Object);

            Users = new Mock<ITable>();
            Groups = new Mock<ITable>();
            GroupUsers = new Mock<ITable>();
            Curriculums = new Mock<ITable>();

            Setup();
            SetupTables();
        }

        public static CurriculumManagementTests GetInstance()
        {
            return _Instance ?? (_Instance = new CurriculumManagementTests());
        }

        public void Setup()
        {
            MockLmsService.Setup(l => l.GetIDataContext()).Returns(MockDataContext.Object);
            //MockDataContext.Setup(l=>l.SubmitChanges()).Re
            //MockLmsService.Setup(l => l.GetDbDataContext()).Returns(MockDataContext.Object);
            //MockStorage.Protected().Setup<IDataContext>("GetDbDataContext").Returns(MockDataContext.Object);
            //MockStorage.Setup(s => s.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        }

        public void SetupTables()
        {
            var mockUserData = new[] {
                new User {Id = Guid.NewGuid(), Username = "panza", Email = "ipetrovych@gmail.com", Password = "", },
            };

            var mockGroupData = new[] {
                new Group {Id = 1, Name = "PMI51", Deleted = false}
            };

            var mockGroupUserData = new[] {
                new GroupUser {GroupRef = 1, UserRef = mockUserData[0].Id}
            };

            var mockCurriculumData = new[] { 
                new Curriculum { Id = 1, Name = "Curriculum1" } 
            };

            var mockUsers = new MockableTable<User>(Users.Object, mockUserData.AsQueryable());
            var mockGroups = new MockableTable<Group>(Groups.Object, mockGroupData.AsQueryable());
            var mockGroupUsers = new MockableTable<GroupUser>(GroupUsers.Object, mockGroupUserData.AsQueryable());
            var mockCurriculums=new MockableTable<Curriculum>(Curriculums.Object, mockCurriculumData.AsQueryable());

            //var mockUsers = new MockableTable<User>(Users.Object);
            //var mockGroups = new MockableTable<Group>(Groups.Object);
            //var mockGroupUsers = new MockableTable<GroupUser>(GroupUsers.Object);

            MockDataContext.SetupGet(c => c.Users).Returns(mockUsers);
            MockDataContext.SetupGet(c => c.Groups).Returns(mockGroups);
            MockDataContext.SetupGet(c => c.GroupUsers).Returns(mockGroupUsers);
            MockDataContext.SetupGet(c => c.Curriculums).Returns(mockCurriculums);
        }
    }
}
