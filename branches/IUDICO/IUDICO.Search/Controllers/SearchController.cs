using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.CourseMgt.Models.Storage;
using IUDICO.Search.Models;
using IUDICO.Search.Models.SearchResult;
using Lucene.Net.Store;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Version = Lucene.Net.Util.Version;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using System.Diagnostics;

namespace IUDICO.Search.Controllers
{
    public class SearchController : BaseController
    {
        protected ICourseStorage Storage
        {
            get
            {
                return HttpContext.Application["CurrStorage"] as ICourseStorage;
            }
        }

        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }

        public void ProcessNode(IndexWriter writer, Node node)
        {
            Document document = new Document();
            document.Add(new Field("Type", "Node", Field.Store.YES, Field.Index.NO));
            document.Add(new Field("ID", node.Id.ToString(), Field.Store.YES, Field.Index.NO));
            document.Add(new Field("Name", node.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
            document.Add(new Field("CourseID", node.CourseId.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("isFolder", node.IsFolder.ToString(), Field.Store.YES, Field.Index.ANALYZED));

            if (node.IsFolder)
            {
                List<Node> nodes = Storage.GetNodes(node.CourseId, node.Id);

                foreach (Node childNode in nodes)
                {
                    ProcessNode(writer, childNode);
                }
            }
            else
            {
                document.Add(new Field("Content", Storage.GetNodeContents(node.Id), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
            }
            
            writer.AddDocument(document);
        }

        [HttpPost]
        public ActionResult Process()
        {
            List<Course> courses = Storage.GetCourses();

            if (courses == null)
                return RedirectToAction("Index");
            
            Directory directory = FSDirectory.Open(new System.IO.DirectoryInfo(Server.MapPath("~/Data/Index")));
            Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_29);
            IndexWriter writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);
            Document document;

            try
            {

                foreach (Course course in courses)
                {
                    document = new Document();

                    document.Add(new Field("Type", "Course", Field.Store.YES, Field.Index.NO));
                    document.Add(new Field("ID", course.Id.ToString(), Field.Store.YES, Field.Index.NO));
                    document.Add(new Field("Name", course.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                    document.Add(new Field("Owner", course.Owner, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

                    writer.AddDocument(document);

                    List<Node> nodes = Storage.GetNodes(course.Id);

                    foreach (Node node in nodes)
                    {
                        ProcessNode(writer, node);
                    }
                }
            }
            catch (Exception e)
            {
                writer.Optimize();
                writer.Close();

                throw e;
            }

            writer.Optimize();
            writer.Close();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(String query)
        {
            Directory directory = FSDirectory.Open(new System.IO.DirectoryInfo(Server.MapPath("~/Data/Index")));
            IndexSearcher searcher = new IndexSearcher(directory, true);
            Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_29);

            MultiFieldQueryParser queryParser = new MultiFieldQueryParser(
                    Version.LUCENE_29,
                    new String[] { "Name", "Content" },
                    analyzer
                );

            ScoreDoc[] scoreDocs = searcher.Search(queryParser.Parse(query), 10).scoreDocs;
            List<ISearchResult> results = new List<ISearchResult>();

            foreach (ScoreDoc doc in scoreDocs)
            {
                ISearchResult result;
                Document document = searcher.Doc(doc.doc);
                String type = document.Get("Type").ToLower();

                switch (type)
                {
                    case "node":
                        Node node = new Node();
                        node.Id = Convert.ToInt32(document.Get("ID"));
                        node.Name = document.Get("Name");
                        node.CourseId = Convert.ToInt32(document.Get("CourseID"));
                        node.IsFolder = Convert.ToBoolean(document.Get("isFolder"));

                        result = new NodeResult(node, document.Get("Content"));
                        
                        break;
                    
                    case "course":

                        Course course = new Course();
                        course.Id = Convert.ToInt32(document.Get("ID"));
                        course.Name = document.Get("Name");

                        result = new CourseResult(course);

                        break;
                    
                    default:
                        throw new Exception("Unknown result type");
                }

                results.Add(result);
            }

            analyzer.Close();
            searcher.Close();
            directory.Close();

            ViewData["SearchString"] = query;

            return View(results);
        }
    }
}
