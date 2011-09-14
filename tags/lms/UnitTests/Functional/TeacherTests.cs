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
        }

        [TearDown]
        public void TearDown()
        {
            Selenium.Open("/Logout.ashx");
        }

        [Test]
        public void CourseImport()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            decimal courses = Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[2]/table/tbody/tr[2]/td/div/table");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            //Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\Users\\Ігор\\Desktop\\Kursova\\IUDICO\\Site\\TestCourses\\newEditor1.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("CourseEdit.aspx", null);
            //Assert.AreEqual("TestCourse", Selenium.GetTable("//div[@id='ctl00_MainContent_TreeView_Courses']/table.0.2"));          
            Assert.AreEqual(courses + 1, Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[2]/table/tbody/tr[2]/td/div/table"));
            
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
        }

        [Test]
        public void CourseImportNoIMSmanifest()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/Noimsmanifest.zip");
            //Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\Users\\Ігор\\Desktop\\Kursova\\IUDICO\\Site\\TestCourses\\newEditor1.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");

            AssertHtmlText("ctl00_MainContent_Label_PageMessage", "No imsmanifest.xml file found");
            AssertIsOnPage("CourseEdit.aspx", null);
        }

        [Test]
        public void CourseImportNoCourseSelected()
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
        public void CourseDelete()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            decimal courses = Selenium.GetXpathCount("//*[@id='ctl00_MainContent_TreeView_Courses']");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "TestCourse");
            Selenium.Click("ctl00_MainContent_TextBox_CourseDescription");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "TestCourse");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            //Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\Users\\Ігор\\Desktop\\Kursova\\IUDICO\\Site\\TestCourses\\newEditor1.zip");
            Selenium.Click("ctl00_MainContent_Button_ImportCourse");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("CourseEdit.aspx", null);
            Assert.AreEqual(courses, Selenium.GetXpathCount("//*[@id='ctl00_MainContent_TreeView_Courses']"));
            //Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Coursest0"));
        }

        [Test]
        public void CourseDeleteAttached()
        {
            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            
            decimal courses = Selenium.GetXpathCount("//*[@id='ctl00_MainContent_TreeView_Courses']");
            Selenium.Click("ctl00_MainContent_TextBox_CourseName");
            Selenium.Type("ctl00_MainContent_TextBox_CourseName", "Test_for_curriculum");
            Selenium.Type("ctl00_MainContent_TextBox_CourseDescription", "Test_for_curriculum");
            Selenium.AttachFile("ctl00_MainContent_FileUpload_Course", "http://localhost:2935/TestCourses/GoodCourse.zip");
            //Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\Users\\Ігор\\Desktop\\Kursova\\IUDICO\\Site\\TestCourses\\newEditor1.zip");
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
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            //Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");          
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            //AssertIsOnPage("CourseDeleteConfirmation.aspx", null);
            Assert.AreEqual(courses, Selenium.GetXpathCount("//*[@id='ctl00_MainContent_TreeView_Courses']"));

            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void Group_Create()
        {
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Test_Group2");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            decimal groups = (Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/div/table/tbody/tr") - 2);

            AssertIsOnPage("Groups.aspx", null);
            Assert.AreEqual(groups + 1, (Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/div/table/tbody/tr") - 1));
            Assert.AreEqual("New_Test_Group2", Selenium.GetTable("ctl00_MainContent_GroupList_gvGroups." + (groups + 1) + ".0"));

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            //ClickOnLButtonRemoveGroup(groups);
            //decimal groups2 = Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/div/table/tbody/tr/td[2]");
            //Selenium.Click("//html/body/form/center/div[2]/center/div[2]/div[3]/div/table/tbody/tr[" + (groups2) + "]/td[2]");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void Group_CreateNoName()
        {
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            decimal groups = (Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/div/table/tbody/tr") - 2);

            AssertIsOnPage("Groups.aspx", null);
            Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_GroupList_gvGroups." + (groups+1) + ".0"));
        }

        [Test]
        public void Group_Rename()
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
            Selenium.WaitForPageToLoad("7000"); decimal groups = (Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/div/table/tbody/tr") - 2);

            AssertIsOnPage("Groups.aspx", null);
            Assert.AreEqual("New_Group", Selenium.GetTable("ctl00_MainContent_GroupList_gvGroups." + (groups + 1) + ".0"));


            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
            Selenium.WaitForPageToLoad("7000");

        }

        [Test]
        public void Group_Try_To_Delete()
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
            decimal groups = (Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/div/table/tbody/tr") - 2);

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnCancel");

            AssertIsOnPage("Groups.aspx", null);
            Assert.AreEqual(groups + 1, (Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/div/table/tbody/tr")-1));
            Assert.AreEqual("Test_Group", Selenium.GetTable("ctl00_MainContent_GroupList_gvGroups." + (groups+1) + ".0"));

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
            Selenium.WaitForPageToLoad("7000");


        }
        [Test]
        public void Group_Delete()
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
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "Test_Group2");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            decimal groups = (Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/div/table/tbody/tr") - 2);

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl05_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl05_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("Groups.aspx", null);
            Assert.AreEqual(groups, (Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/div/table/tbody/tr") - 1));

            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void Group_Try_To_DeleteRenamed()
        {
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Test_Group");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.Click("ctl00_MainContent_tbGroupName");
            Selenium.Type("ctl00_MainContent_tbGroupName", "Very_New_Test_Group");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            decimal groups = (Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/div/table/tbody/tr") - 2);

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnCancel");

            AssertIsOnPage("Groups.aspx", null);
            Assert.AreEqual(groups + 1, (Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/div/table/tbody/tr") - 1));
            Assert.AreEqual("Very_New_Test_Group", Selenium.GetTable("ctl00_MainContent_GroupList_gvGroups." + (groups + 1) + ".0"));

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
            Selenium.WaitForPageToLoad("7000");
        }
        [Test]
        public void Group_DeleteRenamed()
        {
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_btnCreateGroup");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("ctl00_MainContent_tbGroupName", "New_Test_Group");
            Selenium.Click("ctl00_MainContent_btnCreate");
            Selenium.Click("ctl00_MainContent_tbGroupName");
            Selenium.Type("ctl00_MainContent_tbGroupName", "Very_New_Test_Group");
            Selenium.Click("ctl00_MainContent_btnApply");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");
            decimal groups = (Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/div/table/tbody/tr") - 2);

            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_lnkAction");
            Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl04_btnOK");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("link=Groups");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("Groups.aspx", null);
            Assert.AreEqual(groups, (Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/div/table/tbody/tr") - 1));
        }

        [Test]
        public void CurriculumCreate()
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
        public void CurriculumCreateWithStage()
        {
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
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
        public void CurriculumCreateAndModifyStage()
        {
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
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
        public void CurriculumCreateAndModify()
        {
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
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
        public void CurriculumCreateAndDeleteStage()
        {
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");

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
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void CurriculumCreateAndDelete()
        {
            Selenium.Click("link=Curriculums");
            Selenium.WaitForPageToLoad("7000");
            decimal curr = Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[3]/table/tbody/tr[2]/td/div/table");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            Assert.AreEqual(curr, Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[3]/table/tbody/tr[2]/td/div/table"));

        }

        [Test]
        public void CurriculumCreateWithCourse()
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
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            //Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
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
        public void CurriculumRenameWithCourse()
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
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            //Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
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
        public void CurriculumRenameWithCourse2()
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
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            //Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
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
        public void CuriculumDelete()
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
            decimal curr = Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[3]/table/tbody/tr[2]/td/div/table");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            //Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            //Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst2"));
            Assert.AreEqual(curr + 1, Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[3]/table/tbody/tr[2]/td/div/table"));
            
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
        public void CuriculumDelete2()
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
            decimal curr = Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[3]/table/tbody/tr[2]/td/div/table");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            //Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            //Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst1"));
            Assert.AreEqual(curr +1 , Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[3]/table/tbody/tr[2]/td/div/table"));

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
        public void CuriculumDelete3()
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
            decimal curr = Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[3]/table/tbody/tr[2]/td/div/table");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            //Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            AssertIsOnPage("CurriculumEdit.aspx", null);

            //Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst0"));
            Assert.AreEqual(curr, Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[3]/table/tbody/tr[2]/td/div/table"));

            Selenium.Click("link=Courses");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_TreeView_Coursest0");
            Selenium.Click("ctl00_MainContent_Button_DeleteCourse");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
        }

        [Test]
        public void CuriculumDeleteAndCreate()
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
            decimal curr = Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[3]/table/tbody/tr[2]/td/div/table");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            //Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("ctl00_MainContent_Button_Delete");
            Selenium.WaitForPageToLoad("7000");

            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            //Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst2");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            //Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst0"));
            Assert.AreEqual(curr+1, Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[3]/table/tbody/tr[2]/td/div/table"));

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
        public void CuriculumDeleteAndCreate2()
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
            decimal curr = Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[3]/table/tbody/tr[2]/td/div/table");
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            //Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
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
            //Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            //Selenium.Click("//img[@alt='Expand Curriculum_test2']");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            //Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst0"));
            Assert.AreEqual(curr + 1, Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[3]/table/tbody/tr[2]/td/div/table"));

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
        public void CuriculumDeleteAndCreate3()
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
            decimal curr = Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[3]/table/tbody/tr[2]/td/div/table");     
            Selenium.Click("ctl00_MainContent_TextBox_Name");
            Selenium.Type("ctl00_MainContent_TextBox_Name", "Curriculum_test");
            Selenium.Type("ctl00_MainContent_TextBox_Description", "Curriculum_test");
            Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
            Selenium.Click("ctl00_MainContent_Button_AddStage");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            //Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            //Selenium.Click("//img[@alt='Expand Curriculum_test']");

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
            //Selenium.Click("//img[@alt='Expand Curriculum_test2']");
            Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
            //Selenium.Click("//img[@alt='Expand Test_for_curriculum']");
            Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
            Selenium.Click("ctl00_MainContent_Button_AddTheme");
            //Selenium.Click("//img[@alt='Expand Curriculum_test2']");

            AssertIsOnPage("CurriculumEdit.aspx", null);
            //Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent_TreeView_Curriculumst0"));
            Assert.AreEqual(curr, Selenium.GetXpathCount("//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td[3]/table/tbody/tr[2]/td/div/table"));

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
            string word = "teacher002";
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
            //Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\Users\\Ігор\\Desktop\\Kursova\\IUDICO\\Site\\TestCourses\\newEditor1.zip");
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

            Assert.IsTrue(Selenium.IsTextPresent(" надав вам права для Use цього курс"));

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
            string word = "teacher012";
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

            Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Modify цього курс"));

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
            string word = "teacher020";
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

            Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Modify цього курс"));
            Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Use цього курс"));

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
        public void TeacherShareCourse_OnlyUse_AllowDelegate()
        {
            string word = "teacher030";
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
            Pause(300);
            selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr[2]/td[2]/label");
            Selenium.Click("ctl00_MainContent_Button_Update");
            Selenium.WaitForPageToLoad("7000");

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

            Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Use цього курс"));

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
         public void TeacherShareCourse_OnlyModify_AllowDelegate()
         {
            string word = "teacher040";
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
            Pause(300);
            selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr/td[2]/label");

            Selenium.Click("ctl00_MainContent_Button_Update");
            Selenium.WaitForPageToLoad("7000");

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

            Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Modify цього курс"));

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
         public void TeacherShareCourse_UseAndModify_AllowDelegate()
         {
             string word = "teacher051";
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
            Pause(300);
            selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr/td[2]/label");
            selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr[2]/td/label");
            Pause(300);
            selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr[2]/td[2]/label");
            Selenium.Click("ctl00_MainContent_Button_Update");
            Selenium.WaitForPageToLoad("7000");

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

            Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Modify цього курс"));
            Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Use цього курс"));

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
         public void TeacherShareCurr_OnlyUse()
         {
             string word = "teacher060";
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

             Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Use цього навчальний план"));

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
             string word = "teacher070";
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

             Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Modify цього навчальний план"));

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
             string word = "teacher080";
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

             Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Modify цього навчальний план"));
             Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Use цього навчальний план"));

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
         public void TeacherShareCurr_OnlyUse_AllowDelegate()
         {
             string word = "teacher090";
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
             selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr[2]/td/label");
             Pause(300);
             selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr[2]/td[2]/label");
             Selenium.Click("ctl00_MainContent_Button_Update");
             Selenium.WaitForPageToLoad("7000");

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

             Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Use цього навчальний план"));

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
         public void TeacherShareCurr_OnlyModify_AllowDelegate()
         {
             string word = "teacher100";
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
             Pause(300);
             selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr/td[2]/label");
             Selenium.Click("ctl00_MainContent_Button_Update");
             Selenium.WaitForPageToLoad("7000");

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

             Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Modify цього навчальний план"));

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
         public void TeacherShareCurr_UseAndModify_AllowDelegate()
         {
             string word = "teacher110";
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
             selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr[2]/td/label");
             Pause(300);
             selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr[2]/td[2]/label");
             selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr/td/label");
             Pause(300);
             selenium.Click("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[2]/tbody/tr/td/table/tbody/tr/td[2]/label");
             Selenium.Click("ctl00_MainContent_Button_Update");
             Selenium.WaitForPageToLoad("7000");

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

             Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Modify цього навчальний план"));
             Assert.IsTrue(Selenium.IsTextPresent("надав вам права для Use цього навчальний план"));

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


         //add assignment
         [Test]
         public void Group_Assign_To_Curr()
         {
             
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
             //Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\Users\\Ігор\\Desktop\\Kursova\\IUDICO\\Site\\TestCourses\\newEditor1.zip");
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
             //Selenium.Click("//img[@alt='Expand Ass_curric']");
             Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
             //Selenium.Click("//img[@alt='Expand Ass_course']");
             Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
             Selenium.Click("ctl00_MainContent_Button_AddTheme");
             Selenium.Click("link=Assignment");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Select("ctl00_MainContent_GroupList", "label=Ass_test_group");
             Selenium.Click("ctl00_MainContent_Button_AddGroup");
             ClickOnButtonWithValue("Assign");
             Selenium.WaitForPageToLoad("7000");
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
             Assert.IsTrue(Selenium.IsElementPresent("ctl00_MainContent__curriculumTreeViewt0"));

             Selenium.Click("link=Groups");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
             Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Curriculums");
             Selenium.WaitForPageToLoad("7000");
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
         public void Group_Unsign_From_Curr()
         {
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
             //Selenium.Click("//img[@alt='Expand Ass_curric']");
             Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
             //Selenium.Click("//img[@alt='Expand Ass_course']");
             Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
             Selenium.Click("ctl00_MainContent_Button_AddTheme");
             Selenium.Click("link=Assignment");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Select("ctl00_MainContent_GroupList", "label=Ass_test_group");
             Selenium.Click("ctl00_MainContent_Button_AddGroup");
             ClickOnButtonWithValue("Assign");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Users");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_UserList_gvUsers_ctl03_lbLogin");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_btnInclude");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_gvGroups_ctl02_lnkSelect");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_btnYes");

             Selenium.Click("link=Assignment");
             Selenium.WaitForPageToLoad("7000");
             ClickOnButtonWithValue("Unsign");
             Selenium.Click("link=Home");
             Selenium.WaitForPageToLoad("7000");

             Assert.IsFalse(Selenium.IsElementPresent("ctl00_MainContent__curriculumTreeViewt0"));

             Selenium.Click("link=Groups");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
             Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Curriculums");
             Selenium.WaitForPageToLoad("7000");
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
         public void Statistic_Viev()
         {
             string name = "TestUser002";

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
             //Selenium.Type("ctl00_MainContent_FileUpload_Course", "C:\\Users\\Ігор\\Desktop\\Kursova\\IUDICO\\Site\\TestCourses\\newEditor1.zip");

             Selenium.Click("ctl00_MainContent_Button_ImportCourse");
             Selenium.WaitForPageToLoad("7000");
             //Selenium.Click("//img[@alt='Expand Test']");
             Selenium.Click("link=Curriculums");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Type("ctl00_MainContent_TextBox_Name", "Test");
             Selenium.Type("ctl00_MainContent_TextBox_Description", "Test");
             Selenium.Click("ctl00_MainContent_Button_CreateCurriculum");
             Selenium.Click("ctl00_MainContent_TreeView_Curriculumst0");
             Selenium.Click("ctl00_MainContent_Button_AddStage");
             //Selenium.Click("//img[@alt='Expand Test']");
             Selenium.Click("ctl00_MainContent_TreeView_Curriculumst1");
             //Selenium.Click("//img[@alt='Expand Test']");
             Selenium.Click("ctl00_MainContent_TreeView_Coursesn1CheckBox");
             Selenium.Click("ctl00_MainContent_Button_AddTheme");
             //Selenium.Click("//img[@alt='Expand Test']");
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

             Selenium.Click("ctl00_hypLogout");
             Selenium.Click("ctl00_btnOK");
             Selenium.WaitForPageToLoad("7000");

             Selenium.Type("ctl00_MainContent_Login1_UserName", "lex");
             Selenium.Type("ctl00_MainContent_Login1_Password", "lex");
             Selenium.Click("ctl00_MainContent_Login1_LoginButton");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("link=Statistic");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_CheckBoxCurriculums_0");
             Selenium.Click("ctl00_MainContent_Button_Show");
             Selenium.WaitForPageToLoad("7000");
             Assert.AreEqual( name+"("+name+")",Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[3]/tbody/tr[2]/th"));
             Assert.AreEqual("1/1(1 attempt )",Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[3]/tbody/tr[2]/td[1]"));
             Assert.AreEqual("1/1",Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[3]/tbody/tr[2]/td[2]"));
             Assert.AreEqual("100.00",Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[3]/tbody/tr[2]/td[3]"));
             Assert.AreEqual("100.00",Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[3]/tbody/tr[2]/td[3]"));
             Assert.AreEqual("A",Selenium.GetText("xpath=//html/body/form/center/div[2]/center/div[2]/div[3]/table[3]/tbody/tr[2]/td[4]"));

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
             Selenium.Click("link=Groups");
             Selenium.WaitForPageToLoad("7000");
             Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_lnkAction");
             Selenium.Click("ctl00_MainContent_GroupList_gvGroups_ctl03_btnOK");
             Selenium.WaitForPageToLoad("7000");
         }

    }
}