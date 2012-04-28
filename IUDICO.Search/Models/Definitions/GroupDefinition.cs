using System;

using IUDICO.Common.Models.Shared;
using IUDICO.Search.Models.Queries;

using Lucene.Net.Documents;
using Lucene.Net.Index;

using SimpleLucene;

namespace IUDICO.Search.Models.Definitions
{
    public class GroupDefinition : Definition<Group>
    {
        public GroupDefinition()
        {
            this.Query = new GroupQuery();
        }

        public override Document Convert(Group entity)
        {
            var document = new Document();

            document.Add(new Field("ID", entity.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Group", entity.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

            return document;
        }

        public override Term GetIndex(Group entity)
        {
            return new Term("id", entity.Id.ToString());
        }

        public override Group Convert(Document document)
        {
            var group = new Group
                {
                    Id = int.Parse(document.Get("ID")),
                    Name = document.Get("Group")
                };

            return group;
        }
    }
}