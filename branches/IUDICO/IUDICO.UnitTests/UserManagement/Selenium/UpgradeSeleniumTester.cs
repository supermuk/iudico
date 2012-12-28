using IUDICO.UnitTests.Base;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class UpgradeSeleniumTester : SimpleWebTest
    {
        [Test]
        public void LoginAsAdmin()
        {
            this.DefaultLogin();

            this.selenium.Click("//a[contains(@href, '/Account/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsFalse(this.selenium.IsElementPresent("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]"));
        }
        
        /// <summary>
        /// fixed - Yarema Kipetskiy
        /// </summary>
        [Test]
        public void LoginAsTeacher()
        {   
            selenium.Open("/");
            selenium.Type("//input[@id='loginUsername']", "prof");
            selenium.Type("//input[@id='loginPassword']", "prof");
            selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad("30000");

            Assert.IsTrue(selenium.IsElementPresent("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]"));    
        }

        //[Test]
        //public void LoginAsStudent()
        //{
            
        //    selenium.Open("/");
        //    selenium.Type("//input[@id='loginUsername']", "stud");
        //    selenium.Type("//input[@id='loginPassword']", "stud");
        //    selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
        //    selenium.WaitForPageToLoad("30000");
        //    selenium.Click("//a[contains(@href, '/Account/Index')]");
        //    selenium.WaitForPageToLoad("30000");
            
        //    Assert.IsFalse(selenium.IsElementPresent("//a[contains(@href, '/Account/TeacherToAdminUpgrade')]"));
             
        //}

        /// <summary>
        /// fixed - Yarema Kipetskiy
        /// </summary>
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
            selenium.Type("//input[@id='loginPassword']", "lex");
            selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            selenium.WaitForPageToLoad("30000");

            Assert.IsFalse(selenium.IsElementPresent("//a[contains(@href, '/User/Index')]"));
             
        }
    }
}