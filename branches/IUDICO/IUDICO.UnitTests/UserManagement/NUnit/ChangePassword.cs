using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class ChangePassword
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void ChangePasswordCorrect()
        {
            var model = new ChangePasswordModel { OldPassword = "123", ConfirmPassword = "123", NewPassword = "321" };
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.ChangeCurrentUser("panza");
            this.tests.Storage.CreateUser(temp);
            this.tests.ChangeCurrentUser("name");
            this.tests.Storage.ChangePassword(model);

            Assert.AreEqual(
                this.tests.Storage.GetUser(u => u.Username == "name").Password, 
                this.tests.Storage.EncryptPassword("321"));
            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void ChangePasswordIncorrect()
        {
            var model = new ChangePasswordModel { OldPassword = "123", ConfirmPassword = "323", NewPassword = "321" };
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.ChangeCurrentUser("panza");
            this.tests.Storage.CreateUser(temp);
            this.tests.ChangeCurrentUser("name");
            this.tests.Storage.ChangePassword(model);

            Assert.AreEqual(
                this.tests.Storage.GetUser(u => u.Username == "name").Password, 
                this.tests.Storage.EncryptPassword("321"));
            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }
    }
}