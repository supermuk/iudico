using IUDICO.Common.Models.Caching.Provider;
using IUDICO.Security.Models.Storages.Cache;

namespace IUDICO.UnitTests.Security.NUnit
{
    using System;

    using IUDICO.Common.Models;
    using IUDICO.Common.Models.Interfaces;
    using IUDICO.Common.Models.Services;
    using IUDICO.Common.Models.Shared;
    using IUDICO.Security.Models;
    using IUDICO.Security.Models.Storages;
    using IUDICO.Security.Models.Storages.Database;

    using Moq;

    internal class Mocks
    {
        protected IBanStorage banStorage;

        protected ISecurityStorage securityStorage;

        protected Mock<ILmsService> mockLmsService;

        protected Mock<ISecurityDataContext> mockSecurityDataContext;

        public IBanStorage BanStorage
        {
            get
            {
                return this.banStorage;
            }
        }

        public ISecurityStorage SecurityStorage
        {
            get
            {
                return this.securityStorage;
            }
        }

        public Mocks()
        {
            this.MockLms();
            this.MockSecurityDataContext();
            this.MockBanStorage();
            this.MockSecurityStorage();
        }

        public void Reset()
        {
            this.mockSecurityDataContext.SetupGet(c => c.Computers).Returns(CreateComputers());
            this.mockSecurityDataContext.SetupGet(c => c.Rooms).Returns(CreateRooms());
            this.mockSecurityDataContext.SetupGet(c => c.UserActivities).Returns(CreateUserActivities());
        }

        private void MockLms()
        {
            this.mockLmsService = new Mock<ILmsService>();
        }

        private void MockSecurityDataContext()
        {
            this.mockSecurityDataContext = new Mock<ISecurityDataContext>();

            this.mockSecurityDataContext.SetupGet(c => c.Computers).Returns(CreateComputers());
            this.mockSecurityDataContext.SetupGet(c => c.Rooms).Returns(CreateRooms());
            this.mockSecurityDataContext.SetupGet(c => c.UserActivities).Returns(CreateUserActivities());
        }

        private void MockBanStorage()
        {
            var context = this.mockSecurityDataContext.Object;
            Func<ISecurityDataContext> createSecurityDataContext = () => { return context; };

            var dbbanStorage = new DatabaseBanStorage(this.mockLmsService.Object, createSecurityDataContext);
            var cachePrvoider = new HttpCache();
            this.banStorage = new CachedBanStorage(dbbanStorage, cachePrvoider);
        }

        private void MockSecurityStorage()
        {
            var context = this.mockSecurityDataContext.Object;

            Func<ISecurityDataContext> createSecurityDataContext = () => { return context; };

            var dbsecurityStorage = new DatabaseSecurityStorage(createSecurityDataContext);
            var cachePrvoider = new HttpCache();
            this.securityStorage = new CachedSecurityStorage(dbsecurityStorage, cachePrvoider);
        }

        private static IMockableTable<Computer> CreateComputers()
        {
            var computers = new[] { new Computer { CurrentUser = "lex", IpAddress = "100.100.100.100", Banned = true } };

            return new MemoryTable<Computer>(computers);
        }

        private static IMockableTable<Room> CreateRooms()
        {
            var rooms = new[] { new Room { Id = 1, Name = "tester", Allowed = true } };

            return new MemoryTable<Room>(rooms);
        }

        private static IMockableTable<UserActivity> CreateUserActivities()
        {
            return new MemoryTable<UserActivity>();
        }
    }
}