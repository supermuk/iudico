using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class StandartLogin
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
        public void StandartLoginValid()
        {
            selenium.Open("/");
            selenium.Type("id=loginPassword", "lex");
            selenium.Type("id=loginUsername", "lex");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            Assert.IsTrue(selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
        }

        [Test]
        public void StandartLoginInvalid()
        {
            selenium.Open("/");
            selenium.Type("id=loginPassword", "aaa");
            selenium.Type("id=loginUsername", "aaa");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
            Assert.IsFalse(selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
        }
    }
}