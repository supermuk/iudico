using System.Web.Routing;

namespace IUDICO.Common.Models.Plugin
{
    public interface IPlugin
    {
        void RegisterRoutes(RouteCollection routes);
        void Update(string evt, params object[] data);
    }
}
