using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class GetUsers
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();
        
        [Test]
        public void GetListOfUsers()
        {
            var users = new List<User>
                            {
                                new User {Username = "name1", Email = "mail1@mail.com", Password = "123"},
                                new User {Username = "name2", Email = "mail2@mail.com", Password = "321"},
                            };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            
            foreach (var user in users)
            {
                _Tests.Storage.CreateUser(user);
            }

            Assert.IsTrue(_Tests.TestUsers(_Tests.Storage.GetUsers(), users));
            foreach (var user in users)
            {
                _Tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }
    }
}