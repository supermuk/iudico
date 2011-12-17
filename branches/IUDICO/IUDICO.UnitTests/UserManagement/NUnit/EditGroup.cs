using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class EditGroup
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        [Test]
        public void EditGroupExistingValid()
        {
            var group = new Group { Id = 1254, Name = "pmp41" };

            _Tests.Storage.CreateGroup(group);
            group = new Group { Id = 1254, Name = "pmp51" };
            _Tests.Storage.EditGroup(group.Id,group);
            Assert.IsTrue(group.Name == _Tests.Storage.GetGroup(group.Id).Name && group.Id == _Tests.Storage.GetGroup(group.Id).Id);

            _Tests.Storage.DeleteGroup(group.Id);
        }
        [Test]
        public void EditGroupExistingInvalid()
        {
            /*
            var group = new Group { Id = 124, Name = "pmp41" };

            _Tests.Storage.CreateGroup(group);
            group = new Group { Id = 126, Name = "pmp41" };
            _Tests.Storage.EditGroup(group.Id, group);
            //Assert.IsTrue(group.Name == _Tests.Storage.GetGroup(group.Id).Name && group.Id == _Tests.Storage.GetGroup(group.Id).Id);

            _Tests.Storage.DeleteGroup(group.Id);*/
        }
        [Test]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void EditGroupNonExisting()
        {
            var group = new Group { Id = 1253, Name = "pmp51" };
            _Tests.Storage.EditGroup(group.Id, group);
        }
    }
}
