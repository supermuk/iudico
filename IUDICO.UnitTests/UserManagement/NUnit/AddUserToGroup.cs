using System;
using System.Linq;
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

            var group = new Group { Id = 12345678, Name = "pmi31" };
            _Tests.Storage.CreateGroup(group);
            group = _Tests.Storage.GetGroup(group.Id);
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };
            _Tests.Storage.CreateUser(temp);
            temp = _Tests.Storage.GetUser(u => u.Username == temp.Username);

            _Tests.Storage.AddUserToGroup(group, temp);
            Assert.IsTrue(_Tests.Storage.GetGroupsByUser(temp).Contains(group));

            _Tests.Storage.DeleteGroup(group.Id);
            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }
    }
}

