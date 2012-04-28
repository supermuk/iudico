using System.Linq;

using IUDICO.Common.Models.Shared;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class AddUserToGroup
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void AddExistingUserToExistingGroup()
        {
            var group = new Group { Id = 12345678, Name = "pmi31" };
            this.tests.Storage.CreateGroup(group);
            group = this.tests.Storage.GetGroup(group.Id);
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);
            temp = this.tests.Storage.GetUser(u => u.Username == temp.Username);

            this.tests.Storage.AddUserToGroup(group, temp);
            Assert.IsTrue(this.tests.Storage.GetGroupsByUser(temp).Contains(group));

            this.tests.Storage.DeleteGroup(group.Id);
            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void AddExistingUserToNonExistingGroup()
        {
            var group = new Group { Id = 12366, Name = "pmp51" };
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);
            temp = this.tests.Storage.GetUser(u => u.Username == temp.Username);

            this.tests.Storage.AddUserToGroup(group, temp);
            Assert.IsFalse(this.tests.Storage.GetGroupsByUser(temp).Contains(group));

            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void AddNonExistingUserToExistingGroup()
        {
            var group = new Group { Id = 1266678, Name = "pmi31" };
            this.tests.Storage.CreateGroup(group);
            group = this.tests.Storage.GetGroup(group.Id);
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.AddUserToGroup(group, temp);
            Assert.IsTrue(this.tests.Storage.GetGroupsByUser(temp).Contains(group));
            Assert.AreEqual(null, this.tests.Storage.GetUser(u => u.Username == temp.Username));

            this.tests.Storage.DeleteGroup(group.Id);
        }

        [Test]
        public void AddNonExistingUserToNonExistingGroup()
        {
            var group = new Group { Id = 12366, Name = "pmp51" };
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.AddUserToGroup(group, temp);

            Assert.AreEqual(null, this.tests.Storage.GetUser(u => u.Username == temp.Username));
            Assert.AreEqual(0, this.tests.Storage.GetGroupsByUser(temp).Count());
        }

        [Test]
        public void GetGroupsAvailableToExistingUser()
        {
            var group = new Group { Id = 12345678, Name = "pmi31" };
            this.tests.Storage.CreateGroup(group);
            group = this.tests.Storage.GetGroup(group.Id);

            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };
            this.tests.Storage.CreateUser(temp);
            temp = this.tests.Storage.GetUser(u => u.Username == temp.Username);

            Assert.IsTrue(this.tests.Storage.GetGroupsAvailableToUser(temp).Contains(group));

            this.tests.Storage.DeleteGroup(group.Id);
            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void GetGroupsAvailableToNonExistingUser()
        {
            this.tests.MockDatabaseStorage.Setup(s => s.GetCurrentUser()).Returns(
                this.tests.Storage.GetUser(u => u.Username == "panza"));

            var group = new Group { Id = 15678, Name = "pmi31" };
            this.tests.Storage.CreateGroup(group);
            group = this.tests.Storage.GetGroup(group.Id);

            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            Assert.IsTrue(this.tests.Storage.GetGroupsAvailableToUser(temp).Contains(group));
            Assert.AreEqual(null, this.tests.Storage.GetUser(u => u.Username == temp.Username));

            this.tests.Storage.DeleteGroup(group.Id);
        }
    }
}