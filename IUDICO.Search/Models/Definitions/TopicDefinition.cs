using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;
using IUDICO.Search.Models.Queries;

using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace IUDICO.Search.Models.Definitions
{
    public class TopicDefinition : Definition<Topic>
    {
        public TopicDefinition()
        {
            this.Query = new DefaultQuery(new[] { "Topic" });
        }

        public override Document Convert(Topic entity)
        {
            var document = new Document();

            document.Add(new Field("ID", entity.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Topic", entity.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

            document.Add(
                entity.TestCourseRef == null
                    ? new Field("CourseRef", "null", Field.Store.YES, Field.Index.NO)
                    : new Field("CourseRef", entity.TestCourseRef.ToString(), Field.Store.YES, Field.Index.NO));

            return document;
        }

        public override Term GetIndex(Topic entity)
        {
            return new Term("ID", entity.Id.ToString());
        }

        public override Topic Convert(Document document)
        {
            var topic = new Topic
                {
                    Id = int.Parse(document.Get("ID")),
                    Name = document.Get("Topic"),
                    TestCourseRef = document.Get("CourseRef") == null ? (int?)null : int.Parse(document.Get("CourseRef"))
                };

            return topic;
        }
    }
}