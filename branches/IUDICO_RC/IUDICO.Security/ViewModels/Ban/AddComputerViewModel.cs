using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IUDICO.Security.ViewModels.Ban
{
    public class AddComputerViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "ComputerID is required")]
        [StringLength(15, ErrorMessage = "Must be less than 16 characters")]
        [RegularExpression("\\b(?:\\d{1,3}\\.){3}\\d{1,3}\\b", ErrorMessage = "Not valid IP")]
        public string ComputerIP { get; set; }
    }
}