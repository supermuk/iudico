using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Controllers;
using System.Web.Mvc;
using IUDICO.Common.Models.Shared;
using IUDICO.Security.ViewModels.Ban;
using IUDICO.Security.Models.Storages;
using IUDICO.Common.Models;

namespace IUDICO.Security.Controllers
{
    public class BanController : PluginController
    {
        private readonly IBanStorage _BanStorage;

        public BanController(IBanStorage banStorage)
        {
            _BanStorage = banStorage;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddComputers()
        {
            return View(new AddComputerViewModel());
        }

        [HttpPost]
        public ActionResult AddComputers(AddComputerViewModel viewModel)
        {
            if (!String.IsNullOrEmpty(viewModel.ComputerIP))
            {
                var newComputer = new Computer
                {
                    Banned = false,
                    IpAddress = viewModel.ComputerIP
                };

                _BanStorage.CreateComputer(newComputer);
            }
            return View(viewModel);
        }

        public ActionResult AddRoom()
        {
            return View(new AddRoomViewModel());
        }

        [HttpPost]
        public ActionResult AddRoom(AddRoomViewModel viewModel)
        {
            if (!String.IsNullOrEmpty(viewModel.Name))
            {
                var newRoom = new Room
                {
                    Name = viewModel.Name,
                    Allowed = viewModel.Allowed
                };

                _BanStorage.CreateRoom(newRoom);
            }
            return View(viewModel);
        }

        public ActionResult EditComputer()
        {
            return View(new EditComputersViewModel());
        }

        public ActionResult EditComputer(String computer)
        {
            var comp = _BanStorage.GetComputer(computer);
            var ViewModel = new EditComputersViewModel(comp.IpAddress, comp.Room.Name, comp.Banned, comp.CurrentUser);
            return View(ViewModel);
        }

        public ActionResult EditRoom()
        {
            var viewModel = new RoomsViewModel();
            viewModel.Rooms = _BanStorage.GetRooms().Select(i => i.Name).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditRoom(String CurrentRoom)
        {
            var viewModel = new RoomsViewModel();
            viewModel.CurrentRoom = CurrentRoom;
            viewModel.Rooms = _BanStorage.GetRooms().Select(i => i.Name).ToList();
            viewModel.Computers = _BanStorage.GetRoom(CurrentRoom).Computers.Select(c => c.IpAddress).ToList();
            viewModel.UnchoosenComputers = _BanStorage.GetComputers().Where(i => i.Room == null).Select(x => x.IpAddress).ToList();

            return View(viewModel);
        }

        //[HttpPost]
        //public ActionResult EditRoom(RoomsViewModel Model)
        //{
        //    var room = new Room();
        //    room = _BanStorage.GetRoom(Model.CurrentRoom);
        //    foreach(String comp in Model.Computers)
        //    {
        //        var computer = _BanStorage.GetComputer(comp);
        //        if (computer.Room == null)
        //            _BanStorage.AttachComputerToRoom(computer, room);
        //    }

        //    foreach (String comp in Model.UnchoosenComputers)
        //    {
        //        var computer = _BanStorage.GetComputer(comp);
        //        if (computer.Room != null)
        //            _BanStorage.DetachComputer(computer);
        //    }
        //    return View(Model);
        //}

        public ActionResult BanComputer()
        {
            var viewModel = new BanComputerViewModel();
            viewModel.Computers = _BanStorage.GetComputers().ToList();

            return View(viewModel);
        }

        public ActionResult ComputerBan(String computer)
        {
            _BanStorage.BanComputer(_BanStorage.GetComputer(computer));
            return RedirectToAction("BanComputer");            
        }

        public ActionResult ComputerUnban(String computer)
        {
            _BanStorage.UnbanComputer(_BanStorage.GetComputer(computer));
            return RedirectToAction("BanComputer");   
        }

        public ActionResult BanRoom()
        {
            var viewModel = new BanRoomViewModel();
            viewModel.Rooms = _BanStorage.GetRooms().ToList();

            return View("BanRoom", viewModel);
        }

        public ActionResult RoomBan(String room)
        { 
            var viewModel = new BanRoomViewModel();
            _BanStorage.BanRoom(_BanStorage.GetRoom(room));
            viewModel.Rooms = _BanStorage.GetRooms().ToList();
            return View("BanRoom", viewModel);
        }

        public ActionResult RoomUnban(String room)
        {
            var viewModel = new BanRoomViewModel();
            _BanStorage.UnbanRoom(_BanStorage.GetRoom(room));
            viewModel.Rooms = _BanStorage.GetRooms().ToList();
            return View("BanRoom", viewModel);
        }

        public ActionResult DeleteRoom(String room)
        {
            var viewModel = new BanRoomViewModel();
            _BanStorage.DeleteRoom(_BanStorage.GetRoom(room));
            viewModel.Rooms = _BanStorage.GetRooms().ToList();
            return View("BanRoom", viewModel);
        }

        public ActionResult DeleteComputer(String computer)
        {   
            _BanStorage.DeleteComputer(_BanStorage.GetComputer(computer));
            return RedirectToAction("BanComputer");
        }
    }
}