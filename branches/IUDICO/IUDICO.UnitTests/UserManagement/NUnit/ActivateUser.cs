using System;

using IUDICO.Common.Models.Shared;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class ActivateUser
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void ActivateUserExisting()
        {
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);

            var gg = this.tests.Storage.GetUser(u => u.Username == "name").Id;

            this.tests.Storage.ActivateUser(gg);

            Assert.AreEqual(this.tests.Storage.GetUser(u => u.Username == "name").IsApproved, true);

            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ActivateUserNonExisting()
        {
            var gg = Guid.NewGuid();

            this.tests.Storage.ActivateUser(gg);
        }
    }
}