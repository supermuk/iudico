using System;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class DeleteGroup
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void DeleteGroupExisting()
        {
            var group = new Group { Id = 123, Name = "pmi31" };

            this.tests.Storage.CreateGroup(group);
            this.tests.Storage.DeleteGroup(group.Id);

            Assert.IsTrue(this.tests.Storage.GetGroup(group.Id) == null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteGroupNonExisting()
        {
            this.tests.Storage.DeleteGroup(123);
        }
    }
}