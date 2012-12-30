using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selenium;

namespace IUDICO.UnitTests.Security.Selenium
{
    /// <summary>
    /// Helper class for SecuritySeleniumTester.
    /// Performs basic actions with rooms.
    /// </summary>
    class Rooms : Security
    {
        public Rooms(ISelenium selenium) :
            base(selenium) { }

        /// <summary>
        /// Click Add room
        /// </summary>
        public void Add(string room, bool allowed)
        {
            this.selenium.Click("//a[contains(@href, '/Edit/EditRooms')]");
            this.selenium.WaitForPageToLoad("30000");

            this.selenium.Click("link=Add room");
            this.selenium.WaitForPageToLoad("30000");

            this.selenium.Type("id=Name", room);
            if (allowed)
            {
                this.selenium.Check("id=Allowed");
            }
            else
            {
                this.selenium.Uncheck("id=Allowed");
            }
            this.selenium.Click("css=p > input[type=\"submit\"]");
            this.selenium.WaitForPageToLoad("30000");

            /*this.selenium.Click("//a[contains(@href, '/Edit/EditRooms')]");
            this.selenium.WaitForPageToLoad("30000");*/
        }

        /// <summary>
        /// Click Delete room
        /// </summary>
        public void Remove(string room)
        {
            this.selenium.Click("//a[contains(@href," +
                "'/Edit/DeleteRoom?room=" + room + "')]");
            this.selenium.WaitForPageToLoad("30000");
        }

        /// <summary>
        /// Unban room
        /// </summary>
        public void Unban(string room)
        {
            this.selenium.Click("//a[contains(@href," +
                "'/Edit/RoomUnban?room=" + room + "')]");
            this.selenium.WaitForPageToLoad("30000");
        }

        /// <summary>
        /// Ban room
        /// </summary>
        public void Ban(string room)
        {
            this.selenium.Click("//a[contains(@href," +
                "'/Edit/RoomBan?room=" + room + "')]");
            this.selenium.WaitForPageToLoad("30000");
        }

        /// <summary>
        /// Check if computer with given IP is banned.
        /// </summary>
        public bool IsAllowed(string room)
        {
            return this.selenium.GetText("css=td:contains('"
                + room + "') + td").Equals("True");
        }
    }
}
