using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class Register
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

        [Test]
        public void RegisterValid()
        {
            selenium.Open("/");
            selenium.Click("link=Register");
            selenium.WaitForPageToLoad("30000");

            var guid = Guid.NewGuid();
            var name = guid.ToString().Replace('-', '_').Substring(0, 12);
            selenium.Type("id=Username", "un_" + name);
            selenium.Type("id=Password", "1");
            selenium.Type("id=ConfirmPassword", "1");
            selenium.Type("id=Email", "CreateUserSuccess@UniqueUserId.com");
            selenium.Type("id=Name", "name");
            selenium.Click("//input[@value='Register']");
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.IsTextPresent("Registered"));
        }

        [Test]
        public void RegisterInvalid()
        {
            selenium.Open("/");
            selenium.Click("link=Register");
            selenium.WaitForPageToLoad("30000");

            var guid = Guid.NewGuid();
            var name = guid.ToString().Replace('-', '_').Substring(0, 12);
            selenium.Type("id=Name", "nestor");
            selenium.Click("//input[@value='Register']");
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.GetLocation().EndsWith("/Account/Register"));
        }
    }
}