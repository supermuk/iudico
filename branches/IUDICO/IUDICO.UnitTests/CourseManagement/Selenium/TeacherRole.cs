using System;
using System.Threading;
using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.CourseManagement.Selenium
{
    [TestFixture]
    internal class TeacherRole : SimpleWebTest
    {
        private const int SleepTime = 8000;

        [Test]
        public void OpenCourses()
        {
            this.DefaultLogin("prof", "prof");
     
            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href,'/Course/Import')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [Test]
        public void CreateCourseTest()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "Test");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent = this.selenium.IsElementPresent("//table[@id='Courses']//tr//td[contains(.,'Test')]");
            Assert.IsTrue(isPresent);

            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [Test]
        public void CreateCourseWithoutCourseName()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", string.Empty);
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);
            Assert.IsTrue(this.selenium.IsElementPresent("//span[contains(.,'Name is required')]"));
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[2])");
            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [Test]
        public void EditCourse()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            
            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "forEdit");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            this.selenium.Click("xpath=//tr[contains(.,'forEdit')]//a[contains(@title,'Rename')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "Edited");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");

            this.selenium.Open("/");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent = this.selenium.IsElementPresent("xpath=//table[@id='Courses']//tr[contains(.,'Edited')]");
            Assert.IsTrue(isPresent);
            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [Test]
        public void EditCourseContentAddNodeAndFolder()
        {

            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "forEditContent");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            this.selenium.Click("xpath=//tr[contains(.,'forEdit')]//div[contains(text(),'forEditContent')]");
            this.selenium.WaitForPageToLoad((3 * this.seleniumWait).ToString());

            this.CreateNode();

            this.selenium.Click("//a[contains(text(),'Node')]");
            
            this.selenium.Click("//a[contains(@href, '/Course')]");
            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        /// <summary>
        /// Author - Lutsiv Oleg
        /// </summary>
        [Test]
        public void CreateNodeTest()
        {

            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "forEditContent");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            this.selenium.Click("xpath=//tr[contains(.,'forEdit')]//div[contains(text(),'forEditContent')]");
            this.selenium.WaitForPageToLoad((3 * this.seleniumWait).ToString());

            this.CreateNode();
            this.selenium.Click("//h1[contains(text(),'Editing Course')]"); //Editing Course "forEditContent"</h1>

            this.selenium.Click("//a[contains(text(),'Node')]");

            this.selenium.Click("//a[contains(@href, '/Course')]");
            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        /// <summary>
        /// Author - Lutsiv Oleg
        /// </summary>
        [Test]
        public void CreateBlankNodeTest()
        {

            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "forEditContent");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            this.selenium.Click("xpath=//tr[contains(.,'forEdit')]//div[contains(text(),'forEditContent')]");
            this.selenium.WaitForPageToLoad((3 * this.seleniumWait).ToString());

            this.selenium.ContextMenuAt("//a[contains(text(),'Root')]", "20, 10");
            this.selenium.MouseDown("//a[contains(text(),'Create Node')]");
            this.selenium.MouseUp("//a[contains(text(),'Create Node')]");
            this.selenium.TypeKeys("//input[@value='New node']", @"\13");

            this.selenium.Click("//a[contains(text(),'New node')]");
            
            this.selenium.Click("//a[contains(@href, '/Course')]");
            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        /// <summary>
        /// Author - Lutsiv Oleg
        /// </summary>
        [Test]
        public void RenameNodeTest()
        {

            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "forEditContent");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            this.selenium.Click("xpath=//tr[contains(.,'forEdit')]//div[contains(text(),'forEditContent')]");
            this.selenium.WaitForPageToLoad((3 * this.seleniumWait).ToString());

            this.CreateNode();
            Thread.Sleep(SleepTime);

            this.selenium.Click("//a[contains(text(),'Node')]");
            this.selenium.ContextMenuAt("//a[contains(text(),'Node')]", "20, 10");
            this.selenium.MouseDown("//a[contains(text(),'Rename')]");
            this.selenium.MouseUp("//a[contains(text(),'Rename')]");
            this.selenium.Type("//input[@value='Node']", "RenamedNode");
            this.selenium.TypeKeys("//input[@value='Node']", @"/13");

            //this.selenium.Click("//a[contains(text(),'RenamedNode')]");

            this.selenium.Click("//a[contains(@href, '/Course')]");
            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        /// <summary>
        /// Author - Lutsiv Oleg
        /// </summary>
        [Test]
        public void DeleteNodeTest()
        {

            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "forEditContent");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            this.selenium.Click("xpath=//tr[contains(.,'forEdit')]//div[contains(text(),'forEditContent')]");
            this.selenium.WaitForPageToLoad((3 * this.seleniumWait).ToString());

            this.CreateNode();

            this.selenium.Click("//a[contains(text(),'Node')]");
            this.selenium.ContextMenuAt("//a[contains(text(),'Node')]", "20, 10");
            this.selenium.MouseDown("//a[contains(text(),'Delete')]");
            this.selenium.MouseUp("//a[contains(text(),'Delete')]");
            this.selenium.GetConfirmation();
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            try
            {
                this.selenium.Click("//a[contains(text(),'Node')]");
                Assert.Fail("Node was not deleted");
            }
            catch(Exception ex)
            {
                if (ex.Message == "ERROR: Element //a[contains(text(),'Node')] not found")
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail(ex.Message);
                }
            }

            this.selenium.Click("//a[contains(@href, '/Course')]");
            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        /// <summary>
        /// Author - Lutsiv Oleg
        /// </summary>
        [Test]
        public void PreviewNodeTest()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "forEditContent");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            this.selenium.Click("xpath=//tr[contains(.,'forEdit')]//div[contains(text(),'forEditContent')]");
            this.selenium.WaitForPageToLoad((3 * this.seleniumWait).ToString());

            this.CreateNode();

            this.selenium.Click("//a[contains(text(),'Node')]");
            this.selenium.ContextMenuAt("//a[contains(text(),'Node')]", "20, 10");
            this.selenium.MouseDown("//a[contains(text(),'Preview')]");
            this.selenium.MouseUp("//a[contains(text(),'Preview')]");
            Thread.Sleep(SleepTime);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            //this.selenium.Click("//input[@id='ScoSubmit']");

            this.selenium.Click("//a[contains(@href, '/Course')]");
            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        private void CreateNode()
        {
            this.selenium.ContextMenuAt("//a[contains(text(),'Root')]", "20, 10");
            this.selenium.MouseDown("//a[contains(text(),'Create Node')]");
            this.selenium.MouseUp("//a[contains(text(),'Create Node')]");
            this.selenium.Type("//input[@value='New node']", "Node");
            this.selenium.TypeKeys("//input[@value='New node']", @"\13");
        }

        [Test]
        [Ignore]
        public void ShareCourseAndUnshareCourse()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "SharedForProf2");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            this.selenium.Click("xpath=//tr[contains(.,'SharedForProf2')]//a[contains(@title,'Share')]");
            Thread.Sleep(SleepTime);
            Assert.IsFalse(this.selenium.IsElementPresent("//table[@id='shareUserTable']//tr//td[contains(.,'Admin')]"));
            Assert.IsFalse(this.selenium.IsElementPresent("//table[@id='shareUserTable']//tr//td[contains(.,'Student')]"));

            this.selenium.Click("xpath=//tr[contains(.,'prof2')]//input[@name='sharewith']");
            this.selenium.Click("xpath=//button[@type='button']//span[contains(text(),'Share')]");
            Thread.Sleep(SleepTime);
            this.selenium.Click("//a[contains(@href, '/Account/Logout')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);



            this.DefaultLogin("prof2", "prof2");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            var isPresent = this.selenium.IsElementPresent("//table[@id='Courses']//tr//td[contains(.,'SharedForProf2')]");
            Assert.IsTrue(isPresent);
            this.selenium.Click("//a[contains(@href, '/Account/Logout')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("xpath=//tr[contains(.,'SharedForProf2')]//a[contains(@title,'Share')]");
            Thread.Sleep(SleepTime);

            this.selenium.Click("xpath=//tr[contains(.,'prof2')]//input[@name='sharewith']");
            this.selenium.Click("xpath=//button[@type='button']//span[contains(text(),'Share')]");
            Thread.Sleep(SleepTime);
            this.selenium.Click("//a[contains(@href, '/Account/Logout')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);



            this.DefaultLogin("prof2", "prof2");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            isPresent = this.selenium.IsElementPresent("//table[@id='Courses']//tr//td[contains(.,'SharedForProf2')]");
            Assert.IsFalse(isPresent);
            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [Test]
        public void LockAndUnlockCourse()
        {

            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "forLocking");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            this.selenium.Click("xpath=//tr[contains(.,'forLocking')]//a[contains(@title, 'Lock')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent =
                this.selenium.IsElementPresent("xpath=//tr[contains(.,'forLocking')]//a[contains(@title, 'Unlock')]");
            Assert.IsTrue(isPresent);
            this.selenium.Click("xpath=//tr[contains(.,'forLocking')]//a[contains(@title,'Unlock')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            isPresent =
               this.selenium.IsElementPresent("xpath=//tr[contains(.,'forLocking')]//a[contains(@title,'Unlock')]");
            Assert.IsFalse(isPresent);
            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [Test]
        public void ExportCourse()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "forExport");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Thread.Sleep(SleepTime);
            Assert.IsTrue(this.selenium.IsElementPresent("xpath=//tr[contains(.,'forExport')]"));

            this.selenium.Click("xpath=//tr[contains(.,'forExport')]//a[contains(@title,'Export')]");
            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }

        }

        [Test]
        public void DeleteCourse()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "forDeletion");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Thread.Sleep(SleepTime);
            Assert.IsTrue(this.selenium.IsElementPresent("xpath=//tr[contains(.,'forDeletion')]"));

            this.selenium.Click("xpath=//tr[contains(.,'forDeletion')]//a[contains(@title,'Delete')]");
            this.selenium.GetConfirmation();
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Assert.IsFalse(this.selenium.IsElementPresent("xpath=//tr[contains(.,'forDeletion')]"));
            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [Test]
        public void DeleteAllSelectedCourses()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "forDeletion1");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }


            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "forDeletion2");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "forDeletion3");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Thread.Sleep(SleepTime);
            Assert.IsTrue(this.selenium.IsElementPresent("xpath=//tr[contains(.,'forDeletion1')]"));
            Assert.IsTrue(this.selenium.IsElementPresent("xpath=//tr[contains(.,'forDeletion2')]"));
            Assert.IsTrue(this.selenium.IsElementPresent("xpath=//tr[contains(.,'forDeletion3')]"));

            this.selenium.Click("xpath=//tr[contains(.,'forDeletion1')]//input");
            this.selenium.Click("xpath=//tr[contains(.,'forDeletion2')]//input");
            this.selenium.Click("xpath=//tr[contains(.,'forDeletion3')]//input");
            this.selenium.Click("//a[contains(@id, 'DeleteMany')]");
            this.selenium.GetConfirmation();
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Thread.Sleep(SleepTime);
            Assert.IsFalse(this.selenium.IsElementPresent("xpath=//tr[contains(.,'forDeletion1')]"));
            Assert.IsFalse(this.selenium.IsElementPresent("xpath=//tr[contains(.,'forDeletion2')]"));
            Assert.IsFalse(this.selenium.IsElementPresent("xpath=//tr[contains(.,'forDeletion3')]"));

            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [Test]
        public void SelectAllAndDelete()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "forDeletion");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Thread.Sleep(SleepTime);
            Assert.IsTrue(this.selenium.IsElementPresent("xpath=//tr[contains(.,'forDeletion')]"));

            this.selenium.Click("xpath=//input[@id='CoursesCheckAll']");
            this.selenium.Click("//a[contains(@id, 'DeleteMany')]");
            this.selenium.GetConfirmation();
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Thread.Sleep(SleepTime);
            Assert.IsFalse(this.selenium.IsElementPresent("xpath=//tr[contains(.,'forDeletion')]"));
            Assert.IsTrue(this.selenium.IsElementPresent("xpath=//div[contains(.,'No courses available')]"));
            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }

        }

        [Test]
        public void UpdatedByTest()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DeleteAllCourses();

            this.selenium.Click("//a[contains(@onclick, 'addCourse();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "new course");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Assert.IsTrue(this.selenium.IsElementPresent("xpath=//tr[contains(.,'new course')]"));

            this.selenium.Click("xpath=//tr[contains(.,'new course')]//a[contains(@title,'Share')]");
            Thread.Sleep(SleepTime);
            this.selenium.Click("xpath=//tr[contains(.,'prof2')]//input[@name='sharewith']");
            this.selenium.Click("xpath=//button[@type='button']//span[contains(text(),'Share')]");
            Thread.Sleep(SleepTime);

            this.selenium.Click("//a[contains(@href, '/Account/Logout')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.DefaultLogin("prof2", "prof2");

            this.selenium.Click("//a[contains(@href,'/Course')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("xpath=//tr[contains(.,'new course')]//a[contains(@title,'Rename')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "new course after edit");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Assert.IsTrue(this.selenium.IsElementPresent("xpath=//tr[contains(.,'new course after edit')]//td[contains(.,'prof2')]"));

            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        protected void DeleteAllCourses()
        {
            if (this.selenium.IsElementPresent("xpath=//input[@id='CoursesCheckAll']"))
            {
                this.selenium.Click("xpath=//input[@id='CoursesCheckAll']");
                this.selenium.Click("//a[contains(@id, 'DeleteMany')]");
                this.selenium.GetConfirmation();
                this.selenium.Refresh();
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
                while (selenium.IsAlertPresent())
                {
                    selenium.GetAlert();
                }
            }
        }
    }
}