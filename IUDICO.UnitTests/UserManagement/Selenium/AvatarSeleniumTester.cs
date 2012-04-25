using System;
using System.Text;
using IUDICO.UnitTests.Base;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class AvatarSeleniumTester : SimpleWebTest
    {
        /*[Test]
            public void UploadNewAvatar()
            {
                selenium.Open("/");
                selenium.Type("loginPassword", "lex");
                selenium.Type("loginUsername", "lex");
                selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
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
                selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
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
            this.DefaultLogin();

            this.selenium.Click("//a[contains(@href, '/Account/Index')]");
            this.selenium.WaitForPageToLoad(this.seleniumWait);
            this.selenium.Click("//a[contains(@href, '/Account/Edit')]");
            this.selenium.WaitForPageToLoad(this.seleniumWait);
            
            Assert.IsTrue(this.selenium.IsElementPresent("avatar"));

            this.Logout();
        }
    }
}