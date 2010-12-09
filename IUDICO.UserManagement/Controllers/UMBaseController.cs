using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Controllers;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement.Controllers
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
