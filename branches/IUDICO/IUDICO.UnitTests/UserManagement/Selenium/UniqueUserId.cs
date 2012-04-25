using System;
using System.Text;
using IUDICO.UnitTests.Base;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class UniqueUserId : SimpleWebTest
    {
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
            this.DefaultLogin();

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
            this.DefaultLogin();

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
            this.DefaultLogin();

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
            this.DefaultLogin();

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