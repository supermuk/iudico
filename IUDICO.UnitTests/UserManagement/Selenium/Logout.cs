using System;
using System.Text;
using IUDICO.UnitTests.Base;
using NUnit.Framework;
using Selenium;

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
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            Assert.IsFalse(this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
        }
    }
}