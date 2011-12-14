﻿using System;
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
    public class CreateNewUser
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();
        [Test]
        public void CreateUser()
        {
            User temp = new User { Username = "ipep", Email = "ip@interlogic.com.ua", Password = "pass123" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            User expected = _Tests.Storage.GetUser(u => u.Username == "ipep");
            Assert.AreEqual(temp, expected);
        }
        protected class UserComparer : IEqualityComparer<User>
        {
            #region Implementation of IEqualityComparer<in User>

            public bool Equals(User x, User y)
            {
                return x.Username == y.Username && x.Email == y.Email;
            }

            public int GetHashCode(User obj)
            {
                return (obj.Username + obj.Email).GetHashCode();
            }

            #endregion
        }
    }
}
