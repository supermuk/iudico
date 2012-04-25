using NUnit.Framework;

using IUDICO.UnitTests.Base;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    using System.Configuration;

    [TestFixture]
    public class UpgradeSeleniumTester : SimpleWebTest
    {
        public static string BrowserWait
        {
            get
            {
                return ConfigurationManager.AppSettings["SELENIUM_WAIT"];
            }
        }

        public static string browserUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SELENIUM_URL"];
            }
        }

        [Test]
        public void LoginAsAdmin()
        {
            this.DefaultLogin();
            
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad(this.seleniumWait);
            
            Assert.IsFalse(selenium.IsElementPresent("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]"));
        }
        /*
        [Test]
        public void LoginAsTeacher()
        {
            /*
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.IsElementPresent("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]"));
             
        }

        [Test]
        public void LoginAsStudent()
        {
            
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "stud");
            selenium.Type("//input[@id='loginPassword']", "stud");
            selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad("30000");
            
            Assert.IsFalse(selenium.IsElementPresent("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]"));
             
        }

        [Test]
        public void CheckTeacherRolesAfterUpgrade()
        {
            
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad("30000");

            selenium.Click("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]");
            selenium.WaitForPageToLoad("30000");

            Assert.IsFalse(selenium.IsElementPresent("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]"));
             
        }

        [Test]
        public void CheckTeacherAccessToUser()
        {
            
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad("30000");

            if (selenium.IsElementPresent("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]"))
            {
                selenium.Click("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]");
                selenium.WaitForPageToLoad("30000");
            }

            Assert.IsTrue(selenium.IsElementPresent("//a[contains(@href, '/User/Index')]"));
             
        }

        [Test]
        public void TeacherLogoutLogin()
        {
            
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
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
            selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            selenium.WaitForPageToLoad("30000");

            Assert.IsFalse(selenium.IsElementPresent("//a[contains(@href, '/User/Index')]"));
             
        }*/
    }
}