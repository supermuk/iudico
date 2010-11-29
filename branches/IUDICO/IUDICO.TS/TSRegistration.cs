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
                "Training",
                "Training/{packageID}/{attemptID}",
                new { controller = "Training", action = "Details", attemptID = UrlParameter.Optional },
                new { packageID = @"\d+" });

            context.MapRoute(
               "Trainings",
               "Training/{action}/{id}",
               new { controller = "Training", action = "Index", id = UrlParameter.Optional }
            );
           
            
            RegisterAreaEmbeddedResources();
        }

        public override string AreaName
        {
            get { return "TS"; }
        }
    }
}