using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class Account
    {
        private ISelenium selenium;
        private StringBuilder verificationErrors;

        [SetUp]
        public void SetupTest()
        {
            this.selenium = new DefaultSelenium("localhost", 4444, "*chrome", UpgradeSeleniumTester.browserUrl);
            this.selenium.Start();
            this.verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                this.selenium.Stop();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }

            Assert.AreEqual(string.Empty, this.verificationErrors.ToString());
        }

        [Test]
        public void GetCurrentlyLoggedInUserWhenLogged()
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginPassword", "lex");
            this.selenium.Type("id=loginUsername", "lex");
            this.selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

            Assert.IsTrue(this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
            Assert.IsTrue(this.selenium.IsTextPresent("Logged in as lex"));
        }

        [Test]
        public void GetCurrentlyLoggedInUserWhenNotLogged()
        {
            this.selenium.Open("/");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

            Assert.IsFalse(this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
            Assert.IsFalse(this.selenium.IsTextPresent("Logged in as"));
            Assert.IsTrue(this.selenium.IsTextPresent("Login"));
        }
    }
}