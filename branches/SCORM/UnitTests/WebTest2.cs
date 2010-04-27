using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IUDICO.UnitTest.Base;
using Selenium;
using System.Text.RegularExpressions;
using System.Threading;


/*
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
        
    }
}
*/


namespace IUDICO.UnitTest
{
	[TestFixture]
    public class WebTest2 : TestFixtureWeb
	{
        private ISelenium selenium;
       private StringBuilder verificationErrors;

       [SetUp]
       public void SetupTest()
       {
           selenium = new DefaultSelenium("localhost", 4444, "*iexplore", "http://localhost:2935/");
           selenium.Start();
           verificationErrors = new StringBuilder();
       }
       [TearDown]
       public void TeardownTest()
       {
           try
           {
               selenium.Stop();
           }
           catch (Exception)
           {
               // Ignore errors if unable to close the browser
           }
           Assert.AreEqual("", verificationErrors.ToString());
       }


        ///User Autorization
        /// <summary>
        /// Correct Login
        /// </summary>
		[Test]
		public void Test01_CorrectLogin()
		{
			selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("30000");
			selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
			selenium.Type("ctl00_MainContent_Login1_Password", "lex");
			selenium.Click("ctl00_MainContent_Login1_LoginButton");

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
            selenium.Click("ctl00_hypLogout");
            selenium.Click("ctl00_btnOK");
            selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Login.aspx", null);
        }


         /// <summary>
        /// Teacher Tests
        /// </summary>
        
        //import corse
        [Test]
        public void Test04_ImportCourse()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00$MainContent$Login1$UserName", "lex");
            Selenium.Type("ctl00$MainContent$Login1$Password", "lex");
            Selenium.Click("ctl00$MainContent$Login1$LoginButton");
            selenium.WaitForPageToLoad("2000");
            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            selenium.Click("ctl00_MainContent_FileUpload_Course");
            selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            selenium.Click("ctl00_MainContent_Button_ImportCourse");
            selenium.WaitForPageToLoad("30000");

            AssertHtmlText("ctl00_MainContent_TreeView_Coursest0", "TestCourse");
            AssertHasText("TestCourse");
            AssertIsOnPage("CourseEdit.aspx", null);

            selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            selenium.Click("ctl00_MainContent_Button_Delete");
        }

        //imports bad course
        [Test]
        public void Test05_ImportBadCourse()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00$MainContent$Login1$UserName", "lex");
            Selenium.Type("ctl00$MainContent$Login1$Password", "lex");
            Selenium.Click("ctl00$MainContent$Login1$LoginButton");
            selenium.WaitForPageToLoad("2000");
            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            selenium.Click("ctl00_MainContent_FileUpload_Course");
            selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            selenium.Click("ctl00_MainContent_Button_ImportCourse");
            selenium.WaitForPageToLoad("30000");

