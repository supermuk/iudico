using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Search.Models.Queries
{
    public class DisciplineQuery : DefaultQuery
    {
        public DisciplineQuery()
            : base(new[] { "Owner", "Discipline" })
        {
        }
    }
}