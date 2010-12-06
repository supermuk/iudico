using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.CourseManagment.Models;
using IUDICO.CourseManagment.Models.Storage;
using IUDICO.Common.Models;
using IUDICO.Common.Controllers;

namespace IUDICO.CourseManagment.Controllers
{
    public class CourseBaseController : PluginController
    {
        protected ICourseStorage Storage
        {
            get
            {
                return HttpContext.Application["CourseStorage"] as ICourseStorage;
            }
        }
    }
}