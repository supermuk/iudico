using System;

using IUDICO.Common.Models.Shared;
using IUDICO.Search.Models.Queries;

using SimpleLucene;

using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace IUDICO.Search.Models.Definitions
{
    public class UserDefinition : Definition<User>
    {
        public override DefaultQuery Query
        {
            get
            {
                return new UserQuery();
            }
        }

        public override Document Convert(User entity)
        {
            var document = new Document();

            // document.Add(new Field("Type", "User", Field.Store.YES, Field.Index.NO));
            document.Add(new Field("ID", entity.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Name", entity.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
            document.Add(new Field("Username", entity.Username, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

            return document;
        }

        public override Term GetIndex(User entity)
        {
            return new Term("ID", entity.Id.ToString());
        }

        public override User Convert(Document document)
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