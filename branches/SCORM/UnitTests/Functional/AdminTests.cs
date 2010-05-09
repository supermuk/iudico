using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IUDICO.UnitTest.Base;

namespace IUDICO.UnitTest.Functional
{
    [TestFixture]
    public class AdminTests: TestFixtureWeb
    {
        [SetUp]
        public void SetUp()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("Home.aspx", null);
        }

        [TearDown]
        public void TearDown()
        {
            Selenium.Open("/Logout.ashx");
        }

        [Test]
        public void UserRoleTrainer()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_cbTrainerRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_cbTrainerRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("EditUser.aspx", null);
            AssertCheckBoxState(false, "ctl00_MainContent_cbTrainerRole");
        }

        [Test]
        public void UserRoleLector()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_cbLectorRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_cbLectorRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("EditUser.aspx", null);
            AssertCheckBoxState(false, "ctl00_MainContent_cbLectorRole");
        }

        [Test]
        public void UserRoleAdmin()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_cbAdminRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_cbAdminRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("EditUser.aspx", null);
            AssertCheckBoxState(false, "ctl00_MainContent_cbAdminRole");
        }

        [Test]
        public void IncludeGroupToOldUser()
        {
            Selenium.Click("//tr[@id='ctl00_MainMenun14']/td/table/tbody/tr/td");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnInclude");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_gvGroups_ctl02_lnkSelect");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");

            AssertLabelText("ctl00_MainContent_GroupList_gvGroups_ctl02_Label1", "Group");
        }

        [Test]
        public void AddRemoveUser()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser14");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "test14@test.test");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUser14");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            ClickOnLastButtonRemove();
            //selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.Type("ctl00_MainContent_tbSearchPattern", "TestUser14");
            Selenium.Click("ctl00_MainContent_btnSearch");

            AssertNoLabel("ctl00_MainContent_UserList_gvUsers_ctl03_lbFirstName");
            AssertIsNotTextAvailable("TestUser14");
            //AssertHtmlText("ctl00_globalUpdatePanel", "Users available in the system");
        }

        [Test]
        public void AddRemoveUser2()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateUser");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser20");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "test20@test.test");

            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUser20");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            ClickOnLastButtonRemove();
            //selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");

            Selenium.Type("ctl00_MainContent_tbSearchPattern", "TestUser20");
            Selenium.Click("ctl00_MainContent_btnSearch");

            AssertIsNotTextAvailable("TestUser20");
        }

        [Test]
        public void IncludeNewUserToGroup()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateUser");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser21");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "test21@test.test");

            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");

            Selenium.Click("link=TestUser21");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_btnInclude");
            Selenium.WaitForPageToLoad("7000");
            ClickOnButtonWithValue("Include");

            //Selenium.Click("ctl00_MainContent_gvGroups_ctl02_lnkSelect");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            ClickOnLastButtonRemove();
            //selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");

            Selenium.Type("ctl00_MainContent_tbSearchPattern", "TestUser21");
            Selenium.Click("ctl00_MainContent_btnSearch");

            AssertIsNotTextAvailable("TestUser21");
        }
    }
}
