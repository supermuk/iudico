using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IUDICO.LMS.IoC
{
    public class PluginViewEngine : WebFormViewEngine
    {
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return base.FindPartialView(controllerContext, partialViewName, useCache);
            /*
            if (result == null || result.View == null)
            {
                try
                {
                    result = base.FindPartialView(controllerContext, "~/Plugins/IUDICO.CourseManagement.dll/IUDICO.CourseManagement/Views/Shared/" + partialViewName + ".ascx", useCache);
                }
                catch
                {
                    result = null;
                }
            }

            return result;*/
        }
    }
}