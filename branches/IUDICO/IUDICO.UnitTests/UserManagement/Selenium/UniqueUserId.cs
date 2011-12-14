using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models.Storage;
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
            selenium = new DefaultSelenium("localhost", 4444, "*chrome", "http://127.0.0.1:1556/");
            selenium.Start();
            verificationErrors = new StringBuilder();

            selenium.Open("/");
			selenium.Type("id=loginPassword", "lex");
			selenium.Type("id=loginUsername", "lex");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
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
            selenium.Click("css=input[type=\"submit\"]");
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.GetLocation().EndsWith("/User/Index"));
        }

        [Test]
        public void CreateUserViolation()
        {
            selenium.Click("//a[contains(@href, '/User/Index')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/User/Create')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=Username", "does_not_matter");
            selenium.Type("id=Password", "1");
            selenium.Type("id=Email", "does@not.matter");
            selenium.Type("id=Name", "does not matter");
            selenium.Type("id=UserId", "ADMIN 000001");
            selenium.Click("css=input[type=\"submit\"]");
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.GetLocation().EndsWith("/User/Create"));
            Assert.IsTrue(selenium.IsTextPresent("This unique ID is already used"));
        }


        [Test]
        public void EditUserSuccess()
        {
            // do
            selenium.Click("//a[contains(@href, '/User/Index')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/User/Edit?id=d47e8c09-2827-e011-840f-93b2f3060fee')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=Email", "lex@iudico.com");
            selenium.Type("id=UserId", "a");
            selenium.Click("css=p > input[type=\"submit\"]");
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.GetLocation().EndsWith("/User/Index"));
            // undo
            selenium.Click("//a[contains(@href, '/User/Edit?id=d47e8c09-2827-e011-840f-93b2f3060fee')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=UserId", "ADMIN 000001");
            selenium.Click("css=p > input[type=\"submit\"]");
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.GetLocation().EndsWith("/User/Index"));
        }

        [Test]
        public void EditUserViolation()
        {
        }

        [Test]
        public void EditAccount()
        {
            // do
            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Account/Edit')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=Email", "lex@iudico.com");
            selenium.Type("id=UserId", "a");
            selenium.Click("css=p > input[type=\"submit\"]");
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.GetLocation().EndsWith("/Account/Index"));
            // undo
            selenium.Click("//a[contains(@href, '/Account/Edit')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=UserId", "ADMIN 000001");
            selenium.Click("css=p > input[type=\"submit\"]");
            selenium.WaitForPageToLoad("30000");
            Assert.IsTrue(selenium.GetLocation().EndsWith("/Account/Index"));
        }

        [Test]
        public void EditAccountViolation()
        {
        }
    }
}
