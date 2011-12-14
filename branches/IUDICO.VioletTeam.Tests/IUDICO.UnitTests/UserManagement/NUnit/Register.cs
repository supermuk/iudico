using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class Register
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
            Assert.AreEqual(temp.Username, _Tests.Storage.GetUser(u => u.Username == "nestor").Username);
        }
    }
}
