using System.Web.Routing;
using System.Collections.Generic;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models.Action;

namespace IUDICO.Common.Models.Plugin
{
    public interface IPlugin : IWindsorInstaller
    {
        string GetName();

        IEnumerable<IAction> BuildActions();
        void BuildMenu(Menu menu);

        void Setup(IWindsorContainer container);
        
        void RegisterRoutes(RouteCollection routes);
        void Update(string evt, params object[] data);
    }
}