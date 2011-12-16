using System.Linq;
using IUDICO.UserManagement.Models;
using Moq;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    class RestorePassword
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        [Test]
        public void RestorePasswordSuccess()
        {
            var model = new RestorePasswordModel { Email = "ipetrovych@gmail.com" };
            var password = _Tests.DataContext.Users.Where(u => u.Username == "panza").Single().Password;

            _Tests.Storage.RestorePassword(model);
            
            var newpassword = _Tests.DataContext.Users.Where(u => u.Username == "panza").Single().Password;

            Assert.IsTrue(newpassword != password);
        }
    }
}
