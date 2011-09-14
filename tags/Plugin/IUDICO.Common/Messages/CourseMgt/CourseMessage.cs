using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.Common.Models;
using MvcContrib.PortableAreas;

namespace IUDICO.Common.Messages.CourseMgt
{
    public class CourseMessage : ICommandMessage<CoursesResult>
    {
        public CoursesResult Result { get; set; }

        public CourseMessage() 
            : base()
        {
            Result = new CoursesResult();
        }
    }

    public class CoursesResult : ICommandResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
