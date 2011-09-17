using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class CreateStageTimelineModel
    {
        public IEnumerable<SelectListItem> Stages { get; set; }
        public int StageId { get; set; }
        public Timeline Timeline { get; set; }

        public CreateStageTimelineModel()
        {
        }

        public CreateStageTimelineModel(Timeline timeline, IEnumerable<Stage> stages, int stageId)
        {
            Stages = stages
                    .Select(item => new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = false
                    });
            Timeline = timeline;
            StageId = stageId;
        }
    }
}