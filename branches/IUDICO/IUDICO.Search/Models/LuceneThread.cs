using System.Collections.Generic;
using System.IO;
using IUDICO.Common.Models.Services;
using IUDICO.Search.Models.Definitions;
using IUDICO.Search.Models.SearchTypes;

using SimpleLucene;
using SimpleLucene.Impl;
using SimpleLucene.IndexManagement;

using System;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Search.Models
{
    public class LuceneThread
    {
        #region Members
        // private volatile bool shouldStop;

        protected readonly ILmsService LmsService;

        public string LuceneDataPath
        {
            get
            {
                return Path.Combine(this.LmsService.GetServerPath(), "Data", "Index");
            }
        }

        private readonly DirectoryInfo luceneDataDirectory;
        public DirectoryInfo LuceneDataDirectory
        {
            get
            {
                return this.luceneDataDirectory;
            }
        }
        
        protected readonly Dictionary<Type, ISimpleSearchType> SearchTypes = new Dictionary<Type, ISimpleSearchType>(); 
        
        #endregion

        #region Helpers
        
        protected SearchType<T> GetSearchType<T>(T obj) where T : class
        {
            return this.SearchTypes[obj.GetType()] as SearchType<T>;
        }

        protected SearchType<T> GetSearchType<T>() where T : class
        {
            return this.SearchTypes[typeof(T)] as SearchType<T>;
        }

        #endregion

        #region Start & Stop 

        public LuceneThread(ILmsService lmsService)
        {
            this.LmsService = lmsService;
            this.luceneDataDirectory = new DirectoryInfo(this.LuceneDataPath);

            this.SearchTypes.Add(typeof(User), new SearchType<User>(new UserDefinition(), this.LuceneDataPath));
            this.SearchTypes.Add(typeof(Group), new SearchType<Group>(new GroupDefinition(), this.LuceneDataPath));
            this.SearchTypes.Add(typeof(Discipline), new SearchType<Discipline>(new DisciplineDefinition(), this.LuceneDataPath));
        }

        ~LuceneThread()
        {
           /* foreach (var service in this.services)
            {
                service.Dispose();
            }*/
        }

        public void ProcessQueue()
        {
            IIndexTask task;

            while (IndexQueue.Instance.TryDequeue(out task))
            {
                using (var service = new IndexService(new DirectoryIndexWriter(task.IndexOptions.IndexLocation.GetDirectory(), task.IndexOptions.RecreateIndex)))
                {
                    task.Execute(service);
                }
            }
        }

        #endregion

        #region Search

        public IEnumerable<T> Search<T>(string query) where T : class
        {
            return this.GetSearchType<T>().Search(query);
        }

        #endregion

        #region Index

        public void RebuildIndex()
        {
            var directory = Lucene.Net.Store.FSDirectory.Open(this.LuceneDataDirectory);
            // var indexExists = IndexReader.IndexExists(directory);

            directory.EnsureOpen();

            var userService = this.LmsService.FindService<IUserService>();
            var courseService = this.LmsService.FindService<ICourseService>();
            var disciplineService = this.LmsService.FindService<IDisciplineService>();

            var users = userService.GetUsers();
            var groups = userService.GetGroups();

            this.ReIndex(users);
            this.ReIndex(groups);

            var courses = courseService.GetCourses();
            var disciplines = disciplineService.GetDisciplines();

            // this.ReIndex(courses);
            this.ReIndex(disciplines);
        }

        protected void ReIndex<T>(IEnumerable<T> entities) where T : class
        {
            var searchType = this.GetSearchType<T>();

            using (var indexer = searchType.GetIndexService(true))
            {
                indexer.IndexEntities(entities, searchType.GetIndexDefinition());
            }
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