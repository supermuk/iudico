using System;
using System.Linq;
using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models.Storage;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class UniqueUserId
    {
        protected readonly UserManagementTests Tests = UserManagementTests.GetInstance();
        protected readonly IUserStorage Storage = UserManagementTests.GetInstance().Storage;

        [Test]
        public void CreateUser()
        {
            const string userId = "UniqueUserId_CreateUser";
            const string username = userId + "_";

            Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(Tests.Storage.GetUser(u => u.Username == "panza"));

            Storage.CreateUser(new User { Name = "Create User", Username = username, Password = "123456", UserId = userId });
            var created = Storage.GetUser(u => u.Username == username);

            Assert.AreEqual(created.UserId, userId);
        }

        [Test]
        public void UniqueIdAvailablity()
        {
            const string userId = "UniqueUserId_UniqueIdAvailablity";
            const string username = userId + "_";

            Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(Tests.Storage.GetUser(u => u.Username == "panza"));

            Assert.True(Storage.UserUniqueIdAvailable(userId, Guid.NewGuid()));

            Storage.CreateUser(new User { Name = "Unique Id Availablity", Username = username, Password = "123456", UserId = userId });
            var created = Storage.GetUser(u => u.Username == username);
            Assert.AreEqual(created.UserId, userId);

            Assert.True(Storage.UserUniqueIdAvailable("no such id", created.Id));
            Assert.True(Storage.UserUniqueIdAvailable(userId, created.Id));
            Assert.False(Storage.UserUniqueIdAvailable(userId, Guid.NewGuid()));
        }
        [Test]
        public void EditUser()
        {
            const string userId = "UniqueUserId_EditUser";
            const string newUserId = "UniqueUserId_EditUser_Changed";
            const string username = userId + "_";
            const string newUsername = newUserId + "_";

            Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(Tests.Storage.GetUser(u => u.Username == "panza"));

            Storage.CreateUser(new User { Name = "Edit User", Username = username, Password = "123456", UserId = userId });
            var created = Storage.GetUser(u => u.Username == username);
            Assert.AreEqual(created.UserId, userId);

            created.UserId = newUserId;
            Storage.EditUser(created.Id, new User { Name = "Edit User", Username = newUsername, Password = "123456", UserId = newUserId });
            Assert.AreEqual(created.UserId, newUserId);
        }

        [Test]
        public void DeleteUserByUniqueId()
        {
            const string userId = "UniqueUserId_DeleteByUniqueIdUser";
            const string username = userId + "_";

            Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(Tests.Storage.GetUser(u => u.Username == "panza"));

            Assert.True(Storage.UserUniqueIdAvailable(userId, Guid.NewGuid()));

            Storage.CreateUser(new User { Name = "Delete User By Unique Id", Username = username, Password = "123456", UserId = userId });
            var created = Storage.GetUser(u => u.Username == username);

            Assert.AreEqual(created.UserId, userId);

            Assert.True(Storage.UserUniqueIdAvailable("no such id", created.Id));
            Assert.True(Storage.UserUniqueIdAvailable(userId, created.Id));
            Assert.False(Storage.UserUniqueIdAvailable(userId, Guid.NewGuid()));

            Storage.DeleteUser(u=>u.UserId==created.UserId);

            Assert.AreEqual(Storage.GetUsers(u=>u.Id == created.Id).Count(), 0);
        }
    }
}
