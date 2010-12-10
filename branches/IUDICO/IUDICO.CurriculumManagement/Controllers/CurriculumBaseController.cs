using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models.Services;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumBaseController: PluginController
    {
        ICurriculumManagement storage;

        protected ICurriculumManagement Storage
        {
            get
            {
                return lmsService.FindService<ICurriculumManagement>();
            }
        }
    }
}