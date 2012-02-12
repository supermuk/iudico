using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.TestingSystem.Selenium
{
    [TestFixture]
    internal class TestingSystemSeleniumTests : TestFixtureWeb
    {
        private const string LoadTime = "50000";

        private const string AdminName = "lex";
        private const string AdminPassword = "lex";

        private string StudentName;
        private const string StudentPassword = "testPassword";
        private string studentId;

        private string TeacherName;
        private const string TeacherPassword = "testPassword";
        private string teacherId;

        private string GroupName;
        private string groupId;

        private string CourseUri1 =
            ConfigurationManager.AppSettings["SELENIUM_URL"] +
            "Data/ContentPackagingOneFilePerSCO_SCORM20043rdEdition.zip";

        private string CourseName1 = "ContentPackagingOneFilePerSCO_SCORM20043rdEdition";
        private string topicName1;
        private string chapterName1;
        private string courseId1;
        private string topicId1;

        private string disciplineName1;
        private string disciplineId1;

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

            var random = new Random();

            // Creates users (student, teacher).

            StudentName = random.Next().ToString();
            CreateUser(StudentName, StudentPassword, AdminName, AdminPassword);
            studentId = GetUserId(StudentName, AdminName, AdminPassword);
            AddToRole(studentId, "Student", AdminName, AdminPassword);

            TeacherName = random.Next().ToString();
            CreateUser(TeacherName, TeacherPassword, AdminName, AdminPassword);
            teacherId = GetUserId(TeacherName, AdminName, AdminPassword);
            AddToRole(teacherId, "Teacher", AdminName, AdminPassword);

            // Imports courses and gets their ids.

            ImportCourse(CourseUri1, CourseName1, TeacherName, TeacherPassword);
            courseId1 = GetCourseId(CourseName1, TeacherName, TeacherPassword);

            string newCourseName1 = random.Next().ToString();
            
            RenameCourse(courseId1, CourseName1, newCourseName1, TeacherName, TeacherPassword);

            CourseName1 = newCourseName1;

            // Creates group and adds student to it. 

            GroupName = random.Next().ToString();
            CreateGroup(GroupName, TeacherName, TeacherPassword);
            AddToGroup(studentId, GroupName, AdminName, AdminPassword);
            groupId = GetGroupId(GroupName, TeacherName, TeacherPassword);

            disciplineName1 = random.Next().ToString();

            topicName1 = random.Next().ToString();

            chapterName1 = random.Next().ToString();
        }

        [TearDown]
        public void TearDown()
        {
            // Deletes all courses.

            DeleteCourse(courseId1, CourseName1, TeacherName, TeacherPassword);
      
            // Removes all groups.

            DeleteGroup(groupId, GroupName, TeacherName, TeacherPassword);

            // Deletes all disciplines.

            DeleteDiscipline(disciplineId1, disciplineName1, TeacherName, TeacherPassword);

            // Removes all users.

            DeleteUser(studentId, StudentName, AdminName, AdminPassword);
            DeleteUser(teacherId, TeacherName, AdminName, AdminPassword);

            CourseName1 = "ContentPackagingOneFilePerSCO_SCORM20043rdEdition";
        }

        private void Login(string userLogin, string userPassword)
        {
            if (Selenium.IsAlertPresent())
            {
                string alert = Selenium.GetAlert();
            }
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

        private void ImportCourse(string courseUri, string courseName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Course");
            Selenium.WaitForPageToLoad(LoadTime);
            if (!Selenium.IsTextPresent(courseName))
            {
                Selenium.Click("css=a[href='/Course/Import']");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.AttachFile("name=fileUpload", courseUri);
                Selenium.Click("css=input[id='Import']");
                Selenium.WaitForPageToLoad(LoadTime);
            }
            Logout();
        }

        private string GetCourseId(string courseName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Course");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + courseName + "')]]/td[5]/a[1]");
            Selenium.WaitForPageToLoad(LoadTime);
            string courseId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("Course/") + 7,
                                                               Selenium.GetLocation().IndexOf("/Edit") -
                                                               (Selenium.GetLocation().IndexOf("Course/") + 7));
            Logout();
            return courseId;
        }

        private void DeleteCourse(string courseId, string courseName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Course");
            Selenium.WaitForPageToLoad(LoadTime);
            if (Selenium.IsTextPresent(courseName))
            {
                Selenium.Click("css=a[href='/Course/" + courseId + "/Delete']");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.GetConfirmation();
            }
            Logout();
        }

        private void RenameCourse(string courseId, string courseName, string newCourseName, string teacherLogin,
                                  string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Course");
            Selenium.WaitForPageToLoad(LoadTime);
            if (Selenium.IsTextPresent(courseName))
            {
                Selenium.Click("css=a[href='/Course/" + courseId + "/Edit']");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.Type("Name", newCourseName);
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

        private string GetGroupId(string groupName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Group");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + groupName + "')]]/td[2]/a[1]");
            Selenium.WaitForPageToLoad(LoadTime);
            string groupId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("id=") + 3);
            Logout();
            return groupId;
        }

        private void DeleteGroup(string groupId, string groupName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Group");
            Selenium.WaitForPageToLoad(LoadTime);
            if (Selenium.IsTextPresent(groupName))
            {
                Selenium.Click("css=a[href='/Group/Delete?id=" + groupId + "']");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.GetConfirmation();
            }
            Logout();
        }

        private void AddToGroup(string userId, string groupName, string adminLogin, string adminPassword)
        {
            Login(adminLogin, adminPassword);
            Selenium.Open("/User/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/User/AddToGroup?id=" + userId);
            Selenium.WaitForPageToLoad(LoadTime);
            if (Selenium.GetSelectOptions("id=GroupRef").Contains(groupName))
            {
                Selenium.Select("id=GroupRef", groupName);
                Selenium.Click("css=input[value='Save']");
                Selenium.WaitForPageToLoad(LoadTime);
            }
            Logout();
        }

        private void CreateDiscipline(string disciplineName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Discipline");
            Selenium.WaitForPageToLoad(LoadTime);
            if (!Selenium.IsTextPresent(disciplineName))
            {
                Selenium.Open("/Discipline/Create");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.Type("id=Name", disciplineName);
                Selenium.Click("css=input[value='Create']");
                Selenium.WaitForPageToLoad(LoadTime);
            }
            Logout();
        }

        private string GetDisciplineId(string disciplineName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Discipline");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + disciplineName + "')]]/td[5]/a[1]");
            Selenium.WaitForPageToLoad(LoadTime);
            string disciplineId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("Discipline/") + 11,
                                                                   Selenium.GetLocation().IndexOf("/Edit") -
                                                                   (Selenium.GetLocation().IndexOf("Discipline/") + 11));
            Logout();
            return disciplineId;
        }

        private void DeleteDiscipline(string disciplineId, string disciplineName, string teacherLogin,
                                      string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Discipline");
            Selenium.WaitForPageToLoad(LoadTime);
            if (Selenium.IsTextPresent(disciplineName))
            {
                Selenium.Click("css=a[onclick='deleteItem(" + disciplineId + ")']");
                selenium.GetConfirmation();
            }
            Logout();
        }

        private void AddGroupToDiscipline(string disciplineId, string groupName, string teacherLogin,
                                          string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Discipline");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("css=a[href='/Discipline/" + disciplineId + "/Curriculum/Index']");
            Selenium.WaitForPageToLoad(LoadTime);
            if (!Selenium.IsTextPresent(groupName))
            {
                Selenium.Open("/Discipline/" + disciplineId + "/Curriculum/Create");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.Select("id=GroupId", groupName);
                Selenium.Click("css=input[value='Create']");
                Selenium.WaitForPageToLoad(LoadTime);
            }
            Logout();
        }

        private string GetCurriculumId(string disciplineId, string groupName, string teacherLogin,
                                       string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Discipline");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Discipline/" + disciplineId + "/Curriculum/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + groupName + "')]]/td[3]/a[1]");
            Selenium.WaitForPageToLoad(LoadTime);
            string curriculumId =
                Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("Curriculum/") + "Curriculum/".Length,
                                                 Selenium.GetLocation().IndexOf("/Edit") -
                                                 (Selenium.GetLocation().IndexOf("Curriculum/") + "Curriculum/".Length));
            Logout();
            return curriculumId;
        }

        private void AddTimeLineToDiscipline(string dateStart, string dateEnd, string disciplineId,
                                             string curriculumId, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Discipline");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Discipline/" + disciplineId + "/Curriculum/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Curriculum/" + curriculumId + "/CurriculumTimeline/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            if (!Selenium.IsTextPresent(dateStart))
            {
                Selenium.Open("/Curriculum/" + curriculumId + "/CurriculumTimeline/Create");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.Type("id=Timeline_StartDate", dateStart);
                Selenium.Type("id=Timeline_EndDate", dateEnd);
                Selenium.Click("css=input[value='Create']");
            }
            Logout();
        }

        private void AddTimeLineToChapter(string dateStart, string dateEnd, string disciplineId,
                                          string curriculumId, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Discipline");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Discipline/" + disciplineId + "/Curriculum/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Curriculum/" + curriculumId + "/ChapterTimeline/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            if (!Selenium.IsTextPresent(dateStart))
            {
                Selenium.Open("/Curriculum/" + curriculumId + "/ChapterTimeline/Create");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.Type("id=Timeline_StartDate", dateStart);
                Selenium.Type("id=Timeline_EndDate", dateEnd);
                Selenium.Click("css=input[value='Create']");
            }
            Logout();
        }

        private void AddChapter(string disciplineId, string chapterName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Discipline");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Discipline/" + disciplineId + "/Chapter/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            if (!Selenium.IsTextPresent(chapterName))
            {
                Selenium.Open("/Discipline/" + disciplineId + "/Chapter/Create");
                Selenium.WaitForPageToLoad(LoadTime);
                Selenium.Type("id=Name", chapterName);
                Selenium.Click("css=input[value='Create']");
            }
            Logout();
        }

        private string GetChapterId(string disciplineId, string chapterName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Discipline");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Discipline/" + disciplineId + "/Chapter/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + chapterName + "')]]/td[5]/a[1]");
            Selenium.WaitForPageToLoad(LoadTime);
            string chapterId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("Chapter/") + "Chapter/".Length,
                                                                Selenium.GetLocation().IndexOf("/Edit") -
                                                                (Selenium.GetLocation().IndexOf("Chapter/") + "Chapter/".Length));
            Logout();
            return chapterId;
        }

        private void AddTopic(string disciplineId, string chapterId, string topicName, string courseName,
                              string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Discipline");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Open("/Discipline/" + disciplineId + "/Chapter/Index");
            Selenium.WaitForPageToLoad("7000");
            Selenium.Open("/Chapter/" + chapterId + "/Topic/Index");
            Selenium.WaitForPageToLoad("7000");
            if (!Selenium.IsTextPresent(topicName))
            {
                Selenium.Open("/Chapter/" + chapterId + "/Topic/Create");
                Selenium.WaitForPageToLoad("7000");
                Selenium.Type("id=TopicName", topicName);
                Selenium.Select("id=CourseId", courseName);
                Selenium.Select("id=TopicTypeId", "Test");
                Selenium.Click("css=input[value='Create']");
            }
            Logout();
        }

        private string GetTopicId(string disciplineId, string chapterId, string topicName, string teacherLogin,
                                  string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Discipline");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Discipline/" + disciplineId + "/Chapter/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Chapter/" + chapterId + "/Topic/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Click("xpath=//table//tr[td//text()[contains(., '" + topicName + "')]]/td[6]/a[1]");
            Selenium.WaitForPageToLoad(LoadTime);
            string topicId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("Topic/") + "Topic/".Length,
                                                              Selenium.GetLocation().IndexOf("/Edit") -
                                                              (Selenium.GetLocation().IndexOf("Topic/") + "Topic/".Length));
            Logout();
            return topicId;
        }

        private void ChangeCourseInTopic(string courseName, string disciplineId, string chapterId, string topicId,
                                         string teacherLogin, string teacherPassword)
        {
            Login(TeacherName, teacherPassword);
            Selenium.Open("/Discipline");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Discipline/" + disciplineId + "/Chapter/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Chapter/" + chapterId + "/Topic/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Topic/" + topicId + "/Edit");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Select("id=CourseId", courseName);
            Selenium.Click("css=input[value='Update']");
            Logout();
        }

        public void WaitForText(string text, string timeout)
        {
            Thread.Sleep(5000);
            int sleepTime = 0;

            while (!Selenium.IsTextPresent(text))
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
        public void PlayNotPreviouslyAttemptedTopic()
        {
            // Creates assigned to group discipline that contains valid discipline timelines.

            CreateDiscipline(disciplineName1, TeacherName, TeacherPassword);
            disciplineId1 = GetDisciplineId(disciplineName1, TeacherName, TeacherPassword);
            AddGroupToDiscipline(disciplineId1, GroupName, TeacherName, TeacherPassword);
            string curriculumId1 = GetCurriculumId(disciplineId1, GroupName, TeacherName,
                                                   TeacherPassword);
            AddTimeLineToDiscipline("12/18/2010 4:23 PM", "12/18/2100 4:23 PM", disciplineId1, curriculumId1,
                                    TeacherName, TeacherPassword);
            AddChapter(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            string chapterId1 = GetChapterId(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            AddTopic(disciplineId1, chapterId1, topicName1, CourseName1, TeacherName, TeacherPassword);
            topicId1 = GetTopicId(disciplineId1, chapterId1, topicName1, TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);

            Selenium.Open("/Training/Play/" + topicId1);
            Selenium.WaitForPageToLoad(LoadTime);
            WaitForText("IUDICO",LoadTime);
            Assert.IsTrue(Selenium.GetTitle() == "Play course");
            Selenium.SelectFrame("player");
            Selenium.SelectFrame("frameLearnTask");
            Selenium.SelectFrame("frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Select an activity"));
            Assert.IsTrue(Selenium.IsTextPresent("Please select an activity to continue with the training."));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameToc");
            Assert.IsTrue(Selenium.IsElementPresent("css=a[title='How to Play']"));
            Assert.IsTrue(Selenium.IsElementPresent("css=a[title='Keeping Score']"));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Logout();
        }

        [Test]
        public void PlayTopicWithInvalidId()
        {
            Login(StudentName, StudentPassword);

            Selenium.Open("/Training/Play/-1");
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Error happened"));
            Assert.IsTrue(Selenium.IsTextPresent("Topic you had requested was not found!"));
            Assert.IsTrue(Selenium.IsElementPresent("css=a[href='/']"));

            Selenium.Open("/Training/Play/0");
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Error happened"));
            Assert.IsTrue(Selenium.IsTextPresent("Topic you had requested was not found!"));
            Assert.IsTrue(Selenium.IsElementPresent("css=a[href='/']"));
            Logout();
        }

        [Test]
        public void PlayTopicWithInvalidAvailabilityDueToGroupAssignment()
        {
            // Creates Not assigned to group discipline.

            CreateDiscipline(disciplineName1, TeacherName, TeacherPassword);
            disciplineId1 = GetDisciplineId(disciplineName1, TeacherName, TeacherPassword);
            AddChapter(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            string chapterId1 = GetChapterId(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            AddTopic(disciplineId1, chapterId1, topicName1, CourseName1, TeacherName, TeacherPassword);
            topicId1 = GetTopicId(disciplineId1, chapterId1, topicName1, TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);
            Selenium.Open("/Training/Play/" + topicId1);
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Error happened"));
            Assert.IsTrue(Selenium.IsTextPresent("You are not allowed to pass this topic!"));
            Assert.IsTrue(Selenium.IsElementPresent("css=a[href='/']"));
            Assert.IsFalse(Selenium.GetTitle() == "Play course");
            Logout();
        }

        [Test]
        public void PlayTopicWithInvalidAvailabilityDueToInvalidDisciplineTimelines()
        {
            // Creates assigned to group discipline with invalid discipline timelines.

            CreateDiscipline(disciplineName1, TeacherName, TeacherPassword);
            disciplineId1 = GetDisciplineId(disciplineName1, TeacherName, TeacherPassword);
            AddGroupToDiscipline(disciplineId1, GroupName, TeacherName, TeacherPassword);
            string curriculumId1 = GetCurriculumId(disciplineId1, GroupName, TeacherName,
                                                   TeacherPassword);
            AddTimeLineToDiscipline("12/18/1999 4:21 PM", "12/18/2000 4:21 PM", disciplineId1, curriculumId1,
                                    TeacherName, TeacherPassword);
            AddChapter(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            string chapterId1 = GetChapterId(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            AddTopic(disciplineId1, chapterId1, topicName1, CourseName1, TeacherName, TeacherPassword);
            topicId1 = GetTopicId(disciplineId1, chapterId1, topicName1, TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);
            Selenium.Open("/Training/Play/" + topicId1);
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Error happened"));
            Assert.IsTrue(Selenium.IsTextPresent("You are not allowed to pass this topic!"));
            Assert.IsTrue(Selenium.IsElementPresent("css=a[href='/']"));
            Assert.IsFalse(Selenium.GetTitle() == "Play course");
            Logout();
        }

        [Test]
        public void PlayTopicWithInvalidAvailabilityDueToInvalidChapterTimelines()
        {
            // Creates assigned to group discipline with valid discipline timelines, but invalid chapter timelines.

            CreateDiscipline(disciplineName1, TeacherName, TeacherPassword);
            disciplineId1 = GetDisciplineId(disciplineName1, TeacherName, TeacherPassword);
            AddGroupToDiscipline(disciplineId1, GroupName, TeacherName, TeacherPassword);
            string curriculumId1 = GetCurriculumId(disciplineId1, GroupName, TeacherName,
                                                   TeacherPassword);
            AddTimeLineToDiscipline("12/18/1999 1:23 PM", "12/18/2100 1:23 PM", disciplineId1, curriculumId1,
                                    TeacherName, TeacherPassword);
            AddChapter(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            string chapterId1 = GetChapterId(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            AddTimeLineToChapter("12/18/1999 1:23 PM", "12/18/2000 1:23 PM", disciplineId1, curriculumId1,
                                 TeacherName, TeacherPassword);
            AddTopic(disciplineId1, chapterId1, topicName1, CourseName1, TeacherName, TeacherPassword);
            topicId1 = GetTopicId(disciplineId1, chapterId1, topicName1, TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);
            Selenium.Open("/Training/Play/" + topicId1);
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Error happened"));
            Assert.IsTrue(Selenium.IsTextPresent("You are not allowed to pass this topic!"));
            Assert.IsTrue(Selenium.IsElementPresent("css=a[href='/']"));
            Assert.IsFalse(Selenium.GetTitle() == "Play course");
            Logout();
        }

        [Test]
        public void PlayCompletedTopic()
        {
            // Creates assigned to group discipline that contains valid discipline timelines.

            CreateDiscipline(disciplineName1, TeacherName, TeacherPassword);
            disciplineId1 = GetDisciplineId(disciplineName1, TeacherName, TeacherPassword);
            AddGroupToDiscipline(disciplineId1, GroupName, TeacherName, TeacherPassword);
            string curriculumId1 = GetCurriculumId(disciplineId1, GroupName, TeacherName,
                                                   TeacherPassword);
            AddTimeLineToDiscipline("12/18/2010 4:23 PM", "12/18/2100 4:23 PM", disciplineId1, curriculumId1,
                                    TeacherName, TeacherPassword);
            AddChapter(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            string chapterId1 = GetChapterId(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            AddTopic(disciplineId1, chapterId1, topicName1, CourseName1, TeacherName, TeacherPassword);
            topicId1 = GetTopicId(disciplineId1, chapterId1, topicName1, TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);

            Selenium.Open("/Training/Play/" + topicId1);
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.SelectFrame("player");
            Selenium.SelectFrame("frameLearnTask");
            Selenium.SelectFrame("frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Please select an activity to continue with the training."));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("css=a[title='The Rules of Golf']");
            Selenium.WaitForFrameToLoad("frameContent", LoadTime);
            Selenium.MouseUp("css=a[title='The Rules of Golf']");

            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameContent");
            WaitForText("The Rules of Golf (book)", LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=aSUBMIT");
            Selenium.WaitForFrameToLoad("frameContent", LoadTime);
            Selenium.MouseUp("id=aSUBMIT");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameContent");
            WaitForText("Submit this Training?", LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Submit this Training?"));
            Selenium.Click("id=submitBtn");
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Training Completed"));
            Assert.IsTrue(
                Selenium.IsTextPresent("This training has been completed. You cannot make any further changes."));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectWindow(null);
            Selenium.Open("/");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Training/Play/" + topicId1);
            WaitForText("Results", LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Results"));
            Logout();
        }

        [Test]
        public void PlaySuspendedTopic()
        {
            // Imports courses and gets their ids.

            ImportCourse(CourseUri1, CourseName1, TeacherName, TeacherPassword);
            courseId1 = GetCourseId(CourseName1, TeacherName, TeacherPassword);

            // Creates assigned to group discipline that contains valid discipline timelines.

            CreateDiscipline(disciplineName1, TeacherName, TeacherPassword);
            disciplineId1 = GetDisciplineId(disciplineName1, TeacherName, TeacherPassword);
            AddGroupToDiscipline(disciplineId1, GroupName, TeacherName, TeacherPassword);
            string curriculumId1 = GetCurriculumId(disciplineId1, GroupName, TeacherName,
                                                   TeacherPassword);
            AddTimeLineToDiscipline("12/18/2010 4:23 PM", "12/18/2100 4:23 PM", disciplineId1, curriculumId1,
                                    TeacherName, TeacherPassword);
            AddChapter(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            string chapterId1 = GetChapterId(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            AddTopic(disciplineId1, chapterId1, topicName1, CourseName1, TeacherName, TeacherPassword);
            topicId1 = GetTopicId(disciplineId1, chapterId1, topicName1, TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);

            Selenium.Open("/Training/Play/" + topicId1);
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.SelectFrame("player");
            Selenium.SelectFrame("frameLearnTask");
            Selenium.SelectFrame("frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Please select an activity to continue with the training."));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("css=a[title='The Rules of Golf']");
            Selenium.WaitForFrameToLoad("frameContent", LoadTime);
            Selenium.MouseUp("css=a[title='The Rules of Golf']");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("id=frameContent");
            WaitForText("The Rules of Golf (book)", LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");

            Selenium.Open("/");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Training/Play/" + topicId1);
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.WaitForFrameToLoad("frameContent", LoadTime);
            Selenium.SelectFrame("player");
            Selenium.SelectFrame("frameLearnTask");
            Selenium.SelectFrame("frameContent");
            WaitForText("The Rules of Golf (book)", LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Logout();
        }

        [Test]
        public void SuspendTopic()
        {
            // Imports courses and gets their ids.

            ImportCourse(CourseUri1, CourseName1, TeacherName, TeacherPassword);
            courseId1 = GetCourseId(CourseName1, TeacherName, TeacherPassword);

            // Creates assigned to group discipline that contains valid discipline timelines.

            CreateDiscipline(disciplineName1, TeacherName, TeacherPassword);
            disciplineId1 = GetDisciplineId(disciplineName1, TeacherName, TeacherPassword);
            AddGroupToDiscipline(disciplineId1, GroupName, TeacherName, TeacherPassword);
            string curriculumId1 = GetCurriculumId(disciplineId1, GroupName, TeacherName,
                                                   TeacherPassword);
            AddTimeLineToDiscipline("12/18/2010 4:23 PM", "12/18/2100 4:23 PM", disciplineId1, curriculumId1,
                                    TeacherName, TeacherPassword);
            AddChapter(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            string chapterId1 = GetChapterId(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            AddTopic(disciplineId1, chapterId1, topicName1, CourseName1, TeacherName, TeacherPassword);
            topicId1 = GetTopicId(disciplineId1, chapterId1, topicName1, TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);


            Selenium.Open("/Training/Play/" + topicId1);
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.SelectFrame("player");
            Selenium.SelectFrame("frameLearnTask");
            Selenium.SelectFrame("frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Please select an activity to continue with the training."));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("css=a[title='The Rules of Golf']");
            Selenium.WaitForFrameToLoad("frameContent", LoadTime);
            Selenium.MouseUp("css=a[title='The Rules of Golf']");
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            WaitForText("The Rules of Golf (book)", LoadTime);

            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");

            Selenium.Open("/");
            Selenium.WaitForPageToLoad(LoadTime);
            Logout();
        }

        [Test]
        public void SubmitTopic()
        {
            // Creates assigned to group discipline that contains valid discipline timelines.

            CreateDiscipline(disciplineName1, TeacherName, TeacherPassword);
            disciplineId1 = GetDisciplineId(disciplineName1, TeacherName, TeacherPassword);
            AddGroupToDiscipline(disciplineId1, GroupName, TeacherName, TeacherPassword);
            string curriculumId1 = GetCurriculumId(disciplineId1, GroupName, TeacherName,
                                                   TeacherPassword);
            AddTimeLineToDiscipline("12/18/2010 4:23 PM", "12/18/2100 4:23 PM", disciplineId1, curriculumId1,
                                    TeacherName, TeacherPassword);
            AddChapter(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            string chapterId1 = GetChapterId(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            AddTopic(disciplineId1, chapterId1, topicName1, CourseName1, TeacherName, TeacherPassword);
            topicId1 = GetTopicId(disciplineId1, chapterId1, topicName1, TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);

            Selenium.Open("/Training/Play/" + topicId1);
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.SelectFrame("player");
            Selenium.SelectFrame("frameLearnTask");
            Selenium.SelectFrame("frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Please select an activity to continue with the training."));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("css=a[title='The Rules of Golf']");
            Selenium.WaitForFrameToLoad("frameContent", LoadTime);
            Selenium.MouseUp("css=a[title='The Rules of Golf']");

            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameContent");
            WaitForText("The Rules of Golf (book)", LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=aSUBMIT");
            Selenium.WaitForFrameToLoad("frameContent", LoadTime);
            Selenium.MouseUp("id=aSUBMIT");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameContent");
            WaitForText("Submit this Training?", LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Submit this Training?"));
            Selenium.Click("id=submitBtn");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Training Completed"));
            Assert.IsTrue(
                Selenium.IsTextPresent("This training has been completed. You cannot make any further changes."));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Logout();
        }

        [Test]
        public void NavigateForwardThroughTopic()
        {
            // Creates assigned to group discipline that contains valid discipline timelines.

            CreateDiscipline(disciplineName1, TeacherName, TeacherPassword);
            disciplineId1 = GetDisciplineId(disciplineName1, TeacherName, TeacherPassword);
            AddGroupToDiscipline(disciplineId1, GroupName, TeacherName, TeacherPassword);
            string curriculumId1 = GetCurriculumId(disciplineId1, GroupName, TeacherName, TeacherPassword);
            AddTimeLineToDiscipline("12/18/2010 4:23 PM", "12/18/2100 4:23 PM", disciplineId1, curriculumId1,
                                    TeacherName, TeacherPassword);
            AddChapter(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            string chapterId1 = GetChapterId(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            AddTopic(disciplineId1, chapterId1, topicName1, CourseName1, TeacherName, TeacherPassword);
            topicId1 = GetTopicId(disciplineId1, chapterId1, topicName1, TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);
            Selenium.Open("/Training/Play/" + topicId1);
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.SelectFrame("player");
            Selenium.SelectFrame("frameLearnTask");
            Selenium.SelectFrame("frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Please select an activity to continue with the training."));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("css=a[title='The Rules of Golf']");
            Selenium.WaitForFrameToLoad("frameContent", LoadTime);
            Selenium.MouseUp("css=a[title='The Rules of Golf']");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameContent");
            WaitForText("The Rules of Golf (book)", LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Logout();
        }

        [Test]
        public void NavigateForwardOneStep()
        {
            // Creates assigned to group discipline that contains valid discipline timelines.

            CreateDiscipline(disciplineName1, TeacherName, TeacherPassword);
            disciplineId1 = GetDisciplineId(disciplineName1, TeacherName, TeacherPassword);
            AddGroupToDiscipline(disciplineId1, GroupName, TeacherName, TeacherPassword);
            string curriculumId1 = GetCurriculumId(disciplineId1, GroupName, TeacherName, TeacherPassword);
            AddTimeLineToDiscipline("12/18/2010 4:23 PM", "12/18/2100 4:23 PM", disciplineId1, curriculumId1,
                                    TeacherName, TeacherPassword);
            AddChapter(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            string chapterId1 = GetChapterId(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            AddTopic(disciplineId1, chapterId1, topicName1, CourseName1, TeacherName, TeacherPassword);
            topicId1 = GetTopicId(disciplineId1, chapterId1, topicName1, TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);
            Selenium.Open("/Training/Play/" + topicId1);
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.SelectFrame("player");
            Selenium.SelectFrame("frameLearnTask");
            Selenium.SelectFrame("frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Please select an activity to continue with the training."));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("css=a[title='The Rules of Golf']");
            Selenium.WaitForFrameToLoad("frameContent", LoadTime);
            Selenium.MouseUp("css=a[title='The Rules of Golf']");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameContent");
            WaitForText("The Rules of Golf (book)", LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));

            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("css=a[title='Other Scoring Systems']");
            Selenium.WaitForFrameToLoad("frameContent", LoadTime);
            Selenium.MouseUp("css=a[title='Other Scoring Systems']");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameContent");
            WaitForText("Other Scoring Systems", LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Other Scoring Systems"));

            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("css=a[title='The Rules of Golf']");
            Selenium.WaitForFrameToLoad("frameContent", LoadTime);
            Selenium.MouseUp("css=a[title='The Rules of Golf']");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameContent");
            WaitForText("The Rules of Golf (book)", LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));

            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("css=a[title='Playing Golf Quiz']");
            Selenium.WaitForFrameToLoad("frameContent", LoadTime);
            Selenium.MouseUp("css=a[title='Playing Golf Quiz']");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameContent");
            WaitForText("Knowledge Check", LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Knowledge Check"));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Logout();
        }

        [Test]
        public void NavigateChoiceToLastItemToFirst()
        {
            // Creates assigned to group discipline that contains valid discipline timelines.

            CreateDiscipline(disciplineName1, TeacherName, TeacherPassword);
            disciplineId1 = GetDisciplineId(disciplineName1, TeacherName, TeacherPassword);
            AddGroupToDiscipline(disciplineId1, GroupName, TeacherName, TeacherPassword);
            string curriculumId1 = GetCurriculumId(disciplineId1, GroupName, TeacherName, TeacherPassword);
            AddTimeLineToDiscipline("12/18/2010 4:23 PM", "12/18/2100 4:23 PM", disciplineId1, curriculumId1,
                                    TeacherName, TeacherPassword);
            AddChapter(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            string chapterId1 = GetChapterId(disciplineId1, chapterName1, TeacherName, TeacherPassword);
            AddTopic(disciplineId1, chapterId1, topicName1, CourseName1, TeacherName, TeacherPassword);
            topicId1 = GetTopicId(disciplineId1, chapterId1, topicName1, TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);
            Selenium.Open("/Training/Play/" + topicId1);
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.SelectFrame("player");
            Selenium.SelectFrame("frameLearnTask");
            Selenium.SelectFrame("frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Please select an activity to continue with the training."));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("css=a[title='Playing Golf Quiz']");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("css=a[title='Playing Golf Quiz']");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("id=frameContent");
            WaitForText("Knowledge Check", LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Knowledge Check"));

            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("css=a[title='How to Play']");
            Selenium.WaitForFrameToLoad("frameContent", LoadTime);
            Selenium.MouseUp("css=a[title='How to Play']");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("frameContent");
            WaitForText("Play of the game", LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Play of the game"));
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=up");
            Selenium.SelectFrame("relative=top");
            Logout();
        }
    }
}