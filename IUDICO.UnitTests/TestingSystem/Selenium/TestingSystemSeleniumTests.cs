using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.UnitTests.Base;
using NUnit.Framework;
using System.IO;

namespace IUDICO.UnitTests.TestingSystem.Selenium
{
    [TestFixture]
    class TestingSystemSeleniumTests : TestFixtureWeb
    {
        const string studentName = "testStudent";
        const string studentPassword = "testPassword";
        const string teacherName = "testTeacher";
        const string teacherPassword = "testPassword";
        private string teacherId;
        private string studentId;
        const string groupName = "testGroup";
        const string curriculumName = "testCurriculum";
        private string curriculumId;
        private string curriculumAssignmentId;
        const string stageName = "testStage";
        private string stageId;
        const string themeName = "testTheme";

        [SetUp]
        public void SetUp()
        {
            Selenium.Open("/Account/Login");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("id=loginUsername", "lex");
            Selenium.Type("id=loginPassword", "lex");
            Selenium.Submit("css=form[action='/Account/LoginDefault']");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("css=a[href='/User/Index']");
            Selenium.WaitForPageToLoad("7000");

            if (!Selenium.IsTextPresent(studentName))
            {
                Selenium.Click("css=a[href='/User/Create']");
                Selenium.WaitForPageToLoad("7000");

                Selenium.Type("id=Username", studentName);
                Selenium.Type("id=Password", studentPassword);
                Selenium.Type("id=Email", studentName + "@gmail.com");
                Selenium.Type("id=Name", studentName);
                Selenium.Type("id=UserId", studentName);
                Selenium.Submit("css=form[action='/User/Create']");
                Selenium.WaitForPageToLoad("7000");
            }

            Selenium.Click("css=a[href='/User/Index']");
            Selenium.WaitForPageToLoad("7000");

            if (!Selenium.IsTextPresent(teacherName))
            {
                Selenium.Click("css=a[href='/User/Create']");
                Selenium.WaitForPageToLoad("7000");

                Selenium.Type("id=Username", teacherName);
                Selenium.Type("id=Password", teacherPassword);
                Selenium.Type("id=Email", teacherName + "@gmail.com");
                Selenium.Type("id=Name", teacherName);
                Selenium.Type("id=UserId", teacherName);
                Selenium.Submit("css=form[action='/User/Create']");
                Selenium.WaitForPageToLoad("7000");
            }

            Selenium.Click("css=a[href='/User/Index']");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + teacherName + "')]]/td[7]/a[3]");
            Selenium.WaitForPageToLoad("7000");

            teacherId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("id=") + 3);

