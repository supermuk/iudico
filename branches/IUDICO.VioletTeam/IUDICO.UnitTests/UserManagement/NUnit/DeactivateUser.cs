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
    public class DeactivateUser
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        [Test]
        public void DeactivateUsert()
        {
            User temp = new User { Username = "ipke", Email = "ip@interlogic.com.ua", Password = "pass123" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            System.Guid gg = _Tests.Storage.GetUser(u => u.Username == "ipke").Id;
            _Tests.Storage.DeactivateUser(gg);
            Assert.AreEqual(_Tests.Storage.GetUser(u => u.Username == "ipke").IsApproved, false);
        }
    }
}