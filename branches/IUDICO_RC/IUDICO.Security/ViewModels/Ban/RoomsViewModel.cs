using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IUDICO.Security.ViewModels.Ban
{
    public class RoomsViewModel
    {
        public IList<string> Rooms { get; set; }
        public string CurrentRoom { get; set; }
        public IList<string> Computers { get; set; }
        public IList<string> UnchoosenComputers { get; set; }

        public RoomsViewModel() 
        {
            this.Rooms = new List<string>();
            this.CurrentRoom = null;
            this.Computers = new List<string>();
            this.UnchoosenComputers = new List<string>();
        }
    }
}
