﻿namespace IUDICO.UnitTests.Base
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using IUDICO.Common.Controllers;
    using IUDICO.UnitTests.UserManagement;

    using Moq;

    public class BaseControllerTest
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        protected T GetController<T>() where T : PluginController
        {
            var controller = (T)Activator.CreateInstance(typeof(T), new object[] { this.tests.Storage });

            var mocks = new ContextMocks(controller);
            mocks.RouteData.Values["action"] = "Index";
            mocks.RouteData.Values["controller"] = typeof(T).Name;
            
            return controller;
        }

        private class ContextMocks
        {
            public Mock<HttpContextBase> HttpContext { get; private set; }

            public Mock<HttpRequestBase> Request { get; private set; }

            public Mock<HttpResponseBase> Response { get; private set; }

            public RouteData RouteData { get; private set; }

            public ContextMocks(ControllerBase controller)
            {
                // Define all the common context objects, plus relationships between them
                this.HttpContext = new Mock<HttpContextBase>();
                this.Request = new Mock<HttpRequestBase>();
                this.Response = new Mock<HttpResponseBase>();
                this.RouteData = new RouteData();
                this.HttpContext.Setup(x => x.Request).Returns(this.Request.Object);
                this.HttpContext.Setup(x => x.Response).Returns(this.Response.Object);
                this.HttpContext.Setup(x => x.Session).Returns(new FakeSessionState());

                this.Request.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
                this.Response.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
                this.Request.Setup(x => x.QueryString).Returns(new NameValueCollection());
                this.Request.Setup(x => x.Form).Returns(new NameValueCollection());

                // Apply the mock context to the supplied controller instance
                var rc = new RequestContext(this.HttpContext.Object, this.RouteData);
                controller.ControllerContext = new ControllerContext(rc, controller);
            }
        }

        // Use a fake HttpSessionStateBase, because it's hard to mock it with Moq
        private class FakeSessionState : HttpSessionStateBase
        {
            private readonly Dictionary<string, object> items = new Dictionary<string, object>();

            public override object this[string name]
            {
                get
                {
                    return this.items.ContainsKey(name) ? this.items[name] : null;
                }

                set
                {
                    this.items[name] = value;
                }
            }
        }
    }
}
