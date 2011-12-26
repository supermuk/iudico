using System;
using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class EditUsersAccount
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        [Test]
        public void EditUserValid()
        {
            User temp = new User { Username = "name", Email = "ip@interlogic.com.ua", Password = "pass123" };
            
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            temp = _Tests.Storage.GetUser(u => u.Username == "name");

            User expected = new User { Username = "name", Email = "ipp@interlogic.com.ua", Password = "pass123", Id = temp.Id, OpenId = "openid" };

            var model = new EditUserModel(expected);
            _Tests.Storage.EditUser(temp.Id, model);

            var compare = _Tests.Storage.GetUser(u => u.Username == "name");
            Assert.IsTrue(_Tests.TestUsers(compare, expected) && compare.OpenId == expected.OpenId);

            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }
        [Test]
        public void EditUserInvalid()
        {
            //Done by Selenium test.
            /*
            User temp = new User { Username = "name", Email = "ip@interlogic.com.ua", Password = "pass12" };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            temp = _Tests.Storage.GetUser(u => u.Username == "name");

            User expected = new User { Username = "name" };

            var model = new EditUserModel(expected);
            _Tests.Storage.EditUser(temp.Id, model);

            var compare = _Tests.Storage.GetUser(u => u.Username == "name");
            Assert.IsTrue(_Tests.TestUsers(compare, expected) && compare.OpenId == expected.OpenId);

            _Tests.Storage.DeleteUser(u => u.Username == "name");*/
        }
        [Test]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void EditUserNonExisting()
        {
            User temp = new User { Username = "name", Email = "ip@interlogic.com.ua", Password = "pass123" };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            User expected = new User { Username = "name", Email = "ipp@interlogic.com.ua", Password = "pass123", Id = temp.Id, OpenId = "openid" };

            var model = new EditUserModel(expected);
            _Tests.Storage.EditUser(temp.Id, model);
        }
        [Test]
        public void EditUserValid2()
        {
            User temp = new User { Username = "name", Email = "ip@interlogic.com.ua", Password = "pass123" };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            temp = _Tests.Storage.GetUser(u => u.Username == "name");

            User expected = new User { Username = "name", Email = "ipp@interlogic.com.ua", Password = "", Id = temp.Id, OpenId = "openid" };

            var model = new EditUserModel(expected);
            _Tests.Storage.EditUser(temp.Id, model);

            var compare = _Tests.Storage.GetUser(u => u.Username == "name");
            Assert.IsTrue(_Tests.TestUsers(compare, expected) && compare.OpenId == expected.OpenId);

            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }
    }
}
