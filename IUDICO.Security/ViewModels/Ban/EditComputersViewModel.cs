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

        public IEnumerable<string> AllRooms { get; set; } 

        public EditComputersViewModel() {
        }
        public EditComputersViewModel(string cip, string room, bool banned, string curUser, IEnumerable<string> rooms ) 
        {
            this.ComputerIP = cip;
            this.Room = room;
            this.Banned = banned;
            this.CurrentUser = curUser;
            this.AllRooms = rooms;
        }

        public EditComputersViewModel(Computer computer)
        {
            this.ComputerIP = computer.IpAddress;

            if (computer.RoomRef != null)
            {
                this.Room = computer.Room.Name;
            }
            
            this.Banned = computer.Banned;

            if (computer.CurrentUser != null)
            {
                this.CurrentUser = computer.CurrentUser;
            }
            
        }
    }
}