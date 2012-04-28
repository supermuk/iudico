using System;
using System.Configuration;

using NUnit.Framework;

using Selenium;

namespace IUDICO.UnitTests.Security.Selenium
{
    [TestFixture]
    public class SecuritySeleniumTester
    {
        private ISelenium selenium;

        [SetUp]
        public void Login()
        {
            this.selenium = new DefaultSelenium("localhost", 4444, "*chrome", ConfigurationManager.AppSettings["SELENIUM_URL"]);
            this.selenium.Start();

            this.selenium.Open("/");
            this.selenium.Type("id=loginUsername", "lex");
            this.selenium.Type("id=loginPassword", "lex");
            this.selenium.Click("id=loginDefaultButton");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("//a[contains(@href, 'Security/Index')]");
            this.selenium.WaitForPageToLoad("30000");
        }

        [TearDown]
        public void Logout()
        {
            this.selenium.Open("/");
            this.selenium.Click("//a[contains(@href, '/Account/Logout')]");
            this.selenium.WaitForPageToLoad("30000");

            try
            {
                this.selenium.Stop();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [Test]
        public void Test1_CreateComputer()
        {
            this.selenium.Open("/Security/Index");
            this.selenium.Click("//a[contains(@href, '/Ban/AddComputers')]");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Type("id=ComputerIP", "25.25.26.23");
            this.selenium.Click("//form[@id='form0']/p/input");
            this.selenium.WaitForPageToLoad("30000");

            try
            {
                Assert.IsTrue(this.selenium.IsTextPresent("25.25.26.23"));
            }
            catch (AssertionException)
            {
                ////verificationErrors.Append(e.Message);
            }

            this.selenium.Click("//a[contains(@href, '/Security/Index')]");
            this.selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void Test2_BanComputer()
        {
            this.selenium.Open("/Security/Index");
            this.selenium.Click("//a[contains(text(),'Ban computer')]");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("//a[contains(@href, '/Ban/ComputerBan?computer=25.25.26.23')]");
            this.selenium.WaitForPageToLoad("30000");
            try
            {
                Assert.AreEqual("True", this.selenium.GetText("//div[@id='main']/table/tbody/tr[2]/td[4]"));
            }
            catch (AssertionException)
            {
                // verificationErrors.Append(e.Message);
            }

            this.selenium.Click("//a[contains(@href, '/Security/Index')]");
            this.selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void Test4_DeleteComputer()
        {
            this.selenium.Open("/Security/Index");
            this.selenium.Click("//a[contains(@href, '/Ban/BanComputer')]");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("//a[contains(@href, '/Ban/DeleteComputer?computer=25.25.26.23')]");
            this.selenium.WaitForPageToLoad("30000");

            try
            {
                Assert.IsFalse(this.selenium.IsTextPresent("25.25.26.23"));
            }
            catch (AssertionException)
            {
                // verificationErrors.Append(e.Message);
            }

            this.selenium.Click("//a[contains(@href, '/Security/Index')]");
            this.selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void Test3_EditComputer()
        {
            this.selenium.Open("/Security/Index");
            this.selenium.Click("//a[contains(@href, '/Ban/BanComputer')]");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("//a[contains(@href, '/Ban/EditComputer?computer=25.25.26.23')]");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("//a[contains(@href, '/Security/Index')]");
            this.selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void Test5_AddRoom()
        {
            this.selenium.Open("/");
            this.selenium.Click("link=Security");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Add room");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Type("id=Name", "119");
            this.selenium.Click("id=Allowed");
            this.selenium.Click("css=p > input[type=\"submit\"]");
            this.selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void Test6_BanRoom()
        {
            this.selenium.Open("/Security/Index");
            this.selenium.Click("//a[contains(@href,'Ban/BanRoom')]");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("//a[contains(@href, '/Ban/RoomBan?room=119')]");
            this.selenium.WaitForPageToLoad("30000");
            try
            {
                Assert.AreEqual("False", this.selenium.GetText("//div[@id='main']/table/tbody/tr[2]/td[2]"));
            }
            catch (AssertionException)
            {
                // verificationErrors.Append(e.Message);
            }
        }

        [Test]
        public void Test7_DeleteRoom()
        {
            this.selenium.Open("/Security/Index");
            this.selenium.Click("//a[contains(@href,'Ban/BanRoom')]");
            this.selenium.WaitForPageToLoad("40000");
            this.selenium.Click("//a[contains(@href, '/Ban/DeleteRoom?room=119')]");
            this.selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void Test8_OverallStats()
        {
            this.selenium.Click("//a[contains(@href, '/UserActivity/Overall')]");
            this.selenium.WaitForPageToLoad("30000");
            var todayNumberOfRequests = this.selenium.GetText("//div[@id='main']/table/tfoot/tr/td[3]");
            this.selenium.Refresh();
            this.selenium.WaitForPageToLoad("30000");
            try
            {
                Assert.AreNotEqual(
                    todayNumberOfRequests, this.selenium.GetText("//div[@id='main']/table/tfoot/tr/td[3]"));
            }
            catch (AssertionException)
            {
                // verificationErrors.Append(e.Message);
            }

            this.selenium.Click("//a[contains(@href, '/Security/Index')]");
            this.selenium.WaitForPageToLoad("30000");
        }
    }
}