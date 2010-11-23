using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebEditor.Models;
using WebEditor.Models.Storage;

namespace WebEditor
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.CustomMapRoute(
                "Node",
                "Course/{CourseID}/Node/{NodeID}/{action}",
                new { controller = "Node", CourseID = 0 }
            );

            routes.CustomMapRoute(
                "Nodes",
                "Course/{CourseID}/Node/{action}",
                new { controller = "Node", CourseID = 0 }
            );

            routes.CustomMapRoute(
                "Course",
                "Course/{CourseID}/{action}",
                new { controller = "Course" }
            );

            routes.CustomMapRoute(
                "Courses",
                "Course/{action}",
                new { controller = "Course" }
            );

            routes.CustomMapRoute(
                "Stage",
                "Stage/{StageId}/{action}",
                new { controller = "Stage" }
            );

            routes.CustomMapRoute(
                "Stages",
                "Curriculum/{CurriculumId}/Stage/{action}",
                new { controller = "Stage", CurriculumId = 0 }
            );

            routes.CustomMapRoute(
                "Curriculum",
                "Curriculum/{CurriculumID}/{action}",
                new { controller = "Curriculum" }
            );

            routes.CustomMapRoute(
                "Curriculums",
                "Curriculum/{action}",
                new { controller = "Curriculum" }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            Application["Storage"] = StorageFactory.CreateStorage(StorageType.Mixed);
        }
    }
}