using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.LMS.Models;
using IUDICO.UserManagement.Controllers;
using IUDICO.UserManagement.Models.Storage;

using Moq;

using NUnit.Framework;

namespace IUDICO.UnitTests.LMS.NUnit
{
    [TestFixture]
    internal class ChangingLanguageTestsTests
    {
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

        protected void RegisterRoutes(RouteCollection routes, ref IWindsorContainer container)
        {
            var plugins = container.ResolveAll<IPlugin>();

            foreach (var plugin in plugins)
            {
                plugin.RegisterRoutes(routes);
            }

            container.Release(plugins);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ico/{*pathInfo}");
            routes.IgnoreRoute("Scripts/");
            routes.IgnoreRoute("Content/");
            routes.IgnoreRoute("Data/");

            routes.MapRoute(
                "Default", 
                // Route name,
                "{controller}/{action}/{id}", 
                // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        private static Mock<HttpContextBase> MakeMockHttpContext(string url)
        {
            var mockHttpContext = new Mock<HttpContextBase>();

            // Mock the request
            var mockRequest = new Mock<HttpRequestBase>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);
            mockRequest.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(url);

            // Mock the response
            var mockResponse = new Mock<HttpResponseBase>();
            mockHttpContext.Setup(x => x.Response).Returns(mockResponse.Object);
            mockResponse.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(x => x);
            return mockHttpContext;
        }

        public class MockHttpSession : HttpSessionStateBase
        {
            private readonly Dictionary<string, object> sessionDictionary = new Dictionary<string, object>();

            public override object this[string name]
            {
                get
                {
                    return this.sessionDictionary[name];
                }

                set
                {
                    this.sessionDictionary[name] = value;
                }
            }
        }

        [Test]
        public void UkrainianChangesIntoEnglish()
        {
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething/", string.Empty);
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponce);

            var sessionContainer = new HttpSessionStateContainer(
                "id", 
                new SessionStateItemCollection(), 
                new HttpStaticObjectsCollection(), 
                10, 
                true, 
                HttpCookieMode.AutoDetect, 
                SessionStateMode.InProc, 
                false);

            httpContext.Items["AspSession"] =
                typeof(HttpSessionState).GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.Instance, 
                    null, 
                    CallingConventions.Standard, 
                    new[] { typeof(HttpSessionStateContainer) }, 
                    null).Invoke(new object[] { sessionContainer });

            HttpContext.Current = httpContext;
            IWindsorContainer container = new WindsorContainer();
            InitializeWindsor(ref container);
            AccountController pc = null;
            try
            {
                pc = new AccountController(new DatabaseUserStorage(container.Resolve<ILmsService>()));
            }
            catch (Exception e)
            {
            }

            var context = new Mock<ControllerContext>();

            var session = new MockHttpSession();

            context.Setup(m => m.HttpContext.Session).Returns(session);

            var storage = new DatabaseUserStorage(null); // returns null but we don't need it

            pc.ControllerContext = context.Object;
            var result = pc.ChangeCulture("en-US", "/") as ViewResult;
            Assert.AreEqual("en-US", pc.Session["Culture"].ToString());
        }

        [Test]
        public void EnglishChangesIntoUkrainian()
        {
            // lets make fake httpcontext using reflection
            var httpRequest = new HttpRequest(string.Empty, "http://mySomething/", string.Empty);
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponce);

            var sessionContainer = new HttpSessionStateContainer(
                "id", 
                new SessionStateItemCollection(), 
                new HttpStaticObjectsCollection(), 
                10, 
                true, 
                HttpCookieMode.AutoDetect, 
                SessionStateMode.InProc, 
                false);

            httpContext.Items["AspSession"] =
                typeof(HttpSessionState).GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.Instance, 
                    null, 
                    CallingConventions.Standard, 
                    new[] { typeof(HttpSessionStateContainer) }, 
                    null).Invoke(new object[] { sessionContainer });

            HttpContext.Current = httpContext;
            IWindsorContainer container = new WindsorContainer();
            InitializeWindsor(ref container);
            AccountController pc = null;
            try
            {
                pc = new AccountController(new DatabaseUserStorage(container.Resolve<ILmsService>()));
            }
            catch (Exception e)
            {
            }

            var context = new Mock<ControllerContext>();

            var session = new MockHttpSession();

            context.Setup(m => m.HttpContext.Session).Returns(session);

            var storage = new DatabaseUserStorage(null); // returns null but we don't need it

            pc.ControllerContext = context.Object;
            var result = pc.ChangeCulture("uk-UA", "/") as ViewResult;
            Assert.AreEqual("uk-UA", pc.Session["Culture"].ToString());
        }
    }
}