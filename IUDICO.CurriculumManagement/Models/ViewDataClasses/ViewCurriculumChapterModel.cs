using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class ViewCurriculumChapterModel
    {
        public int Id { get; set; }
        public int CurriculumRef { get; set; }
        public bool HaveTimelines { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ChapterName { get; set; }
    }
}