using IUDICO.UnitTests.Base;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class StandartLogin : SimpleWebTest
    {
        [Test]
        public void StandartLoginValid()
        {
            this.DefaultLogin();
            Assert.IsTrue(this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
        }

        [Test]
        public void StandartLoginInvalid()
        {
            this.DefaultLogin("invalid", "user");
            Assert.IsFalse(this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
        }
    }
}