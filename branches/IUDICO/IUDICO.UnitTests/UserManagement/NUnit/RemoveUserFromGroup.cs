using System.Linq;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class RemoveUserFromGroup
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void RemoveExistingUserFromExistingGroup()
        {
            var group = new Group { Id = 12345678, Name = "pmi31" };
            
            this.tests.Storage.CreateGroup(group);
            group = this.tests.Storage.GetGroup(group.Id);

            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };
            this.tests.Storage.CreateUser(temp);
            
            temp = this.tests.Storage.GetUser(u => u.Username == temp.Username);

            this.tests.Storage.AddUserToGroup(group, temp);
            this.tests.Storage.RemoveUserFromGroup(group, temp);
            
            Assert.IsFalse(this.tests.Storage.GetGroupsByUser(temp).Contains(group));

            this.tests.Storage.DeleteGroup(group.Id);
            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void RemoveExistingUserFromNonExistingGroup()
        {
            var group = new Group { Id = 12677, Name = "pmi31" };
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);
            temp = this.tests.Storage.GetUser(u => u.Username == temp.Username);

            this.tests.Storage.AddUserToGroup(group, temp);
            this.tests.Storage.RemoveUserFromGroup(group, temp);
            Assert.IsFalse(this.tests.Storage.GetGroupsByUser(temp).Contains(group));

            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void RemoveNonExistingUserFromExistingGroup()
        {
            var group = new Group { Id = 12345678, Name = "pmi31" };
            
            this.tests.Storage.CreateGroup(group);

            group = this.tests.Storage.GetGroup(group.Id);

            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.AddUserToGroup(group, temp);
            this.tests.Storage.RemoveUserFromGroup(group, temp);
            
            Assert.IsFalse(this.tests.Storage.GetGroupsByUser(temp).Contains(group));
            Assert.AreEqual(null, this.tests.Storage.GetUser(u => u.Username == temp.Username));

            this.tests.Storage.DeleteGroup(group.Id);
        }

        [Test]
        public void RemoveNonExistingUserFromNonExistingGroup()
        {
            var group = new Group { Id = 345678, Name = "pmi31" };
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.AddUserToGroup(group, temp);
            this.tests.Storage.RemoveUserFromGroup(group, temp);

            Assert.AreEqual(null, this.tests.Storage.GetUser(u => u.Username == temp.Username));
            Assert.AreEqual(0, this.tests.Storage.GetGroupsByUser(temp).Count());
        }
    }
}