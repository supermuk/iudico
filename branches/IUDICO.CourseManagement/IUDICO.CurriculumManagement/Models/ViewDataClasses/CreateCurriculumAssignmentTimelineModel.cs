﻿using System.Collections.Generic;
using System.Web.Mvc;
using IUDICO.Common.Models;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class CreateCurriculumAssignmentTimelineModel
    {
        public Timeline Timeline { get; set; }

        public CreateCurriculumAssignmentTimelineModel()
        {
        }

        public CreateCurriculumAssignmentTimelineModel(Timeline timeline)
        {
            Timeline = timeline;
        }
    }
}