using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IUDICO.UnitTest.Base;
using Selenium;

namespace IUDICO.UnitTest.Functional
{
    [TestFixture]
    class TeacherTests: TestFixtureWeb
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

            //AssertIsOnPage("Home.aspx", null);
        }

        [TearDown]
        public void TearDown()
        {
            Selenium.Open("/Logout.ashx");
        }

        [Test]
        public void ImportCourse()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("CourseEdit.aspx", null);
            Assert.AreEqual("TestCourse", Selenium.GetTable("//div[@id='ctl00_MainContent_TreeView_Courses']/table.0.2"));

            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
        }

        [Test]
        public void ImportCourseNoIMSmanifest()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/Noimsmanifest.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");

            AssertHtmlText("ctl00_MainContent_Label_PageMessage", "No imsmanifest.xml file found");
            AssertIsOnPage("CourseEdit.aspx", null);
        }

        [Test]
        public void ImportCourseNoCourseSelected()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");

            AssertHtmlText("ctl00_MainContent_Label_PageMessage", "Specify course path.");
            AssertIsOnPage("CourseEdit.aspx", null);
        }

        [Test]
        public void DeleteCourse()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("CourseEdit.aspx", null);

            Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Coursest0"));
        }

        [Test]
        public void DeleteAttachedCourse()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
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

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("CourseDeleteConfirmation.aspx", null);
            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_GridView_Dependencies"));

            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void CreateGroup()
        {
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            decimal groups = Selenium.GetXpathCount("//table[@id='ctl00_MainContent_GroupList_gvGroups']/tbody/tr");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Test_Group");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("Groups.aspx", null);
            Assert.AreEqual(groups + 1, Selenium.GetXpathCount("//table[@id='ctl00_MainContent_GroupList_gvGroups']/tbody/tr"));
            Assert.AreEqual("New_Test_Group", Selenium.GetTable("ctl00_MainContent_GroupList_gvGroups." + groups + ".0"));

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Groups");
        }

        [Test]
        public void CreateGroupNoName()
        {
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("Groups.aspx", null);
            Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkGroupName"));
        }

        [Test]
        public void RenameGroup()
        {
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "Test_Group");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.Click("ctl00_MainContent_tbGroupName");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Group");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("Groups.aspx", null);
            Assert.AreEqual("New_Group", Selenium.GetTable("ctl00_MainContent_GroupList_gvGroups." +
                Selenium.GetXpathCount("//table[@id='ctl00_MainContent_GroupList_gvGroups']/tbody/tr") + ".0"));


            //Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            //Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            //Selenium.WaitForPageToLoad("7000");
            //Selenium.Click("link=Groups");
        }

        [Test]
        public void DeleteGroup()
        {
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "Test_Group");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnCancel");

            AssertIsOnPage("Groups.aspx", null);
            Assert.AreEqual("Test_Group", Selenium.GetTable("ctl00_MainContent_GroupList_gvGroups.1.0"));
            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkGroupName"));

            //Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            //Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            //Selenium.WaitForPageToLoad("7000");
            //Selenium.Click("link=Groups");

        }

        [Test]
        public void DeleteRenamedGroup()
        {
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "Test_Group");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.Click("ctl00_MainContent_tbGroupName");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Test_Group");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnCancel");

            AssertIsOnPage("Groups.aspx", null);
            Assert.AreEqual("New_Test_Group", Selenium.GetTable("ctl00_MainContent_GroupList_gvGroups.1.0"));
            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkGroupName"));

            //Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            //Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
            //Selenium.WaitForPageToLoad("7000");
            //Selenium.Click("link=Groups");
        }

        [Test]
        public void CreateCurriculum()
        {
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst0"));

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void CreateCurriculumWithStage()
        {
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Pause(3000);

            AssertIsOnPage("CurriculumEdit.aspx", null);
            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst1"));

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void CreateCurriculumAndModifyStage()
        {
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "New_name");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "New_name");
            Selenium.Click("ctl00_MainContent_Button_Modify");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "");
            Selenium.Click("ctl00_MainContent_TextBox_Description");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            Assert.AreEqual("New_name", Selenium.GetText("ctl00_MainContent_TreeView_Curriculumst1"));

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void CreateCurriculumAndModify()
        {
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "New_name");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "New_name");
            Selenium.Click("ctl00_MainContent_Button_Modify");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "");
            Selenium.Click("ctl00_MainContent_TextBox_Description");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            Assert.AreEqual("New_name", Selenium.GetText("ctl00_MainContent_TreeView_Curriculumst0"));

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void CreateCurriculumAndDeleteStage()
        {
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst1"));
        }

        [Test]
        public void CreateCurriculumAndDelete()
        {
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst0"));
        }

        [Test]
        public void CreateCurriculumWithCourse()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
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
            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst2"));

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void RenameCurriculumWithCourse()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
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
            //?
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "New_name");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "New_name");
            Selenium.Click("ctl00_MainContent_Button_Modify");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "");
            Selenium.Click("ctl00_MainContent_TextBox_Description");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "");
            //
            AssertIsOnPage("CurriculumEdit.aspx", null);
            Assert.AreEqual("New_name", Selenium.GetText("ctl00_MainContent_TreeView_Curriculumst1"));

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void RenameCurriculum2WithCourse()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
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
            Assert.AreEqual("New_curriculum", Selenium.GetText("ctl00_MainContent_TreeView_Curriculumst0"));

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void DeleteCuriculum()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
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

            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst2"));

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void DeleteCuriculum2()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
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
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst1"));

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void DeleteCuriculum3()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
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
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("CurriculumEdit.aspx", null);

            Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst0"));

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void DeleteAndCeateCuriculum()
        {
            Selenium.Open("/Login.aspx");
            Selenium.Click("ctl00_MainContent_Login1_UserName");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
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

            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst0"));

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void DeleteAndCeateCuriculum2()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
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
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");

            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test2");
            Selenium.Click("ctl00_MainContent_TextBox_Description");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test2");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Curriculum_test2']");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst0"));

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void DeleteAndCeateCuriculum3()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
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

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test2");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test2");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            Selenium.Click("//img[@alt='Expand Curriculum_test2']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            Selenium.Click("//img[@alt='Expand Curriculum_test2']");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst0"));

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void TeacherShareCourse_OnlyUse()
        {
            string word = "teacher15";
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", word);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", word);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", word);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", word);
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Pause(2000);
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_lbLogin");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_cbLectorRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My objects");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=" + word + "(" + word + ")");
            Selenium.WaitForPageToLoad("7000");
            selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr[2]/td/label");
            Selenium.Click("ctl00_MainContent_Button_Update");
            Selenium.WaitForPageToLoad("7000");
            //Selenium.Refresh();
            //Selenium.WaitForPageToLoad("7000");
            //Assert.AreEqual("teacher15(teacher15)", Selenium.GetTable("ctl00_MainContent_Table_SharedWith.0.0"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_Login1_UserName", word);
            Selenium.Type("ctl00_MainContent_Login1_Password", word);
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My objects");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestCourse");

            Assert.IsTrue(Selenium.IsTextPresent("granted you permission to Use this course"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Users");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000"); 
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
        }

        [Test]
        public void TeacherShareCourse_OnlyModify()
        {
            string word = "teacher16";
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", word);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", word);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", word);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", word);
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Pause(2000);
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_lbLogin");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_cbLectorRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My objects");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=" + word + "(" + word + ")");
            Selenium.WaitForPageToLoad("7000");
            selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr/td/label");
            
            Selenium.Click("ctl00_MainContent_Button_Update");
            Selenium.WaitForPageToLoad("7000");
            //Selenium.Refresh();
            //Selenium.WaitForPageToLoad("7000");
            //Assert.AreEqual("teacher16(teacher16)", Selenium.GetTable("ctl00_MainContent_Table_SharedWith.0.0"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_Login1_UserName", word);
            Selenium.Type("ctl00_MainContent_Login1_Password", word);
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My objects");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestCourse");

            Assert.IsTrue(Selenium.IsTextPresent("granted you permission to Modify this course"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Users");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
        }

        [Test]
        public void TeacherShareCourse_UseAndModify()
        {
            string word = "teacher18";
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", word);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", word);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", word);
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", word);
            Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
            Pause(2000);
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_lbLogin");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_cbLectorRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My objects");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link="+ word + "("+ word+ ")");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr/td/label");
            Selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr[2]/td/label");

            Selenium.Click("ctl00_MainContent_Button_Update");
            Selenium.WaitForPageToLoad("7000");
            //Selenium.Refresh();
            //Selenium.WaitForPageToLoad("7000");
            //Assert.AreEqual("teacher17(teacher17)", Selenium.GetTable("ctl00_MainContent_Table_SharedWith.0.0"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_Login1_UserName", word);
            Selenium.Type("ctl00_MainContent_Login1_Password", word);
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My objects");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestCourse");

            Assert.IsTrue(Selenium.IsTextPresent("granted you permission to Modify this course"));
            Assert.IsTrue(Selenium.IsTextPresent("granted you permission to Use this course"));

            Selenium.Click("ctl00_hypLogout");
            Selenium.Click("ctl00_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
            Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
            Selenium.Click("ctl00_MainContent_Login1_LoginButton");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Users");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnYes");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
        }
        /*
        [Test]
        public void TeacherShareCourse_OnlyUse_AllowDelegate()
        {
            Selenium.Open("/Student/StudentPage.aspx");
            Selenium.Click("link=Create User");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "teacher");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "teacher");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "teacher");
            Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "aa");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl07_lbLogin");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_cbLectorRole");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Users");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\course.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=My objects");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=TestCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=teacher(teacher)");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_1349");
            Selenium.Click("ctl00_MainContent_Button_Update");
            Assert.IsTrue(Selenium.IsTextPresent("teacher(teacher)"));
        }
         [Test]
         public void TeacherShareCourse_OnlyModify_AllowDelegate()
         {
             Selenium.Open("/Student/StudentPage.aspx");
             Selenium.Click("link=Create User");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "teacher");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "teacher");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "teacher");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "aa");
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl07_lbLogin");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_cbLectorRole");
             Selenium.Click("ctl00_MainContent_btnApply");
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Courses");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
             Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
             Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\course.zip");
             Selenium.Click("ctl00_MainContent_Button_ImportCourse");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=My objects");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=TestCourse");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=teacher(teacher)");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_1349");
             Selenium.Click("ctl00_MainContent_Button_Update");
             Assert.IsTrue(Selenium.IsTextPresent("teacher(teacher)"));
         }
         [Test]
         public void TeacherShareCourse_UseAndModify_AllowDelegate()
         {
             Selenium.Open("/Student/StudentPage.aspx");
             Selenium.Click("link=Create User");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "teacher");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "teacher");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "teacher");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "aa");
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl07_lbLogin");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_cbLectorRole");
             Selenium.Click("ctl00_MainContent_btnApply");
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Courses");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
             Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
             Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\course.zip");
             Selenium.Click("ctl00_MainContent_Button_ImportCourse");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=My objects");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=TestCourse");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=teacher(teacher)");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_1349");
             Selenium.Click("ctl00_MainContent_Button_Update");
             Assert.IsTrue(Selenium.IsTextPresent("teacher(teacher)"));
         }
         */
         [Test]
         public void TeacherShareCurr_OnlyUse()
         {
             string word = "teacher22";
             Selenium.Click("link=Create User");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", word);
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", word);
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", word);
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", word);
             Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
             Pause(2000);
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_lbLogin");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_cbLectorRole");
             Selenium.Click("ctl00_MainContent_btnApply");
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Curriculums");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_TextBox_Name");
             Selenium.Type("ctl00_MainContent_TextBox_Name", "TeacherCurr");
             Selenium.Type("ctl00_MainContent_TextBox_Description", "TeacherCurr");
             Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");

             Selenium.Click("link=My objects");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=TeacherCurr");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=" + word + "(" + word + ")");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr[2]/td/label");
             Selenium.Click("ctl00_MainContent_Button_Update");
             Selenium.WaitForPageToLoad("7000");
             //Selenium.Refresh();
             //Selenium.WaitForPageToLoad("7000");
             //Assert.AreEqual("teacher15(teacher15)", Selenium.GetTable("ctl00_MainContent_Table_SharedWith.0.0"));

             Selenium.Click("ctl00_hypLogout");
             Selenium.Click("ctl00_btnOK");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_Login1_UserName", word);
             Selenium.Type("ctl00_MainContent_Login1_Password", word);
             Selenium.Click("ctl00_MainContent_Login1_LoginButton");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=My objects");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=TeacherCurr");

             Assert.IsTrue(Selenium.IsTextPresent("granted you permission to Use this curriculum"));

             Selenium.Click("ctl00_hypLogout");
             Selenium.Click("ctl00_btnOK");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
             Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
             Selenium.Click("ctl00_MainContent_Login1_LoginButton");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Users");
             Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_btnYes");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Curriculums");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
             Selenium.Click("ctl00_MainContent_Button_Delete");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_Button_Delete");
             Selenium.WaitForPageToLoad("7000");
         }
         [Test]
         public void TeacherShareCurr_OnlyModify()
         {
             string word = "teacher25";
             Selenium.Click("link=Create User");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", word);
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", word);
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", word);
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", word);
             Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
             Pause(2000);
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_lbLogin");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_cbLectorRole");
             Selenium.Click("ctl00_MainContent_btnApply");
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Curriculums");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_TextBox_Name");
             Selenium.Type("ctl00_MainContent_TextBox_Name", "TeacherCurr");
             Selenium.Type("ctl00_MainContent_TextBox_Description", "TeacherCurr");
             Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");

             Selenium.Click("link=My objects");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=TeacherCurr");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=" + word + "(" + word + ")");
             Selenium.WaitForPageToLoad("7000");
             selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr/td/label");
             Selenium.Click("ctl00_MainContent_Button_Update");
             Selenium.WaitForPageToLoad("7000");
             //Selenium.Refresh();
             //Selenium.WaitForPageToLoad("7000");
             //Assert.AreEqual("teacher15(teacher15)", Selenium.GetTable("ctl00_MainContent_Table_SharedWith.0.0"));

             Selenium.Click("ctl00_hypLogout");
             Selenium.Click("ctl00_btnOK");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_Login1_UserName", word);
             Selenium.Type("ctl00_MainContent_Login1_Password", word);
             Selenium.Click("ctl00_MainContent_Login1_LoginButton");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=My objects");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=TeacherCurr");

             Assert.IsTrue(Selenium.IsTextPresent("granted you permission to Modify this curriculum"));

             Selenium.Click("ctl00_hypLogout");
             Selenium.Click("ctl00_btnOK");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
             Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
             Selenium.Click("ctl00_MainContent_Login1_LoginButton");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Users");
             Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_btnYes");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Curriculums");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
             Selenium.Click("ctl00_MainContent_Button_Delete");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_Button_Delete");
             Selenium.WaitForPageToLoad("7000");
         }

         [Test]
         public void TeacherShareCurr_UseAndModify()
         {
             string word = "teacher30";
             Selenium.Click("link=Create User");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", word);
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", word);
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", word);
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", word);
             Selenium.Click("ctl00_MainContent_CreateUserWizard1___CustomNav0_StepNextButtonButton");
             Pause(2000);
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_lbLogin");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_cbLectorRole");
             Selenium.Click("ctl00_MainContent_btnApply");
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Curriculums");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_TextBox_Name");
             Selenium.Type("ctl00_MainContent_TextBox_Name", "TeacherCurr");
             Selenium.Type("ctl00_MainContent_TextBox_Description", "TeacherCurr");
             Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");

             Selenium.Click("link=My objects");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=TeacherCurr");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=" + word + "(" + word + ")");
             Selenium.WaitForPageToLoad("7000");
             selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr/td/label");
             Selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr[2]/td/label");
             Selenium.Click("ctl00_MainContent_Button_Update");
             Selenium.WaitForPageToLoad("7000");
             //Selenium.Refresh();
             //Selenium.WaitForPageToLoad("7000");
             //Assert.AreEqual("teacher15(teacher15)", Selenium.GetTable("ctl00_MainContent_Table_SharedWith.0.0"));

             Selenium.Click("ctl00_hypLogout");
             Selenium.Click("ctl00_btnOK");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_Login1_UserName", word);
             Selenium.Type("ctl00_MainContent_Login1_Password", word);
             Selenium.Click("ctl00_MainContent_Login1_LoginButton");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=My objects");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=TeacherCurr");

             Assert.IsTrue(Selenium.IsTextPresent("granted you permission to Modify this curriculum"));
             Assert.IsTrue(Selenium.IsTextPresent("granted you permission to Use this curriculum"));

             Selenium.Click("ctl00_hypLogout");
             Selenium.Click("ctl00_btnOK");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
             Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
             Selenium.Click("ctl00_MainContent_Login1_LoginButton");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Users");
             Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl05_btnAction");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_btnYes");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Curriculums");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
             Selenium.Click("ctl00_MainContent_Button_Delete");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_Button_Delete");
             Selenium.WaitForPageToLoad("7000");
         }
         /*
         [Test]
         public void TeacherShareCurr_OnlyView()
         {
             Selenium.Open("/Student/StudentPage.aspx");
             Selenium.Click("link=Create User");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "teacher");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "teacher");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "teacher");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "aa");
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl07_lbLogin");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_cbLectorRole");
             Selenium.Click("ctl00_MainContent_btnApply");
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Courses");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
             Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
             Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\course.zip");
             Selenium.Click("ctl00_MainContent_Button_ImportCourse");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=My objects");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=TestCourse");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=teacher(teacher)");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_1349");
             Selenium.Click("ctl00_MainContent_Button_Update");
             Assert.IsTrue(Selenium.IsTextPresent("teacher(teacher)"));
         }
         [Test]
         public void TeacherShareCurr_OnlyPass()
         {
             Selenium.Open("/Student/StudentPage.aspx");
             Selenium.Click("link=Create User");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName", "teacher");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Password", "teacher");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_ConfirmPassword", "teacher");
             Selenium.Type("ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_Email", "aa");
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl07_lbLogin");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_cbLectorRole");
             Selenium.Click("ctl00_MainContent_btnApply");
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Courses");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
             Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
             Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\course.zip");
             Selenium.Click("ctl00_MainContent_Button_ImportCourse");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=My objects");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=TestCourse");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=teacher(teacher)");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_1349");
             Selenium.Click("ctl00_MainContent_Button_Update");
             Assert.IsTrue(Selenium.IsTextPresent("teacher(teacher)"));
         }
         */

    }
}