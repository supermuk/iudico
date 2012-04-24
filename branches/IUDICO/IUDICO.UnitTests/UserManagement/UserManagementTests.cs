// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManagementTests.cs" company="">
//   
// </copyright>
// <summary>
//   The user management tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IUDICO.UnitTests.UserManagement
{
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

    /// <summary>
    /// The user management tests.
    /// </summary>
    public class UserManagementTests
    {
        #region Protected members

        /// <summary>
        /// The instance.
        /// </summary>
        protected static UserManagementTests instance;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets MockDataContext.
        /// </summary>
        public Mock<IDataContext> MockDataContext { get; protected set; }

        /// <summary>
        /// Gets or sets MockLmsService.
        /// </summary>
        public Mock<ILmsService> MockLmsService { get; protected set; }

        /// <summary>
        /// Gets or sets MockStorage.
        /// </summary>
        public Mock<DatabaseUserStorage> MockStorage { get; protected set; }

        /// <summary>
        /// Gets DataContext.
        /// </summary>
        public IDataContext DataContext
        {
            get
            {
                return this.MockDataContext.Object;
            }
        }

        /// <summary>
        /// Gets LmsService.
        /// </summary>
        public ILmsService LmsService
        {
            get
            {
                return this.MockLmsService.Object;
            }
        }

        /// <summary>
        /// Gets Storage.
        /// </summary>
        public IUserStorage Storage
        {
            get
            {
                return this.MockStorage.Object;
            }
        }

        /// <summary>
        /// Gets or sets Users.
        /// </summary>
        public Mock<ITable> Users { get; protected set; }

        /// <summary>
        /// Gets or sets Groups.
        /// </summary>
        public Mock<ITable> Groups { get; protected set; }

        /// <summary>
        /// Gets or sets GroupUsers.
        /// </summary>
        public Mock<ITable> GroupUsers { get; protected set; }

        /// <summary>
        /// Gets or sets UserRoles.
        /// </summary>
        public Mock<ITable> UserRoles { get; protected set; }

        #endregion

        /// <summary>
        /// The user comparer.
        /// </summary>
        public class UserComparer : IEqualityComparer<User>
        {
            #region Implementation of IEqualityComparer<in User>

            /// <summary>
            /// The equals.
            /// </summary>
            /// <param name="x">
            /// The x.
            /// </param>
            /// <param name="y">
            /// The y.
            /// </param>
            /// <returns>
            /// The equals.
            /// </returns>
            public bool Equals(User x, User y)
            {
                return x.Username == y.Username && x.Email == y.Email;
            }

            /// <summary>
            /// The get hash code.
            /// </summary>
            /// <param name="obj">
            /// The obj.
            /// </param>
            /// <returns>
            /// The get hash code.
            /// </returns>
            public int GetHashCode(User obj)
            {
                return (obj.Username + obj.Email).GetHashCode();
            }

            #endregion
        }

        /// <summary>
        /// The test users.
        /// </summary>
        /// <param name="users">
        /// The users.
        /// </param>
        /// <param name="inserted">
        /// The inserted.
        /// </param>
        /// <returns>
        /// The test users.
        /// </returns>
        public bool TestUsers(IEnumerable<User> users, IEnumerable<User> inserted)
        {
            return inserted.Except(users, new UserComparer()).Count() == 0;
        }

        /// <summary>
        /// The test users.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        /// <returns>
        /// The test users.
        /// </returns>
        public bool TestUsers(User user, User expected)
        {
            return (new UserComparer()).Equals(user, expected);
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="UserManagementTests"/> class from being created.
        /// </summary>
        private UserManagementTests()
        {
            this.MockDataContext = new Mock<IDataContext>();
            this.MockLmsService = new Mock<ILmsService>();
            this.MockStorage = new Mock<DatabaseUserStorage>(this.MockLmsService.Object);

            this.Users = new Mock<ITable>();
            this.Groups = new Mock<ITable>();
            this.GroupUsers = new Mock<ITable>();
            this.UserRoles = new Mock<ITable>();

            this.Setup();
            this.SetupTables();

            this.ChangeCurrentUser("panza");
        }

        /// <summary>
        /// The get instance.
        /// </summary>
        /// <returns>
        /// </returns>
        public static UserManagementTests GetInstance()
        {
            return instance ?? (instance = new UserManagementTests());
        }

        /// <summary>
        /// The setup.
        /// </summary>
        public void Setup()
        {
            this.MockStorage.Protected().Setup<IDataContext>("GetDbContext").Returns(this.MockDataContext.Object);
            this.MockStorage.Protected().Setup<string>("GetPath").Returns(
                Path.Combine(ConfigurationManager.AppSettings["PathToIUDICO.UnitTests"], "IUDICO.LMS"));
            this.MockStorage.Setup(
                s => s.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).
                Returns(true);

            this.MockStorage.Setup(s => s.GetUserRoles(It.IsAny<string>())).Returns(
                (string username) => this.GetUserRoles(username));
            this.MockStorage.Setup(s => s.GetGroupsByUser(It.IsAny<User>())).Returns(
                (User user) => this.GetGroupsByUser(user));
        }

        /// <summary>
        /// The setup tables.
        /// </summary>
        public void SetupTables()
        {
            var mockUserData = new[]
                {
                    new User
                        {
                            Id = Guid.NewGuid(), 
                            Username = "panza", 
                            Email = "ipetrovych@gmail.com", 
                            Password = this.Storage.EncryptPassword("somepassword"), 
                        }, 
                };

            var mockGroupData = new[] { new Group { Id = 1, Name = "PMI51", Deleted = false } };

            var mockGroupUserData = new[] { new GroupUser { GroupRef = 1, UserRef = mockUserData[0].Id } };

            var mockUserRoleData = new[] { new UserRole { UserRef = mockUserData[0].Id, RoleRef = (int)Role.Teacher } };

            var mockUsers = new MemoryTable<User>(mockUserData);
            var mockGroups = new MemoryTable<Group>(mockGroupData);
            var mockGroupUsers = new MemoryTable<GroupUser>(mockGroupUserData);
            var mockUserRoles = new MemoryTable<UserRole>(mockUserRoleData);

            this.MockDataContext.SetupGet(c => c.Users).Returns(mockUsers);
            this.MockDataContext.SetupGet(c => c.Groups).Returns(mockGroups);
            this.MockDataContext.SetupGet(c => c.GroupUsers).Returns(mockGroupUsers);
            this.MockDataContext.SetupGet(c => c.UserRoles).Returns(mockUserRoles);
        }

        #region Mocked Functions

        /// <summary>
        /// The change current user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        public void ChangeCurrentUser(User user)
        {
            this.MockStorage.Setup(s => s.GetCurrentUser()).Returns(user);
            this.MockStorage.Protected().Setup<string>("GetIdentityName").Returns(user.Username);
        }

        /// <summary>
        /// The change current user.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        public void ChangeCurrentUser(string username)
        {
            this.ChangeCurrentUser(this.Storage.GetUser(u => u.Username == username));
        }

        /// <summary>
        /// The get user roles.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <returns>
        /// </returns>
        protected IEnumerable<Role> GetUserRoles(string username)
        {
            var user = this.Storage.GetUser(u => u.Username == username);

            return this.DataContext.UserRoles.Where(ur => ur.UserRef == user.Id).Select(ur => (Role)ur.RoleRef);
        }

        /// <summary>
        /// The get groups by user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// </returns>
        protected IEnumerable<Group> GetGroupsByUser(User user)
        {
            var groupIds = this.DataContext.GroupUsers.Where(gu => gu.UserRef == user.Id).Select(gu => gu.GroupRef);

            return this.DataContext.Groups.Where(g => groupIds.Contains(g.Id)).ToList();
        }

        #endregion
    }
}