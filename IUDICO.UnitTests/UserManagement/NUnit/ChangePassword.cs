using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class ChangePassword
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();


        [Test]
        public void ChangePasswordCorrect()
        {
            var model = new ChangePasswordModel {OldPassword = "123", ConfirmPassword = "123", NewPassword = "321"};
            User temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "name"));
            _Tests.Storage.ChangePassword(model);
            Assert.AreEqual(_Tests.Storage.GetUser(u => u.Username == "name").Password, _Tests.Storage.EncryptPassword("321"));
            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }
        [Test]
        public void ChangePasswordIncorrect()
        {
            var model = new ChangePasswordModel { OldPassword = "123", ConfirmPassword = "323", NewPassword = "321" };
            User temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "name"));
            _Tests.Storage.ChangePassword(model);
            Assert.AreEqual(_Tests.Storage.GetUser(u => u.Username == "name").Password, _Tests.Storage.EncryptPassword("321"));
            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }
    }
}
