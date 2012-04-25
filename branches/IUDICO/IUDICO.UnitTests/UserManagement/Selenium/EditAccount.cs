using System;
using System.Text;
using IUDICO.UnitTests.Base;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class EditAccount : SimpleWebTest
    {
        [Test]
        public void WithValidData()
        {
            this.DefaultLogin();

            this.selenium.Click("//a[contains(@href, '/Account/Index')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            this.selenium.Click("//a[contains(@href, '/Account/Edit')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            this.selenium.Type("id=Name", "lex");
            this.selenium.Type("id=Email", "lex@iudico.com");
            this.selenium.Click("//input[@value='Save']");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait+"0");
            Assert.IsTrue(this.selenium.IsTextPresent("lex"));
        }

        [Test]
        public void WithInvalidData()
        {
            this.DefaultLogin();

            this.selenium.Click("//a[contains(@href, '/Account/Index')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            this.selenium.Click("//a[contains(@href, '/Account/Edit')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            this.selenium.Type("id=Name", string.Empty);
            this.selenium.Click("//input[@value='Save']");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            Assert.IsTrue(this.selenium.IsTextPresent("Full name is requiered"));
        }
    }
}