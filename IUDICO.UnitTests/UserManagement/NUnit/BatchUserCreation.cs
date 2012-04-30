using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class BatchUserCreation
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void CreateUsersWithValidFile()
        {
            this.tests = UserManagementTests.GetInstance();

            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            const string Text = "Username,Email\nipe,ip@interlogic.com.ua\nvladykx,vladykx@gmail.com";

            File.WriteAllText(path, Text);

            var users = new List<User> {
                    new User { Username = "ipe", Email = "ip@interlogic.com.ua" }, 
                    new User { Username = "vladykx", Email = "vladykx@gmail.com" }, 
                };

            this.tests.Storage.CreateUsersFromCSV(path);

            Assert.IsTrue(this.tests.TestUsers(this.tests.Storage.GetUsers(), users));

            foreach (var user in users)
            {
                this.tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateUsersWithInValidFile()
        {
            this.tests = UserManagementTests.GetInstance();

            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            const string Text = "UsernamEail\nipe,ip@interlogic.com.ua\nvladykx,vladykx@gmail.com";

            File.WriteAllText(path, Text);
            this.tests.Storage.CreateUsersFromCSV(path);
        }

        [Test]
        public void CreateUsersWithPasswordSet()
        {
            this.tests = UserManagementTests.GetInstance();

            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            const string Text = "Username,Email,Password\nipe,ip@interlogic.com.ua,123\nvladykx,vladykx@gmail.com,321";

            File.WriteAllText(path, Text);

            var users = new List<User> {
                    new User { Username = "ipe", Email = "ip@interlogic.com.ua", Password = "123" }, 
                    new User { Username = "vladykx", Email = "vladykx@gmail.com", Password = "123" }, 
                };

            this.tests.Storage.CreateUsersFromCSV(path);

            Assert.IsTrue(this.tests.TestUsers(this.tests.Storage.GetUsers(), users));
            Assert.AreEqual(
                this.tests.Storage.GetUser("ipe").Password, this.tests.Storage.EncryptPassword("123"));

            foreach (var user in users)
            {
                this.tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }

        [Test]
        public void CreateUsersWithOpenId()
        {
            this.tests = UserManagementTests.GetInstance();

            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            const string Text =
                "Username,Email,OpenId\nipe,ip@interlogic.com.ua,openid\nvladykx,vladykx@gmail.com,openid2";

            File.WriteAllText(path, Text);

            var users = new List<User> {
                    new User { Username = "ipe", Email = "ip@interlogic.com.ua", OpenId = "openid" }, 
                    new User { Username = "vladykx", Email = "vladykx@gmail.com", OpenId = "openid2" }, 
                };

            this.tests.Storage.CreateUsersFromCSV(path);

            Assert.IsTrue(this.tests.TestUsers(this.tests.Storage.GetUsers(), users));
            Assert.AreEqual(this.tests.Storage.GetUser("ipe").OpenId, "openid");

            foreach (var user in users)
            {
                this.tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }

        [Test]
        public void CreateUsersWithName()
        {
            this.tests = UserManagementTests.GetInstance();
            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            const string Text = "Username,Email,Name\nipe,ip@interlogic.com.ua,name1\nvladykx,vladykx@gmail.com,name2";

            File.WriteAllText(path, Text);

            var users = new List<User> {
                    new User { Username = "ipe", Email = "ip@interlogic.com.ua", Name = "name1" }, 
                    new User { Username = "vladykx", Email = "vladykx@gmail.com", Name = "name2" }, 
                };

            this.tests.Storage.CreateUsersFromCSV(path);

            Assert.IsTrue(this.tests.TestUsers(this.tests.Storage.GetUsers(), users));
            Assert.AreEqual(this.tests.Storage.GetUser("ipe").Name, "name1");

            foreach (var user in users)
            {
                this.tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }

        [Test]
        public void CreateUsersWithRole()
        {
            this.tests = UserManagementTests.GetInstance();

            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            const string Text =
                "Username,Email,Role\nipe,ip@interlogic.com.ua,Teacher\nvladykx,vladykx@gmail.com,Student";

            File.WriteAllText(path, Text);

            var users = new List<User> {
                    new User { Username = "ipe", Email = "ip@interlogic.com.ua", Name = "name1" }, 
                    new User { Username = "vladykx", Email = "vladykx@gmail.com", Name = "name2" }, 
                };

            this.tests.Storage.CreateUsersFromCSV(path);

            Assert.IsTrue(this.tests.Storage.GetUser("ipe").Roles.Contains(Role.Teacher));
            Assert.IsTrue(this.tests.Storage.GetUser("vladykx").Roles.Contains(Role.Student));

            foreach (var user in users)
            {
                this.tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }

        [Test]
        public void CreateUsersDuplicate()
        {
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);
            this.tests = UserManagementTests.GetInstance();

            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            const string Text = "Username,Email\nname,mail2@mail.com";

            File.WriteAllText(path, Text);

            this.tests.Storage.CreateUsersFromCSV(path);

            Assert.AreEqual(this.tests.Storage.GetUser("name").Email, "mail@mail.com");

            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }
    }
}