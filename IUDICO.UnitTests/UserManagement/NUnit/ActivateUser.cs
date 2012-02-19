using System;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class ActivateUser
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        [Test]
        public void ActivateUserExisting()
        {
            User temp = new User {Username = "name", Email = "mail@mail.com", Password = "123"};
            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));
            _Tests.Storage.CreateUser(temp);

            Guid gg = _Tests.Storage.GetUser(u => u.Username == "name").Id;

            _Tests.Storage.ActivateUser(gg);

            Assert.AreEqual(_Tests.Storage.GetUser(u => u.Username == "name").IsApproved, true);

            _Tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void ActivateUserNonExisting()
        {
            Guid gg = Guid.NewGuid();
            _Tests.Storage.ActivateUser(gg);
        }
    }
}