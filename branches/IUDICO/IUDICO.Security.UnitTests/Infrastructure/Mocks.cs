using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.Security.Models.Storages;
using Moq;
using IUDICO.Security.Models.Storages.Database;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models;
using IUDICO.Security.Models;

namespace IUDICO.Security.UnitTests.Infrastructure
{
    class Mocks
    {
        protected IBanStorage _BanStorage;
        protected Mock<ILmsService> _MockLmsService;
        protected Mock<ISecurityDataContext> _MockSecurityDataContext;

        public IBanStorage BanStorage
        {
            get { return _BanStorage; }
        }

        public Mocks()
        {
            MockLms();
            MockSecurityDataContext();
            MockBanStorage();
        }

        private void MockLms()
        {
            _MockLmsService = new Mock<ILmsService>();
        }

        private void MockSecurityDataContext()
        {
            _MockSecurityDataContext = new Mock<ISecurityDataContext>();

            _MockSecurityDataContext.SetupGet(c => c.Computers).Returns(CreateComputers());
            _MockSecurityDataContext.SetupGet(c => c.Rooms).Returns(CreateRooms());
        }

        private void MockBanStorage()
        {
            var context = _MockSecurityDataContext.Object;
            Func<ISecurityDataContext> createSecurityDataContext = () =>
            {
                return context;
            };
            _BanStorage = new DatabaseBanStorage
                (_MockLmsService.Object, createSecurityDataContext);
        }

        private IMockableTable<Computer> CreateComputers()
        {
            var computers = new Computer[]
            {
                new Computer
                {
                    CurrentUser = "lex",
                    IpAddress = "100.100.100.100",
                    Banned = false
                }
            };

            return new MemoryTable<Computer>(computers);
        }

        private IMockableTable<Room> CreateRooms()
        {
            var rooms = new Room[]
            {
                new Room
                {
                    Id = 1,
                    Name = "tester",
                    Allowed = true
                }
            };

            return new MemoryTable<Room>(rooms);
        }
    }
}
