using System;
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

        private string secretIP = "18.18.18.18";
        private string secretUser = "user18";
        private string secretRoom = "room18";

        [SetUp]
        public void Start()
        {
            this.selenium = new DefaultSelenium(
                "localhost", 4444, "*firefox", ConfigurationManager.AppSettings["SELENIUM_URL"]);

            this.selenium.Start();

            this.verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void Finish()
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

        // author: Крупич Андрій
        [Test]
        public void Test1_CreateAndDeleteComputer()
        {
            var computers = new Computers(this.selenium);

            computers.Clean(this.secretIP);

            computers.Add(this.secretIP);

            Assert.IsTrue(computers.IsPresented(this.secretIP));

            computers.Delete(this.secretIP);

            Assert.IsFalse(computers.IsPresented(this.secretIP));

            computers.Logout();
        }

        // author: Крупич Андрій
        [Test]
        public void Test2_BanUnbanComputer()
        {
            var computers = new Computers(this.selenium);

            computers.Clean(this.secretIP);

            computers.Add(this.secretIP);

            Assert.IsFalse(computers.IsBanned(this.secretIP));

            computers.Ban(this.secretIP);

            Assert.IsTrue(computers.IsBanned(this.secretIP));

            computers.Unban(this.secretIP);

            Assert.IsFalse(computers.IsBanned(this.secretIP));

            computers.Delete(this.secretIP);

            computers.Logout();
        }

        // author: Крупич Андрій
        [Test]
        public void Test3_EditComputer()
        {
            var computers = new Computers(this.selenium);

            computers.Clean(this.secretIP);

            computers.Add(this.secretIP);

            Assert.IsFalse(computers.IsPresented(this.secretUser));

            computers.Edit(this.secretIP, false, true, this.secretUser);

            Assert.IsTrue(computers.IsPresented(this.secretUser));
            Assert.IsTrue(computers.IsBanned(this.secretIP));

            computers.Delete(this.secretIP);

            computers.Logout();
        }

        // author: Крупич Андрій
        [Test]
        public void Test4_NoComputerDuplicates()
        {
            var computers = new Computers(this.selenium);

            computers.Clean(this.secretIP);

            computers.Add(this.secretIP);

            computers.Add(this.secretIP);

            computers.Delete(this.secretIP);

            Assert.IsFalse(computers.IsPresented(this.secretIP));

            computers.Logout();
        }

        // author: Крупич Андрій
        [Test]
        public void Test5_AddAndDeleteRoom()
        {
            var rooms = new Rooms(this.selenium);

            rooms.Add(this.secretRoom, true);

            Assert.IsTrue(rooms.IsPresented(this.secretRoom));
            Assert.IsTrue(rooms.IsAllowed(this.secretRoom));

            rooms.Remove(this.secretRoom);

            Assert.IsFalse(rooms.IsPresented(this.secretRoom));

            rooms.Logout();
        }

        // author: Крупич Андрій
        [Test]
        public void Test6_BanRoom()
        {
            var rooms = new Rooms(this.selenium);

            rooms.Add(this.secretRoom, false);

            Assert.IsFalse(rooms.IsAllowed(this.secretRoom));

            rooms.Unban(this.secretRoom);

            Assert.IsTrue(rooms.IsAllowed(this.secretRoom));

            rooms.Ban(this.secretRoom);

            Assert.IsFalse(rooms.IsAllowed(this.secretRoom));

            rooms.Remove(this.secretRoom);

            rooms.Logout();
        }

        // author: Крупич Андрій
        [Test]
        public void Test7_NoRoomsDuplicates()
        {
            var rooms = new Rooms(this.selenium);

            rooms.Add(this.secretRoom, true);

            rooms.GoBack();

            rooms.Add(this.secretRoom, false);

            rooms.Remove(this.secretRoom);

            Assert.IsFalse(rooms.IsPresented(this.secretRoom));

            rooms.Logout();
        }

        // fails because of the bug
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

            Assert.True(this.selenium.IsTextPresent("User"));
            
            this.selenium.Click("link=Logout");
            this.selenium.WaitForPageToLoad("30000");
        }
    }
}