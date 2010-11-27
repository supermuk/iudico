using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using MvcContrib.PortableAreas;
using MvcContrib.UI.InputBuilder.ViewEngine;

namespace IUDICO.CourseMgt
{
    public class CourseMgtRegistration : PortableAreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            context.Routes.MapRoute(
               "Packages", // Route name
               "Package/{action}/{id}", // URL with parameters
               new { controller = "Package", action = "Index", id = 1 } // Parameter defaults
           );
            
            RegisterAreaEmbeddedResources();

            //HttpContext.Current.Application["CourseStorage"] = CourseStorageFactory.CreateStorage(CourseStorageType.Mixed);
        }

        public override string AreaName
        {
            get { return "TS"; }
        }
    }
}