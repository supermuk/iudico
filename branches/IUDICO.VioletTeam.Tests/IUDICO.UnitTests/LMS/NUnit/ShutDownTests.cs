using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using IUDICO.Common.Models.Services;
using IUDICO.LMS;
using IUDICO.LMS.Controllers;
using IUDICO.LMS.Models;
using NUnit.Framework;

namespace IUDICO.UnitTests.LMS
{
    [TestFixture]
    class ShutDownTests
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
        public void ContainerIsBeingDesposedProperly()
        {
            IWindsorContainer container = new WindsorContainer();
            InitializeWindsor(ref container);
            ILmsService service = container.Resolve<ILmsService>();
            container.Dispose();
            Assert.That(container.ResolveAll<Object>().Count(item=>item!=null)==0);

        }
    }
}
