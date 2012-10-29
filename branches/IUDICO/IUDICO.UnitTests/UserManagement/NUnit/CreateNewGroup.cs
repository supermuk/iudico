using IUDICO.Common.Models.Shared;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class CreateNewGroup
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void CreateGroupValid()
        {
            tests = UserManagementTests.Update();

            var group = new Group { Id = 12578, Name = "pmp41" };

            this.tests.Storage.CreateGroup(group);

            Assert.IsTrue(
                group.Name == this.tests.Storage.GetGroup(group.Id).Name
                && group.Id == this.tests.Storage.GetGroup(group.Id).Id);

            this.tests.Storage.DeleteGroup(group.Id);
        }

        [Test]
        public void CreateGroupInvalid()
        {
            tests = UserManagementTests.Update();

            var group = new Group { Id = 4567, Name = "pma51" };

            this.tests.Storage.CreateGroup(group);

            Assert.IsTrue(
                group.Name == this.tests.Storage.GetGroup(group.Id).Name
                && group.Id == this.tests.Storage.GetGroup(group.Id).Id);

            this.tests.Storage.DeleteGroup(group.Id);
        }
    }
}