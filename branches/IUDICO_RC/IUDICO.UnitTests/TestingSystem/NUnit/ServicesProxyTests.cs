// -----------------------------------------------------------------------
// <copyright file="ServicesProxyTests.cs" company="">
// 
// </copyright>
// -----------------------------------------------------------------------

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    using System;

    using IUDICO.Common.Models.Services;
    using IUDICO.TestingSystem.Models;

    using Moq;

    using global::NUnit.Framework;

    /// <summary>
    /// Bunch of tests to test <see cref="ServicesProxy"/>.
    /// </summary>
    [TestFixture]
    public class ServicesProxyTests
    {
        [Test]
        public void TestServicesProxyIsSingleton()
        {
            var servicesProxy = ServicesProxy.Instance;
            var servicesProxy2 = ServicesProxy.Instance;

            Assert.AreSame(servicesProxy, servicesProxy2);
        }

        [Test]
        public void ServiceProxyNotInitializedCall()
        {
            Assert.Throws(
                typeof(NullReferenceException),
                () =>
                    { var testingService = ServicesProxy.Instance.TestingService; });
            
            Assert.Throws(
                typeof(NullReferenceException),
                () =>
                { var userService = ServicesProxy.Instance.UserService; });
        }

        [Test]
        public void TestServicesProxyCorrectInitializeAndCall()
        {
            var testingServiceMock = new Mock<ITestingService>(MockBehavior.Strict);
            var userServiceMock = new Mock<IUserService>(MockBehavior.Strict);

            var lmsServiceMock = new Mock<ILmsService>(MockBehavior.Strict);

            lmsServiceMock.Setup(mock => mock.FindService<IUserService>()).Returns(userServiceMock.Object);
            lmsServiceMock.Setup(mock => mock.FindService<ITestingService>()).Returns(testingServiceMock.Object);

            ServicesProxy.Instance.Initialize(lmsServiceMock.Object);

            Assert.AreSame(lmsServiceMock.Object, ServicesProxy.Instance.LmsService);
            Assert.AreSame(testingServiceMock.Object, ServicesProxy.Instance.TestingService);
            Assert.AreSame(userServiceMock.Object, ServicesProxy.Instance.UserService);
        }
    }
}
