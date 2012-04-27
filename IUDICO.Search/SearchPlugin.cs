using System.Linq;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using System.Collections.Generic;
using Castle.Windsor;
using System;

using IUDICO.Search.Models.IndexDefinitions;

using Lucene.Net.Store;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Analysis.Standard;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Shared;
using System.IO;
using System.Timers;
using System.Web.Routing;
using Castle.MicroKernel.SubSystems.Configuration;

using SimpleLucene;
using SimpleLucene.Impl;

using Action = IUDICO.Common.Models.Action;
using Directory = Lucene.Net.Store.Directory;
using Node = IUDICO.Common.Models.Shared.Node;
using System.Threading;
using IUDICO.Search.Models;
using SimpleLucene.IndexManagement;
using System.Reflection;

namespace IUDICO.Search
{
    public class SearchPlugin : IWindsorInstaller, IPlugin
    {
        protected IWindsorContainer container;
        protected Thread thread;
        // protected LuceneThread luceneThread;

        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name)),
                Component.For<IPlugin>().Instance(this).LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<LuceneThread>().ImplementedBy<LuceneThread>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton));

            this.container = container;
        }

        #endregion

        #region IPlugin Members
        public string GetName()
        {
            return "Search";
        }

        public IEnumerable<Action> BuildActions()
        {
            return new Action[] { };
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new MenuItem[] { };
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Search",
                "Search/{action}",
                new { controller = "Search" });
        }

        public void Update(string evt, params object[] data)
        {
            return;

            var luceneThread = this.container.Resolve<LuceneThread>();

            if (evt == LMSNotifications.ApplicationStart)
            {
                /*
                var types = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(typeof(IIndexDefinition<>).IsAssignableFrom);

                http://stackoverflow.com/questions/6174956/isassignablefrom-when-interface-has-generics-but-not-the-implementation

                foreach (var type in types)
                {
                    directoryIndex.Add(type, type.AssemblyQualifiedName);
                }
                */

                // luceneThread = new LuceneThread(data[0] as ILmsService);
                // this.thread = new Thread(luceneThread.Run);
                // this.thread.Start();
            }
            else if (evt == LMSNotifications.ApplicationStop)
            {
                luceneThread.RequestStop();
            }

            return;

            switch (evt)
            {
                case UserNotifications.UserCreate:
                case UserNotifications.UserEdit:
                    luceneThread.UpdateIndex((User)data[0]);
                    break;
                case UserNotifications.UserDelete:
                    luceneThread.DeleteIndex((User)data[0]);
                    break;
            }

            /*
            if (evt == UserNotifications.UserCreate)
            {
                User user = (User)data[0];

                Document document = new Document();
                
                this.AddToIndex(document);
            }

            if (evt == UserNotifications.UserEdit)
            {
                this.Update(UserNotifications.UserDelete, data[0]);
                this.Update(UserNotifications.UserCreate, data[0]);
            }

            if (evt == UserNotifications.UserDelete)
            {
                User user = (User)data[0];
                Term term = new Term("UserID", user.Id.ToString());
                this.DeleteFromIndex(term);
            }

            if (evt == DisciplineNotifications.DisciplineCreated)
            {
                Discipline discipline = (Discipline)data[0];
                var document = new Document();
                this.AddToIndex(document);
            }

            if (evt == DisciplineNotifications.DisciplineEdited)
            {
                this.Update(DisciplineNotifications.DisciplineDeleted, data[0]);
                this.Update(DisciplineNotifications.DisciplineCreated, data[1]);
            }

            if (evt == DisciplineNotifications.DisciplineDeleted)
            {
                Discipline discipline = (Discipline)data[0];
                Term term = new Term("DisciplineID", discipline.Id.ToString());
                this.DeleteFromIndex(term);
            }

            if (evt == DisciplineNotifications.TopicCreated)
            {
                Topic topic = (Topic)data[0];
                Document document = new Document();
                document = new Document();
                document.Add(new Field("Type", "Topic", Field.Store.YES, Field.Index.NO));
                document.Add(new Field("TopicID", topic.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("Topic", topic.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                if (topic.TestCourseRef == null)
                {
                    document.Add(new Field("CourseRef", "null", Field.Store.YES, Field.Index.NO));
                }
                else
                {
                    document.Add(new Field("CourseRef", topic.TestCourseRef.ToString(), Field.Store.YES, Field.Index.NO));
                }

                this.AddToIndex(document);
            }

            if (evt == DisciplineNotifications.TopicEdited)
            {
                this.Update(DisciplineNotifications.TopicDeleted, data[0]);
                this.Update(DisciplineNotifications.TopicCreated, data[1]);
            }

            if (evt == DisciplineNotifications.TopicDeleted)
            {
                Topic topic = (Topic)data[0];
                Term term = new Term("TopicID", topic.Id.ToString());
                this.DeleteFromIndex(term);
            }

            if (evt == UserNotifications.GroupCreate)
            {
                Group group = (Group)data[0];
                Document document = new Document();
                document.Add(new Field("Type", "Group", Field.Store.YES, Field.Index.NO));
                document.Add(new Field("GroupID", group.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("Group", group.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

                this.AddToIndex(document);
            }

            if (evt == UserNotifications.GroupEdit)
            {
                this.Update(UserNotifications.GroupDelete, data[0]);
                this.Update(UserNotifications.GroupCreate, data[1]);
            }

            if (evt == UserNotifications.GroupDelete)
            {
                Group group = (Group)data[0];
                Term term = new Term("GroupID", group.Id.ToString());
                this.DeleteFromIndex(term);
            }

            if (evt == CourseNotifications.NodeCreate)
            {
                Directory directory = FSDirectory.Open(new DirectoryInfo(serverPath));
                Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);

                IndexWriter writer = new IndexWriter(directory, analyzer, false, IndexWriter.MaxFieldLength.UNLIMITED);
                try
                {
                    ProcessNode(writer, (IUDICO.Common.Models.Shared.Node)data[0], (container.Resolve<ILmsService>()).FindService<ICourseService>());
                }
                finally
                {
                    writer.Optimize();
                    writer.Close();
                }
            }

            if (evt == CourseNotifications.NodeEdit)
            {
                this.Update(CourseNotifications.NodeDelete, data[0]);
                this.Update(CourseNotifications.NodeCreate, data[1]);
            }

            if (evt == CourseNotifications.NodeDelete)
            {
                Node node = (IUDICO.Common.Models.Shared.Node)data[0];
                Term term = new Term("NodeID", node.Id.ToString());
                this.DeleteFromIndex(term);
            }

            if (evt == CourseNotifications.CourseCreate)
            {
                Course course = (Course)data[0];
                Document document = new Document();
                document.Add(new Field("Type", "Course", Field.Store.YES, Field.Index.NO));
                document.Add(new Field("CourseID", course.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("Name", course.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                document.Add(new Field("Owner", course.Owner, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

                this.AddToIndex(document);
            }

            if (evt == CourseNotifications.CourseEdit)
            {
                this.Update(CourseNotifications.CourseDelete, data[0]);
                this.Update(CourseNotifications.CourseCreate, data[1]);
            }

            if (evt == CourseNotifications.CourseDelete)
            {
                Course course = (Course)data[0];
                Term term = new Term("GroupID", course.Id.ToString());
                this.DeleteFromIndex(term);
            }
            */

        }

        /*protected Timer mTimer = new Timer(1000 * 60 * 60);

        protected void Timer_Elapsed(object sender, EventArgs args)
        {
            RebuildIndex(this.lmsObject);
        }

        public void StartMyTimer(object o)
        {
            this.lmsObject = o;
            this.mTimer.Elapsed += new ElapsedEventHandler(this.Timer_Elapsed);
            this.mTimer.Start();
        }
        */
        public void Setup(IWindsorContainer container)
        {
        }

        #endregion

        public static void ProcessNode(IndexWriter writer, Node node, ICourseService courseService)
        {
            Document document = new Document();
            document.Add(new Field("Type", "Node", Field.Store.YES, Field.Index.NO));
            document.Add(new Field("NodeID", node.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Name", node.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
            document.Add(new Field("NodeCourseID", node.CourseId.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("isFolder", node.IsFolder.ToString(), Field.Store.YES, Field.Index.ANALYZED));

            if (node.IsFolder)
            {
                var nodes = courseService.GetNodes(node.CourseId, node.Id);

                foreach (Node childNode in nodes)
                {
                    ProcessNode(writer, childNode, courseService);
                }
            }
            else
            {
                var content = courseService.GetNodeContents(node.Id);

                document.Add(new Field("Content", content, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
            }

            writer.AddDocument(document);
        }


        public static void RebuildIndex(object o)
        {
            var service = o as ILmsService;

            // var user = service.FindService<IUserService>().GetCurrentUser();

            var courseService = service.FindService<ICourseService>();
            var disciplineService = service.FindService<IDisciplineService>();
            var userService = service.FindService<IUserService>();

            var courses = courseService.GetCourses();
            var disciplines = disciplineService.GetDisciplines();
            var users = userService.GetUsers();
            var groups = userService.GetGroups();

            var a = Environment.CurrentDirectory;
            Directory directory = FSDirectory.Open(new DirectoryInfo("1"));
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            IndexWriter writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);
            Document document;

            try
            {
                foreach (Course course in courses)
                {
                    document = new Document();

                    document.Add(new Field("Type", "Course", Field.Store.YES, Field.Index.NO));
                    document.Add(new Field("CourseID", course.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("Name", course.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                    document.Add(new Field("Owner", course.Owner, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

                    writer.AddDocument(document);

                    var nodes = courseService.GetNodes(course.Id);

                    foreach (Node node in nodes)
                    {
                        ProcessNode(writer, node, courseService);
                    }
                }

                foreach (Discipline discipline in disciplines)
                {
                    document = new Document();
                    document.Add(new Field("Type", "Discipline", Field.Store.YES, Field.Index.NO));
                    document.Add(new Field("DisciplineID", discipline.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("Owner", discipline.Owner, Field.Store.YES, Field.Index.NO));
                    document.Add(new Field("Discipline", discipline.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                    writer.AddDocument(document);

                    var topics = disciplineService.GetTopicsByDisciplineId(discipline.Id);
                    
                    foreach (Topic topic in topics)
                    {
                        document = new Document();
                        document.Add(new Field("Type", "Topic", Field.Store.YES, Field.Index.NO));
                        document.Add(new Field("TopicID", topic.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("Topic", topic.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                        if (topic.TestCourseRef == null)
                        {
                            document.Add(new Field("CourseRef", "null", Field.Store.YES, Field.Index.NO));
                        }
                        else
                        {
                            document.Add(new Field("CourseRef", topic.TestCourseRef.ToString(), Field.Store.YES, Field.Index.NO));
                        }

                        writer.AddDocument(document);
                    }
                }


                foreach (User user in users)
                {
                    document = new Document();
                    document.Add(new Field("Type", "User", Field.Store.YES, Field.Index.NO));
                    document.Add(new Field("UserID", user.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("User", user.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

                    writer.AddDocument(document);
                }

                foreach (Group group in groups)
                {
                    document = new Document();
                    document.Add(new Field("Type", "Group", Field.Store.YES, Field.Index.NO));
                    document.Add(new Field("GroupID", group.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("Group", group.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

                    writer.AddDocument(document);
                }
            }
            catch (Exception)
            {
                // writer.Optimize();
                // writer.Close();

                // throw e;
            }
            finally
            {
                writer.Optimize();
                writer.Close();
            }
        }
    }
}