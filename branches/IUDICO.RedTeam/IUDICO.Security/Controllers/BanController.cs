using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Controllers;
using System.Web.Mvc;
using IUDICO.Security.ViewModels;

namespace IUDICO.Security.Controllers
{
    public class BanController : PluginController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddComputers()
        {
            return View(new BanAddComputerViewModel());
        }

        [HttpPost]
        public ActionResult AddComputers(BanAddComputerViewModel viewModel)
        {
            return View(viewModel);
        }

        public ActionResult AddRoom()
        {
            return View(new BanAddRoomViewModel());
        }

        [HttpPost]
        public ActionResult AddRoom(BanAddRoomViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}