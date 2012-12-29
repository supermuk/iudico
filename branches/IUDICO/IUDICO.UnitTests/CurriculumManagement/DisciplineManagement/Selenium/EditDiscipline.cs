using System;
using System.Threading;
using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.CurriculumManagement.Selenium
{
    [TestFixture]
    public class EditDiscipline : SimpleWebTest
    {
        private const int SleepTime = 8000;

        /// <summary>
        /// Author - Kachmar Oleg
        /// </summary>
        [Test]
        public void EditDisciplineRenameTest()
        {
            // logging in
            this.DefaultLogin("prof", "prof");

            // moving to disciplines
            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // checks if edited discipline present. if yes, deletes it.
            var isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'EditedDiscipline')]");
            while (isPresent)
            {
                this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'EditedDiscipline')]//a[contains(text(),'Delete')]");
                this.selenium.GetConfirmation();
                this.selenium.Refresh();
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
                while (selenium.IsAlertPresent())
                {
                    selenium.GetAlert();
                }
                Thread.Sleep(SleepTime);

                isPresent =
                    this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'EditedDiscipline')]");
            }
            // checks if successfully deleted
            Assert.IsFalse(isPresent);
            
            //checks if present discipline to edit, if yes, deletes it.
            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'DisciplineForEdit')]");
            while (isPresent)
            {
                this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'DisciplineForEdit')]//a[contains(text(),'Delete')]");
                this.selenium.GetConfirmation();
                this.selenium.Refresh();
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
                while (selenium.IsAlertPresent())
                {
                    selenium.GetAlert();
                }
                Thread.Sleep(SleepTime);

                isPresent =
                    this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'DisciplineForEdit')]");
            }
            // checks if deleted successfully
            Assert.IsFalse(isPresent);

            // adds discipline for edit
            this.selenium.Click("//a[contains(@onclick, 'addDiscipline();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "DisciplineForEdit");
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
                this.selenium.IsElementPresent(
                    "//table[@id='disciplines']//tr//td[contains(.,'DisciplineForEdit')]");
            Assert.IsTrue(isPresent);

            // edits discipline for edit
            this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'DisciplineForEdit')]//a[contains(text(),'Edit')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "EditedDiscipline");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);
            
            // checks if successfully edited
            isPresent =
               this.selenium.IsElementPresent(
                    "//table[@id='disciplines']//tr//td[contains(.,'DisciplineForEdit')]");
            Assert.IsFalse(isPresent);

            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'EditedDiscipline')]");
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
        /// Author - Chopenko Vitaliy
        /// </summary>
        [Test]
        public void EditDisciplineWithChapterAndTopicTest()
        {
            // logging in
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']");
            Assert.IsTrue(isPresent);

            // checks if present discipline for edit, if yes, deletes it.
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

            // checks if edited discipline present. if yes deletes it.
            isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'MyNewRenamedDiscipline')]");
            if (isPresent)
            {
                this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'MyNewRenamedDiscipline')]//a[contains(text(),'Delete')]");
                this.selenium.GetConfirmation();
                this.selenium.Refresh();
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
                while (selenium.IsAlertPresent())
                {
                    selenium.GetAlert();
                }
                Thread.Sleep(SleepTime);
            }

            // adds discipline for edit
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

            // edits discipline for edit
            this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'MyNewDiscipline')]//a[contains(text(),'Edit')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "MyNewRenamedDiscipline");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Thread.Sleep(SleepTime);

            // checks if edited successfully
            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'MyNewRenamedDiscipline')]");
            Assert.IsTrue(isPresent);

            // adds chapter for edit
            this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'MyNewRenamedDiscipline')]//a[contains(text(),'Add chapter')]");
            Thread.Sleep(SleepTime);

            this.selenium.Type("id=Name", "MyNewChapter");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);

            // checks if added successfully
            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'MyNewChapter')]");
            Assert.IsTrue(isPresent);

            // edits chapter for edit
            this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'MyNewChapter')]//a[contains(text(),'Edit')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "MyNewRenamedChapter");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);

            // checks if edited successfully
            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'MyNewRenamedChapter')]");
            Assert.IsTrue(isPresent);

            // adds topic for edit
            this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'MyNewRenamedChapter')]//a[contains(text(),'Add topic')]");
            Thread.Sleep(SleepTime);

            this.selenium.Type("id=TopicName", "MyNewTopic");
            this.selenium.Select("id=TestCourseId", "Test without course");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);

            // checks if added succesfully
            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'MyNewTopic')]");
            Assert.IsTrue(isPresent);

            // edits topic for edit
            this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'MyNewTopic')]//a[contains(text(),'Edit')]");
            Thread.Sleep(SleepTime);

            this.selenium.Type("id=TopicName", "MyNewRenamedTopic");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);

            // checks if edited successfully
            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'MyNewRenamedTopic')]");
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
