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
        public void ViewTheory1()
        {
            string name = "TestUser24";

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", name);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", name);
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

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

            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/newEditor1.zip");
            //Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\Users\\Іван\\Desktop\\newEditor1.zip");

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


            Selenium.Click("ctl00_MainContent__nextButton");
            //Pause(7000);
            //Selenium.Type("TextBox1", "123");
            //Pause(2000);

            //Selenium.Click("Button1");
            //Pause(2000);
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
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
            //ClickOnLastButtonRemove();

            //Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsTextPresent(name));

            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            //ClickOnButtonWithValue("Delete");
            Selenium.WaitForPageToLoad("9000");

            Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst0"));
            //Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Coursest0"));


        }


        //[Test]
        //public void ViewTheory2()
        //{
        //    string name = "TestUser35";

        //    Selenium.Click("link=Create User");
        //    Selenium.WaitForPageToLoad("7000");

        //    Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", name);
        //    Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1");
        //    Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1");
        //    Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", name);
        //    Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
        //    //Selenium.WaitForPageToLoad("7000");
        //    Selenium.Click("ctl00_MainContent_CreateUserWizard1_CompleteStepContainer_ContinueButtonButton");
        //    Selenium.WaitForPageToLoad("7000");

        //    Assert.IsTrue(Selenium.IsTextPresent(name));

        //    Selenium.Click("link=" + name);
        //    Selenium.WaitForPageToLoad("7000");

        //    Selenium.Check("ctl00_MainContent_cbStudentRole");
        //    Selenium.Click("ctl00_MainContent_btnApply");
        //    Selenium.Click("ctl00_MainContent_btnInclude");
        //    Selenium.WaitForPageToLoad("7000");
        //    Selenium.Click("ctl00_MainContent_gvGroups_ctl02_lnkSelect");
        //    Selenium.WaitForPageToLoad("7000");
        //    Selenium.Click("ctl00_MainContent_btnYes");
        //    Selenium.WaitForPageToLoad("7000");

        //    AssertLabelText("ctl00_MainContent_lbUserGroups", name + "(" + name + ") participating in following groups:");

        //    Selenium.Click("link=Courses");
        //    selenium.WaitForPageToLoad("7000");
        //    Selenium.Type("ctl00_MainContent_TextBox_CourseName", "dfg");
        //    Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "dfg");

        //    Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\Users\\Іван\\Desktop\\newEditor1.zip");
        //    //Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/newEditor1.zip");

        //    Selenium.Click("ctl00_MainContent_Button_ImportCourse");
        //    Selenium.WaitForPageToLoad("7000");
        //    Selenium.Click("//img[@alt='Expand dfg']");
        //    Selenium.Click("link=Curriculums");
        //    Selenium.WaitForPageToLoad("7000");
        //    Selenium.Type("ctl00_MainContent_TextBox_Name", "fdg");
        //    Selenium.Type("ctl00_MainContent_TextBox_Description", "dfg");
        //    Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
        //    Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
        //    Selenium.Click("ctl00_MainContent_Button_AddStage");
        //    Selenium.Click("//img[@alt='Expand fdg']");
        //    Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
        //    Selenium.Click("//img[@alt='Expand dfg']");
        //    Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
        //    Selenium.Click("ctl00_MainContent_Button_AddTheme");
        //    Selenium.Click("//img[@alt='Expand fdg']");
        //    Selenium.Click("link=Assignment");
        //    Selenium.WaitForPageToLoad("7000");
        //    Selenium.Click("ctl00_MainContent_Button_AddGroup");
        //    //Selenium.Click("ctl00_MainContent_AssignmentTable_1a7");
        //    ClickOnButtonWithValue("Assign");
        //    Selenium.WaitForPageToLoad("7000");

        //    Selenium.Click("ctl00_hypLogout");
        //    Selenium.Click("ctl00_btnOK");
        //    Selenium.WaitForPageToLoad("7000");
        //    //Selenium.Open("/Login.aspx");
        //    //Selenium.WaitForPageToLoad("7000");

        //    Selenium.Type("ctl00_MainContent_Login1_UserName", name);
        //    Selenium.Type("ctl00_MainContent_Login1_Password", "1");
        //    Selenium.Click("ctl00_MainContent_Login1_LoginButton");
        //    Selenium.WaitForPageToLoad("7000");
        //    Selenium.Click("ctl00_MainContent__curriculumTreeViewt2");
        //    Selenium.Click("ctl00_MainContent__openTest");
        //    Selenium.WaitForPageToLoad("7000");

        //    AssertIsOnPage("OpenTest.aspx", null);
        //    AssertLabelText("ctl00_MainContent__descriptionLabel", "You opened newEditor1(New Theory) page");
        //    //Selenium.WaitForPageToLoad("7000");

        //    Selenium.Click("ctl00_MainContent__nextButton");
        //    //Selenium.WaitForPageToLoad("7000");
        //    Pause(7000);
        //    Selenium.Click("ctl00_MainContent__previousButton");
        //    //Selenium.WaitForPageToLoad("7000");
        //    AssertLabelText("ctl00_MainContent__descriptionLabel", "You opened newEditor1(New Theory) page");
        //    Selenium.Click("ctl00_MainContent__nextButton");
        //    Pause(7000);

        //    //Selenium.WaitForPageToLoad("10000");
        //    //Pause(7000);
        //    Selenium.Type("TextBox1", "123");
        //    Pause(2000);
        //    //Selenium.WaitForPageToLoad("10000");

        //    Selenium.Click("Button1");
        //    Pause(7000);
        //    Selenium.Click("ctl00_MainContent__nextButton");
        //    Selenium.WaitForPageToLoad("7000");

        //    Selenium.Click("ctl00_hypLogout");
        //    Selenium.Click("ctl00_btnOK");
        //    Selenium.WaitForPageToLoad("7000");

        //    Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
        //    Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
        //    Selenium.Click("ctl00_MainContent_Login1_LoginButton");
        //    Selenium.WaitForPageToLoad("7000");

        //    Selenium.Click("link=Users");
        //    selenium.WaitForPageToLoad("7000");
        //    //Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
        //    Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
        //    Selenium.WaitForPageToLoad("7000");
        //    Selenium.Click("ctl00_MainContent_btnYes");
        //    Selenium.WaitForPageToLoad("7000");

        //    Assert.IsFalse(Selenium.IsTextPresent(name));

        //    Selenium.Click("link=Curriculums");
        //    Selenium.WaitForPageToLoad("7000");
        //    Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
        //    Selenium.Click("ctl00_MainContent_Button_Delete");
        //    Selenium.WaitForPageToLoad("2000");
        //    Selenium.Click("ctl00_MainContent_Button_Delete");
        //    Selenium.WaitForPageToLoad("7000");

        //    Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst0"));

        //    Selenium.Click("link=Courses");
        //    Selenium.WaitForPageToLoad("7000");
        //    Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
        //    Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
        //    Selenium.WaitForPageToLoad("7000");
        //    Selenium.Click("ctl00_MainContent_Button_Delete");
        //    Selenium.WaitForPageToLoad("7000");

        //    Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Coursest0"));


        //}
        
        [Test]
        public void PassingTestCorrect_EditField()
        {
            string name = "TestUser318";

            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Test_Group2");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", name);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", name);
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

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
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test");

            //Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/newEditor1.zip");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\Users\\Іван\\Desktop\\newEditor1.zip");
            

            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Assignment");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_AddGroup");

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

            Selenium.Click("ctl00_MainContent__nextButton");
            Pause(7000);
            Selenium.Type("TextBox1", "123");
            Pause(2000);

            Selenium.Click("Button1");
            Pause(2000);
            Selenium.Click("ctl00_MainContent__nextButton");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("ThemeResult.aspx", null);

            Assert.AreEqual("New Examination", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[1]"));
            Assert.AreEqual("123", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[2]"));
            Assert.AreEqual("123", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[3]"));
            Assert.AreEqual("correct", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[4]"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Users");
            selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");

            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsTextPresent(name));

            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            Selenium.WaitForPageToLoad("7000");

            
        }


        [Test]
        public void PassingTestInCorrect_EditField()
        {
            string name = "TestUser42";

            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Test_Group2");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", name);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", name);
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

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
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test");

            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/newEditor1.zip");
            
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Assignment");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_AddGroup");

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

            Selenium.Click("ctl00_MainContent__nextButton");
            Pause(7000);
            Selenium.Type("TextBox1", "321");
            Pause(2000);

            Selenium.Click("Button1");
            Pause(2000);
            Selenium.Click("ctl00_MainContent__nextButton");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("ThemeResult.aspx", null);

            Assert.AreEqual("New Examination", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[1]"));
            Assert.AreEqual("321", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[2]"));
            Assert.AreEqual("123", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[3]"));
            Assert.AreEqual("incorrect", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[4]"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Users");
            selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");

            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsTextPresent(name));

            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            Selenium.WaitForPageToLoad("7000");
        }


        [Test]
        public void PassingTestCorrect_SimpleQuestion()
        {
            string name = "TestUser64";

            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Test_Group2");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", name);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", name);
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

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
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test");

            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/newEditor2.zip");           

            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Assignment");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_AddGroup");

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
            Pause(3000);
            AssertIsOnPage("OpenTest.aspx", null);
            AssertLabelText("ctl00_MainContent__descriptionLabel", "You opened newEditor2(New Examination) page");

            Selenium.Click("//input[@type='checkbox']");
            Pause(7000);
            Selenium.Click("Button1");
            Pause(7000);
            Selenium.Click("ctl00_MainContent__nextButton");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("ThemeResult.aspx", null);
            Assert.AreEqual("New Examination", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[1]"));
            Assert.AreEqual("100", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[2]"));
            Assert.AreEqual("100", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[3]"));
            Assert.AreEqual("correct", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[4]"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Users");
            selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");

            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsTextPresent(name));

            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            Selenium.WaitForPageToLoad("7000");

        }


        [Test]
        public void PassingTestInCorrect_SimpleQuestion()
        {
            string name = "TestUser73";

            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Test_Group2");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", name);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", name);
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

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
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test");

            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/newEditor2.zip");

            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Assignment");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_AddGroup");

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
            Pause(3000);
            AssertIsOnPage("OpenTest.aspx", null);
            AssertLabelText("ctl00_MainContent__descriptionLabel", "You opened newEditor2(New Examination) page");

            Selenium.Click("//div[@id='SimpleQuestion1']/input[2]");
            Pause(7000);
            Selenium.Click("Button1");
            Pause(7000);
            Selenium.Click("ctl00_MainContent__nextButton");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("ThemeResult.aspx", null);
            Assert.AreEqual("New Examination", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[1]"));
            Assert.AreEqual("010", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[2]"));
            Assert.AreEqual("100", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[3]"));
            Assert.AreEqual("incorrect", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[4]"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Users");
            selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");

            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsTextPresent(name));

            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            Selenium.WaitForPageToLoad("7000");
        }



        [Test]
        public void PassingTestCorrect_ComboBox()
        {
            string name = "TestUser83";

            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Test_Group2");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", name);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", name);
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

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
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test");

            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/newEditor3.zip");

            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Assignment");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_AddGroup");

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
            Pause(3000);
            AssertIsOnPage("OpenTest.aspx", null);
            AssertLabelText("ctl00_MainContent__descriptionLabel", "You opened newEditor3(New Examination) page");

            Pause(7000);
            Selenium.Click("Button1");
            Pause(7000);
            Selenium.Click("ctl00_MainContent__nextButton");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("ThemeResult.aspx", null);
            Assert.AreEqual("New Examination", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[1]"));
            Assert.AreEqual("0", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[2]"));
            Assert.AreEqual("0", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[3]"));
            Assert.AreEqual("correct", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[4]"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Users");
            selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");

            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsTextPresent(name));

            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            Selenium.WaitForPageToLoad("7000");

        }

        [Test]
        public void PassingTestInCorrect_ComboBox()
        {
            string name = "TestUser93";

            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Test_Group2");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", name);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", name);
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

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
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test");

            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/newEditor3.zip");

            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Assignment");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_AddGroup");

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
            Pause(3000);
            AssertIsOnPage("OpenTest.aspx", null);
            AssertLabelText("ctl00_MainContent__descriptionLabel", "You opened newEditor3(New Examination) page");

            Selenium.Select("ComboBox1", "label=Incorect");
            Pause(7000);
            Selenium.Click("Button1");
            Pause(7000);
            Selenium.Click("ctl00_MainContent__nextButton");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("ThemeResult.aspx", null);
            Assert.AreEqual("New Examination", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[1]"));
            Assert.AreEqual("1", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[2]"));
            Assert.AreEqual("0", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[3]"));
            Assert.AreEqual("incorrect", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[4]"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Users");
            selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");

            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsTextPresent(name));

            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            Selenium.WaitForPageToLoad("7000");
        }

        
        [Test]
        public void ViewResult1()
        {
            string name = "TestUser30";

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", name);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", name);
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

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
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test");

            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/newEditor1.zip");
            //Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\Users\\Іван\\Desktop\\newEditor1.zip");


            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Assignment");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_AddGroup");

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

            Selenium.Click("ctl00_MainContent__nextButton");
            Pause(7000);
            Selenium.Type("TextBox1", "123");
            Pause(2000);

            Selenium.Click("Button1");
            Pause(2000);
            Selenium.Click("ctl00_MainContent__nextButton");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("ThemeResult.aspx", null);

           
            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent__curriculumTreeViewt2");
            Selenium.Click("ctl00_MainContent__showResult");

            //Assert.AreEqual("New Examination", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[1]"));
            //Assert.AreEqual("123", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[2]"));
            //Assert.AreEqual("123", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[3]"));
            //Assert.AreEqual("correct", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[4]"));
            //Assert.IsTrue(Selenium.IsTextPresent("correct"));
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Users");
            selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");

            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsTextPresent(name));

            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            //Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            //Selenium.WaitForPageToLoad("7000");
            //Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            //Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            //Selenium.WaitForPageToLoad("7000");


        }

        [Test]
        public void ViewResult2()
        {
            string name = "TestUser32";

            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", name);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "1");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", name);
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");

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
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test");

            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/newEditor1.zip");
            //Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\Users\\Іван\\Desktop\\newEditor1.zip");


            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Test']");
            Selenium.Click("link=Assignment");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_AddGroup");

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

            Selenium.Click("ctl00_MainContent__nextButton");
            Pause(7000);
            Selenium.Type("TextBox1", "12433");
            Pause(2000);

            Selenium.Click("Button1");
            Pause(2000);
            Selenium.Click("ctl00_MainContent__nextButton");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("ThemeResult.aspx", null);


            Selenium.Click("link=Home");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent__curriculumTreeViewt2");
            Selenium.Click("ctl00_MainContent__showResult");

            //Assert.AreEqual("New Examination", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[1]"));
            //Assert.AreEqual("123", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[2]"));
            //Assert.AreEqual("123", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[3]"));
            //Assert.AreEqual("correct", Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table/tbody/tr[2]/td[4]"));
            //Assert.IsTrue(Selenium.IsTextPresent("correct"));
            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Users");
            selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");

            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");

            Assert.IsFalse(Selenium.IsTextPresent(name));

            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            //Selenium.Click("//tr[@id='ctl00_MainMenun15']/td/table/tbody/tr/td/a");
            //Selenium.WaitForPageToLoad("7000");
            //Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            //Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            //Selenium.WaitForPageToLoad("7000");


        }
    }
}
