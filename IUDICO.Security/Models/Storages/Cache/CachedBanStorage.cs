using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Caching;

namespace IUDICO.Security.Models.Storages.Cache
{
    public class CachedBanStorage : IBanStorage
    {
        private readonly IBanStorage storage;
        private readonly ICacheProvider cachePrvoider;
        private readonly object lockObject = new object();

        public CachedBanStorage(IBanStorage storage, ICacheProvider cachePrvoider)
        {
            this.storage = storage;
            this.cachePrvoider = cachePrvoider;
        }

        public void AttachComputerToRoom(Computer computer, Room room)
        {
            this.storage.AttachComputerToRoom(computer, room);

            this.cachePrvoider.Invalidate("computers", "rooms", "computer-" + computer.IpAddress);
        }

        public void DetachComputer(Computer computer)
        {
            this.cachePrvoider.Invalidate("computers", "rooms", "computer-" + computer.IpAddress); 
            
            this.storage.DetachComputer(computer);
        }

        public void BanComputer(Computer computer)
        {
            this.storage.BanComputer(computer);

            this.cachePrvoider.Invalidate("computers", "computer-" + computer.IpAddress);
        }

        public void UnbanComputer(Computer computer)
        {
            this.storage.UnbanComputer(computer);

            this.cachePrvoider.Invalidate("computers", "computer-" + computer.IpAddress);
        }

        public void EditComputer(string ip, bool banned, string currentUser)
        {
            this.storage.EditComputer(ip, banned, currentUser);

            this.cachePrvoider.Invalidate("computers", "computer-" + ip);
        }

        public void BanRoom(Room room)
        {
            this.storage.BanRoom(room);

            //REDO
            var computers = this.storage.GetComputers().Select(s => "computer-" + s.IpAddress);

            this.cachePrvoider.Invalidate(computers.ToArray());
            this.cachePrvoider.Invalidate("computers", "room-" + room.Name, "rooms");
        }

        public void UnbanRoom(Room room)
        {
            this.storage.UnbanRoom(room);

            //REDO
            var computers = this.storage.GetComputers().Select(s => "computer-" + s.IpAddress);

            this.cachePrvoider.Invalidate(computers.ToArray());
            this.cachePrvoider.Invalidate("computers", "room-" + room.Name, "rooms");
        }

        public Computer GetComputer(string compAddress)
        {
            return this.cachePrvoider.Get<Computer>("computer-" + compAddress, @lockObject, () => this.storage.GetComputer(compAddress), DateTime.Now.AddDays(1), "computer-" + compAddress, "computers");
        }

        public Room GetRoom(string name)
        {
            return this.cachePrvoider.Get<Room>("room-" + name, @lockObject, () => this.storage.GetRoom(name), DateTime.Now.AddDays(1), "room-" + name, "rooms");
        }

        public Room GetRoom(int id)
        {
            return this.cachePrvoider.Get<Room>("room-" + id, @lockObject, () => this.storage.GetRoom(id), DateTime.Now.AddDays(1), "room-" + id, "rooms");
        }

        public Room GetRoom(Computer computer)
        {
            return this.storage.GetRoom(computer);
        }

        public RoomAttachment GetAttachment(string computerIp)
        {
            return this.storage.GetAttachment(computerIp);
        }

        public RoomAttachment GetAttachment(Computer computer)
        {
            return this.storage.GetAttachment(computer);
        }

        public IEnumerable<Computer> GetComputers()
        {
            return this.cachePrvoider.Get<IEnumerable<Computer>>("computers", @lockObject, () => this.storage.GetComputers().ToList(), DateTime.Now.AddDays(1), "computers");
        }

        public IEnumerable<Room> GetRooms()
        {
            return this.cachePrvoider.Get<IEnumerable<Room>>("rooms", @lockObject, () => this.storage.GetRooms().ToList(), DateTime.Now.AddDays(1), "rooms");
        }

        public IEnumerable<Computer> ComputersAttachedToRoom(Room room)
        {
            return this.storage.ComputersAttachedToRoom(room);
        }

        public IEnumerable<RoomAttachment> GetRoomAttachments()
        {
            return this.storage.GetRoomAttachments();
        }

        public void CreateComputer(Computer computer)
        {
            this.storage.CreateComputer(computer);

            this.cachePrvoider.Invalidate("computer-" + computer.IpAddress, "computers");
        }

        public void CreateRoom(Room room)
        {
            this.storage.CreateRoom(room);

            this.cachePrvoider.Invalidate("rooms", "room-" + room.Name);
        }

        public void DeleteComputer(Computer computer)
        {
            this.storage.DeleteComputer(computer);

            this.cachePrvoider.Invalidate("computers", "computer-" + computer.IpAddress);
        }

        public void DeleteRoom(Room room)
        {
            this.storage.DeleteRoom(room);

            this.cachePrvoider.Invalidate("rooms", "room-" + room.Name);
        }

        public bool IfBanned(string compAddress)
        {
            var computer = this.GetComputer(compAddress);

            return computer == null ? false : computer.Banned;
        }
    }
}