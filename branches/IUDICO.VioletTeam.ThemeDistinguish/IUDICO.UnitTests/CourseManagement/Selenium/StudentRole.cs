using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IUDICO.UnitTests.Base;
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
            selenium = new DefaultSelenium("localhost", 4444, "*chrome", "http://127.0.0.1:1556/");
            selenium.Start();

            selenium.Open("/");
            selenium.Type("id=loginUsername", "prof");
            selenium.Type("id=loginPassword", "prof");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
        }

        [TearDown]
        public void Logout()
        {
            selenium.Click("link=Logout");
            selenium.WaitForPageToLoad("30000");

        }

        [Test]
        public void OpenCourses()
        {
            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Back to List");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Import");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Back to List");
            selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void CreateCourse()
        {
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Create New");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=Name", "Test");
            selenium.Click("css=input[type=\"submit\"]");
            selenium.WaitForPageToLoad("30000");

        }

        [Test]
        public void EditCourse()
        {
            selenium.Click("link=Edit Course");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=Name", "Edited");
            selenium.Click("css=input[type=\"submit\"]");
            selenium.WaitForPageToLoad("30000");

        }

        [Test]
        public void ShareCourse()
        {
            selenium.Click("link=Edit Course");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("css=p.AddAll");
            selenium.Click("css=input[type=\"submit\"]");
            selenium.WaitForPageToLoad("30000");

        }
        [Test]
        public void PublishCourse()
        {
            selenium.Click("link=Courses");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=#Publish");
            selenium.WaitForPageToLoad("30000");

        }
        [Test]
        public void UnlockCourse()
        {
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=#Publish");
            selenium.WaitForPageToLoad("30000");

        }
        [Test]
        public void ExportCourse()
        {
            //selenium.Click("link=Export");
            //selenium.WaitForPageToLoad("30000");

        }

        [Test]
        public void DeleteCourse()
        {
        }

    }
}