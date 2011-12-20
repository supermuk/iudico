using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.Common.Models.Shared;
using IUDICO.Security.ViewModels.Ban;
using NUnit.Framework;

namespace IUDICO.Security.UnitTests.Tests
{
    [TestFixture]
    class ViewModelsTester
    {
        [Test]
        public void RoomsViewModelTest()
        {
            List<string> rooms = new List<string> {"room1", "room2", "room3"};
            string currentRoom = "room1";

            List<string> computers = new List<string> {"computer1", "computer2", "computer3", "computer4"};

            List<string> unchosenComputers = new List<string> {"comuter3"};

            RoomsViewModel roomsViewModel = new RoomsViewModel
                                                {
                                                    Computers = computers,
                                                    CurrentRoom = currentRoom,
                                                    Rooms = rooms,
                                                    UnchoosenComputers = unchosenComputers
                                                };

            Assert.AreEqual(3,roomsViewModel.Rooms.Count);
            Assert.True(roomsViewModel.Rooms.Contains("room2"));
            Assert.AreEqual(4,roomsViewModel.Computers.Count);
            Assert.False(roomsViewModel.Computers.Contains("computer9"));
        }

        [Test]
        public void EditComputersViewModel()
        {
            ViewModels.Ban.EditComputersViewModel editComputersViewModel = new EditComputersViewModel("999.999.888.777","room1",false,"user1");

            EditComputersViewModel viewModel = new EditComputersViewModel
                                                   {
                                                       Banned = true,
                                                       ComputerIP = "888.777.777.888",
                                                       CurrentUser = "user2",
                                                       Room = "room1"
                                                   };

            Assert.AreEqual(editComputersViewModel.Room,viewModel.Room);
        }

        [Test]
        public void BanRoomViewModelTest()
        {
            BanRoomViewModel banRoomViewModel1 = new BanRoomViewModel();

            Assert.AreEqual(0,banRoomViewModel1.Rooms.Count);

            List<Room> rooms = new List<Room>
                                   {
                                       new Room {Allowed = true, Id = 1, Name = "room1"},
                                       new Room {Allowed = false, Id = 2, Name = "room2"}
                                   };

            BanRoomViewModel banRoomViewModel2 = new BanRoomViewModel {Rooms = rooms};

            Assert.AreEqual(2,banRoomViewModel2.Rooms.Count);

            Assert.True(banRoomViewModel2.Rooms.Count(r => r.Name == "room2") == 1);
        }

        [Test]
        public void BanComputerViewModelTest()
        {
            List<Computer> computers = new List<Computer>
                                           {
                                               new Computer
                                                   {
                                                       Banned = false,
                                                       CurrentUser = "user1",
                                                       IpAddress = "999.998.997.996"
                                                   },
                                               new Computer
                                                   {Banned = true, CurrentUser = "user2", IpAddress = "889.888.887.886"}
                                           };

            BanComputerViewModel banComputerViewModel = new BanComputerViewModel {Computers = computers};

            Assert.AreEqual(2,banComputerViewModel.Computers.Count);

            Assert.True(banComputerViewModel.Computers.Count(c => c.Banned) == 1);
        }
    }
}
