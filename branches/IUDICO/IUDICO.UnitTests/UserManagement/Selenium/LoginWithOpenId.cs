using System;
using System.Text;
using NUnit.Framework;
using Selenium;


namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class LoginWithOpenId
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
        public void LoginWithOpenIdSuccess()
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginPassword", "lex");
            this.selenium.Type("id=loginUsername", "lex");
            this.selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

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

            this.selenium.Type("id=loginIdentifier", "yavora.livejournal.com");
            this.selenium.Click("//form[contains(@action, '/Account/Login')]/input[2]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

            if (!this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"))
            {
                this.selenium.Type("id=login_user", "yavora");
                this.selenium.Type("id=login_password", "nestor1");
                this.selenium.Click("//input[@id='loginlj_submit']");
                this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
                
                if (this.selenium.GetLocation().Contains("http://www.livejournal.com"))
                {
                    this.selenium.Click("//input[@name='yes:once']");
                    this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
                }
            }

            Assert.IsTrue(this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
        }

        [Test]
        public void LoginWithInvalidConnected()
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginIdentifier", "aaa");
            this.selenium.Click("//form[contains(@action, '/Account/Login')]/input[2]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            Assert.IsFalse(this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
            Assert.IsTrue(this.selenium.IsTextPresent("Login failed using the provided OpenID identifier"));
        }
    }
}