using System;

using NUnit.Framework;

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
            var storage = new DatabaseUserStorage(this.tests.LmsService);

            Assert.IsTrue(storage.GetCurrentUser().Username == null);
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
        public void GetPath()
        {
            var storage = new DatabaseUserStorage(this.tests.LmsService);

            try
            {
                storage.DeleteAvatar(Guid.NewGuid());
            }
            catch
            {
            }
        }

        [Test]
        public void UsernameExistsCached()
        {
            var storage = new CachedUserStorage(this.tests.Storage, this.tests.MockCacheProvider.Object);

            Assert.IsFalse(storage.UsernameExists(Guid.NewGuid().ToString()));
        }
    }
}
