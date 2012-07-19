using System;

using IUDICO.Common.Models.Shared;
using IUDICO.Search.Models.Queries;

using SimpleLucene;

using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace IUDICO.Search.Models.Definitions
{
    public class CourseDefinition : Definition<Course>
    {
        public override DefaultQuery Query
        {
            get
            {
                return new CourseQuery();
            }
        }

        public override Document Convert(Course course)
        {
            var document = new Document();
            document.Add(new Field("Type", "Course", Field.Store.YES, Field.Index.NO));
            document.Add(new Field("CourseID", course.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Name", course.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
            document.Add(new Field("Owner", course.Owner, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

            return document;
        }

        public override Term GetIndex(Course entity)
        {
            return new Term("CourseID", entity.Id.ToString());
        }

        public override Course Convert(Document document)
        {
            var course = new Course
            {
                Id = int.Parse(document.GetValue("CourseID")),
                Name = document.GetValue("Name"),
                Owner = document.GetValue("Owner")
            };

            return course;
        }
    }
}