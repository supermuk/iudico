using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Services;

namespace IUDICO.Common.Models
{
    public class LMS
    {
        protected Dictionary<Type, IService> services = new Dictionary<Type,IService>();

        private static LMS instance = new LMS();
        public static LMS Instance
        {
            get
            {
                return instance;
            }
        }

        private LMS()
        {

        }

        public void RegisterService(IService service)
        {
            IService existingService;

            if (services.TryGetValue(service.GetType(), out existingService))
            {
                // TODO: log: service already exists

                existingService = service;
            }
            else
            {
                services.Add(service.GetType(), service);
            }
        }

        public void Inform(string evt, params object[] data)
        {
            
        }

        public IService GetService(Type type)
        {
            IService existingService;

            if (services.TryGetValue(type, out existingService))
            {
                return existingService;
            }

            // return fake
            return null;
        }
    }
}