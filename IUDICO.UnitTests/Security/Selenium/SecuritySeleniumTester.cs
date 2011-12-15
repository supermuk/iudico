using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IUDICO.UnitTests.Base;
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
            selenium = new DefaultSelenium("localhost", 4444, "*chrome", "http://127.0.0.1:1556/");
            selenium.Start();

            selenium.Open("/");
            selenium.Type("id=loginUsername", "lex");
            selenium.Type("id=loginPassword", "lex");
            selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, 'UserActivity/Index')]");
            selenium.WaitForPageToLoad("30000");
        }

        [TearDown]
        public void Logout()
        {
            selenium.Open("/");
            selenium.Click("//a[contains(@href, '/Account/Logout')]");
            selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void Test1_CreateComputer()
        {
            selenium.Open("/Security/Index");
            selenium.Click("//a[contains(@href, '/Ban/AddComputers')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=ComputerIP", "25.25.26.23");
            selenium.Click("//form[@id='form0']/p/input");
            selenium.WaitForPageToLoad("30000");
            
            try
            {
                Assert.IsTrue(selenium.IsTextPresent("25.25.26.23"));
            }
            catch (AssertionException e)
            {
                //verificationErrors.Append(e.Message);
            }

            selenium.Click("//a[contains(@href, '/Security/Index')]");
            selenium.WaitForPageToLoad("30000");

        }

        [Test]
        public void Test2_BanComputer()
        {
            selenium.Open("/Security/Index");
            selenium.Click("//a[contains(text(),'Ban computer')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Ban/ComputerBan?computer=25.25.26.23')]");
            selenium.WaitForPageToLoad("30000");
            try
            {
                Assert.AreEqual("True", selenium.GetText("//div[@id='main']/table/tbody/tr[2]/td[4]"));
            }
            catch (AssertionException e)
            {
                // verificationErrors.Append(e.Message);
            }

            selenium.Click("//a[contains(@href, '/Security/Index')]");
            selenium.WaitForPageToLoad("30000");

        }

        [Test]
        public void Test4_DeleteComputer()
        {
            selenium.Open("/Security/Index");
            selenium.Click("//a[contains(@href, '/Ban/BanComputer')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Ban/DeleteComputer?computer=25.25.26.23')]");
            selenium.WaitForPageToLoad("30000");
            
            try
            {
                Assert.IsFalse(selenium.IsTextPresent("25.25.26.23"));
            }
            catch (AssertionException e)
            {
                //verificationErrors.Append(e.Message);
            }

            selenium.Click("//a[contains(@href, '/Security/Index')]");
            selenium.WaitForPageToLoad("30000");

        }

        [Test]
        public void Test3_EditComputer()
        {
            selenium.Open("/Security/Index");
            selenium.Click("//a[contains(@href, '/Ban/BanComputer')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Ban/EditComputer?computer=25.25.26.23')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Security/Index')]");
            selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void Test5_AddRoom()
        {
            selenium.Open("/Security/Index");
            selenium.Click("//a[contains(@href, '/Ban/AddRoom')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("//input[@id='Name']", "119");
            selenium.Click("//input[@id='Allowed']");
            selenium.Click("css=input[type=\"submit\"]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Security/Index')]");
            selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void Test6_BanRoom()
        {
            selenium.Open("/Security/Index");
            selenium.Click("//a[contains(@href, '/Ban/BanRoom')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Ban/RoomBan?room=119')]");
            selenium.WaitForPageToLoad("30000");
            try
            {
                Assert.AreEqual("False", selenium.GetText("//div[@id='main']/table/tbody/tr[2]/td[2]"));
            }
            catch (AssertionException e)
            {
                // verificationErrors.Append(e.Message);
            }
            selenium.Click("//a[contains(@href, '/Security/Index')]");
            selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void Test7_DeleteRoom()
        {
            selenium.Open("/Security/Index");
            selenium.Click("//a[contains(@href, '/Ban/BanRoom')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Ban/DeleteRoom?room=119')]");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(@href, '/Security/Index')]");
            selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void Test8_OverallStats()
        {
            selenium.Click("//a[contains(@href, '/UserActivity/Overall')]");
            selenium.WaitForPageToLoad("30000");
            String todayNumberOfRequests = selenium.GetText("//div[@id='main']/table/tfoot/tr/td[3]");
            selenium.Refresh();
            selenium.WaitForPageToLoad("30000");
            try
            {
                Assert.AreNotEqual(todayNumberOfRequests, selenium.GetText("//div[@id='main']/table/tfoot/tr/td[3]"));
            }
            catch (AssertionException e)
            {
                // verificationErrors.Append(e.Message);
            }
            selenium.Click("//a[contains(@href, '/Security/Index')]");
            selenium.WaitForPageToLoad("30000");
        }
    }
}
