using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            }

            [Test]
            public void DisplayUserAvatar()
            {
                selenium.Open("/");
                selenium.Type("loginPassword", "lex");
                selenium.Type("loginUsername", "lex");
                selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("link=Users");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("//div[@id='main']/table/tbody/tr[3]/td[7]/a[3]");
                selenium.WaitForPageToLoad("30000");
                Assert.IsTrue(selenium.IsElementPresent("avatar"));
            }
        }
}
