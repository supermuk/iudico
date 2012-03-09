using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class EditAccount
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
        public void WithValidData()
        {
            selenium.Open("/");
            selenium.Type("id=loginPassword", "lex");
            selenium.Type("id=loginUsername", "lex");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            selenium.Click("//a[contains(@href, '/Account/Edit')]");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            selenium.Type("id=Name", "nestor");
            selenium.Type("id=Email", "lex@iudico.com");
            selenium.Click("//input[@value='Save']");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            Assert.IsTrue(selenium.IsTextPresent("nestor"));
        }

        [Test]
        public void WithInvalidData()
        {
            selenium.Open("/");
            selenium.Type("id=loginPassword", "lex");
            selenium.Type("id=loginUsername", "lex");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            selenium.Click("//a[contains(@href, '/Account/Edit')]");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            selenium.Type("id=Name", "");
            selenium.Click("//input[@value='Save']");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            Assert.IsTrue(selenium.IsTextPresent("Full name is requiered"));
        }
    }
}