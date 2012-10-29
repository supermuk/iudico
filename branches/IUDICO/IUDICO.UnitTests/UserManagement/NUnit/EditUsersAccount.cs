using System;

using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class EditUsersAccount
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void EditUserValid()
        {
            tests = new UserManagementTests();

            var temp = new User { Username = "name", Email = "ip@interlogic.com.ua", Password = "pass123" };

            this.tests.Storage.CreateUser(temp);

            temp = this.tests.Storage.GetUser("name");

            var expected = new User
                {
                    Username = "name", 
                    Email = "ipp@interlogic.com.ua", 
                    Password = "pass123", 
                    Id = temp.Id, 
                    OpenId = "openid"
                };

            var model = new EditUserModel(expected);

            this.tests.Storage.EditUser(temp.Id, model);

            var compare = this.tests.Storage.GetUser("name");

            Assert.IsTrue(this.tests.TestUsers(compare, expected) && compare.OpenId == expected.OpenId);

            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void EditUserInvalid()
        {
            tests = new UserManagementTests();

            var temp = new User { Username = "name", Email = "ip@interlogic.com.ua", Password = "pass12" };

            this.tests.Storage.CreateUser(temp);

            temp = this.tests.Storage.GetUser(u => u.Username == "name");

            var expected = new User { Username = "name" };

            var model = new EditUserModel(expected);
            
            this.tests.Storage.EditUser(temp.Id, model);

            var compare = this.tests.Storage.GetUser(u => u.Username == "name");

            Assert.IsTrue(this.tests.TestUsers(compare, expected) && compare.OpenId == string.Empty);

            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EditUserNonExisting()
        {
            tests = new UserManagementTests();

            var temp = new User { Username = "name", Email = "ip@interlogic.com.ua", Password = "pass123" };

            var expected = new User
                {
                    Username = "name", 
                    Email = "ipp@interlogic.com.ua", 
                    Password = "pass123", 
                    Id = temp.Id, 
                    OpenId = "openid"
                };

            var model = new EditUserModel(expected);

            this.tests.Storage.EditUser(temp.Id, model);
        }

        [Test]
        public void EditUserValid2()
        {
            tests = new UserManagementTests();

            var temp = new User { Username = "name", Email = "ip@interlogic.com.ua", Password = "pass123" };

            this.tests.Storage.CreateUser(temp);

            temp = this.tests.Storage.GetUser(u => u.Username == "name");

            var expected = new User
                {
                    Username = "name", 
                    Email = "ipp@interlogic.com.ua", 
                    Password = string.Empty, 
                    Id = temp.Id, 
                    OpenId = "openid"
                };

            var model = new EditUserModel(expected);
            this.tests.Storage.EditUser(temp.Id, model);

            var compare = this.tests.Storage.GetUser(u => u.Username == "name");
            Assert.IsTrue(this.tests.TestUsers(compare, expected) && compare.OpenId == expected.OpenId);

            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void EditAccount()
        {
            tests = new UserManagementTests();

            var temp = new User { Username = "name", Email = "ip@interlogic.com.ua", Password = "pass123" };

            this.tests.Storage.CreateUser(temp);

            this.tests.ChangeCurrentUser(temp);

            this.tests.Storage.EditAccount(new EditModel(temp));

            this.tests.ChangeCurrentUser("panza");

            Assert.IsTrue(this.tests.Storage.GetUser(temp.Username) != null);
        }
    }
}