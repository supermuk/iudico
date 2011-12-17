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
    public class EditAccount
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        [Test]
        public void EditAccountt()
        {
            /*
            User temp = new User { Username = "iipe", Email = "ip@interlogic.com.ua", Password = "pass123",  OpenId = "openid", UserId = "userid"};
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            User expected = new User { Username = "ipee", Email = "ip@interlogic.com.ua", Password = "pass123", Id = temp.Id, OpenId = "openid", UserId = "userid" };
            var model = new EditModel(expected);
            _Tests.Storage.EditAccount(model);
            _Tests.MockDataContext.Verify(d => d.SubmitChanges());
            Assert.AreEqual("ipee", _Tests.Storage.GetUser(u => u.Username == "ipee").Name);*/
        }
    }
}
