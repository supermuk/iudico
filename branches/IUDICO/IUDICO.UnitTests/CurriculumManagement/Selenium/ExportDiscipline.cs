using System;
using System.Threading;
using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.CurriculumManagement.Selenium
{
    [TestFixture]
    public class ExportDiscipline : SimpleWebTest
    {
        private const int SleepTime = 8000;

        [Test]
        public void ExportDisciplineTest()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            var isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'ExportDiscipline')]");
            while (isPresent)
            {
                this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'ExportDiscipline')]//a[contains(text(),'Delete')]");
                this.selenium.GetConfirmation();
                this.selenium.Refresh();
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
                while (selenium.IsAlertPresent())
                {
                    selenium.GetAlert();
                }
                Thread.Sleep(SleepTime);

                isPresent =
                    this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'ExportDiscipline')]");
            }
            Assert.IsFalse(isPresent);

            this.selenium.Click("//a[contains(@onclick, 'addDiscipline();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "ExportDiscipline");
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
                    "//table[@id='disciplines']//tr//td[contains(.,'ExportDiscipline')]");
            Assert.IsTrue(isPresent);

            this.selenium.Click("//table[@id='disciplines']//tr[contains(.,'ExportDiscipline')]//a[contains(text(),'Export')]");

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
