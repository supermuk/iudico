using System;
using System.Threading;
using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.CurriculumManagement.Selenium
{
    [TestFixture]
    public class CreateCurriculum : SimpleWebTest
    {
        private const int SleepTime = 8000;

        /// <summary>
        /// Author - Khrystyna Makar
        /// </summary>
        [Test]
        public void CreateCurriculumTest()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@onclick, 'addDiscipline();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "Discipline1");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            this.selenium.Click("//a[contains(@href,'/Curriculum')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href,'/Curriculum/Create')]");
            Thread.Sleep(SleepTime);
            this.selenium.Select("id=DisciplineId", "label=Discipline1");
            this.selenium.Select("id=GroupId", "value=1");
            this.selenium.Click("xpath=(//input[@value='Create'])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent = this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Демонстраційна група')]")
                && this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]");


            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'Discipline1')]//a[contains(text(),'Delete')]");
            this.selenium.GetConfirmation();
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Thread.Sleep(SleepTime);
            Assert.IsFalse(this.selenium.IsElementPresent("xpath=//tr[contains(.,'Discipline1')]"));

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
        /// Author - Khrystyna Makar
        /// </summary>
        [Test]
        public void CreateCurriculumTestWithDefaultTimeline()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@onclick, 'addDiscipline();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "Discipline1");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            this.selenium.Click("//a[contains(@href,'/Curriculum')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href,'/Curriculum/Create')]");
            Thread.Sleep(SleepTime);
            this.selenium.Select("id=DisciplineId", "label=Discipline1");
            this.selenium.Select("id=GroupId", "value=1");

            this.selenium.Click("xpath=(//input[@name='SetTimeline'])");
            this.selenium.Click("xpath=(//input[@value='Create'])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent = this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Демонстраційна група')]")
                && this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]");

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'Discipline1')]//a[contains(text(),'Delete')]");
            this.selenium.GetConfirmation();
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Thread.Sleep(SleepTime);
            Assert.IsFalse(this.selenium.IsElementPresent("xpath=//tr[contains(.,'Discipline1')]"));

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
        /// Author - Mariana Khljebyk
        /// </summary>
        [Test]
        public void CreateCurriculumTestWithEditStartDate()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@onclick, 'addDiscipline();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "Discipline1");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            this.selenium.Click("//a[contains(@href,'/Curriculum')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href,'/Curriculum/Create')]");
            Thread.Sleep(SleepTime);
            this.selenium.Select("id=DisciplineId", "label=Discipline1");
            this.selenium.Select("id=GroupId", "value=1");

            this.selenium.Click("xpath=(//input[@name='SetTimeline'])");
            this.selenium.Type("id=StartDate", "10/20/2012 9:52 AM");
            this.selenium.Click("xpath=(//input[@value='Create'])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent = this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Демонстраційна група')]")
                && this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]");

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'Discipline1')]//a[contains(text(),'Delete')]");
            this.selenium.GetConfirmation();
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Thread.Sleep(SleepTime);
            Assert.IsFalse(this.selenium.IsElementPresent("xpath=//tr[contains(.,'Discipline1')]"));

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
        /// Author - Mariana Khljebyk
        /// </summary>
        [Test]
        public void CreateCurriculumTestWithEditEndDate()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@onclick, 'addDiscipline();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "Discipline1");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            this.selenium.Click("//a[contains(@href,'/Curriculum')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href,'/Curriculum/Create')]");
            Thread.Sleep(SleepTime);
            this.selenium.Select("id=DisciplineId", "label=Discipline1");
            this.selenium.Select("id=GroupId", "value=1");

            this.selenium.Click("xpath=(//input[@name='SetTimeline'])");
            this.selenium.Type("id=EndDate", "10/27/2012 9:52 AM");
            this.selenium.Click("xpath=(//input[@value='Create'])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent = this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Демонстраційна група')]")
                && this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]");

            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'Discipline1')]//a[contains(text(),'Delete')]");

            this.selenium.GetConfirmation();
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Thread.Sleep(SleepTime);
            Assert.IsFalse(this.selenium.IsElementPresent("xpath=//tr[contains(.,'Discipline1')]"));

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
