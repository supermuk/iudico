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
            User temp = new User { Username = "icpe", Email = "ip@interlogic.com.ua", Password = "pass123" };
            
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            temp = _Tests.Storage.GetUser(u => u.Username == temp.Username);
            
            var role = Role.Teacher;
            _Tests.Storage.AddUserToRole(role, temp);

            Assert.IsTrue(_Tests.Storage.GetUserRoles(temp.Username).Contains(role));

            _Tests.Storage.RemoveUserFromRole(role, temp);
        }

        [Test]
        public void RemoveUserFromRole()
        {
            User temp = new User { Username = "izpe", Email = "ip@interlogic.com.ua", Password = "pass123" };
            
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            
            var role = Role.Teacher;

            _Tests.Storage.AddUserToRole(role, temp);
            _Tests.Storage.RemoveUserFromRole(role, temp);
            
            Assert.IsFalse(_Tests.Storage.GetUserRoles("ipe").Contains(role));
        }

        [Test]
        public void AddUsersToRoles()
        {
            var users = new List<User>
                            {
                                new User {Username = "idpe", Email = "ip@interlogic.com.ua", Password = "asd"},
                                new User {Username = "vladykx", Email = "vladykx@gmail.com",Password = "asd"},
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
        }

        [Test]
        public void RemoveUsersFromRoles()
        {

        }
    }
}