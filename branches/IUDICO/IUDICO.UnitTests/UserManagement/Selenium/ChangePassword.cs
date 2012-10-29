using IUDICO.Common;
using IUDICO.UnitTests.Base;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    using System;

    [TestFixture]
    public class ChangePassword : SimpleWebTest
    {
        [Test]
        public void WithValidData()
        {
            this.DefaultLogin();
            this.selenium.Click("//a[contains(@href, '/Account/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href, '/Account/ChangePassword')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Type("id=OldPassword", "lex");
            this.selenium.Type("id=NewPassword", "lex");
            this.selenium.Type("id=ConfirmPassword", "lex");
            this.selenium.Click("//input[@value='Change']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(this.selenium.GetLocation().Contains("Account/Index"));
        }

        [Test]
        public void WithInvalidData()
        {
            this.DefaultLogin();
            this.selenium.Click("//a[contains(@href, '/Account/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href, '/Account/ChangePassword')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Type("id=OldPassword", string.Empty);
            this.selenium.Type("id=NewPassword", string.Empty);
            this.selenium.Type("id=ConfirmPassword", string.Empty);
            this.selenium.Click("//input[@value='Change']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(this.selenium.IsTextPresent(Localization.GetMessage("OldPasswordRequired", "IUDICO.UserManagement")));
            Assert.IsTrue(this.selenium.IsTextPresent(Localization.GetMessage("NewPasswordRequired", "IUDICO.UserManagement")));
            Assert.IsTrue(this.selenium.IsTextPresent(Localization.GetMessage("ConfirmPasswordRequired", "IUDICO.UserManagement")));
        }

        [Test]
        public void WithInvalidData1()
        {
            this.DefaultLogin();
            this.selenium.Click("//a[contains(@href, '/Account/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href, '/Account/ChangePassword')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Type("id=OldPassword", string.Empty);
            this.selenium.Type("id=NewPassword", "lex");
            this.selenium.Type("id=ConfirmPassword", "lex");
            this.selenium.Click("//input[@value='Change']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(this.selenium.IsTextPresent(Localization.GetMessage("OldPasswordRequired", "IUDICO.UserManagement")));
        }

        [Test]
        public void WithInvalidData2()
        {
            this.DefaultLogin();
            this.selenium.Click("//a[contains(@href, '/Account/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href, '/Account/ChangePassword')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Type("id=OldPassword", "lex");
            this.selenium.Type("id=NewPassword", string.Empty);
            this.selenium.Type("id=ConfirmPassword", string.Empty);
            this.selenium.Click("//input[@value='Change']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(this.selenium.IsTextPresent(Localization.GetMessage("NewPasswordRequired", "IUDICO.UserManagement")));
            Assert.IsTrue(this.selenium.IsTextPresent(Localization.GetMessage("ConfirmPasswordRequired", "IUDICO.UserManagement")));
        }

        [Test]
        public void WithInvalidData3()
        {
            this.DefaultLogin();
            this.selenium.Click("//a[contains(@href, '/Account/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href, '/Account/ChangePassword')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Type("id=OldPassword", "lex");
            this.selenium.Type("id=NewPassword", "lex1");
            this.selenium.Type("id=ConfirmPassword", "lex");
            this.selenium.Click("//input[@value='Change']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(this.selenium.IsTextPresent("Paswords don't match"));
        }
    }
}
