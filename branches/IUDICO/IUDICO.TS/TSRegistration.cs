using System.Web.Mvc;
using MvcContrib.PortableAreas;

namespace IUDICO.TS
{
    public class TSRegistration : PortableAreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            context.MapRoute(
               "Packages", // Route name
               "Package/{action}/{id}", // URL with parameters
               new { controller = "Package", action = "Index", id = 1 } // Parameter defaults
           );
            
            RegisterAreaEmbeddedResources();
        }

        public override string AreaName
        {
            get { return "TS"; }
        }
    }
}