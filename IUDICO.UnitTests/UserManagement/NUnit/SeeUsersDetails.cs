using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class SeeUsersDetails
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();
        
        [Test]
        public void GetUser()
        {
            User temp = new User { Username = "iper", Email = "ip@interlogic.com.ua", Password = "pass123" };
            
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            Assert.IsTrue(_Tests.TestUsers(temp, _Tests.Storage.GetUser(u => u.Username == "iper")));
        }
    }
}
