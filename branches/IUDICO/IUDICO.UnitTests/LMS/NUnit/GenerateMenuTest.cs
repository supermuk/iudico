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
    internal class GenerateMenuTest
    {
        private static IWindsorContainer container = new WindsorContainer();

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
                         FromAssembly.InDirectory(new AssemblyFilter(fullPath.Replace("Plugins", "bin"), "IUDICO.LMS.dll")),
                         FromAssembly.InDirectory(new AssemblyFilter(fullPath, "IUDICO.*.dll"))
                );
        }

        public bool IsAllowed(string controller, string action, IEnumerable<Role> roles)
        {
            // if can't resolve controller, don't allow access to it
            try
            {
                var _controller = container.Resolve<IController>(controller + "controller");
                var _action =
                    _controller.GetType().GetMethods().Where(
                        m => m.Name == action && !IsPost(m) && m.GetParameters().Length == 0).FirstOrDefault();

                var attribute = Attribute.GetCustomAttribute(_action, typeof (AllowAttribute), false) as AllowAttribute;

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
            return Attribute.GetCustomAttribute(action, typeof (HttpPostAttribute), false) != null;
        }

        private static ILmsService service;

        [Test]
        public void GenerateMenuUsingRoleNone()
        {
            HttpRequest httpRequest = new HttpRequest("", "http://mySomething/", "");
            StringWriter stringWriter = new StringWriter();
            HttpResponse httpResponce = new HttpResponse(stringWriter);
            //httpResponce.Filter = new FileStream("asd.pdo",FileMode.CreateNew);
            HttpContext httpConextMock = new HttpContext(httpRequest, httpResponce);

            HttpContext.Current = httpConextMock;
            Menu menu = new Menu();
            Menu menu1 = new Menu();
            container = new WindsorContainer();
            //HttpContext.Current = new HttpContext(new HttpRequest("", "http://iudico.com", null), new HttpResponse(new StreamWriter("mayBeDeleted.txt")));
            InitializeWindsor(ref container);

            service = container.Resolve<ILmsService>();
            PluginController.LmsService = service;
            var plugins = container.ResolveAll<IPlugin>();
            Dictionary<IPlugin, IEnumerable<Action>> actions =
                new Dictionary<IPlugin, IEnumerable<Action>>();
            Dictionary<IPlugin, IEnumerable<Action>> actions1;
            List<Role> roles = new List<Role>();
            roles.Add(Role.None);

            IEnumerable<Role> currentRole = from rol in roles
                                            select rol;
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(item => item.GetCurrentUserRoles()).Returns(currentRole);
            IUserService userServiceVar = service.FindService<IUserService>();
            userServiceVar = userServiceMock.Object;
            Mock<ILmsService> lmsservice = new Mock<ILmsService>();
            lmsservice.Setup(item => item.FindService<IUserService>()).Returns(userServiceMock.Object);
            AccountController acct = new AccountController(new DatabaseUserStorage(service));
            lmsservice.Setup(item => item.GetMenu()).Returns(menu);

            try
            {
                service = lmsservice.Object;
                service.Inform(LMSNotifications.ApplicationRequestStart);
            }
            catch (Exception e)
            {
            }

            foreach (var plugin in plugins)
            {
                menu.Add(plugin.BuildMenuItems().Where(m => IsAllowed(m.Controller, m.Action, roles)));
            }

            foreach (var plugin in plugins)
            {
                IEnumerable<MenuItem> menu11 = plugin.BuildMenuItems();
                foreach (var menu2 in menu.Items)
                {
                    if (menu11.Count(item => item == menu2) == 0)
                    {
                        Assert.Fail("Iterms mismatch");
                    }
                }
            }
        }

        [Test]
        public void GenerateMenuUsingRoleStudent()
        {
            HttpRequest httpRequest = new HttpRequest("", "http://mySomething/", "");
            StringWriter stringWriter = new StringWriter();
            HttpResponse httpResponce = new HttpResponse(stringWriter);
            //httpResponce.Filter = new FileStream("asd.pdo",FileMode.CreateNew);
            HttpContext httpConextMock = new HttpContext(httpRequest, httpResponce);

            HttpContext.Current = httpConextMock;
            Menu menu = new Menu();
            Menu menu1 = new Menu();
            container = new WindsorContainer();
            //HttpContext.Current = new HttpContext(new HttpRequest("", "http://iudico.com", null), new HttpResponse(new StreamWriter("mayBeDeleted.txt")));
            InitializeWindsor(ref container);

            service = container.Resolve<ILmsService>();
            PluginController.LmsService = service;
            var plugins = container.ResolveAll<IPlugin>();
            List<Role> roles = new List<Role>();
            roles.Add(Role.Student);

            IEnumerable<Role> currentRole = from rol in roles
                                            select rol;
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(item => item.GetCurrentUserRoles()).Returns(currentRole);
            IUserService userServiceVar = service.FindService<IUserService>();
            userServiceVar = userServiceMock.Object;
            Mock<ILmsService> lmsservice = new Mock<ILmsService>();
            lmsservice.Setup(item => item.FindService<IUserService>()).Returns(userServiceMock.Object);
            AccountController acct = new AccountController(new DatabaseUserStorage(service));
            lmsservice.Setup(item => item.GetMenu()).Returns(menu);

            try
            {
                service = lmsservice.Object;
                service.Inform(LMSNotifications.ApplicationRequestStart);
            }
            catch (Exception e)
            {
            }

            foreach (var plugin in plugins)
            {
                menu.Add(plugin.BuildMenuItems().Where(m => IsAllowed(m.Controller, m.Action, roles)));
            }

            foreach (var plugin in plugins)
            {
                Menu menu11 = service.GetMenu();

                foreach (var menu2 in menu.Items)
                {
                    if (menu11.Items.Count(item => item == menu2) == 0)
                    {
                        Assert.Fail("Iterms mismatch");
                    }
                }
            }
        }

        [Test]
        public void GenerateMenuUsingRoleTeacher()
        {
            HttpRequest httpRequest = new HttpRequest("", "http://mySomething/", "");
            StringWriter stringWriter = new StringWriter();
            HttpResponse httpResponce = new HttpResponse(stringWriter);
            //httpResponce.Filter = new FileStream("asd.pdo",FileMode.CreateNew);
            HttpContext httpConextMock = new HttpContext(httpRequest, httpResponce);

            HttpContext.Current = httpConextMock;
            Menu menu = new Menu();
            Menu menu1 = new Menu();
            container = new WindsorContainer();
            //HttpContext.Current = new HttpContext(new HttpRequest("", "http://iudico.com", null), new HttpResponse(new StreamWriter("mayBeDeleted.txt")));
            InitializeWindsor(ref container);

            service = container.Resolve<ILmsService>();
            PluginController.LmsService = service;
            var plugins = container.ResolveAll<IPlugin>();
            List<Role> roles = new List<Role>();
            roles.Add(Role.Teacher);

            IEnumerable<Role> currentRole = from rol in roles
                                            select rol;
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(item => item.GetCurrentUserRoles()).Returns(currentRole);
            IUserService userServiceVar = service.FindService<IUserService>();
            userServiceVar = userServiceMock.Object;
            Mock<ILmsService> lmsservice = new Mock<ILmsService>();
            lmsservice.Setup(item => item.FindService<IUserService>()).Returns(userServiceMock.Object);
            AccountController acct = new AccountController(new DatabaseUserStorage(service));
            lmsservice.Setup(item => item.GetMenu()).Returns(menu);

            try
            {
                service = lmsservice.Object;
                service.Inform(LMSNotifications.ApplicationRequestStart);
            }
            catch (Exception e)
            {
            }

            foreach (var plugin in plugins)
            {
                menu.Add(plugin.BuildMenuItems().Where(m => IsAllowed(m.Controller, m.Action, roles)));
            }

            foreach (var plugin in plugins)
            {
                Menu menu11 = service.GetMenu();

                foreach (var menu2 in menu.Items)
                {
                    if (menu11.Items.Count(item => item == menu2) == 0)
                    {
                        Assert.Fail("Iterms mismatch");
                    }
                }
            }
        }

        [Test]
        public void GenerateMenuUsingRoleAdmin()
        {
            HttpRequest httpRequest = new HttpRequest("", "http://mySomething/", "");
            StringWriter stringWriter = new StringWriter();
            HttpResponse httpResponce = new HttpResponse(stringWriter);
            //httpResponce.Filter = new FileStream("asd.pdo",FileMode.CreateNew);
            HttpContext httpConextMock = new HttpContext(httpRequest, httpResponce);

            HttpContext.Current = httpConextMock;
            Menu menu = new Menu();
            Menu menu1 = new Menu();
            container = new WindsorContainer();
            //HttpContext.Current = new HttpContext(new HttpRequest("", "http://iudico.com", null), new HttpResponse(new StreamWriter("mayBeDeleted.txt")));
            InitializeWindsor(ref container);

            service = container.Resolve<ILmsService>();
            PluginController.LmsService = service;
            var plugins = container.ResolveAll<IPlugin>();
            List<Role> roles = new List<Role>();
            roles.Add(Role.Admin);

            IEnumerable<Role> currentRole = from rol in roles
                                            select rol;
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(item => item.GetCurrentUserRoles()).Returns(currentRole);
            IUserService userServiceVar = service.FindService<IUserService>();
            userServiceVar = userServiceMock.Object;
            Mock<ILmsService> lmsservice = new Mock<ILmsService>();
            lmsservice.Setup(item => item.FindService<IUserService>()).Returns(userServiceMock.Object);
            AccountController acct = new AccountController(new DatabaseUserStorage(service));
            lmsservice.Setup(item => item.GetMenu()).Returns(menu);

            try
            {
                service = lmsservice.Object;
                service.Inform(LMSNotifications.ApplicationRequestStart);
            }
            catch (Exception e)
            {
            }

            foreach (var plugin in plugins)
            {
                menu.Add(plugin.BuildMenuItems().Where(m => IsAllowed(m.Controller, m.Action, roles)));
            }

            foreach (var plugin in plugins)
            {
                Menu menu11 = service.GetMenu();

                foreach (var menu2 in menu.Items)
                {
                    if (menu11.Items.Count(item => item == menu2) == 0)
                    {
                        Assert.Fail("Iterms mismatch");
                    }
                }
            }
        }
    }
}