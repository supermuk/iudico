using System.IO;
using System.Reflection;

using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Services;
using IUDICO.LMS.Models;

using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository.Hierarchy;

using NUnit.Framework;

using Logger = IUDICO.Common.Logger;

namespace IUDICO.UnitTests.LMS.NUnit
{
    [TestFixture]
    internal class LoggerTests
    {
        // <summary>
        /// Initializes Windsor container
        /// NOTE: IN CASE OF CHANGING PATH OF THIS PROJECT YOU SHOULD ASSIGN CORRECT PATH TO THE VARIABLE NAMED "fullPath"
        /// </summary>
        /// <param name="_Container"></param>
        private static void InitializeWindsor(ref IWindsorContainer container)
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
        public void IsLoggerObjectExists()
        {
            var ident = false;
            var logger = Logger.Instance;

            if (logger == null)
            {
                ident = false;
            }
            else
            {
                ident = true;
            }

            Assert.AreEqual(ident, true);
        }

        [Test]
        public void LmsGetLoggedApplicationStop()
        {
            var a = Assembly.GetExecutingAssembly();
            var fullPath = a.CodeBase;
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.Combine(fullPath, "IUDICO.LMS", "log.xml");
            ILmsService service = new LmsService(new WindsorContainer());
            fullPath = fullPath.Remove(0, 6);
            XmlConfigurator.Configure(new FileInfo(fullPath));
            var log = LogManager.GetLogger(typeof(ILmsService));
            service.Inform(LMSNotifications.ApplicationStop);
            var rootAppender = (FileAppender)((Hierarchy)LogManager.GetRepository()).Root.Appenders[0];
            fullPath = rootAppender.File;
            rootAppender.Close();
            var reader = new StreamReader(fullPath);
            var toRead = reader.ReadToEnd();
            Assert.IsTrue(toRead.IndexOf("Notification:application/stop") != -1);
        }
    }
}