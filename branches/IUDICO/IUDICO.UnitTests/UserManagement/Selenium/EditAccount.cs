using IUDICO.UnitTests.Base;
using NUnit.Framework;
using IUDICO.Common;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
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
            this.selenium.WaitForPageToLoad((this.seleniumWait * 2).ToString());

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
    }
}