using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.TestingSystem.Selenium
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

        private const string CourseUri2 = "http://localhost:1556/Data/RunTimeAdvancedCalls_SCORM20043rdEdition.zip";
        private const string CourseName2 = "RunTimeAdvancedCalls_SCORM20043rdEdition";
        private string courseId2;
        private string themeId2;

        private const string CourseUri3 = "http://localhost:1556/Data/SequencingForcedSequential_SCORM20043rdEdition.zip";
        private const string CourseName3 = "SequencingForcedSequential_SCORM20043rdEdition";
        private string courseId3;
        private string themeId3;

        private const string CourseUri4 = "http://localhost:1556/Data/SequencingRandomTest_SCORM20043rdEdition.zip";
        private const string CourseName4 = "SequencingRandomTest_SCORM20043rdEdition";
        private string courseId4;
        private string themeId4;

        private const string curriculumName1 = "testCurriculum1";
        private string curriculumId1;

        private string curriculumId2;
        private const string curriculumName2 = "testCurriculum2";

        private string curriculumId3;
        private const string curriculumName3 = "testCurriculum3";

        private string curriculumId4;
        private const string curriculumName4 = "testCurriculum4";

        private bool IsFirstRun = true;

        [SetUp]
        public void SetUp()
        {
            Selenium.Start();

            Selenium.Open("/");
            Selenium.WaitForPageToLoad(LoadTime);

            Selenium.WindowMaximize();

            if (!Selenium.IsElementPresent("css=a[href='/Account/ChangeCulture?lang=en-US&returnUrl=%2F']"))
            {
                Selenium.Click("css=a[href='/Account/ChangeCulture?lang=uk-UA&returnUrl=%2F']");
                Selenium.WaitForPageToLoad(LoadTime);
            }

            // Creates users (student, teacher).

            CreateUser(StudentName, StudentPassword, AdminName, AdminPassword);
            studentId = GetUserId(StudentName, AdminName, AdminPassword);
            AddToRole(studentId, "Student", AdminName, AdminPassword);

            CreateUser(TeacherName, TeacherPassword, AdminName, AdminPassword);
            teacherId = GetUserId(TeacherName, AdminName, AdminPassword);
            AddToRole(teacherId, "Teacher", AdminName, AdminPassword);

            // Imports courses and gets their ids.

            ImportCourse(CourseUri1, CourseName1, TeacherName, TeacherPassword);
            courseId1 = GetCourseId(CourseName1, TeacherName, TeacherPassword);

            ImportCourse(CourseUri2, CourseName2, TeacherName, TeacherPassword);
            courseId2 = GetCourseId(CourseName2, TeacherName, TeacherPassword);

            ImportCourse(CourseUri3, CourseName3, TeacherName, TeacherPassword);
            courseId3 = GetCourseId(CourseName3, TeacherName, TeacherPassword);

            ImportCourse(CourseUri4, CourseName4, TeacherName, TeacherPassword);
            courseId4 = GetCourseId(CourseName4, TeacherName, TeacherPassword);

            // Creates group and adds student to it. 

            CreateGroup(GroupName, TeacherName, TeacherPassword);
            AddToGroup(studentId, GroupName, AdminName, AdminPassword);
            groupId = GetGroupId(GroupName, TeacherName, TeacherPassword);
        }

        [TearDown]
        public void TearDown()
        {
            // Deletes all courses.

            DeleteCourse(courseId1, CourseName1, TeacherName, TeacherPassword);
            DeleteCourse(courseId2, CourseName2, TeacherName, TeacherPassword);
            DeleteCourse(courseId3, CourseName3, TeacherName, TeacherPassword);
            DeleteCourse(courseId4, CourseName4, TeacherName, TeacherPassword);

            // Removes all groups.

            DeleteGroup(groupId, GroupName, TeacherName, TeacherPassword);

            // Deletes all curriculums.

            DeleteCurriculum(curriculumId1, curriculumName1, TeacherName, TeacherPassword);
            DeleteCurriculum(curriculumId2, curriculumName2, TeacherName, TeacherPassword);
            DeleteCurriculum(curriculumId3, curriculumName3, TeacherName, TeacherPassword);
            DeleteCurriculum(curriculumId4, curriculumName4, TeacherName, TeacherPassword);

            // Removes all users.

            DeleteUser(studentId, StudentName, AdminName, AdminPassword);
            DeleteUser(teacherId, TeacherName, AdminName, AdminPassword);
        }

        [Test]
        public void PlayNotPreviouslyAttemptedTheme()
        {


            // Creates assigned to group curriculum that contains valid curriculum timelines.

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
            Assert.IsTrue(Selenium.GetTitle() == "Play course");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Select an activity"));
            Assert.IsTrue(Selenium.IsTextPresent("Please select an activity to continue with the training."));
            Selenium.SelectFrame("relative=top");
            Assert.IsTrue(Selenium.IsElementPresent("css=a[title='How to Play']"));
            Assert.IsTrue(Selenium.IsElementPresent("css=a[title='Keeping Score']"));

            Logout();
        }

        [Test]
        public void PlayThemeWithInvalidId()
        {
            Login(StudentName, StudentPassword);

            Selenium.Open("/Training/Play/-1");
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Error happened"));
            Assert.IsTrue(Selenium.IsTextPresent("Theme you had requested was not found!"));
            Assert.IsTrue(Selenium.IsElementPresent("css=a[href='/']"));

            Selenium.Open("/Training/Play/0");
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Error happened"));
            Assert.IsTrue(Selenium.IsTextPresent("Theme you had requested was not found!"));
            Assert.IsTrue(Selenium.IsElementPresent("css=a[href='/']"));

            Logout();
        }

        [Test]
        public void PlayThemeWithInvalidAvailabilityDueToGroupAssignment()
        {


            // Creates Not assigned to group curriculum.

            CreateCurriculum(curriculumName2, TeacherName, TeacherPassword);
            curriculumId2 = GetCurriculumId(curriculumName2, TeacherName, TeacherPassword);
            AddStage(curriculumId2, "testStage2", TeacherName, TeacherPassword);
            string stageId2 = GetStageId(curriculumId2, "testStage2", TeacherName, TeacherPassword);
            AddTheme(curriculumId2, stageId2, "testTheme2", CourseName2, TeacherName, TeacherPassword);
            themeId2 = GetThemeId(curriculumId2, stageId2, "testTheme2", TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);
            Selenium.Open("/Training/Play/" + themeId2);
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Error happened"));
            Assert.IsTrue(Selenium.IsTextPresent("You are not allowed to pass this theme!"));
            Assert.IsTrue(Selenium.IsElementPresent("css=a[href='/']"));
            Assert.IsFalse(Selenium.GetTitle() == "Play course");
            Logout();
        }

        [Test]
        public void PlayThemeWithInvalidAvailabilityDueToInvalidCurriculumTimelines()
        {


            // Creates assigned to group curriculum with invalid curriculum timelines.

            CreateCurriculum(curriculumName3, TeacherName, TeacherPassword);
            curriculumId3 = GetCurriculumId(curriculumName3, TeacherName, TeacherPassword);
            AddGroupToCurriculum(curriculumId3, GroupName, TeacherName, TeacherPassword);
            string curriculumAssignmentId3 = GetCurriculumAssignmentId(curriculumId3, GroupName, TeacherName, TeacherPassword);
            AddTimeLineToCurriculum("12/18/1999 4:23 PM", "12/18/2000 4:23 PM", curriculumId3, curriculumAssignmentId3, TeacherName, TeacherPassword);
            AddStage(curriculumId3, "testStage3", TeacherName, TeacherPassword);
            string stageId3 = GetStageId(curriculumId3, "testStage3", TeacherName, TeacherPassword);
            AddTheme(curriculumId3, stageId3, "testTheme3", CourseName3, TeacherName, TeacherPassword);
            themeId3 = GetThemeId(curriculumId3, stageId3, "testTheme3", TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);
            Selenium.Open("/Training/Play/" + themeId3);
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Error happened"));
            Assert.IsTrue(Selenium.IsTextPresent("You are not allowed to pass this theme!"));
            Assert.IsTrue(Selenium.IsElementPresent("css=a[href='/']"));
            Assert.IsFalse(Selenium.GetTitle() == "Play course");
            Logout();
        }

        [Test]
        public void PlayThemeWithInvalidAvailabilityDueToInvalidStageTimelines()
        {


            // Creates assigned to group curriculum with valid curriculum timelines, but invalid stage timelines.

            CreateCurriculum(curriculumName4, TeacherName, TeacherPassword);
            curriculumId4 = GetCurriculumId(curriculumName4, TeacherName, TeacherPassword);
            AddGroupToCurriculum(curriculumId4, GroupName, TeacherName, TeacherPassword);
            string curriculumAssignmentId4 = GetCurriculumAssignmentId(curriculumId4, GroupName, TeacherName, TeacherPassword);
            AddTimeLineToCurriculum("12/18/1999 4:23 PM", "12/18/2100 4:23 PM", curriculumId4, curriculumAssignmentId4, TeacherName, TeacherPassword);
            AddStage(curriculumId4, "testStage4", TeacherName, TeacherPassword);
            string stageId4 = GetStageId(curriculumId4, "testStage4", TeacherName, TeacherPassword);
            AddTimeLineToStage("12/18/1999 4:23 PM", "12/18/2000 4:23 PM", curriculumId4, curriculumAssignmentId4, TeacherName, TeacherPassword);
            AddTheme(curriculumId4, stageId4, "testTheme4", CourseName4, TeacherName, TeacherPassword);
            themeId4 = GetThemeId(curriculumId4, stageId4, "testTheme4", TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);
            Selenium.Open("/Training/Play/" + themeId4);
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Error happened"));
            Assert.IsTrue(Selenium.IsTextPresent("You are not allowed to pass this theme!"));
            Assert.IsTrue(Selenium.IsElementPresent("css=a[href='/']"));
            Assert.IsFalse(Selenium.GetTitle() == "Play course");
            Logout();
        }

        [Test]
        public void PlayCompletedTheme()
        {

            // Creates assigned to group curriculum that contains valid curriculum timelines.

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
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=a7");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("id=a7");

            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=aSUBMIT");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("id=aSUBMIT");
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Submit this Training?"));
            Selenium.Click("id=submitBtn");
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Training Completed"));
            Assert.IsTrue(Selenium.IsTextPresent("This training has been completed. You cannot make any further changes."));
            Selenium.Open("/");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Training/Play/" + themeId1);
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Results"));
            Logout();
        }

        [Test]
        public void PlaySuspendedTheme()
        {
            // Imports courses and gets their ids.

            ImportCourse(CourseUri1, CourseName1, TeacherName, TeacherPassword);
            courseId1 = GetCourseId(CourseName1, TeacherName, TeacherPassword);

            // Creates assigned to group curriculum that contains valid curriculum timelines.

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
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=a7");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("id=a7");
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));
            Selenium.Open("/");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Training/Play/" + themeId1);
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));
            Logout();
        }

        [Test]
        public void SuspendTheme()
        {

            // Creates assigned to group curriculum that contains valid curriculum timelines.

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
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=a7");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("id=a7");
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));
            Selenium.Open("/");
            Selenium.WaitForPageToLoad(LoadTime);
            Logout();
        }

        [Test]
        public void SubmitTheme()
        {

            // Creates assigned to group curriculum that contains valid curriculum timelines.

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
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=a7");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("id=a7");

            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=aSUBMIT");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("id=aSUBMIT");
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Submit this Training?"));
            Selenium.Click("id=submitBtn");
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Training Completed"));
            Assert.IsTrue(Selenium.IsTextPresent("This training has been completed. You cannot make any further changes."));

            Logout();
        }

        [Test]
        public void InteractWithCourseContentNextPrev()
        {

            CreateCurriculum(curriculumName3, TeacherName, TeacherPassword);
            curriculumId3 = GetCurriculumId(curriculumName3, TeacherName, TeacherPassword);
            AddGroupToCurriculum(curriculumId3, GroupName, TeacherName, TeacherPassword);
            string curriculumAssignmentId3 = GetCurriculumAssignmentId(curriculumId3, GroupName, TeacherName, TeacherPassword);
            AddTimeLineToCurriculum("12/18/1999 4:23 PM", "12/18/2100 4:23 PM", curriculumId3, curriculumAssignmentId3, TeacherName, TeacherPassword);
            AddStage(curriculumId3, "testStage3", TeacherName, TeacherPassword);
            string stageId3 = GetStageId(curriculumId3, "testStage3", TeacherName, TeacherPassword);
            AddTheme(curriculumId3, stageId3, "testTheme3", CourseName3, TeacherName, TeacherPassword);
            themeId3 = GetThemeId(curriculumId3, stageId3, "testTheme3", TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);
            Selenium.Open("/Training/Play/" + themeId3);
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.SelectFrame("id=frameContent");
            Selenium.Click("id=butNext");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Par"));
            Selenium.SelectFrame("id=frameContent");
            Selenium.Click("id=butPrev");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Play of the game"));
            Logout();
        }

        [Test]
        public void InteractWithCourseContentSubmitLastItem()
        {

            // Creates assigned to group curriculum that contains valid curriculum timelines.

            CreateCurriculum(curriculumName2, TeacherName, TeacherPassword);
            curriculumId2 = GetCurriculumId(curriculumName2, TeacherName, TeacherPassword);
            AddGroupToCurriculum(curriculumId2, GroupName, TeacherName, TeacherPassword);
            string curriculumAssignmentId2 = GetCurriculumAssignmentId(curriculumId2, GroupName, TeacherName, TeacherPassword);
            AddTimeLineToCurriculum("12/18/2010 4:23 PM", "12/18/2100 4:23 PM", curriculumId2, curriculumAssignmentId2, TeacherName, TeacherPassword);
            AddStage(curriculumId2, "testStage2", TeacherName, TeacherPassword);
            string stageId2 = GetStageId(curriculumId2, "testStage2", TeacherName, TeacherPassword);
            AddTheme(curriculumId2, stageId2, "testTheme2", CourseName2, TeacherName, TeacherPassword);
            themeId1 = GetThemeId(curriculumId2, stageId2, "testTheme2", TeacherName, TeacherPassword);

            Login(StudentName, StudentPassword);
            Selenium.Open("/Training/Play/" + themeId2);
            for (int i = 0; i < 15; i++)
            {
                Selenium.SelectFrame("id=frameContent");
                Selenium.Click("id=butNext");
                Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            }

            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=aSUBMIT");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("id=aSUBMIT");
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Submit this Training?"));
            Selenium.Click("id=submitBtn");
            Selenium.WaitForPageToLoad(LoadTime);
            Assert.IsTrue(Selenium.IsTextPresent("Training Completed"));
            Assert.IsTrue(Selenium.IsTextPresent("This training has been completed. You cannot make any further changes."));

            Logout();

        }

        [Test]
        public void NavigateForwardThroughTheme()
        {
            // Creates assigned to group curriculum that contains valid curriculum timelines.

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
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=a7");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("id=a7");
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));
            Logout();
        }

        [Test]
        public void NavigateForwardOneStep()
        {

            // Creates assigned to group curriculum that contains valid curriculum timelines.

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
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=a7");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("id=a7");
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));

            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=a6");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("id=a6");
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Other Scoring Systems"));

            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=a7");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("id=a7");
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("The Rules of Golf (book)"));

            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=a8");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("id=a8");
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Knowledge Check"));
            Logout();
        }

        [Test]
        public void NavigateChoiceToLastItemToFirst()
        {

            // Creates assigned to group curriculum that contains valid curriculum timelines.

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
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=a24");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("id=a24");
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Knowledge Check"));

            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("frameToc");
            Selenium.MouseDown("id=a3");
            Selenium.WaitForFrameToLoad("id=frameContent", LoadTime);
            Selenium.MouseUp("id=a3");
            Selenium.SelectFrame("relative=parent");
            Selenium.SelectFrame("id=frameContent");
            Assert.IsTrue(Selenium.IsTextPresent("Play of the game"));
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
            string courseId = Selenium.GetLocation().Substring(Selenium.GetLocation().IndexOf("Course/") + 7, Selenium.GetLocation().IndexOf("/Edit") - (Selenium.GetLocation().IndexOf("Course/") + 7));
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

        private void DeleteCurriculum(string curriculumId, string curriculumName, string teacherLogin, string teacherPassword)
        {
            Login(teacherLogin, teacherPassword);
            Selenium.Open("/Curriculum");
            Selenium.WaitForPageToLoad(LoadTime);
            if (Selenium.IsTextPresent(curriculumName))
            {
                Selenium.Click("css=a[onclick='deleteItem(" + curriculumId + ")']");
                selenium.GetConfirmation();
            }
            Logout();
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

        private void ChangeCourseInTheme(string courseName, string curriculumId, string stageId, string themeId, string teacherLogin, string teacherPassword)
        {
            Login(TeacherName, teacherPassword);
            Selenium.Open("/Curriculum");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Curriculum/" + curriculumId + "/Stage/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Stage/" + stageId + "/Theme/Index");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Open("/Theme/" + themeId + "/Edit");
            Selenium.WaitForPageToLoad(LoadTime);
            Selenium.Select("id=CourseId", courseName);
            Selenium.Click("css=input[value='Update']");
            Logout();
        }
    }
}
