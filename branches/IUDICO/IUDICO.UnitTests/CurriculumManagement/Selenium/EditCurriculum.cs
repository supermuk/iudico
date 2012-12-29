using System;
using System.Threading;
using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.CurriculumManagement.Selenium
{
    [TestFixture]
    public class EditCurriculum : SimpleWebTest
    {
        private const int SleepTime = 8000;

        /// <summary>
        /// Author - Volodymyr Vinichuk
        /// Editing only Curriculum group 
        /// </summary>
        [Test]
        public void EditCurriculumTest()
        {
            //login
            this.DefaultLogin("prof", "prof");
            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            //create discipline
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

            //create curriculum
            this.selenium.Click("//a[contains(@href,'/Curriculum')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href,'/Curriculum/Create')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Select("id=DisciplineId", "label=Discipline1");
            this.selenium.Select("id=GroupId", "value=1");
            this.selenium.Click("xpath=(//input[@value='Create'])");

            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent = this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Демонстраційна група')]");
            Assert.IsTrue(isPresent);

            //editing curriculum
            this.selenium.Click("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]//a[contains(@href,'Edit')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Select("id=GroupId", "value=2");
            this.selenium.Click("xpath=(//input[@value='Update'])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent2 =
                this.selenium.IsElementPresent(
                    "//table[@id='curriculumsTable']//tr[contains(.,'Selenium testing system group')]");

            Assert.IsTrue(isPresent2);
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]//a[contains(@href,'Edit')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Select("id=GroupId", "value=1");

            this.selenium.Click("xpath=(//input[@value='Update'])");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent4 = this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Демонстраційна група')]");

            Assert.IsTrue(isPresent4);

            //deleting discipline
            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'Discipline1')]//a[contains(@onclick,'deleteDiscipline')]");
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
        /// Author - Volodymyr Vinichuk
        /// Editing only timeLine 
        /// </summary>
        [Test]
        public void EditCurriculumTestWithTimeLine()
        {
            //login
            this.DefaultLogin("prof", "prof");
            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            //create discipline
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

            //create curriculum
            this.selenium.Click("//a[contains(@href,'/Curriculum')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//a[contains(@href,'/Curriculum/Create')]");
            Thread.Sleep(SleepTime);
            this.selenium.Select("id=DisciplineId", "label=Discipline1");
            this.selenium.Select("id=GroupId", "value=1");
            //set default timeline
            this.selenium.Click("xpath=(//input[@name='SetTimeline'])");
            this.selenium.Click("xpath=(//input[@value='Create'])");

            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Thread.Sleep(SleepTime);
            //check
            var isPresent = (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Демонстраційна група')]"));
            Assert.IsTrue(isPresent);

            //editing
            this.selenium.Click("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]//a[contains(text(),'Edit')]");
            Thread.Sleep(SleepTime);
            this.selenium.Select("id=GroupId", "value=2");
            this.selenium.Click("xpath=(//input[@name='SetTimeline'])");
            this.selenium.Click("xpath=(//input[@value='Update'])");

            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Thread.Sleep(SleepTime);
            var isPresent2 = this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Selenium testing system group')]");
            Assert.IsTrue(isPresent2);

            //delete discipline
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
        /// Author - Volodymyr Vinichuk
        /// </summary>
        [Test]
        public void EditCurriculumTestWithEditStartDate()
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
            this.selenium.Type("id=StartDate", "10/29/2012 08:10 AM");
            this.selenium.Type("id=EndDate", "10/29/2013 08:10 AM");
            this.selenium.Click("xpath=(//input[@value='Create'])");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Thread.Sleep(SleepTime);
            var isPresent = (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Not specified')]"))
                && this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]");

            this.selenium.Click("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]//a[contains(text(),'Edit')]");
            Thread.Sleep(SleepTime);
            this.selenium.Select("id=GroupId", "value=2");
            this.selenium.Click("xpath=(//input[@name='SetTimeline'])");
            this.selenium.Type("id=StartDate", "10/29/2014 8:10 AM");
            var isExist = (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2014 8:10 AM')]"));
            Assert.IsFalse(isExist);
            this.selenium.Click("xpath=(//input[@name='SetTimeline'])");
            this.selenium.Type("id=StartDate", "10/29/2011 08:10 AM");
            isExist = (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2011 8:10 AM')]"));
            Assert.IsFalse(isExist);
            Thread.Sleep(SleepTime);
            this.selenium.Click("xpath=(//input[@value='Update'])");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Thread.Sleep(SleepTime);
            var isPresent3 = (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2011 8:10 AM')]"));
            Assert.IsTrue(isPresent3);
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent2 = this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Selenium testing system group')]")
               && this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]");
            Assert.IsTrue(isPresent2);

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
        /// Author - Volodymyr Vinichuk
        /// </summary>
        [Test]
        public void EditCurriculumTestWithEditEndDate()
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
            this.selenium.Type("id=StartDate", "10/29/2012 8:10 AM");
            this.selenium.Type("id=EndDate", "10/29/2013 8:10 AM");
            this.selenium.Click("xpath=(//input[@value='Create'])");
            //this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Thread.Sleep(SleepTime);
            var isPresent = (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2013 8:10 AM')]"));
            Assert.IsTrue(isPresent);

            Thread.Sleep(SleepTime);
            this.selenium.Click("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]//a[contains(text(),'Edit')]");
            this.selenium.Select("id=GroupId", "value=2");
            this.selenium.Click("xpath=(//input[@name='SetTimeline'])");
            this.selenium.Type("id=EndDate", "10/29/2008 8:10 AM");
            var isExist = (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2008 8:10 AM')]"));
            Assert.IsFalse(isExist);
            this.selenium.Click("xpath=(//input[@name='SetTimeline'])");
            this.selenium.Type("id=EndDate", "10/29/2014 8:10 AM");
            isExist = (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2014 8:10 AM')]"));
            Assert.IsFalse(isExist);
            this.selenium.Click("xpath=(//input[@value='Update'])");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Thread.Sleep(SleepTime);
            var isPresent3 = (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2014 8:10 AM')]"));
            Assert.IsTrue(isPresent3);
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent2 = this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Selenium testing system group')]")
               && this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]");
            Assert.IsTrue(isPresent2);

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
        /// Author - Volodymyr Vinichuk
        /// </summary>
        [Test]
        public void EditCurriculumTestWithEditStartAndEndDate()
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
            this.selenium.Type("id=StartDate", "10/29/2012 8:10 AM");
            this.selenium.Type("id=EndDate", "10/29/2013 8:10 AM");
            this.selenium.Click("xpath=(//input[@value='Create'])");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Thread.Sleep(SleepTime);
            var isPresent = (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2013 8:10 AM')]") &&
                this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2012 8:10 AM')]"));
            Assert.IsTrue(isPresent);

            this.selenium.Click("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]//a[contains(text(),'Edit')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Thread.Sleep(SleepTime);
            this.selenium.Select("id=GroupId", "value=2");
            this.selenium.Click("xpath=(//input[@name='SetTimeline'])");
            this.selenium.Type("id=EndDate", "10/29/2008 8:10 AM");
            this.selenium.Type("id=StartDate", "10/29/2014 8:10 AM");
            var isExist = (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2008 8:10 AM')]"));
            Assert.IsFalse(isExist);
            isExist = (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2014 8:10 AM')]"));
            Assert.IsFalse(isExist);
            this.selenium.Click("xpath=(//input[@name='SetTimeline'])");
            this.selenium.Type("id=EndDate", "10/29/2014 8:10 AM");
            this.selenium.Type("id=StartDate", "10/29/2008 8:10 AM");
            isExist =
                (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2014 8:10 AM')]")) &&
                (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2008 8:10 AM')]"));
            Assert.IsFalse(isExist);
            this.selenium.Click("xpath=(//input[@value='Update'])");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            Thread.Sleep(SleepTime);
            var isPresent3 = (this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2014 8:10 AM')]") &&
                this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'10/29/2008 8:10 AM')]"));
            Assert.IsTrue(isPresent3);
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent2 = this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Selenium testing system group')]")
               && this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]");
            Assert.IsTrue(isPresent2);

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
