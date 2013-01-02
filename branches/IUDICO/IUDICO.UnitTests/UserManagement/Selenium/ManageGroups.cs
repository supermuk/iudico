using IUDICO.Common;
using IUDICO.UnitTests.Base;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    using System;

    [TestFixture]
    public class ManageGroups : SimpleWebTest
    {
        /// <summary>
        /// fixed - Yarema Kipetskiy
        /// </summary>
        [Test]
        public void CreateWithValidDataTest()
        {
            // Signing in as a teacher.
            this.DefaultLogin("prof", "prof");
            selenium.WaitForPageToLoad(this.SeleniumWait);
            // Creating new group wuth valid data.
            selenium.Click("//a[contains(@href, '/Group/Index')]");
            selenium.WaitForPageToLoad(this.SeleniumWait);
            selenium.Click("//a[contains(@href, '/Group/Create')]");
            selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Type("id=Name", "test");
            this.selenium.Click("//input[@value='Create']");
            selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(this.selenium.GetLocation().Contains("Group/Index"));
        }

        [Test]
        public void CreateWithInvalidData()
        {
            this.DefaultLogin("prof", "prof");
            selenium.WaitForPageToLoad(this.SeleniumWait);
            selenium.Click("//a[contains(@href, '/Group/Index')]");
            selenium.WaitForPageToLoad(this.SeleniumWait);
            selenium.Click("//a[contains(@href, '/Group/Create')]");
            selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Type("id=Name", string.Empty);
            this.selenium.Click("//input[@value='Create']");
            selenium.WaitForPageToLoad(this.SeleniumWait);

            Assert.IsTrue(this.selenium.IsElementPresent("//span[contains(.,'Name is required')]"));
        }
    }
}
