using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace IUDICO.Common.Models.Services
{
    public interface IService
    {
        void RegisterRoutes(RouteCollection routes);
        void Update(string evt, params object[] data);
    }
}
