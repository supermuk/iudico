using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.UserManagement.Models;
using IUDICO.Common.Models.Notifications;
using Moq;
using NUnit.Framework;
using IUDICO.Common.Models.Shared;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class CreateMultiple
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        protected class UserComparer : IEqualityComparer<User>
        {
            #region Implementation of IEqualityComparer<in User>

            public bool Equals(User x, User y)
            {
                return x.Username == y.Username && x.Email == y.Email;
            }

            public int GetHashCode(User obj)
            {
                return (obj.Username + obj.Email).GetHashCode();
            }

            #endregion
        }
        protected bool TestUsers(IEnumerable<User> users, IEnumerable<User> inserted)
        {
            return inserted.Except(users, new UserComparer()).Count() == 0;
        }
        [Test]
        public void CreateUsersFromCSV()
        {
            _Tests = UserManagementTests.GetInstance();
            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            var text = "Username,Email\nipe,ip@interlogic.com.ua\nvladykx,vladykx@gmail.com";

            File.WriteAllText(path, text);

            var users = new List<User>
                            {
                                new User {Username = "ipe", Email = "ip@interlogic.com.ua"},
                                new User {Username = "vladykx", Email = "vladykx@gmail.com"},
                            };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            var results = _Tests.Storage.CreateUsersFromCSV(path);

            Assert.IsTrue(TestUsers(_Tests.Storage.GetUsers(), users));
            Assert.IsTrue(_Tests.Storage.GetUser(u => u.Username == "ipe").Username == "ipe");

            /*_Tests.Users.Verify(u => u.InsertAllOnSubmit(It.Is<IEnumerable<User>>(ie => TestUsers(ie, users))));*/
            _Tests.MockStorage.Verify(u => u.SendEmail(It.IsAny<string>(), It.Is<string>(s => s == "ip@interlogic.com.ua"), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _Tests.MockStorage.Verify(u => u.SendEmail(It.IsAny<string>(), It.Is<string>(s => s == "vladykx@gmail.com"), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _Tests.MockLmsService.Verify(s => s.Inform(UserNotifications.UserCreateMultiple, It.Is<IEnumerable<User>>(ie => TestUsers(ie, users))));
        }

        [Test]
        public void CreateUser()
        {
            User temp = new User { Username = "ipe", Email = "ip@interlogic.com.ua", Password = "pass123" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            User expected = _Tests.Storage.GetUser(u => u.Username == "ipe");
            Assert.AreEqual(temp, expected);
        }
        [Test]
        public void DeleteUser()
        {
            User temp = new User { Username = "ipe", Email = "ip@interlogic.com.ua", Password = "pass123" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            _Tests.Storage.DeleteUser(u => u.Username == "ipe");
            User expected = _Tests.Storage.GetUser(u => u.Username == "ipe");
            Assert.IsTrue(temp.Deleted);
        }
        [Test]
        public void GetUser()
        {
            User temp = new User { Username = "ipe", Email = "ip@interlogic.com.ua", Password = "pass123" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            Assert.AreEqual(temp, _Tests.Storage.GetUser(u => u.Username == "ipe"));
        }
        [Test]
        public void GetUsers()
        {
            var users = new List<User>
                            {
                                new User {Username = "ipe", Email = "ip@interlogic.com.ua", Password = "asd"},
                                new User {Username = "vladykx", Email = "vladykx@gmail.com",Password = "asd"},
                            };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            foreach (var user in users)
            {
                _Tests.Storage.CreateUser(user);
            }
            Assert.IsTrue(TestUsers(_Tests.Storage.GetUsers(), users));
        }
        [Test]
        public void ActivateUser()
        {
            System.Guid g = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            User temp = new User { Username = "ipe", Email = "ip@interlogic.com.ua", Password = "pass123", Id = g };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            System.Guid gg = _Tests.Storage.GetUser(u => u.Username == "ipe").Id;
            _Tests.Storage.ActivateUser(gg);
            Assert.AreEqual(_Tests.Storage.GetUser(u => u.Username == "ipe").IsApproved, true);
        }
        [Test]
        public void DeactivateUser()
        {
            System.Guid g = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            User temp = new User { Username = "ipe", Email = "ip@interlogic.com.ua", Password = "pass123", Id = g };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            System.Guid gg = _Tests.Storage.GetUser(u => u.Username == "ipe").Id;
            _Tests.Storage.DeactivateUser(gg);
            Assert.AreEqual(_Tests.Storage.GetUser(u => u.Username == "ipe").IsApproved, false);
        }
        [Test]
        public void EditUser()
        {
            System.Guid g = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            User temp = new User { Username = "ipe", Email = "ip@interlogic.com.ua", Password = "pass123", Id = g };
            User expected = new User { Username = "ipep", Email = "ipp@interlogic.com.ua", Password = "pass123", Id = g, OpenId = "openid" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            var model = new EditUserModel(expected);
            _Tests.Storage.EditUser(g, model);

            //Assert.AreEqual(expected, _Tests.Storage.GetUser(u => u.Username == "ipep"));
        }
        [Test]
        public void AddUserToRole()
        {
            User temp = new User { Username = "ipe", Email = "ip@interlogic.com.ua", Password = "pass123" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            var role = Role.Teacher;
            _Tests.Storage.AddUserToRole(role, temp);
            List<Role> roles = new List<Role>();
            roles.Add(role);
            Assert.AreEqual(roles, _Tests.Storage.GetUser(u => u.Username == "ipe").UserRoles);
        }
        [Test]
        public void RemoveUserFromRole()
        {
            User temp = new User { Username = "ipe", Email = "ip@interlogic.com.ua", Password = "pass123" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            var role = Role.Teacher;
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
                                new User {Username = "ipe", Email = "ip@interlogic.com.ua", Password = "asd"},
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
        }
        [Test]
        public void RemoveUsersFromRoles()
        {

        }
        [Test]
        public void RegisterUser()
        {
            var model = new RegisterModel
            {
                Username = "nestor",
                Password = "1234",
                ConfirmPassword = "1234",
                Email = "yn@gmail.com",
                Name = "Nestor"
            };
            _Tests.Storage.RegisterUser(model);

            User temp = new User { Username = "nestor", Email = "yn@gmail.com", Password = "1234", Name = "Nestor" };
            // Assert.AreEqual(temp, _Tests.Storage.GetUser(u => u.Username == "nestor"));
        }
        [Test]
        public void EditAccount()
        {
            /*
            System.Guid g = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            User temp = new User { Username = "ipe", Email = "ip@interlogic.com.ua", Password = "pass123", Id = g, OpenId = "openid", UserID = "userid" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            User expected = new User { Username = "ipee", Email = "ip@interlogic.com.ua", Password = "pass123", Id = _Tests.Storage.GetUser(u => u.Username == "ipe").Id, OpenId = "openid", UserID = "userid" };
            var model = new EditModel(expected);
            _Tests.Storage.EditAccount(model);
            _Tests.MockDataContext.Verify(d => d.SubmitChanges());
            Assert.AreEqual("ipee", _Tests.Storage.GetUser(u => u.Username == "ipe").Name);*/
        }
        [Test]
        public void ChangePassword()
        {

        }
        [Test]
        public void CreateGroup()
        {

        }
        [Test]
        public void EditGroup()
        {

        }
        [Test]
        public void DeleteGroup()
        {

        }
        [Test]
        public void GetGroup()
        {

        }
        [Test]
        public void GetGroups()
        {

        }
        [Test]
        public void GetGroupsAvailableToUser()
        {

        }
        [Test]
        public void AddUserToGroup()
        {

        }
        [Test]
        public void RemoveUserFromGroup()
        {

        }
    }
}
