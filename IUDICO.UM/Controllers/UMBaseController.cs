using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Controllers;
using IUDICO.UM.Models;
using IUDICO.UM.Models.Storage;

namespace IUDICO.UM.Controllers
{
    public class UMBaseController : BaseController
    {
        protected IUMStorage Storage
        {
            get
            {
                return HttpContext.Application["UMStorage"] as IUMStorage;
            }
        }
    }
}
