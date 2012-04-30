using IUDICO.Common;
using IUDICO.UnitTests.Base;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    using System;

    [TestFixture]
    public class EditAccount : SimpleWebTest
    {
        [Test]
        public void WithValidData()
        {
            this.DefaultLogin();

            this.selenium.Click("//a[contains(@href, '/Account/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href, '/Account/Edit')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Type("id=Name", "lex");
            this.selenium.Type("id=Email", "lex@iudico.com");
            this.selenium.Click("//input[@value='Save']");
            this.selenium.WaitForPageToLoad((this.seleniumWait * 3).ToString());

            Assert.IsTrue(this.selenium.IsTextPresent("lex"));

            this.Logout();
        }

        [Test]
        public void WithInvalidData()
        {
            this.DefaultLogin();

            this.selenium.Click("//a[contains(@href, '/Account/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href, '/Account/Edit')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Type("id=Name", string.Empty);
            this.selenium.Click("//input[@value='Save']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(this.selenium.IsTextPresent(Localization.GetMessage("FullNameRequired", "IUDICO.UserManagement")));

            this.Logout();
        }

        [Test]
        public void EditUserSuccess()
        {
            this.DefaultLogin();

            var guid = Guid.NewGuid();
            var name = guid.ToString().Replace('-', '_').Substring(0, 12);

            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//tr[2]//a[contains(@href, '/User/Edit?id=')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Type("id=Name", name);
            this.selenium.Type("id=Password", name);
            this.selenium.Type("id=Email", name + "@iudico.com");
            this.selenium.Click("//input[@value='Save']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(this.selenium.GetLocation().EndsWith("/User/Index"));

            this.Logout();
        }

        [Test]
        public void EditUserViolation()
        {
            this.DefaultLogin();

            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.selenium.Click("//a[contains(@href, '/User/Edit?id=')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Type("id=Email", "lex@iudic");
            this.selenium.Click("//input[@value='Save']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(this.selenium.GetLocation().Contains("/User/Edit?id="));

            this.Logout();
        }
    }
}