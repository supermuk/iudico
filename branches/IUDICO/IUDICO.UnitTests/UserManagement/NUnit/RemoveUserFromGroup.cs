using System.Linq;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class RemoveUserFromGroup
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();


        [Test]
        public void RemoveExistingUserFromExistingGroup()
        {
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            var group = new Group {Id = 12345678, Name = "pmi31"};
            _Tests.Storage.CreateGroup(group);
            group = _Tests.Storage.GetGroup(group.Id);
            var temp = new User {Username = "name", Email = "mail@mail.com", Password = "123"};
            _Tests.Storage.CreateUser(temp);
            temp = _Tests.Storage.GetUser(u => u.Username == temp.Username);

            _Tests.Storage.AddUserToGroup(group, temp);
            _Tests.Storage.RemoveUserFromGroup(group, temp);
            Assert.IsFalse(_Tests.Storage.GetGroupsByUser(temp).Contains(group));

            _Tests.Storage.DeleteGroup(group.Id);
            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void RemoveExistingUserFromNonExistingGroup()
        {
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            var group = new Group {Id = 12677, Name = "pmi31"};

            var temp = new User {Username = "name", Email = "mail@mail.com", Password = "123"};
            _Tests.Storage.CreateUser(temp);
            temp = _Tests.Storage.GetUser(u => u.Username == temp.Username);

            _Tests.Storage.AddUserToGroup(group, temp);
            _Tests.Storage.RemoveUserFromGroup(group, temp);
            Assert.IsFalse(_Tests.Storage.GetGroupsByUser(temp).Contains(group));

            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void RemoveNonExistingUserFromExistingGroup()
        {
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            var group = new Group {Id = 12345678, Name = "pmi31"};
            _Tests.Storage.CreateGroup(group);
            group = _Tests.Storage.GetGroup(group.Id);
            var temp = new User {Username = "name", Email = "mail@mail.com", Password = "123"};


            _Tests.Storage.AddUserToGroup(group, temp);
            _Tests.Storage.RemoveUserFromGroup(group, temp);
            Assert.IsFalse(_Tests.Storage.GetGroupsByUser(temp).Contains(group));
            Assert.AreEqual(null, _Tests.Storage.GetUser(u => u.Username == temp.Username));
            _Tests.Storage.DeleteGroup(group.Id);
        }

        [Test]
        public void RemoveNonExistingUserFromNonExistingGroup()
        {
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            var group = new Group {Id = 345678, Name = "pmi31"};
            var temp = new User {Username = "name", Email = "mail@mail.com", Password = "123"};


            _Tests.Storage.AddUserToGroup(group, temp);
            _Tests.Storage.RemoveUserFromGroup(group, temp);
            Assert.AreEqual(null, _Tests.Storage.GetUser(u => u.Username == temp.Username));
            Assert.AreEqual(0, _Tests.Storage.GetGroupsByUser(temp).Count());
        }
    }
}