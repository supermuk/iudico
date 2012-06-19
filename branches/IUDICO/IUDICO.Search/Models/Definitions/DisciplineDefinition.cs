using IUDICO.Common.Models.Shared;
using IUDICO.Search.Models.Queries;

using Lucene.Net.Documents;
using Lucene.Net.Index;

using SimpleLucene;

namespace IUDICO.Search.Models.Definitions
{
    public class DisciplineDefinition : Definition<Discipline>
    {
        public override DefaultQuery Query
        {
            get
            {
                return new DisciplineQuery();
            }
        }

        public override Document Convert(Discipline entity)
        {
            var document = new Document();

            // document.Add(new Field("Type", "Discipline", Field.Store.YES, Field.Index.NO));
            document.Add(new Field("ID", entity.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Owner", entity.Owner, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("Discipline", entity.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

            return document;
        }

        public override Term GetIndex(Discipline entity)
        {
            return new Term("ID", entity.Id.ToString());
        }

        public override Discipline Convert(Document document)
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