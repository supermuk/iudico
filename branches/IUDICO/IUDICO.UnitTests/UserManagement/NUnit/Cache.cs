using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class Cache
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void InvalidateUser()
        {
            // original caching
            this.tests.Storage.GetUsers();

            var temp = new User { Username = "name", Email = "ip@interlogic.com.ua", Password = "pass123" };
            this.tests.Storage.CreateUser(temp);

            var user = this.tests.Storage.GetUsers().SingleOrDefault(u => u.Username == temp.Username);
            Assert.IsNotNull(user);
            
            temp = this.tests.Storage.GetUser("name");

            var expected = new User
            {
                Username = "name",
                Email = "ipp@interlogic.com.ua",
                Password = "pass123",
                Id = temp.Id,
                OpenId = "openid"
            };

            this.tests.Storage.EditUser(temp.Id, new EditUserModel(expected));
            user = this.tests.Storage.GetUsers().SingleOrDefault(u => u.Username == temp.Username);

            Assert.IsTrue(this.tests.TestUsers(this.tests.Storage.GetUser("name"), expected));
            Assert.IsTrue(this.tests.TestUsers(user, expected));

            this.tests.Storage.DeleteUser(u => u.Id == temp.Id);

            Assert.IsNull(this.tests.Storage.GetUser("name"));
            Assert.IsNull(this.tests.Storage.GetUsers().SingleOrDefault(u => u.Username == temp.Username));
        }

        public void InvalidateGroup()
        {
            // original caching
            this.tests.Storage.GetGroups();

            var group = new Group { Id = 1254, Name = "pmp41" };

            this.tests.Storage.CreateGroup(group);

            group = new Group { Id = 1254, Name = "pmp51" };

            this.tests.Storage.EditGroup(group.Id, group);

            Assert.IsTrue(
                group.Name == this.tests.Storage.GetGroup(group.Id).Name
                && group.Id == this.tests.Storage.GetGroup(group.Id).Id);

            this.tests.Storage.DeleteGroup(group.Id);
        }
    }
}
