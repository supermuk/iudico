using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.LMS;
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

        protected void RegisterRoutes(RouteCollection routes, ref IWindsorContainer Container)
        {
            var plugins = Container.ResolveAll<IPlugin>();

            foreach (var plugin in plugins)
            {
                plugin.RegisterRoutes(routes);
            }

            Container.Release(plugins);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ico/{*pathInfo}");
            routes.IgnoreRoute("Scripts/");
            routes.IgnoreRoute("Content/");
            routes.IgnoreRoute("Data/");

            routes.MapRoute(
                "Default", // Route name,
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
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
            mockResponse.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>()))
                .Returns<string>(x => x);
            return mockHttpContext;
        }

        public class MockHttpSession : HttpSessionStateBase
        {

            private Dictionary<string, object> _sessionDictionary = new Dictionary<string, object>();

            public override object this[string name]
            {

                get { return _sessionDictionary[name]; }

                set { _sessionDictionary[name] = value; }

            }

        }

        [Test]
        public void UkrainianChangesIntoEnglish()
        {
            var httpRequest = new HttpRequest("", "http://mySomething/", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponce);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                                 new HttpStaticObjectsCollection(), 10, true,
                                                                 HttpCookieMode.AutoDetect,
                                                                 SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                                     BindingFlags.NonPublic | BindingFlags.Instance,
                                                     null, CallingConventions.Standard,
                                                     new[] { typeof(HttpSessionStateContainer) },
                                                     null)
                                                .Invoke(new object[] { sessionContainer });

            HttpContext.Current = httpContext;
            IWindsorContainer container=new WindsorContainer();
            InitializeWindsor(ref container);
            AccountController pc = null;
            try
            {
                pc = new AccountController(new DatabaseUserStorage(container.Resolve<ILmsService>()));
            }
            catch(Exception e)
            {
            }
            var context = new Mock<ControllerContext>();

            var session = new MockHttpSession();

            context.Setup(m => m.HttpContext.Session).Returns(session);
            
            DatabaseUserStorage storage=new DatabaseUserStorage(null);//returns null but we don't need it
            
            pc.ControllerContext = context.Object;
            ViewResult result = pc.ChangeCulture("en-US","/") as ViewResult;
            Assert.AreEqual("en-US",pc.Session["Culture"].ToString());

        }
        [Test]
        public void EnglishChangesIntoUkrainian()
        {
            //lets make fake httpcontext using reflection
            var httpRequest = new HttpRequest("", "http://mySomething/", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponce);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                                 new HttpStaticObjectsCollection(), 10, true,
                                                                 HttpCookieMode.AutoDetect,
                                                                 SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                                     BindingFlags.NonPublic | BindingFlags.Instance,
                                                     null, CallingConventions.Standard,
                                                     new[] { typeof(HttpSessionStateContainer) },
                                                     null)
                                                .Invoke(new object[] { sessionContainer });
            
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

            DatabaseUserStorage storage = new DatabaseUserStorage(null);//returns null but we don't need it

            pc.ControllerContext = context.Object;
            ViewResult result = pc.ChangeCulture("uk-UA", "/") as ViewResult;
            Assert.AreEqual("uk-UA", pc.Session["Culture"].ToString());

        }

    }
}
