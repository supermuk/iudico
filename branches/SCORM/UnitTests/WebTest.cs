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
        /// change first name
        /// </summary>
        [Test]
        public void Test001_ChangeFirstName()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("3000");
            
            Selenium.Type("ctl00_MainContent_TextBox_FirstName", "NewFName");
            Selenium.Click("ctl00_MainContent_Button_Update");
            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("3000");

            AssertIsOnPage("StudentPage.aspx", null);
            AssertLabelText("ctl00_MainContent__headerLabel", "Student Page For: NewFName Shtenovych");

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("3000");
            
            Selenium.Type("ctl00_MainContent_TextBox_FirstName", "Volodymyr");
            Selenium.Click("ctl00_MainContent_Button_Update");
            //Selenium.WaitForPageToLoad("3000");
            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("3000");

            AssertIsOnPage("StudentPage.aspx", null);
            AssertLabelText("ctl00_MainContent__headerLabel", "Student Page For: Volodymyr Shtenovych");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            
        }

        /// <summary>
        /// change second name
        /// </summary>
        [Test]
        public void Test002_ChangeSecondName()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_SecondName", "NewSName");
            Selenium.Click("ctl00_MainContent_Button_Update");

            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("7000");

            AssertLabelText("ctl00_MainContent__headerLabel", "Student Page For: Volodymyr NewSName");

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_SecondName", "Shtenovych");
            Selenium.Click("ctl00_MainContent_Button_Update");
            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("StudentPage.aspx", null);
            AssertLabelText("ctl00_MainContent__headerLabel", "Student Page For: Volodymyr Shtenovych");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// change first name
        /// </summary>
        [Test]
        public void Test003_ChangeEmail()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_Email", "mail@mail.mail");
            Selenium.Click("ctl00_MainContent_Button_Update");

            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_Email", "ShVolodya@gmail.com");
            Selenium.Click("ctl00_MainContent_Button_Update");
            
            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertTextBoxValue("ctl00_MainContent_TextBox_Email", "ShVolodya@gmail.com");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// change info all
        /// </summary>
        [Test]
        public void Test004_ChangeInfoAll()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_FirstName", "NewFName");
            Selenium.Type("ctl00_MainContent_TextBox_SecondName", "NewSName");
            Selenium.Type("ctl00_MainContent_TextBox_Email", "NewMail@n.n");
            Selenium.Click("ctl00_MainContent_Button_Update");

            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("7000");

            AssertLabelText("ctl00_MainContent__headerLabel", "Student Page For: NewFName NewSName");

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_FirstName", "Volodymyr");
            Selenium.Type("ctl00_MainContent_TextBox_SecondName", "Shtenovych");
            Selenium.Type("ctl00_MainContent_TextBox_Email", "ShVolodya@gmail.com");
            Selenium.Click("ctl00_MainContent_Button_Update");
            
            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("7000");
            
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertTextBoxValue("ctl00_MainContent_TextBox_FirstName", "Volodymyr");
            AssertTextBoxValue("ctl00_MainContent_TextBox_SecondName", "Shtenovych");
            AssertTextBoxValue("ctl00_MainContent_TextBox_Email", "ShVolodya@gmail.com");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// change password bad
        /// </summary>
        [Test]
        public void Test005_ChangePasswordBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "badpassword");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "newpassword");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "newpassword");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertHasText("Password incorrect or New Password invalid. New Password length minimum: 3. Non-alphanumeric characters required: 0.");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// change password bad
        /// </summary>
        [Test]
        public void Test006_ChangePasswordBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "lex");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "lex");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertHasText("Password incorrect or New Password invalid. New Password length minimum: 3. Non-alphanumeric characters required: 0.");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// change password bad
        /// </summary>
        [Test]
        public void Test007_ChangePasswordBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");
            
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "1111");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "11111");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertHasText("The Confirm New Password must match the New Password entry.");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// change password bad
        /// </summary>
        [Test]
        public void Test008_ChangePasswordBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "11111");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertHasText("The Confirm New Password must match the New Password entry.");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// change password bad
        /// </summary>
        [Test]
        public void Test012_ChangePasswordBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertIsVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPasswordRequired");
            AssertIsVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPasswordRequired");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// change password bad
        /// </summary>
        [Test]
        public void Test009_ChangePasswordBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "newpassword");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertIsNotVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPasswordRequired");
            AssertIsVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPasswordRequired");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// change password bad
        /// </summary>
        [Test]
        public void Test010_ChangePasswordBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "newpassword");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "newpassword");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertIsVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPasswordRequired");
            AssertIsNotVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPasswordRequired");
            AssertIsNotVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPasswordRequired");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// change password bad
        /// </summary>
        [Test]
        public void Test011_ChangePasswordBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "newpassword");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertIsVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPasswordRequired");
            AssertIsNotVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPasswordRequired");
            AssertIsVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPasswordRequired");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// change password good
        /// </summary>
        [Test]
        public void Test013_ChangePasswordGood()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "lex1");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "lex1");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");
            Selenium.Click("ctl00_MainContent_ChangePassword_SuccessContainerID_ContinuePushButton");
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex1");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex1");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "lex");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "lex");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");
            Selenium.Click("ctl00_MainContent_ChangePassword_SuccessContainerID_ContinuePushButton");
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            AssertIsOnPage("StudentPage.aspx", null);

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// my permissions
        /// </summary>
        [Test]
        public void Test014_MyPermissions()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My permissions");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("MyPermissions.aspx", null);
            AssertHasText("You don't have permissions to any of Course");
            AssertHasText("You don't have permissions to any of Theme");
            AssertHasText("You don't have permissions to any of Stage");
            AssertHasText("You don't have permissions to any of Group");
            AssertHasText("You don't have permissions to any of Curriculum");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

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
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Create Group");
            Selenium.WaitForPageToLoad("5000");
            Selenium.Click("ctl00_MainContent_btnCreate");
            
            AssertIsOnPage("CreateGroup.aspx", null);

            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("30000");

            AssertCountButtonOnPage();

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
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
            Selenium.WaitForPageToLoad("30000");
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

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
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
            Selenium.WaitForPageToLoad("7000");
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

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// out role
        /// </summary>
        [Test]
        public void Test018_UserRoleAnyone()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
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

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// give role student to user
        /// </summary>
        [Test]
        public void Test018_UserRoleStudent()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");
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

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// give role trainer to user
        /// </summary>
        [Test]
        public void Test019_UserRoleTrainer()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");
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

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        ///  give role lector to user
        /// </summary>
        [Test]
        public void Test020_UserRoleLector()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");
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

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        ///  give role admin to user
        /// </summary>
        [Test]
        public void Test021_UserRoleAdmin()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
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

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// give role super_admin to user
        /// </summary>
        [Test]
        public void Test022_UserRoleSuper_Admin()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");
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

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// give all roles to user
        /// </summary>
        [Test]
        public void Test023_UserAllRole()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");
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

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }
         
        /// <summary>
        /// include group to user
        /// </summary>
        [Test]
        public void Test024_IncludeGroupToOldUser()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

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

            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkGroupName");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_btnAction");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("7000");

            AssertLabelText("ctl00_MainContent_lbUserGroups", "V P(vladykx) are not participating in any groups");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }
      
        /// <summary>
        /// create user and remove user from link = create user
        /// </summary>
        [Test]
        public void Test025_AddRemoveUser()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");
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

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// add user and remove user from button 'Create'
        /// </summary>
        [Test]
        public void Test026_AddRemoveUser()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");
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

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// add user and include him to group
        /// </summary>
        [Test]
        public void Test027_IncludeNewUserToGroup()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");
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

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }


        /// <summary>
        /// create user bad( click only on button create)
        /// </summary>
        [Test]
        public void Test028_AddUserBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");


            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

            AssertIsVisibleTeg("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserNameRequired");
            AssertIsVisibleTeg("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_PasswordRequired");
            AssertIsVisibleTeg("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPasswordRequired");
            AssertIsVisibleTeg("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_EmailRequired");
            AssertIsOnPage("CreateUser.aspx", null);

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Login.aspx", null);
        }

        /// <summary>
        /// email not present
        /// </summary>
        [Test]
        public void Test029_AddUserBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser11");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

            AssertIsVisibleTeg("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_EmailRequired");
            AssertHasText("E-mail is required.");
            AssertIsOnPage("CreateUser.aspx", null);

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Login.aspx", null);
        }

        /// <summary>
        /// bad confirm password
        /// </summary>
        [Test]
        public void Test030_AddUserBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser12");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "asdsad");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

            AssertHasText("The Password and Confirmation Password must match.");
            AssertIsOnPage("CreateUser.aspx", null);

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Login.aspx", null);

        }

        /// <summary>
        /// user name not present 
        /// </summary>
        [Test]
        public void Test031_AddUserBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "asdsad");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

            AssertHasText("User Name is required.");
            AssertIsOnPage("CreateUser.aspx", null);

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Login.aspx", null);
        }

        /// <summary>
        /// password not present
        /// </summary>
        [Test]
        public void Test032_AddUserBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser13");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "asdsad");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password");

            AssertHasText("Password is required.");
            AssertIsOnPage("CreateUser.aspx", null);

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Login.aspx", null);

        }

        /// <summary>
        /// create multiple users bad not set all
        /// </summary>
        [Test]
        public void Test033_AddMultipleUsersBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreateUser");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_lbCreateMultiple");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreate");

            AssertLabelText("ctl00_MainContent_lbErrors", "is not a number");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Login.aspx", null);
        }

        /// <summary>
        /// create multiple users bad not set count and password
        /// </summary>
        [Test]
        public void Test034_AddMultipleUsersBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreateUser");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_lbCreateMultiple");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_tbPrefix", "TestUser");
            Selenium.Click("ctl00_MainContent_btnCreate");

            AssertLabelText("ctl00_MainContent_lbErrors", "is not a number");
            
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Login.aspx", null);
        }

        /// <summary>
        /// create multiple users bad not set password
        /// </summary>
        [Test]
        public void Test035_AddMultipleUsersBad()
		{
			Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

			Selenium.Click("link=Create User");
			Selenium.WaitForPageToLoad("30000");
			Selenium.Click("ctl00_MainContent_lbCreateMultiple");
			Selenium.WaitForPageToLoad("30000");
			Selenium.Type("ctl00_MainContent_tbPrefix", "TestUser");
			Selenium.Type("ctl00_MainContent_tbCount", "2");
			Selenium.Click("ctl00_MainContent_btnCreate");

            AssertLabelText("ctl00_MainContent_lbErrors", "Password is not specified");

			Selenium.Click("ctl00_hypLogout");
			Selenium.Click("ctl00_btnOK");
			Selenium.WaitForPageToLoad("30000");

		}

        /// <summary>
        /// create multiple users bad not set sufix name
        /// </summary>
        [Test]
		public void Test036_AddMultipleUsersBad()
		{
			Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

			Selenium.Click("link=Create User");
			Selenium.WaitForPageToLoad("30000");
			Selenium.Click("ctl00_MainContent_lbCreateMultiple");
			Selenium.WaitForPageToLoad("30000");
			Selenium.Type("ctl00_MainContent_tbCount", "2");
			Selenium.Type("ctl00_MainContent_tbPassword", "2");
			Selenium.Click("ctl00_MainContent_btnCreate");
			
            AssertLabelText("ctl00_MainContent_lbErrors", "Some of users like these already exist");

			Selenium.Click("ctl00_hypLogout");
			Selenium.Click("ctl00_btnOK");
			Selenium.WaitForPageToLoad("30000");
		}

        /// <summary>
        /// create multiple users with incorect count
        /// </summary>
        [Test]
        public void Test037_AddMultipleUsersBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_lbCreateMultiple");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_tbPrefix", "TestUser");
            Selenium.Type("ctl00_MainContent_tbCount", "count");
            Selenium.Type("ctl00_MainContent_tbPassword", "2");
            Selenium.Click("ctl00_MainContent_btnCreate");

            AssertLabelText("ctl00_MainContent_lbErrors", "count is not a number");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");
        }


        /// <summary>
        /// create multiple users and remove them
        /// </summary>
        [Test]
        public void Test038_AddMultipleUsers()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_lbCreateMultiple");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbPrefix", "TestUsers00");
            Selenium.Type("ctl00_MainContent_tbCount", "2");
            Selenium.Type("ctl00_MainContent_tbPassword", "1111");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");
            
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUsers000");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("MyInfo.aspx", null);

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");
            
            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUsers001");
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

            Assert.IsTrue(Selenium.IsTextPresent("TestUsers000"));
            
            ClickOnLastButtonRemove();
            //Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");

            Assert.IsTrue(Selenium.IsTextPresent("TestUsers001"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");
        }


        /// <summary>
        /// create multiple users, make them students and include to old group "123"
        /// </summary>
        [Test]
        public void Test039_AddMultipleUsers()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

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

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }


        /// <summary>
        /// create multiple users, make them students and include to new group "NewGroup"
        /// </summary>
        [Test]
        public void Test040_AddMultipleUsers()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

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

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }


        /// <summary>
        /// search user with his login 
        /// </summary>
        [Test]
        public void Test041_SearchUsersWithLogin()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbSearchPattern", "lex");
            Selenium.Click("ctl00_MainContent_btnSearch");

            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_UserList_gvUsers"));
            AssertLabelText("ctl00_MainContent_UserList_gvUsers_ctl03_lbFirstName", "Volodymyr");
            Assert.IsTrue(Selenium.IsTextPresent("Shtenovych"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// search user with his second name
        /// </summary>
        [Test]
        public void Test042_SearchUsersWithSName()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbSearchPattern", "Shtenovych");
            Selenium.Click("ctl00_MainContent_btnSearch");

            Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_UserList_gvUsers"));
            Assert.IsFalse(Selenium.IsElementPresent("lex"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// search user with his email 
        /// </summary>
        [Test]
        public void Test043_SearchUsersWithEmail()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbSearchPattern", "ShVolodya@gmail.com");
            Selenium.Click("ctl00_MainContent_btnSearch");

            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_UserList_gvUsers"));
            AssertLabelText("ctl00_MainContent_UserList_gvUsers_ctl03_lbFirstName", "Volodymyr");
            Assert.IsTrue(Selenium.IsTextPresent("Shtenovych"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// search user with his first name 
        /// </summary>
        [Test]
        public void Test044_SearchUsersWithFName()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbSearchPattern", "Volodymyr");
            Selenium.Click("ctl00_MainContent_btnSearch");

            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_UserList_gvUsers"));
            AssertLabelText("ctl00_MainContent_UserList_gvUsers_ctl03_lbFirstName", "Volodymyr");
            Assert.IsTrue(Selenium.IsTextPresent("Shtenovych"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }

        /// <summary>
        /// search user with his substring 
        /// </summary>
        [Test]
        public void Test045_SearchUsersSubString()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbSearchPattern", "dy");
            Selenium.Click("ctl00_MainContent_btnSearch");

            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_UserList_gvUsers"));
            AssertLabelText("ctl00_MainContent_UserList_gvUsers_ctl03_lbFirstName", "Volodymyr");
            Assert.IsTrue(Selenium.IsTextPresent("Shtenovych"));
            Assert.IsTrue(Selenium.IsTextPresent("vladykx"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
        }
    }
}
