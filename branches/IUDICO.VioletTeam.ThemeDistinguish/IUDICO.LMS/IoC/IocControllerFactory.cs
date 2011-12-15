﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace IUDICO.LMS.IoC
{
    public class IocControllerFactory : IControllerFactory
    {
        readonly IKernel _Kernel;

        public IocControllerFactory(IKernel kernel)
        {
            _Kernel = kernel;
        }

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
                return _Kernel.Resolve<IController>(controllerName + "controller");
            }
            catch (ComponentNotFoundException e)
            {
                // TODO: log not found
                throw  new HttpException(404, string.Format("No controller with name '{0}' found", controllerName), e);
            }
        }

        public void ReleaseController(IController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            _Kernel.ReleaseComponent(controller);
        }
    }
}