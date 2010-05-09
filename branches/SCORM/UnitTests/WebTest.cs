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
        /// create group bad
        /// </summary>
        [Test]
        public void Test015_CreateNewGroupBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Create Group");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreate");

            AssertIsOnPage("CreateGroup.aspx", null);
            AssertTextBoxValue("ctl00_MainContent_tbGroupName", "");
        }

        /// <summary>
        /// edit group
        /// </summary>
        [Test]
        public void Test016_EditNameGroup()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("7000");
            
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkGroupName");
            Selenium.WaitForPageToLoad("7000");
            
            Selenium.Type("ctl00_MainContent_tbGroupName", "12");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("7000");
            
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkGroupName");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "123");
            Selenium.Click("ctl00_MainContent_btnApply");

            AssertIsOnPage("EditGroup.aspx", null);
            AssertLabelText("ctl00_MainContent_lbTitle", "Edit group 123");
        }

        /// <summary>
        /// add and remove group
        /// </summary>
        [Test]
        public void Test017_AddRemoveGroup()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("link=Create Group");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "TestGroup1");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("Groups.aspx", null);
        }

        
        
    }
}
