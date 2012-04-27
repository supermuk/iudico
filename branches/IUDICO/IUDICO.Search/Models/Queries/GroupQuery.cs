using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Search.Models.Queries
{
    public class GroupQuery : DefaultQuery
    {
        public GroupQuery()
            : base(new[] { "Group" })
        {
        }
    }
}