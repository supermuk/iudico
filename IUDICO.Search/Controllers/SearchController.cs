using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using IUDICO.Common.Controllers;
//using IUDICO.Common.Messages.CourseMgt;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.Search.Models.SearchResult;
using Lucene.Net.Store;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Version = Lucene.Net.Util.Version;
using Lucene.Net.Index;
using Lucene.Net;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using IUDICO.Common.Models.Services;
using System.Diagnostics;

using IUDICO.Common.Models.Shared.CurriculumManagement;

using IUDICO.Search.Models.ViewDataClasses;
using IUDICO.Search.Models;


namespace IUDICO.Search.Controllers
{
    [Authorize]
    public class SearchController : PluginController
    {

        private ICourseService _CourseService;
        private ICurriculumService _CurriculmService;
        private IUserService _UserService;

        public SearchController()
        {
            _CourseService = LmsService.FindService<ICourseService>();
            _CurriculmService = LmsService.FindService<ICurriculumService>();
            _UserService = LmsService.FindService<IUserService>();
        }

        //
        // GET: /Search/

        //public ActionResult Index()
        //{
        //    //Process();
        //    return View();
        //}


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

                List<Node> nodes = _CourseService.GetNodes(node.CourseId, node.Id).ToList();

                foreach (Node childNode in nodes)
                {
                    ProcessNode(writer, childNode);
                }
            }
            else
            {
                var content = _CourseService.GetNodeContents(node.Id);

                document.Add(new Field("Content", content, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
            }

            writer.AddDocument(document);
        }

        [HttpPost]
        public ActionResult Process()
        {
            List<Course> courses = _CourseService.GetCourses(_UserService.GetCurrentUser()).ToList();
            List<Curriculum> curriculums = _CurriculmService.GetCurriculumsWithThemesOwnedByUser(_UserService.GetCurrentUser()).ToList();//GetCurriculums().ToList();
            List<User> users = _UserService.GetUsers().ToList();
            List<Group> groups = _UserService.GetGroupsByUser(_UserService.GetCurrentUser()).ToList();//GetGroups(_UserService.GetCurrentUser()).ToList();

            var roles = _UserService.GetCurrentUser().UserRoles;

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

                    List<Node> nodes = _CourseService.GetNodes(course.Id).ToList();

                    foreach (Node node in nodes)
                    {

                        ProcessNode(writer, node);
                    }
                }

                foreach (Curriculum curriculum in curriculums)
                {
                    document = new Document();
                    document.Add(new Field("Type", "Curriculum", Field.Store.YES, Field.Index.NO));
                    document.Add(new Field("ID", curriculum.Id.ToString(), Field.Store.YES, Field.Index.NO));
                    document.Add(new Field("Curriculum", curriculum.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                    writer.AddDocument(document);

                    List<Theme> themes = _CurriculmService.GetThemesByCurriculumId(curriculum.Id).ToList();

                    foreach (Theme theme in themes)
                    {
                        document = new Document();
                        document.Add(new Field("Type", "Theme", Field.Store.YES, Field.Index.NO));
                        document.Add(new Field("ID", theme.Id.ToString(), Field.Store.YES, Field.Index.NO));
                        document.Add(new Field("Theme", theme.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                        document.Add(new Field("CourseRef", theme.CourseRef.ToString(), Field.Store.YES, Field.Index.NO));
                        writer.AddDocument(document);
                    }
                }

                //if ( roles.Contains(Role.Admin) || roles.Contains(Role.Teacher))
                {
                    foreach (User user in users)
                    {
                        document = new Document();
                        document.Add(new Field("Type", "User", Field.Store.YES, Field.Index.NO));
                        //document.Add(new Field("RoleId", user.RolesLine, Field.Store.YES, Field.Index.NO));
                        document.Add(new Field("ID", user.Id.ToString(), Field.Store.YES, Field.Index.NO));
                        document.Add(new Field("User", user.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                        writer.AddDocument(document);
                    }
                }

                foreach (Group group in groups)
                {
                    document = new Document();
                    document.Add(new Field("Type", "Group", Field.Store.YES, Field.Index.NO));
                    document.Add(new Field("ID", group.Id.ToString(), Field.Store.YES, Field.Index.NO));
                    document.Add(new Field("Group", group.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                    writer.AddDocument(document);
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
        public ActionResult SearchSimple(String query)
        {
            var model = new SearchModel
            {
                SearchText = query,
                CheckBoxes = GetAvailableCheckBoxes()
            };
            MakeSearch(model);
            return View("Search", model);
        }

        [HttpPost]
        public ActionResult Search(SearchModel model)
        {
            MakeSearch(model);
            return View("Search", model);
        }

        //private void MakeSearch(SearchModel model)
        //{
            //string query = model.SearchText + "~";

            //DateTime datastart = DateTime.Now;
            //Directory directory = FSDirectory.Open(new System.IO.DirectoryInfo(Server.MapPath("~/Data/Index")));
            //IndexSearcher searcher = new IndexSearcher(directory, true);
            //Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_29);

            //MultiFieldQueryParser queryParser = new MultiFieldQueryParser(
            //        Version.LUCENE_29,
            //        new String[] { "Name", "Content", "Curriculum", "User", "Group", "Theme" },
            //        analyzer
            //    );


            //ScoreDoc[] scoreDocs = searcher.Search(queryParser.Parse(query), 100).scoreDocs;

            //Hits hit = searcher.Search(queryParser.Parse(query));
            //int total = hit.Length();

            //List<Curriculum> curriculums123 = _CurriculmService.GetCurriculums(_UserService.GetCurrentUser()).ToList();
            //List<Course> courses123 = _CourseService.GetCourses(_UserService.GetCurrentUser()).ToList();
            //List<ThemeDescription> themes123 = _CurriculmService.GetThemesAvailableForUser(_UserService.GetCurrentUser()).ToList();

            ////List<Curriculum> themes123 = _CurriculmService.GetCurriculumsWithThemesOwnedByUser(_UserService.GetCurrentUser()).ToList();
            ////foreach(Curriculum curr in curriculums123){
            ////    themes123.InsertRange(themes123.Count - 1, _CurriculmService.GetThemesByCurriculumId(curr.Id));
            ////}

            //List<ISearchResult> results = new List<ISearchResult>();
            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            //foreach (ScoreDoc doc in scoreDocs)
            //{
            //    ISearchResult result;
            //    Document document = searcher.Doc(doc.doc);
            //    String type = document.Get("Type").ToLower();

            //    switch (type)
            //    {
            //        case "node":

            //            Node node = new Node();
            //            node.Id = Convert.ToInt32(document.Get("NodeID"));
            //            node.Name = document.Get("Name");
            //            node.CourseId = Convert.ToInt32(document.Get("CourseID"));
            //            node.IsFolder = Convert.ToBoolean(document.Get("isFolder"));

            //            result = new NodeResult(node, _CourseService.GetCourse(node.CourseId).Name, document.Get("Content"), _CourseService.GetCourse(node.CourseId).Updated.ToString());
            //            results.Add(result);
            //            break;

            //        case "course":

            //            Course course = new Course();
            //            course.Id = Convert.ToInt32(document.Get("CourseID"));
            //            course.Name = document.Get("Name");
            //            foreach (Course cour in courses123)
            //            {
            //                if (cour.Id == course.Id)
            //                {
            //                    result = new CourseResult(course, _CourseService.GetCourse(course.Id).Updated.ToString(), _CourseService.GetCourse(course.Id).Owner);
            //                    results.Add(result);
            //                    break;
            //                }
            //            }
            //            break;

            //        case "curriculum":

            //            Curriculum curriculum = new Curriculum();
            //            curriculum.Id = Convert.ToInt32(document.Get("CurriculumID"));
            //            curriculum.Name = document.Get("Curriculum");
            //            curriculum.Owner = document.Get("Owner");

            //            string str = _CurriculmService.GetCurriculum(curriculum.Id).Owner;
            //            foreach (Curriculum curr in curriculums123)
            //            {
            //                if (curr.Owner.Equals(curriculum.Owner))
            //                {
            //                    result = new CurriculumResult(curriculum, _CurriculmService.GetCurriculum(curriculum.Id).Updated.ToString());
            //                    results.Add(result);
            //                    break;
            //                }
            //            }
            //            break;

            //        case "user":

            //            User user = new User();
            //            user.Id = Guid.Parse(document.Get("UserID"));
            //            user.Name = document.Get("User");
            //            //user.Roles
            //            /*user.RoleId = Convert.ToInt32(document.Get("RoleId"));*/

            //            result = new UserResult(user);
            //            results.Add(result);
            //            break;

            //        case "group":

            //            Group group = new Group();
            //            group.Id = int.Parse(document.Get("GroupID"));
            //            group.Name = document.Get("Group");
            //            result = new GroupResult(group);
            //            results.Add(result);
            //            break;

            //        case "theme":

            //            Theme theme = new Theme();
            //            theme.Id = Convert.ToInt32(document.Get("ThemeID"));
            //            theme.Name = document.Get("Theme");
            //            if (document.Get("CourseRef") == "null")
            //            {
            //                theme.CourseRef = null;
            //            }
            //            else
            //            {
            //                theme.CourseRef = Convert.ToInt32(document.Get("CourseRef"));
            //            }
                        
            //            foreach (ThemeDescription themdesc in themes123)
            //            {
            //                if (themdesc.Theme.Id == theme.Id)
            //                {
            //                    result = new ThemeResult(theme, _CourseService.GetCourse(theme.CourseRef.Value).Name);
            //                    results.Add(result);
            //                    break;
            //                }
            //            }
            //            break;

            //        default:
            //            throw new Exception("Unknown result type");
            //    }
            //}


            //DateTime dataend = DateTime.Now;
            //analyzer.Close();
            //searcher.Close();
            //directory.Close();

            //ViewData["SearchString"] = query;
            //ViewData["score"] = Math.Abs(dataend.Millisecond - datastart.Millisecond); //sw.ElapsedMilliseconds.ToString();
            //ViewData["total"] = total;

            //            result = new ThemeResult(theme, _CourseService.GetCourse(theme.CourseRef.Value).Name);

            //            break;

            //        default:
            //            throw new Exception("Unknown result type");
            //    }

            //    results.Add(result);
            //}
            //sw.Stop();

            //DateTime dataend = DateTime.Now;
            //analyzer.Close();
            //searcher.Close();
            //directory.Close();
        //}

        private List<CheckBoxModel> GetAvailableCheckBoxes()
        {
            var roles = _UserService.GetCurrentUserRoles();
            var result = new List<CheckBoxModel>();
            result.Add(new CheckBoxModel(SearchType.Themes ));
            if (roles.Contains(Role.Teacher))
            {
                result.Add(new CheckBoxModel(SearchType.Users));
                result.Add(new CheckBoxModel(SearchType.Courses));
                result.Add(new CheckBoxModel(SearchType.Curriculums));
            }
            else if (roles.Contains(Role.Admin))
            {
                result.Add(new CheckBoxModel(SearchType.Users ));
            }
            return result;
        }

        private void MakeSearch(SearchModel model)
        {
            model.SearchResult = new List<ISearchResult>();
            foreach (var checkBox in model.CheckBoxes)
            {
                if (checkBox.IsChecked)
                {
                    if (checkBox.SearchType == SearchType.Courses)
                    {
                        //make filtration here...
                    }
                    //make filtration here...
                }
            }
            model.Total = 20;
            model.Score = 20;
        }
    }
}
