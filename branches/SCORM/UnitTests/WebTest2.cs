using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IUDICO.UnitTest.Base;
using Selenium;
using System.Text.RegularExpressions;
using System.Threading;

namespace IUDICO.UnitTest
{
	[TestFixture]
    public class WebTest2 : TestFixtureWeb
	{
       private StringBuilder verificationErrors;



        ///User Autorization
        /// <summary>
        /// Correct Login
        /// </summary>
		[Test]
		public void Test01_CorrectLogin()
		{
			Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("30000");
			Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
			Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
			Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            AssertIsOnPage("StudentPage.aspx", null);
		}
        /// <summary>
        /// InCorrect Login
        /// </summary>
        [Test]
        public void Test02_InCorrectLogin()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("3000");
            Selenium.Type("ctl00$MainContent$Login1$UserName", "baduser");
            Selenium.Type("ctl00$MainContent$Login1$Password", "baduser");
            Selenium.Click("ctl00$MainContent$Login1$LoginButton");

            AssertHasText("Your login attempt was not successful. Please try again.");
            AssertIsOnPage("Login.aspx", null);
        }

        /// <summary>
        /// Logout Test
        /// </summary>
        [Test]
        public void Test03_LogoutTest()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("3000");
            Selenium.Type("ctl00$MainContent$Login1$UserName", "lex");
            Selenium.Type("ctl00$MainContent$Login1$Password", "lex");
            Selenium.Click("ctl00$MainContent$Login1$LoginButton");
            Pause(3000);
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Login.aspx", null);
        }


         /// <summary>
        /// Teacher Tests
        /// </summary>
        
        //import corse
        [Test, Ignore]
        public void Test04_ImportCourse()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00$MainContent$Login1$UserName", "lex");
            Selenium.Type("ctl00$MainContent$Login1$Password", "lex");
            Selenium.Click("ctl00$MainContent$Login1$LoginButton");
            Selenium.WaitForPageToLoad("2000");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("30000");

            AssertHtmlText("ctl00_MainContent_TreeView_Coursest0", "TestCourse");
            AssertHasText("TestCourse");
            AssertIsOnPage("CourseEdit.aspx", null);

            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.Click("ctl00_MainContent_Button_Delete");
        }

        //imports bad course
        [Test, Ignore]
        public void Test05_ImportBadCourse()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00$MainContent$Login1$UserName", "lex");
            Selenium.Type("ctl00$MainContent$Login1$Password", "lex");
            Selenium.Click("ctl00$MainContent$Login1$LoginButton");
            Selenium.WaitForPageToLoad("2000");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("30000");

            AssertHtmlText("ctl00_MainContent_Label_PageMessage", "No imsmanifest.xml file found");
            AssertIsOnPage("CourseEdit.aspx", null);
        }

        //create and delete course
        [Test, Ignore]
        public void Test06_DeleteCourse()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Courses");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("CourseEdit.aspx", null);

        }

        //create 2 courses & delete first
        [Test, Ignore]
        public void Test07_DeleteCourse2()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Courses");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse2");
            Selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse2");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("CourseEdit.aspx", null);

            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.Click("ctl00_MainContent_Button_Delete");
        }

        //create 2 courses & delete second
        [Test, Ignore]
        public void Test08_DeleteCourse3()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Courses");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse2");
            Selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse2");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest2");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("CourseEdit.aspx", null);

            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.Click("ctl00_MainContent_Button_Delete");

        }

        //create group
        [Test]
        public void Test09_CreateGroup()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "Test_Group");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Groups.aspx", null);
            AssertHasText("Test_Group");

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
        }

        //create & change group name
        [Test]
        public void Test10_ChangeNameGroup()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "Test_Group");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.Click("ctl00_MainContent_tbGroupName");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Group");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Groups.aspx", null);
            AssertHasText("New_Group");

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
        }

        //create & change group name
        [Test]
        public void Test11_ChangeNameGroup2()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("2000");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "Test_Group");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkGroupName");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_tbGroupName");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Group");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Groups.aspx", null);
            AssertHasText("New_Group");

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
        }

        //create & delete group
        [Test]
        public void Test12_DeleteGroup()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Click("ctl00_MainContent_Login1_UserName");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "Test_Group");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Groups.aspx", null);
        }

        //create curriculum
        [Test]
        public void Test13_CreateCurriculum()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Click("ctl00_MainContent_Login1_UserName");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            AssertHasText("Curriculum_test");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.Click("ctl00_MainContent_Button_Delete");
        }

        //change name of stage
        [Test]
        public void Test14_ModifyCurriculum()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Click("ctl00_MainContent_Login1_UserName");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "New_name");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "New_name");
            Selenium.Click("ctl00_MainContent_Button_Modify");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "");
            Selenium.Click("ctl00_MainContent_TextBox_Description");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            AssertHasText("New_name");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.Click("ctl00_MainContent_Button_Delete");
        }

        //change name of curriculum
        [Test]
        public void Test15_ModifyCurriculum2()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Click("ctl00_MainContent_Login1_UserName");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "New_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "New_curriculum");
            Selenium.Click("ctl00_MainContent_Button_Modify");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "");
            Selenium.Click("ctl00_MainContent_TextBox_Description");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            AssertHasText("New_curriculum");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.Click("ctl00_MainContent_Button_Delete");
        }

        //delete theme from curriculum
        [Test]
        public void Test16_DeleteCuriculum()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Click("ctl00_MainContent_Login1_UserName");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");

            AssertIsOnPage("CurriculumEdit.aspx", null);

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.Click("ctl00_MainContent_Button_Delete");
        }

        //delete stage from curriculum
        [Test]
        public void Test17_DeleteCuriculum2()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Click("ctl00_MainContent_Login1_UserName");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("ctl00_MainContent_Button_Delete");

            AssertIsOnPage("CurriculumEdit.aspx", null);

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.Click("ctl00_MainContent_Button_Delete");
        }

        //delete curriculum
        [Test]
        public void Test18_DeleteCuriculum3()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Click("ctl00_MainContent_Login1_UserName");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("ctl00_MainContent_Button_Delete");

            AssertIsOnPage("CurriculumEdit.aspx", null);

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.Click("ctl00_MainContent_Button_Delete");
        }
        /*
        //add assignment
        [Test]
        public void Test19_Add_Assignment()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Click("ctl00_MainContent_Login1_UserName");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");

            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_tbGroupName");
            Selenium.Type("ctl00_MainContent_tbGroupName", "Ass_test_group");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Ass_course");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Ass_test");
            Selenium.Click("ctl00_MainContent_FileUpload_Course");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Ass_curric");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Ass_curric");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Ass_curric']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Ass_course']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("link=Assignment");
            Selenium.WaitForPageToLoad("30000");
            Selenium.Select("ctl00_MainContent_GroupList", "label=Ass_test_group");
            Selenium.Click("ctl00_MainContent_Button_AddGroup");
           // :( Selenium.Click("ctl00_MainContent_AssignmentTable_1a27");
            Selenium.Click("//td[@onclick=\"SetDate_ctl00_MainContent_OperationsTable_Operations_Since137('04/26/2010')\"]");
            Selenium.Click("//td[@onclick=\"SetDate_ctl00_MainContent_OperationsTable_Operations_Till137('04/30/2010')\"]");
            Selenium.Click("ctl00_MainContent_OperationsTable_Operations_a137");
            Selenium.Click("ctl00_MainContent_OperationsTable_Operations_Since138_image");
            Selenium.Click("//td[@onclick=\"SetDate_ctl00_MainContent_OperationsTable_Operations_Since138('04/28/2010')\"]");
            Selenium.Click("//td[@onclick=\"SetDate_ctl00_MainContent_OperationsTable_Operations_Till138('04/29/2010')\"]");
            Selenium.Click("ctl00_MainContent_OperationsTable_Operations_a138");
            Selenium.Click("link=Assignment");
            Selenium.WaitForPageToLoad("30000");
        }
         */

	}
}

