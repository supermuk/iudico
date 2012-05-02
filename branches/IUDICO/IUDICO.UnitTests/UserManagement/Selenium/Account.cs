using IUDICO.UnitTests.Base;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class Account : SimpleWebTest
    {
        [Test]
        public void GetCurrentlyLoggedInUserWhenLogged()
        {
            this.DefaultLogin();

            Assert.IsTrue(this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
            Assert.IsTrue(this.selenium.IsTextPresent("Logged in as"));
        }

        [Test]
        public void GetCurrentlyLoggedInUserWhenNotLogged()
        {
            this.selenium.Open("/");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsFalse(this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
            Assert.IsFalse(this.selenium.IsTextPresent("Logged in as"));
            Assert.IsTrue(this.selenium.IsTextPresent("Login"));
        }
    }
}