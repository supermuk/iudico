using IUDICO.Common.Models.Shared;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class SeeUsersDetails
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        public void SetUp()
        {
            this.tests = new UserManagementTests();
        }

        /// <summary>
        /// Fixed - Yarema Kipetskiy
        /// </summary>
        [Test]
        public void GetUserExistingTest()
        {
            // Creating new user.
            var temp = new User { Username = "name1", Email = "mail@mail.com", Password = "123" };
            this.tests.Storage.CreateUser(temp);

            // Getting user data of created user from storage.
            var user = this.tests.Storage.GetUser(u => u.Username == "name1");
            // Verifying if user data is correct.
            Assert.AreEqual(temp.Username, user.Username);
            Assert.AreEqual(temp.Email, user.Email);
            Assert.AreEqual(temp.Password, user.Password);

            this.tests.Storage.DeleteUser(u => u.Username == "name1");
        }

        [Test]
        public void GetUserNonExisting()
        {
            Assert.AreEqual(null, this.tests.Storage.GetUser(u => u.Username == "name"));
        }
    }
}