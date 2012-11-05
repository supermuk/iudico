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
            this.selenium = new DefaultSelenium("localhost", 4444,
                "*firefox", ConfigurationManager.AppSettings["SELENIUM_URL"]);
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

        //author: Крупич Андрій
        [Test]
        public void Test1_CreateAndDeleteComputer()
        {
            Computers computers = new Computers(selenium);
            computers.Clean(secretIP);
            computers.Add(secretIP);
            Assert.IsTrue(computers.IsPresented(secretIP));
            computers.Delete(secretIP);
            Assert.IsFalse(computers.IsPresented(secretIP));
            computers.Logout();
        }

        //author: Крупич Андрій
        [Test]
        public void Test2_BanUnbanComputer()
        {
            Computers computers = new Computers(selenium);
            computers.Clean(secretIP);
            computers.Add(secretIP);
            Assert.IsFalse(computers.IsBanned(secretIP));
            computers.Ban(secretIP);
            Assert.IsTrue(computers.IsBanned(secretIP));
            computers.Unban(secretIP);
            Assert.IsFalse(computers.IsBanned(secretIP));
            computers.Delete(secretIP);
            computers.Logout();
        }

        //author: Крупич Андрій
        [Test]
        public void Test3_EditComputer()
        {
            Computers computers = new Computers(selenium);
            computers.Clean(secretIP);
            computers.Add(secretIP);
            Assert.IsFalse(computers.IsPresented(secretUser));
            computers.Edit(secretIP, false, true, secretUser);
            Assert.IsTrue(computers.IsPresented(secretUser));
            Assert.IsTrue(computers.IsBanned(secretIP));
            computers.Delete(secretIP);
            computers.Logout();
        }

        //author: Крупич Андрій
        [Test]
        public void Test4_NoComputerDuplicates()
        {
            Computers computers = new Computers(selenium);
            computers.Clean(secretIP);
            computers.Add(secretIP);
            computers.GoBack();
            computers.Add(secretIP);
            computers.Delete(secretIP);
            Assert.IsFalse(computers.IsPresented(secretIP));
            computers.Logout();
        }

        //author: Крупич Андрій
        [Test]
        public void Test5_AddAndDeleteRoom()
        {
            Rooms rooms = new Rooms(selenium);
            rooms.Add(secretRoom, true);
            Assert.IsTrue(rooms.IsPresented(secretRoom));
            Assert.IsTrue(rooms.IsAllowed(secretRoom));
            rooms.Remove(secretRoom);
            Assert.IsFalse(rooms.IsPresented(secretRoom));
            rooms.Logout();
        }

        //author: Крупич Андрій
        [Test]
        public void Test6_BanRoom()
        {
            Rooms rooms = new Rooms(selenium);
            rooms.Add(secretRoom, false);
            Assert.IsFalse(rooms.IsAllowed(secretRoom));
            rooms.Unban(secretRoom);
            Assert.IsTrue(rooms.IsAllowed(secretRoom));
            rooms.Ban(secretRoom);
            Assert.IsFalse(rooms.IsAllowed(secretRoom));
            rooms.Remove(secretRoom);
            rooms.Logout();
        }

        //author: Крупич Андрій
        [Test]
        public void Test7_NoRoomsDuplicates()
        {
            Rooms rooms = new Rooms(selenium);
            rooms.Add(secretRoom, true);
            rooms.GoBack();
            rooms.Add(secretRoom, false);
            rooms.Remove(secretRoom);
            Assert.IsFalse(rooms.IsPresented(secretRoom));
            rooms.Logout();
        }
        //fails because of the bug
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