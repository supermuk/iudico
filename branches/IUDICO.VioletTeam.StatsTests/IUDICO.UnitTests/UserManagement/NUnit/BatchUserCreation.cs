using System.Collections.Generic;
using System.IO;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Notifications;
using Moq;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class BatchUserCreation
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        [Test]
        public void CreateUsersWithValidFile()
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

            _Tests.Storage.CreateUsersFromCSV(path);

            Assert.IsTrue(_Tests.TestUsers(_Tests.Storage.GetUsers(), users));

            foreach (var user in users)
            {
                _Tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }
        [Test]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void CreateUsersWithInValidFile()
        {
            _Tests = UserManagementTests.GetInstance();
            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            var text = "UsernamEail\nipe,ip@interlogic.com.ua\nvladykx,vladykx@gmail.com";
            File.WriteAllText(path, text);
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUsersFromCSV(path);
        }
        [Test]
        public void CreateUsersWithPasswordSet()
        {
            _Tests = UserManagementTests.GetInstance();
            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            var text = "Username,Email,Password\nipe,ip@interlogic.com.ua,123\nvladykx,vladykx@gmail.com,321";

            File.WriteAllText(path, text);

            var users = new List<User>
                            {
                                new User {Username = "ipe", Email = "ip@interlogic.com.ua", Password = "123"},
                                new User {Username = "vladykx", Email = "vladykx@gmail.com", Password = "123"},
                            };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            _Tests.Storage.CreateUsersFromCSV(path);

            Assert.IsTrue(_Tests.TestUsers(_Tests.Storage.GetUsers(), users));
            Assert.AreEqual(_Tests.Storage.GetUser(u => u.Username == "ipe").Password, _Tests.Storage.EncryptPassword("123"));
            foreach (var user in users)
            {
                _Tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }
        [Test]
        public void CreateUsersWithOpenId()
        {
            _Tests = UserManagementTests.GetInstance();
            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            var text = "Username,Email,OpenId\nipe,ip@interlogic.com.ua,openid\nvladykx,vladykx@gmail.com,openid2";

            File.WriteAllText(path, text);

            var users = new List<User>
                            {
                                new User {Username = "ipe", Email = "ip@interlogic.com.ua",OpenId = "openid"},
                                new User {Username = "vladykx", Email = "vladykx@gmail.com", OpenId = "openid2"},
                            };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            _Tests.Storage.CreateUsersFromCSV(path);

            Assert.IsTrue(_Tests.TestUsers(_Tests.Storage.GetUsers(), users));

            Assert.AreEqual(_Tests.Storage.GetUser(u => u.Username == "ipe").OpenId, "openid");
            foreach (var user in users)
            {
                _Tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }
        [Test]
        public void CreateUsersWithName()
        {
            _Tests = UserManagementTests.GetInstance();
            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            var text = "Username,Email,Name\nipe,ip@interlogic.com.ua,name1\nvladykx,vladykx@gmail.com,name2";

            File.WriteAllText(path, text);

            var users = new List<User>
                            {
                                new User {Username = "ipe", Email = "ip@interlogic.com.ua", Name = "name1"},
                                new User {Username = "vladykx", Email = "vladykx@gmail.com", Name = "name2"},
                            };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            _Tests.Storage.CreateUsersFromCSV(path);

            Assert.IsTrue(_Tests.TestUsers(_Tests.Storage.GetUsers(), users));

            Assert.AreEqual(_Tests.Storage.GetUser(u => u.Username == "ipe").Name, "name1");
            foreach (var user in users)
            {
                _Tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }
        [Test]
        public void CreateUsersWithRole()
        {
            
            _Tests = UserManagementTests.GetInstance();
            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            var text = "Username,Email,Role\nipe,ip@interlogic.com.ua,Teacher\nvladykx,vladykx@gmail.com,Student";

            File.WriteAllText(path, text);
            var users = new List<User>
                            {
                                new User {Username = "ipe", Email = "ip@interlogic.com.ua", Name = "name1"},
                                new User {Username = "vladykx", Email = "vladykx@gmail.com", Name = "name2"},
                            };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            _Tests.Storage.CreateUsersFromCSV(path);

            Assert.IsTrue(_Tests.Storage.GetUser(u => u.Username == "ipe").Roles.Contains(Role.Teacher));
            Assert.IsTrue(_Tests.Storage.GetUser(u => u.Username == "vladykx").Roles.Contains(Role.Student));
            foreach (var user in users)
            {
                _Tests.Storage.DeleteUser(u => u.Username == user.Username);
            }
        }
        [Test]
        public void CreateUsersDuplicate()
        {
            User temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);
            _Tests = UserManagementTests.GetInstance();
            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            var text = "Username,Email\nname,mail2@mail.com";

            File.WriteAllText(path, text);


            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            _Tests.Storage.CreateUsersFromCSV(path);

            Assert.AreEqual(_Tests.Storage.GetUser(u => u.Username == "name").Email, "mail@mail.com");
            
            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }
    }
}
