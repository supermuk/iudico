using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IUDICO.UnitTest.Base;

namespace IUDICO.UnitTest.Functional
{
    [TestFixture]
    public class UserTests: TestFixtureWeb
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
        #region ChangeUserInfo
        
        /// <summary>
        /// change first name
        /// </summary>
        [Test]
        public void ChangeFirstName()
        {

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
        }

        /// <summary>
        /// change second name
        /// </summary>
        [Test]
        public void ChangeSecondName()
        {
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
        }

        /// <summary>
        /// change first name
        /// </summary>
        [Test]
        public void ChangeEmail()
        {
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
        }

        /// <summary>
        /// change info all
        /// </summary>
        [Test]
        public void ChangeInfoAll()
        {
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
        #endregion

        #region ChangePassword

        ///// <summary>
        ///// change password bad
        ///// </summary>
        //[Test]
        //public void ChangePasswordTooSmall()
        //{
        //    Selenium.Open("/Login.aspx");
        //    Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
        //    Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
        //    Selenium.Click("ctl00_MainContent_Login1_LoginButton");
        //    Selenium.WaitForPageToLoad("7000");
        //    Selenium.Click("link=My Personal Info");
        //    Selenium.WaitForPageToLoad("7000");


        //    Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lex");
        //    Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "lex");
        //    Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "lex");
        //    Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");
        //    Selenium.WaitForPageToLoad("7000");

        //    AssertIsOnPage("MyInfo.aspx", null);
        //    AssertHasText("Password incorrect or New Password invalid. New Password length minimum: 3. Non-alphanumeric characters required: 0.");
        //}

        /// <summary>
        /// change password bad
        /// </summary>
        [Test]
        public void ChangePasswordInvalidCurrentPassword()
        {
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");

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
        public void ChangePasswordSamePassword()
        {
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");

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
        public void ChangePasswordConfirmDontMatch()
        {
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");

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
        public void ChangePasswordConfirmNotSet()
        {
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");

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
        public void ChangePasswordNewPasswordAndConfirmNotSet()
        {
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");
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
        public void ChangePasswordBad()
        {
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");
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
        public void ChangePasswordCurrentPasswordNotSet()
        {
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");

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
        public void ChangePasswordCurrentAndConfirmNotSet()
        {
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("7000");

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
        public void ChangePassword()
        {
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
        }

        #endregion

        #region Permissions
        /// <summary>
        /// my permissions
        /// </summary>
        [Test]
        public void MyPermissionsUser()
        {
            Selenium.Click("link=My permissions");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("MyPermissions.aspx", null);
            AssertHasText("Courses you have access to:");
            AssertHasText("Themes you have access to:");
            AssertHasText("Stages you have access to:");
            AssertHasText("Groups you have access to:");
            AssertHasText("Curriculums you have access to:");
            AssertCheckBoxState(true, "ctl00_MainContent_uplPermissions_ctl11_PermissionsGrid_ctl02_ctl00");
        }
        #endregion

        
    }
}
