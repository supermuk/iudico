﻿using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class CreateNewUser
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();
        
        [Test]
        public void CreateUserValid()
        {
            User temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };
            
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            
            User expected = _Tests.Storage.GetUser(u => u.Username == "name");
            
            Assert.IsTrue(temp.Username == expected.Username && temp.Email == expected.Email);

            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }
        [Test]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void CreateUserInvalid()
        {
            User temp = new User { Username = "name", Email = "mail@mail.com"};

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            
        }
    }
}