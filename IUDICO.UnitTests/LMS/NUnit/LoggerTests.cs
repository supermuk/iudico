using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using IUDICO.Common;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Services;
using IUDICO.LMS.Models;
using NUnit.Framework;
using System.IO;

namespace IUDICO.UnitTests.LMS.NUnit
{
    [TestFixture]
    class LoggerTests
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
        public void LmsGetLogged()
        {
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
            service.Inform(LMSNotifications.ApplicationStop);
            fullPath = Path.GetDirectoryName(fullPath);
            fullPath = Path.Combine(fullPath, "Data","Logs", "log4net.log");
            StreamReader reader=new StreamReader(fullPath);
            string toRead = reader.ReadToEnd();
            Assert.That(toRead.IndexOf("Notification:application/stop") != -1);
        }
    }
}