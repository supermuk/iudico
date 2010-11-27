using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using IUDICO.CourseMgt.Models.Storage;
using MvcContrib.PortableAreas;

namespace IUDICO.CourseMgt
{
    public class CourseMgtRegistration : PortableAreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            context.MapRoute(
                "Node",
                "Course/{CourseID}/Node/{NodeID}/{action}",
                new { controller = "Node", CourseID = 0 }
            );

            context.MapRoute(
                "Nodes",
                "Course/{CourseID}/Node/{action}",
                new { controller = "Node", CourseID = 0 }
            );

            context.MapRoute(
                "Course",
                "Course/{CourseID}/{action}",
                new { controller = "Course" }
            );

            context.MapRoute(
                "Courses",
                "Course/{action}",
                new { controller = "Course" }
            );

            RegisterAreaEmbeddedResources();

            HttpContext.Current.Application["CourseStorage"] = CourseStorageFactory.CreateStorage(CourseStorageType.Mixed);
        }

        public override string AreaName
        {
            get { return "CourseMgt"; }
        }
    }
}