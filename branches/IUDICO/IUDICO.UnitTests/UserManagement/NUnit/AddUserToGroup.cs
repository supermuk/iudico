using System.Collections.Generic;
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
            var group = new Group { Id = 12345678, Name = "pmi31" };
            _Tests.Storage.CreateGroup(group);

            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            _Tests.Storage.AddUserToGroup(group,temp);
            Assert.AreEqual(_Tests.Storage.GetUser(u => u.Username == "name").GroupUsers.Count,1);


            _Tests.Storage.DeleteGroup(group.Id);
            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }
    }
}

