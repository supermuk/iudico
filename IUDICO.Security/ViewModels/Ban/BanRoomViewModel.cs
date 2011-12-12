using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.ViewModels.Ban
{
    public class BanRoomViewModel
    {
        public IList<Room> Rooms { get; set; }

        public BanRoomViewModel()
        {
            Rooms = new List<Room>();
        } 
    }
}