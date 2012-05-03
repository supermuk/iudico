using IUDICO.UnitTests.Base;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class Logout : SimpleWebTest
    {
        [Test]
        public void LogoutSuccess()
        {
            this.DefaultLogin();
            this.Logout();

            Assert.IsFalse(this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"));
        }
    }
}