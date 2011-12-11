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

namespace IUDICO.Security.Models.Storages.Database
{
    class DatabaseBanStorage : IBanStorage
    {
        private readonly ILmsService _LmsService;

        public DatabaseBanStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
        }

        #region IBanStorage

        public void AttachComputerToRoom(Computer computer, Room room)
        {
            using (var context = NewContext())
            {
                computer.Room = room;
                context.Computers.Attach(computer, true);

                context.SubmitChanges();
            }
        }

        public void DetachComputer(Computer computer)
        {
            using (var context = NewContext())
            {
                computer.Room = null;
                context.Computers.Attach(computer, true);

                context.SubmitChanges();
            }
        }

        public void BanComputer(Computer computer)
        {
            using (var context = NewContext())
            {
                var r = context.Computers.SingleOrDefault(x => x.IpAddress == computer.IpAddress);
                r.Banned = true;
                context.SubmitChanges();
            }
        }

        public void UnbanComputer(Computer computer)
        {
            using (var context = NewContext())
            {
                var r = context.Computers.SingleOrDefault(x => x.IpAddress == computer.IpAddress);
                r.Banned = false;
                context.SubmitChanges();
            }
        }

        public void BanRoom(Room room)
        {
            using (var context = NewContext())
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
            using (var context = NewContext())
            {
                var r = context.Rooms.SingleOrDefault(x => x.Id == room.Id);
                r.Allowed = true;

                context.SubmitChanges();
            }
        }

        public Computer GetComputer(string ipAddress)
        {
            using (var context = NewContext())
            {
                return context.Computers.FirstOrDefault(i => i.IpAddress == ipAddress);
            }
        }

        public Room GetRoom(string name)
        {
            using (var context = NewContext())
            {
                DataLoadOptions opts = new DataLoadOptions();
                opts.LoadWith<Room>(r => r.Computers);
                context.LoadOptions = opts;

                return context.Rooms.FirstOrDefault(room => room.Name == name);
            }
        }

        public IEnumerable<Room> GetRooms()
        {
               return NewContext().Rooms;
        }

        public IEnumerable<Computer> GetComputers()
        {
            return NewContext().Computers;
        }

        public void CreateComputer(Computer computer)
        {
            using (var context = NewContext())
            {
                context.Computers.InsertOnSubmit(computer);
                context.SubmitChanges();
            }
        }

        public void CreateRoom(Room room)
        {
            using (var context = NewContext())
            {
                context.Rooms.InsertOnSubmit(room);
                context.SubmitChanges();
            }
        }

        public void DeleteComputer(Computer computer)
        {
            using (var context = NewContext())
            {
                context.Computers.Attach(computer, true);
                context.Computers.DeleteOnSubmit(computer);

                context.SubmitChanges();
            }
        }

        public void DeleteRoom(Room room)
        {
            using (var context = NewContext())
            {
                context.Rooms.Attach(room, true);
                context.Rooms.DeleteOnSubmit(room);

                context.SubmitChanges();
            }
        }

        public bool ifBanned(string ipAddress)
        {
            var comp = new Computer();
            comp.IpAddress = ipAddress;
            using (var context = NewContext())
            {
                return context.Computers.Contains(comp);
            }
        }

        #endregion

        protected DBDataContext NewContext()
        {
            return new DBDataContext();
        }
    }
}
