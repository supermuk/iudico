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
        [TearDown]
        public void TearDown()
        {
            Selenium.Open("/Logout.aspx");
        }

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
            Selenium.WaitForPageToLoad("7000");
            
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

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_TextBox_FirstName", "NewFName");
            Selenium.Click("ctl00_MainContent_Button_Update");
            
            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_TextBox_FirstName", "Volodymyr");
            Selenium.Click("ctl00_MainContent_Button_Update");
            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("StudentPage.aspx", null);
            AssertLabelText("ctl00_MainContent__headerLabel", "Student Page For: Volodymyr Shtenovych");
            
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

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_TextBox_SecondName", "NewSName");
            Selenium.Click("ctl00_MainContent_Button_Update");

            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_TextBox_SecondName", "Shtenovych");
            Selenium.Click("ctl00_MainContent_Button_Update");
            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("StudentPage.aspx", null);
            AssertLabelText("ctl00_MainContent__headerLabel", "Student Page For: Volodymyr Shtenovych");
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

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_TextBox_Email", "mail@mail.mail");
            Selenium.Click("ctl00_MainContent_Button_Update");

            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_TextBox_Email", "ShVolodya@gmail.com");
            Selenium.Click("ctl00_MainContent_Button_Update");
            
            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertTextBoxValue("ctl00_MainContent_TextBox_Email", "ShVolodya@gmail.com");
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

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_TextBox_FirstName", "NewFName");
            Selenium.Type("ctl00_MainContent_TextBox_SecondName", "NewSName");
            Selenium.Type("ctl00_MainContent_TextBox_Email", "NewMail@n.n");
            Selenium.Click("ctl00_MainContent_Button_Update");

            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_TextBox_FirstName", "Volodymyr");
            Selenium.Type("ctl00_MainContent_TextBox_SecondName", "Shtenovych");
            Selenium.Type("ctl00_MainContent_TextBox_Email", "ShVolodya@gmail.com");
            Selenium.Click("ctl00_MainContent_Button_Update");
            
            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("30000");
            
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertTextBoxValue("ctl00_MainContent_TextBox_FirstName", "Volodymyr");
            AssertTextBoxValue("ctl00_MainContent_TextBox_SecondName", "Shtenovych");
            AssertTextBoxValue("ctl00_MainContent_TextBox_Email", "ShVolodya@gmail.com");
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
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "badpassword");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "newpassword");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "newpassword");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertHasText("Password incorrect or New Password invalid. New Password length minimum: 3. Non-alphanumeric characters required: 0.");
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
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "lex");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "lex");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertHasText("Password incorrect or New Password invalid. New Password length minimum: 3. Non-alphanumeric characters required: 0.");
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

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");
            
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "1111");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "11111");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertHasText("The Confirm New Password must match the New Password entry.");
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

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "11111");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertHasText("The Confirm New Password must match the New Password entry.");
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

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertIsVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPasswordRequired");
            AssertIsVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPasswordRequired");
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

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "newpassword");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertIsNotVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPasswordRequired");
            AssertIsVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPasswordRequired");
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

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "newpassword");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "newpassword");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertIsVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPasswordRequired");
            AssertIsNotVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPasswordRequired");
            AssertIsNotVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPasswordRequired");
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

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "newpassword");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertIsOnPage("MyInfo.aspx", null);
            AssertIsVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPasswordRequired");
            AssertIsNotVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPasswordRequired");
            AssertIsVisibleTeg("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPasswordRequired");
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
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "lex1");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "lex1");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");
            Selenium.Click("ctl00_MainContent_ChangePassword_SuccessContainerID_ContinuePushButton");
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex1");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex1");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "lex");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "lex");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");
            Selenium.Click("ctl00_MainContent_ChangePassword_SuccessContainerID_ContinuePushButton");
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            AssertIsOnPage("StudentPage.aspx", null);
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
            Selenium.Click("link=My permissions");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("MyPermissions.aspx", null);
            AssertHasText("You don't have permissions to any of Course");
            AssertHasText("You don't have permissions to any of Theme");
            AssertHasText("You don't have permissions to any of Stage");
            AssertHasText("You don't have permissions to any of Group");
            AssertHasText("You don't have permissions to any of Curriculum");
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
            Selenium.Click("link=Create Group");
            Selenium.WaitForPageToLoad("30000");
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
            Selenium.WaitForPageToLoad("30000");
            
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkGroupName");
            Selenium.WaitForPageToLoad("30000");
            
            Selenium.Type("ctl00_MainContent_tbGroupName", "12");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("30000");
            
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkGroupName");
            Selenium.WaitForPageToLoad("30000");
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
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "TestGroup1");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Groups.aspx", null);
        }

        /// <summary>
        /// give role trainer to user
        /// </summary>
        [Test]
        public void Test018_UserRoleTrainer()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("30000");
            
            Selenium.Click("ctl00_MainContent_cbTrainerRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("ctl00_MainContent_cbTrainerRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("EditUser.aspx", null);
            AssertCheckBoxState(false, "ctl00_MainContent_cbTrainerRole");
        }

        /// <summary>
        ///  give role lector to user
        /// </summary>
        [Test]
        public void Test019_UserRoleLector()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("ctl00_MainContent_cbLectorRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("ctl00_MainContent_cbLectorRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("EditUser.aspx", null);
            AssertCheckBoxState(false, "ctl00_MainContent_cbLectorRole");
        }

        /// <summary>
        ///  give role admin to user
        /// </summary>
        [Test]
        public void Test020_UserRoleAdmin()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("ctl00_MainContent_cbAdminRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Click("ctl00_MainContent_cbAdminRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("EditUser.aspx", null);
            AssertCheckBoxState(false, "ctl00_MainContent_cbAdminRole");
        }
         
        /// <summary>
        /// include groupe to user
        /// </summary>
        [Test]
        public void Test021_IncludeGroupToOldUser()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("//tr[@id='ctl00_MainMenun14']/td/table/tbody/tr/td");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl04_lbLogin");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnInclude");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_gvGroups_ctl02_lnkSelect");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnYes");

            AssertLabelText("ctl00_MainContent_GroupList_gvGroups_ctl02_Label1", "Group");
        }
      
        /// <summary>
        /// create user and remove user from link = create user
        /// </summary>
        [Test]
        public void Test022_AddRemoveUser()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");
            
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser14");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "test14@test.test");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");
            
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");
            
            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUser14");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");
            
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            ClickOnLastButtonRemove();
            //selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.Type("ctl00_MainContent_tbSearchPattern", "TestUser14");
            Selenium.Click("ctl00_MainContent_btnSearch");

            AssertNoLabel("ctl00_MainContent_UserList_gvUsers_ctl03_lbFirstName");
            AssertIsNotTextAvailable("TestUser14");
            //AssertHtmlText("ctl00_globalUpdatePanel", "Users available in the system");
        }

        /// <summary>
        /// add user and remove user from button 'Create'
        /// </summary>
        [Test]
        public void Test023_AddRemoveUser()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreateUser");
            Selenium.WaitForPageToLoad("30000");
            
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser20");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "test20@test.test");

            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");
            
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");
            
            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUser20");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            ClickOnLastButtonRemove();
            //selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnYes");
            
            Selenium.Type("ctl00_MainContent_tbSearchPattern", "TestUser20");
            Selenium.Click("ctl00_MainContent_btnSearch");

            AssertIsNotTextAvailable("TestUser20");
        }

        /// <summary>
        /// add user and include him to group
        /// </summary>
        [Test]
        public void Test024_IncludeNewUserToGroup()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreateUser");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser21");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "test21@test.test");

            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");

            Selenium.Click("link=TestUser21");
            Selenium.WaitForPageToLoad("30000");
            
            Selenium.Click("ctl00_MainContent_btnInclude");
            Selenium.WaitForPageToLoad("30000");
            ClickOnButtonWithValue("Include");

            //Selenium.Click("ctl00_MainContent_gvGroups_ctl02_lnkSelect");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnYes");

            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            ClickOnLastButtonRemove();
            //selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnYes");

            Selenium.Type("ctl00_MainContent_tbSearchPattern", "TestUser21");
            Selenium.Click("ctl00_MainContent_btnSearch");

            AssertIsNotTextAvailable("TestUser21");
        }
        
    }
}
