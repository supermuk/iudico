using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using IUDICO.Common;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.ViewModels.Ban
{
    
    public class AddComputerViewModel : BaseViewModel
    {
        [LocalizedRequired(ErrorMessage = "CompIDIsRequired")]
        [LocalizedStringLength(15, ErrorMessage = "Must be less than 16 characters")]
        [LocalizedRegularExpression("\\b(?:\\d{1,3}\\.){3}\\d{1,3}\\b", ErrorMessage = "NotValidIP")]
        [LocalizedDisplayName("ComputerIP")]
        public string ComputerIP { get; set; }
    }
}