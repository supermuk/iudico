using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.LMS.Models;
using IUDICO.UserManagement.Controllers;
using IUDICO.UserManagement.Models.Storage;

using Moq;

using NUnit.Framework;

using Action = IUDICO.Common.Models.Action;

namespace IUDICO.UnitTests.LMS.NUnit
{
    [TestFixture]
    internal class GenerateActions
    {
        public Dictionary<IPlugin, IEnumerable<Action>> DataFromLms(Role role)
        {
            var accts = new Dictionary<IPlugin, IEnumerable<Action>>();
            var roles = new List<Role>();
            roles.Add(role);
            foreach (var plugin in container.ResolveAll<IPlugin>())
            {
                accts.Add(
                    plugin, 
                    plugin.BuildActions().Where(
                        a => this.IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles)));
            }

            return accts;
        }

        private IService FindServ<T>(IUserService ser) where T : IService
        {
            if (typeof(T) == typeof(IUserService))
            {
                return ser;
            }
            else
            {
                return container.Resolve<T>();
            }
        }

        private static IWindsorContainer container = new WindsorContainer();

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

        public bool IsAllowed(string controller, string action, IEnumerable<Role> roles)
        {
            // if can't resolve controller, don't allow access to it
            try
            {
                var contr = container.Resolve<IController>(controller + "controller");
                var act =
                    contr.GetType().GetMethods().FirstOrDefault(m => m.Name == action && !this.IsPost(m) && m.GetParameters().Length == 0);

                var attribute = Attribute.GetCustomAttribute(act, typeof(AllowAttribute), false) as AllowAttribute;

                if (attribute == null)
                {
                    return true;
                }
                else if (attribute.Role == Role.None)
                {
                    return !roles.Contains(Role.None);
                }
                else
                {
                    return roles.Any(i => i != Role.None && attribute.Role.HasFlag(i));
                }
            }
            catch
            {
                return false;
            }
        }

        protected bool IsPost(MethodInfo action)
        {
            return Attribute.GetCustomAttribute(action, typeof(HttpPostAttribute), false) != null;
        }

        private static ILmsService service;

        [Test]
        public void GenerateActionsUsingRoleNone()
        {
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething/", string.Empty);
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);

            // httpResponce.Filter = new FileStream("asd.pdo",FileMode.CreateNew);
            var httpConextMock = new HttpContext(httpRequest, httpResponce);

            HttpContext.Current = httpConextMock;

            container = new WindsorContainer();

            // HttpContext.Current = new HttpContext(new HttpRequest("", "http://iudico.com", null), new HttpResponse(new StreamWriter("mayBeDeleted.txt")));
            InitializeWindsor(ref container);

            service = container.Resolve<ILmsService>();
            PluginController.LmsService = service;
            var plugins = container.ResolveAll<IPlugin>();
            var actions = new Dictionary<IPlugin, IEnumerable<Action>>();

            var roles = new List<Role>();
            roles.Add(Role.None);

            var currentRole = from rol in roles select rol;
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(item => item.GetCurrentUserRoles()).Returns(currentRole);
            var userServiceVar = service.FindService<IUserService>();
            userServiceVar = userServiceMock.Object;
            var lmsservice = new Mock<ILmsService>();
            lmsservice.Setup(item => item.FindService<IUserService>()).Returns(userServiceMock.Object);
            var acct = new AccountController(new DatabaseUserStorage(service));
            lmsservice.Setup(item => item.GetActions()).Returns(actions);

            try
            {
                service = lmsservice.Object;
                service.Inform(LMSNotifications.ApplicationRequestStart);
            }
            catch (Exception)
            {
            }

