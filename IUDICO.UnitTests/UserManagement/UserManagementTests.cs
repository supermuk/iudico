using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.IO;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models;
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

        public Mock<IDataContext> MockDataContext { get; protected set; }

        public Mock<ILmsService> MockLmsService { get; protected set; }

        public Mock<DatabaseUserStorage> MockStorage { get; protected set; }

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

        public Mock<ITable> Users { get; protected set; }

        public Mock<ITable> Groups { get; protected set; }

        public Mock<ITable> GroupUsers { get; protected set; }

        public Mock<ITable> UserRoles { get; protected set; }

        #endregion

        public class UserComparer : IEqualityComparer<User>
        {
            #region Implementation of IEqualityComparer<in User>

            public bool Equals(User x, User y)
            {
                return x.Username == y.Username && x.Email == y.Email;
            }

            public int GetHashCode(User obj)
            {
                return (obj.Username + obj.Email).GetHashCode();
            }

            #endregion
        }

        public bool TestUsers(IEnumerable<User> users, IEnumerable<User> inserted)
        {
            return inserted.Except(users, new UserComparer()).Count() == 0;
        }

        public bool TestUsers(User user, User expected)
        {
            return (new UserComparer()).Equals(user, expected);
        }

        private UserManagementTests()
        {
            MockDataContext = new Mock<IDataContext>();
            MockLmsService = new Mock<ILmsService>();
            MockStorage = new Mock<DatabaseUserStorage>(MockLmsService.Object);

            //Storage = new DatabaseUserStorage(MockLmsService.Object);

            Users = new Mock<ITable>();
            Groups = new Mock<ITable>();
            GroupUsers = new Mock<ITable>();
            UserRoles = new Mock<ITable>();

            Setup();
            SetupTables();
        }

        public static UserManagementTests GetInstance()
        {
            return _Instance ?? (_Instance = new UserManagementTests());
        }

        public void Setup()
        {
//            MockLmsService.Setup(l => l.GetIDataContext()).Returns(MockDataContext.Object);
            MockStorage.Protected().Setup<IDataContext>("GetDbContext").Returns(MockDataContext.Object);
            MockStorage.Protected().Setup<string>("GetPath").Returns(Path.Combine(ConfigurationManager.AppSettings["PathToIUDICO.UnitTests"], "IUDICO.LMS"));
//            MockStorage.Setup(s => s.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            MockStorage.Setup(s => s.GetUserRoles(It.IsAny<string>())).Returns(
                (string username) => GetUserRoles(username));
            MockStorage.Setup(s => s.GetGroupsByUser(It.IsAny<User>())).Returns((User user) => GetGroupsByUser(user));
        }

        public void SetupTables()
        {
            var mockUserData = new[]
                                   {
                                       new User
                                           {
                                               Id = Guid.NewGuid(),
                                               Username = "panza",
                                               Email = "ipetrovych@gmail.com",
                                               Password = Storage.EncryptPassword("somepassword"),
                                           },
                                   };

            var mockGroupData = new[]
                                    {
                                        new Group {Id = 1, Name = "PMI51", Deleted = false}
                                    };

            var mockGroupUserData = new[]
                                        {
                                            new GroupUser {GroupRef = 1, UserRef = mockUserData[0].Id}
                                        };

            var mockUserRoleData = new[]
                                       {
                                           new UserRole {UserRef = mockUserData[0].Id, RoleRef = (int) Role.Teacher}
                                       };

            var mockUsers = new MemoryTable<User>(mockUserData);
            var mockGroups = new MemoryTable<Group>(mockGroupData);
            var mockGroupUsers = new MemoryTable<GroupUser>(mockGroupUserData);
            var mockUserRoles = new MemoryTable<UserRole>(mockUserRoleData);

            MockDataContext.SetupGet(c => c.Users).Returns(mockUsers);
            MockDataContext.SetupGet(c => c.Groups).Returns(mockGroups);
            MockDataContext.SetupGet(c => c.GroupUsers).Returns(mockGroupUsers);
            MockDataContext.SetupGet(c => c.UserRoles).Returns(mockUserRoles);
        }

        #region Mocked Functions

        protected IEnumerable<Role> GetUserRoles(string username)
        {
            var user = Storage.GetUser(u => u.Username == username);

            return DataContext.UserRoles.Where(ur => ur.UserRef == user.Id).Select(ur => (Role) ur.RoleRef);
        }

        protected IEnumerable<Group> GetGroupsByUser(User user)
        {
            var groupIds = DataContext.GroupUsers.Where(gu => gu.UserRef == user.Id).Select(gu => gu.GroupRef);

            return DataContext.Groups.Where(g => groupIds.Contains(g.Id)).ToList();
        }

        #endregion
    }
}