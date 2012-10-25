using System;
using System.Threading;
using IUDICO.UnitTests.Base;
using NUnit.Framework;


namespace IUDICO.UnitTests.CurriculumManagement.Selenium
{
    [TestFixture]
    public class EditCurricilumChapterTopic: SimpleWebTest
    {
          private const int SleepTime = 8000;

        /// <summary>
        /// Author - Khrystyna Makar
        /// </summary>
        [Test]
        public void EditCurriculumChapterTopic()
        {
            this.DefaultLogin("prof", "prof");

            // add discipline
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

            // add chapter
            this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'Discipline1')]//a[contains(text(),'Add chapter')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "Chapter1");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            // add topic 
            this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'Discipline1')]/following::tr//a[contains(text(),'Add topic')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=TopicName", "Topic1");
            this.selenium.Select("id=TestCourseId", "value=-1");
            this.selenium.Select("id=TheoryCourseId", "value=-2");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            // create curriculum
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
            Assert.IsTrue(isPresent);
            // edit chapter timelines
            this.selenium.Click("//table[@id='curriculumsTable']//tr[contains(.,'Discipline1')]//a[contains(text(),'Edit chapter timelines')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//table//tr[contains(.,'Chapter1')]//a[contains(text(),'Edit Topic Assignment')]");
            
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Click("//table//tr[contains(.,'Topic1')]//a[contains(text(),'Edit')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            this.selenium.Type("xpath=(//input[@id='ThresholdOfSuccess'])", "51");
            this.selenium.Click("xpath=(//input[@id='BlockTopicAtTesting'])");
            this.selenium.Click("xpath=(//input[@id='BlockCurriculumAtTesting'])");
            this.selenium.Click("xpath=(//input[@value='Update'])");

            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            //check
            Assert.IsTrue(this.selenium.IsElementPresent("//table//tr[contains(.,'51')]"));
            Assert.IsTrue(this.selenium.IsElementPresent("//table//tr[contains(.,'True')]"));


            //remove discipline
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
