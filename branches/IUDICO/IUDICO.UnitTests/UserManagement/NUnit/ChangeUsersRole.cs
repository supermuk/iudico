using System;
using System.Collections.Generic;
using System.Linq;

using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class ChangeUsersRole
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void AddUserToRole()
        {
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);

            temp = this.tests.Storage.GetUser(u => u.Username == temp.Username);

            const Role Role = Role.Teacher;
            this.tests.Storage.AddUserToRole(Role, temp);

            Assert.IsTrue(this.tests.Storage.GetUserRoles(temp.Username).Contains(Role));

            this.tests.Storage.RemoveUserFromRole(Role, temp);
            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void RemoveUserFromRole()
        {
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);

            const Role Role = Role.Teacher;

            this.tests.Storage.AddUserToRole(Role, temp);
            this.tests.Storage.RemoveUserFromRole(Role, temp);

            Assert.IsFalse(this.tests.Storage.GetUserRoles("name").Contains(Role));

            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void AddExistingUsersToRoles()
        {
            var users = new List<User> {
                    new User { Username = "name1", Email = "mail1@mail.com", Password = "123" }, 
                    new User { Username = "name2", Email = "mail2@mail.com", Password = "123" }, 
                };

            this.tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(
                this.tests.Storage.GetUser(u => u.Username == "panza"));

            foreach (var user in users)
            {
                this.tests.Storage.CreateUser(user);
            }

            var usernames = users.Select(u => u.Username);
            var roles = new List<Role> { Role.Teacher };

            this.tests.Storage.AddUsersToRoles(usernames, roles);

            foreach (var user in users)
            {
                Assert.IsTrue(this.tests.Storage.GetUserRoles(user.Username).Contains(roles.Single()));
            }

            this.tests.Storage.RemoveUsersFromRoles(usernames, roles);

            foreach (var user in users)
            {
                this.tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddNonExistingUsersToRoles()
        {
            var users = new List<User> {
                    new User { Username = "name12", Email = "mail1@mail.com", Password = "123" }, 
                    new User { Username = "name22", Email = "mail2@mail.com", Password = "123" }, 
                };

            var usernames = users.Select(u => u.Username);
            var roles = new List<Role> { Role.Teacher };

            this.tests.Storage.AddUsersToRoles(usernames, roles);
            foreach (var user in users)
            {
                Assert.AreEqual(null, this.tests.Storage.GetUser(u => u.Username == user.Username));
            }

            foreach (var user in users)
            {
                Assert.IsTrue(this.tests.Storage.GetUserRoles(user.Username).Contains(roles.Single()));
            }
        }

        [Test]
        public void RemoveExistingUsersFromRoles()
        {
            var users = new List<User> {
                    new User { Username = "name122", Email = "mail1@mail.com", Password = "123" }, 
                    new User { Username = "name233", Email = "mail2@mail.com", Password = "123" }, 
                };

            foreach (var user in users)
            {
                this.tests.Storage.CreateUser(user);
            }

            var usernames = users.Select(u => u.Username);
            var roles = new List<Role> { Role.Teacher };

            this.tests.Storage.AddUsersToRoles(usernames, roles);
            this.tests.Storage.RemoveUsersFromRoles(usernames, roles);

            foreach (var user in users)
            {
                Assert.IsFalse(this.tests.Storage.GetUserRoles(user.Username).Contains(roles.Single()));
            }

            foreach (var user in users)
            {
                this.tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void RemoveNonExistingUsersFromRoles()
        {
            var users = new List<User> {
                    new User { Username = "name12", Email = "mail1@mail.com", Password = "123" }, 
                    new User { Username = "name22", Email = "mail2@mail.com", Password = "123" }, 
                };

            var usernames = users.Select(u => u.Username);
            var roles = new List<Role> { Role.Teacher };

            this.tests.Storage.AddUsersToRoles(usernames, roles);
            this.tests.Storage.RemoveUsersFromRoles(usernames, roles);

            foreach (var user in users)
            {
                Assert.AreEqual(null, this.tests.Storage.GetUser(u => u.Username == user.Username));
            }

            foreach (var user in users)
            {
                Assert.IsFalse(this.tests.Storage.GetUserRoles(user.Username).Contains(roles.Single()));
            }
        }
    }
}