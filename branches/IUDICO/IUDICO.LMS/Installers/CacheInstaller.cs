using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models.Caching.Provider;

namespace IUDICO.LMS.Installers
{
    public class CacheInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ICacheProvider>()
                    .ImplementedBy<HttpCache>().LifeStyle.Singleton);
        }
    }
}