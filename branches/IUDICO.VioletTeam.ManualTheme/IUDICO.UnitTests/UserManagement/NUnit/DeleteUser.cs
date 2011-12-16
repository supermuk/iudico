using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class DeleteUser
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();
        
        [Test]
        public void DeleteUserTrue()
        {
            User temp = new User { Username = "ipel", Email = "ip@interlogic.com.ua", Password = "pass123" };
            
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            
            _Tests.Storage.CreateUser(temp);
            _Tests.Storage.DeleteUser(u => u.Username == "ipel");

            Assert.IsTrue(_Tests.Storage.GetUser(u => u.Username == "ipel") == null);
        }
    }
}
