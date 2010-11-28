using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.UM.Models;

namespace IUDICO.UM.Controllers
{
    public class BaseController : Controller
    {
        private static readonly DataStorage storage;

        static BaseController()
        {
            storage = new DataStorage();
        }

        protected DataStorage Storage
        {
            get
            {
                return storage;
            }
        }
    }
}
