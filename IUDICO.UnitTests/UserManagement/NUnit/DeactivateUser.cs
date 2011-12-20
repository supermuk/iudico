using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class DeactivateUser
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        [Test]
        public void DeactivateUserExisting()
        {
            User temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };
            
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            System.Guid gg = _Tests.Storage.GetUser(u => u.Username == "name").Id;

            _Tests.Storage.ActivateUser(gg);
            _Tests.Storage.DeactivateUser(gg);

            Assert.AreEqual(_Tests.Storage.GetUser(u => u.Username == "name").IsApproved, false);

            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }
        [Test]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void DeactivateUserNonExisting()
        {
            System.Guid gg = System.Guid.NewGuid();
            _Tests.Storage.DeactivateUser(gg);
        }
    }
}