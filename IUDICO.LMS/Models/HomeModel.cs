using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Action;

namespace IUDICO.LMS.Models
{
    public class HomeModel
    {
        public Dictionary<IPlugin, IEnumerable<IAction>> Actions { get; set; }
        /// <summary>
        /// Gets or sets descriptions of themes that are available for playing.
        /// </summary>
        /// <value>
        /// The themes descriptions.
        /// </value>
        public IEnumerable<ThemeDescription> ThemesDescriptions { get; set; }
    }
}