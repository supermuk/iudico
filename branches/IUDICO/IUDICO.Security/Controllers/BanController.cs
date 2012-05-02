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
using IUDICO.Common.Models.Attributes;

namespace IUDICO.Security.Controllers
{
    public class BanController : PluginController
    {
        public readonly IBanStorage BanStorage;

        public BanController(IBanStorage banStorage)
        {
            this.BanStorage = banStorage;
        }

        [Allow(Role = Role.Admin)]
        public ActionResult Index()
        {
            return View();
        }

        [Allow(Role = Role.Admin)]
        public ActionResult AddComputers()
        {
            return View(new AddComputerViewModel());
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult AddComputers(AddComputerViewModel viewModel)
        {
            viewModel.State = Models.ViewModelState.Edit;
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(viewModel.ComputerIP))
                {
                    var newComputer = new Computer
                    {
                        Banned = false,
                        IpAddress = viewModel.ComputerIP
                    };

                    this.BanStorage.CreateComputer(newComputer);
                }
                viewModel.State = Models.ViewModelState.View;
            }

            return View(viewModel);
        }

        [Allow(Role = Role.Admin)]
        public ActionResult AddRoom()
        {
            return View(new AddRoomViewModel());
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult AddRoom(AddRoomViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(viewModel.Name))
            {
                var newRoom = new Room
                {
                    Name = viewModel.Name,
                    Allowed = viewModel.Allowed
                };

                this.BanStorage.CreateRoom(newRoom);
            }
            return View(viewModel);
        }


        public ActionResult EditComputer(EditComputersViewModel viewModel)
        {
            var comp = this.BanStorage.GetComputer(viewModel.ComputerIP);

            this.BanStorage.DeleteComputer(comp);

            this.BanStorage.CreateComputer(new Computer
                                               {
                                                   IpAddress = viewModel.ComputerIP, Banned = viewModel.Banned, CurrentUser = viewModel.CurrentUser
                                               } );           

            var viewModel1 = new EditComputersViewModel(
                    comp.IpAddress,
                    (comp.Room != null) ? comp.Room.Name : "N/A",
                    comp.Banned,
                    comp.CurrentUser);

            return View(viewModel1);
        }

        [Allow(Role = Role.Admin)]
        public ActionResult EditRoom()
        {
            var viewModel = new RoomsViewModel();
            viewModel.Rooms = this.BanStorage.GetRooms().Select(i => i.Name).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult EditRoom(string currentRoom)
        {
            var viewModel = new RoomsViewModel();
            viewModel.CurrentRoom = currentRoom;
            viewModel.Rooms = this.BanStorage.GetRooms().Select(i => i.Name).ToList();
            viewModel.Computers = this.BanStorage.GetRoom(currentRoom).Computers.Select(c => c.IpAddress).ToList();
            viewModel.UnchoosenComputers = this.BanStorage.GetComputers().Where(i => i.Room == null).Select(x => x.IpAddress).ToList();

            return View(viewModel);
        }

        // [HttpPost]
        // public ActionResult EditRoom(RoomsViewModel Model)
        // {
        //    var room = new Room();
        //    room = BanStorage.GetRoom(Model.CurrentRoom);
        //    foreach(string comp in Model.Computers)
        //    {
        //        var computer = BanStorage.GetComputer(comp);
        //        if (computer.Room == null)
        //            BanStorage.AttachComputerToRoom(computer, room);
        //    }

        // foreach (string comp in Model.UnchoosenComputers)
        //    {
        //        var computer = BanStorage.GetComputer(comp);
        //        if (computer.Room != null)
        //            BanStorage.DetachComputer(computer);
        //    }
        //    return View(Model);
        //  }

        [Allow(Role = Role.Admin)]
        public ActionResult BanComputer()
        {
            var viewModel = new BanComputerViewModel();
            viewModel.Computers = this.BanStorage.GetComputers().ToList();

            return View(viewModel);
        }

        [Allow(Role = Role.Admin)]
        public ActionResult ComputerBan(string computer)
        {
            this.BanStorage.BanComputer(this.BanStorage.GetComputer(computer));
            return RedirectToAction("BanComputer");
        }

        [Allow(Role = Role.Admin)]
        public ActionResult ComputerUnban(string computer)
        {
            this.BanStorage.UnbanComputer(this.BanStorage.GetComputer(computer));
            return RedirectToAction("BanComputer");   
        }

        [Allow(Role = Role.Admin)]
        public ActionResult BanRoom()
        {
            var viewModel = new BanRoomViewModel();
            viewModel.Rooms = this.BanStorage.GetRooms().ToList();

            return View("BanRoom", viewModel);
        }

        [Allow(Role = Role.Admin)]
        public ActionResult RoomBan(string room)
        { 
            var viewModel = new BanRoomViewModel();
            this.BanStorage.BanRoom(this.BanStorage.GetRoom(room));
            viewModel.Rooms = this.BanStorage.GetRooms().ToList();
            return View("BanRoom", viewModel);
        }

        [Allow(Role = Role.Admin)]
        public ActionResult RoomUnban(string room)
        {
            var viewModel = new BanRoomViewModel();
            this.BanStorage.UnbanRoom(this.BanStorage.GetRoom(room));
            viewModel.Rooms = this.BanStorage.GetRooms().ToList();
            return View("BanRoom", viewModel);
        }

        [Allow(Role = Role.Admin)]
        public ActionResult DeleteRoom(string room)
        {
            var viewModel = new BanRoomViewModel();
            this.BanStorage.DeleteRoom(this.BanStorage.GetRoom(room));
            viewModel.Rooms = this.BanStorage.GetRooms().ToList();
            return View("BanRoom", viewModel);
        }

        [Allow(Role = Role.Admin)]
        public ActionResult DeleteComputer(string computer)
        {
            this.BanStorage.DeleteComputer(this.BanStorage.GetComputer(computer));
            return RedirectToAction("BanComputer");
        }

        public ActionResult Banned()
        {
            return View();
        }
    }
}