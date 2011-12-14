using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class UpgradeSeleniumTester
    {
        private ISelenium selenium;

        private StringBuilder verificationErrors;

        [SetUp]
        public void SetupTest()
        {
            selenium = new DefaultSelenium("localhost", 4444, "*chrome", "http://127.0.0.1:1556/");
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
        public void LoginAsAdmin()
        {
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "lex");
            selenium.Type("//input[@id='loginPassword']", "lex");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=" + IUDICO.UserManagement.Localization.getMessage("Account"));
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.IsElementPresent("link=" + IUDICO.UserManagement.Localization.getMessage("UpgradeToAdmin")) == false);
        }

        [Test]
        public void LoginAsTeacher()
        {
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=" + IUDICO.UserManagement.Localization.getMessage("Account"));
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.IsElementPresent("link=" + IUDICO.UserManagement.Localization.getMessage("UpgradeToAdmin")) == true);    
        }

        [Test]
        public void LoginAsStudent()
        {
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "stud");
            selenium.Type("//input[@id='loginPassword']", "stud");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=" + IUDICO.UserManagement.Localization.getMessage("Account"));
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.IsElementPresent("link=" + IUDICO.UserManagement.Localization.getMessage("UpgradeToAdmin")) == false);
        }

        [Test]
        public void CheckTeacherRolesAfterUpgrade()
        {
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=" + IUDICO.UserManagement.Localization.getMessage("Account"));
            selenium.WaitForPageToLoad("30000");

            selenium.Click("link=" + IUDICO.UserManagement.Localization.getMessage("UpgradetoAdmin"));
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue((selenium.IsElementPresent("//div[@id='main']/fieldset[2]/ul/li[1]") == true) &&
                          (selenium.IsElementPresent("//div[@id='main']/fieldset[2]/ul/li[2]") == true));
        }

        [Test]
        public void CheckTeacherAccessToUser()
        {
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=" + IUDICO.UserManagement.Localization.getMessage("Account"));
            selenium.WaitForPageToLoad("30000");

            if (selenium.IsElementPresent("link=" + IUDICO.UserManagement.Localization.getMessage("UpgradeToAdmin")))
            {
                selenium.Click("link=" + IUDICO.UserManagement.Localization.getMessage("UpgradeToAdmin"));
                selenium.WaitForPageToLoad("30000");
            }

            selenium.Click("link=" + IUDICO.UserManagement.Localization.getMessage("Users"));
            selenium.WaitForPageToLoad("30000");
            string headerText = selenium.GetText("//div[@id='main']/h2");
            Assert.IsTrue(headerText == IUDICO.UserManagement.Localization.getMessage("Users"));
        }

        [Test]
        public void TeacherLogoutLogin()
        {
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=" + IUDICO.UserManagement.Localization.getMessage("Account"));
            selenium.WaitForPageToLoad("30000");

            if (selenium.IsElementPresent("link=" + IUDICO.UserManagement.Localization.getMessage("UpgradeToAdmin")))
            {
                selenium.Click("link=" + IUDICO.UserManagement.Localization.getMessage("UpgradeToAdmin"));
                selenium.WaitForPageToLoad("30000");
            }

            selenium.Click("link=Logout");

            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=" + IUDICO.UserManagement.Localization.getMessage("Users"));
            selenium.WaitForPageToLoad("30000");

            string headerText = selenium.GetText("//div[@id='main']/h2");
            Assert.IsTrue(headerText != IUDICO.UserManagement.Localization.getMessage("Users"));
        }

    }
}
