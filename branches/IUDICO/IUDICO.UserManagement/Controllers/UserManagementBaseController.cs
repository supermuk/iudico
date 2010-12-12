using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Controllers;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Services;
using IUDICO.Common.Models.Services;

namespace IUDICO.UserManagement.Controllers
{
    public class UserManagementBaseController : PluginController
    {
        protected IUserManagement Storage
        {
            get
            {
                return LmsService.FindService<IUserManagement>();
            }
        }
    }
}
