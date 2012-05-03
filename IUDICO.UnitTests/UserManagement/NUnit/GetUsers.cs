using System.Collections.Generic;

using IUDICO.Common.Models.Shared;

using NUnit.Framework;

using System.Linq;

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

        [Test]
        public void GetListOfUsersPage()
        {
            var users = new List<User> {
                    new User { Username = "name1", Email = "mail1@mail.com", Password = "123" }, 
                    new User { Username = "name2", Email = "mail2@mail.com", Password = "321" }, 
                    new User { Username = "name3", Email = "mail3@mail.com", Password = "321" }, 
                    new User { Username = "name4", Email = "mail4@mail.com", Password = "321" }, 
                };

            foreach (var user in users)
            {
                this.tests.Storage.CreateUser(user);
            }

            var usersSkiped = this.tests.Storage.GetUsers().Skip(1).Take(2);
            var usersPaged = this.tests.Storage.GetUsers(1, 2);

            Assert.IsTrue(this.tests.TestUsers(usersSkiped, usersPaged));
        }
    }
}