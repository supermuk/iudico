using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;

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

        public EditComputersViewModel() { }
        public EditComputersViewModel(string cip, string room, bool banned, string curUser) 
        {
            this.ComputerIP = cip;
            this.Room = room;
            this.Banned = banned;
            this.CurrentUser = curUser;
        }

        public EditComputersViewModel(Computer computer)
        {
            this.ComputerIP = computer.IpAddress;

            if (computer.Room != null)
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