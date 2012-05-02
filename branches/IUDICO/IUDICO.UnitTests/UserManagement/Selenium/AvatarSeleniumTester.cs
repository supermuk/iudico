using IUDICO.UnitTests.Base;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class AvatarSeleniumTester : SimpleWebTest
    {
        [Test]
        public void UploadNewAvatar()
        {
            this.DefaultLogin();

            selenium.Click("//a[contains(@href, '/Account/Index')]");
            selenium.WaitForPageToLoad(this.SeleniumWait);
            selenium.Click("//a[contains(@href, '/Account/Edit')]");
            selenium.WaitForPageToLoad(this.SeleniumWait);
            selenium.AttachFile("name=file", "http://dl.dropbox.com/u/38366179/test.jpg");
            selenium.Click("//form[contains(@action, '/Account/UploadAvatar')]//input[@type='submit']");
            selenium.WaitForPageToLoad((this.seleniumWait * 3).ToString());
            
            Assert.IsTrue(selenium.IsElementPresent("id=avatar"));
            Assert.IsTrue(selenium.GetAttribute("//img[@id='avatar']/@src") != "/Data/Avatars/default.png");
        }

        [Test]
        public void EditUserAvatar()
        {
            this.DefaultLogin();

            selenium.Click("//a[contains(@href, '/User/Index')]");
            selenium.WaitForPageToLoad(this.SeleniumWait);
            selenium.Click("//a[contains(@href, '/User/Edit?id=')]");
            selenium.WaitForPageToLoad(this.SeleniumWait);
            selenium.AttachFile("name=file", "http://dl.dropbox.com/u/38366179/test2.png");
            selenium.Click("//form[contains(@action, '/User/UploadAvatar')]//input[@type='submit']");
            selenium.WaitForPageToLoad((this.seleniumWait * 3).ToString());

            Assert.IsTrue(selenium.IsElementPresent("id=avatar"));
            Assert.IsTrue(selenium.GetAttribute("//img[@id='avatar']/@src") != "/Data/Avatars/default.png");
        }

        [Test]
        public void DisplayUserAvatar()
        {
            this.DefaultLogin();

            this.selenium.Click("//a[contains(@href, '/Account/Index')]");
            this.selenium.WaitForPageToLoad((this.seleniumWait * 2).ToString());
            this.selenium.Click("//a[contains(@href, '/Account/Edit')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(this.selenium.IsElementPresent("id=avatar"));
        }
    }
}