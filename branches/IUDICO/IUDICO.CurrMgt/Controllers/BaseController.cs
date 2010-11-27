using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Controllers;
using IUDICO.CurrMgt.Models.Storage;

namespace IUDICO.CurrMgt.Controllers
{
    public class CurrBaseController: Controller
    {
        protected ICurrStorage Storage
        {
            get
            {
                return HttpContext.Application["CurrStorage"] as ICurrStorage;
            }
        }
    }
}