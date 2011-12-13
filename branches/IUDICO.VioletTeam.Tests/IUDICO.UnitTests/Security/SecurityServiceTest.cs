using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using IUDICO.Common.Models.Services;
using Castle.Core;
using IUDICO.Security.Models;
using IUDICO.Security;

namespace IUDICO.UnitTests.Security
{
    [TestClass]
    public class SecurityServiceTest
    {
        private ISecurityService GetSecurityService_Mock()
        {
            var mockSecurityService = new Mock<ISecurityService>();

            mockSecurityService
                .Setup(security => security.CheckRequestSafety(It.IsAny<HttpRequestBase>()))
                .Returns(delegate(HttpRequestBase request)
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

        [TestMethod]
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
