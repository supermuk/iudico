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
    public class WebTest : TestFixtureWeb
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

        ///Авторизація коистувача
        /// <summary>
        /// Correct Login
        /// </summary>
		[Test]
		public void Test01_CorrectLogin()
		{
			selenium.Open("/Login.aspx");
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
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00$MainContent$Login1$UserName", "baduser");
            Selenium.Type("ctl00$MainContent$Login1$Password", "baduser");
            Selenium.Click("ctl00$MainContent$Login1$LoginButton");
            Pause(3000);

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
            Selenium.WaitForPageToLoad("7000");
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
        }


        [Test]
        public void Test05_DeleteCourse()
        {
            Selenium.Open("/Login.aspx");
            Selenium.WaitForPageToLoad("7000");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");
            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            selenium.Click("ctl00_MainContent_Button_Delete");

        }

        [Test]
        public void Test06_CreateGroup()
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
            selenium.Click("//form[@id='aspnetForm']/center");

            AssertIsOnPage("Groups.aspx", null);
            AssertHasText("Test_Group");
        }
        [Test]
        public void Test07_DeleteGroup()
        {
            selenium.Open("/Login.aspx");
            selenium.Click("ctl00_MainContent_Login1_UserName");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");
            selenium.Click("link=Groups");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Groups");
            selenium.WaitForPageToLoad("30000");

            AssertIsOnPage("Groups.aspx", null);
        }
        [Test]
        public void Test08_CreateCuriculum()
        {
            selenium.Open("/Login.aspx");
            selenium.Click("ctl00_MainContent_Login1_UserName");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");

            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("ctl00_MainContent_TextBox_CourseName");
            selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curiculum");
            selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curiculum");
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
            selenium.Click("//img[@alt='Expand Test_for_curiculum']");
            selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            selenium.Click("ctl00_MainContent_Button_AddTheme");
            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            AssertHasText("Curriculum_test");
        }

        [Test]
        public void Test09_ModifyCuriculum()
        {
            selenium.Open("/Login.aspx");
            selenium.Click("ctl00_MainContent_Login1_UserName");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");

            selenium.Click("link=Curriculums");
            selenium.WaitForPageToLoad("30000");

            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("//img[@alt='Expand Curriculum_test']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            selenium.Type("ctl00_MainContent_TextBox_Name", "New_name");
            selenium.Type("ctl00_MainContent_TextBox_Description", "New_name");
            selenium.Click("ctl00_MainContent_Button_Modify");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_TextBox_Name");
            selenium.Type("ctl00_MainContent_TextBox_Name", "Other_name");
            selenium.Type("ctl00_MainContent_TextBox_Description", "Other_name");
            selenium.Click("ctl00_MainContent_Button_Modify");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            AssertHasText("New_name");
            AssertHasText("Other_name");
        }

        [Test]
        public void Test10_DeleteCuriculum()
        {
            selenium.Open("/Login.aspx");
            selenium.Click("ctl00_MainContent_Login1_UserName");
            selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            selenium.Click("ctl00_MainContent_Login1_LoginButton");

            selenium.Click("link=Curriculums");
            selenium.WaitForPageToLoad("30000");

            selenium.Click("//img[@alt='Expand Other_name']");
            selenium.Click("//img[@alt='Expand New_name']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("//img[@alt='Expand Other_name']");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            selenium.Click("ctl00_MainContent_Button_Delete");
            selenium.Click("ctl00_MainContent_Button_Delete");

        }



        [Test]
        public void Test11_Add_Assignment()
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

	}
}

