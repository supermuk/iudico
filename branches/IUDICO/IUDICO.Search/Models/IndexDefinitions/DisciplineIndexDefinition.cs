using IUDICO.Common.Models.Shared;

using Lucene.Net.Documents;
using Lucene.Net.Index;

using SimpleLucene;

namespace IUDICO.Search.Models.IndexDefinitions
{
    public class DisciplineIndexDefinition : IIndexDefinition<Discipline>
    {
        public Document Convert(Discipline entity)
        {
            var document = new Document();

            document.Add(new Field("Type", "Discipline", Field.Store.YES, Field.Index.NO));
            document.Add(new Field("DisciplineID", entity.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Owner", entity.Owner, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("Discipline", entity.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

            return document;
        }

        public Term GetIndex(Discipline entity)
        {
            return new Term("id", "Discipline-" + entity.Id.ToString());
        }
    }
}