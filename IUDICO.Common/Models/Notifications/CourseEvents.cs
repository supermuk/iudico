using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Events
{
    public class CourseEvents
    {
        /// <summary>
        /// Course Published event is fired when course was published. Course object is passed as the first parameter.
        /// </summary>
        public const string CoursePublished = "course/publish/";
    }
}
