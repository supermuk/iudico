using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using System.Collections.Generic;
using Castle.Windsor;
using System;
using Lucene.Net.Store;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Analysis.Standard;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Search
{
    public class SearchPlugin : IWindsorInstaller, IPlugin
    {
        static IWindsorContainer _Container;

        #region IWindsorInstaller Members
        bool isRun = false;
        protected Object myObject;
        protected  static string serverPath;

        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name)),
                Component.For<IPlugin>().ImplementedBy<SearchPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
            );

            _Container = container;
        }

        #endregion

        #region IPlugin Members
        public string GetName()
        {
            return "Search";
        }

        public IEnumerable<IUDICO.Common.Models.Action> BuildActions()
        {
            return new IUDICO.Common.Models.Action[] { };
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new MenuItem[] { };
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute(
                "Search",
                "Search/{action}",
                new { controller = "Search" }
            );
        }

        public void AddToIndex(Document doc)
        {
            Directory directory = FSDirectory.Open(new System.IO.DirectoryInfo(serverPath));
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            IndexWriter writer = new IndexWriter(directory, analyzer, false, IndexWriter.MaxFieldLength.UNLIMITED);

            writer.AddDocument(doc);
            writer.Optimize();
            writer.Close();
        }

        public void DeleteFromIndex(Term term)
        {
            Directory directory = FSDirectory.Open(new System.IO.DirectoryInfo(serverPath));
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            IndexWriter writer = new IndexWriter(directory, analyzer, false, IndexWriter.MaxFieldLength.UNLIMITED);

            writer.DeleteDocuments(term);
            writer.Commit();
            writer.Close();
        }

        public void Update(string evt, params object[] data)
        {

            if (evt == LMSNotifications.ApplicationStart)
            {
                if (!isRun)
                {

                    //mTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
                    //mTimer.Start();
                    string root = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
                    int index = root.IndexOf("\\Plugins");
                    root = root.Substring(0, index);
                    serverPath = root.Insert(index, "\\Data\\Index");

                    RebuildIndex(data[0] as ILmsService);

                    //var thread = new Thread(startMyTimer);
                    //thread.Start(((IWindsorContainer)data[0]).Resolve<ILmsService>());
                    //isRun = true;
                }
            }

            if (evt == UserNotifications.UserCreate)
            {
                User user = (User)data[0];

                Document document = new Document();
                document.Add(new Field("Type", "User", Field.Store.YES, Field.Index.NO));
                document.Add(new Field("UserID", user.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("User", user.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

                AddToIndex(document);
            }

            if (evt == UserNotifications.UserEdit)
            {
                Update(UserNotifications.UserDelete, data[0]);
                Update(UserNotifications.UserCreate, data[1]);
            }

            if (evt == UserNotifications.UserDelete)
            {
                User user = (User)data[0];
                Term term = new Term("UserID", user.Id.ToString());
                DeleteFromIndex(term);
            }

            if (evt == DisciplineNotifications.DisciplineCreated)
            {
                Discipline discipline = (Discipline)data[0];
                Document document = new Document();
                document.Add(new Field("Type", "Discipline", Field.Store.YES, Field.Index.NO));
                document.Add(new Field("DisciplineID", discipline.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("Owner", discipline.Owner, Field.Store.YES, Field.Index.NO));
                document.Add(new Field("Discipline", discipline.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

                AddToIndex(document);
            }

            if (evt == DisciplineNotifications.DisciplineEdited)
            {
                Update(DisciplineNotifications.DisciplineDeleted, data[0]);
                Update(DisciplineNotifications.DisciplineCreated, data[1]);
            }

            if (evt == DisciplineNotifications.DisciplineDeleted)
            {
                Discipline discipline = (Discipline)data[0];
                Term term = new Term("DisciplineID", discipline.Id.ToString());
                DeleteFromIndex(term);
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

                AddToIndex(document);
            }

            if (evt == DisciplineNotifications.TopicEdited)
            {
                Update(DisciplineNotifications.TopicDeleted, data[0]);
                Update(DisciplineNotifications.TopicCreated, data[1]);
            }

            if (evt == DisciplineNotifications.TopicDeleted)
            {
                Topic topic = (Topic)data[0];
                Term term = new Term("TopicID", topic.Id.ToString());
                DeleteFromIndex(term);
            }

            if (evt == UserNotifications.GroupCreate)
            {
                Group group = (Group)data[0];
                Document document = new Document();
                document.Add(new Field("Type", "Group", Field.Store.YES, Field.Index.NO));
                document.Add(new Field("GroupID", group.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("Group", group.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

                AddToIndex(document);
            }

            if (evt == UserNotifications.GroupEdit)
            {
                Update(UserNotifications.GroupDelete, data[0]);
                Update(UserNotifications.GroupCreate, data[1]);
            }

            if (evt == UserNotifications.GroupDelete)
            {
                Group group = (Group)data[0];
                Term term = new Term("GroupID", group.Id.ToString());
                DeleteFromIndex(term);
            }

            if (evt == CourseNotifications.NodeCreate)
            {
                Directory directory = FSDirectory.Open(new System.IO.DirectoryInfo(serverPath));
                Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);

                IndexWriter writer = new IndexWriter(directory, analyzer, false, IndexWriter.MaxFieldLength.UNLIMITED);
                try
                {
                    ProcessNode(writer, (IUDICO.Common.Models.Shared.Node)data[0], (_Container.Resolve<ILmsService>()).FindService<ICourseService>());
                }
                finally
                {
                    writer.Optimize();
                    writer.Close();
                }
            }

            if (evt == CourseNotifications.NodeEdit)
            {
                Update(CourseNotifications.NodeDelete, data[0]);
                Update(CourseNotifications.NodeCreate, data[1]);
            }

            if (evt == CourseNotifications.NodeDelete)
            {
                IUDICO.Common.Models.Shared.Node node = (IUDICO.Common.Models.Shared.Node)data[0];
                Term term = new Term("NodeID", node.Id.ToString());
                DeleteFromIndex(term);
            }

            if (evt == CourseNotifications.CourseCreate)
            {
                Course course = (Course)data[0];
                Document document = new Document();
                document.Add(new Field("Type", "Course", Field.Store.YES, Field.Index.NO));
                document.Add(new Field("CourseID", course.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("Name", course.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                document.Add(new Field("Owner", course.Owner, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

                AddToIndex(document);
            }

            if (evt == CourseNotifications.CourseEdit)
            {
                Update(CourseNotifications.CourseDelete, data[0]);
                Update(CourseNotifications.CourseCreate, data[1]);
            }

            if (evt == CourseNotifications.CourseDelete)
            {
                Course course = (Course)data[0];
                Term term = new Term("GroupID", course.Id.ToString());
                DeleteFromIndex(term);
            }

        }

          

        protected System.Timers.Timer mTimer = new System.Timers.Timer(1000 * 60 * 60);

        protected void Timer_Elapsed(object sender, EventArgs args)
        {
            RebuildIndex(myObject);
        }

        public void startMyTimer(object o)
        {
            myObject = o;
            mTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            mTimer.Start();
        }

        public void Setup(IWindsorContainer container){}

        #endregion

        public static void ProcessNode(IndexWriter writer, IUDICO.Common.Models.Shared.Node node, ICourseService courseService)
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

                foreach (IUDICO.Common.Models.Shared.Node childNode in nodes)
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

            //var user = service.FindService<IUserService>().GetCurrentUser();

            var courseService = service.FindService<ICourseService>();
            var disciplineService = service.FindService<IDisciplineService>();
            var userService = service.FindService<IUserService>();

            var courses = courseService.GetCourses();
            var disciplines = disciplineService.GetDisciplines();
            var users = userService.GetUsers();
            var groups = userService.GetGroups();

            var a = Environment.CurrentDirectory;
            Directory directory = FSDirectory.Open(new System.IO.DirectoryInfo(serverPath));
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

                    foreach (IUDICO.Common.Models.Shared.Node node in nodes)
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
//                writer.Optimize();
//                writer.Close();

                //throw e;
            }
            finally
            {
                writer.Optimize();
                writer.Close();
            }
        }
    }
}