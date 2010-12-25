using System.Web.Routing;
using System.Collections.Generic;

namespace IUDICO.Common.Models.Plugin
{
    public interface IPlugin
    {
        IEnumerable<Action> BuildActions(Role role);
        void BuildMenu(Menu menu);
        
        void RegisterRoutes(RouteCollection routes);
        void Update(string evt, params object[] data);        
    }
}
