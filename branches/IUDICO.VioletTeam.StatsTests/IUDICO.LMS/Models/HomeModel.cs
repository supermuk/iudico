using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models;
using Action = IUDICO.Common.Models.Action;
using IUDICO.Common.Models.Shared.CurriculumManagement;

namespace IUDICO.LMS.Models
{
    public class HomeModel
    {
        public Dictionary<IPlugin, IEnumerable<Action>> Actions { get; set; }
        /// <summary>
        /// Gets or sets descriptions of themes that are available for playing.
        /// </summary>
        /// <value>
        /// The themes descriptions.
        /// </value>
        public IEnumerable<ThemeDescription> ThemesDescriptions { get; set; }
    }
}