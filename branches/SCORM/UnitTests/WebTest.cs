using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IUDICO.UnitTest.Base;
using Selenium;

namespace IUDICO.UnitTest
{
    [TestFixture]
    public class WebTest: TestFixtureWeb
    {
        /// <summary>
        /// Correct Login
        /// </summary>
        [Test]
        public void Test001()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00$MainContent$Login1$UserName", "lex");
            Selenium.Type("ctl00$MainContent$Login1$Password", "lex");
            Selenium.Click("ctl00$MainContent$Login1$LoginButton");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("StudentPage.aspx", null);
        }

        /// <summary>
        /// Incorrect Login
        /// </summary>
        [Test]
        public void Test002()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00$MainContent$Login1$UserName", "baduser");
            Selenium.Type("ctl00$MainContent$Login1$Password", "baduser");
            Selenium.Click("ctl00$MainContent$Login1$LoginButton");
            Pause(3000);
            
            AssertHasText("Your login attempt was not successful. Please try again.");
            AssertIsOnPage("Login.aspx", null);
        }

        /// <summary>
        /// Open Test
        /// </summary>
        [Test]
        public void Test003()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00$MainContent$Login1$UserName", "lex");
            Selenium.Type("ctl00$MainContent$Login1$Password", "lex");
            Selenium.Click("ctl00$MainContent$Login1$LoginButton");
            Selenium.WaitForPageToLoad("7000");
            AssertLabelText("ctl00_MainContent__headerLabel", "Student Page For: Volodymyr Shtenovych");
            AssertIsOnPage("StudentPage.aspx", null);
            //Selenium.Click("ctl00_MainContent__curriculumTreeViewt2");

            //Pause(3000);
            //Selenium.Click("ctl00_MainContent__openTest");
            //Selenium.WaitForPageToLoad("7000");

            //AssertIsOnPage("OpenTest.aspx", null);
        }
        /*
        /// <summary>
        /// Open Result
        /// </summary>
        [Test]
        public void Test004()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00$MainContent$Login1$UserName", "lex");
            Selenium.Type("ctl00$MainContent$Login1$Password", "lex");
            Selenium.Click("ctl00$MainContent$Login1$LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent__curriculumTreeViewt2");
            Pause(3000);
            Selenium.Click("ctl00_MainContent__showResult");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("ThemeResult.aspx", null);
        }
        */
    }
}
