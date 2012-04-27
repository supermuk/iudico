using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using IUDICO.Common.Models.Shared;
using IUDICO.Search.Models.Queries;

using SimpleLucene.Impl;
using SimpleLucene.IndexManagement;

namespace IUDICO.Search.Models.SearchTypes
{
    public class UserSearchType : SearchType<User>
    {
        public override void Search(string keywords)
        {
            var query = this.GetQuery() as UserQuery;

            query.WithKeywords()

        }
    }
}