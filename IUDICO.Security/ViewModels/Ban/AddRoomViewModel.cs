using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.ViewModels.Ban
{
    public class AddRoomViewModel
    {
        [LocalizedRequired(ErrorMessage = "Name is required")]
        [LocalizedStringLength(11, ErrorMessage = "Must be less than 11 characters")]
        [LocalizedDisplayName("Name")]
        public string Name { get; set; }
        [LocalizedDisplayName("Allowed")]
        [LocalizedRequired(ErrorMessage = "Allowed is required")]
        public bool Allowed { get; set; }
    }
}
