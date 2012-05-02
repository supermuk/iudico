using System;
using System.Linq;

using IUDICO.Common.Models.Shared;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class UniqueUserId
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void CreateUser()
        {
            const string UserId = "UniqueUserId_CreateUser";
            const string Username = UserId + "_";

            this.tests.Storage.CreateUser(
                new User { Name = "Create User", Username = Username, Password = "123456", UserId = UserId });

            var created = this.tests.Storage.GetUser(u => u.Username == Username);

            Assert.AreEqual(created.UserId, UserId);

            this.tests.Storage.DeleteUser(u => u.Username == Username);
        }

        [Test]
        public void UniqueOpenIdAvailablity()
        {
            const string OpenId = "UniqueUserId_UniqueIdAvailablity";
            const string Username = OpenId + "_";

            Assert.True(this.tests.Storage.UserOpenIdAvailable(OpenId, Guid.NewGuid()));

            this.tests.Storage.CreateUser(
                new User { Name = "OpenId Id Availablity", Username = Username, Password = "123456", OpenId = OpenId });

            var created = this.tests.Storage.GetUser(u => u.Username == Username);

            Assert.AreEqual(created.OpenId, OpenId);
            Assert.True(this.tests.Storage.UserOpenIdAvailable("no such id", created.Id));
            Assert.True(this.tests.Storage.UserOpenIdAvailable(OpenId, created.Id));
            Assert.False(this.tests.Storage.UserOpenIdAvailable(OpenId, Guid.NewGuid()));

            this.tests.Storage.DeleteUser(u => u.Username == Username);
        }

        [Test]
        public void UniqueIdAvailablity()
        {
            const string UserId = "UniqueUserId_UniqueIdAvailablity";
            const string Username = UserId + "_";

            Assert.True(this.tests.Storage.UserUniqueIdAvailable(UserId, Guid.NewGuid()));

            this.tests.Storage.CreateUser(
                new User { Name = "Unique Id Availablity", Username = Username, Password = "123456", UserId = UserId });

            var created = this.tests.Storage.GetUser(u => u.Username == Username);

            Assert.AreEqual(created.UserId, UserId);
            Assert.True(this.tests.Storage.UserUniqueIdAvailable("no such id", created.Id));
            Assert.True(this.tests.Storage.UserUniqueIdAvailable(UserId, created.Id));
            Assert.False(this.tests.Storage.UserUniqueIdAvailable(UserId, Guid.NewGuid()));

            this.tests.Storage.DeleteUser(u => u.Username == Username);
        }

        [Test]
        public void EditUser()
        {
            const string UserId = "UniqueUserId_EditUser";
            const string NewUserId = "UniqueUserId_EditUser_Changed";
            const string Username = UserId + "_";
            const string NewUsername = NewUserId + "_";

            this.tests.Storage.CreateUser(
                new User { Name = "Edit User", Username = Username, Password = "123456", UserId = UserId });

            var created = this.tests.Storage.GetUser(u => u.Username == Username);

            Assert.AreEqual(created.UserId, UserId);

            created.UserId = NewUserId;
            this.tests.Storage.EditUser(
                created.Id, 
                new User { Name = "Edit User", Username = NewUsername, Password = "123456", UserId = NewUserId });

            Assert.AreEqual(created.UserId, NewUserId);

            this.tests.Storage.DeleteUser(u => u.Username == NewUsername);
        }

        [Test]
        public void DeleteUserByUniqueId()
        {
            const string UserId = "UniqueUserId_DeleteByUniqueIdUser";
            const string Username = UserId + "_";

            Assert.True(this.tests.Storage.UserUniqueIdAvailable(UserId, Guid.NewGuid()));

            this.tests.Storage.CreateUser(
                new User
                    {
                       Name = "Delete User By Unique Id", Username = Username, Password = "123456", UserId = UserId 
                    });

            var created = this.tests.Storage.GetUser(u => u.Username == Username);

            Assert.AreEqual(created.UserId, UserId);
            Assert.True(this.tests.Storage.UserUniqueIdAvailable("no such id", created.Id));
            Assert.True(this.tests.Storage.UserUniqueIdAvailable(UserId, created.Id));
            Assert.False(this.tests.Storage.UserUniqueIdAvailable(UserId, Guid.NewGuid()));

            this.tests.Storage.DeleteUser(u => u.UserId == created.UserId);

            Assert.AreEqual(this.tests.Storage.GetUsers(u => u.Id == created.Id).Count(), 0);
        }
    }
}