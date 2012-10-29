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
            tests = new UserManagementTests();
            var temp = new User { Username = "Username", Email = "usermail@mail.com", Password = "123" };

            
            this.tests.Storage.CreateUser(temp);
            this.tests.Storage.DeleteUser(u => u.Username == "Username");

            Assert.IsTrue(this.tests.Storage.GetUser(u => u.Username == "Username") == null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteUserNonExisting()
        {
            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }
    }
}