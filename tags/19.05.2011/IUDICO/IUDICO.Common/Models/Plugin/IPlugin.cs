using System.Web.Routing;
using System.Collections.Generic;
using Castle.Windsor;

namespace IUDICO.Common.Models.Plugin
{
    public interface IPlugin
    {
        string GetName();

        IEnumerable<Action> BuildActions(Role role);
        void BuildMenu(Menu menu);

        void Setup(IWindsorContainer container);
        
        void RegisterRoutes(RouteCollection routes);
        void Update(string evt, params object[] data);
    }
}