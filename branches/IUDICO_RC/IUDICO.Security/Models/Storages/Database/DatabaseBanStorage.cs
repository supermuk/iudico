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
                var curRoom = GetRoom(context, room.Name);
                var curComp = context.Computers.SingleOrDefault(i => i.IpAddress == computer.IpAddress);
                curComp.RoomRef = curRoom.Id;
                context.SubmitChanges();                
            }
        }

        public void DetachComputer(Computer computer)
        {
            using (var context = this.NewContext())
            {
                var curComp = context.Computers.FirstOrDefault(i => i.IpAddress == computer.IpAddress);
                curComp.RoomRef = null;
                context.SubmitChanges();
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
                var r = context.Rooms.SingleOrDefault(x => x.Id == room.Id);
                r.Allowed = false;

                foreach (Computer comp in r.Computers)
                    comp.Banned = true;
                    
                context.SubmitChanges();
            }
        }

        public void UnbanRoom(Room room)
        {
            using (var context = this.NewContext())
            {
                var r = context.Rooms.SingleOrDefault(x => x.Id == room.Id);
                r.Allowed = true;

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

        public Room GetRoom(ISecurityDataContext context, string name)
        {   
                return context.Rooms.FirstOrDefault(room => room.Name == name);            
        }

        public IEnumerable<Room> GetRooms()
        {
            return this.NewContext().Rooms;
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
