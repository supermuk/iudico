using System;
using System.Text;
using System.Threading;
using IUDICO.UnitTests.Base;
using NUnit.Framework;
using Selenium;

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
        }

        [Test]
        public void InitSearch()
        {
            this.DefaultLogin();

            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Thread.Sleep(3000);
            Assert.IsTrue(this.selenium.IsElementPresent("id=myDataTable_filter"));
        }

        [Test]
        public void InitRolesContainer()
        {
            this.DefaultLogin();
            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Thread.Sleep(3000);
            Assert.IsTrue(this.selenium.IsElementPresent("css=span.ui-icon.ui-icon-triangle-1-e"));
        }

        [Test]
        public void LoadUsersPage()
        {
            this.DefaultLogin();
            this.selenium.Click("//a[contains(@href, '/User/Index')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Assert.IsTrue(this.selenium.IsElementPresent("css=h2"));
        }
    }
}