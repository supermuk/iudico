using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class ViewThemeAssignmentModel
    {
        public ThemeAssignment ThemeAssignment { get; set; }
        public Theme Theme { get; set; }
    }
}