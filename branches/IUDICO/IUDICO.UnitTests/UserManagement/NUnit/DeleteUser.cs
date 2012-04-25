using System;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class DeleteUser
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void DeleteUserExisting()
        {
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);
            this.tests.Storage.DeleteUser(u => u.Username == "name");

            Assert.IsTrue(this.tests.Storage.GetUser(u => u.Username == "name") == null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteUserNonExisting()
        {
            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }
    }
}