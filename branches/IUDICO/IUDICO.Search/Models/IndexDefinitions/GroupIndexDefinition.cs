using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;

using Lucene.Net.Documents;
using Lucene.Net.Index;

using SimpleLucene;

namespace IUDICO.Search.Models.IndexDefinitions
{
    public class GroupIndexDefinition : IIndexDefinition<Group>
    {
        public Document Convert(Group entity)
        {
            var document = new Document();

            document.Add(new Field("ID", entity.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Group", entity.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

            return document;
        }

        public Term GetIndex(Group entity)
        {
            return new Term("id", entity.Id.ToString());
        }
    }
}