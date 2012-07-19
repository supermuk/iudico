using System;

using IUDICO.Common.Models.Shared;
using IUDICO.Search.Models.Queries;

using SimpleLucene;

using Lucene.Net.Documents;
using Lucene.Net.Index;
using IUDICO.Common.Models.Services;


namespace IUDICO.Search.Models.Definitions
{
    public class NodeDefinition : Definition<Node>
    {
        public override DefaultQuery Query
        {
            get
            {
                return new CourseQuery();
            }
        }

        public ICourseService CourseService
        {
            get;
            set;
        }

        public override Document Convert(Node node)
        {
            var document = new Document();
            document.Add(new Field("Type", "Node", Field.Store.YES, Field.Index.NO));
            document.Add(new Field("NodeID", node.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Name", node.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
            document.Add(new Field("NodeCourseID", node.CourseId.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("isFolder", node.IsFolder.ToString(), Field.Store.YES, Field.Index.ANALYZED));

            if (!node.IsFolder)
            {
                var content = this.CourseService.GetNodeContents(node.Id);

                document.Add(new Field("Content", content, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
            }

            return document;
        }

        public override Term GetIndex(Node entity)
        {
            return new Term("NodeID", entity.Id.ToString());
        }

        public override Node Convert(Document document)
        {
            var node = new Node
            {
                Id = int.Parse(document.GetValue("NodeID")),
                Name = document.GetValue("Name"),
                CourseId = int.Parse(document.GetValue("NodeCourseID")),
                IsFolder = bool.Parse(document.GetValue("isFolder"))
            };

            return node;
        }
    }
}