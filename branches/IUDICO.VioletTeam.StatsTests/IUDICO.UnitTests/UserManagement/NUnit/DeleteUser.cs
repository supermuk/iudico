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
        public void DeleteUserExisting()
        {
            User temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };
            
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            
            _Tests.Storage.CreateUser(temp);
            _Tests.Storage.DeleteUser(u => u.Username == "name");

            Assert.IsTrue(_Tests.Storage.GetUser(u => u.Username == "name") == null);
        }
        [Test]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void DeleteUserNonExisting()
        {
            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }
    }
}
