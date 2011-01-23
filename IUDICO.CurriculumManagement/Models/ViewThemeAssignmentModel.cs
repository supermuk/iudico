using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;

namespace IUDICO.CurriculumManagement.Models
{
    public class ViewThemeAssignmentModel
    {
        public ThemeAssignment ThemeAssignment { get; set; }
        public Theme Theme { get; set; }
    }
}