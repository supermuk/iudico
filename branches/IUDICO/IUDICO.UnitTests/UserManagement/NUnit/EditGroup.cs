using System;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class EditGroup
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void EditGroupExistingValid()
        {
            var group = new Group { Id = 1254, Name = "pmp41" };

            this.tests.Storage.CreateGroup(group);

            group = new Group { Id = 1254, Name = "pmp51" };

            this.tests.Storage.EditGroup(group.Id, group);
            
            Assert.IsTrue(group.Name == this.tests.Storage.GetGroup(group.Id).Name && group.Id == this.tests.Storage.GetGroup(group.Id).Id);

            this.tests.Storage.DeleteGroup(group.Id);
        }

        [Test]
        public void EditGroupExistingInvalid()
        {
            //Done by Selenium test.
            /*
            var group = new Group { Id = 124, Name = "pmp41" };

            _Tests.Storage.CreateGroup(group);
            group = new Group { Id = 126, Name = "pmp41" };
            _Tests.Storage.EditGroup(group.Id, group);
            //Assert.IsTrue(group.Name == _Tests.Storage.GetGroup(group.Id).Name && group.Id == _Tests.Storage.GetGroup(group.Id).Id);

            _Tests.Storage.DeleteGroup(group.Id);*/
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EditGroupNonExisting()
        {
            var group = new Group { Id = 1253, Name = "pmp51" };

            this.tests.Storage.EditGroup(group.Id, group);
        }
    }
}