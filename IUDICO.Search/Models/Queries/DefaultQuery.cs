using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Util;

using SimpleLucene.Impl;

namespace IUDICO.Search.Models.Queries
{
    public class DefaultQuery : QueryBase
    {
        protected readonly string[] SearchFields;

        public DefaultQuery()
        {
        }

        public DefaultQuery(string[] searchFields)
        {
            this.SearchFields = searchFields;
        }

        public DefaultQuery WithKeywords(string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var parser = new MultiFieldQueryParser(
                    Version.LUCENE_29, this.SearchFields, new StandardAnalyzer(Version.LUCENE_29));
                Query multiQuery = parser.Parse(keywords);

                this.AddQuery(multiQuery);
            }
            return this;
        }
    }
}