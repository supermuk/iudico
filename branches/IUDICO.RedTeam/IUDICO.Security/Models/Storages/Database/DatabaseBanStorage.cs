using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Interfaces;

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
            throw new NotImplementedException();
        }
        public void DetachComputer(Computer computer)
        {
            throw new NotImplementedException();
        }
        public void BanComputer(Computer computer)
        {
            throw new NotImplementedException();
        }
        public void UnbanComputer(Computer computer)
        {
            throw new NotImplementedException();
        }

        public Computer GetComputer(string ipAddress)
        {
            throw new NotImplementedException();
        }
        public Room GetRoom(string name)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Computer> GetComputers()
        {
           return NewContext().Computers;
        }
        public IEnumerable<Room> GetRooms()
        {
            return NewContext().Rooms;
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
            throw new NotImplementedException();
        }
        public void DeleteRoom(Room room)
        {
            throw new NotImplementedException();
        }
        #endregion

        protected IDataContext NewContext()
        {
            return _LmsService.GetIDataContext();
        }
    }
}
