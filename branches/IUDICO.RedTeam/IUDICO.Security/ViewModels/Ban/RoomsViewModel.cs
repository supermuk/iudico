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
            Rooms = new List<String> { "Room1", "Room2", "Room3", "Room4", "Room5" };
            CurrentRoom = "Room1";
            Computers = new List<String> { "Comp1", "Comp2", "Comp3", "Comp4", "Comp5" };
            UnchoosenComputers = new List<String> { "Comp6", "Comp7", "Comp8", "Comp9", "Comp10" };
        }
    }
}
