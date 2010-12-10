using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Controllers;
using IUDICO.CurriculumManagement.Models.Storage;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurrBaseController: BaseController
    {
        protected ICurriculumStorage Storage
        {
            get
            {
                return HttpContext.Application["CurrStorage"] as ICurriculumStorage;
            }
        }
    }
}