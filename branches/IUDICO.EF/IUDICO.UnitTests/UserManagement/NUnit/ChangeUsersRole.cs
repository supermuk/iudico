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
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        [Test]
        public void AddUserToRole()
        {
            User temp = new User {Username = "name", Email = "mail@mail.com", Password = "123"};

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            temp = _Tests.Storage.GetUser(u => u.Username == temp.Username);

            var role = Role.Teacher;
            _Tests.Storage.AddUserToRole(role, temp);

            Assert.IsTrue(_Tests.Storage.GetUserRoles(temp.Username).Contains(role));

            _Tests.Storage.RemoveUserFromRole(role, temp);
            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void RemoveUserFromRole()
        {
            User temp = new User {Username = "name", Email = "mail@mail.com", Password = "123"};

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            var role = Role.Teacher;

            _Tests.Storage.AddUserToRole(role, temp);
            _Tests.Storage.RemoveUserFromRole(role, temp);

            Assert.IsFalse(_Tests.Storage.GetUserRoles("name").Contains(role));

            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        public void AddExistingUsersToRoles()
        {
            var users = new List<User>
                            {
                                new User {Username = "name1", Email = "mail1@mail.com", Password = "123"},
                                new User {Username = "name2", Email = "mail2@mail.com", Password = "123"},
                            };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            foreach (var user in users)
            {
                _Tests.Storage.CreateUser(user);
            }

            var usernames = users.Select(u => u.Username);
            var roles = new List<Role> {Role.Teacher};

            _Tests.Storage.AddUsersToRoles(usernames, roles);

            foreach (var user in users)
            {
                Assert.IsTrue(_Tests.Storage.GetUserRoles(user.Username).Contains(roles.Single()));
            }

            _Tests.Storage.RemoveUsersFromRoles(usernames, roles);
            foreach (var user in users)
            {
                _Tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }

        [Test]
        [ExpectedException(typeof (NullReferenceException))]
        public void AddNonExistingUsersToRoles()
        {
            var users = new List<User>
                            {
                                new User {Username = "name12", Email = "mail1@mail.com", Password = "123"},
                                new User {Username = "name22", Email = "mail2@mail.com", Password = "123"},
                            };

            var usernames = users.Select(u => u.Username);
            var roles = new List<Role> {Role.Teacher};

            _Tests.Storage.AddUsersToRoles(usernames, roles);
            foreach (var user in users)
            {
                Assert.AreEqual(null, _Tests.Storage.GetUser(u => u.Username == user.Username));
            }
            foreach (var user in users)
            {
                Assert.IsTrue(_Tests.Storage.GetUserRoles(user.Username).Contains(roles.Single()));
            }
        }

        [Test]
        public void RemoveExistingUsersFromRoles()
        {
            var users = new List<User>
                            {
                                new User {Username = "name122", Email = "mail1@mail.com", Password = "123"},
                                new User {Username = "name233", Email = "mail2@mail.com", Password = "123"},
                            };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            foreach (var user in users)
            {
                _Tests.Storage.CreateUser(user);
            }

            var usernames = users.Select(u => u.Username);
            var roles = new List<Role> {Role.Teacher};

            _Tests.Storage.AddUsersToRoles(usernames, roles);
            _Tests.Storage.RemoveUsersFromRoles(usernames, roles);

            foreach (var user in users)
            {
                Assert.IsFalse(_Tests.Storage.GetUserRoles(user.Username).Contains(roles.Single()));
            }
            foreach (var user in users)
            {
                _Tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }

        [Test]
        [ExpectedException(typeof (NullReferenceException))]
        public void RemoveNonExistingUsersFromRoles()
        {
            var users = new List<User>
                            {
                                new User {Username = "name12", Email = "mail1@mail.com", Password = "123"},
                                new User {Username = "name22", Email = "mail2@mail.com", Password = "123"},
                            };

            var usernames = users.Select(u => u.Username);
            var roles = new List<Role> {Role.Teacher};

            _Tests.Storage.AddUsersToRoles(usernames, roles);
            _Tests.Storage.RemoveUsersFromRoles(usernames, roles);
            foreach (var user in users)
            {
                Assert.AreEqual(null, _Tests.Storage.GetUser(u => u.Username == user.Username));
            }
            foreach (var user in users)
            {
                Assert.IsFalse(_Tests.Storage.GetUserRoles(user.Username).Contains(roles.Single()));
            }
        }
    }
}