// -----------------------------------------------------------------------
// <copyright file="TestingServiceTests.cs" company="">
// 
// </copyright>
// -----------------------------------------------------------------------

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    using IUDICO.Common.Models.Services;
    using IUDICO.Common.Models.Shared;
    using IUDICO.Common.Models.Shared.DisciplineManagement;
    using IUDICO.Common.Models.Shared.Statistics;
    using IUDICO.TestingSystem.Models;

    using Moq;

    using global::NUnit.Framework;

    /// <summary>
    /// Bunch of tests for <see cref="TestingService"/>.
    /// Mostly checks if TestService delegates everything to <see cref="IMlcProxy"/>.
    /// </summary>
    [TestFixture]
    public class TestingServiceTests
    {
        private Mock<IMlcProxy> mlcProxyMock;

        private ITestingService testingService;
   
        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            this.mlcProxyMock = new Mock<IMlcProxy>(MockBehavior.Loose);
            this.testingService = new TestingService(this.mlcProxyMock.Object);
        }

        [Test]
        public void TestGetAnswers()
        {
            this.testingService.GetAnswers(It.IsAny<AttemptResult>());
            this.mlcProxyMock.Verify(proxy => proxy.GetAnswers(It.IsAny<AttemptResult>()), Times.Once());
        }

        [Test]
        public void TestGetResult()
        {
            this.testingService.GetResult(It.IsAny<long>());
            this.mlcProxyMock.Verify(proxy => proxy.GetResult(It.IsAny<long>()), Times.Once());
        }

        [Test]
        public void TestGetResults()
        {
            this.testingService.GetResults();
            this.mlcProxyMock.Verify(proxy => proxy.GetResults(), Times.Once());
        }

        [Test]
        public void TestGetResults2()
        {
            this.testingService.GetResults(It.IsAny<CurriculumChapterTopic>());
            this.mlcProxyMock.Verify(proxy => proxy.GetResults(It.IsAny<CurriculumChapterTopic>()), Times.Once());
        }

        [Test]
        public void TestGetResults3()
        {
            this.testingService.GetResults(It.IsAny<Topic>());
            this.mlcProxyMock.Verify(proxy => proxy.GetResults(It.IsAny<Topic>()), Times.Once());
        }

        [Test]
        public void TestGetResults4()
        {
            this.testingService.GetResults(It.IsAny<User>());
            this.mlcProxyMock.Verify(proxy => proxy.GetResults(It.IsAny<User>()), Times.Once());
        }

        [Test]
        public void TestGetResults5()
        {
            this.testingService.GetResults(It.IsAny<User>(), It.IsAny<CurriculumChapterTopic>());
            this.mlcProxyMock.Verify(proxy => proxy.GetResults(It.IsAny<User>(), It.IsAny<CurriculumChapterTopic>()), Times.Once());
        }

        [Test]
        public void TestGetResults6()
        {
            this.testingService.GetResults(It.IsAny<User>(), It.IsAny<CurriculumChapterTopic>(), It.IsAny<TopicTypeEnum>());
            this.mlcProxyMock.Verify(proxy => proxy.GetResults(It.IsAny<User>(), It.IsAny<CurriculumChapterTopic>(), It.IsAny<TopicTypeEnum>()), Times.Once());
        }
    }
}
