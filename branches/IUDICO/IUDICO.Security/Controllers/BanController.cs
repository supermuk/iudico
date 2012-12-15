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
            return View(new IndexViewModel());
        }

        [Allow(Role = Role.Admin)]
        [HttpGet]
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
                if (this.BanStorage.GetComputer(viewModel.ComputerIP) == null)
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
                }
                
                viewModel.State = Models.ViewModelState.View;
            }

            return RedirectToAction("EditComputers");
        }

        [HttpGet]
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
                if (this.BanStorage.GetRoom(viewModel.Name) == null)
                {
                    var newRoom = new Room { Name = viewModel.Name, Allowed = viewModel.Allowed };

                    this.BanStorage.CreateRoom(newRoom);
                }
            }
            return RedirectToAction("EditRooms", "Ban");
        }

        [HttpGet]
        [Allow(Role = Role.Admin)]
        public ActionResult EditComputer(string computerIp)
        {
            var computer = this.BanStorage.GetComputer(computerIp);

            var room = this.BanStorage.GetRoom(computer);

            var name = "N/A";

            if(room != null)
            {
                name = room.Name;
            }

            var viewModel = new EditComputersViewModel(computerIp, name, computer.Banned, computer.CurrentUser,
                                                       new List<string> {"N/A"}.Concat(
                                                           this.BanStorage.GetRooms().Select(r => r.Name)));
            
            return View(viewModel);
        }

        [Allow(Role = Role.Admin)]
        public ActionResult EditComputers()
        {
            var computers = this.BanStorage.GetComputers().ToList();

            var attachments = this.BanStorage.GetRoomAttachments().ToList();

            var list = new List<ComputerWithAttachmentViewModel>();

            foreach (var computer in computers)
            {
                if (attachments.SingleOrDefault(a => a.ComputerIp == computer.IpAddress) != null)
                {
                    var roomId = attachments.SingleOrDefault(a => a.ComputerIp == computer.IpAddress).RoomId;

                    var roomName = this.BanStorage.GetRoom(roomId).Name;

                    list.Add(new ComputerWithAttachmentViewModel { Computer = computer, RoomName = roomName });
                }

                else
                {
                    list.Add(new ComputerWithAttachmentViewModel { Computer = computer });
                }
            }

            var viewModel = new BanComputerViewModel { Computers = list };

            return View(viewModel);
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult EditComputer(EditComputersViewModel viewModel)
        {
            this.BanStorage.EditComputer(viewModel.ComputerIP, viewModel.Banned, viewModel.CurrentUser);

            if(viewModel.Room != "N/A")
            {
                this.BanStorage.AttachComputerToRoom(this.BanStorage.GetComputer(viewModel.ComputerIP), this.BanStorage.GetRoom(viewModel.Room));
            }
            else
            {
                this.BanStorage.DetachComputer(this.BanStorage.GetComputer(viewModel.ComputerIP));
            }

            return RedirectToAction("EditComputers");
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
            var attachments = this.BanStorage.GetRoomAttachments();

            var viewModel = new RoomsViewModel
                                {
                                    CurrentRoom = currentRoom,
                                    Rooms = this.BanStorage.GetRooms().Select(i => i.Name).ToList(),
                                    Computers = this.BanStorage.GetComputers().Select(c => c.IpAddress).ToList()
                                };
            viewModel.UnchoosenComputers = viewModel.Computers.Where(c => attachments.Count(a => a.ComputerIp == c) == 0).ToList();

            return View(viewModel);
        }

        [HttpGet]
        [Allow(Role = Role.Admin)]
        public void UpdateRoom(string currentRoom, string compArray, string unchoosenComputers)
        {
            var room = new Room();
            room = this.BanStorage.GetRoom(currentRoom);
            var computers = compArray.Split(',');
            foreach (string computer in computers)
            {
                var tempComp = this.BanStorage.GetComputer(computer);
   
                if(this.BanStorage.GetAttachment(computer) == null)
                {
                    this.BanStorage.AttachComputerToRoom(tempComp, room);
                }
            }
            computers = unchoosenComputers.Split(',');
            foreach (string comp in computers)
            {
                var computer = this.BanStorage.GetComputer(comp);

                if (this.BanStorage.GetAttachment(computer) != null)
                {
                    this.BanStorage.DetachComputer(computer);
                }
            }
        }

        [Allow(Role = Role.Admin)]
        public ActionResult ComputerBan(string computer)
        {
            this.BanStorage.BanComputer(this.BanStorage.GetComputer(computer));
            return RedirectToAction("EditComputers");
        }

        [Allow(Role = Role.Admin)]
        public ActionResult ComputerUnban(string computer)
        {
            this.BanStorage.UnbanComputer(this.BanStorage.GetComputer(computer));
            return RedirectToAction("EditComputers");   
        }

        [Allow(Role = Role.Admin)]
        public ActionResult EditRooms()
        {
            var viewModel = new BanRoomViewModel();
            viewModel.Rooms = this.BanStorage.GetRooms().ToList();

            return View(viewModel);
        }

        [Allow(Role = Role.Admin)]
        public ActionResult RoomBan(string room)
        { 
            this.BanStorage.BanRoom(this.BanStorage.GetRoom(room));
            return RedirectToAction("EditRooms", "Ban");
        }

        [Allow(Role = Role.Admin)]
        public ActionResult RoomUnban(string room)
        {
            this.BanStorage.UnbanRoom(this.BanStorage.GetRoom(room));
            return RedirectToAction("EditRooms", "Ban");
        }

        [Allow(Role = Role.Admin)]
        public ActionResult DeleteRoom(string room)
        {
            this.BanStorage.DeleteRoom(this.BanStorage.GetRoom(room));
            return RedirectToAction("EditRooms", "Ban");
        }

        [Allow(Role = Role.Admin)]
        public ActionResult DeleteComputer(string computer)
        {
            this.BanStorage.DeleteComputer(this.BanStorage.GetComputer(computer));
            return RedirectToAction("EditComputers");
        }

        public ActionResult Banned()
        {
            return View();
        }
    }
}