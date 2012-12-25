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

        [Test]
        public void EditDisciplineRenameTest()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

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
            Assert.IsFalse(isPresent);
            
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
            Assert.IsFalse(isPresent);

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

            isPresent =
                this.selenium.IsElementPresent(
                    "//table[@id='disciplines']//tr//td[contains(.,'DisciplineForEdit')]");
            Assert.IsTrue(isPresent);

            this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'DisciplineForEdit')]//a[contains(text(),'Edit')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "EditedDiscipline");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);
            
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
        [Test]
        public void EditDisciplineWithChapterAndTopicTest()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']");
            Assert.IsTrue(isPresent);

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

            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'MyNewDiscipline')]");
            Assert.IsTrue(isPresent);

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

            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'MyNewRenamedDiscipline')]");
            Assert.IsTrue(isPresent);

            this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'MyNewRenamedDiscipline')]//a[contains(text(),'Add chapter')]");
            Thread.Sleep(SleepTime);

            this.selenium.Type("id=Name", "MyNewChapter");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);

            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'MyNewChapter')]");
            Assert.IsTrue(isPresent);

            this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'MyNewChapter')]//a[contains(text(),'Edit')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "MyNewRenamedChapter");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);

            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'MyNewRenamedChapter')]");
            Assert.IsTrue(isPresent);

            this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'MyNewRenamedChapter')]//a[contains(text(),'Add topic')]");
            Thread.Sleep(SleepTime);

            this.selenium.Type("id=TopicName", "MyNewTopic");
            this.selenium.Select("id=TestCourseId", "Test without course");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);

            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'MyNewTopic')]");
            Assert.IsTrue(isPresent);

            this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'MyNewTopic')]//a[contains(text(),'Edit')]");
            Thread.Sleep(SleepTime);

            this.selenium.Type("id=TopicName", "MyNewRenamedTopic");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);

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
