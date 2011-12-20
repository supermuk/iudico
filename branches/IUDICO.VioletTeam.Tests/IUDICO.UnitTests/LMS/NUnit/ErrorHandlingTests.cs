using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.LMS.Models;
using Moq;
using NUnit.Framework;
using System.Web.SessionState;
namespace IUDICO.UnitTests.LMS
{
    [TestFixture]
    class ErrorHandlingTests
    {
        private void InitializeWindsor(ref IWindsorContainer _Container)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            string fullPath = a.CodeBase;
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.Combine(fullPath, "IUDICO.LMS", "Plugins");
            fullPath = fullPath.Remove(0, 6);
            _Container
                .Register(
                    Component.For<IWindsorContainer>().Instance(_Container))
                .Register(
                    Component.For<ILmsService>().ImplementedBy<LmsService>().LifeStyle.Singleton)
                .Install(FromAssembly.This(),
                         FromAssembly.InDirectory(new AssemblyFilter(fullPath, "IUDICO.*.dll"))
            );
        }
        [Test]
        public void WindsorThrowsExceptionWhenCannotResolve()
        {
            WindsorContainer cont=new WindsorContainer();
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
            LmsService service=new LmsService(new WindsorContainer());
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
            IWindsorContainer cont=new WindsorContainer();
            InitializeWindsor(ref cont);
            ILmsService serv = cont.Resolve<ILmsService>();
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
            InitializeWindsor(ref cont);
            ILmsService serv = cont.Resolve<ILmsService>();
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
