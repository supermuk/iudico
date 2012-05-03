using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.ViewModels.Ban
{
    public class RoomsViewModel
    {
        [LocalizedDisplayName("Rooms")]
        public IList<string> Rooms { get; set; }
        [LocalizedDisplayName("CurrentRoom")]
        public string CurrentRoom { get; set; }
        [LocalizedDisplayName("Computers")]
        public IList<string> Computers { get; set; }
        [LocalizedDisplayName("UnchoosenComputers")]
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
