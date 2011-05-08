using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IUDICO.LMS
{
    public class PluginViewEngine : WebFormViewEngine
    {
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var result = base.FindPartialView(controllerContext, partialViewName, useCache);

            if (result == null || result.View == null)
            {
                //result = base.FindPartialView(controllerContext, "~/Plugins/IUDICO.CourseManagment.dll/IUDICO.CourseManagment/Views/Shared/" + partialViewName + ".ascx", useCache);
            }

            return result;
        }
    }
}