using IUDICO.UserManagement.Models;

using NUnit.Framework;

using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models.Auth;

using System;
using System.Web.Security;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    using Moq;

    [TestFixture]
    internal class Models
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();
        protected UserService userService = new UserService(UserManagementTests.GetInstance().Storage);
        protected OpenIdRoleProvider roleProvider = new OpenIdRoleProvider(UserManagementTests.GetInstance().Storage);

        protected OpenIdMembershipProvider membProvider;

        [SetUp]
        public void SetUp()
        {
            var mock = new Mock<OpenIdMembershipProvider>(this.tests.Storage) { CallBase = true };
            mock.SetupGet(s => s.Name).Returns("IoCMembershipProvider");

            this.membProvider = mock.Object;
        }

        [Test]
        public void EditUsersAccount()
        {
            new EditUsersAccount();
        }

        [Test]
        public void EditModel()
        {
            new EditModel(this.tests.Storage.GetCurrentUser());
        }

        [Test]
        public void EditUserModel()
        {
            new EditUserModel();
        }

        [Test]
        public void AdminDetailsModel()
        {
            new AdminDetailsModel(
                this.tests.Storage.GetCurrentUser(),
                this.tests.Storage.GetUserRoles(this.tests.Storage.GetCurrentUser().Username),
                this.tests.Storage.GetGroupsByUser(this.tests.Storage.GetCurrentUser()));
        }

        [Test]
        public void UserService()
        {
            this.userService.GetUsersByGroup(new Group());
            this.userService.GetGroupsByUser(new User());
            this.userService.GetUsers();
            this.userService.GetUsers(u => u.Username == string.Empty);
            this.userService.GetCurrentUser();
            this.userService.GetGroup(0);
            this.userService.GetGroups();
            this.userService.GetCurrentUserRoles();
            
            this.tests.ChangeCurrentUser(new User());
            this.userService.GetCurrentUserRoles();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void OpenIdMembershipUser()
        {
            new OpenIdMembershipUser(
                "IocMembershipProvider",
                string.Empty,
                new object(),
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                true,
                true,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now);
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void OpenIdRoleProviderCreateRole()
        {
            this.roleProvider.CreateRole(string.Empty);
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void OpenIdRoleProviderDeleteRole()
        {
            this.roleProvider.DeleteRole(string.Empty, false);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void OpenIdRoleProviderUsersInRole()
        {
            this.roleProvider.FindUsersInRole("Admin", string.Empty);
        }

        [Test]
        public void OpenIdRoleProvider()
        {
            this.roleProvider.IsUserInRole(this.tests.Storage.GetCurrentUser().Username, "Admin");
            this.roleProvider.GetRolesForUser(this.tests.Storage.GetCurrentUser().Username);
            this.roleProvider.RoleExists("Admin");
            this.roleProvider.AddUsersToRoles(new[] { this.tests.Storage.GetCurrentUser().Username }, new[] { "Admin" });
            this.roleProvider.RemoveUsersFromRoles(new[] { this.tests.Storage.GetCurrentUser().Username }, new[] { "Admin" });
            this.roleProvider.GetUsersInRole("Admin");
            this.roleProvider.GetAllRoles();
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void OpenIdMembershipProviderChangeQa()
        {
            this.membProvider.ChangePasswordQuestionAndAnswer(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void OpenIdMembershipProviderResetPwd()
        {
            this.membProvider.ResetPassword(string.Empty, string.Empty);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void OpenIdMembershipProviderUpdateUser()
        {
            this.membProvider.UpdateUser(null);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void OpenIdMembershipProviderUnlockUser()
        {
            this.membProvider.UnlockUser(string.Empty);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void OpenIdMembershipProviderUsersOnline()
        {
            this.membProvider.GetNumberOfUsersOnline();
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void OpenIdMembershipProviderPassFormat()
        {
            var pf = this.membProvider.PasswordFormat;
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void OpenIdMembershipProviderCreateUser()
        {
            MembershipCreateStatus status;
            this.membProvider.CreateUser(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, new object(), out status);
        }

        //[Test]
        //[ExpectedException(typeof(ArgumentException))]
        //public void OpenIdMembershipProviderGetUser1()
        //{
        //    this.membProvider.GetUser(this.tests.Storage.GetCurrentUser().Id, false);
        //}

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void OpenIdMembershipProviderGetUser2()
        {
            this.membProvider.GetUser(this.tests.Storage.GetCurrentUser().Username, false);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void OpenIdMembershipProviderGetAllUsers()
        {
            int total;

            this.membProvider.GetAllUsers(0, 100, out total);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void OpenIdMembershipProviderFindByName()
        {
            int total;

            this.membProvider.FindUsersByName(string.Empty, 0, 100, out total);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void OpenIdMembershipProviderFindByEmail()
        {
            int total;

            this.membProvider.FindUsersByEmail(string.Empty, 0, 100, out total);
        }

        //[Test]
        //[Ignore]
        //public void OpenIdMembershipProvider()
        //{
        //    this.membProvider.GetPassword(string.Empty, string.Empty);
        //    this.membProvider.ChangePassword(string.Empty, string.Empty, string.Empty);
        //    this.membProvider.ChangePassword(this.tests.Storage.GetCurrentUser().Username, string.Empty, string.Empty);
        //    this.membProvider.ValidateUser(string.Empty, string.Empty);
        //    this.membProvider.GetUserNameByEmail(this.tests.Storage.GetCurrentUser().Email);
        //    this.membProvider.DeleteUser(string.Empty, false);

        //    var a = this.membProvider.EnablePasswordRetrieval;
        //    var b = this.membProvider.EnablePasswordReset;
        //    var c = this.membProvider.RequiresQuestionAndAnswer;
        //    var d = this.membProvider.MaxInvalidPasswordAttempts;
        //    var e = this.membProvider.PasswordAttemptWindow;
        //    var f = this.membProvider.RequiresUniqueEmail;
        //    var g = this.membProvider.MinRequiredPasswordLength;
        //    var h = this.membProvider.MinRequiredNonAlphanumericCharacters;
        //    var i = this.membProvider.PasswordStrengthRegularExpression;
        //}
    }
}