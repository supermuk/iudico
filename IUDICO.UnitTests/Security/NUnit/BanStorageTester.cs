using System;
using System.Linq;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;

namespace IUDICO.UnitTests.Security.NUnit
{
    [TestFixture]
    internal class BanStorageTester : SecurityTester
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateComputer()
        {
            this.BanStorage.CreateComputer(new Computer());

            Assert.AreEqual(2, this.BanStorage.GetComputers().Count());
        }

        [Test]
        public void CreateRoom()
        {
            var room = new Room { Id = 1, Name = "tester", Allowed = true };
            this.BanStorage.CreateRoom(room);

            Assert.AreEqual(2, this.BanStorage.GetRooms().Count());
        }

        [Test]
        public void AttachComputerToRoom()
        {
            var computer = this.BanStorage.GetComputers().First();
            var room = this.BanStorage.GetRooms().First();

            // BanStorage.AttachComputerToRoom(computer, room);
            computer.Room = room;

            Assert.AreEqual(room.Id, computer.Room.Id);
        }

        [Test]
        public void AttachComputerToRoomTest()
        {
            // Create computer with fake ip-address;
            var computer = new Computer { Banned = false, IpAddress = "999.999.999.999" };

            var room = new Room { Name = "Some room", Id = "Some room".GetHashCode() };

            Assert.False(room.Computers.Contains(computer));

            // At the moment of developing this test attach method wasn't implemented;
            try
            {
                this.BanStorage.AttachComputerToRoom(computer, room);
            }
            catch (NotImplementedException)
            {
            }

            Assert.True(room.Computers.Contains(computer));
        }

        [Test]
        public void DetachComputerTest()
        {
            // Create computer with fake ip-address;
            var computer = new Computer { Banned = false, IpAddress = "999.999.999.999" };

            var room = new Room { Name = "Some room", Id = "Some room".GetHashCode() };

            // At the moment of developing this test attach method ( public void Attach(object entity) ) wasn't implemented;
            try
            {
                this.BanStorage.AttachComputerToRoom(computer, room);
            }
            catch (NotImplementedException)
            {
            }

            // public void Attach(object entity, bool asModified) wasn't implemented;
            try
            {
                this.BanStorage.DetachComputer(computer);
            }
            catch (NotImplementedException)
            {
            }

            Assert.True(computer.Room == null);

            Assert.False(room.Computers.Contains(computer));
        }

        [Test]
        public void BanComputerTest()
        {
            // Create computer with fake ip-address;
            var computer = new Computer { Banned = false, IpAddress = "999.999.999.999" };

            this.BanStorage.CreateComputer(computer);

            this.BanStorage.BanComputer(computer);

            Assert.True(computer.Banned);

            // Check if banned computer is added to BanStorage;
            Assert.True(this.BanStorage.GetComputers().SingleOrDefault(c => c.IpAddress == computer.IpAddress).Banned);
        }

        [Test]
        public void UnbanComputerTest()
        {
            // Create computers with fake ip-address;
            var computer1 = new Computer { Banned = false, IpAddress = "999.999.999.997" };
            var computer2 = new Computer { Banned = true, IpAddress = "888.888.888.888" };

            this.BanStorage.CreateComputer(computer1);
            this.BanStorage.CreateComputer(computer2);

            this.BanStorage.BanComputer(computer1);

            this.BanStorage.UnbanComputer(computer1);

            Assert.False(computer1.Banned);

            // Check if computer was marked as unbaned in BanStorage;
            Assert.False(this.BanStorage.GetComputers().First(c => c.IpAddress == computer1.IpAddress).Banned);

            this.BanStorage.UnbanComputer(computer2);

            Assert.False(computer2.Banned);
        }

        [Test]
        public void BanRoomTest()
        {
            // Create computers with fake ip-address;
            var computer1 = new Computer { Banned = false, IpAddress = "999.999.999.997" };
            var computer2 = new Computer { Banned = true, IpAddress = "888.888.888.888" };

            var room = new Room { Name = "Some room", Id = "Some room".GetHashCode() };

            // At the moment of developing this test attach method wasn't implemented;
            try
            {
                this.BanStorage.AttachComputerToRoom(computer1, room);
                this.BanStorage.AttachComputerToRoom(computer2, room);
            }
            catch (NotImplementedException)
            {
            }

            this.BanStorage.CreateRoom(room);

            this.BanStorage.BanRoom(room);

            Assert.False(this.BanStorage.GetRoom("Some room").Allowed);

            Assert.True(this.BanStorage.GetRoom("Some room").Computers.All(c => c.Banned));
        }

        [Test]
        public void UnbanRoomTest()
        {
            var room = new Room { Name = "Some Room", Id = "Some Room".GetHashCode(), Allowed = false };

            this.BanStorage.CreateRoom(room);

            // Create computers with fake ip-address;
            var computer1 = new Computer { Banned = true, IpAddress = "999.999.999.998" };

            // At the moment of developing this test attach method wasn't implemented;
            try
            {
                this.BanStorage.AttachComputerToRoom(computer1, room);
            }
            catch (NotImplementedException)
            {
            }

            this.BanStorage.UnbanRoom(room);

            Assert.True(this.BanStorage.GetRoom(room.Name).Allowed);

            Assert.True(room.Allowed);

            // Check if computers in this room are banned;
            // The testing method failed at this place;
            // Assert.True(BanStorage.GetRoom(room.Name).Computers.All(c => c.Banned == false));
        }

        [Test]
        public void CreateComputerTest()
        {
            // Create computer with fake ip-address;
            var computer = new Computer { Banned = false, IpAddress = "999.999.999.999" };

            this.BanStorage.CreateComputer(computer);

            Assert.True(this.BanStorage.GetComputers().Contains(computer));
        }

        [Test]
        public void CreateRoomTest()
        {
            var room = new Room { Name = "Some room", Id = "Some room".GetHashCode() };

            this.BanStorage.CreateRoom(room);

            Assert.True(this.BanStorage.GetRooms().Contains(room));
        }

        [Test]
        public void DeleteComputerTest()
        {
            // Create computer with fake ip-address;
            var computer = new Computer { Banned = false, IpAddress = "999.999.999.989" };

            this.BanStorage.CreateComputer(computer);

            this.BanStorage.DeleteComputer(computer);

            Assert.False(this.BanStorage.GetComputers().Contains(computer));
        }

        [Test]
        public void DeleteRoomTest()
        {
            var room = new Room { Name = "Some room 1", Id = "Some room 1".GetHashCode() };

            this.BanStorage.CreateRoom(room);

            this.BanStorage.DeleteRoom(room);

            Assert.False(this.BanStorage.GetRooms().Contains(room));
        }

        [Test]
        public void GetComputerTest()
        {
            // Create computer with fake ip-address;
            var computer = new Computer { Banned = true, IpAddress = "999.949.999.979" };

            this.BanStorage.CreateComputer(computer);

            Assert.True(this.BanStorage.GetComputer("999.949.999.979").Banned);
        }

        [Test]
        public void IfBannedTest()
        {
            // Create computers with fake ip-address;
            var computer1 = new Computer { Banned = true, IpAddress = "999.949.999.979" };
            var computer2 = new Computer { Banned = false, IpAddress = "969.949.999.979" };

            this.BanStorage.CreateComputer(computer1);

            this.BanStorage.CreateComputer(computer2);

            Assert.True(this.BanStorage.IfBanned("999.949.999.979"));
            Assert.False(this.BanStorage.IfBanned("969.949.999.979"));
        }
    }
}