using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selenium;

namespace IUDICO.UnitTests.Security.Selenium
{
    /// <summary>
    /// Helper class for SecuritySeleniumTester.
    /// Performs basic actions around Security tab.
    /// </summary>
    class Security
    {
        protected ISelenium selenium;

        public Security(ISelenium selenium)
        {
            this.selenium = selenium;
            Login();
            SwitchToSecurity();
        }

        /// <summary>
        /// Login as admin
        /// </summary>
        public void Login()
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginUsername", "lex");
            this.selenium.Type("id=loginPassword", "lex");
            this.selenium.Click("id=loginDefaultButton");
            this.selenium.WaitForPageToLoad("30000");
        }

        /// <summary>
        /// Click Security tab
        /// </summary>
        public void SwitchToSecurity()
        {
            this.selenium.Click("link=Security");
            this.selenium.WaitForPageToLoad("30000");
        }

        /// <summary>
        /// Click Back link.
        /// </summary>
        public void GoBack()
        {
            this.selenium.Click("link=Back");
            this.selenium.WaitForPageToLoad("30000");
        }

        /// <summary>
        /// Check if there is such text in the list.
        /// </summary>
        public bool IsPresented(string text)
        {
            return this.selenium.IsTextPresent(text);
        }

        /// <summary>
        /// Logout from IUDICO.
        /// </summary>
        public void Logout()
        {
            this.selenium.Click("link=Logout");
            this.selenium.WaitForPageToLoad("30000");
        }
    }
}
