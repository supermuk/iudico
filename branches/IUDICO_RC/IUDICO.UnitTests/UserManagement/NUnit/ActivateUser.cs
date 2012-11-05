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

            tests = UserManagementTests.Update();
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);

            var gg = this.tests.Storage.GetUser("name").Id;

            this.tests.Storage.ActivateUser(gg);

            Assert.AreEqual(this.tests.Storage.GetUser("name").IsApproved, true);
            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ActivateUserNonExisting()
        {
            tests = UserManagementTests.Update();

            var gg = Guid.NewGuid();

            this.tests.Storage.ActivateUser(gg);
        }
    }
}