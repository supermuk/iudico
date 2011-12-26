using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.Statistics
{
    [TestFixture]
    class TestingSystemSeleniumTests : TestFixtureWeb
    {
        private const string LoadTime = "7000";

        private const string AdminName = "lex";
        private const string AdminPassword = "lex";

        private const string StudentName = "testStudent";
        private const string StudentPassword = "testPassword";
        private string studentId;

        private const string TeacherName = "testTeacher";
        private const string TeacherPassword = "testPassword";
        private string teacherId;

        private const string GroupName = "testGroup";
        private string groupId;

        private const string CourseUri1 = "http://localhost:1556/Data/ContentPackagingOneFilePerSCO_SCORM20043rdEdition.zip";
        private const string CourseName1 = "ContentPackagingOneFilePerSCO_SCORM20043rdEdition";
        private string courseId1;
        private string themeId1;

        private const string curriculumName1 = "testCurriculum1";
        private string curriculumId1;

        //private bool IsFirstRun = true;

        [SetUp]
        public void SetUp()
        {
            Selenium.Open("/");

            Selenium.WaitForPageToLoad(LoadTime);

            Selenium.WindowMaximize();

            if (!Selenium.IsElementPresent("css=a[href='/Account/ChangeCulture?lang=en-US&returnUrl=%2F']"))
            {
                Selenium.Click("css=a[href='/Account/ChangeCulture?lang=uk-UA&returnUrl=%2F']");
                Selenium.WaitForPageToLoad(LoadTime);
            }

            CreateUser(StudentName, StudentPassword, AdminName, AdminPassword);
            studentId = GetUserId(StudentName, AdminName, AdminPassword);
            AddToRole(studentId, "Student", AdminName, AdminPassword);

            CreateUser(TeacherName, TeacherPassword, AdminName, AdminPassword);
            teacherId = GetUserId(TeacherName, AdminName, AdminPassword);
            AddToRole(teacherId, "Teacher", AdminName, AdminPassword);
        }

        [TearDown]
        public void TearDown()
        {
            DeleteUser(teacherId, TeacherName, AdminName, AdminPassword);
        }

        [Test]
        public void NoGroupInSystem()
        {
            Login(TeacherName, TeacherPassword);

            Selenium.Open("/Group/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Group/Create");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.SelectWindow("null");
            Logout();
        }

        [Test]
        public void NoCurriculumsAssignedToGroup()
        {
            Login(TeacherName, TeacherPassword);

            CreateGroup(GroupName, TeacherName, TeacherPassword);
            Login(TeacherName, TeacherPassword);
            Selenium.Open("/Stats");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("//input[@value='Show']");
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsFalse(selenium.IsElementPresent("No curricuulm has been created for testGroup."));
            Selenium.SelectWindow("null");
            Logout();
        }

        [Test]
        public void SelectNoSelectionCurriculums()
        {
            Login(TeacherName, TeacherPassword);

            CreateGroup(GroupName, TeacherName, TeacherPassword);
            CreateCurriculum(curriculumName1, TeacherName, TeacherPassword);
            curriculumId1 = GetCurriculumId(curriculumName1, TeacherName, TeacherPassword);
            AddGroupToCurriculum(curriculumId1, GroupName, TeacherName, TeacherPassword);
            Login(TeacherName, TeacherPassword);
            Selenium.Open("/Stats");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("//input[@value='Show']");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("//input[@value='Show']");
            Assert.IsTrue(Selenium.GetAlert() == "Please, select one or more curriculum ");
            Selenium.SelectWindow("null");
            Logout();
        }

        [Test]
        public void ViewCurriculumsResult()
        {
            Login(TeacherName, TeacherPassword);

            CreateGroup(GroupName, TeacherName, TeacherPassword);
            CreateCurriculum(curriculumName1, TeacherName, TeacherPassword);
            curriculumId1 = GetCurriculumId(curriculumName1, TeacherName, TeacherPassword);
            AddGroupToCurriculum(curriculumId1, GroupName, TeacherName, TeacherPassword);
            Login(TeacherName, TeacherPassword);
            Selenium.Open("/Stats");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("//input[@value='Show']");
            Selenium.WaitForPageToLoad(LoadTime);
            //Selenium.Click("//input[@value='Show']");
            //Selenium.WaitForPageToLoad(LoadTime);
            Selenium.SelectWindow("null");
            Logout();
        }

        [Test]
        public void ViewThemeResult()
        {
            CreateCurriculum(curriculumName1, TeacherName, TeacherPassword);
            curriculumId1 = GetCurriculumId(curriculumName1, TeacherName, TeacherPassword);
            AddGroupToCurriculum(curriculumId1, GroupName, TeacherName, TeacherPassword);
            string curriculumAssignmentId1 = GetCurriculumAssignmentId(curriculumId1, GroupName, TeacherName, TeacherPassword);
            AddTimeLineToCurriculum("12/18/2010 4:23 PM", "12/18/2100 4:23 PM", curriculumId1, curriculumAssignmentId1, TeacherName, TeacherPassword);
            AddStage(curriculumId1, "testStage1", TeacherName, TeacherPassword);
            string stageId1 = GetStageId(curriculumId1, "testStage1", TeacherName, TeacherPassword);
            AddTheme(curriculumId1, stageId1, "testTheme1", CourseName1, TeacherName, TeacherPassword);
            themeId1 = GetThemeId(curriculumId1, stageId1, "testTheme1", TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);
            Selenium.Open("/Training/Play/" + themeId1);
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Please select an activity to continue with the training."));
            Logout();

        }

        private void Login(string userLogin, string userPassword)
        {
            Selenium.Open("/Account/Login");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Type("id=loginUsername", userLogin);
            Selenium.Type("id=loginPassword", userPassword);
            Selenium.Submit("css=form[action='/Account/LoginDefault']");
            Selenium.WaitForPageToLoad(LoadTime);
        }

        private void Logout()
        {
            Selenium.Open("/Account/Logout");
            Selenium.WaitForPageToLoad("7000");
        }

        private void CreateUser(string userLogin, string userPassword, string adminLogin, string adminPassword)
        {
            Login(adminLogin, adminPassword);
            Selenium.Open("/User/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            if (!Selenium.IsTextPresent(userLogin))
            {
                Selenium.Open("/User/Create");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.Type("id=Username", userLogin);
                Selenium.Type("id=Password", userPassword);
                Selenium.Type("id=Email", userLogin + "@gmail.com");
                Selenium.Type("id=Name", userLogin);
                Selenium.Type("id=UserId", userLogin);
                Selenium.Submit("css=form[action='/User/Create']");
                Selenium.WaitForPageToLoad(LoadTime);
            }
            Logout();
        }

        private void DeleteUser(string userId, string userLogin, string adminLogin, string adminPassword)
        {
            Login(adminLogin, adminPassword);
            Selenium.Open("/User/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            if (Selenium.IsTextPresent(userLogin))
            {
                Selenium.Click("css=a[href='/User/Delete?id=" + userId + "']");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.GetConfirmation();
            }
            Logout();
        }

        private string GetUserId(string userLogin, string adminLogin, string adminPassword)
        {
            Login(adminLogin, adminPassword);
            Selenium.Open("/User/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + userLogin + "')]]/td[7]/a[3]");
            Selenium.WaitForPageToLoad(LoadTime);
            string userId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("id=") + 3);
            Logout();
            return userId;
        }

        private void AddToRole(string userId, string userRole, string adminLogin, string adminPassword)
        {
            Login(adminLogin, adminPassword);
            Selenium.Open("/User/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/User/AddToRole?id=" + userId);
            Selenium.WaitForPageToLoad(LoadTime);
            if (Selenium.GetSelectOptions("id=RoleRef").Contains(userRole))
            {
                Selenium.Select("id=RoleRef", userRole);
                Selenium.Click("css=input[value='Save']");
                Selenium.WaitForPageToLoad(LoadTime);
            }
            Logout();
        }


        private void CreateGroup(string groupName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Group/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            if (!Selenium.IsTextPresent(groupName))
            {
                Selenium.Open("/Group/Create");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.Type("id=Name", groupName);
                Selenium.Click("css=input[value='Create']");
                Selenium.WaitForPageToLoad(LoadTime);
            }
            Logout();
        }

        private void CreateCurriculum(string curriculumName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Curriculum");
            Selenium.WaitForPageToLoad(LoadTime);
            if (!Selenium.IsTextPresent(curriculumName))
            {
                Selenium.Open("/Curriculum/Create");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.Type("id=Name", curriculumName);
                Selenium.Click("css=input[value='Create']");
                Selenium.WaitForPageToLoad(LoadTime);
            }
            Logout();
        }

        private string GetCurriculumId(string curriculumName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Curriculum");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + curriculumName + "')]]/td[5]/a[1]");
            Selenium.WaitForPageToLoad(LoadTime);
            string curriculumId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("Curriculum/") + 11, Selenium.GetLocation().IndexOf("/Edit") - (Selenium.GetLocation().IndexOf("Curriculum/") + 11));
            Logout();
            return curriculumId;
        }

        private void AddGroupToCurriculum(string curriculumId, string groupName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Curriculum");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("css=a[href='/Curriculum/" + curriculumId + "/CurriculumAssignment/Index']");
            Selenium.WaitForPageToLoad(LoadTime);
            if (!Selenium.IsTextPresent(groupName))
            {
                Selenium.Open("/Curriculum/" + curriculumId + "/CurriculumAssignment/Create");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.Select("id=GroupId", groupName);
                Selenium.Click("css=input[value='Create']");
                Selenium.WaitForPageToLoad(LoadTime);
            }
            Logout();
        }

        private string GetCurriculumAssignmentId(string curriculumId, string groupName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Curriculum");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Curriculum/" + curriculumId + "/CurriculumAssignment/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + groupName + "')]]/td[3]/a[1]");
            Selenium.WaitForPageToLoad(LoadTime);
            string curriculumAssignmentId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("CurriculumAssignment/") + 21, Selenium.GetLocation().IndexOf("/Edit") - (Selenium.GetLocation().IndexOf("CurriculumAssignment/") + 21));
            Logout();
            return curriculumAssignmentId;
        }

        private void AddTimeLineToCurriculum(string dateStart, string dateEnd, string curriculumId, string curriculumAssignmentId, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Curriculum");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Curriculum/" + curriculumId + "/CurriculumAssignment/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/CurriculumAssignment/" + curriculumAssignmentId + "/CurriculumAssignmentTimeline/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            if (!Selenium.IsTextPresent(dateStart))
            {
                Selenium.Open("/CurriculumAssignment/" + curriculumAssignmentId + "/CurriculumAssignmentTimeline/Create");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.Type("id=Timeline_StartDate", dateStart);
                Selenium.Type("id=Timeline_EndDate", dateEnd);
                Selenium.Click("css=input[value='Create']");
            }
            Logout();
        }

        private void AddTimeLineToStage(string dateStart, string dateEnd, string curriculumId, string curriculumAssignmentId, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Curriculum");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Curriculum/" + curriculumId + "/CurriculumAssignment/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/CurriculumAssignment/" + curriculumAssignmentId + "/StageTimeline/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            if (!Selenium.IsTextPresent(dateStart))
            {
                Selenium.Open("/CurriculumAssignment/" + curriculumAssignmentId + "/StageTimeline/Create");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.Type("id=Timeline_StartDate", dateStart);
                Selenium.Type("id=Timeline_EndDate", dateEnd);
                Selenium.Click("css=input[value='Create']");
            }
            Logout();
        }

        private void AddStage(string curriculumId, string stageName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Curriculum");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Curriculum/" + curriculumId + "/Stage/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            if (!Selenium.IsTextPresent(stageName))
            {
                Selenium.Open("/Curriculum/" + curriculumId + "/Stage/Create");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.Type("id=Name", stageName);
                Selenium.Click("css=input[value='Create']");
            }
            Logout();
        }

        private string GetStageId(string curriculumId, string stageName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Curriculum");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Curriculum/" + curriculumId + "/Stage/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + stageName + "')]]/td[5]/a[1]");
            Selenium.WaitForPageToLoad(LoadTime);
            string stageId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("Stage/") + 6, Selenium.GetLocation().IndexOf("/Edit") - (Selenium.GetLocation().IndexOf("Stage/") + 6));
            Logout();
            return stageId;
        }

        private void AddTheme(string curriculumId, string stageId, string themeName, string courseName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Curriculum");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Open("/Curriculum/" + curriculumId + "/Stage/Index");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Open("/Stage/" + stageId + "/Theme/Index");
            Selenium.WaitForPageToLoad("7000");
            if (!Selenium.IsTextPresent(themeName))
            {
                Selenium.Open("/Stage/" + stageId + "/Theme/Create");
                Selenium.WaitForPageToLoad("7000");
                Selenium.Type("id=ThemeName", themeName);
                Selenium.Select("id=CourseId", courseName);
                Selenium.Select("id=ThemeTypeId", "Test");
                Selenium.Click("css=input[value='Create']");
            }
            Logout();
        }

        private string GetThemeId(string curriculumId, string stageId, string themeName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Curriculum");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Curriculum/" + curriculumId + "/Stage/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Stage/" + stageId + "/Theme/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + themeName + "')]]/td[6]/a[1]");
            Selenium.WaitForPageToLoad(LoadTime);
            string themeId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("Theme/") + 6, Selenium.GetLocation().IndexOf("/Edit") - (Selenium.GetLocation().IndexOf("Theme/") + 6));
            Logout();
            return themeId;
        }
    }
}
