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
    public class EditUsersAccount
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        [Test]
        public void EditUser()
        {
            User temp = new User { Username = "iphe", Email = "ip@interlogic.com.ua", Password = "pass123" };
            User expected = new User { Username = "ipvep", Email = "ipp@interlogic.com.ua", Password = "pass123", Id = temp.Id , OpenId = "openid" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            var model = new EditUserModel(expected);
            _Tests.Storage.EditUser(temp.Id, model);
            //Assert.AreEqual(expected, _Tests.Storage.GetUser(u => u.Username == "ipvep"));
        }
    }
}
