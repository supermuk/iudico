using System.IO;
using System.Reflection;
using System.Web;

using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.LMS.Models;

using NUnit.Framework;

namespace IUDICO.UnitTests.LMS.NUnit
{
    [TestFixture]
    internal class ErrorHandlingTests
    {
        private void InitializeWindsor(ref IWindsorContainer container)
        {
            var a = Assembly.GetExecutingAssembly();
            var fullPath = a.CodeBase;
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.Combine(fullPath, "IUDICO.LMS", "Plugins");
            fullPath = fullPath.Remove(0, 6);
            container.Register(Component.For<IWindsorContainer>().Instance(container)).Register(
                Component.For<ILmsService>().ImplementedBy<LmsService>().LifeStyle.Singleton).Install(
                    FromAssembly.This(), 
                    FromAssembly.InDirectory(new AssemblyFilter(fullPath.Replace("Plugins", "bin"), "IUDICO.LMS.dll")), 
                    FromAssembly.InDirectory(new AssemblyFilter(fullPath, "IUDICO.*.dll")));
        }

        [Test]
        public void WindsorThrowsExceptionWhenCannotResolve()
        {
            var cont = new WindsorContainer();
            try
            {
                cont.Resolve<ILmsService>();
            }
            catch
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void LmsRemainsTellsWhenNoServiceFound()
        {
            var service = new LmsService(new WindsorContainer());
            try
            {
                service.FindService<IUserService>();
            }
            catch
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void LmsDoesNotThrowsExceptionWhenNoMenuFound()
        {
            HttpContext.Current = null;
            IWindsorContainer cont = new WindsorContainer();
            this.InitializeWindsor(ref cont);
            var serv = cont.Resolve<ILmsService>();
            var plugin = cont.Resolve<IPlugin>();
            try
            {
                plugin.BuildMenuItems();
            }
            catch
            {
                Assert.Fail();
            }

            Assert.Pass();
        }

        [Test]
        public void LmsDoesNotThrowsExceptionWhenNoActionsFound()
        {
            HttpContext.Current = null;
            IWindsorContainer cont = new WindsorContainer();
            this.InitializeWindsor(ref cont);
            var serv = cont.Resolve<ILmsService>();
            var plugin = cont.Resolve<IPlugin>();
            try
            {
                plugin.BuildActions();
            }
            catch
            {
                Assert.Fail();
            }

            Assert.Pass();
        }
    }
}