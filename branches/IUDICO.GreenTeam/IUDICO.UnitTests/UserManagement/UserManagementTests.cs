using System;
using System.Data.Linq;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Services;
using IUDICO.UserManagement.Models.Storage;
using Moq;
using Moq.Protected;

namespace IUDICO.UnitTests.UserManagement
{
    public class UserManagementTests
    {
        #region Protected members

        protected static UserManagementTests _Instance;

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

        #endregion

        private UserManagementTests()
        {
            MockDataContext = new Mock<IDataContext>();
            MockLmsService = new Mock<ILmsService>();
            MockStorage = new Mock<DatabaseUserStorage>(MockLmsService.Object);

            //Storage = new DatabaseUserStorage(MockLmsService.Object);

            Users = new Mock<ITable>();
            Groups = new Mock<ITable>();
            GroupUsers = new Mock<ITable>();

            Setup();
            SetupTables();
        }
        
        public static UserManagementTests GetInstance()
        {
            return _Instance ?? (_Instance = new UserManagementTests());
        }

        public void Setup()
        {
            MockLmsService.Setup(l => l.GetIDataContext()).Returns(MockDataContext.Object);
            MockStorage.Protected().Setup<IDataContext>("GetDbContext").Returns(MockDataContext.Object);
            //MockStorage.Setup(s => s.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        }

        public void SetupTables()
        {
            var mockUserData = new[]
                                   {
                                       new User {Id = Guid.NewGuid(), Username = "panza", Password = Storage.EncryptPassword("somepassword"), },
                                   };

            var mockGroupData = new[]
                                    {
                                        new Group {Id = 1, Name = "PMI51", Deleted = false}
                                    };

            var mockGroupUserData = new[]
                                        {
                                            new GroupUser {GroupRef = 1, UserRef = mockUserData[0].Id}
                                        };

            var mockUsers = new MockableTable<User>(Users.Object, mockUserData.AsQueryable());
            var mockGroups = new MockableTable<Group>(Groups.Object, mockGroupData.AsQueryable());
            var mockGroupUsers = new MockableTable<GroupUser>(GroupUsers.Object, mockGroupUserData.AsQueryable());

            MockDataContext.SetupGet(c => c.Users).Returns(mockUsers);
            MockDataContext.SetupGet(c => c.Groups).Returns(mockGroups);
            MockDataContext.SetupGet(c => c.GroupUsers).Returns(mockGroupUsers);
        }
    }
}
