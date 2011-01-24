using System.Collections.Generic;
using System.Web.Mvc;
using IUDICO.Common.Models;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class CreateCurriculumAssignmentTimelineModel
    {
        public IEnumerable<SelectListItem> Operations { get; set; }
        public int OperationId { get; set; }
        public Timeline Timeline { get; set; }
    }
}