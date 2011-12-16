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
        public void EditUser()
        {
            User temp = new User { Username = "iphe", Email = "ip@interlogic.com.ua", Password = "pass123" };
            
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            temp = _Tests.Storage.GetUser(u => u.Username == "iphe");

            User expected = new User { Username = "ipvep", Email = "ipp@interlogic.com.ua", Password = "pass123", Id = temp.Id, OpenId = "openid" };

            var model = new EditUserModel(expected);
            _Tests.Storage.EditUser(temp.Id, model);

            var compare = _Tests.Storage.GetUser(u => u.Username == "ipvep");

            Assert.IsTrue(_Tests.TestUsers(compare, expected));
        }
    }
}
