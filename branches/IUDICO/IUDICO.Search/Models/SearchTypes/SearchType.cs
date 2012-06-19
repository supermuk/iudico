﻿using System;
using System.Collections.Generic;
using System.IO;

using IUDICO.Search.Models.Queries;

using Lucene.Net.Analysis.Standard;

using SimpleLucene;
using SimpleLucene.Impl;
using SimpleLucene.IndexManagement;

using Version = Lucene.Net.Util.Version;
using IUDICO.Search.Models.Definitions;

namespace IUDICO.Search.Models.SearchTypes
{
    public class SearchType<T> : ISimpleSearchType
        where T : class
    {
        private Definition<T> definition;

        private FileSystemIndexLocation fsiLocation;

        public SearchType()
        {
        }

        public SearchType(
            Definition<T> definition,
            string dataPath)
        {
            this.definition = definition;
            this.fsiLocation = new FileSystemIndexLocation(new DirectoryInfo(Path.Combine(dataPath, typeof(T).Name)));
        }

        public void Update(T obj)
        {
            IndexQueue.Instance.Queue(new EntityUpdateTask<T>(obj, this.definition, this.fsiLocation));
        }

        public void Delete(T obj)
        {
            var term = this.definition.GetIndex(obj);

            IndexQueue.Instance.Queue(new EntityDeleteTask<T>(this.fsiLocation, term.Field(), term.Text()));
        }

        public IIndexService GetIndexService(bool recreateIndex)
        {
            return
                new IndexService(
                    new DirectoryIndexWriter(
                        this.fsiLocation.GetDirectory(), new StandardAnalyzer(Version.LUCENE_29), recreateIndex));
        }

        public IIndexDefinition<T> GetIndexDefinition()
        {
            return this.definition;
        }

        public IResultDefinition<T> GetResultDefinition()
        {
            return this.definition;
        }

        public DefaultQuery GetQuery()
        {
            return this.definition.Query;
        }

        public virtual IEnumerable<T> Search(string keywords)
        {
            return this.Search(this.GetQuery().WithKeywords(keywords));
        }

        public IEnumerable<T> Search(DefaultQuery query)
        {
            var indexSearcher = new DirectoryIndexSearcher(this.fsiLocation.GetDirectory());

            using (var searchService = new SearchService(indexSearcher))
            {
                var result = searchService.SearchIndex(query.Query, this.GetResultDefinition());

                return result.Results;
            }
        }
    }
}