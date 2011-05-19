﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class ViewStageTimelineModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StageName { get; set; }
    }
}