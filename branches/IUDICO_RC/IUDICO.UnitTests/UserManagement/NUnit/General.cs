using System;

using NUnit.Framework;

using IUDICO.Common.Models.Shared;

using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    using IUDICO.UserManagement.Models;

    [TestFixture]
    public class General
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void SendEmail()
        {
            var storage = new DatabaseUserStorage(this.tests.LmsService);
            var email = storage.SendEmail("nobody@tests-ua.com", "nobody@tests-ua.com", "Test Email", "Test Email");

            Assert.IsTrue(email);
        }

        [Test]
        public void GetCurrentUser()
        {
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.Storage.CreateUser(temp);

            this.tests.Storage.ActivateUser(this.tests.Storage.GetUser("name").Id);

            tests.ChangeCurrentUser("name");

            Assert.IsTrue(tests.Storage.GetCurrentUser().Username== "name");
        }

        [Test]
        public void PromotedToAdmin()
        {
            var storage = new CachedUserStorage(new DatabaseUserStorage(this.tests.LmsService), this.tests.MockCacheProvider.Object);

            Assert.IsFalse(storage.IsPromotedToAdmin());
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetIdentityName()
        {
            var storage = new CachedUserStorage(new DatabaseUserStorage(this.tests.LmsService), this.tests.MockCacheProvider.Object);

            storage.EditAccount(new EditModel());
        }


        [Test]
        public void UsernameExistsCached()
        {
            var storage = new CachedUserStorage(this.tests.Storage, this.tests.MockCacheProvider.Object);

            Assert.IsFalse(storage.UsernameExists(Guid.NewGuid().ToString()));
        }
    }
}
