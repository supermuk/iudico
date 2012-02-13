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
            selenium = new DefaultSelenium("localhost", 4444, "*chrome", UpgradeSeleniumTester.browserURL);
            selenium.Start();
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                selenium.Stop();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }

            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void LoginWithOpenIdSuccess()
        {
            selenium.Open("/");
            selenium.Type("id=loginPassword", "lex");
            selenium.Type("id=loginUsername", "lex");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);

            var guid = Guid.NewGuid();
            var name = guid.ToString().Replace('-', '_').Substring(0, 12);

            selenium.Click("//a[contains(@href, '/User/Index')]");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            selenium.Click("//a[contains(@href, '/User/Create')]");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            selenium.Type("id=Username", "nestor");
            selenium.Type("id=Password", "1");
            selenium.Type("id=Email", "yavorskyy.nestor@gmail.com");
            selenium.Type("id=Name", "nestor");
            selenium.Type("id=UserId", "id_nestor");
            selenium.Type("id=OpenId", "yavora.livejournal.com");
            selenium.Click("//input[@value='Create']");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            selenium.Click("//a[contains(@href, '/Account/Logout')]");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);

            selenium.Type("id=loginIdentifier", "yavora.livejournal.com");
            selenium.Click("//div[@id='logindisplay']/form[1]/input[2]");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            if (!selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"))
            {
                selenium.Type("id=login_user", "yavora");
                selenium.Type("id=login_password", "nestor1");
                selenium.Click("//input[@id='loginlj_submit']");
                selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
                selenium.Click("//input[@name='yes:once']");
                selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            }

            Assert.IsTrue(selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
        }

        [Test]
        public void LoginWithInvalidConnected()
        {
            selenium.Open("/");
            selenium.Type("id=loginIdentifier", "aaa");
            selenium.Click("//div[@id='logindisplay']/form[1]/input[2]");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            Assert.IsFalse(selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
            Assert.IsTrue(selenium.IsTextPresent("Login failed using the provided OpenID identifier"));
        }
    }
}