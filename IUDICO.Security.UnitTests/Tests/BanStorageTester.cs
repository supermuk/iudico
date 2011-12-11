using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.Security.UnitTests.Tests
{
    class BanStorageTester : SecurityTester
    {
        [Test]
        public void CreateComputer()
        {
            BanStorage.CreateComputer(new Computer());

            Assert.AreEqual(2, BanStorage.GetComputers().Count());
        }

        [Test]
        public void CreateRoom()
        {
            var room = new Room
            {
                Id = 1,
                Name = "tester",
                Allowed = true
            };
            BanStorage.CreateRoom(room);

            Assert.AreEqual(2, BanStorage.GetRooms().Count());
        }

        [Test]
        public void AttachComputerToRoom()
        {
            var computer = BanStorage.GetComputers().First();
            var room = BanStorage.GetRooms().First();

            BanStorage.AttachComputerToRoom(computer, room);

            Assert.AreEqual(room.Id, computer.Room.Id);
        }
    }
}
