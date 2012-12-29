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

        /// <summary>
        /// Author - Kachmar Oleg
        /// </summary>
        [Test]
        public void DeleteDisciplineTest()
        {
            // logging in
            this.DefaultLogin("prof", "prof");

            // moving to disciplines
            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // checks if present disciplines to delete
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

            // checks if deleted succesfully
            Assert.IsFalse(isPresent);

            // adds discipline
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

            // checks if present discipline to delete
            isPresent =
                this.selenium.IsElementPresent(
                    "//table[@id='disciplines']//tr//td[contains(.,'DisciplineForDelete')]");
            Assert.IsTrue(isPresent);

            // deletes discipline to delete
            this.selenium.Click(
                "//table[@id='disciplines']//tr[contains(.,'DisciplineForDelete')]//a[contains(text(),'Delete')]");
            this.selenium.GetConfirmation();
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
 
            // checks if delted successfully
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

        /// <summary>
        /// Author - Kachmar Oleg
        /// </summary>
        [Test]
        public void DeleteAllDisciplinesTest()
        {
            // delete all disciplines
            this.DeleteAllDisciplines("prof", "prof");

            // checks if deleted successfully
            var isPresent =
                this.selenium.IsElementPresent("//table[@id='disciplines']//tr[contains(.,'Delete')]");
            Assert.IsFalse(isPresent);
            
            // checks the same thing
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

        /// <summary>
        /// Author - Kachmar Oleg
        /// </summary>
        public void DeleteAllDisciplines(string username, string password)
        {
            // logging in
            this.DefaultLogin(username, password);

            // moving to disciplines
            this.selenium.Click("//a[contains(@href,'/DisciplineAction')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // delete all disciplines
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
            // checks if deleted successfully
            Assert.IsFalse(isPresent);
        }
    }
}
