using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class Logout : SimpleWebTest
    {
        [Test]
        public void LogoutSuccess()
        {
            this.DefaultLogin();

            this.selenium.Click("//a[contains(@href, '/Account/Logout')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Assert.IsFalse(this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
        }
    }
}