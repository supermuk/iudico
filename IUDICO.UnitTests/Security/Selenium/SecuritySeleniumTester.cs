﻿using System;
using System.Configuration;
using System.Text;
using NUnit.Framework;

using Selenium;

namespace IUDICO.UnitTests.Security.Selenium
{
    [TestFixture]
    public class SecuritySeleniumTester
    {
        private ISelenium selenium;

        private StringBuilder verificationErrors;

        [SetUp]
        public void Login()
        {
            this.selenium = new DefaultSelenium("localhost", 4444, "*chrome", ConfigurationManager.AppSettings["SELENIUM_URL"]);
            selenium.Start();
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void Logout()
        {
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
        public void Test1_CreateAndDeleteComputer()
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginUsername", "lex");
            this.selenium.Type("id=loginPassword", "lex");
            this.selenium.Click("id=loginDefaultButton");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Security");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Add computer");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Type("id=ComputerIP", "192.169.0.32");
            this.selenium.Click("name=saveButton");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Security");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Edit computer");
            this.selenium.WaitForPageToLoad("30000");

            Assert.IsTrue(this.selenium.IsTextPresent("192.169.0.32"));

            this.selenium.Click("//a[contains(@href, '/Ban/DeleteComputer?computer=192.169.0.32')]");

            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Logout");
            this.selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void Test2_BanComputer()
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginUsername", "lex");
            this.selenium.Type("id=loginPassword", "lex");
            this.selenium.Click("id=loginDefaultButton");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Security");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Add computer");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Type("id=ComputerIP", "192.169.0.32");
            this.selenium.Click("name=saveButton");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Security");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Edit computer");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Ban");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Unban");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Security");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Edit computer");
            this.selenium.WaitForPageToLoad("30000");

            Assert.IsTrue(this.selenium.IsTextPresent("192.169.0.32"));

            this.selenium.Click("//a[contains(@href, '/Ban/DeleteComputer?computer=192.169.0.32')]");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Logout");
            this.selenium.WaitForPageToLoad("30000");
        }


        [Test]
        public void Test3_EditComputer()
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginUsername", "lex");
            this.selenium.Type("id=loginPassword", "lex");
            this.selenium.Click("id=loginDefaultButton");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Security");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Add computer");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Type("id=ComputerIP", "192.169.0.32");
            this.selenium.Click("name=saveButton");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Security");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Edit computer");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Edit");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Type("id=CurrentUser", "lex");
            this.selenium.Click("name=saveButton");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Back to list");
            this.selenium.WaitForPageToLoad("30000");

            Assert.IsTrue(this.selenium.IsTextPresent("192.169.0.32"));

            this.selenium.Click("//a[contains(@href, '/Ban/DeleteComputer?computer=192.169.0.32')]");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Logout");
            this.selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void Test5_AddAndDeleteRoom()
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginUsername", "lex");
            this.selenium.Type("id=loginPassword", "lex");
            this.selenium.Click("id=loginDefaultButton");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Security");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Add room");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Type("id=Name", "145");
            this.selenium.Click("css=p > input[type=\"submit\"]");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Security");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Ban room");
            this.selenium.WaitForPageToLoad("30000");

            Assert.IsTrue(this.selenium.IsTextPresent("145"));

            this.selenium.Click("//a[contains(@href, '/Ban/DeleteRoom?room=145')]");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Logout");
            this.selenium.WaitForPageToLoad("30000");
        }

        [Test]
        public void Test6_BanRoom()
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginUsername", "lex");
            this.selenium.Type("id=loginPassword", "lex");
            this.selenium.Click("id=loginDefaultButton");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Security");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Add room");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Type("id=Name", "142");
            this.selenium.Click("css=p > input[type=\"submit\"]");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Security");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Ban room");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Unban");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Ban");
            this.selenium.WaitForPageToLoad("30000");

            Assert.IsTrue(this.selenium.IsTextPresent("142"));

            this.selenium.Click("//a[contains(@href, '/Ban/DeleteRoom?room=142')]");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Logout");
            this.selenium.WaitForPageToLoad("30000");
        }


        [Test]
        public void Test8_OverallStats()
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginUsername", "lex");
            this.selenium.Type("id=loginPassword", "lex");
            this.selenium.Click("id=loginDefaultButton");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Security");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Overall stats");
            this.selenium.WaitForPageToLoad("30000");
            this.selenium.Click("link=Logout");
            this.selenium.WaitForPageToLoad("30000");

            Assert.True(this.selenium.IsTextPresent("User"));
        }
    }
}