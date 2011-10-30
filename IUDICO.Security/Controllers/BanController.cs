using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Controllers;
using System.Web.Mvc;
using IUDICO.Security.ViewModels.Ban;
using IUDICO.Security.Models.Storages;

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
            return View(viewModel);
        }

        public ActionResult AddRoom()
        {
            return View(new AddRoomViewModel());
        }

        [HttpPost]
        public ActionResult AddRoom(AddRoomViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {
                ModelState.AddModelError("Error", "Error");
                return View(viewModel);
            }
        }

        public ActionResult EditComputer()
        {
            return View();
        }

        public ActionResult EditRoom()
        {   
            return View(new RoomsViewModel());
        }
    }
}