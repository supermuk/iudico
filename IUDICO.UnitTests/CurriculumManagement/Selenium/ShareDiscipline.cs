using System;
using System.Threading;
using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.CurriculumManagement.Selenium
{
    [TestFixture]
    public class ShareDiscipline : SimpleWebTest
    {
        private const int SleepTime = 8000;

        [Test]
        public void ShareDisciplineTest()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            var isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'ShareDiscipline')]");
            while (isPresent)
            {
                this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'ShareDiscipline')]//a[contains(text(),'Delete')]");
                this.selenium.GetConfirmation();
                this.selenium.Refresh();
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
                while (selenium.IsAlertPresent())
                {
                    selenium.GetAlert();
                }
                Thread.Sleep(SleepTime);

                isPresent =
                    this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'ShareDiscipline')]");
            }
            Assert.IsFalse(isPresent);

            this.selenium.Click("//a[contains(@onclick, 'addDiscipline();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "ShareDiscipline");
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
                    "//table[@id='disciplines']//tr//td[contains(.,'ShareDiscipline')]");
            Assert.IsTrue(isPresent);

            this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'ShareDiscipline')]//a[contains(text(),'Share')]");
            Thread.Sleep(SleepTime);
            this.selenium.Click("xpath=//tr[contains(.,'prof2')]//input[@name='sharewith']");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);

            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }

            this.DefaultLogin("prof2", "prof2");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'ShareDiscipline')]");
            Assert.IsTrue(isPresent);

            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'SharedDiscipline')]");
            Assert.IsFalse(isPresent);

            this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'ShareDiscipline')]//a[contains(text(),'Edit')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "SharedDiscipline");
            this.selenium.Click("xpath=(//div[contains(@class,'ui-dialog-buttonset')]//button[1])");
            Thread.Sleep(SleepTime);

            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr//td[contains(.,'SharedDiscipline')]");
            Assert.IsTrue(isPresent);

            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }

            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'ShareDiscipline')]");
            Assert.IsFalse(isPresent);

            isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'SharedDiscipline')]");
            Assert.IsTrue(isPresent);

            this.selenium.Click(
                "//table[@id='disciplines']//tr[contains(.,'SharedDiscipline')]//a[contains(text(),'Delete')]");
            this.selenium.GetConfirmation();
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            while (selenium.IsAlertPresent())
            {
                selenium.GetAlert();
            }
            Thread.Sleep(SleepTime);

            isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'SharedDiscipline')]");
            Assert.IsFalse(isPresent);

            try
            {
                this.Logout();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }

            this.DefaultLogin("prof2", "prof2");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'SharedDiscipline')]");
            Assert.IsFalse(isPresent);

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
