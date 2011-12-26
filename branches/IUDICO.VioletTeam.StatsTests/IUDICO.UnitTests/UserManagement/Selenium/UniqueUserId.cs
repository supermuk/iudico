using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class UniqueUserId
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
            selenium.Click("//a[contains(@href, '/Account/Logout')]");
			selenium.WaitForPageToLoad("30000");

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

        [TestFixtureSetUp]
        public void SetupFixture()
        {
        }

        [TestFixtureTearDown]
        public void TeardownFixture()
        {
        }

        [Test]
        public void CreateUserSuccess()
        {
            selenium.Open("/");
            selenium.Type("id=loginPassword", "lex");
            selenium.Type("id=loginUsername", "lex");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");

            var guid = Guid.NewGuid();
            var name = guid.ToString().Replace('-', '_').Substring(0, 12);

            selenium.Click("//a[contains(@href, '/User/Index')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/User/Create')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=Username", "un_" + name);
            selenium.Type("id=Password", "1");
            selenium.Type("id=Email", "CreateUserSuccess@UniqueUserId.com");
            selenium.Type("id=Name", "name");
            selenium.Type("id=UserId", "id_" + name);
            selenium.Click("//input[@value='Create']");
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.GetLocation().EndsWith("/User/Index"));
        }

        [Test]
        public void CreateUserViolation()
        {

            selenium.Open("/");
            selenium.Type("id=loginPassword", "lex");
            selenium.Type("id=loginUsername", "lex");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");


            selenium.Click("//a[contains(@href, '/User/Index')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/User/Create')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=Username", "lex");
            selenium.Type("id=Password", "1");
            selenium.Type("id=Email", "asdsd");
            selenium.Type("id=Name", "name");
            selenium.Type("id=UserId", "id");
            selenium.Click("//input[@value='Create']");
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.GetLocation().EndsWith("/User/Create"));
        }


        [Test]
        public void EditUserSuccess()
        {
            selenium.Open("/");
            selenium.Type("id=loginPassword", "lex");
            selenium.Type("id=loginUsername", "lex");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");


            selenium.Click("//a[contains(@href, '/User/Index')]");
            selenium.WaitForPageToLoad("30000");

            selenium.Click("//a[contains(@href, '/User/Edit?id=d47e8c09-2827-e011-840f-93b2f3060fee')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=Name", "nestor");
            selenium.Type("id=Password", "lex");
            selenium.Type("id=Email", "lex@iudico.com");
            selenium.Click("//input[@value='Save']");
            selenium.WaitForPageToLoad("30000");
            
            
            Assert.IsTrue(selenium.GetLocation().EndsWith("/User/Index"));
        }

        [Test]
        public void EditUserViolation()
        {
            selenium.Open("/");
            selenium.Type("id=loginPassword", "lex");
            selenium.Type("id=loginUsername", "lex");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");


            selenium.Click("//a[contains(@href, '/User/Index')]");
            selenium.WaitForPageToLoad("30000");

            selenium.Click("//a[contains(@href, '/User/Edit?id=d47e8c09-2827-e011-840f-93b2f3060fee')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=Email", "lex@iudic");
            selenium.Click("//input[@value='Save']");
            selenium.WaitForPageToLoad("30000");


            Assert.IsTrue(selenium.GetLocation().EndsWith("/User/Edit?id=d47e8c09-2827-e011-840f-93b2f3060fee"));
        }

        

    }
}
