﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEditor.Models;
using WebEditor.Models.Storage;

namespace WebEditor.Controllers
{
    public class BaseController: Controller
    {
        protected ButterflyDB db = ButterflyDB.Instance;
        protected IStorageInterface Storage
        {
            get
            {
                return HttpContext.Application["Storage"] as IStorageInterface;
            }
        }

        
    }
}