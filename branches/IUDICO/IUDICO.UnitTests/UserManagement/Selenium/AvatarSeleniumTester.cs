using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class AvatarSeleniumTester
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

        /*[Test]
            public void UploadNewAvatar()
            {
                selenium.Open("/");
                selenium.Type("loginPassword", "lex");
                selenium.Type("loginUsername", "lex");
                selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("link=Account");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("link=Edit");
                selenium.WaitForPageToLoad("30000");
                selenium.Type("file", "D:\\IUDICO\\Iudico Code\\IUDICO.LMS\\Data\\Avatars\\test.png");
                selenium.Click("//input[@value='Upload']");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("link=Back to Account");
                selenium.WaitForPageToLoad("30000");
                Assert.IsTrue(selenium.IsElementPresent("avatar"));
            }

            [Test]
            public void EditUserAvatar()
            {
                selenium.Open("/");
                selenium.Type("loginPassword", "lex");
                selenium.Type("loginUsername", "lex");
                selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("link=Users");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("//div[@id='main']/table/tbody/tr[4]/td[7]/a[2]");
                selenium.WaitForPageToLoad("30000");
                selenium.Type("file", "D:\\IUDICO\\Iudico Code\\IUDICO.LMS\\Data\\Avatars\\test3.png");
                selenium.Click("//input[@value='Upload']");
                selenium.WaitForPageToLoad("30000");
                Assert.IsTrue(selenium.IsElementPresent("avatar"));
            }*/

        [Test]
        public void DisplayUserAvatar()
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginPassword", "lex");
            this.selenium.Type("id=loginUsername", "lex");
            this.selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            this.selenium.Click("//a[contains(@href, '/Account/Index')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            this.selenium.Click("//a[contains(@href, '/Account/Edit')]");
            this.selenium.WaitForPageToLoad(UpgradeSeleniumTester.BrowserWait);
            Assert.IsTrue(this.selenium.IsElementPresent("avatar"));
        }
    }
}