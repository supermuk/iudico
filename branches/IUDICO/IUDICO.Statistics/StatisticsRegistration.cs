using System.Web;
using System.Web.Mvc;
using MvcContrib.PortableAreas;

namespace IUDICO.Statistics
{
    public class StatisticsRegistration : PortableAreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            context.MapRoute(
                "Statistics",
                "Statistics/{action}",
                new { controller = "Statistics" }
            );

            RegisterAreaEmbeddedResources();
        }

        public override string AreaName
        {
            get { return "Statistics"; }
        }
    }
}