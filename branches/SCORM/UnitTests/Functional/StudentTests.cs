using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IUDICO.UnitTest.Base;

namespace IUDICO.UnitTest.Functional
{
    [TestFixture]
    public class StudentTests : TestFixtureWeb
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

        [Test]
        public void SeeTheory()
        {
            string name = "TestUser23";
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", name);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", name);
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            //Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsTrue(Selenium.IsTextPresent(name));

            Selenium.Click("link=" + name);
            Selenium.WaitForPageToLoad("7000");

            Selenium.Check("ctl00_MainContent_cbStudentRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("ctl00_MainContent_btnInclude");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_gvGroups_ctl02_lnkSelect");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            AssertLabelText("ctl00_MainContent_lbUserGroups", name + "(" + name + ") participating in following groups:");

            Selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "dfg");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "dfg");

            //Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\Users\\Іван\\Desktop\\newEditor1.zip");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/newEditor1.zip");

            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//img[@alt='Expand dfg']");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "fdg");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "dfg");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand fdg']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand dfg']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand fdg']");
            Selenium.Click("link=Assignment");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_AddGroup");
            //Selenium.Click("ctl00_MainContent_AssignmentTable_1a7");
            ClickOnButtonWithValue("Assign");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", name);
            Selenium.Type("ctl00_MainContent_Login1_Password", "1");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent__curriculumTreeViewt2");
            Selenium.Click("ctl00_MainContent__openTest");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("OpenTest.aspx", null);
            AssertLabelText("ctl00_MainContent__descriptionLabel", "You opened newEditor1(New Theory) page");
            //Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent__nextButton");

            //Selenium.WaitForPageToLoad("10000");
            //Selenium.Type("TextBox1", "123");
            //Selenium.WaitForPageToLoad("10000");

            //Selenium.Click("Button1");
            Selenium.Click("ctl00_MainContent__nextButton");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Users");
            selenium.WaitForPageToLoad("7000");
            //Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
            ClickOnLastButtonRemove();
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsTextPresent(name));

            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            //Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst0"));

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Coursest0"));


        }
    }
}
