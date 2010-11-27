using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Messages.CourseMgt
{
    public class GetNodesMessage : CourseMessage
    {
        public new GetNodesInput Input { get; set; }
    }

    public class GetNodesInput
    {
        public int CourseId { get; set; }
        public int? ParentId { get; set; }
    }
}
