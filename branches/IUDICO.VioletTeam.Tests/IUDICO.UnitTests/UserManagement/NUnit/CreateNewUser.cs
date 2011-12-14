using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
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
            
            Assert.IsTrue(temp.Username == expected.Username && temp.Email == expected.Email);
        }
    }
}
