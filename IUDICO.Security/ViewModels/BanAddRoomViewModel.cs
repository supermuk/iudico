using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IUDICO.Security.ViewModels
{
    public class BanAddRoomViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(11, ErrorMessage = "Must be less than 11 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Allowed is required")]
        public bool Allowed { get; set; }
    }
}