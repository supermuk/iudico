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
        readonly IKernel _kernel;

        public IocControllerFactory(IKernel kernel)
        {
            this._kernel = kernel;
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
                return _kernel.Resolve<IController>(controllerName + "Controller");
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

            _kernel.ReleaseComponent(controller);
        }
    }
}