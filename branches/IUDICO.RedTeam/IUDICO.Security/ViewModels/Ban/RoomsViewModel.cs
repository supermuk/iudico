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
        public IList<String> Rooms { get; set; }
        public String CurrentRoom { get; set; }
        public IList<String> Computers { get; set; }
        public IList<String> UnchoosenComputers { get; set; }

        public RoomsViewModel() 
        {
            Rooms = new List<String>();
            CurrentRoom = null;
            Computers = new List<String>();
            UnchoosenComputers = new List<String>();
        }
    }
}
