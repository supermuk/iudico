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


        [Test]
        public void GetUserExisting()
        {
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);

           var user = this.tests.Storage.GetUser(u => u.Username == "name");
            Assert.AreEqual(temp.Username, user.Username);
            Assert.AreEqual(temp.Email, user.Email);
            Assert.AreEqual(temp.Password, user.Password);

            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void GetUserNonExisting()
        {
            Assert.AreEqual(null, this.tests.Storage.GetUser(u => u.Username == "name"));
        }
    }
}