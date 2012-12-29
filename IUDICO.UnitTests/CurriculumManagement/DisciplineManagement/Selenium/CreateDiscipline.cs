using System;
using System.Threading;
using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.CurriculumManagement.Selenium
{
    [TestFixture]
    public class CreateDiscipline : SimpleWebTest
    {
        private const int SleepTime = 8000;

        /// <summary>
        /// Author - Kachmar Oleg
        /// </summary>
        [Test]
        public void OpenDisciplineTest()
        {
            // logging in
            this.DefaultLogin("prof", "prof");

            // moving to Disciplines
            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // checks if moved to Disciplines
            var isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']");
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

        /// <summary>
        /// Author - Kachmar Oleg
        /// </summary>
        [Test]
        public void CreateDisciplineTest()
        {
            // logging in
            this.DefaultLogin("prof", "prof");

            // moving to disciplines
            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // checks if moved to Disciplines
            var isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']");
            Assert.IsTrue(isPresent);

            // checks if test Discipline are present. If present delete it
            isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'MyNewDiscipline')]");
            if (isPresent)
            {
                this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'MyNewDiscipline')]//a[contains(text(),'Delete')]");
                this.selenium.GetConfirmation();
                this.selenium.Refresh();
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
                while (selenium.IsAlertPresent())
                {
                    selenium.GetAlert();
                }
                Thread.Sleep(SleepTime);    
            }

            // adding discipline
            this.selenium.Click("//a[contains(@onclick, 'addDiscipline();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "MyNewDiscipline");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Thread.Sleep(SleepTime); 

            // checks if successfully added
            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'MyNewDiscipline')]");
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

        /// <summary>
        /// Author - Kachmar Oleg
        /// </summary>
        [Test]
        public void CreateDisciplineWithoutNameTest()
        {
            // logging in
            this.DefaultLogin("prof", "prof");

            // moving to disciplines
            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            
            // checks if moved to disciplines
            var isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']");
            Assert.IsTrue(isPresent);

            // checks if table is empty
            var isEmptyTable =
                this.selenium.IsElementPresent(
                    "//table[@id='disciplines']//tr//td[contains(.,'No data available in table')]");

            // adds discipline with empty name
            this.selenium.Click("//a[contains(@onclick, 'addDiscipline();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", string.Empty);
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);

            // checks if expected error occurred
            isPresent =
                this.selenium.IsElementPresent("//span[contains(.,'Correct the following error(s) and try again:')]");
            Assert.IsTrue(isPresent);

            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[2])");
            Thread.Sleep(SleepTime);
            isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']");
            Assert.IsTrue(isPresent);

            // checks if table still empty
            if (isEmptyTable)
            {
                isPresent =
                    this.selenium.IsElementPresent(
                        "//table[@id='disciplines']//tr//td[contains(.,'No data available in table')]");
                Assert.IsTrue(isPresent);
            }

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
        /// Author - Chopenko Vitaliy
        /// </summary>
        [Test]
        public void CreateDisciplineWithChapterAndTopicTest()
        {
            // logging in
            this.DefaultLogin("prof", "prof");

            // moving to disciplines
            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // checks if moved successfully
            var isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']");
            Assert.IsTrue(isPresent);

            // checks if test discipline is present. If present, deletes it.
            isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'MyNewDiscipline')]");
            if (isPresent)
            {
                this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'MyNewDiscipline')]//a[contains(text(),'Delete')]");
                this.selenium.GetConfirmation();
                this.selenium.Refresh();
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
                while (selenium.IsAlertPresent())
                {
                    selenium.GetAlert();
                }
                Thread.Sleep(SleepTime);
            }

            // adds discipline
            this.selenium.Click("//a[contains(@onclick, 'addDiscipline();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "MyNewDiscipline");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Thread.Sleep(SleepTime);

            // checks if added successfully
            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'MyNewDiscipline')]");
            Assert.IsTrue(isPresent);

            // adds chapter to added discipline
            this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'MyNewDiscipline')]//a[contains(text(),'Add chapter')]");
            Thread.Sleep(SleepTime);

            this.selenium.Type("id=Name", "MyNewChapter");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);

            // checks if added successfully
            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'MyNewChapter')]");
            Assert.IsTrue(isPresent);

            // adds topiv to created chapter
            this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'MyNewChapter')]//a[contains(text(),'Add topic')]");
            Thread.Sleep(SleepTime);

            this.selenium.Type("id=TopicName", "MyNewTopic");
            this.selenium.Select("id=TestCourseId", "Test without course");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);

            // checks if added successfully
            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'MyNewTopic')]");
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
    }
}
