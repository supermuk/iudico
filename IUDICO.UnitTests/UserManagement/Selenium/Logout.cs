using System.Text;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class Logout
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
        public void LogoutSuccess()
        {
            selenium.Open("/");
            selenium.Type("id=loginPassword", "lex");
            selenium.Type("id=loginUsername", "lex");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Account/Logout')]");
            selenium.WaitForPageToLoad("30000");
            Assert.IsFalse(selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
        }
    }
}