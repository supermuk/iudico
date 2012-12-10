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

        [Test]
        public void OpenDisciplineTest()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
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
        [Test]
        public void CreateDisciplineTest()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']");
            Assert.IsTrue(isPresent);

            isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'MyNewDiscipline')]");
            if(isPresent)
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
        public void CreateDisciplineWithoutNameTest()
        {
            this.DefaultLogin("prof2", "prof2");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']");
            Assert.IsTrue(isPresent);

            var isEmptyTable =
                this.selenium.IsElementPresent(
                    "//table[@id='disciplines']//tr//td[contains(.,'No data available in table')]");


            this.selenium.Click("//a[contains(@onclick, 'addDiscipline();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", string.Empty);
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);
            isPresent =
                this.selenium.IsElementPresent("//span[contains(.,'Correct the following error(s) and try again:')]");
            Assert.IsTrue(isPresent);

            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[2])");
            Thread.Sleep(SleepTime);
            isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']");
            Assert.IsTrue(isPresent);

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
    }
}
