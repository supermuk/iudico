using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models.Services;
using System.Collections;

namespace IUDICO.LMS.IoC
{
    public class IocControllerFactory : IControllerFactory
    {
        readonly IKernel kernel;

        public IocControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }
        /*
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;

            if (typeof(PluginController).IsAssignableFrom(controllerType))
            {
                return (IController)kernel.Resolve(controllerType, new { lmsService = kernel.Resolve<ILmsService>() });
                //return (IController)Activator.CreateInstance(controllerType, kernel.Resolve<ILmsService>());
            }
            else
            {
                return (IController)kernel.Resolve(controllerType);
                //return (IController)Activator.CreateInstance(controllerType);
            }
        }
        */
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }
            if (controllerName == null)
            {
                throw new ArgumentNullException("controllerName");
            }

            try
            {
                return kernel.Resolve<IController>(controllerName + "controller");
                /*
                IController controller = kernel.Resolve<IController>(controllerName + "controller");
                if (typeof(PluginController).IsAssignableFrom(controller.GetType()))
                {
//                    (controller as PluginController).
                }
                return controller;
                */
            }
            catch (ComponentNotFoundException e)
            {
                throw new ApplicationException(string.Format("No controller with name '{0}' found", controllerName), e);
            }
        }

        public void ReleaseController(IController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            kernel.ReleaseComponent(controller);
        }
    }
}