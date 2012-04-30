using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IUDICO.Common.Models.Shared;
using IUDICO.Search.Models.Queries;

using Lucene.Net.Documents;
using Lucene.Net.Index;

using SimpleLucene;

namespace IUDICO.Search.Models.Definitions
{
    abstract public class Definition<T> : IIndexDefinition<T>, IResultDefinition<T>
        where T : class
    {
        public DefaultQuery Query
        {
            get; protected set;
        }

        public abstract Document Convert(T entity);

        public abstract Term GetIndex(T entity);

        public abstract T Convert(Document document);
    }
}