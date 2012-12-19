using System;

using IUDICO.UnitTests.Base;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class Register : SimpleWebTest
    {
        [Test]
        public void RegisterValid()
        {
            this.selenium.Open("/");
            this.selenium.Click("//a[contains(@href, '/Account/Register')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            var guid = Guid.NewGuid();
            var name = guid.ToString().Replace('-', '_').Substring(0, 12);
            this.selenium.Type("id=Username", "un_" + name);
            this.selenium.Type("id=Password", "1");
            this.selenium.Type("id=ConfirmPassword", "1");
            this.selenium.Type("id=Email", "CreateUserSuccess@UniqueUserId.com");
            this.selenium.Type("id=Name", "name");
            this.selenium.Click("//input[@value='Register']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Assert.IsTrue(this.selenium.IsTextPresent("Registered"));
        }

        [Test]
        public void RegisterInvalid()
        {
            this.selenium.Open("/");
            this.selenium.Click("//a[contains(@href, '/Account/Register')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            var guid = Guid.NewGuid();
            var name = guid.ToString().Replace('-', '_').Substring(0, 12);
            this.selenium.Type("id=Name", "nestor");
            this.selenium.Click("//input[@value='Register']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Assert.IsTrue(this.selenium.GetLocation().EndsWith("/Account/Register"));
        }

        [Test]
        public void CreateDuplicateUser()
        {
            this.selenium.Open("/");
            this.selenium.Click("//a[contains(@href, '/Account/Register')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            var guid = Guid.NewGuid();
            var name = guid.ToString().Replace('-', '_').Substring(0, 12);
            this.selenium.Type("id=Username", "lex");
            this.selenium.Type("id=Password", "1");
            this.selenium.Type("id=ConfirmPassword", "1");
            this.selenium.Type("id=Email", "CreateDuplicatUser@UniqueUserId.com");
            this.selenium.Type("id=Name", "name");
            this.selenium.Click("//input[@value='Register']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Assert.IsTrue(this.selenium.IsTextPresent("User with such username already exists"));
        }
    }
}