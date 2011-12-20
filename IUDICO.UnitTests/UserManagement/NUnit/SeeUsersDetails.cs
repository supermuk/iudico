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
        public void GetUserExisting()
        {
            User temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };
            
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            Assert.AreEqual(temp, _Tests.Storage.GetUser(u => u.Username == "name"));

            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }
        [Test]
        public void GetUserNonExisting()
        {
            Assert.AreEqual(null, _Tests.Storage.GetUser(u => u.Username == "name"));
        }
    }
}
