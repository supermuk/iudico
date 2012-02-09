using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.LMS.Models;
using NUnit.Framework;
using Action = IUDICO.Common.Models.Action;

namespace IUDICO.UnitTests.LMS.NUnit
{
    class FakePlugin:IPlugin
    {
        public string GetName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Action> BuildActions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            throw new NotImplementedException();
        }

        public void Setup(IWindsorContainer container)
        {
            throw new NotImplementedException();
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            throw new NotImplementedException();
        }

        public void Update(string evt, params object[] data)
        {
            throw new NotImplementedException();
        }
    }
     [TestFixture]
    class PluginsLoadingTests
    {
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
         public void TryLoadCorrectPlugin()
         {
             IWindsorContainer container=new WindsorContainer();
             InitializeWindsor(ref container);
             var plugins = container.ResolveAll<IPlugin>();
             foreach (var plugin in plugins)
             {
                 Assert.IsNotNull(plugin.BuildActions(),plugin.GetName());
                 Assert.IsNotNull(plugin.BuildMenuItems(), plugin.GetName());
             }
             Assert.Pass("Every plugin can be accessed");
         }
         [Test]
         public void TryLoadIncorrectPlugin()
         {
             IWindsorContainer container=new WindsorContainer();
             InitializeWindsor(ref container);
             try
             {
                 var plugin = container.Resolve<FakePlugin>();
                
             }
             catch
             {
                 Assert.Pass();
             }
             Assert.Fail();
         }
    }
}
