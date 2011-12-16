using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.Common.Models.Shared;
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

        protected ISecurityStorage SecurityStorage
        {
            get { return _Mocks.SecurityStorage; }
        }

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            _Mocks.Reset();
        }
    }
}
