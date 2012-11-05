using System;
using System.Linq;

using IUDICO.UserManagement.Models;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    internal class RestorePassword
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void RestorePasswordExisting()
        {
            var model = new RestorePasswordModel {Username = "panza", Email = "ipetrovych@gmail.com" };
            var password = this.tests.DataContext.Users.Single(u => u.Username == "panza").Password;

            this.tests.Storage.RestorePassword(model);

            var newpassword = this.tests.DataContext.Users.Single(u => u.Username == "panza").Password;

            Assert.IsTrue(newpassword != password);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void RestorePasswordNonExisting()
        {
           var model = new RestorePasswordModel { Username = "unknown", Email = "mail@mail.com" };

            this.tests.Storage.RestorePassword(model);
        }
    }
}