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
        public void IsLoggerObjectExists()
        {
            bool ident = false;
            Logger logger = Logger.Instance;

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
            Assembly a = Assembly.GetExecutingAssembly();
            string fullPath = a.CodeBase;
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.Combine(fullPath, "IUDICO.LMS", "log.xml");
            ILmsService service = new LmsService(new WindsorContainer());
            fullPath = fullPath.Remove(0, 6);
            XmlConfigurator.Configure(new FileInfo(fullPath));
            ILog log = LogManager.GetLogger(typeof (ILmsService));
            service.Inform(LMSNotifications.ApplicationStop);
            FileAppender rootAppender = (FileAppender) ((Hierarchy) LogManager.GetRepository()).Root.Appenders[0];
            fullPath = rootAppender.File;
            rootAppender.Close();
            StreamReader reader = new StreamReader(fullPath);
            string toRead = reader.ReadToEnd();
            Assert.IsTrue(toRead.IndexOf("Notification:application/stop") != -1);
        }
    }
}