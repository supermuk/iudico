using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using IUDICO.Common.Models;

namespace IUDICO.Common.Controllers
{
    public class BaseController : Controller
    {
        private DB db = DB.Instance;
    }
}
