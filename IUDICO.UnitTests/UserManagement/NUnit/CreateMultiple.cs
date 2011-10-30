using System.Collections.Generic;
using System.IO;
using System.Linq;
using IUDICO.Common.Models;
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

            /*var results = _Tests.Storage.CreateUsersFromCSV(path);

            _Tests.Users.Verify(u => u.InsertAllOnSubmit(It.Is<IEnumerable<User>>(ie => TestUsers(ie, users))));
            _Tests.MockStorage.Verify(u => u.SendEmail(It.IsAny<string>(), It.Is<string>(s => s == "ip@interlogic.com.ua"), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _Tests.MockStorage.Verify(u => u.SendEmail(It.IsAny<string>(), It.Is<string>(s => s == "vladykx@gmail.com"), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _Tests.MockLmsService.Verify(s => s.Inform(UserNotifications.UserCreateMultiple, It.Is<IEnumerable<User>>(ie => TestUsers(ie, users))));
            Assert.IsTrue(users.Count == results.Count);*/
        }

        [Test]
        public void RestorePassword()
        {
            /*var model = new RestorePasswordModel { Email = "ipetrovych@gmail.com" };
            var password = _Tests.DataContext.Users.Where(u => u.Username == "panza").Single().Password;

            _Tests.Storage.RestorePassword(model);

            _Tests.MockDataContext.Verify(d => d.SubmitChanges());
            _Tests.MockStorage.Verify(u => u.SendEmail(It.IsAny<string>(), It.Is<string>(s => s == "ipetrovych@gmail.com"), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            var newpassword = _Tests.DataContext.Users.Where(u => u.Username == "panza").Single().Password;

            Assert.IsTrue(newpassword != password);*/

        }

        protected bool TestUsers(IEnumerable<User> inserted, IEnumerable<User> expected)
        {
            if (inserted.Count() != expected.Count())
            {
                return false;
            }

            return inserted.Except(expected, new UserComparer()).Count() == 0;
        }
    }
}
