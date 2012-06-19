using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Search.Models.Queries
{
    public class CourseQuery : DefaultQuery
    {
        public CourseQuery()
            : base(new[] { "Name", "Owner" })
        {

        }
    }
}