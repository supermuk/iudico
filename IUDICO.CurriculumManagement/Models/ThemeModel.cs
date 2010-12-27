using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;

namespace IUDICO.CurriculumManagement.Models
{
    public class ThemeModel
    {
        public IEnumerable<SelectListItem> Courses { get; set; }
        public IEnumerable<Theme> Themes { get; set; }
        public int CourseId { get; set; }
        public int StageId { get; set; }
        public int ThemeId { get; set; }
    }
}