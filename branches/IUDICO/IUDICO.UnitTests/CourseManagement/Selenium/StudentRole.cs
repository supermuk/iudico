using System;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.CourseManagement.Selenium
{
    [TestFixture]
    class StudentRole
    {
        private ISelenium selenium;

        [SetUp]
        public void Login()
        {
            selenium = new DefaultSelenium("localhost", 4444, "*chrome", "http://127.0.0.1:1569/");
            selenium.Start();

            selenium.Open("/");
            selenium.Type("id=loginUsername", "prof");
            selenium.Type("id=loginPassword", "prof");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("40000");

        }

        [TearDown]
        public void Logout()
        {
            try
            {
                selenium.Click("link=Logout");
                selenium.WaitForPageToLoad("40000");

                selenium.Stop();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [Test]
        public void OpenCourses()
        {
            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Back to List");
            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Import");
            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Back to List");
            selenium.WaitForPageToLoad("40000");
        }

        [Test]
        public void CreateCourseTest()
        {
            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("40000");
            selenium.Type("id=Name", "Test");
            selenium.Click("css=input[value=\"Create\"]");
            selenium.WaitForPageToLoad("40000");

            bool isPresent = selenium.IsElementPresent("//table[@id='myCourses']//tr//td[contains(.,'Test')]");
            Assert.IsTrue(isPresent);
        }

        [Test]
        public void CreateCourseWithoutCourseName()
        {
            string pos = selenium.GetLocation();
            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("40000");
            selenium.Click("css=input[value=\"Create\"]");
            Assert.AreEqual(pos,selenium.GetLocation());
        }

        [Test]
        public void EditCourse()
        {

            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("40000");
            selenium.Type("id=Name", "forEdit");
            selenium.Click("css=input[value=\"Create\"]");
            selenium.WaitForPageToLoad("40000");
            selenium.Click("xpath=//tr[contains(.,'forEdit')]//a[text()='Edit Course']");
            selenium.WaitForPageToLoad("40000");
            selenium.Type("id=Name", "Edited");
            selenium.Click("css=input[value=\"Save\"]");
            selenium.WaitForPageToLoad("40000");
            bool isPresent = selenium.IsElementPresent("//tr//td[contains(.,'Edited')]");
            Assert.IsTrue(isPresent);
        }

        [Test]
        public void EditPublishCourse()
        {
            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("40000");
            selenium.Type("id=Name", "forPublish");
            selenium.Click("css=input[value=\"Create\"]");
            selenium.WaitForPageToLoad("40000");
            selenium.Click("xpath=//tr[contains(.,'forPublish')]//a[text()='#Publish']");
            selenium.WaitForPageToLoad("40000");

            selenium.Click("xpath=//table[@id='publishedCourses']//tr[contains(.,'forPublish')]//a[text()='Edit']");
            selenium.WaitForPageToLoad("40000");
            selenium.Type("id=Name", "Edited");
            selenium.Click("css=input[value=\"Save\"]");
            selenium.WaitForPageToLoad("40000");
            bool isPresent = selenium.IsElementPresent("//table[@id='publishedCourses']//tr//td[contains(.,'Edited')]");
            Assert.IsTrue(isPresent);

        }

        [Test]
        public void EditCourseContent()
        {

            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("40000");
            selenium.Type("id=Name", "forEditContent");
            selenium.Click("css=input[value=\"Create\"]");
            selenium.WaitForPageToLoad("40000");
            selenium.Click("xpath=//tr[contains(.,'forEdit')]//a[text()='Edit content course']");
            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Back to List");
            selenium.WaitForPageToLoad("40000");
            bool isPresent = selenium.IsElementPresent("//tr//td[contains(.,'forEditContent')]");
            Assert.IsTrue(isPresent);
        }

        [Test]
		  [Ignore]
        public void ShareOnCreateCourse()
        {

            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("40000");
            selenium.Type("id=Name", "Test");
            selenium.WaitForPageToLoad("40000");

  //          selenium.Click("//p[2]");

			  selenium.DoubleClick("//div/select/option");

            selenium.Click("css=input[value=\"Create\"]");
            selenium.WaitForPageToLoad("40000");

            bool isPresent = selenium.IsElementPresent("//table[@id='myCourses']//tr//td[contains(.,'Test')]");
            Assert.IsTrue(isPresent);

        }
     
        [Test]
        public void PublishAndUnlockCourse()
        {
            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("40000");
            selenium.Type("id=Name", "forPublish");
            selenium.Click("css=input[value=\"Create\"]");
            selenium.WaitForPageToLoad("40000");
            selenium.Click("xpath=//tr[contains(.,'forPublish')]//a[text()='#Publish']");
            selenium.WaitForPageToLoad("40000");

            bool isPresent = selenium.IsElementPresent("//table[@id='publishedCourses']//tr[contains(.,'forPublish')]");
            Assert.IsTrue(isPresent);
            
            selenium.Click("link=Unlock");
            isPresent = selenium.IsElementPresent("//table[@id='publishedCourses']//tr[contains(.,'forPublish')]");
            Assert.IsTrue(isPresent);
        }

        [Test]
        public void ExportCourse()
        {
            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("40000");
            selenium.Type("id=Name", "forExport");
            selenium.Click("css=input[value=\"Create\"]");
            selenium.WaitForPageToLoad("40000");

            selenium.Click("xpath=//tr[contains(.,'forExport')]//a[text()='Export']");
        }

        [Test]
        public void DeleteCourse()
        {
            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("40000");
            selenium.Type("id=Name", "forDeletion");
            selenium.Click("css=input[value=\"Create\"]");
            selenium.WaitForPageToLoad("40000");

            selenium.Click("xpath=//tr[contains(.,'forDeletion')]//a[text()='Delete']");

            selenium.GetConfirmation();
            selenium.WaitForPageToLoad("40000");
        }

        [Test]
        public void DeletePublishCourse()
        {
            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("40000");
            selenium.Type("id=Name", "forPublish");
            selenium.Click("css=input[value=\"Create\"]");
            selenium.WaitForPageToLoad("40000");
            selenium.Click("xpath=//tr[contains(.,'forPublish')]//a[text()='#Publish']");
            selenium.WaitForPageToLoad("40000");


            selenium.Click("//table[@id='publishedCourses']//tr[contains(.,'forPublish')]//a[text()='Delete']");
            selenium.GetConfirmation();

            bool isPresent = selenium.IsElementPresent("//tr[contains(.,'forPublish')]");
            Assert.IsTrue(isPresent);
        }

        [Test]
        public void DeleteAllSelectedCoursesCourse()
        {
            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("40000");
            selenium.Type("id=Name", "forDeletion1");
            selenium.Click("css=input[value=\"Create\"]");
            selenium.WaitForPageToLoad("40000");

            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("40000");
            selenium.Type("id=Name", "forDeletion2");
            selenium.Click("css=input[value=\"Create\"]");
            selenium.WaitForPageToLoad("40000");

            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("40000");
            selenium.Type("id=Name", "forDeletion3");
            selenium.Click("css=input[value=\"Create\"]");
            selenium.WaitForPageToLoad("40000");

            selenium.Click("xpath=//tr[contains(.,'forDeletion1')]//input");
            selenium.Click("xpath=//tr[contains(.,'forDeletion2')]//input");
            selenium.Click("xpath=//tr[contains(.,'forDeletion3')]//input");
            selenium.Click("link=Delete Selected");

            selenium.GetConfirmation();
        }


        [Test]
        public void Validate()
        {
            selenium.WaitForPageToLoad("40000");
            selenium.Click("link=Import");
            selenium.WaitForPageToLoad("40000");

        }


        [Test]
        public void Import()
        {
            
        }
    }
}