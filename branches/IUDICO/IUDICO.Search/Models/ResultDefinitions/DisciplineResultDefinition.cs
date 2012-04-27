using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Lucene.Net.Documents;

using SimpleLucene;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Search.Models.ResultDefinitions
{
    public class DisciplineResultDefinition : IResultDefinition<Discipline>
    {
        public Discipline Convert(Document document)
        {
            var discipline = new Discipline
                {
                    Id = int.Parse(document.GetValue("ID")),
                    Owner = document.Get("Owner"),
                    Name = document.Get("Discipline")
                };

            return discipline;
        }
    }
}