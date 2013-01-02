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

        /// <summary>
        /// Fixed - Yarema Kipetskiy
        /// </summary>
        [Test]
        public void RestorePasswordExistingTest()
        {
            var model = new RestorePasswordModel {Username = "panza", Email = "ipetrovych@gmail.com" };
            // Getting old password.
            var password = this.tests.DataContext.Users.Single(u => u.Username == "panza").Password;

            this.tests.Storage.RestorePassword(model);

            // Getting new password.
            var newpassword = this.tests.DataContext.Users.Single(u => u.Username == "panza").Password;

            // Verifying if new and old passwords are not equal.
            Assert.AreNotEqual(newpassword, password);
        }

        /// <summary>
        /// Fixed - Yarema Kipetskiy
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void RestorePasswordNonExistingTest()
        {
            var model = new RestorePasswordModel { Username = "unknown", Email = "mail@mail.com" };
            
            // Trying to restore password of non existing user.
            this.tests.Storage.RestorePassword(model);
        }
    }
}