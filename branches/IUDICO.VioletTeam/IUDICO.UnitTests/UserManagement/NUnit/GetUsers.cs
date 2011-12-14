using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.UserManagement.Models;
using IUDICO.Common.Models.Notifications;
using Moq;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class GetUsers
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();
        [Test]
        public void GetUsersSuccess()
        {
            var users = new List<User>
                            {
                                new User {Username = "ipex", Email = "ip@interlogic.com.ua", Password = "asd"},
                                new User {Username = "vladykx", Email = "vladykx@gmail.com",Password = "asd"},
                            };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            foreach (var user in users)
            {
                _Tests.Storage.CreateUser(user);
            }
            Assert.IsTrue(TestUsers(_Tests.Storage.GetUsers(), users));
        }
        protected bool TestUsers(IEnumerable<User> users, IEnumerable<User> inserted)
        {
            return inserted.Except(users, new UserComparer()).Count() == 0;
        }
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
    }
}