            Selenium.Click("css=a[href='/User/Index']");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + studentName + "')]]/td[7]/a[3]");
            Selenium.WaitForPageToLoad("7000");

            studentId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("id=") + 3);

            Selenium.Click("css=a[href='/User/Index']");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("css=a[href='/User/AddToRole?id=" + studentId + "']");
            Selenium.WaitForPageToLoad("7000");

            if (Selenium.GetSelectOptions("id=RoleRef").Contains("Student"))
            {
                Selenium.Select("id=RoleRef", "Student");
                Selenium.Click("css=input[value='Save']");
                Selenium.WaitForPageToLoad("7000");
            }

            Selenium.Click("css=a[href='/User/Index']");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("css=a[href='/User/AddToRole?id=" + teacherId + "']");
            Selenium.WaitForPageToLoad("7000");

            if (Selenium.GetSelectOptions("id=RoleRef").Contains("Teacher"))
            {
                Selenium.Select("id=RoleRef", "Teacher");
                Selenium.Click("css=input[value='Save']");
                Selenium.WaitForPageToLoad("7000");
            }

            Selenium.Click("css=a[href='/User/Index']");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("css=a[href='/Account/Logout']");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Open("/Account/Login");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("id=loginUsername", teacherName);
            Selenium.Type("id=loginPassword", teacherPassword);
            Selenium.Submit("css=form[action='/Account/LoginDefault']");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("css=a[href='/Group/Index']");
            Selenium.WaitForPageToLoad("7000");



            if (!Selenium.IsTextPresent(groupName))
            {
                Selenium.Click("css=a[href='/Group/Create']");
                Selenium.WaitForPageToLoad("7000");
                Selenium.Type("id=Name", groupName);
                Selenium.Click("css=input[value='Create']");
                Selenium.WaitForPageToLoad("7000");
            }

            Selenium.Click("css=a[href='/Account/Logout']");

            Selenium.Open("/Account/Login");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("id=loginUsername", "lex");
            Selenium.Type("id=loginPassword", "lex");
            Selenium.Submit("css=form[action='/Account/LoginDefault']");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("css=a[href='/User/Index']");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("css=a[href='/User/AddToGroup?id=" + studentId + "']");
            Selenium.WaitForPageToLoad("7000");

            if (Selenium.GetSelectOptions("id=GroupRef").Contains(groupName))
            {
                Selenium.Select("id=GroupRef", groupName);
                Selenium.Click("css=input[value='Save']");
                Selenium.WaitForPageToLoad("7000");
            }

            Selenium.Click("css=a[href='/User/Index']");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("css=a[href='/Account/Logout']");

            Selenium.Open("/Account/Login");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("id=loginUsername", teacherName);
            Selenium.Type("id=loginPassword", teacherPassword);
            Selenium.Submit("css=form[action='/Account/LoginDefault']");
            Selenium.WaitForPageToLoad("7000");



            Selenium.Click("css=a[href='/Curriculum']");
            Selenium.WaitForPageToLoad("7000");

            if (!Selenium.IsTextPresent(curriculumName))
            {
                Selenium.Click("css=a[href='/Curriculum/Create']");
                Selenium.WaitForPageToLoad("7000");
                Selenium.Type("id=Name", curriculumName);
                Selenium.Click("css=input[value='Create']");
                Selenium.WaitForPageToLoad("7000");
            }

            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + curriculumName + "')]]/td[5]/a[1]");
            Selenium.WaitForPageToLoad("7000");

            curriculumId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("Curriculum/") + 11, Selenium.GetLocation().IndexOf("/Edit") - (Selenium.GetLocation().IndexOf("Curriculum/") + 11));

            Selenium.Click("css=a[href='/Curriculum/" + curriculumId + "/Index']");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("css=a[href='/Curriculum/" + curriculumId + "/CurriculumAssignment/Index']");
            Selenium.WaitForPageToLoad("7000");

            if (!Selenium.IsTextPresent(groupName))
            {
                Selenium.Click("css=a[href='/Curriculum/" + curriculumId + "/CurriculumAssignment/Create']");
                Selenium.WaitForPageToLoad("7000");
                Selenium.Select("id=GroupId", groupName);
                Selenium.Click("css=input[value='Save']");
                Selenium.WaitForPageToLoad("7000");
            }

            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + groupName + "')]]/td[3]/a[1]");
            Selenium.WaitForPageToLoad("7000");

            curriculumAssignmentId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("CurriculumAssignment/") + 21, Selenium.GetLocation().IndexOf("/Edit") - (Selenium.GetLocation().IndexOf("CurriculumAssignment/") + 21));

            Selenium.Click("css=a[href='/Curriculum/" + curriculumId + "/CurriculumAssignment/Index']");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("css=a[href='/CurriculumAssignment/" + curriculumAssignmentId + "/CurriculumAssignmentTimeline/Index']");
            Selenium.WaitForPageToLoad("7000");

            if (!Selenium.IsTextPresent("12/15/2010 8:28 AM"))
            {
                Selenium.Click("css=a[href='/CurriculumAssignment/" + curriculumAssignmentId +
                               "/CurriculumAssignmentTimeline/Create']");
                Selenium.WaitForPageToLoad("7000");
                Selenium.Type("id=Timeline_StartDate", "12/15/2010 8:28 AM");
                Selenium.Type("id=Timeline_EndDate", "12/15/2100 8:28 AM");
                Selenium.Click("css=input[value='Create']");
            }

            Selenium.Click("css=a[href='/Curriculum']");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Click("css=a[href='/Curriculum/" + curriculumId + "/Stage/Index']");
            Selenium.WaitForPageToLoad("7000");



            if (!Selenium.IsTextPresent(stageName))
            {
                Selenium.Click("css=a[href='/Curriculum/" + curriculumId + "/Stage/Create']");
                Selenium.WaitForPageToLoad("7000");
                Selenium.Type("id=Name", stageName);
                Selenium.Click("css=input[value='Create']");
            }

            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + stageName + "')]]/td[5]/a[1]");
            Selenium.WaitForPageToLoad("7000");

            stageId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("Stage/") + 6, Selenium.GetLocation().IndexOf("/Edit") - (Selenium.GetLocation().IndexOf("Stage/") + 6));

            Selenium.Click("css=a[href='/Curriculum/" + curriculumId + "/Stage/Index']");
            Selenium.WaitForPageToLoad("7000");

            Selenium.Click("css=a[href='/Stage/" + stageId + "/Theme/Index']");
            Selenium.WaitForPageToLoad("7000");



            if (!Selenium.IsTextPresent(themeName))
            {
                Selenium.Click("css=a[href='/Stage/" + stageId + "/Theme/Create']");
                Selenium.WaitForPageToLoad("7000");
                Selenium.Type("id=ThemeName", themeName);
                Selenium.Select("id=CourseId", "RunTimeAdvancedCalls_SCORM20043rdEdition");
                Selenium.Select("id=ThemeTypeId", "Test");
                Selenium.Click("css=input[value='Create']");
            }

            Selenium.Click("css=a[href='/Account/Logout']");
            Selenium.WaitForPageToLoad("7000");
        }

        [TearDown]
        public void TearDown()
        {
            Selenium.Open("/Account/Login");
        }

        [Test]
        public void PlayNotPreviouslyAttemptedTheme()
        {
            Selenium.Open("/Account/Login");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Type("id=loginUsername", studentName);
            Selenium.Type("id=loginPassword", studentPassword);
            Selenium.Submit("css=form[action='/Account/LoginDefault']");
            Selenium.WaitForPageToLoad("7000");
        }
    }
}
