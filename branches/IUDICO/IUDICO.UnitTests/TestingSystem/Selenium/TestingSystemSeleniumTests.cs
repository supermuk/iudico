using System;
using System.Configuration;
using System.Linq;
using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.TestingSystem.Selenium
{
    using System.Globalization;
    using System.Threading;

    [TestFixture]
    internal class TestingSystemSeleniumTests : SimpleWebTest
    {
        private const string LoadTime = "50000";

        private const string AdminName = "lex";
        private const string AdminPassword = "lex";

        private string userName;
        private const string UserPassword = "password";
        private string userId;

        private const string StudentRole = "Студент";
        private const string TeacherRole = "Вчитель";

        private string groupName;

        private string disciplineName;

        private string chapterName;
        private string chapterName2;

        private string curriculumId;

        private const string StartDate = "12.08.1999 4:23";
        private const string EndDate = "12.08.2100 4:23";

        private readonly string courseUri = ConfigurationManager.AppSettings["SELENIUM_URL"] + "Data/ContentPackagingOneFilePerSCO_SCORM20043rdEdition.zip";
        private string courseName = "ContentPackagingOneFilePerSCO_SCORM20043rdEdition";
        private string courseId;

        private readonly string courseUri2 = ConfigurationManager.AppSettings["SELENIUM_URL"] + "Data/SequencingRandomTest_SCORM20043rdEdition.zip";
        private string courseName2 = "SequencingRandomTest_SCORM20043rdEdition";
        private string courseId2;

        private string topicId;
        private string topicId2;

        private readonly Random random = new Random();

        [SetUp]
        public void SetUp()
        {
            ExSelenium.Selenium = selenium;
            ExSelenium.Timeout = LoadTime;
            selenium.SetSpeed("500");

            selenium.Open("/");
            selenium.WaitForPageToLoad(LoadTime);

            this.ChangeCulture();

            selenium.WindowMaximize();

            this.userName = "000" + this.random.Next().ToString(CultureInfo.InvariantCulture);
            this.CreateUser(this.userName, UserPassword, AdminName, AdminPassword);
            this.userId = this.GetUserId(this.userName, AdminName, AdminPassword);
            this.AddToRole(this.userId, TeacherRole, AdminName, AdminPassword);
            this.AddToRole(this.userId, StudentRole, AdminName, AdminPassword);

            this.ImportCourse(this.courseUri, this.courseName, this.userName, UserPassword);

            string newCourseName = "000" + this.random.Next().ToString(CultureInfo.InvariantCulture);
            this.RenameCourse(this.courseName, newCourseName, this.userName, UserPassword);
            this.courseName = newCourseName;
            this.courseId = this.GetCourseId(this.courseName, this.userName, UserPassword);

            this.ImportCourse(this.courseUri2, this.courseName2, this.userName, UserPassword);

            string newCourseName2 = "000" + this.random.Next().ToString(CultureInfo.InvariantCulture);
            this.RenameCourse(this.courseName2, newCourseName2, this.userName, UserPassword);
            this.courseName2 = newCourseName2;
            this.courseId2 = this.GetCourseId(this.courseName2, this.userName, UserPassword);

            this.groupName = "000" + this.random.Next().ToString(CultureInfo.InvariantCulture);
            this.CreateGroup(this.groupName, this.userName, UserPassword);

            this.disciplineName = "000" + this.random.Next().ToString(CultureInfo.InvariantCulture);
            this.CreateDiscipline(this.disciplineName, this.userName, UserPassword);

            this.chapterName = "000" + this.random.Next().ToString(CultureInfo.InvariantCulture);
            this.AddChapter(this.chapterName, this.disciplineName, this.userName, UserPassword);

            this.chapterName2 = "000" + this.random.Next().ToString(CultureInfo.InvariantCulture);
            this.AddChapter(this.chapterName2, this.disciplineName, this.userName, UserPassword);

            this.AddCourseToChapter(this.courseName, this.chapterName, this.disciplineName, this.userName, UserPassword);
            this.AddCourseToChapter(this.courseName2, this.chapterName2, this.disciplineName, this.userName, UserPassword);
        }

        [TearDown]
        public void TearDown()
        {
            this.DeleteCurriculum(this.groupName, this.userName, UserPassword);
            this.DeleteDiscipline(this.disciplineName, this.userName, UserPassword);
            this.DeleteCourse(this.courseName, this.userName, UserPassword);
            this.DeleteCourse(this.courseName2, this.userName, UserPassword);
            this.courseName = "ContentPackagingOneFilePerSCO_SCORM20043rdEdition";
            this.courseName2 = "SequencingRandomTest_SCORM20043rdEdition";
            this.DeleteGroup(this.groupName, this.userName, UserPassword);
            this.DeleteUser(this.userId, this.userName, AdminName, AdminPassword);
            this.Logout();
            selenium.SetSpeed("0");
        }

        private void ChangeCulture()
        {
            Thread.Sleep(300);
            if (selenium.IsTextPresent("UK"))
            {
                selenium.Click("xpath=/html/body/div/div/div[2]/div[2]/div[2]/span/a");
                selenium.WaitForPageToLoad(LoadTime);
            }
        }

        private void Login(string userLogin, string userPassword)
        {
            selenium.Open("/Account/Login");
            selenium.WaitForPageToLoad(LoadTime);
            selenium.Type("id=loginUsername", userLogin);
            selenium.Type("id=loginPassword", userPassword);
            selenium.Submit("css=form[action='/Account/LoginDefault']");
            selenium.WaitForPageToLoad(LoadTime);
        }

        private bool IsLogged(string userLogin = "")
        {
            this.ChangeCulture();

            if (userLogin == string.Empty)
            {
                return selenium.IsElementPresent("css=a[href='/Account/Logout']");
            }

            return selenium.IsTextPresent("Ви увійшли як " + userLogin);
        }

        private void Logout()
        {
            selenium.Open("/Account/Logout");
            selenium.WaitForPageToLoad(LoadTime);
        }

        private void CreateUser(string userLogin, string userPassword, string adminLogin, string adminPassword)
        {
            if (!this.IsLogged(adminLogin))
            {
                this.Login(adminLogin, adminPassword);
            }

            selenium.Open("/User/Index");
            selenium.WaitForPageToLoad(LoadTime);

            if (!selenium.IsTextPresent(userLogin))
            {
                selenium.Open("/User/Create");
                selenium.WaitForPageToLoad(LoadTime);
                selenium.Type("id=Username", userLogin);
                selenium.Type("id=Password", userPassword);
                selenium.Type("id=Email", userLogin + "@gmail.com");
                selenium.Type("id=Name", userLogin);
                selenium.Type("id=UserId", userLogin);
                selenium.Submit("css=form[action='/User/Create']");
                selenium.WaitForPageToLoad(LoadTime);
            }
        }

        private void DeleteUser(string id, string userLogin, string adminLogin, string adminPassword)
        {
            if (!this.IsLogged(adminLogin))
            {
                this.Login(adminLogin, adminPassword);
            }

            selenium.Open("/User/Index");
            selenium.WaitForPageToLoad(LoadTime);

            if (selenium.IsTextPresent(userLogin))
            {
                selenium.Click("css=a[href='/User/Delete?id=" + id + "']");
                selenium.WaitForPageToLoad(LoadTime);
                selenium.GetConfirmation();
            }
        }

        private string GetUserId(string userLogin, string adminLogin, string adminPassword)
        {
            if (!this.IsLogged(adminLogin))
            {
                this.Login(adminLogin, adminPassword);
            }

            selenium.Open("/User/Index");

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            selenium.Type("xpath=/html/body/div/div[2]/div[3]/div/div/div[2]/input", userLogin);
            ExSelenium.WaitForElement("xpath=//table//tr[td//text()[contains(., '" + userLogin + "')]]/td[8]/a[2]");

            selenium.Click("xpath=//table//tr[td//text()[contains(., '" + userLogin + "')]]/td[8]/a[2]");
            selenium.WaitForPageToLoad(LoadTime);

            return selenium.GetLocation().Substring(this.selenium.GetLocation().IndexOf("id=", StringComparison.Ordinal) + 3);
        }

        private void AddToRole(string loginId, string userRole, string adminLogin, string adminPassword)
        {
            this.ChangeCulture();

            if (!this.IsLogged(adminLogin))
            {
                this.Login(adminLogin, adminPassword);
            }

            selenium.Open("/User/Index");
            selenium.WaitForPageToLoad(LoadTime);

            selenium.Open("/User/AddToRole?id=" + loginId);
            selenium.WaitForPageToLoad(LoadTime);

            if (selenium.GetSelectOptions("id=RoleRef").Contains(userRole))
            {
                selenium.Select("id=RoleRef", userRole);
                selenium.Click("css=input[value='Зберегти']");
                selenium.WaitForPageToLoad(LoadTime);
            }
        }

        private void ImportCourse(string courseUriP, string courseNameP, string teacherLogin, string teacherPassword)
        {
            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            selenium.Open("/Course");
            selenium.WaitForPageToLoad(LoadTime);

            if (!selenium.IsTextPresent(courseNameP))
            {
                selenium.Click("css=a[href='/Course/Import']");
                selenium.WaitForPageToLoad(LoadTime);
                selenium.AttachFile("name=fileUpload", courseUriP);
                selenium.Click("css=input[id='Import']");
                selenium.WaitForPageToLoad(LoadTime);
            }
        }

        private void DeleteCourse(string courseNameP, string teacherLogin, string teacherPassword)
        {
            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

             selenium.Open("/Course");


            if (selenium.IsTextPresent(courseNameP))
            {
                if (selenium.IsAlertPresent())
                {
                    string temp = selenium.GetAlert();                   
                }

                selenium.Type("xpath=/html/body/div/div[2]/div[2]/div/div/div[2]/input", courseNameP);


                ExSelenium.WaitForElement("xpath=//table//tr[td//text()[contains(., '" + courseNameP + "')]]/td[6]/a[4]");

                selenium.Click("xpath=//table//tr[td//text()[contains(., '" + courseNameP + "')]]/td[6]/a[4]");
                selenium.GetConfirmation();
                selenium.WaitForPageToLoad(LoadTime);
            }
        }

        private void RenameCourse(string courseNameP, string newCourseName, string teacherLogin, string teacherPassword)
        {
            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            selenium.Open("/Course");

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            if (selenium.IsTextPresent(courseNameP))
            {
                while (selenium.IsAlertPresent())
                {
                    selenium.GetAlert();
                }

                selenium.Type("xpath=/html/body/div/div[2]/div[2]/div/div/div[2]/input", courseNameP);
                ExSelenium.WaitForElement("xpath=//table//tr[td//text()[contains(., '" + courseNameP + "')]]/td[6]/a");

                while (selenium.IsAlertPresent())
                {
                    selenium.GetAlert();
                }

                selenium.Click("xpath=//table//tr[td//text()[contains(., '" + courseNameP + "')]]/td[6]/a");
                ExSelenium.WaitForElement("id=Name");

                while (selenium.IsAlertPresent())
                {
                    selenium.GetAlert();
                }

                selenium.Type("id=Name", newCourseName);
                selenium.Click("xpath=/html/body/div[3]/div[11]/div/button");

                selenium.Open("/Course");
            }
        }

        private string GetCourseId(string courseNameP, string teacherLogin, string teacherPassword)
        {
            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            selenium.Open("/Course");

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }


            if (selenium.IsTextPresent(courseNameP))
            {
                selenium.Type("xpath=/html/body/div/div[2]/div[2]/div/div/div[2]/input", courseNameP);
                ExSelenium.WaitForElement("xpath=//table//tr[td//text()[contains(., '" + courseNameP + "')]]/td[6]/a[2]");

                selenium.Click("xpath=//table//tr[td//text()[contains(., '" + courseNameP + "')]]/td[6]/a[2]");
                selenium.WaitForPageToLoad(LoadTime);
                return selenium.GetLocation().Substring(this.selenium.GetLocation().IndexOf("Course/", System.StringComparison.Ordinal) + 7, this.selenium.GetLocation().IndexOf("/Parse", System.StringComparison.Ordinal) - (this.selenium.GetLocation().IndexOf("Course/", System.StringComparison.Ordinal) + 7));
            }

            return " ";
        }

        private void CreateGroup(string name, string teacherLogin, string teacherPassword)
        {
            this.ChangeCulture();

            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            selenium.Open("/Group/Index");
            selenium.WaitForPageToLoad(LoadTime);

            if (!selenium.IsTextPresent(name))
            {
                selenium.Open("/Group/Create");
                selenium.WaitForPageToLoad(LoadTime);
                selenium.Type("id=Name", name);
                selenium.Click("css=input[value='Створити']");
                selenium.WaitForPageToLoad(LoadTime);
            }
        }

        private void DeleteGroup(string name, string teacherLogin, string teacherPassword)
        {
            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            selenium.Open("/Group/Index");
            selenium.WaitForPageToLoad(LoadTime);

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            if (selenium.IsTextPresent(name))
            {
                selenium.Click("xpath=//table//tr[td//text()[contains(., '" + name + "')]]/td[2]/a[2]");
                selenium.GetConfirmation();
            }
        }

        private void AddToGroup(string id, string name, string adminLogin, string adminPassword)
        {
            this.ChangeCulture();

            if (!this.IsLogged(adminLogin))
            {
                this.Login(adminLogin, adminPassword);
            }

            selenium.Open("/User/Index");
            selenium.WaitForPageToLoad(LoadTime);
            selenium.Open("/User/AddToGroup?id=" + id);
            selenium.WaitForPageToLoad(LoadTime);

            if (selenium.GetSelectOptions("id=GroupRef").Contains(name))
            {
                selenium.Select("id=GroupRef", name);
                selenium.Click("css=input[value='Зберегти']");
                selenium.WaitForPageToLoad(LoadTime);
            }
        }

        private void CreateDiscipline(string name, string teacherLogin, string teacherPassword)
        {
            this.ChangeCulture();

            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            selenium.Open("/Discipline");
            selenium.WaitForPageToLoad(LoadTime);

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            if (!selenium.IsTextPresent(name))
            {
                selenium.Open("/Discipline/Create");
                selenium.WaitForPageToLoad(LoadTime);
                selenium.Type("id=Name", name);
                selenium.Click("css=input[value='#Create']");
                selenium.WaitForPageToLoad(LoadTime);
            }
        }

        private void DeleteDiscipline(string name, string teacherLogin, string teacherPassword)
        {
            this.ChangeCulture();

            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            selenium.Open("/Discipline");
            selenium.WaitForPageToLoad(LoadTime);

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            if (selenium.IsTextPresent(name))
            {
                
              selenium.Click("xpath=//table//tr[td//text()[contains(., '" + name + "')]]/td[5]/div/a[5]");
              selenium.GetConfirmation();
            }
        }

        private void AddChapter(string name, string disciplineNameP, string teacherLogin, string teacherPassword)
        {

            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            selenium.Open("/Discipline");
            selenium.WaitForPageToLoad(LoadTime);

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            this.selenium.Click("xpath=//table//tr[td//text()[contains(., '" + disciplineNameP + "')]]/td[5]/div/a");
            ExSelenium.WaitForElement("id=Name");
            selenium.Type("id=Name", name);
            selenium.Click("xpath=/html/body/div[2]/div[11]/div/button");
        }

        private void AddCourseToChapter(string courseNameP, string chapterNameP, string disciplineNameP, string teacherLogin, string teacherPassword)
        {

            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            selenium.Open("/Discipline");
            selenium.WaitForPageToLoad(LoadTime);

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            this.selenium.Click("xpath=//table//tr[td//text()[contains(., '" + disciplineNameP + "')]]/td[2]");
            ExSelenium.WaitForElement("xpath=//table//tr[td//text()[contains(., '" + chapterNameP + "')]]/td[5]/a");
            this.selenium.Click("xpath=//table//tr[td//text()[contains(., '" + chapterNameP + "')]]/td[5]/a");
            ExSelenium.WaitForElement("id=TopicName");
            selenium.Type("id=TopicName", chapterNameP);
            selenium.Select("id=TestCourseId", courseNameP);
            selenium.Select("id=TheoryCourseId", courseNameP);
            selenium.Click("xpath=/html/body/div[2]/div[11]/div/button");
        }

        private void CreateCurriculum(string groupNameP, string disciplineNameP, string startDateP, string endDateP, string teacherLogin, string teacherPassword)
        {
            this.ChangeCulture();

            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            selenium.Open("/Curriculum/Index");
            selenium.Open("/Curriculum/Create");
            selenium.Select("id=DisciplineId", disciplineNameP);
            selenium.Select("id=GroupId", groupNameP);
            selenium.Check("id=SetTimeline");
            ExSelenium.WaitForElement("id=StartDate");
            selenium.Type("id=StartDate", startDateP);
            selenium.Type("id=EndDate", endDateP);
            selenium.Click("css=input[value='Створити']");
            selenium.WaitForPageToLoad(LoadTime);
        }

        private string GetCurriculumId(string groupNameP, string disciplineNameP, string teacherLogin, string teacherPassword)
        {
            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            selenium.Open("/Curriculum/Index");

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            selenium.Click("xpath=//table//tr[td//text()[contains(., '" + disciplineNameP + "')]]/td[6]/a");
            selenium.WaitForPageToLoad(LoadTime);
            return selenium.GetLocation().Substring(this.selenium.GetLocation().IndexOf("Curriculum/", System.StringComparison.Ordinal) + "Curriculum/".Length, this.selenium.GetLocation().IndexOf("/Edit", System.StringComparison.Ordinal) - (this.selenium.GetLocation().IndexOf("Curriculum/", System.StringComparison.Ordinal) + "Curriculum/".Length));
        }

        private string GetTopicId(string groupNameP, string disciplineNameP, string chapterNameP, string teacherLogin, string teacherPassword)
        {
            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            selenium.Open("/Curriculum/Index");

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            selenium.Click("xpath=//table//tr[td//text()[contains(., '" + disciplineNameP + "')]]/td[6]/a[2]");
            selenium.WaitForPageToLoad(LoadTime);
            selenium.Click("xpath=//table//tr[td//text()[contains(., '" + chapterNameP + "')]]/td[4]/a[2]");
            selenium.WaitForPageToLoad(LoadTime);
            selenium.Click("xpath=//table//tr[td//text()[contains(., '" + chapterNameP + "')]]/td[9]/a");
            selenium.WaitForPageToLoad(LoadTime);
            return selenium.GetLocation().Substring(this.selenium.GetLocation().IndexOf("CurriculumChapterTopic/", System.StringComparison.Ordinal) + "CurriculumChapterTopic/".Length, this.selenium.GetLocation().IndexOf("/Edit", System.StringComparison.Ordinal) - (this.selenium.GetLocation().IndexOf("CurriculumChapterTopic/", System.StringComparison.Ordinal) + "CurriculumChapterTopic/".Length));
        }

        private void EditCurriculumChapters(string curriculumIdP, string chapterNameP, string startDateP, string endDateP, string teacherLogin, string teacherPassword)
        {
            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            selenium.Open("/Curriculum/" + curriculumIdP + "/CurriculumChapter/Index");

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            selenium.Click("xpath=//table//tr[td//text()[contains(., '" + chapterNameP + "')]]/td[4]/a");
            selenium.WaitForPageToLoad(LoadTime);
            selenium.Check("id=SetTimeline");
            ExSelenium.WaitForElement("id=StartDate");
            selenium.Type("id=StartDate", startDateP);
            selenium.Type("id=EndDate", endDateP);
            selenium.Click("css=input[value='Оновити']");
            selenium.WaitForPageToLoad(LoadTime);
        }

        private void EditCurriculumChaptersTopics(string curriculumIdP, string chapterNameP, string startDateP, string endDateP, string teacherLogin, string teacherPassword)
        {
            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            selenium.Open("/Curriculum/" + curriculumIdP + "/CurriculumChapter/Index");

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            selenium.Click("xpath=//table//tr[td//text()[contains(., '" + chapterNameP + "')]]/td[4]/a[2]");
            selenium.WaitForPageToLoad(LoadTime);
            selenium.Click("xpath=//table//tr[td//text()[contains(., '" + chapterNameP + "')]]/td[9]/a");
            selenium.WaitForPageToLoad(LoadTime);
            selenium.Check("id=SetTestTimeline");
            ExSelenium.WaitForElement("id=TestStartDate");
            selenium.Type("id=TestStartDate", startDateP);
            selenium.Type("id=TestEndDate", endDateP);
            selenium.Check("id=SetTheoryTimeline");
            ExSelenium.WaitForElement("id=TheoryStartDate");
            selenium.Type("id=TheoryStartDate", startDateP);
            selenium.Type("id=TheoryEndDate", endDateP);
            selenium.Click("css=input[value='Оновити']");
            selenium.WaitForPageToLoad(LoadTime);
        }

        private void DeleteCurriculum(string groupNameP, string teacherLogin, string teacherPassword)
        {
            if (!this.IsLogged(teacherLogin))
            {
                this.Login(teacherLogin, teacherPassword);
            }

            selenium.Open("/Curriculum/Index");

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            selenium.Click("xpath=//table//tr[td//text()[contains(., '" + groupNameP + "')]]/td[6]/a[3]");
            selenium.GetConfirmation();
        }

        public void WaitForText(string text, string timeout)
        {
            Thread.Sleep(5000);
            int sleepTime = 0;

            while (!selenium.IsTextPresent(text))
            {
                if (sleepTime > Convert.ToInt32(timeout))
                {
                    return;
                }

                Thread.Sleep(100);
                sleepTime += 100;
            }
        }

        [Test]
        public void PlayTopicWithInvalidTopicId()
        {
            this.AddToGroup(this.userId, this.groupName, AdminName, AdminPassword);

            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, EndDate, this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);

            this.topicId = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName, this.userName, UserPassword);

            this.selenium.Open("/Training/Play/" + "-1" + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Трапилась помилка"));
            Assert.IsTrue(selenium.IsTextPresent("Не вдалось знайти обрану тему!"));
            Assert.IsTrue(selenium.IsElementPresent("css=a[href='/']"));

            this.selenium.Open("/Training/Play/" + "0" + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Трапилась помилка"));
            Assert.IsTrue(selenium.IsTextPresent("Не вдалось знайти обрану тему!"));
            Assert.IsTrue(selenium.IsElementPresent("css=a[href='/']"));
        }

        [Test]
        public void PlayTopicWithInvalidCourseId()
        {
            this.AddToGroup(this.userId, this.groupName, AdminName, AdminPassword);

            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, EndDate, this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);


            this.topicId = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName, this.userName, UserPassword);

            this.selenium.Open("/Training/Play/" + this.topicId + "/" + "-1" + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Трапилась помилка"));
            Assert.IsTrue(selenium.IsTextPresent("Не вдалось знайти обрану тему!"));
            Assert.IsTrue(selenium.IsElementPresent("css=a[href='/']"));

            this.selenium.Open("/Training/Play/" + this.topicId + "/" + "0" + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Трапилась помилка"));
            Assert.IsTrue(selenium.IsTextPresent("Не вдалось знайти обрану тему!"));
            Assert.IsTrue(selenium.IsElementPresent("css=a[href='/']"));
        }

        [Test]
        public void PlayNotPreviouslyAttemptedTopic()
        {
            this.AddToGroup(this.userId, this.groupName, AdminName, AdminPassword);

            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, EndDate, this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);


            this.topicId = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName, this.userName, UserPassword);

            this.selenium.Open("/Training/Play/" + this.topicId + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(selenium.GetTitle() == "Проходження курсу");
            selenium.SelectFrame("player");
            selenium.SelectFrame("frameLearnTask");
            selenium.SelectFrame("frameContent");
            Assert.IsTrue(selenium.IsTextPresent("Виберіть завдання"));
            Assert.IsTrue(selenium.IsTextPresent("Будь ласка, виберіть завдання, щоб продовжити проходити курс."));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameToc");
            Assert.IsTrue(selenium.IsElementPresent("css=a[title='How to Play']"));
            Assert.IsTrue(selenium.IsElementPresent("css=a[title='Keeping Score']"));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
        }

        [Test]
        public void PlayTopicWithInvalidAvailabilityDueToGroupAssignment()
        {
            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, EndDate, this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);


            this.topicId = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName, this.userName, UserPassword);

            this.selenium.Open("/Training/Play/" + this.topicId + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Трапилась помилка"));
            Assert.IsTrue(selenium.IsTextPresent("Ви не маєте права проходити дану тему!"));
            Assert.IsTrue(selenium.IsElementPresent("css=a[href='/']"));
            Assert.IsFalse(selenium.GetTitle() == "Проходження курсу");
        }

        [Test]
        public void PlayTopicWithInvalidAvailabilityDueToInvalidDisciplineTimelines()
        {
            this.AddToGroup(this.userId, this.groupName, AdminName, AdminPassword);
            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, "12.09.1999 4:23", this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);


            this.topicId = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName, this.userName, UserPassword);

            this.selenium.Open("/Training/Play/" + this.topicId + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Трапилась помилка"));
            Assert.IsTrue(selenium.IsTextPresent("Ви не маєте права проходити дану тему!"));
            Assert.IsTrue(selenium.IsElementPresent("css=a[href='/']"));
            Assert.IsFalse(selenium.GetTitle() == "Проходження курсу");
        }

        [Test]
        public void PlayTopicWithInvalidAvailabilityDueToInvalidChapterTimelines()
        {
            this.AddToGroup(this.userId, this.groupName, AdminName, AdminPassword);

            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, EndDate, this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName, StartDate, "12.09.1999 4:23", this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);

            this.topicId = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName, this.userName, UserPassword);
     
            this.selenium.Open("/Training/Play/" + this.topicId + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Трапилась помилка"));
            Assert.IsTrue(selenium.IsTextPresent("Ви не маєте права проходити дану тему!"));
            Assert.IsTrue(selenium.IsElementPresent("css=a[href='/']"));
            Assert.IsFalse(selenium.GetTitle() == "Проходження курсу");
        }

        [Test]
        public void PlayTopicWithInvalidAvailabilityDueToInvalidTopicTimelines()
        {
            this.AddToGroup(this.userId, this.groupName, AdminName, AdminPassword);

            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, EndDate, this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName, StartDate, "12.09.1999 4:23", this.userName, UserPassword);


            this.topicId = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName, this.userName, UserPassword);

            this.selenium.Open("/Training/Play/" + this.topicId + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Трапилась помилка"));
            Assert.IsTrue(selenium.IsTextPresent("Ви не маєте права проходити дану тему!"));
            Assert.IsTrue(selenium.IsElementPresent("css=a[href='/']"));
            Assert.IsFalse(selenium.GetTitle() == "Проходження курсу");
        }

        [Test]
        public void PlayCompletedTopic()
        {
            this.AddToGroup(this.userId, this.groupName, AdminName, AdminPassword);

            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, EndDate, this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);


            this.topicId = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName, this.userName, UserPassword);

            this.selenium.Open("/Training/Play/" + this.topicId + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);

            selenium.SelectFrame("player");
            selenium.SelectFrame("frameLearnTask");
            selenium.SelectFrame("frameContent");
            Assert.IsTrue(selenium.IsTextPresent("Будь ласка, виберіть завдання, щоб продовжити проходити курс."));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameToc");
            selenium.MouseDown("css=a[title='The Rules of Golf']");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.MouseUp("css=a[title='The Rules of Golf']");

            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameContent");
            this.WaitForText("The Rules of Golf (book)", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("The Rules of Golf (book)"));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameToc");
            selenium.MouseDown("id=aSUBMIT");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.MouseUp("id=aSUBMIT");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameContent");
            this.WaitForText("Підтвердити цей курс?", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Підтвердити цей курс?"));
            selenium.Click("id=submitBtn");
            selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Курс пройдений"));
            Assert.IsTrue(
                selenium.IsTextPresent("Ви не можете робити будь-які подальші зміни після підтвердження цього курсу."));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectWindow(null);
            selenium.Open("/");
            selenium.WaitForPageToLoad(LoadTime);

            this.selenium.Open("/Training/Play/" + this.topicId + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);

            this.WaitForText("Результати", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Результати"));
        }

        [Test]
        public void PlaySuspendedTopic()
        {
            this.AddToGroup(this.userId, this.groupName, AdminName, AdminPassword);

            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, EndDate, this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);


            this.topicId = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName, this.userName, UserPassword);

            this.selenium.Open("/Training/Play/" + this.topicId + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);
           
            selenium.SelectFrame("player");
            selenium.SelectFrame("frameLearnTask");
            selenium.SelectFrame("frameContent");
            Assert.IsTrue(selenium.IsTextPresent("Будь ласка, виберіть завдання, щоб продовжити проходити курс."));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameToc");
            selenium.MouseDown("css=a[title='The Rules of Golf']");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.MouseUp("css=a[title='The Rules of Golf']");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("id=frameContent");
            this.WaitForText("The Rules of Golf (book)", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("The Rules of Golf (book)"));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");

            selenium.Open("/");
            selenium.WaitForPageToLoad(LoadTime);
            this.selenium.Open("/Training/Play/" + this.topicId + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);
            selenium.WaitForPageToLoad(LoadTime);
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.SelectFrame("player");
            selenium.SelectFrame("frameLearnTask");
            selenium.SelectFrame("frameContent");
            this.WaitForText("The Rules of Golf (book)", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("The Rules of Golf (book)"));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
        }

        [Test]
        public void SuspendTopic()
        {
            this.AddToGroup(this.userId, this.groupName, AdminName, AdminPassword);

            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, EndDate, this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);


            this.topicId = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName, this.userName, UserPassword);

            this.selenium.Open("/Training/Play/" + this.topicId + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);

            selenium.SelectFrame("player");
            selenium.SelectFrame("frameLearnTask");
            selenium.SelectFrame("frameContent");
            Assert.IsTrue(selenium.IsTextPresent("Будь ласка, виберіть завдання, щоб продовжити проходити курс."));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameToc");
            selenium.MouseDown("css=a[title='The Rules of Golf']");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.MouseUp("css=a[title='The Rules of Golf']");
            selenium.SelectFrame("relative=parent");
            selenium.SelectFrame("id=frameContent");
            this.WaitForText("The Rules of Golf (book)", LoadTime);

            Assert.IsTrue(selenium.IsTextPresent("The Rules of Golf (book)"));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");

            selenium.Open("/");
            selenium.WaitForPageToLoad(LoadTime);
        }

        [Test]
        public void SubmitTopic()
        {
            this.AddToGroup(this.userId, this.groupName, AdminName, AdminPassword);

            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, EndDate, this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);


            this.topicId = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName, this.userName, UserPassword);

            this.selenium.Open("/Training/Play/" + this.topicId + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);

            selenium.SelectFrame("player");
            selenium.SelectFrame("frameLearnTask");
            selenium.SelectFrame("frameContent");
            Assert.IsTrue(selenium.IsTextPresent("Будь ласка, виберіть завдання, щоб продовжити проходити курс."));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameToc");
            selenium.MouseDown("css=a[title='The Rules of Golf']");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.MouseUp("css=a[title='The Rules of Golf']");

            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameContent");
            this.WaitForText("The Rules of Golf (book)", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("The Rules of Golf (book)"));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameToc");
            selenium.MouseDown("id=aSUBMIT");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.MouseUp("id=aSUBMIT");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameContent");
            this.WaitForText("Підтвердити цей курс?", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Підтвердити цей курс?"));
            selenium.Click("id=submitBtn");
            selenium.WaitForPageToLoad(LoadTime);
            selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Курс пройдений"));
            Assert.IsTrue(
                selenium.IsTextPresent("Ви не можете робити будь-які подальші зміни після підтвердження цього курсу."));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectWindow(null);
        }

        [Test]
        public void NavigateForwardThroughTopic()
        {
            this.AddToGroup(this.userId, this.groupName, AdminName, AdminPassword);

            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, EndDate, this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);


            this.topicId = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName, this.userName, UserPassword);

            this.selenium.Open("/Training/Play/" + this.topicId + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);

            selenium.WaitForPageToLoad(LoadTime);
            selenium.SelectFrame("player");
            selenium.SelectFrame("frameLearnTask");
            selenium.SelectFrame("frameContent");
            Assert.IsTrue(selenium.IsTextPresent("Будь ласка, виберіть завдання, щоб продовжити проходити курс."));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameToc");
            selenium.MouseDown("css=a[title='The Rules of Golf']");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.MouseUp("css=a[title='The Rules of Golf']");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameContent");
            this.WaitForText("The Rules of Golf (book)", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("The Rules of Golf (book)"));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
        }

        [Test]
        public void NavigateForwardOneStep()
        {
            this.AddToGroup(this.userId, this.groupName, AdminName, AdminPassword);

            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, EndDate, this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);


            this.topicId = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName, this.userName, UserPassword);
           
            this.selenium.Open("/Training/Play/" + this.topicId + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);

            selenium.SelectFrame("player");
            selenium.SelectFrame("frameLearnTask");
            selenium.SelectFrame("frameContent");
            Assert.IsTrue(selenium.IsTextPresent("Будь ласка, виберіть завдання, щоб продовжити проходити курс."));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameToc");
            selenium.MouseDown("css=a[title='The Rules of Golf']");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.MouseUp("css=a[title='The Rules of Golf']");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameContent");
            this.WaitForText("The Rules of Golf (book)", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("The Rules of Golf (book)"));

            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameToc");
            selenium.MouseDown("css=a[title='Other Scoring Systems']");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.MouseUp("css=a[title='Other Scoring Systems']");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameContent");
            this.WaitForText("Other Scoring Systems", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Other Scoring Systems"));

            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameToc");
            selenium.MouseDown("css=a[title='The Rules of Golf']");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.MouseUp("css=a[title='The Rules of Golf']");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameContent");
            this.WaitForText("The Rules of Golf (book)", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("The Rules of Golf (book)"));

            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameToc");
            selenium.MouseDown("css=a[title='Playing Golf Quiz']");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.MouseUp("css=a[title='Playing Golf Quiz']");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameContent");
            this.WaitForText("Knowledge Check", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Knowledge Check"));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
        }

        [Test]
        public void NavigateChoiceToLastItemToFirst()
        {
            this.AddToGroup(this.userId, this.groupName, AdminName, AdminPassword);

            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, EndDate, this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName, StartDate, EndDate, this.userName, UserPassword);


            this.topicId = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName, this.userName, UserPassword);
          
            this.selenium.Open("/Training/Play/" + this.topicId + "/" + this.courseId + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);

            selenium.SelectFrame("player");
            selenium.SelectFrame("frameLearnTask");
            selenium.SelectFrame("frameContent");
            Assert.IsTrue(selenium.IsTextPresent("Будь ласка, виберіть завдання, щоб продовжити проходити курс."));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameToc");
            selenium.MouseDown("css=a[title='Playing Golf Quiz']");
            selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            selenium.MouseUp("css=a[title='Playing Golf Quiz']");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("id=frameContent");
            this.WaitForText("Knowledge Check", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Knowledge Check"));

            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameToc");
            selenium.MouseDown("css=a[title='How to Play']");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.MouseUp("css=a[title='How to Play']");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("frameContent");
            this.WaitForText("Play of the game", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Play of the game"));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=top");
        }

        [Test]
        public void NavigateByNextPrevButtons()
        {
            this.AddToGroup(this.userId, this.groupName, AdminName, AdminPassword);

            this.CreateCurriculum(this.groupName, this.disciplineName, StartDate, EndDate, this.userName, UserPassword);
            this.curriculumId = this.GetCurriculumId(this.groupName, this.disciplineName, this.userName, UserPassword);
            this.EditCurriculumChapters(this.curriculumId, this.chapterName2, StartDate, EndDate, this.userName, UserPassword);
            this.EditCurriculumChaptersTopics(this.curriculumId, this.chapterName2, StartDate, EndDate, this.userName, UserPassword);

            this.topicId2 = this.GetTopicId(this.groupName, this.disciplineName, this.chapterName2, this.userName, UserPassword);

            this.selenium.Open("/Training/Play/" + this.topicId2 + "/" + this.courseId2 + "/" + "Test");
            selenium.WaitForPageToLoad(LoadTime);

            selenium.SelectFrame("player");
            selenium.SelectFrame("frameLearnTask");
            selenium.SelectFrame("frameContent");
            Assert.IsTrue(selenium.IsElementPresent("id=butNext"));
            selenium.SelectFrame("contentFrame");
            this.WaitForText("Play of the game", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Play of the game"));
            selenium.SelectFrame("relative=up");
            selenium.Click("id=butNext");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.Click("id=butNext");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            Assert.IsTrue(selenium.IsElementPresent("id=butPrevious"));
            selenium.Click("id=butPrevious");
            selenium.WaitForFrameToLoad("frameContent", LoadTime);
            selenium.SelectFrame("contentFrame");
            this.WaitForText("Par", LoadTime);
            Assert.IsTrue(selenium.IsTextPresent("Par"));
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=up");
            selenium.SelectFrame("relative=top");
        }
    }
}