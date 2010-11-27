using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcContrib.PortableAreas;

namespace IUDICO.Search
{
    public class SearchRegistration : PortableAreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            context.MapRoute(
                "Search",
                "Search/{action}",
                new { controller = "Search" }
            );

            RegisterAreaEmbeddedResources();
        }

        public override string AreaName
        {
            get { return "Search"; }
        }
    }
}