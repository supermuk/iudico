using System.Collections.Generic;
using System.IO;
using IUDICO.Common.Models.Services;
using IUDICO.Search.Models.IndexDefinitions;
using IUDICO.Search.Models.Queries;
using IUDICO.Search.Models.ResultDefinitions;
using IUDICO.Search.Models.SearchTypes;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Util;
using SimpleLucene;
using SimpleLucene.Impl;
using SimpleLucene.IndexManagement;
using Lucene.Net.Index;
using System;
using IUDICO.Common.Models.Shared;

using Version = Lucene.Net.Util.Version;
using Lucene.Net.Search;

namespace IUDICO.Search.Models
{
    public class LuceneThread
    {
        #region Members
        private volatile bool shouldStop;

        protected readonly ILmsService LmsService;

        private readonly DirectoryInfo luceneDataDirectory;
        public DirectoryInfo LuceneDataDirectory
        {
            get
            {
                return this.luceneDataDirectory;
            }
        }

        protected Dictionary<Type, ISimpleSearchType> searchTypes = new Dictionary<Type, ISimpleSearchType>(); 


        //// dsa
         
        /*
        protected Dictionary<Type, string> directoryIndex = new Dictionary<Type, string>();
        public Dictionary<Type, string> DirectoryIndex
        {
            get
            {
                return this.directoryIndex;
            }
        }
        */
        /*
        protected HashSet<IIndexService> services = new HashSet<IIndexService>();
         */
        public string LuceneDataPath
        {
            get
            {
                return Path.Combine(this.LmsService.GetServerPath(), "Data", "Index");
            }
        }
        
        #endregion

        #region Helpers
        /*
        protected IIndexService GetService(Type t)
        {
            return this.services.WithOptions(new IndexOptions()
                {
                    IndexLocation = new FileSystemIndexLocation(
                        new DirectoryInfo(
                            Path.Combine(this.LuceneDataPath, this.directoryIndex[t]))),
                            Analyzer = new StandardAnalyzer(Version.LUCENE_29),
                            RecreateIndex = true
                });
        }
        */
        protected SearchType<T> GetSearchType<T>(T obj) where T : class
        {
            return this.searchTypes[obj.GetType()] as SearchType<T>;
        }

        protected SearchType<T> GetSearchType<T>() where T : class
        {
            return this.searchTypes[typeof(T)] as SearchType<T>;
        }

        #endregion

        #region Start & Stop 

        public LuceneThread(ILmsService lmsService)
        {
            this.LmsService = lmsService;
            this.luceneDataDirectory = new DirectoryInfo(this.LuceneDataPath);

            this.searchTypes = new Dictionary<Type, ISimpleSearchType>();
            this.searchTypes.Add(typeof(User), new SearchType<User>(new UserResultDefinition(), new UserIndexDefinition(), new UserQuery(), this.LuceneDataPath));
        }

        ~LuceneThread()
        {
           /* foreach (var service in this.services)
            {
                service.Dispose();
            }*/
        }

        public void Run()
        {
            var directory = Lucene.Net.Store.FSDirectory.Open(this.LuceneDataDirectory);
            var indexExists = IndexReader.IndexExists(directory);
            
            // if (!indexExists)
            {
                directory.EnsureOpen();
                this.RebuildIndex();
            }

            IIndexTask task;

            while (!this.shouldStop && IndexQueue.Instance.TryDequeue(out task))
            {
                // var service = this.services.WithOptions(task.IndexOptions);
                
                // if (service == null)
                // {
                    var service = new IndexService(new DirectoryIndexWriter(task.IndexOptions.IndexLocation.GetDirectory(), task.IndexOptions.RecreateIndex));
                    // this.services.Add(service);
                // }

                task.Execute(service);
            }

            // service.Dispose();
        }

        public void RequestStop()
        {
            this.shouldStop = true;
        }

        #endregion

        #region Search

        public SearchResult<T> Search<T>(string query) where T : class
        {
            return this.GetSearchType<T>().Search(query);
        }

        #endregion

        #region Index

        private void RebuildIndex()
        {
            var userService = this.LmsService.FindService<IUserService>();

            var users = userService.GetUsers();
            // var groups = userService.GetGroups();

            var searchType = this.GetSearchType<User>();
            searchType.GetIndexService(true).IndexEntities(users, searchType.GetIndexDefinition());


            // this.services.IndexEntities(users, new UserDefinition());

            // var courseService = this.LmsService.FindService<ICourseService>();
            // var disciplineService = this.LmsService.FindService<IDisciplineService>();


            // var courses = courseService.GetCourses();
            // var disciplines = disciplineService.GetDisciplines();
        }
        
        public void UpdateIndex<T>(T obj) where T : class
        {
            this.GetSearchType(obj).Update(obj);
        }

        public void DeleteIndex<T>(T obj) where T : class
        {
            this.GetSearchType(obj).Delete(obj);
        }
        #endregion
    }
}