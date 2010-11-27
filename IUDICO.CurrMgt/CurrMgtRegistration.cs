using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcContrib.PortableAreas;

namespace IUDICO.CurrMgt
{
    public class CurrMgtRegistration : PortableAreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            context.MapRoute(
                "Stage",
                "Stage/{StageId}/{action}",
                new { controller = "Stage" }
            );

            context.MapRoute(
                "Stages",
                "Curriculum/{CurriculumId}/Stage/{action}",
                new { controller = "Stage", CurriculumId = 0 }
            );

            context.MapRoute(
                "Curriculum",
                "Curriculum/{CurriculumID}/{action}",
                new { controller = "Curriculum" }
            );

            context.MapRoute(
                "Curriculums",
                "Curriculum/{action}",
                new { controller = "Curriculum" }
            );
        }

        public override string AreaName
        {
            get { return "CurrMgt"; }
        }
    }
}