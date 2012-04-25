using IUDICO.UnitTests.Base;
using NUnit.Framework;
using Selenium;
using System.Text;
using System;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class Register : SimpleWebTest
    {
        [Test]
        public void RegisterValid()
        {
            this.selenium.Open("/");
            this.selenium.Click("link=Register");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

            var guid = Guid.NewGuid();
            var name = guid.ToString().Replace('-', '_').Substring(0, 12);
            this.selenium.Type("id=Username", "un_" + name);
            this.selenium.Type("id=Password", "1");
            this.selenium.Type("id=ConfirmPassword", "1");
            this.selenium.Type("id=Email", "CreateUserSuccess@UniqueUserId.com");
            this.selenium.Type("id=Name", "name");
            this.selenium.Click("//input[@value='Register']");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait+"0");
            Assert.IsTrue(this.selenium.IsTextPresent("Registered"));
        }

        [Test]
        public void RegisterInvalid()
        {
            this.selenium.Open("/");
            this.selenium.Click("link=Register");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

            var guid = Guid.NewGuid();
            var name = guid.ToString().Replace('-', '_').Substring(0, 12);
            this.selenium.Type("id=Name", "nestor");
            this.selenium.Click("//input[@value='Register']");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            Assert.IsTrue(this.selenium.GetLocation().EndsWith("/Account/Register"));
        }
    }
}