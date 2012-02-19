using IUDICO.Common.Models.Shared;
using NUnit.Framework;
using Moq;
using System.Web;
using IUDICO.Common.Models.Services;
using System.Collections.Specialized;
using System.Web.Routing;

namespace IUDICO.Security.UnitTests.Tests
{
    class SecurityServiceTester : SecurityTester
    {
        private ISecurityService GetSecurityService_Mock()
        {
            var mockSecurityService = new Mock<ISecurityService>();

            mockSecurityService
                .Setup(security => security.CheckRequestSafety(It.IsAny<HttpRequestBase>()))
                .Returns((HttpRequestBase request) =>
                    {
                        return request.ContentLength < 500;
                    });

            return mockSecurityService.Object;
        }

        private ISecurityService GetSecurityService_Real()
        {
            return new SecurityService(BanStorage);
        }

        private HttpRequestBase GetGoodRequest()
        {
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest
                .SetupGet(x => x.ContentLength).Returns(100);

            var vars = new NameValueCollection();
            vars.Add("REMOTE_ADDR", "100.100.100.100");

            mockRequest.SetupGet(r => r.ServerVariables)
                .Returns(vars);

            var routeData = new RouteData();
            routeData.Values.Add("action", "Banned");

            var httpContext = new Mock<HttpContextBase>();

            var rc = new RequestContext(httpContext.Object, routeData);

            mockRequest.Setup(r => r.RequestContext)
              .Returns(rc);

            return mockRequest.Object;
        }

        private HttpRequestBase GetBadRequest()
        {
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest
                .SetupGet(x => x.ContentLength).Returns(1000);

            var vars = new NameValueCollection();
            vars.Add("REMOTE_ADDR", "100.100.100.100");

            mockRequest.SetupGet(r => r.ServerVariables)
                .Returns(vars);

            var routeData = new RouteData();
            routeData.Values.Add("action", "!Banned");

            var httpContext = new Mock<HttpContextBase>();

            var rc = new RequestContext(httpContext.Object, routeData);

            mockRequest.Setup(r => r.RequestContext)
              .Returns(rc);

            return mockRequest.Object;
        }

        [Test]
        public void Test_CheckRequestSafety()
        {
            var goodRequest = GetGoodRequest();
            var badRequest = GetBadRequest();
            var securityService = GetSecurityService_Real();

            Assert.IsTrue(securityService.CheckRequestSafety(goodRequest));
            Assert.IsFalse(securityService.CheckRequestSafety(badRequest));
        }
    }
}
