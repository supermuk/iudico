using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using IUDICO.Common.Models.Services;

namespace IUDICO.LMS.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IService>()
                    .Configure(c => c.LifeStyle.Transient.Named(c.Implementation.Name)));
        }
    }
}