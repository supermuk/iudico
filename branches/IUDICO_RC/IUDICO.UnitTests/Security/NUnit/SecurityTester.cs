using NUnit.Framework;

using IUDICO.Security.Models.Storages;

namespace IUDICO.UnitTests.Security.NUnit
{
    [TestFixture]
    internal class SecurityTester
    {
        private static readonly Mocks Mocks;

        static SecurityTester()
        {
            Mocks = new Mocks();
        }

        protected IBanStorage BanStorage
        {
            get
            {
                return Mocks.BanStorage;
            }
        }

        protected ISecurityStorage SecurityStorage
        {
            get
            {
                return Mocks.SecurityStorage;
            }
        }

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            Mocks.Reset();
        }
    }
}