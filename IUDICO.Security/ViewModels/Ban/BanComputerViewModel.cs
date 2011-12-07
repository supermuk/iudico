using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using IUDICO.Common.Models;

namespace IUDICO.Security.ViewModels.Ban
{
    public class BanComputerViewModel
    {
        public IList<Computer> Computers { get; set; }

        public BanComputerViewModel()
        {
            Computers = new List<Computer>();
        }
    }
}