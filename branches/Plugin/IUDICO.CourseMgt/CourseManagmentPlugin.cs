using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using IUDICO.CourseManagment.Models.Storage;
using IUDICO.Common;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;

namespace IUDICO.CourseManagment
{
    public class CourseManagmentPlugin : IIudicoPlugin, ICourseManagment
    {
        protected LMS lms;

        #region IIudicoPlugin Members
        public void Initialize(LMS lms)
        {
            this.lms = lms;
            HttpContext.Current.Application["CourseStorage"] = CourseStorageFactory.CreateStorage(CourseStorageType.Mixed);
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Node",
                "Course/{CourseID}/Node/{NodeID}/{action}",
                new { controller = "Node", CourseID = 0 }
            );

            routes.MapRoute(
                "Nodes",
                "Course/{CourseID}/Node/{action}",
                new { controller = "Node", action = "Index", CourseID = 0 }
            );

            routes.MapRoute(
                "Course",
                "Course/{CourseID}/{action}",
                new { controller = "Course" }
            );

            routes.MapRoute(
                "Courses",
                "Course/{action}",
                new { controller = "Course", action = "Index" }
            );
        }

        public IService GetService()
        {
            return this;
        }
        #endregion

        #region IService members
        public void Update(string evt, params object[] data)
        {
            // handle appropriate events
        }
        #endregion
    }
}