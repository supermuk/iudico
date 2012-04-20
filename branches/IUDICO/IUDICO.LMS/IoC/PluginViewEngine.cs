using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IUDICO.LMS.IoC
{
    public class PluginViewEngine : WebFormViewEngine
    {
        public PluginViewEngine() : base()
        {
            PartialViewLocationFormats = new[]
                                             {
                                                 "~/Plugins/IUDICO.DisciplineManagement.dll/IUDICO.DisciplineManagement/Views/{1}/{0}.ascx",
                                                 "~/Plugins/IUDICO.CourseManagement.dll/IUDICO.CourseManagement/Views/{1}/{0}.ascx",
                                                 "~/Plugins/IUDICO.CurriculumManagement.dll/IUDICO.CurriculumManagement/Views/{1}/{0}.ascx"
                                             };
            ViewLocationFormats = PartialViewLocationFormats;
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            bool res;

            try
            {
                res = base.FileExists(controllerContext, virtualPath);
            }
            catch (HttpException)
            {
                res = false;
            }

            return res;
        }
    }
}