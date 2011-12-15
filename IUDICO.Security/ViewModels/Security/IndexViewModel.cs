using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Security.ViewModels.Security
{
    public class IndexViewModel : LocalizedViewModel
    {
        public IndexViewModel()
            : base(Localization.GetProvider())
        {
        }
    }
}