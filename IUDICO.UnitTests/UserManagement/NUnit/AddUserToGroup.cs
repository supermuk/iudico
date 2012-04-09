using System.Linq;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class AddUserToGroup
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        [Test]
        public void AddExistingUserToExistingGroup()
        {
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            var group = new Group {Id = 12345678, Name = "pmi31"};
            _Tests.Storage.CreateGroup(group);
            group = _Tests.Storage.GetGroup(group.Id);
            var temp = new User {Username = "name", Email = "mail@mail.com", Password = "123"};
            _Tests.Storage.CreateUser(temp);
            temp = _Tests.Storage.GetUser(u => u.Username == temp.Username);

            _Tests.Storage.AddUserToGroup(group, temp);
            Assert.IsTrue(_Tests.Storage.GetGroupsByUser(temp).Contains(group));

            _Tests.Storage.DeleteGroup(group.Id);
            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void AddExistingUserToNonExistingGroup()
        {
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            var group = new Group {Id = 12366, Name = "pmp51"};

            var temp = new User {Username = "name", Email = "mail@mail.com", Password = "123"};
            _Tests.Storage.CreateUser(temp);
            temp = _Tests.Storage.GetUser(u => u.Username == temp.Username);

            _Tests.Storage.AddUserToGroup(group, temp);
            Assert.IsFalse(_Tests.Storage.GetGroupsByUser(temp).Contains(group));

            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void AddNonExistingUserToExistingGroup()
        {
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            var group = new Group {Id = 1266678, Name = "pmi31"};
            _Tests.Storage.CreateGroup(group);
            group = _Tests.Storage.GetGroup(group.Id);
            var temp = new User {Username = "name", Email = "mail@mail.com", Password = "123"};

            _Tests.Storage.AddUserToGroup(group, temp);
            Assert.IsTrue(_Tests.Storage.GetGroupsByUser(temp).Contains(group));
            Assert.AreEqual(null, _Tests.Storage.GetUser(u => u.Username == temp.Username));

            _Tests.Storage.DeleteGroup(group.Id);
        }

        [Test]
        public void AddNonExistingUserToNonExistingGroup()
        {
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            var group = new Group {Id = 12366, Name = "pmp51"};

            var temp = new User {Username = "name", Email = "mail@mail.com", Password = "123"};

            _Tests.Storage.AddUserToGroup(group, temp);

            Assert.AreEqual(null, _Tests.Storage.GetUser(u => u.Username == temp.Username));
            Assert.AreEqual(0, _Tests.Storage.GetGroupsByUser(temp).Count());
        }

        [Test]
        public void GetGroupsAvailableToExistingUser()
        {
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            var group = new Group {Id = 12345678, Name = "pmi31"};
            _Tests.Storage.CreateGroup(group);
            group = _Tests.Storage.GetGroup(group.Id);

            var temp = new User {Username = "name", Email = "mail@mail.com", Password = "123"};
            _Tests.Storage.CreateUser(temp);
            temp = _Tests.Storage.GetUser(u => u.Username == temp.Username);

            Assert.IsTrue(_Tests.Storage.GetGroupsAvailableToUser(temp).Contains(group));

            _Tests.Storage.DeleteGroup(group.Id);
            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void GetGroupsAvailableToNonExistingUser()
        {
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            var group = new Group {Id = 15678, Name = "pmi31"};
            _Tests.Storage.CreateGroup(group);
            group = _Tests.Storage.GetGroup(group.Id);

            var temp = new User {Username = "name", Email = "mail@mail.com", Password = "123"};

            Assert.IsTrue(_Tests.Storage.GetGroupsAvailableToUser(temp).Contains(group));
            Assert.AreEqual(null, _Tests.Storage.GetUser(u => u.Username == temp.Username));

            _Tests.Storage.DeleteGroup(group.Id);
        }
    }
}
