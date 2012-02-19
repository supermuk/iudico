using System;
using System.IO;
using System.Linq;
using System.Reflection;
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
    internal class StartUpTests
    {
        /// <summary>
        /// Initializes Windsor container
        /// NOTE: IN CASE OF CHANGING PATH OF THIS PROJECT YOU SHOULD ASSIGN CORRECT PATH TO THE VARIABLE NAMED "fullPath"
        /// </summary>
        /// <param name="_Container"></param>
        private static void InitializeWindsor(ref IWindsorContainer _Container)
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
        public void WindsorIsNotNull()
        {
            IWindsorContainer container = new WindsorContainer();
            InitializeWindsor(ref container);
            Assert.AreNotEqual(container, null, "Windsor is not initialized");
        }

        [Test]
        public void WindsorCanResolveLmsService()
        {
            IWindsorContainer container = new WindsorContainer();
            InitializeWindsor(ref container);
            try
            {
                ILmsService lms = container.Resolve<ILmsService>();
            }
            catch (Exception e)
            {
                Assert.Fail("LmsService instance could not be resolved from Windsor container");
            }
        }

        [Test]
        public void PluginsCanBeResolvedFromWindsor()
        {
            IWindsorContainer container = new WindsorContainer();
            InitializeWindsor(ref container);
            var cnt = container.ResolveAll<IPlugin>().Count(item => item != null);
                //count of somehow initialized plugins
            Assert.AreNotEqual(cnt, 0, "Windsor container could not plugins have resolved");
        }

        /*
         * 
            IWindsorContainer container = new WindsorContainer();
            InitializeWindsor(ref container);
            Common.Log4NetLoggerService.InitLogger();
            Assembly a = Assembly.GetExecutingAssembly();
            string fullPath = a.CodeBase;
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.Combine(fullPath, "IUDICO.LMS", "log.xml");
            ILmsService service = container.Resolve<ILmsService>();
            fullPath = fullPath.Remove(0, 6);
            Common.Log4NetLoggerService.InitLogger();
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(fullPath));
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(ILmsService));
            service.Inform(LMSNotifications.ApplicationStart);
            log.Info("WTF");
            Assert.Pass();
         */
    }
}