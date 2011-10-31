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
    }
}