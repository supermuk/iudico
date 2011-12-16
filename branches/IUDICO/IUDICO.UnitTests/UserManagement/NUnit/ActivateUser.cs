using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class ActivateUser
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        [Test]
        public void ActivateUserSuccess()
        {
            User temp = new User { Username = "ippe", Email = "oip@interlogic.com.ua", Password = "pass123" };
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            System.Guid gg = _Tests.Storage.GetUser(u => u.Username == "ippe").Id;

            _Tests.Storage.ActivateUser(gg);

            Assert.AreEqual(_Tests.Storage.GetUser(u => u.Username == "ippe").IsApproved, true);
        }
    }
}
