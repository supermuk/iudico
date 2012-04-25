using System;
using System.Text;
using IUDICO.UnitTests.Base;
using NUnit.Framework;
using Selenium;


namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class LoginWithOpenId : SimpleWebTest
    {
        [Test]
        public void LoginWithOpenIdSuccess()
        {
            this.DefaultLogin();

            var guid = Guid.NewGuid();
            var name = guid.ToString().Replace('-', '_').Substring(0, 12);

            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            this.selenium.Click("//a[contains(@href, '/User/Create')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            this.selenium.Type("id=Username", "nestor");
            this.selenium.Type("id=Password", "1");
            this.selenium.Type("id=Email", "yavorskyy.nestor@gmail.com");
            this.selenium.Type("id=Name", "nestor");
            this.selenium.Type("id=UserId", "id_nestor");
            this.selenium.Type("id=OpenId", "yavora.livejournal.com");
            this.selenium.Click("//input[@value='Create']");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            this.selenium.Click("//a[contains(@href, '/Account/Logout')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

            this.LoginOpenId("yavorskyy.nestor@gmail.com","nestor","1");

            Assert.IsTrue(this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
        }

        [Test]
        public void LoginWithInvalidConnected()
        {
            this.LoginOpenId("test", "test", "test");

            Assert.IsFalse(this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
            Assert.IsTrue(this.selenium.IsTextPresent("Login failed using the provided OpenID identifier"));
        }
    }
}