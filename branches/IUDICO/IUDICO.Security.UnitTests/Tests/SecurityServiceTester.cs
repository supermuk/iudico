using NUnit.Framework;
using Moq;
using System.Web;
using IUDICO.Common.Models.Services;

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
            return new SecurityService();
        }

        private HttpRequestBase GetGoodRequest()
        {
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest
                .SetupGet(x => x.ContentLength).Returns(100);

            return mockRequest.Object;
        }

        private HttpRequestBase GetBadRequest()
        {
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest
                .SetupGet(x => x.ContentLength).Returns(1000);

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
