using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;

namespace IUDICO.CurriculumManagement.Models
{
    public class CreateThemeModel
    {
        public IEnumerable<SelectListItem> Courses { get; set; }
        public IEnumerable<SelectListItem> ThemeTypes { get; set; }
        public int CourseId { get; set; }
        public int StageId { get; set; }
        public int ThemeId { get; set; }
        public int ThemeTypeId{get;set;}
    }
}