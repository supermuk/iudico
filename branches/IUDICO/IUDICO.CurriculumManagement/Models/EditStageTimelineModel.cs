using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;

namespace IUDICO.CurriculumManagement.Models
{
    public class EditStageTimelineModel
    {
        public IEnumerable<SelectListItem> Operations { get; set; }
        public int OperationId { get; set; }
        public IEnumerable<SelectListItem> Stages { get; set; }
        public int StageId { get; set; }
        public Timeline Timeline { get; set; }
    }
}