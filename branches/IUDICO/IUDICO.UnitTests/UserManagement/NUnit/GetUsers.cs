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
        public void GetUsersSuccess()
        {
            var users = new List<User>
                            {
                                new User {Username = "ipex", Email = "ip@interlogic.com.ua", Password = "asd"},
                                new User {Username = "vladykx", Email = "vladykx@gmail.com", Password = "asd"},
                            };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            
            foreach (var user in users)
            {
                _Tests.Storage.CreateUser(user);
            }

            Assert.IsTrue(_Tests.TestUsers(_Tests.Storage.GetUsers(), users));
        }
    }
}