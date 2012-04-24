using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class UniqueUserId
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
            this.selenium.Click("//a[contains(@href, '/Account/Logout')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

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

        [TestFixtureSetUp]
        public void SetupFixture()
        {
        }

        [TestFixtureTearDown]
        public void TeardownFixture()
        {
        }

        [Test]
        public void CreateUserSuccess()
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
            this.selenium.Type("id=Username", "un_" + name);
            this.selenium.Type("id=Password", "1");
            this.selenium.Type("id=Email", "CreateUserSuccess@UniqueUserId.com");
            this.selenium.Type("id=Name", "name");
            this.selenium.Type("id=UserId", "id_" + name);
            this.selenium.Click("//input[@value='Create']");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            Assert.IsTrue(this.selenium.GetLocation().EndsWith("/User/Index"));
        }

        [Test]
        public void CreateUserViolation()
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginPassword", "lex");
            this.selenium.Type("id=loginUsername", "lex");
            this.selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            this.selenium.Click("//a[contains(@href, '/User/Create')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            this.selenium.Type("id=Username", "lex");
            this.selenium.Type("id=Password", "1");
            this.selenium.Type("id=Email", "asdsd");
            this.selenium.Type("id=Name", "name");
            this.selenium.Type("id=UserId", "id");
            this.selenium.Click("//input[@value='Create']");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            Assert.IsTrue(this.selenium.GetLocation().EndsWith("/User/Create"));
        }

        [Test]
        public void EditUserSuccess()
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginPassword", "lex");
            this.selenium.Type("id=loginUsername", "lex");
            this.selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

            this.selenium.Click("//a[contains(@href, '/User/Edit?id=d47e8c09-2827-e011-840f-93b2f3060fee')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            this.selenium.Type("id=Name", "nestor");
            this.selenium.Type("id=Password", "lex");
            this.selenium.Type("id=Email", "lex@iudico.com");
            this.selenium.Click("//input[@value='Save']");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

            Assert.IsTrue(this.selenium.GetLocation().EndsWith("/User/Index"));
        }

        [Test]
        public void EditUserViolation()
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginPassword", "lex");
            this.selenium.Type("id=loginUsername", "lex");
            this.selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

            this.selenium.Click("//a[contains(@href, '/User/Edit?id=d47e8c09-2827-e011-840f-93b2f3060fee')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            this.selenium.Type("id=Email", "lex@iudic");
            this.selenium.Click("//input[@value='Save']");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);

            Assert.IsTrue(this.selenium.GetLocation().EndsWith("/User/Edit?id=d47e8c09-2827-e011-840f-93b2f3060fee"));
        }
    }
}