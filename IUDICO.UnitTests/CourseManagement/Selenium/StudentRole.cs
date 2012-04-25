using System;
using System.Configuration;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.CourseManagement.Selenium
{
    [TestFixture]
    internal class StudentRole
    {
        private ISelenium selenium;

        [SetUp]
        public void Login()
        {
            this.selenium = new DefaultSelenium("localhost", 4444, "*chrome", ConfigurationManager.AppSettings["this.selenium_URL"]);
            this.selenium.Start();

            this.selenium.Open("/");
            this.selenium.Type("id=loginUsername", "prof");
            this.selenium.Type("id=loginPassword", "prof");
            this.selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Courses");
            this.selenium.WaitForPageToLoad("40000");
        }

        [TearDown]
        public void Logout()
        {
            try
            {
                this.selenium.Click("link=Logout");
                this.selenium.WaitForPageToLoad("40000");

                this.selenium.Stop();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [Test]
        public void OpenCourses()
        {
            this.selenium.Click("link=Create New");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Back to List");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Import");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Back to List");
            this.selenium.WaitForPageToLoad("40000");
        }

        [Test]
        public void CreateCourseTest()
        {
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Create New");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Type("id=Name", "Test");
            this.selenium.Click("css=input[value=\"Create\"]");
            this.selenium.WaitForPageToLoad("40000");

            bool isPresent = this.selenium.IsElementPresent("//table[@id='myCourses']//tr//td[contains(.,'Test')]");
            Assert.IsTrue(isPresent);
        }

        [Test]
        public void CreateCourseWithoutCourseName()
        {
            this.selenium.Click("link=Create New");
            this.selenium.WaitForPageToLoad("40000");
            string pos = this.selenium.GetLocation();
            this.selenium.Type("id=Name", string.Empty);
            this.selenium.Click("css=input[value=\"Create\"]");
            Assert.AreEqual(pos, this.selenium.GetLocation());
        }

        [Test]
        public void EditCourse()
        {
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Create New");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Type("id=Name", "forEdit");
            this.selenium.Click("css=input[value=\"Create\"]");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("xpath=//tr[contains(.,'forEdit')]//a[text()='Edit Course']");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Type("id=Name", "Edited");
            this.selenium.Click("css=input[value=\"Save\"]");
            this.selenium.WaitForPageToLoad("40000");

            this.selenium.Open("/");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Courses");
            this.selenium.WaitForPageToLoad("40000");
            bool isPresent = this.selenium.IsElementPresent("xpath=//table[@id='myCourses']//tr[contains(.,'Edited')]");
            Assert.IsTrue(isPresent);
        }

        [Test]
        public void EditPublishCourse()
        {
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Create New");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Type("id=Name", "forPublish");
            this.selenium.Click("css=input[value=\"Create\"]");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("xpath=//tr[contains(.,'forPublish')]//a[text()='#Publish']");
            this.selenium.WaitForPageToLoad("40000");

            this.selenium.Click("xpath=//table[@id='publishedCourses']//tr[contains(.,'forPublish')]//a[text()='Edit']");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Type("id=Name", "Edited");
            this.selenium.Click("css=input[value=\"Save\"]");
            this.selenium.WaitForPageToLoad("40000");

            this.selenium.Open("/");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Courses");
            this.selenium.WaitForPageToLoad("40000");
            bool isPresent =
                this.selenium.IsElementPresent("xpath=//table[@id='publishedCourses']//tr[contains(.,'Edited')]");
            Assert.IsTrue(isPresent);
        }

        [Test]
        public void EditCourseContent()
        {
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Create New");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Type("id=Name", "forEditContent");
            this.selenium.Click("css=input[value=\"Create\"]");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("xpath=//tr[contains(.,'forEdit')]//a[text()='Edit content course']");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Back to List");
            this.selenium.WaitForPageToLoad("40000");
            bool isPresent = this.selenium.IsElementPresent("//tr//td[contains(.,'forEditContent')]");
            Assert.IsTrue(isPresent);
        }

        [Test]
        [Ignore]
        public void ShareOnCreateCourse()
        {
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Create New");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Type("id=Name", "Test");
            this.selenium.WaitForPageToLoad("40000");

            this.selenium.DoubleClick("//div/select/option");

            this.selenium.Click("css=input[value=\"Create\"]");
            this.selenium.WaitForPageToLoad("40000");

            bool isPresent = this.selenium.IsElementPresent("//table[@id='myCourses']//tr//td[contains(.,'Test')]");
            Assert.IsTrue(isPresent);
        }

        [Test]
        public void PublishAndUnlockCourse()
        {
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Create New");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Type("id=Name", "forPublish");
            this.selenium.Click("css=input[value=\"Create\"]");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("xpath=//tr[contains(.,'forPublish')]//a[text()='#Publish']");
            this.selenium.WaitForPageToLoad("40000");

            bool isPresent = this.selenium.IsElementPresent("//table[@id='publishedCourses']//tr[contains(.,'forPublish')]");
            Assert.IsTrue(isPresent);

            this.selenium.Click("link=Unlock");
            isPresent = this.selenium.IsElementPresent("//table[@id='publishedCourses']//tr[contains(.,'forPublish')]");
            Assert.IsTrue(isPresent);
        }

        [Test]
        public void ExportCourse()
        {
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Create New");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Type("id=Name", "forExport");
            this.selenium.Click("css=input[value=\"Create\"]");
            this.selenium.WaitForPageToLoad("40000");

            this.selenium.Click("xpath=//tr[contains(.,'forExport')]//a[text()='Export']");
        }

        [Test]
        public void DeleteCourse()
        {
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Create New");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Type("id=Name", "forDeletion");
            this.selenium.Click("css=input[value=\"Create\"]");
            this.selenium.WaitForPageToLoad("40000");

            this.selenium.Click("xpath=//tr[contains(.,'forDeletion')]//a[text()='Delete']");

            this.selenium.GetConfirmation();
            this.selenium.WaitForPageToLoad("40000");
        }

        [Test]
        public void DeletePublishCourse()
        {
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Create New");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Type("id=Name", "forPublish");
            this.selenium.Click("css=input[value=\"Create\"]");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("xpath=//tr[contains(.,'forPublish')]//a[text()='#Publish']");
            this.selenium.WaitForPageToLoad("40000");


            this.selenium.Click("//table[@id='publishedCourses']//tr[contains(.,'forPublish')]//a[text()='Delete']");
            this.selenium.GetConfirmation();

            bool isPresent = this.selenium.IsElementPresent("//tr[contains(.,'forPublish')]");
            Assert.IsTrue(isPresent);
        }

        [Test]
        public void DeleteAllSelectedCoursesCourse()
        {
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Create New");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Type("id=Name", "forDeletion1");
            this.selenium.Click("css=input[value=\"Create\"]");
            this.selenium.WaitForPageToLoad("40000");

            this.selenium.Click("link=Create New");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Type("id=Name", "forDeletion2");
            this.selenium.Click("css=input[value=\"Create\"]");
            this.selenium.WaitForPageToLoad("40000");

            this.selenium.Click("link=Create New");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Type("id=Name", "forDeletion3");
            this.selenium.Click("css=input[value=\"Create\"]");
            this.selenium.WaitForPageToLoad("40000");

            this.selenium.Click("xpath=//tr[contains(.,'forDeletion1')]//input");
            this.selenium.Click("xpath=//tr[contains(.,'forDeletion2')]//input");
            this.selenium.Click("xpath=//tr[contains(.,'forDeletion3')]//input");
            this.selenium.Click("link=Delete Selected");

            this.selenium.GetConfirmation();
        }


        [Test]
        public void Validate()
        {
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("link=Import");
            this.selenium.WaitForPageToLoad("40000");
        }


        [Test]
        public void Import()
        {
        }
    }
}