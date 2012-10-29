using IUDICO.Common;
using IUDICO.UnitTests.Base;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
    using System;

    [TestFixture]
    public class ManageGroups : SimpleWebTest
    {
        [Test]
        public void CreateWithValidData()
        {
            this.DefaultLogin("lex", "prof");
            selenium.WaitForPageToLoad(this.SeleniumWait);
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
            this.DefaultLogin("lex", "prof");
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
