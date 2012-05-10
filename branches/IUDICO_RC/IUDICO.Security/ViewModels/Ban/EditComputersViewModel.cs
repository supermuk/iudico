using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;
using IUDICO.Security.Models.Storages;
using IUDICO.Security.Models.Storages.Database;

namespace IUDICO.Security.ViewModels.Ban
{
    public class EditComputersViewModel
    {
        [LocalizedDisplayName("ComputerIP")]
        public string ComputerIP { get; set; }
        [LocalizedDisplayName("Room")]
        public string Room { get; set; }
        [LocalizedDisplayName("Banned")]
        public bool Banned { get; set; }
        [LocalizedDisplayName("CurrentUser")]
        public string CurrentUser { get; set; }
        public readonly IBanStorage BanStorage;

        public EditComputersViewModel() {
            this.BanStorage = new DatabaseBanStorage();
        }
        public EditComputersViewModel(string cip, string room, bool banned, string curUser) 
        {
            this.ComputerIP = cip;
            this.Room = room;
            this.Banned = banned;
            this.CurrentUser = curUser;
        }

        public EditComputersViewModel(Computer computer)
        {
            this.BanStorage = new DatabaseBanStorage();
            this.ComputerIP = computer.IpAddress;

            if (computer.RoomRef != null)
            {
                var curRoom = this.BanStorage.GetRoom((int)computer.RoomRef);
                this.Room = curRoom.Name;
            }
            
            this.Banned = computer.Banned;

            if (computer.CurrentUser != null)
            {
                this.CurrentUser = computer.CurrentUser;
            }
            
        }
    }
}