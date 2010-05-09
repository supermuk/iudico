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
         /// <summary>
        /// Teacher Tests
        /// </summary>


        //create & change group name2
        /*
        [Test]
        public void Test17_ChangeNameGroup2()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "Test_Group");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkGroupName");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_tbGroupName");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Group");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("Groups.aspx", null);
            Assert.AreEqual("New_Group", Selenium.GetTable("ctl00_MainContent_GroupList_gvGroups." +
                Selenium.GetXpathCount("//table[@id='ctl00_MainContent_GroupList_gvGroups']/tbody/tr") + ".0"));

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Groups");
        }
        */


        /*
        //add assignment
        [Test]
        public void Test34_Add_Assignment()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Click("ctl00_MainContent_Login1_UserName");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_tbGroupName");
            Selenium.Type("ctl00_MainContent_tbGroupName", "Ass_test_group");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Ass_course");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Ass_test");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
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
            Selenium.WaitForPageToLoad("7000");
            Selenium.Select("ctl00_MainContent_GroupList", "label=Ass_test_group");
            Selenium.Click("ctl00_MainContent_Button_AddGroup");
            ClickOnButtonWithValue("Assign");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_OperationsTable_Operations_Since1132_image");
            Selenium.Click("//td[@onclick=\"SetDate_ctl00_MainContent_OperationsTable_Operations_Since1132('05/05/2010')\"]");
            Selenium.Click("ctl00_MainContent_OperationsTable_Operations_Till1132_image");
            Selenium.Click("//td[@onclick=\"SetDate_ctl00_MainContent_OperationsTable_Operations_Till1132('05/31/2010')\"]");
            Selenium.Click("ctl00_MainContent_OperationsTable_Operations_a1132");
            Selenium.Click("//td[@onclick=\"SetDate_ctl00_MainContent_OperationsTable_Operations_Since1133('05/06/2010')\"]");
            Selenium.Click("//td[@onclick=\"SetDate_ctl00_MainContent_OperationsTable_Operations_Till1133('05/31/2010')\"]");
            Selenium.Click("ctl00_MainContent_OperationsTable_Operations_a1133");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl03_lbLogin");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnInclude");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_gvGroups_ctl02_lnkSelect");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("7000");
        }


        [Test]
        public void Test35_TeacherShareHisCourse()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            //Selenium.Click("link=Courses");
            //Selenium.WaitForPageToLoad("7000");
            //Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            //Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            //Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            //Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            //Selenium.WaitForPageToLoad("7000");
            //Selenium.Click("link=Curriculums");
            //Selenium.WaitForPageToLoad("7000");
            //Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            //Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            //Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            //Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            //Selenium.Click("ctl00_MainContent_Button_AddStage");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            //Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            //Selenium.Click("//img[@alt='Expand TestCourse']");
            //Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            //Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateUser");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "TestT");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "test");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "test");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "test");
            ClickOnButtonWithValue("Create User");
            //Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
           //ClickOnButtonWithValue("Continue");
            //Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_lbLogin");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_cbLectorRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=My objects");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestTeacher(TestTeacher)");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_1201");
            Selenium.Click("ctl00_MainContent_Button_Update");
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//table[@id='ctl00_MainContent_Login1']/tbody/tr/td/table/tbody/tr[2]/td[2]");
            Selenium.Click("ctl00_MainContent_Login1_UserName");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "TestTeacher");
            Selenium.Type("ctl00_MainContent_Login1_Password", "test");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.Click("link=My objects");
            Selenium.WaitForPageToLoad("7000");

            try
            {
                Assert.AreEqual("TestCourse", Selenium.GetText("ctl00_MainContent_Table_Courses"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

        }
        */
         

    }
}

