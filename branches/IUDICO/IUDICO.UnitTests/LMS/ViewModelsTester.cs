namespace IUDICO.UnitTests.Security.NUnit
{
    using System.Collections.Generic;
    using System.Linq;

    using global::NUnit.Framework;

    using IUDICO.Common.Models.Shared;
    using IUDICO.Security.ViewModels.Ban;

    [TestFixture]
    internal class ViewModelsTester
    {
        [Test]
        public void RoomsViewModelTest()
        {
            var rooms = new List<string> { "room1", "room2", "room3" };
            var computers = new List<string> { "computer1", "computer2", "computer3", "computer4" };
            var unchosenComputers = new List<string> { "comuter3" };
            const string CurrentRoom = "room1";

            var roomsViewModel = new RoomsViewModel
                {
                    Computers = computers, 
                    CurrentRoom = CurrentRoom, 
                    Rooms = rooms, 
                    UnchoosenComputers = unchosenComputers
                };

            Assert.AreEqual(3, roomsViewModel.Rooms.Count);
            Assert.True(roomsViewModel.Rooms.Contains("room2"));
            Assert.AreEqual(4, roomsViewModel.Computers.Count);
            Assert.False(roomsViewModel.Computers.Contains("computer9"));
        }

        [Test]
        public void EditComputersViewModel()
        {
            var editComputersViewModel = new EditComputersViewModel("999.999.888.777", "room1", false, "user1");

            var viewModel = new EditComputersViewModel
                {
                   Banned = true, ComputerIP = "888.777.777.888", CurrentUser = "user2", Room = "room1" 
                };

            Assert.AreEqual(editComputersViewModel.Room, viewModel.Room);
        }

        [Test]
        public void BanRoomViewModelTest()
        {
            var banRoomViewModel1 = new BanRoomViewModel();

            Assert.AreEqual(0, banRoomViewModel1.Rooms.Count);

            var rooms = new List<Room> {
                    new Room { Allowed = true, Id = 1, Name = "room1" }, 
                    new Room { Allowed = false, Id = 2, Name = "room2" }
                };

            var banRoomViewModel2 = new BanRoomViewModel { Rooms = rooms };

            Assert.AreEqual(2, banRoomViewModel2.Rooms.Count);

            Assert.True(banRoomViewModel2.Rooms.Count(r => r.Name == "room2") == 1);
        }

        [Test]
        public void BanComputerViewModelTest()
        {
            var computers = new List<Computer> {
                    new Computer { Banned = false, CurrentUser = "user1", IpAddress = "999.998.997.996" }, 
                    new Computer { Banned = true, CurrentUser = "user2", IpAddress = "889.888.887.886" }
                };

            var banComputerViewModel = new BanComputerViewModel { Computers = computers };

            Assert.AreEqual(2, banComputerViewModel.Computers.Count);

            Assert.True(banComputerViewModel.Computers.Count(c => c.Banned) == 1);
        }
    }
}