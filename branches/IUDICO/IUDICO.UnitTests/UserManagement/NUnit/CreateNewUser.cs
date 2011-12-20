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
        [Test]
        public void EncryptPassword()
        {
            Assert.AreEqual(_Tests.Storage.EncryptPassword("Sha"), "BA79BAEB9F10896A46AE74715271B7F586E74640");
            
        }
        [Test]
        public void CreateDuplicateUser()
        {
            User temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            User temp2 = new User { Username = "name", Email = "mail2@mail.com", Password = "123" };
            _Tests.Storage.CreateUser(temp);

            User expected = _Tests.Storage.GetUser(u => u.Username == "name");

            Assert.IsTrue("mail@mail.com" == expected.Email);

            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }
    }
}
