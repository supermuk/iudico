using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.CourseMgt.Models;
using IUDICO.CourseMgt.Models.Storage;
using IUDICO.Common.Models;
using IUDICO.Common.Controllers;

namespace IUDICO.CourseMgt.Controllers
{
    public class CourseBaseController: BaseController
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