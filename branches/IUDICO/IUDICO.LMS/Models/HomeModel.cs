using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models;
using Action = IUDICO.Common.Models.Action;

namespace IUDICO.LMS.Models
{
    public class HomeModel
    {
        public Dictionary<IPlugin, IEnumerable<Action>> Actions { get; set; }
        public IEnumerable<Theme> AvailableThemes { get; set; }
    }
}