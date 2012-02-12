using System.Collections.Generic;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class CreateCurriculumTimelineModel
    {
        public Timeline Timeline { get; set; }

        public CreateCurriculumTimelineModel()
        {
        }

        public CreateCurriculumTimelineModel(Timeline timeline)
        {
            Timeline = timeline;
        }
    }
}