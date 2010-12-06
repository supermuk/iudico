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
using IUDICO.Common.Models.Services;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;

namespace IUDICO.CourseManagment
{
    public class CourseManagmentService : ICourseManagment, IWindsorInstaller
    {
        protected IWindsorContainer container;

        #region IService Members
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

        public void Update(string evt, params object[] data)
        {
            // handle appropriate events
        }
        #endregion

        #region IWindsorInstaller Members
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            HttpContext.Current.Application["CourseStorage"] = CourseStorageFactory.CreateStorage(CourseStorageType.Mixed);

            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name))
                );

            this.container = container;
        }
        #endregion
    }
}