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
            selenium.AttachFile("name=file", "http://avotarov.net/picture/avatarki/2/kartinki/34-3.jpg");
            selenium.Click("//form[contains(@action, '/Account/UploadAvatar')]//input[@type='submit']");
            selenium.WaitForPageToLoad(this.SeleniumWait);
            
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
            selenium.AttachFile("name=file", "http://avotarov.net/picture/avatarki/2/kartinki/4-3.jpg");
            selenium.Click("//form[contains(@action, '/User/UploadAvatar')]//input[@type='submit']");
            selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(selenium.IsElementPresent("id=avatar"));
            Assert.IsTrue(selenium.GetAttribute("//img[@id='avatar']/@src") != "/Data/Avatars/default.png");
        }

        [Test]
        public void DisplayUserAvatar()
        {
            this.DefaultLogin();

            this.selenium.Click("//a[contains(@href, '/Account/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href, '/Account/Edit')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(this.selenium.IsElementPresent("id=avatar"));
        }
    }
}