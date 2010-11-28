﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.CurrMgt.Models.Storage;
using MvcContrib.PortableAreas;

namespace IUDICO.CurrMgt
{
    public class CurrMgtRegistration : PortableAreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            context.MapRoute(
                "Curriculum",
                "Curriculum/{CurriculumID}/{action}",
                new { controller = "Curriculum", action = "Index", CurriculumId = 0 }
            );

            context.MapRoute(
                "Curriculums",
                "Curriculum/{action}",
                new { controller = "Curriculum", action = "Index" }
            );

            context.MapRoute(
                "Stage",
                "Stage/{StageId}/{action}",
                new { controller = "Stage", action = "Index", StageId = 0 }
            );

            context.MapRoute(
                "Stages",
                "Curriculum/{CurriculumId}/Stage/{action}",
                new { controller = "Stage", action = "Index", CurriculumId = 0 }
            );

            RegisterAreaEmbeddedResources();

            HttpContext.Current.Application["CurrStorage"] = CurriculumStorageFactory.CreateStorage(CurriculumStorageType.Mixed);
        }

        public override string AreaName
        {
            get { return "CurrMgt"; }
        }
    }
}