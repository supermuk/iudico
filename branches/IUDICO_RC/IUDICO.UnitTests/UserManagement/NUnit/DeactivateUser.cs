using System;

using IUDICO.Common.Models.Shared;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class DeactivateUser
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void DeactivateUserExisting()
        {
            tests = new UserManagementTests();
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);

            var gg = this.tests.Storage.GetUser(u => u.Username == "name").Id;

            this.tests.Storage.ActivateUser(gg);
            this.tests.Storage.DeactivateUser(gg);

            Assert.AreEqual(this.tests.Storage.GetUser(u => u.Username == "name").IsApproved, false);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeactivateUserNonExisting()
        {
            var gg = Guid.NewGuid();

            this.tests.Storage.DeactivateUser(gg);
        }
    }
}