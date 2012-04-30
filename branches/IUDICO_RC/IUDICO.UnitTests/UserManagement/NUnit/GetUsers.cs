using System.Collections.Generic;

using IUDICO.Common.Models.Shared;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class GetUsers
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void GetListOfUsers()
        {
            var users = new List<User> {
                    new User { Username = "name1", Email = "mail1@mail.com", Password = "123" }, 
                    new User { Username = "name2", Email = "mail2@mail.com", Password = "321" }, 
                };

            foreach (var user in users)
            {
                this.tests.Storage.CreateUser(user);
            }

            Assert.IsTrue(this.tests.TestUsers(this.tests.Storage.GetUsers(), users));

            foreach (var user in users)
            {
                this.tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }
    }
}