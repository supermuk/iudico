using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;
using IUDICO.Security.UnitTests.Infrastructure;
using IUDICO.Security.Models.Storages;

namespace IUDICO.Security.UnitTests
{
    [TestFixture]
    class SecurityTester
    {
        private static Mocks _Mocks;

        static SecurityTester()
        {
            _Mocks = new Mocks();
        }

        protected IBanStorage BanStorage
        {
            get { return _Mocks.BanStorage; }
        }

        protected ISecurityStorage SecurityStorage
        {
            get { return _Mocks.SecurityStorage; }
        }

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            _Mocks.Reset();
        }

        [Test]
        public void AttachComputerToRoomTest()
        {
            //Create computer with fake ip-address;
            Computer computer = new Computer {Banned = false, IpAddress = "999.999.999.999"};

            Room room = new Room {Name = "Some room", Id = "Some room".GetHashCode()};

            Assert.False(room.Computers.Contains(computer));


            //At the moment of developing this test attach method wasn't implemented;
            try
            {
                BanStorage.AttachComputerToRoom(computer, room);
            }
            catch (NotImplementedException)
            {
                
            }

            Assert.True(room.Computers.Contains(computer));

        }


        [Test]
        public void DetachComputerTest()
        {
            //Create computer with fake ip-address;
            Computer computer = new Computer { Banned = false, IpAddress = "999.999.999.999" };

            Room room = new Room { Name = "Some room", Id = "Some room".GetHashCode() };



            //At the moment of developing this test attach method ( public void Attach(object entity) ) wasn't implemented;
            try
            {
                BanStorage.AttachComputerToRoom(computer, room);
            }
            catch (NotImplementedException)
            {

            }

            //public void Attach(object entity, bool asModified) wasn't implemented;
            try
            {
                BanStorage.DetachComputer(computer);
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
            //Create computer with fake ip-address;
            Computer computer = new Computer { Banned = false, IpAddress = "999.999.999.999" };

            BanStorage.CreateComputer(computer);

            BanStorage.BanComputer(computer);

            Assert.True(computer.Banned);

            //Check if banned computer is added to BanStorage;
            Assert.True(BanStorage.GetComputers().SingleOrDefault(c => c.IpAddress == computer.IpAddress).Banned);
        }


        [Test]
        public void UnbanComputerTest()
        {
            //Create computers with fake ip-address;
            Computer computer1 = new Computer { Banned = false, IpAddress = "999.999.999.997" };
            Computer computer2 = new Computer { Banned = true, IpAddress = "888.888.888.888" };


            BanStorage.CreateComputer(computer1);
            BanStorage.CreateComputer(computer2);

            BanStorage.BanComputer(computer1);

            BanStorage.UnbanComputer(computer1);

            Assert.False(computer1.Banned);

            //Check if computer was marked as unbaned in BanStorage;
            Assert.False(BanStorage.GetComputers().First(c => c.IpAddress == computer1.IpAddress).Banned);                       

            BanStorage.UnbanComputer(computer2);

            Assert.False(computer2.Banned);
        }


        [Test]
        public void BanRoomTest()
        {
            //Create computers with fake ip-address;
            Computer computer1 = new Computer { Banned = false, IpAddress = "999.999.999.997" };
            Computer computer2 = new Computer { Banned = true, IpAddress = "888.888.888.888" };

            Room room = new Room { Name = "Some room", Id = "Some room".GetHashCode() };


            //At the moment of developing this test attach method wasn't implemented;
            try
            {
                BanStorage.AttachComputerToRoom(computer1, room);
                BanStorage.AttachComputerToRoom(computer2,room);
            }
            catch (NotImplementedException)
            {

            }

            BanStorage.CreateRoom(room);

            BanStorage.BanRoom(room);

            Assert.False(BanStorage.GetRoom("Some room").Allowed);

            Assert.True(BanStorage.GetRoom("Some room").Computers.All(c =>c.Banned));
        }


        [Test]
        public void UnbanRoomTest()
        {
            Room room = new Room { Name = "Some Room", Id = "Some Room".GetHashCode(),Allowed = false};

            BanStorage.CreateRoom(room);

            //Create computers with fake ip-address;
            Computer computer1 = new Computer { Banned = true, IpAddress = "999.999.999.998" };


            //At the moment of developing this test attach method wasn't implemented;
            try
            {
                BanStorage.AttachComputerToRoom(computer1, room);
            }
            catch (NotImplementedException)
            {

            }

            BanStorage.UnbanRoom(room);

            Assert.True(BanStorage.GetRoom(room.Name).Allowed);

            Assert.True(room.Allowed);


            //Check if computers in this room are banned;
            //The testing method failed at this place;
            Assert.True(BanStorage.GetRoom(room.Name).Computers.All(c => c.Banned == false));
        }


        [Test]
        public void CreateComputerTest()
        {
            //Create computer with fake ip-address;
            Computer computer = new Computer { Banned = false, IpAddress = "999.999.999.999" };

            BanStorage.CreateComputer(computer);

            Assert.True(BanStorage.GetComputers().Contains(computer));
        }


        [Test]
        public void CreateRoomTest()
        {
            Room room = new Room { Name = "Some room", Id = "Some room".GetHashCode() };

            BanStorage.CreateRoom(room);

            Assert.True(BanStorage.GetRooms().Contains(room));
        }


        [Test]
        public void DeleteComputerTest()
        {
            //Create computer with fake ip-address;
            Computer computer = new Computer { Banned = false, IpAddress = "999.999.999.989" };

            BanStorage.CreateComputer(computer);

            BanStorage.DeleteComputer(computer);

            Assert.False(BanStorage.GetComputers().Contains(computer));
        }


        [Test]
        public void DeleteRoomTest()
        {
            Room room = new Room { Name = "Some room 1", Id = "Some room 1".GetHashCode() };

            BanStorage.CreateRoom(room);

            BanStorage.DeleteRoom(room);

            Assert.False(BanStorage.GetRooms().Contains(room));
        }
    }
}
