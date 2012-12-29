using System;
using System.Threading;
using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.CurriculumManagement.Selenium
{
    [TestFixture]
    public class ImportDiscipline : SimpleWebTest
    {
        private const int SleepTime = 8000;

        /// <summary>
        /// Author - Chopenko Vitaliy
        /// </summary>
        [Test]
        public void ImportDisciplineTest()
        {
            // logging in
            this.DefaultLogin("prof", "prof");

            // moving to disciplines
            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            var isPresent = this.selenium.IsElementPresent("//table[@id='disciplines']");
            Assert.IsTrue(isPresent);

            // checks if present discipline to import, if yes, deletes it
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

            // moving to import
            this.selenium.Click("//a[contains(text(),'Import')]");
            Thread.Sleep(SleepTime);

            // importing
            selenium.Click("//*[@id=\"file_upload\"]/div/span/a");
            Thread.Sleep(SleepTime);

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
