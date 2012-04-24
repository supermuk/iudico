using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Security.ViewModels.Ban
{
    public class EditComputersViewModel
    {
        public string ComputerIP { get; set; }
        public string Room { get; set; }
        public bool Banned { get; set; }
        public string CurrentUser { get; set; }

        public EditComputersViewModel() { }
        public EditComputersViewModel(string cip, string room, bool banned, string curUser) 
        {
            this.ComputerIP = cip;
            this.Room = room;
            this.Banned = banned;
            this.CurrentUser = curUser;
        }
    }
}