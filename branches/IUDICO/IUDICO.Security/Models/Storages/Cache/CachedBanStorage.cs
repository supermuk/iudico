using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Caching;
using System.Diagnostics;

namespace IUDICO.Security.Models.Storages.Cache
{
    public class CachedBanStorage: IBanStorage
    {
        private readonly IBanStorage _storage;
        private readonly ICacheProvider _cachePrvoider;
        private readonly object lockObject = new object();

        public CachedBanStorage(IBanStorage storage, ICacheProvider cachePrvoider)
        {
            _storage = storage;
            _cachePrvoider = cachePrvoider;
        }

        public void AttachComputerToRoom(Computer computer, Room room)
        {
            _storage.AttachComputerToRoom(computer, room);

            _cachePrvoider.Invalidate("computers", "rooms", "computer-" + computer.IpAddress, "room-" + computer.Room.Name);
        }

        public void DetachComputer(Computer computer)
        {
            _cachePrvoider.Invalidate("computers", "rooms", "computer-" + computer.IpAddress, "room-" + computer.Room.Name);

            _storage.DetachComputer(computer);
        }

        public void BanComputer(Computer computer)
        {
            _storage.BanComputer(computer);

            _cachePrvoider.Invalidate("computers", "computer-" + computer.IpAddress);
        }

        public void UnbanComputer(Computer computer)
        {
            _storage.BanComputer(computer);

            _cachePrvoider.Invalidate("computers", "computer-" + computer.IpAddress);
        }

        public void BanRoom(Room room)
        {
            _storage.BanRoom(room);

            var computers = room.Computers.Select(s => "computer-" + s.IpAddress);

            _cachePrvoider.Invalidate(computers.ToArray());
            _cachePrvoider.Invalidate("computers", "room-" + room.Name, "rooms");
        }

        public void UnbanRoom(Room room)
        {
            _storage.UnbanRoom(room);

            var computers = room.Computers.Select(s => "computer-" + s.IpAddress);

            _cachePrvoider.Invalidate(computers.ToArray());
            _cachePrvoider.Invalidate("computers", "room-" + room.Name, "rooms");
        }

        public Computer GetComputer(string ipAddress)
        {
            return _cachePrvoider.Get<Computer>("computer-" + ipAddress, @lockObject, () => _storage.GetComputer(ipAddress), DateTime.Now.AddDays(1), "computer-" + ipAddress, "computers");
        }

        public Room GetRoom(string name)
        {
            return _cachePrvoider.Get<Room>("room-" + name, @lockObject, () => _storage.GetRoom(name), DateTime.Now.AddDays(1), "room-" + name, "rooms");
        }

        public IEnumerable<Computer> GetComputers()
        {
            return _cachePrvoider.Get<IEnumerable<Computer>>("computers", @lockObject, () => _storage.GetComputers(), DateTime.Now.AddDays(1), "computers");
        }

        public IEnumerable<Room> GetRooms()
        {
            return _cachePrvoider.Get<IEnumerable<Room>>("rooms", @lockObject, () => _storage.GetRooms(), DateTime.Now.AddDays(1), "rooms");
        }

        public void CreateComputer(Computer computer)
        {
            _storage.CreateComputer(computer);

            _cachePrvoider.Invalidate("computer-" + computer.IpAddress, "computers");
        }

        public void CreateRoom(Room room)
        {
            _storage.CreateRoom(room);

            _cachePrvoider.Invalidate("rooms", "room-" + room.Name);
        }

        public void DeleteComputer(Computer computer)
        {
            _storage.DeleteComputer(computer);

            _cachePrvoider.Invalidate("computers", "computer-" + computer.IpAddress);
        }

        public void DeleteRoom(Room room)
        {
            _storage.DeleteRoom(room);

            _cachePrvoider.Invalidate("rooms", "room-" + room.Name);
        }

        public bool ifBanned(string ipAddress)
        {
            var computer = GetComputer(ipAddress);

            return computer == null ? false : computer.Banned;
        }
    }
}