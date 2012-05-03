using System.Collections.Generic;

using IUDICO.Common.Models.Shared;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class SeeGroupsDetails
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void GetGroupExisting()
        {
            var group = new Group { Id = 12, Name = "pmi31" };

            this.tests.Storage.CreateGroup(group);

            Assert.AreEqual(group, this.tests.Storage.GetGroup(group.Id));

            this.tests.Storage.DeleteGroup(group.Id);
        }

        [Test]
        public void GetGroupNonExisting()
        {
            Assert.AreEqual(null, this.tests.Storage.GetGroup(123));
        }
    }
}