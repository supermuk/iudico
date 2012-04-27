using IUDICO.Common.Models.Shared;

using SimpleLucene;

using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace IUDICO.Search.Models.IndexDefinitions
{
    public class UserIndexDefinition : IIndexDefinition<User>
    {
        public Document Convert(User entity)
        {
            var document = new Document();

            // document.Add(new Field("Type", "User", Field.Store.YES, Field.Index.NO));
            document.Add(new Field("ID", entity.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Name", entity.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
            document.Add(new Field("Username", entity.Username, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

            return document;
        }

        public Term GetIndex(User entity)
        {
            return new Term("id", "User-" + entity.Id.ToString());
        }
    }
}