using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class ViewTopicAssignmentModel
    {
        public TopicAssignment TopicAssignment { get; set; }
        public Topic Topic { get; set; }
    }
}