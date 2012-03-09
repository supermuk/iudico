using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using System.Reflection;
using IUDICO.Common.Models.Shared;
using System.ComponentModel;
using IUDICO.Common.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class ViewTopicModel
    {
        #region Properties

        public int Id { get; set; }
        public string TopicName { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public string TestTopicType { get; set; }
        public string TestCourseName { get; set; }
        public string TheoryTopicType { get; set; }
        public string TheoryCourseName { get; set; }

        #endregion
    }
}