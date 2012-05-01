using System.Threading;

using IUDICO.UnitTests.Base;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    [TestFixture]
    public class Users : SimpleWebTest
    {
        [Test]
        public void InitTable()
        {
            this.DefaultLogin();

            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            
            Thread.Sleep(3000);
            Assert.IsTrue(this.selenium.IsElementPresent("id=myDataTable_length"));

            this.Logout();
        }

        [Test]
        public void InitTableWithUsers()
        {
            this.DefaultLogin();

            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            
            Thread.Sleep(3000);
            Assert.IsTrue(this.selenium.IsElementPresent("id=myDataTable_info"));

            this.Logout();
        }

        [Test]
        public void InitSortableTable()
        {
            this.DefaultLogin();

            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            
            Thread.Sleep(3000);
            Assert.IsTrue(this.selenium.IsElementPresent("css=div.DataTables_sort_wrapper"));

            this.Logout();
        }

        [Test]
        public void InitSearch()
        {
            this.DefaultLogin();

            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            
            Thread.Sleep(3000);
            Assert.IsTrue(this.selenium.IsElementPresent("id=myDataTable_filter"));

            this.Logout();
        }

        [Test]
        public void InitRolesContainer()
        {
            this.DefaultLogin();
            
            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            
            Thread.Sleep(3000);
            Assert.IsTrue(this.selenium.IsElementPresent("css=span.ui-icon.ui-icon-triangle-1-e"));

            this.Logout();
        }

        [Test]
        public void LoadUsersPage()
        {
            this.DefaultLogin();
            
            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            
            Assert.IsTrue(this.selenium.IsElementPresent("css=h2"));

            this.Logout();
        }
    }
}