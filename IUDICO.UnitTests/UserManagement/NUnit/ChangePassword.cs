using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.UserManagement.Models;
using IUDICO.Common.Models.Notifications;
using Moq;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class ChangePassword
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();


        [Test]
        public void ChangePasswordSuccess()
        {
            var model = new ChangePasswordModel {OldPassword = "123", ConfirmPassword = "123", NewPassword = "321"};
            User temp = new User { Username = "ipepp", Email = "ip@interlogic.com.ua", Password = "123" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "ipepp"));
            _Tests.Storage.ChangePassword(model);
            Assert.AreEqual(_Tests.Storage.GetUser(u => u.Username == "ipepp").Password, _Tests.Storage.EncryptPassword("321"));
        }
    }
}
