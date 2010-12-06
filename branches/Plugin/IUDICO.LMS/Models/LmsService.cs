using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Services;
using Castle.Windsor;

namespace IUDICO.LMS.Models
{
    public class LmsService: ILmsService
    {
        protected IWindsorContainer container
        {
            get
            {
                return (HttpContext.Current.ApplicationInstance as MvcApplication).Container;
            }
        }

        public void Inform(string evt, params object[] data)
        {
            IService[] services = container.ResolveAll<IService>();

            foreach(IService service in services)
            {
                service.Update(evt, data);
            }
        }
    }
}