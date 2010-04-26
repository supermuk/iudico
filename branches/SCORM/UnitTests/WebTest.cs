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
        //change first and second name and mail
        [Test]
        public void ChangeInfo()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_TextBox_FirstName", "Volodymyra");
            Selenium.Type("ctl00_MainContent_TextBox_SecondName", "Shtenovycha");
            Selenium.Type("ctl00_MainContent_TextBox_Email", "ShVolodya@ukr.net");
            Selenium.Click("ctl00_MainContent_Button_Update");


            AssertIsOnPage("MyInfo.aspx", null);
            AssertTextBoxValue("ctl00_MainContent_TextBox_FirstName", "Volodymyra");
            AssertTextBoxValue("ctl00_MainContent_TextBox_SecondName", "Shtenovycha");
            AssertTextBoxValue("ctl00_MainContent_TextBox_Email", "ShVolodya@ukr.net");
        }

        //change password bad
        [Test]
        public void ChangePasswordBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=My Personal Info");
            Selenium.WaitForPageToLoad("30000");

            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_CurrentPassword", "lexaaaa");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_NewPassword", "lexbbbbb");
            Selenium.Type("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ConfirmNewPassword", "lexbbbbb");
            Selenium.Click("ctl00_MainContent_ChangePassword_ChangePasswordContainerID_ChangePasswordPushButton");

            AssertHasText("Password incorrect or New Password invalid. New Password length minimum: 3. Non-alphanumeric characters required: 0.");
        }
        //change password good
        [Test]
        public void ChangePasswordGood()
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

        //create new group
        [Test]
        public void CreateNewGroupGood()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Create Group");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "new_group1");
            Selenium.Click("ctl00_MainContent_btnCreate");

            AssertLabelText("ctl00_MainContent_lbTitle", "Edit group new_group1");
            AssertIsOnPage("EditGroup.aspx", null);
            
        }

        /// <summary>
        /// create group bad
        /// </summary>
        [Test]
        public void CreateNewGroupBad()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Create Group");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreate");

            AssertIsOnPage("CreateGroup.aspx", null);
        }

        /// <summary>
        /// my permissions
        /// </summary>
        [Test]
        public void MyPermissions()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=My permissions");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("MyPermissions.aspx", null);
            AssertCheckBoxState(true, "ctl00$MainContent$uplPermissions$ctl02$PermissionsGrid$ctl02$ctl00");
        }
        /// <summary>
        /// create new user
        /// </summary>
        [Test]
        public void CreateNewuser()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestUser2");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1111");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "TestUser2@gmail.com");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");

            AssertIsOnPage("Users.aspx", null);
        }

        /// <summary>
        /// Enter user
        /// </summary>
        [Test]
        public void EnterUser()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestUser2");
            Selenium.Type("ctl00_MainContent_Login1_Password", "1111");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            AssertIsOnPage("StudentPage.aspx", null);
            AssertLabelText("ctl00_MainContent__headerLabel", "Student Page For: TestUser2");
        }
        /// <summary>
        /// give user role 
        /// </summary>
        [Test]
        public void GiveUserRole()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl06_lbLogin");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_cbStudentRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl06_lbLogin");

            AssertCheckBoxState(true, "ctl00_MainContent_cbStudentRole");
        }

        /// <summary>
        /// include group to user
        /// </summary>
        [Test]
        public void IncludeGroupToUser()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl06_lbLogin");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnInclude");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_gvGroups_ctl04_lnkSelect");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnYes");

            AssertIsOnPage("EditUser.aspx", null);
        }

        /// <summary>
        /// remove user
        /// </summary>
        [Test]
        public void RemoveUser()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl06_btnAction");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnYes");

            AssertIsOnPage("Users.aspx", null);
        }

        /// <summary>
        /// import course
        /// </summary>
        [Test]
        public void ImportCourse()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "NewCourse1");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "NC1");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "D:\\3 курс\\Перший семестр\\Програмування\\Курсовий проект\\Гурський Іван\\Static.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("CourseEdit.aspx", null);
            AssertLabelText("ctl00_MainContent_TreeView_Coursesn4", "NewCourse1");
        }

        /// <summary>
        /// remove course
        /// </summary>
        [Test]
        public void RemoveCourse()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest4");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("CourseEdit.aspx", null);
        }

        /// <summary>
        /// create group lector
        /// </summary>
        [Test]
        public void CreateGroupLector()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "NewGroup1");
            Selenium.Click("ctl00_MainContent_btnCreate");

            AssertIsOnPage("EditGroup.aspx", null);
            AssertLabelText("ctl00_MainContent_lbTitle", "Edit group NewGroup1");
        }

        /// <summary>
        /// remove group lector
        /// </summary>
        [Test]
        public void RemoveGroupLector()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl05_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl05_btnOK");
            Selenium.WaitForPageToLoad("30000");

            AssertNoLabel("ctl00_MainContent_GroupList_gvGroups_ctl02_Label1");
        }
    }
}
