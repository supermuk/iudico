using System.Collections.Generic;
using System.IO;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Notifications;
using Moq;
using NUnit.Framework;

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

        [Test]
        public void CreateMultipleSuccess()
        {
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

        protected bool TestUsers(IEnumerable<User> users, IEnumerable<User> inserted)
        {
            return inserted.Except(users, new UserComparer()).Count() == 0;
        }
    }
}
