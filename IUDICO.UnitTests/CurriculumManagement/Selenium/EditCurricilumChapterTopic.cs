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
          /// Author - Volodymyr Vinichuk
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
              this.selenium.Type("id=Name", "EditChapterTopicDiscipline");
              this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
              this.selenium.Refresh();
              this.selenium.WaitForPageToLoad(this.SeleniumWait);
              while (selenium.IsAlertPresent())
              {
                  selenium.GetAlert();
              }

              // add chapter
              this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'EditChapterTopicDiscipline')]//a[contains(text(),'Add chapter')]");
              Thread.Sleep(SleepTime);
              this.selenium.Type("id=Name", "Chapter1");
              this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
              Thread.Sleep(SleepTime);
              // add topic
              this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'Chapter1')]//a[contains(@onclick,'addTopic')]");
              Thread.Sleep(SleepTime);
              this.selenium.Type("id=TopicName", "Topic1");
              this.selenium.Select("id=TestCourseId", "value=-1");
              this.selenium.Select("id=TheoryCourseId", "value=-2");
              this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
              while (selenium.IsAlertPresent())
              {
                  selenium.GetAlert();
              }
              // create curriculum
              this.selenium.Click("//a[contains(@href,'/Curriculum')]");

              this.selenium.Click("//a[contains(@href,'/Curriculum/Create')]");
              this.selenium.WaitForPageToLoad(this.SeleniumWait);
              this.selenium.Select("id=DisciplineId", "label=EditChapterTopicDiscipline");
              this.selenium.Select("id=GroupId", "value=1");
              this.selenium.Click("xpath=(//input[@value='Create'])");
              this.selenium.WaitForPageToLoad(this.SeleniumWait);
              var isPresent = this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'Демонстраційна група')]")
                  && this.selenium.IsElementPresent("//table[@id='curriculumsTable']//tr[contains(.,'EditChapterTopicDiscipline')]");
              Assert.IsTrue(isPresent);

              // edit chapter timelines
              this.selenium.Click("//table[@id='curriculumsTable']//tr[contains(.,'EditChapterTopicDiscipline')]//a[contains(text(),'Edit chapter timelines')]");
              this.selenium.WaitForPageToLoad(this.SeleniumWait);
              this.selenium.Click("//table//tr[contains(.,'Chapter1')]//a[contains(text(),'Edit Topic Assignment')]");

              this.selenium.WaitForPageToLoad(this.SeleniumWait);
              this.selenium.Click("//table//tr[contains(.,'Topic1')]//a[contains(text(),'Edit')]");
              this.selenium.WaitForPageToLoad(this.SeleniumWait);
              this.selenium.Type("xpath=(//input[@id='ThresholdOfSuccess'])", "51");
              this.selenium.Click("xpath=(//input[@id='BlockTopicAtTesting'])");
              this.selenium.Click("xpath=(//input[@id='BlockCurriculumAtTesting'])");
              this.selenium.Click("xpath=(//input[@value='Update'])");

              this.selenium.WaitForPageToLoad(this.SeleniumWait);

              // check
              Assert.IsTrue(this.selenium.IsElementPresent("//table//tr[contains(.,'51')]"));
              Assert.IsTrue(this.selenium.IsElementPresent("//table//tr[contains(.,'True')]"));

              //delete discipline
              this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
              this.selenium.WaitForPageToLoad(this.SeleniumWait);
              this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'EditChapterTopicDiscipline')]//a[contains(text(),'Delete')]");
              this.selenium.GetConfirmation();
              while (selenium.IsAlertPresent())
              {
                  selenium.GetAlert();
              }

              //check right removing
              Assert.IsFalse(this.selenium.IsElementPresent("xpath=//tr[contains(.,'EditChapterTopicDiscipline')]"));

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
