using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using IUDICO.Common.Models.Services;

namespace IUDICO.Common.Models.Plugin
{
    public interface IIudicoPlugin
    {
        void Initialize(LMS lms);
        void RegisterRoutes(RouteCollection routes);

        IService GetService();
        //LMS GetLMS();
    }
}



/*

LMS.infom("lms/course-mgt/cource/delete", courceId)



update(event, data)
if event == 
*/