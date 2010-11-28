using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using MvcContrib.PortableAreas;

namespace IUDICO.TS
{
    public class TSRegistration : PortableAreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            context.MapRoute(
               "Trainings", // Route name
               "Training/{action}/{id}", // URL with parameters
               new { controller = "Training", action = "Index", id = UrlParameter.Optional } // Parameter defaults
           );
            
            RegisterAreaEmbeddedResources();
        }

        public override string AreaName
        {
            get { return "TS"; }
        }
    }
}