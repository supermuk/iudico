using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Shared;
using System.Data.Linq;
using System.Diagnostics;

namespace IUDICO.Security.Models.Storages.Database
{
    public class DatabaseBanStorage : IBanStorage
    {
        protected readonly ILmsService LmsService;
        private readonly Func<ISecurityDataContext> CreateIDataContext;
        protected readonly LinqLogger Logger;

        public DatabaseBanStorage()
        {
            this.CreateIDataContext = () =>
            {
                return new DBDataContext();
            };
        }

        public DatabaseBanStorage(ILmsService lmsService, LinqLogger logger)
        {
            this.LmsService = lmsService;
            this.Logger = logger;
            this.CreateIDataContext = () =>
                {
                    var db = new DBDataContext();

#if DEBUG
                    db.Log = logger;
#endif

                    return db;
                };
        }

        public DatabaseBanStorage(ILmsService lmsService, Func<ISecurityDataContext> createIDataContext)
        {
            this.LmsService = lmsService;
            this.CreateIDataContext = createIDataContext;
        }

        #region IBanStorage

        public void AttachComputerToRoom(Computer computer, Room room)
        {
            using (var context = this.NewContext())
            {
                var attachment =
                    context.RoomAttachments.FirstOrDefault(a => (a.ComputerIp == computer.IpAddress) && (a.RoomId == room.Id));

                if(attachment == null)
                {
                    context.RoomAttachments.InsertOnSubmit(new RoomAttachment {ComputerIp = computer.IpAddress, RoomId = room.Id});

                    context.SubmitChanges();
                }
            }
        }

        public void DetachComputer(Computer computer)
        {
            using (var context = this.NewContext())
            {
                var attachment = context.RoomAttachments.SingleOrDefault(a => a.ComputerIp == computer.IpAddress);

                if(attachment != null)
                {
                    context.RoomAttachments.DeleteOnSubmit(attachment);

                    context.SubmitChanges();
                }
            }
        }

        public void BanComputer(Computer computer)
        {
            using (var context = this.NewContext())
            {
                var r = context.Computers.SingleOrDefault(x => x.IpAddress == computer.IpAddress);
                r.Banned = true;
                context.SubmitChanges();
            }
        }

        public void UnbanComputer(Computer computer)
        {
            using (var context = this.NewContext())
            {
                var r = context.Computers.SingleOrDefault(x => x.IpAddress == computer.IpAddress);
                r.Banned = false;
                context.SubmitChanges();
            }
        }

        public void EditComputer(string ip, bool banned, string currentUser)
        {
            using (var context = this.NewContext())
            {
                var computer = context.Computers.SingleOrDefault(c => c.IpAddress == ip);

                if(computer != null)
                {
                    computer.Banned = banned;
                    computer.CurrentUser = currentUser;
                }

                context.SubmitChanges();
            }
        }

        public void BanRoom(Room room)
        {
            using (var context = this.NewContext())
            {
                var computers = context.RoomAttachments.Where(a => a.RoomId == room.Id).Select(a => a.ComputerIp);

                computers.ForEach(c => context.Computers.Single(x => x.IpAddress == c).Banned = true);

                var room1 = context.Rooms.Single(r => r.Id == room.Id);
                room1.Allowed = false;

                context.SubmitChanges();
            }
        }

        public void UnbanRoom(Room room)
        {
            using (var context = this.NewContext())
            {
                var computers = context.RoomAttachments.Where(a => a.RoomId == room.Id).Select(a => a.ComputerIp);

                computers.ForEach(c => context.Computers.Single(x => x.IpAddress == c).Banned = false);

                var room1 = context.Rooms.Single(r => r.Id == room.Id);
                room1.Allowed = true;

                context.SubmitChanges();
            }
        }

        public Computer GetComputer(string compAddress)
        {
            using (var context = this.NewContext())
            {
                return context.Computers.FirstOrDefault(i => i.IpAddress == compAddress);
            }
        }

        public Room GetRoom(string name)
        {
            using (var context = this.NewContext())
            {
                return context.Rooms.FirstOrDefault(room => room.Name == name);
            }
        }

        public Room GetRoom(int id)
        {
            using (var context = this.NewContext())
            {
                return context.Rooms.FirstOrDefault(room => room.Id == id);
            }
        }

        public Room GetRoom(Computer computer)
        {
            using (var context = this.NewContext())
            {
                var attachment = context.RoomAttachments.SingleOrDefault(a => a.ComputerIp == computer.IpAddress);

                if(attachment != null)
                {
                    return context.Rooms.SingleOrDefault(r => r.Id == attachment.RoomId);
                }

                return null;
            }
        }

        public RoomAttachment GetAttachment(string computerIp)
        {
            using (var context = this.NewContext())
            {
                return context.RoomAttachments.SingleOrDefault(a => a.ComputerIp == computerIp);
            }
        }

        public RoomAttachment GetAttachment(Computer computer)
        {
            using (var context = this.NewContext())
            {
                return context.RoomAttachments.SingleOrDefault(a => a.ComputerIp == computer.IpAddress);
            }
        }

        public IEnumerable<Room> GetRooms()
        {
            return this.NewContext().Rooms;
        }

        public IEnumerable<Computer> ComputersAttachedToRoom(Room room)
        {
            using (var context =this.NewContext())
            {
                return
                    context.RoomAttachments.Where(a => a.RoomId == room.Id).Select(
                        a => context.Computers.SingleOrDefault(c => c.IpAddress == a.ComputerIp));
            }
        }

        public IEnumerable<RoomAttachment> GetRoomAttachments()
        {
            return this.NewContext().RoomAttachments;
        }

        public IEnumerable<Computer> GetComputers()
        {
            return this.NewContext().Computers;
        }

        public void CreateComputer(Computer computer)
        {
            using (var context = this.NewContext())
            {
                context.Computers.InsertOnSubmit(computer);
                context.SubmitChanges();
            }
        }

        public void CreateRoom(Room room)
        {
            using (var context = this.NewContext())
            {
                context.Rooms.InsertOnSubmit(room);
                context.SubmitChanges();
            }
        }

        public void DeleteComputer(Computer computer)
        {
            using (var context = this.NewContext())
            {
                var ccomputer = context.Computers.FirstOrDefault(c => c.IpAddress == computer.IpAddress);
                if (ccomputer != null)
                {
                    context.Computers.DeleteOnSubmit(ccomputer);
                }

                context.SubmitChanges();
            }
        }

        public void DeleteRoom(Room room)
        {
            using (var context = this.NewContext())
            {
                var rroom = context.Rooms.FirstOrDefault(c => c.Name == room.Name);
                if (rroom != null)
                {
                    context.Rooms.DeleteOnSubmit(rroom);
                }

                context.SubmitChanges();
            }
        }

        public bool IfBanned(string compAddress)
        {
            using (var context = this.NewContext())
            {
                return context.Computers.Where(c => c.IpAddress == compAddress && c.Banned == true).Count() > 0;
            }
        }

        #endregion

        protected ISecurityDataContext NewContext()
        {
            return this.CreateIDataContext();
        }
    }
}
