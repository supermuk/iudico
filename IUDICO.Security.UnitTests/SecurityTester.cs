using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IUDICO.Security.UnitTests.Infrastructure;
using IUDICO.Security.Models.Storages;

namespace IUDICO.Security.UnitTests
{
    [TestFixture]
    class SecurityTester
    {
        private static Mocks _Mocks;

        static SecurityTester()
        {
            _Mocks = new Mocks();
        }

        protected IBanStorage BanStorage
        {
            get { return _Mocks.BanStorage; }
        }

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}
