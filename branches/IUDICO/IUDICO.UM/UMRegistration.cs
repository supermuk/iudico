using System.Web;
using System.Web.Mvc;
using IUDICO.UM.Models.Storage;
using MvcContrib.PortableAreas;

namespace IUDICO.UM
{
    public class UMRegistration : PortableAreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            context.MapRoute(
                "Account",
                "Account/{action}",
                new { controller = "Account" }
            );

            context.MapRoute(
                "Group",
                "Group/{action}",
                new { controller = "Group" }
            );

            context.MapRoute(
                "Role",
                "Role/{action}",
                new { controller = "Role" }
            );

            RegisterAreaEmbeddedResources();
            
            HttpContext.Current.Application["UMStorage"] = UMStorageFactory.CreateStorage(UMStorageType.Database);
        }

        public override string AreaName
        {
            get { return "UM"; }
        }
    }
}