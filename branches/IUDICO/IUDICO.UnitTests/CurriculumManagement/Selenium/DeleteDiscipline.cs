using System;
using System.Threading;
using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.CurriculumManagement.Selenium
{
    [TestFixture]
    public class DeleteDiscipline : SimpleWebTest
    {
        private const int SleepTime = 8000;

        [Test]
        public void DeleteDisciplineTest()
        {
            this.DefaultLogin("prof", "prof");

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            var isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'DisciplineForDelete')]");
            while (isPresent)
            {
                this.selenium.Click(
                    "//table[@id='disciplines']//tr[contains(.,'DisciplineForDelete')]//a[contains(text(),'Delete')]");
                this.selenium.GetConfirmation();
                this.selenium.Refresh();
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
                while (selenium.IsAlertPresent())
                {
                    selenium.GetAlert();
                }
                Thread.Sleep(SleepTime);

                isPresent =
                    this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'DisciplineForDelete')]");
            }
            Assert.IsFalse(isPresent);

            this.selenium.Click("//a[contains(@onclick, 'addDiscipline();')]");
            Thread.Sleep(SleepTime);
            this.selenium.Type("id=Name", "DisciplineForDelete");
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
                    "//table[@id='disciplines']//tr//td[contains(.,'DisciplineForDelete')]");
            Assert.IsTrue(isPresent);

            this.selenium.Click(
                "//table[@id='disciplines']//tr[contains(.,'DisciplineForDelete')]//a[contains(text(),'Delete')]");
            this.selenium.GetConfirmation();
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
 

            isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'DisciplineForDelete')]");
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

        [Test]
        public void DeleteAllDisciplinesTest()
        {
            this.DeleteAllDisciplines("prof", "prof");

            var isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'Delete')]");
            
            Assert.IsFalse(isPresent);

            isPresent =
                this.selenium.IsElementPresent(
                    "//table[@id='disciplines']//tr//td[contains(.,'No data available in table')]");
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

        public void DeleteAllDisciplines(string username, string password)
        {
            this.DefaultLogin(username, password);

            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            var isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'Delete')]");
            while (isPresent)
            {
                this.selenium.Click(
                    "//table[@id='disciplines']//tr//a[contains(text(),'Delete')]");
                this.selenium.GetConfirmation();
                this.selenium.Refresh();
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
                while (selenium.IsAlertPresent())
                {
                    selenium.GetAlert();
                }
                Thread.Sleep(SleepTime);

                isPresent =
                    this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'Delete')]");
            }   
        }
    }
}