            AssertHtmlText("ctl00_MainContent_Label_PageMessage", "No imsmanifest.xml file found");
            AssertIsOnPage("CourseEdit.aspx", null);
        }

        //create and delete course
        [Test]
        public void Test06_DeleteCourse()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");
            selenium.Click("link=Courses");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            selenium.Click("ctl00_MainContent_FileUpload_Course");
            selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            selenium.Click("ctl00_MainContent_Button_ImportCourse");
            selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("CourseEdit.aspx", null);

        }

        //create 2 courses & delete first
        [Test]
        public void Test07_DeleteCourse2()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");
            selenium.Click("link=Courses");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            selenium.Click("ctl00_MainContent_FileUpload_Course");
            selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            selenium.Click("ctl00_MainContent_Button_ImportCourse");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse2");
            selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse2");
            selenium.Click("ctl00_MainContent_FileUpload_Course");
            selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            selenium.Click("ctl00_MainContent_Button_ImportCourse");
            selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("CourseEdit.aspx", null);

            selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            selenium.Click("ctl00_MainContent_Button_Delete");
        }

        //create 2 courses & delete second
        [Test]
        public void Test08_DeleteCourse3()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");
            selenium.Click("link=Courses");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            selenium.Click("ctl00_MainContent_FileUpload_Course");
            selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            selenium.Click("ctl00_MainContent_Button_ImportCourse");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse2");
            selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse2");
            selenium.Click("ctl00_MainContent_FileUpload_Course");
            selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            selenium.Click("ctl00_MainContent_Button_ImportCourse");
            selenium.Click("ctl00_MainContent_TreeView_Coursest2");
            selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("CourseEdit.aspx", null);

            selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            selenium.Click("ctl00_MainContent_Button_Delete");

        }

        //create group
        [Test]
        public void Test09_CreateGroup()
        {
            selenium.Open("/Login.aspx");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");
            selenium.Click("link=Groups");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_btnCreateGroup");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("ctl00_MainContent_tbGroupName", "Test_Group");
            selenium.Click("ctl00_MainContent_btnCreate");
            selenium.Click("link=Groups");
            selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Groups.aspx", null);
            AssertHasText("Test_Group");

            selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
        }

        //create & change group name
        [Test]
        public void Test10_ChangeNameGroup()
        {
            selenium.Open("/Login.aspx");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");
            selenium.Click("link=Groups");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_btnCreateGroup");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("ctl00_MainContent_tbGroupName", "Test_Group");
            selenium.Click("ctl00_MainContent_btnCreate");
            selenium.Click("ctl00_MainContent_tbGroupName");
            selenium.Type("ctl00_MainContent_tbGroupName", "New_Group");
            selenium.Click("ctl00_MainContent_btnApply");
            selenium.Click("link=Groups");
            selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Groups.aspx", null);
            AssertHasText("New_Group");

            selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
        }

        //create & change group name
        [Test]
        public void Test11_ChangeNameGroup2()
        {
            selenium.Open("/Login.aspx");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");
            selenium.WaitForPageToLoad("2000");
            selenium.Click("link=Groups");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_btnCreateGroup");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("ctl00_MainContent_tbGroupName", "Test_Group");
            selenium.Click("ctl00_MainContent_btnCreate");
            selenium.Click("link=Groups");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkGroupName");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_tbGroupName");
            selenium.Type("ctl00_MainContent_tbGroupName", "New_Group");
            selenium.Click("ctl00_MainContent_btnApply");
            selenium.Click("link=Groups");
            selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Groups.aspx", null);
            AssertHasText("New_Group");

            selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
        }

        //create & delete group
        [Test]
        public void Test12_DeleteGroup()
        {
            selenium.Open("/Login.aspx");
            selenium.Click("ctl00_MainContent_Login1_UserName");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");
            selenium.Click("link=Groups");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_btnCreateGroup");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("ctl00_MainContent_tbGroupName", "Test_Group");
            selenium.Click("ctl00_MainContent_btnCreate");
            selenium.Click("link=Groups");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Groups");
            selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Groups.aspx", null);
        }

        //create curriculum
        [Test]
        public void Test13_CreateCurriculum()
        {
            selenium.Open("/Login.aspx");
            selenium.Click("ctl00_MainContent_Login1_UserName");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");

            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            selenium.Click("ctl00_MainContent_FileUpload_Course");
            selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            selenium.Click("ctl00_MainContent_Button_ImportCourse");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Curriculums");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_Name");
            selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_Button_AddStage");
            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            selenium.Click("ctl00_MainContent_Button_AddTheme");
            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            AssertHasText("Curriculum_test");

            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            selenium.Click("ctl00_MainContent_Button_Delete");
        }

        //change name of stage
        [Test]
        public void Test14_ModifyCurriculum()
        {
            selenium.Open("/Login.aspx");
            selenium.Click("ctl00_MainContent_Login1_UserName");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");

            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            selenium.Click("ctl00_MainContent_FileUpload_Course");
            selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            selenium.Click("ctl00_MainContent_Button_ImportCourse");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Curriculums");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_Name");
            selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_Button_AddStage");
            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            selenium.Click("ctl00_MainContent_Button_AddTheme");
            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            selenium.Type("ctl00_MainContent_TextBox_Name", "New_name");
            selenium.Type("ctl00_MainContent_TextBox_Description", "New_name");
            selenium.Click("ctl00_MainContent_Button_Modify");
            selenium.Click("ctl00_MainContent_TextBox_Name");
            selenium.Type("ctl00_MainContent_TextBox_Name", "");
            selenium.Click("ctl00_MainContent_TextBox_Description");
            selenium.Type("ctl00_MainContent_TextBox_Description", "");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            AssertHasText("New_name");

            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            selenium.Click("ctl00_MainContent_Button_Delete");
        }

        //change name of curriculum
        [Test]
        public void Test15_ModifyCurriculum2()
        {
            selenium.Open("/Login.aspx");
            selenium.Click("ctl00_MainContent_Login1_UserName");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");

            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            selenium.Click("ctl00_MainContent_FileUpload_Course");
            selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            selenium.Click("ctl00_MainContent_Button_ImportCourse");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Curriculums");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_Name");
            selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_Button_AddStage");
            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            selenium.Click("ctl00_MainContent_Button_AddTheme");
            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Type("ctl00_MainContent_TextBox_Name", "New_curriculum");
            selenium.Type("ctl00_MainContent_TextBox_Description", "New_curriculum");
            selenium.Click("ctl00_MainContent_Button_Modify");
            selenium.Click("ctl00_MainContent_TextBox_Name");
            selenium.Type("ctl00_MainContent_TextBox_Name", "");
            selenium.Click("ctl00_MainContent_TextBox_Description");
            selenium.Type("ctl00_MainContent_TextBox_Description", "");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            AssertHasText("New_curriculum");

            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            selenium.Click("ctl00_MainContent_Button_Delete");
        }

        //delete theme from curriculum
        [Test]
        public void Test16_DeleteCuriculum()
        {
            selenium.Open("/Login.aspx");
            selenium.Click("ctl00_MainContent_Login1_UserName");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");

            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            selenium.Click("ctl00_MainContent_FileUpload_Course");
            selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            selenium.Click("ctl00_MainContent_Button_ImportCourse");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Curriculums");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_Name");
            selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_Button_AddStage");
            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            selenium.Click("ctl00_MainContent_Button_AddTheme");
            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("//img[@alt='Expand Curriculum_test']");

            AssertIsOnPage("CurriculumEdit.aspx", null);

            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            selenium.Click("ctl00_MainContent_Button_Delete");
        }

        //delete stage from curriculum
        [Test]
        public void Test17_DeleteCuriculum2()
        {
            selenium.Open("/Login.aspx");
            selenium.Click("ctl00_MainContent_Login1_UserName");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");

            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            selenium.Click("ctl00_MainContent_FileUpload_Course");
            selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            selenium.Click("ctl00_MainContent_Button_ImportCourse");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Curriculums");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_Name");
            selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_Button_AddStage");
            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            selenium.Click("ctl00_MainContent_Button_AddTheme");
            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("ctl00_MainContent_Button_Delete");

            AssertIsOnPage("CurriculumEdit.aspx", null);

            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            selenium.Click("ctl00_MainContent_Button_Delete");
        }

        //delete curriculum
        [Test]
        public void Test18_DeleteCuriculum3()
        {
            selenium.Open("/Login.aspx");
            selenium.Click("ctl00_MainContent_Login1_UserName");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");

            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            selenium.Click("ctl00_MainContent_FileUpload_Course");
            selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            selenium.Click("ctl00_MainContent_Button_ImportCourse");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Curriculums");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_Name");
            selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_Button_AddStage");
            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            selenium.Click("ctl00_MainContent_Button_AddTheme");
            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("ctl00_MainContent_Button_Delete");

            AssertIsOnPage("CurriculumEdit.aspx", null);

            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            selenium.Click("ctl00_MainContent_Button_Delete");
        }
        /*
        //add assignment
        [Test]
        public void Test19_Add_Assignment()
        {
            selenium.Open("/Login.aspx");
            selenium.Click("ctl00_MainContent_Login1_UserName");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");

            selenium.Click("link=Groups");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_btnCreateGroup");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_tbGroupName");
            selenium.Type("ctl00_MainContent_tbGroupName", "Ass_test_group");
            selenium.Click("ctl00_MainContent_btnCreate");
            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "Ass_course");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Ass_test");
            selenium.Click("ctl00_MainContent_FileUpload_Course");
            selenium.Type("ctl00_MainContent_FileUpload_Course", "");
            selenium.Click("ctl00_MainContent_Button_ImportCourse");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Curriculums");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_Name");
            selenium.Type("ctl00_MainContent_TextBox_Name", "Ass_curric");
            selenium.Type("ctl00_MainContent_TextBox_Description", "Ass_curric");
            selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_Button_AddStage");
            selenium.Click("//img[@alt='Expand Ass_curric']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            selenium.Click("//img[@alt='Expand Ass_course']");
            selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            selenium.Click("ctl00_MainContent_Button_AddTheme");
            selenium.Click("link=Assignment");
            selenium.WaitForPageToLoad("30000");
            selenium.Select("ctl00_MainContent_GroupList", "label=Ass_test_group");
            selenium.Click("ctl00_MainContent_Button_AddGroup");
           // :( selenium.Click("ctl00_MainContent_AssignmentTable_1a27");
            selenium.Click("//td[@onclick=\"SetDate_ctl00_MainContent_OperationsTable_Operations_Since137('04/26/2010')\"]");
            selenium.Click("//td[@onclick=\"SetDate_ctl00_MainContent_OperationsTable_Operations_Till137('04/30/2010')\"]");
            selenium.Click("ctl00_MainContent_OperationsTable_Operations_a137");
            selenium.Click("ctl00_MainContent_OperationsTable_Operations_Since138_image");
            selenium.Click("//td[@onclick=\"SetDate_ctl00_MainContent_OperationsTable_Operations_Since138('04/28/2010')\"]");
            selenium.Click("//td[@onclick=\"SetDate_ctl00_MainContent_OperationsTable_Operations_Till138('04/29/2010')\"]");
            selenium.Click("ctl00_MainContent_OperationsTable_Operations_a138");
            selenium.Click("link=Assignment");
            selenium.WaitForPageToLoad("30000");
        }
         */

	}
}

