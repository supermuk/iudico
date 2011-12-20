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
        public static string browserURL = "http://127.0.0.1:1556/";
        private StringBuilder verificationErrors;

        [SetUp]
        public void SetupTest()
        {
            selenium = new DefaultSelenium("localhost", 4444, "*chrome", browserURL);
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
            selenium.Type("id=loginPassword", "lex");
            selenium.Type("id=loginUsername", "lex");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad("30000");
            Assert.IsFalse(selenium.IsElementPresent("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]"));
        }

        [Test]
        public void LoginAsTeacher()
        {
            /*
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.IsElementPresent("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]"));
             */
        }

        [Test]
        public void LoginAsStudent()
        {
            /*
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "stud");
            selenium.Type("//input[@id='loginPassword']", "stud");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad("30000");
            
            Assert.IsFalse(selenium.IsElementPresent("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]"));
             */
        }

        [Test]
        public void CheckTeacherRolesAfterUpgrade()
        {
            /*
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad("30000");

            selenium.Click("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]");
            selenium.WaitForPageToLoad("30000");

            Assert.IsFalse(selenium.IsElementPresent("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]"));
             */
        }

        [Test]
        public void CheckTeacherAccessToUser()
        {
            /*
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad("30000");

            if (selenium.IsElementPresent("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]"))
            {
                selenium.Click("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]");
                selenium.WaitForPageToLoad("30000");
            }

            Assert.IsTrue(selenium.IsElementPresent("//a[contains(@href, '/User/Index')]"));
             * */
        }

        [Test]
        public void TeacherLogoutLogin()
        {
            /*
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad("30000");

            if (selenium.IsElementPresent("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]"))
            {
                selenium.Click("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]");
                selenium.WaitForPageToLoad("30000");
            }

            selenium.Click("//a[contains(@href, '/Account/Logout')]");
            selenium.WaitForPageToLoad("30000");

            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");

            Assert.IsFalse(selenium.IsElementPresent("//a[contains(@href, '/User/Index')]"));
             */
        }
    }
}