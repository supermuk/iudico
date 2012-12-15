using System;
using System.Linq;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.Security.NUnit
{
    //[TestFixture]
    internal class BanStorageTester : SecurityTester
    {
        //[SetUp]
        //public void Setup()
        //{
        //}
        public Computer AddComputer(bool isBanned, string ip)
        {
            return new Computer { Banned = isBanned, IpAddress = ip };
        }
        public Room AddRoom(int id, string name, bool allowed)
        {
            return new Room { Id = id, Name = name, Allowed = allowed };
        }

        //author: Фай Роман
        [Test]
        public void CreateComputerTest()
        {
            // create computer
            var computer = AddComputer(false, "999.999.999.999");
            // add computers to storage
            this.BanStorage.CreateComputer(new Computer());
            this.BanStorage.CreateComputer(new Computer());
            this.BanStorage.CreateComputer(new Computer());
            this.BanStorage.CreateComputer(computer);

            Assert.AreEqual(5, this.BanStorage.GetComputers().Count());
            Assert.True(this.BanStorage.GetComputers().Contains(computer));
        }

        //author: Фай Роман
        [Test]
        public void CreateRoomTest()
        {
            // create various rooms
            var room1 = AddRoom(1, "tester1", true);
            var room2 = AddRoom(2, "tester2", false);
            var room3 = AddRoom(3, "tester3",true);
            // add rooms to storage
            this.BanStorage.CreateRoom(room1);
            this.BanStorage.CreateRoom(room2);
            this.BanStorage.CreateRoom(room3);

            Assert.AreEqual(4, this.BanStorage.GetRooms().Count());
            Assert.True(this.BanStorage.GetRooms().Contains(room2));
        }

        [Test]
        public void AttachComputerToRoom()
        {
            var computer = this.BanStorage.GetComputers().First();
            var room = this.BanStorage.GetRooms().First();

            BanStorage.AttachComputerToRoom(computer, room);
            // computer.RoomRef = room.Id;

            //REDO
            //Assert.AreEqual(room.Id, computer.Room.Id);
        }

        //author: Фай Роман
        [Test]
        public void BanComputerTest()
        {
            // create computer
            var computer = AddComputer(false, "999.999.999.999");
            // add created computer to storage and ban it
            this.BanStorage.CreateComputer(computer);
            this.BanStorage.BanComputer(computer);

            Assert.True(computer.Banned);

            // check if banned computer is added to BanStorage
            Assert.True(this.BanStorage.GetComputers().SingleOrDefault(c => c.IpAddress == computer.IpAddress).Banned);
        }

        //author: Фай Роман
        [Test]
        public void UnbanComputerTest()
        {
            // create computers
            var computer1 = AddComputer(false, "999.999.999.997");
            var computer2 = AddComputer(true, "888.888.888.888");
            // add them to storage
            this.BanStorage.CreateComputer(computer1);
            this.BanStorage.CreateComputer(computer2);
            //baning / unbaning concrete computer
            this.BanStorage.BanComputer(computer1);
            this.BanStorage.UnbanComputer(computer1);

            Assert.False(computer1.Banned);

            // check if computer was marked as unbaned in BanStorage;
            Assert.False(this.BanStorage.GetComputers().First(c => c.IpAddress == computer1.IpAddress).Banned);
            //unban banned computer
            this.BanStorage.UnbanComputer(computer2);
          
            Assert.False(computer2.Banned);
        }

        [Test]
        public void BanRoomTest()
        {
            var room = new Room { Name = "Room", Allowed = true };
            this.BanStorage.CreateRoom(room);
            Assert.True(this.BanStorage.GetRoom("Room").Allowed);
            this.BanStorage.BanRoom(room);
            Assert.False(this.BanStorage.GetRoom("Room").Allowed);
        }

        [Test]
        public void UnbanRoomTest()
        {
            var room = new Room { Name = "Room", Allowed = false };
            this.BanStorage.CreateRoom(room);
            Assert.False(this.BanStorage.GetRoom("Room").Allowed);
            this.BanStorage.UnbanRoom(room);
            Assert.True(this.BanStorage.GetRoom("Room").Allowed);          
        }

        //author: Фай Роман
        [Test]
        public void DeleteComputerTest()
        {
            // create computers
            var computer1 = AddComputer(false, "999.999.999.989");
            var computer2 = AddComputer(true, "888.888.888.888");
            // adding them to storage
            this.BanStorage.CreateComputer(computer1);
            this.BanStorage.CreateComputer(computer2);
            // delete concrete computer
            this.BanStorage.DeleteComputer(computer1);

            Assert.False(this.BanStorage.GetComputers().Contains(computer1));
            Assert.AreEqual(2, this.BanStorage.GetComputers().Count());
        }

        //author: Фай Роман
        [Test]
        public void DeleteRoomTest()
        {
            // create rooms...
            var room1 = AddRoom(1, "tester1", true);
            var room2 = AddRoom(2, "tester2", true);
            // adding them to storage
            this.BanStorage.CreateRoom(room1);
            this.BanStorage.CreateRoom(room2);
            //delete concrete room
            this.BanStorage.DeleteRoom(room1);

            Assert.False(this.BanStorage.GetRooms().Contains(room1));
            Assert.AreEqual(2, this.BanStorage.GetRooms().Count());
        }

        //author: Фай Роман
        [Test]
        public void GetComputerTest()
        {
            // Create computer
            var computer = AddComputer(true, "999.949.999.979");
            // add it to storage
            this.BanStorage.CreateComputer(computer);
            //get it
            Assert.True(this.BanStorage.GetComputer("999.949.999.979").Banned);
        }

        //author: Фай Роман
        [Test]
        public void IfBannedTest()
        {
            // create computers
            var computer1 = AddComputer(true, "999.949.999.979");
            var computer2 = AddComputer(false,"969.949.999.979");
            // add them to storage
            this.BanStorage.CreateComputer(computer1);
            this.BanStorage.CreateComputer(computer2);

            Assert.True(this.BanStorage.IfBanned("999.949.999.979"));
            Assert.False(this.BanStorage.IfBanned("969.949.999.979"));
        }
    }
}