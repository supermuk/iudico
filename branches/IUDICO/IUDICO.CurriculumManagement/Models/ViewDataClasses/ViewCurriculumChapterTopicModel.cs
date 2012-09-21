using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class ViewCurriculumChapterTopicModel
    {
        public int Id { get; set; }
        public int ThresholdOfSuccess { get; set; }
        public bool BlockTopicAtTesting { get; set; }
        public bool BlockCurriculumAtTesting { get; set; }
        public string TestStartDate { get; set; }
        public string TestEndDate { get; set; }
        public string TheoryStartDate { get; set; }
        public string TheoryEndDate { get; set; }
        public string TopicName { get; set; }
    }
}