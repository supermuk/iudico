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
        }

        [TearDown]
        public void TearDown()
        {
            Selenium.Open("/Logout.ashx");
        }

        /// <summary>
        /// create group bad
        /// </summary>
        [Test]
        public void CreateNewGroupBad()
        {
            Selenium.Click("link=Create Group");
            Selenium.WaitForPageToLoad("5000");
            Selenium.Click("ctl00_MainContent_btnCreate");

            AssertIsOnPage("CreateGroup.aspx", null);

            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("30000");

            AssertCountButtonOnPage();
        }

        /// <summary>
        /// edit group
        /// </summary>
        [Test]
        public void EditNameGroup()
        {
            Selenium.Click("link=Create Group");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "123");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");

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

            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            //Selenium.WaitForPageToLoad("30000");
        }

        /// <summary>
        /// add and remove group
        /// </summary>
        [Test]
        public void AddRemoveGroup()
        {
            Selenium.Click("link=Create Group");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "TestGroup1");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("7000");

            AssertHtmlText("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkGroupName", "TestGroup1");


            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("7000");
            Assert.IsFalse(Selenium.IsTextPresent("TestGroup"));
            AssertIsOnPage("Groups.aspx", null);
        }

        /// <summary>
        /// out role
        /// </summary>
        [Test]
        public void UserRoleAnyone()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "test1@test.test");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");

            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUser1");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertHtmlText("ctl00_MainContent_Label_Roles", "");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsTextPresent("TestUser1"));
        }

        /// <summary>
        /// give role student to user
        /// </summary>
        [Test]
        public void UserRoleStudent()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser2");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "t2@t.t");

            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            //Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestUser2");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Check("ctl00_MainContent_cbStudentRole");
            Selenium.Click("ctl00_MainContent_btnApply");

            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUser2");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=My Personal Info");

            AssertHasText("STUDENT");
            AssertHtmlText("ctl00_MainContent_Label_Roles", "STUDENT");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");

            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("Users.aspx", null);
            //AssertIsNotTextAvailable("TestUser15");
            Assert.IsFalse(Selenium.IsTextPresent("TestUser2"));
        }

        /// <summary>
        /// give role trainer to user
        /// </summary>
        [Test]
        public void UserRoleTrainer()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser3");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "tu3@t.t");

            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=TestUser3");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Check("ctl00_MainContent_cbTrainerRole");
            Selenium.Click("ctl00_MainContent_btnApply");

            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUser3");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            AssertHasText("TRAINER");
            AssertHtmlText("ctl00_MainContent_Label_Roles", "TRAINER");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");

            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("Users.aspx", null);
            Assert.IsFalse(Selenium.IsTextPresent("TestUser3"));
        }

        /// <summary>
        ///  give role lector to user
        /// </summary>
        [Test]
        public void UserRoleLector()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser4");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "tu4@t.t");

            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestUser4");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Check("ctl00_MainContent_cbLectorRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            //Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUser4");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            AssertHasText("LECTOR");
            AssertHtmlText("ctl00_MainContent_Label_Roles", "LECTOR");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");

            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("Users.aspx", null);
            Assert.IsFalse(Selenium.IsTextPresent("TestUser4"));
        }

        /// <summary>
        ///  give role admin to user
        /// </summary>
        [Test]
        public void UserRoleAdmin()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser5");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "tu5@t.t");

            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestUser5");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Check("ctl00_MainContent_cbAdminRole");
            Selenium.Click("ctl00_MainContent_btnApply");

            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUser5");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            AssertHasText("ADMIN");
            AssertHtmlText("ctl00_MainContent_Label_Roles", "ADMIN");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");

            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("Users.aspx", null);
            Assert.IsFalse(Selenium.IsTextPresent("TestUser5"));
        }

        /// <summary>
        /// give role super_admin to user
        /// </summary>
        [Test]
        public void UserRoleSuper_Admin()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser6");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "tu6@t.t");

            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestUser6");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Check("ctl00_MainContent_cbSuperAdminRole");
            Selenium.Click("ctl00_MainContent_btnApply");

            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUser6");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            AssertHasText("SUPER_ADMIN");
            AssertHtmlText("ctl00_MainContent_Label_Roles", "SUPER_ADMIN");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");

            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("Users.aspx", null);
            Assert.IsFalse(Selenium.IsTextPresent("TestUser6"));
        }

        /// <summary>
        /// give all roles to user
        /// </summary>
        [Test]
        public void UserAllRole()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser7");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "tu7@t.t");

            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestUser7");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Check("ctl00_MainContent_cbStudentRole");
            Selenium.Check("ctl00_MainContent_cbTrainerRole");
            Selenium.Check("ctl00_MainContent_cbLectorRole");
            Selenium.Check("ctl00_MainContent_cbAdminRole");
            Selenium.Check("ctl00_MainContent_cbSuperAdminRole");
            Selenium.Click("ctl00_MainContent_btnApply");

            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUser7");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=My Personal Info");

            AssertHasText("STUDENT, LECTOR, TRAINER, ADMIN, SUPER_ADMIN");
            AssertHtmlText("ctl00_MainContent_Label_Roles", "STUDENT, LECTOR, TRAINER, ADMIN, SUPER_ADMIN");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");

            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("Users.aspx", null);
            Assert.IsFalse(Selenium.IsTextPresent("TestUser7"));
        }

        /// <summary>
        /// include group to user
        /// </summary>
        [Test]
        public void IncludeGroupToOldUser()
        {
            Selenium.Click("link=Create Group");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "123");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");

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
            Selenium.WaitForPageToLoad("7000");

            AssertLabelText("ctl00_MainContent_GroupList_gvGroups_ctl02_Label1", "Group");

            //Selenium.Click("link=Groups");
            //Selenium.WaitForPageToLoad("7000");
            //Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkGroupName");
            //Selenium.WaitForPageToLoad("7000");
            //Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_btnAction");
            //Selenium.WaitForPageToLoad("7000");
            //Selenium.Click("ctl00_MainContent_btnYes");
            //Selenium.Click("link=Users");
            //Selenium.WaitForPageToLoad("7000");
            //Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            //Selenium.WaitForPageToLoad("7000");

            //AssertLabelText("ctl00_MainContent_lbUserGroups", "V P(vladykx) are not participating in any groups");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            Selenium.WaitForPageToLoad("7000");
        }

        /// <summary>
        /// create user and remove user from link = create user
        /// </summary>
        [Test]
        public void AddRemoveUser1()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser8");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "test8@test.test");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");

            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUser8");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("MyInfo.aspx", null);

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsTextPresent("TestUser8"));
        }

        /// <summary>
        /// add user and remove user from button 'Create'
        /// </summary>
        [Test]
        public void AddRemoveUser2()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateUser");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser9");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "test9@test.test");

            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUser9");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("MyInfo.aspx", null);

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsTextPresent("TestUser9"));
        }

        /// <summary>
        /// add user and include him to group
        /// </summary>
        [Test]
        public void IncludeNewUserToGroup()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateUser");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser10");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "test10@test.test");

            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestUser10");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_btnInclude");
            Selenium.WaitForPageToLoad("7000");
            ClickOnButtonWithValue("Include");


            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            AssertLabelText("ctl00_MainContent_lbUserGroups", "TestUser10(TestUser10) participating in following groups:");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsTextPresent("TestUser10"));
        }


        /// <summary>
        /// create user bad( click only on button create)
        /// </summary>
        [Test]
        public void AddUserBad1()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

            AssertIsVisibleTeg("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserNameRequired");
            AssertIsVisibleTeg("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_PasswordRequired");
            AssertIsVisibleTeg("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPasswordRequired");
            AssertIsVisibleTeg("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_EmailRequired");
            AssertIsOnPage("CreateUser.aspx", null);
        }

        /// <summary>
        /// email not present
        /// </summary>
        [Test]
        public void AddUserBad2()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser11");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

            AssertIsVisibleTeg("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_EmailRequired");
            AssertHasText("E-mail is required.");
            AssertIsOnPage("CreateUser.aspx", null);
        }

        /// <summary>
        /// bad confirm password
        /// </summary>
        [Test]
        public void AddUserBad3()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser12");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "asdsad");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

            AssertHasText("The Password and Confirmation Password must match.");
            AssertIsOnPage("CreateUser.aspx", null);

        }

        /// <summary>
        /// user name not present 
        /// </summary>
        [Test]
        public void AddUserBad4()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "asdsad");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

            AssertHasText("User Name is required.");
            AssertIsOnPage("CreateUser.aspx", null);
        }

        /// <summary>
        /// password not present
        /// </summary>
        [Test]
        public void AddUserBad5()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser13");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "asdsad");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password");

            AssertHasText("Password is required.");
            AssertIsOnPage("CreateUser.aspx", null);

        }

        /// <summary>
        /// create multiple users bad not set all
        /// </summary>
        [Test]
        public void AddMultipleUsersBad1()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreateUser");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_lbCreateMultiple");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreate");

            AssertLabelText("ctl00_MainContent_lbErrors", "is not a number");
        }

        /// <summary>
        /// create multiple users bad not set count and password
        /// </summary>
        [Test]
        public void AddMultipleUsersBad2()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreateUser");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_lbCreateMultiple");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_tbPrefix", "TestUser");
            Selenium.Click("ctl00_MainContent_btnCreate");

            AssertLabelText("ctl00_MainContent_lbErrors", "is not a number");
        }

        /// <summary>
        /// create multiple users bad not set password
        /// </summary>
        [Test]
        public void AddMultipleUsersBad3()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_lbCreateMultiple");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_tbPrefix", "TestUser");
            Selenium.Type("ctl00_MainContent_tbCount", "2");
            Selenium.Click("ctl00_MainContent_btnCreate");

            AssertLabelText("ctl00_MainContent_lbErrors", "Password is not specified");

        }

        /// <summary>
        /// create multiple users bad not set sufix name
        /// </summary>
        [Test]
        public void AddMultipleUsersBad4()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_lbCreateMultiple");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_tbCount", "2");
            Selenium.Type("ctl00_MainContent_tbPassword", "2");
            Selenium.Click("ctl00_MainContent_btnCreate");

            AssertLabelText("ctl00_MainContent_lbErrors", "Some of users like these already exist");
        }

        /// <summary>
        /// create multiple users with incorect count
        /// </summary>
        [Test]
        public void AddMultipleUsersBad5()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_lbCreateMultiple");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_tbPrefix", "TestUser");
            Selenium.Type("ctl00_MainContent_tbCount", "count");
            Selenium.Type("ctl00_MainContent_tbPassword", "2");
            Selenium.Click("ctl00_MainContent_btnCreate");

            AssertLabelText("ctl00_MainContent_lbErrors", "count is not a number");
        }


        /// <summary>
        /// create multiple users and remove them
        /// </summary>
        [Test]
        public void AddMultipleUsers1()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_lbCreateMultiple");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbPrefix", "TestUsers000");
            Selenium.Type("ctl00_MainContent_tbCount", "2");
            Selenium.Type("ctl00_MainContent_tbPassword", "1111");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUsers0000");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("MyInfo.aspx", null);

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUsers0001");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("MyInfo.aspx", null);

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            ClickOnLastButtonRemove();
            //Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");

            Assert.IsTrue(Selenium.IsTextPresent("TestUsers0000"));

            ClickOnLastButtonRemove();
            //Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");

            Assert.IsTrue(Selenium.IsTextPresent("TestUsers0001"));
        }


        /// <summary>
        /// create multiple users, make them students and include to old group "123"
        /// </summary>
        [Test]
        public void AddMultipleUsers2()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateUser");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_lbCreateMultiple");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_tbPrefix", "TestUsers6");
            Selenium.Type("ctl00_MainContent_tbCount", "2");
            Selenium.Type("ctl00_MainContent_tbPassword", "1111");
            Selenium.Click("ctl00_MainContent_cbAddToGroup");
            Selenium.Check("ctl00_MainContent_cbMakeStudent");
            Selenium.Select("ctl00_MainContent_cbGroups", "label=123");

            selenium.Click("//option[@value='1']");

            Selenium.Click("ctl00_MainContent_btnCreate");

            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestUsers60");
            Selenium.WaitForPageToLoad("7000");

            AssertCheckBoxState(true, "ctl00_MainContent_cbStudentRole");
            AssertLabelText("ctl00_MainContent_GroupList_gvGroups_ctl02_Label1", "Group");
            Assert.IsTrue(Selenium.IsTextPresent("123"));

            //Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestUsers61");
            Selenium.WaitForPageToLoad("7000");

            AssertCheckBoxState(true, "ctl00_MainContent_cbStudentRole");
            AssertLabelText("ctl00_MainContent_GroupList_gvGroups_ctl02_Label1", "Group");
            Assert.IsTrue(Selenium.IsTextPresent("123"));

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");

            Assert.IsTrue(Selenium.IsTextPresent("TestUsers60"));

            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");

            Assert.IsTrue(Selenium.IsTextPresent("TestUsers61"));
        }


        /// <summary>
        /// create multiple users, make them students and include to new group "NewGroup"
        /// </summary>
        [Test]
        public void AddMultipleUsers3()
        {
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_lbCreateMultiple");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbPrefix", "TestUsers13");
            Selenium.Type("ctl00_MainContent_tbCount", "2");
            Selenium.Type("ctl00_MainContent_tbPassword", "1111");

            Selenium.Click("ctl00_MainContent_cbAddToGroup");
            Selenium.Type("ctl00_MainContent_tbNewGroup", "NewGroup");
            Selenium.Check("ctl00_MainContent_cbMakeStudent");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=TestUsers130");
            Selenium.WaitForPageToLoad("7000");

            AssertCheckBoxState(true, "ctl00_MainContent_cbStudentRole");
            AssertLabelText("ctl00_MainContent_GroupList_gvGroups_ctl02_Label1", "Group");
            Assert.IsTrue(Selenium.IsTextPresent("NewGroup"));

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=TestUsers131");
            Selenium.WaitForPageToLoad("7000");

            AssertCheckBoxState(true, "ctl00_MainContent_cbStudentRole");
            AssertLabelText("ctl00_MainContent_GroupList_gvGroups_ctl02_Label1", "Group");
            Assert.IsTrue(Selenium.IsTextPresent("NewGroup"));

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            //Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            //Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");

            Assert.IsTrue(Selenium.IsTextPresent("TestUsers130"));

            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");

            Assert.IsTrue(Selenium.IsTextPresent("TestUsers131"));
        }


        /// <summary>
        /// search user with his login 
        /// </summary>
        [Test]
        public void SearchUsersWithLogin()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbSearchPattern", "lex");
            Selenium.Click("ctl00_MainContent_btnSearch");

            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_UserList_gvUsers"));
            AssertLabelText("ctl00_MainContent_UserList_gvUsers_ctl03_lbFirstName", "Volodymyr");
            Assert.IsTrue(Selenium.IsTextPresent("Shtenovych"));
        }

        /// <summary>
        /// search user with his second name
        /// </summary>
        [Test]
        public void SearchUsersWithSName()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbSearchPattern", "Shtenovych");
            Selenium.Click("ctl00_MainContent_btnSearch");

            Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_UserList_gvUsers"));
            Assert.IsFalse(Selenium.IsElementPresent("lex"));
        }

        /// <summary>
        /// search user with his email 
        /// </summary>
        [Test]
        public void SearchUsersWithEmail()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbSearchPattern", "ShVolodya@gmail.com");
            Selenium.Click("ctl00_MainContent_btnSearch");

            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_UserList_gvUsers"));
            AssertLabelText("ctl00_MainContent_UserList_gvUsers_ctl03_lbFirstName", "Volodymyr");
            Assert.IsTrue(Selenium.IsTextPresent("Shtenovych"));
        }

        /// <summary>
        /// search user with his first name 
        /// </summary>
        [Test]
        public void SearchUsersWithFName()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbSearchPattern", "Volodymyr");
            Selenium.Click("ctl00_MainContent_btnSearch");

            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_UserList_gvUsers"));
            AssertLabelText("ctl00_MainContent_UserList_gvUsers_ctl03_lbFirstName", "Volodymyr");
            Assert.IsTrue(Selenium.IsTextPresent("Shtenovych"));
        }

        /// <summary>
        /// search user with his substring 
        /// </summary>
        [Test]
        public void SearchUsersSubString()
        {
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbSearchPattern", "dy");
            Selenium.Click("ctl00_MainContent_btnSearch");

            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_UserList_gvUsers"));
            AssertLabelText("ctl00_MainContent_UserList_gvUsers_ctl03_lbFirstName", "Volodymyr");
            Assert.IsTrue(Selenium.IsTextPresent("Shtenovych"));
            Assert.IsTrue(Selenium.IsTextPresent("vladykx"));
        }
    }
}