            foreach (var plugin in plugins)
            {
                actions.Add(
                    plugin, 
                    plugin.BuildActions().Where(
                        a => this.IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles)));
            }

            foreach (var plugin in plugins)
            {
                var action1 =
                    service.GetActions()[plugin].Where(
                        a => this.IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles));
                var action =
                    actions[plugin].Where(
                        a => this.IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles));

                foreach (var action2 in action)
                {
                    if (action1.Count(item => item == action2) == 0)
                    {
                        Assert.Fail("Iterms mismatch");
                    }
                }
            }
        }

        [Test]
        public void GenerateActionsUsingRoleStudent()
        {
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething/", string.Empty);
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);

            // httpResponce.Filter = new FileStream("asd.pdo",FileMode.CreateNew);
            var httpConextMock = new HttpContext(httpRequest, httpResponce);

            HttpContext.Current = httpConextMock;

            container = new WindsorContainer();

            // HttpContext.Current = new HttpContext(new HttpRequest("", "http://iudico.com", null), new HttpResponse(new StreamWriter("mayBeDeleted.txt")));
            InitializeWindsor(ref container);

            service = container.Resolve<ILmsService>();
            PluginController.LmsService = service;
            var plugins = container.ResolveAll<IPlugin>();
            var actions = new Dictionary<IPlugin, IEnumerable<Action>>();

            var roles = new List<Role> { Role.Student };

            var currentRole = from rol in roles select rol;
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(item => item.GetCurrentUserRoles()).Returns(currentRole);
            var userServiceVar = service.FindService<IUserService>();
            userServiceVar = userServiceMock.Object;
            var lmsservice = new Mock<ILmsService>();
            lmsservice.Setup(item => item.FindService<IUserService>()).Returns(userServiceMock.Object);
            var acct = new AccountController(new DatabaseUserStorage(service));
            lmsservice.Setup(item => item.GetActions()).Returns(actions);

            try
            {
                service = lmsservice.Object;
                service.Inform(LMSNotifications.ApplicationRequestStart);
            }
            catch (Exception)
            {
            }

            foreach (var plugin in plugins)
            {
                actions.Add(
                    plugin, 
                    plugin.BuildActions().Where(
                        a => this.IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles)));
            }

            foreach (var plugin in plugins)
            {
                var action1 =
                    service.GetActions()[plugin].Where(
                        a => this.IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles));
                var action =
                    actions[plugin].Where(
                        a => this.IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles));

                foreach (var action2 in action)
                {
                    if (action1.Count(item => item == action2) == 0)
                    {
                        Assert.Fail("Iterms mismatch");
                    }
                }
            }
        }

        [Test]
        public void GenerateActionsUsingRoleSTeacher()
        {
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething/", string.Empty);
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);

            // httpResponce.Filter = new FileStream("asd.pdo",FileMode.CreateNew);
            var httpConextMock = new HttpContext(httpRequest, httpResponce);

            HttpContext.Current = httpConextMock;

            container = new WindsorContainer();

            // HttpContext.Current = new HttpContext(new HttpRequest("", "http://iudico.com", null), new HttpResponse(new StreamWriter("mayBeDeleted.txt")));
            InitializeWindsor(ref container);

            service = container.Resolve<ILmsService>();
            PluginController.LmsService = service;
            var plugins = container.ResolveAll<IPlugin>();
            var actions = new Dictionary<IPlugin, IEnumerable<Action>>();

            var roles = new List<Role> { Role.Teacher };

            var currentRole = from rol in roles select rol;
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(item => item.GetCurrentUserRoles()).Returns(currentRole);
            var userServiceVar = service.FindService<IUserService>();
            userServiceVar = userServiceMock.Object;
            var lmsservice = new Mock<ILmsService>();
            lmsservice.Setup(item => item.FindService<IUserService>()).Returns(userServiceMock.Object);
            var acct = new AccountController(new DatabaseUserStorage(service));
            lmsservice.Setup(item => item.GetActions()).Returns(actions);

            try
            {
                service = lmsservice.Object;
                service.Inform(LMSNotifications.ApplicationRequestStart);
            }
            catch (Exception)
            {
            }

            foreach (var plugin in plugins)
            {
                actions.Add(
                    plugin, 
                    plugin.BuildActions().Where(
                        a => this.IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles)));
            }

            foreach (var plugin in plugins)
            {
                var action1 =
                    service.GetActions()[plugin].Where(
                        a => this.IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles));
                var action =
                    actions[plugin].Where(
                        a => this.IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles));
                foreach (var action2 in action)
                {
                    if (action1.Count(item => item == action2) == 0)
                    {
                        Assert.Fail("Iterms mismatch");
                    }
                }
            }
        }

        [Test]
        public void GenerateActionsUsingRoleAdmin()
        {
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething/", string.Empty);
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);

            // httpResponce.Filter = new FileStream("asd.pdo",FileMode.CreateNew);
            var httpConextMock = new HttpContext(httpRequest, httpResponce);

            HttpContext.Current = httpConextMock;

            container = new WindsorContainer();

            // HttpContext.Current = new HttpContext(new HttpRequest("", "http://iudico.com", null), new HttpResponse(new StreamWriter("mayBeDeleted.txt")));
            InitializeWindsor(ref container);

            service = container.Resolve<ILmsService>();
            PluginController.LmsService = service;
            var plugins = container.ResolveAll<IPlugin>();
            var actions = new Dictionary<IPlugin, IEnumerable<Action>>();

            var roles = new List<Role> { Role.Admin };

            var currentRole = from rol in roles select rol;
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(item => item.GetCurrentUserRoles()).Returns(currentRole);
            var userServiceVar = service.FindService<IUserService>();
            userServiceVar = userServiceMock.Object;
            var lmsservice = new Mock<ILmsService>();
            lmsservice.Setup(item => item.FindService<IUserService>()).Returns(userServiceMock.Object);
            var acct = new AccountController(new DatabaseUserStorage(service));
            lmsservice.Setup(item => item.GetActions()).Returns(actions);

            try
            {
                service = lmsservice.Object;
                service.Inform(LMSNotifications.ApplicationRequestStart);
            }
            catch (Exception)
            {
            }

            foreach (var plugin in plugins)
            {
                actions.Add(
                    plugin, 
                    plugin.BuildActions().Where(
                        a => this.IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles)));
            }

            foreach (var plugin in plugins)
            {
                var action1 =
                    service.GetActions()[plugin].Where(
                        a => this.IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles));

                var action =
                    actions[plugin].Where(
                        a => this.IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles));

                foreach (var action2 in action)
                {
                    if (action1.Count(item => item == action2) == 0)
                    {
                        Assert.Fail("Iterms mismatch");
                    }
                }
            }
        }
    }
}