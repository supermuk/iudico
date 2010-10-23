using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebEditor.Models;

namespace WebEditor.Controllers
{
    public class BaseController: Controller
    {
        protected ButterflyDB db = ButterflyDB.Instance;
    }
}