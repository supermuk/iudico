using System;

using IUDICO.UnitTests.Base;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class UniqueUserId : SimpleWebTest
    {
        [Test]
        public void CreateUserSuccess()
        {
            this.DefaultLogin();

            var guid = Guid.NewGuid();
            var name = guid.ToString().Replace('-', '_').Substring(0, 12);

            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href, '/User/Create')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Type("id=Username", "un_" + name);
            this.selenium.Type("id=Password", "1");
            this.selenium.Type("id=Email", "CreateUserSuccess@UniqueUserId.com");
            this.selenium.Type("id=Name", "name");
            this.selenium.Type("id=UserId", "id_" + name);
            this.selenium.Click("//input[@value='Create']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(this.selenium.GetLocation().EndsWith("/User/Index"));
        }

        [Test]
        public void CreateUserViolation()
        {
            this.DefaultLogin();

            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href, '/User/Create')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Type("id=Username", "lex");
            this.selenium.Type("id=Password", "1");
            this.selenium.Type("id=Email", "asdsd");
            this.selenium.Type("id=Name", "name");
            this.selenium.Type("id=UserId", "id");
            this.selenium.Click("//input[@value='Create']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(this.selenium.GetLocation().EndsWith("/User/Create"));
        }
    }
}