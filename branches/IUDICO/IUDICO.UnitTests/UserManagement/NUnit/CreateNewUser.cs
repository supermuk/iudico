using System;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class CreateNewUser
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void CreateUserValid()
        {
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);

            var expected = this.tests.Storage.GetUser(u => u.Username == "name");

            Assert.IsTrue(temp.Username == expected.Username && temp.Email == expected.Email);

            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateUserInvalid()
        {
            User temp = new User { Username = "name", Email = "mail@mail.com" };

            this.tests.Storage.CreateUser(temp);
        }

        [Test]
        public void EncryptPassword()
        {
            Assert.AreEqual(this.tests.Storage.EncryptPassword("Sha"), "BA79BAEB9F10896A46AE74715271B7F586E74640");
        }

        [Test]
        public void CreateDuplicateUser()
        {
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);
            
            var temp2 = new User { Username = "name", Email = "mail2@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);

            var expected = this.tests.Storage.GetUser(u => u.Username == "name");

            Assert.IsTrue("mail@mail.com" == expected.Email);

            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }
    }
}