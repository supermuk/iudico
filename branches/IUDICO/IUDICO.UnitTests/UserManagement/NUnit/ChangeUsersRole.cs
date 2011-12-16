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
            List<Role> roles = new List<Role>();
            roles.Add(role);
            //Assert.AreEqual(roles.First(), _Tests.Storage.GetUserRoles("ipe"));
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

            var user_names = new List<string>();

            foreach (var user in users)
            {
                user_names.Add(user.Username);
            }

            var roles = new List<Role>();
            
            roles.Add(Role.Teacher);
            
            _Tests.Storage.AddUsersToRoles(user_names, roles);
            
            var expected_users = _Tests.Storage.GetUsers().ToList();
            /*
            foreach (var expectedUser in expected_users)
            {
                Assert.AreEqual(roles,_Tests.Storage.GetUserRoles(expectedUser.Username));
            }*/

            _Tests.Storage.RemoveUsersFromRoles(user_names, roles);
        }

        [Test]
        public void RemoveUsersFromRoles()
        {

        }
    }
}