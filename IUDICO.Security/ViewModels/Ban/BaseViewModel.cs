using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Security.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IUDICO.Security.ViewModels.Ban
{
    public class BaseViewModel
    {
        [HiddenInput(DisplayValue = false)]
        [ScaffoldColumn(false)]
        public ViewModelState State { get; set; }
    }
}