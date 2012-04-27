using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IUDICO.Common.Models.Shared;

using Lucene.Net.Documents;

using SimpleLucene;

namespace IUDICO.Search.Models.ResultDefinitions
{
    public class UserResultDefinition : IResultDefinition<User>
    {
        public User Convert(Document document)
        {
            var user = new User
                {
                    Id = Guid.Parse(document.GetValue("ID")),
                    Name = document.GetValue("Name"),
                    Username = document.GetValue("Username")
                };

            return user;
        }
    }
}