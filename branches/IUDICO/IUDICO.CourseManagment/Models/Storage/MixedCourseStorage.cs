using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.CourseManagement.Helpers;
using IUDICO.CourseManagement.Models.ManifestModels;
using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.ManifestModels.ResourceModels;

namespace IUDICO.CourseManagement.Models.Storage
{
    internal class MixedCourseStorage : ICourseStorage
    {
        private readonly ILmsService _LmsService;

        public MixedCourseStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
        }

        protected DBDataContext GetDbContext()
        {
            return _LmsService.GetDbDataContext();
        }

        #region IStorage Members

        #region Course methods

        public IEnumerable<Course> GetCourses()
        {
            return GetDbContext().Courses.Where(c => c.Deleted == false).AsEnumerable();
        }
        public IEnumerable<Course> GetCourses(Guid userId)
        {
            var db = GetDbContext();
            var courses = from c in db.Courses
                          join u in db.CourseUsers on c.Id equals u.CourseRef
                          where u.UserRef == userId && c.Deleted == false
                          select c;
            return courses;
        }

        public IEnumerable<Course> GetCourses(string owner)
        {
            return GetDbContext().Courses.Where(i => i.Owner == owner && i.Deleted == false);
        }

        public Course GetCourse(int id)
        {
            return GetDbContext().Courses.Single(c => c.Id == id);
        }

        public IEnumerable<User> GetCourseUsers(int courseId)
        {
            var db = GetDbContext();

            var userIds = db.CourseUsers.Where(i => i.CourseRef == courseId);
            var users = db.Users.Where(i => userIds.Any(j => j.UserRef == i.Id));

            return users;
        }

        public void UpdateCourseUsers(int courseId, IEnumerable<Guid> userIds)
        {
            var db = GetDbContext();
            
            var oldUsers = db.CourseUsers.Where(i => i.CourseRef == courseId);
            db.CourseUsers.DeleteAllOnSubmit(oldUsers);

            if (userIds != null)
            {
                var newUsers = userIds.Select(i => new CourseUser { CourseRef = courseId, UserRef = i });
                db.CourseUsers.InsertAllOnSubmit(newUsers);
            }

            db.SubmitChanges();
        }

        public int AddCourse(Course course)
        {
            course.Created = DateTime.Now;
            course.Updated = DateTime.Now;

            var db = GetDbContext();

            db.Courses.InsertOnSubmit(course);
            db.SubmitChanges();

            string path = GetCoursePath(course.Id);
            @Directory.CreateDirectory(path);

            _LmsService.Inform("course/create", course);

            return course.Id;
        }

        public void UpdateCourse(int id, Course course)
        {
            var db = GetDbContext();

            var oldCourse = db.Courses.Single(c => c.Id == id);

            oldCourse.Name = course.Name;
            oldCourse.Owner = course.Owner;
            oldCourse.Updated = DateTime.Now;

            db.SubmitChanges();

            _LmsService.Inform("course/edit", course);
        }

        [Obsolete("Directory.Delete gives exception when files are present: http://stackoverflow.com/questions/329355/cannot-delete-directory-with-directory-deletepath-true. Set CASCADE on DELETE & UPDATE action in foreign index CourseID")]
        public void DeleteCourse(int id)
        {
            var db = GetDbContext();

            var course = db.Courses.Single(c => c.Id == id);

            course.Deleted = true;

            db.SubmitChanges();

            _LmsService.Inform("course/delete", course);
        }

        public void DeleteCourses(List<int> ids)
        {
            var db = GetDbContext();

            var courses = (from n in db.Courses where ids.Contains(n.Id) select n);

            foreach (var course in courses)
            {
                course.Deleted = true;

                _LmsService.Inform("course/delete", course);
            }

            db.SubmitChanges();
        }

        [Obsolete("HttpContext.Current could be null sometimes. See MixedCourseManagement.GetCoursePath(int)")]
        public string Export(int id)
        {
            var course = this.GetCourse(id);

            var path = HttpContext.Current == null ? Path.Combine(System.Environment.CurrentDirectory, "Site") : HttpContext.Current.Request.PhysicalApplicationPath;
            
            path = Path.Combine(path, @"Data\WorkFolder");
            path = Path.Combine(path, Guid.NewGuid().ToString());
            path = Path.Combine(path, course.Name);

            Directory.CreateDirectory(path);

            var nodes = GetNodes(id).ToList();
            for (var i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].IsFolder == false)
                {
                    var fs = System.IO.File.Create(Path.Combine(path, nodes[i].Name + ".html"));
                    fs.Close();
                }
                else
                {
                    var subNodes = GetNodes(id, nodes[i].Id);
                    nodes.AddRange(subNodes);
                }
            }

            var manifest = new Manifest();
            var sw = new StreamWriter(Path.Combine(path, SCORM.ImsManifset));
            var parentItem = new Item();

            parentItem = AddSubItems(parentItem, null, id, ref manifest);
            manifest.Organizations[0].Items = parentItem.Items;
            manifest.Organizations[0].Title = course.Name;
            manifest.Organizations[0].ApplySequencingPattern(SequencingPattern.OrganizationDefaultSequencingPattern);
            manifest.Serialize(sw);
            sw.Close();

            Zipper.CreateZip(path + ".zip", path);
            return path + ".zip";
        }

        private Item AddSubItems(Item parentItem, Node parentNode, int courseId, ref Manifest manifest)
        {
            var nodes = parentNode == null ? GetNodes(courseId) : GetNodes(courseId, parentNode.Id);
            
            foreach (var node in nodes)
            {
                if (node.IsFolder)
                {
                    var item = new Item() { Title = node.Name };
                    item = AddSubItems(item, node, courseId, ref manifest);
                    parentItem.AddChildItem(item);
                }
                else
                {
                    var files = new List<ManifestModels.ResourceModels.File>();
                    files.Add(new ManifestModels.ResourceModels.File(node.Name + ".html"));

                    var resourceId = manifest.Resources.AddResource(new Resource(ScormType.Asset, files) { Href = node.Name + ".html" });
                    parentItem.AddChildItem(new Item(resourceId) { Title = node.Name, Identifier = ConstantStrings.ItemIdPrefix + node.Id.ToString() });
                }
            }
            return parentItem;
        }

        public int Import(string path)
        {
            var course = new Course();
            var folderPath = path.Substring(0, path.Length - 4);

            Zipper.ExtractZipFile(path, folderPath);

            course.Name = folderPath.Split('\\').Last();
            course.Owner = "Imported";

            return AddCourse(course);
        }
        #endregion

        #region Node methods
        public IEnumerable<Node> GetNodes(int courseId)
        {
            return GetNodes(courseId, null);
        }

        public IEnumerable<Node> GetNodes(int courseId, int? parentId)
        {
            try
            {
                var db = GetDbContext();

                var course = db.Courses.SingleOrDefault(c => c.Id == courseId);
                var nodes = course.Nodes.OrderBy(n => n.Position).ToList();

                if (parentId == null)
                {
                    nodes = nodes.Where(n => n.ParentId == null).ToList();
                }
                else
                {
                    nodes = nodes.Where(n => n.ParentId == parentId).ToList();
                }

                return nodes;
            }
            catch (Exception)
            {
                return new List<Node>();
            }
        }

        public Node GetNode(int id)
        {
            return GetDbContext().Nodes.SingleOrDefault(n => n.Id == id);
        }

        public int? AddNode(Node node)
        {
            try
            {
                var db = GetDbContext();

                db.Nodes.InsertOnSubmit(node);
                db.SubmitChanges();

                if (node.IsFolder)
                {
                    var path = GetNodePath(node.Id);
                    @Directory.CreateDirectory(path);
                }

                return node.Id;
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateNode(int id, Node node)
        {
            try
            {
                var db = GetDbContext();

                var oldNode = db.Nodes.SingleOrDefault(n => n.Id == id);

                oldNode.Name = node.Name;
                oldNode.ParentId = node.ParentId;
                oldNode.Position = node.Position;

                db.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteNode(int id)
        {
            try
            {
                var db = GetDbContext();

                var node = db.Nodes.SingleOrDefault(n => n.Id == id);

                db.Nodes.DeleteOnSubmit(node);
                db.SubmitChanges();

                @Directory.Delete(GetNodePath(id));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteNodes(List<int> ids)
        {
            try
            {
                var db = GetDbContext();

                var nodes = (from n in db.Nodes where ids.Contains(n.Id) select n);

                db.Nodes.DeleteAllOnSubmit(nodes);
                db.SubmitChanges();

                foreach (var id in ids)
                {
                    @Directory.Delete(GetNodePath(id));
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int? CreateCopy(Node node, int? parentId, int position)
        {
            try
            {
                var db = GetDbContext();

                var newnode = new Node
                {
                    CourseId = node.CourseId,
                    Name = node.Name,
                    ParentId = parentId,
                    IsFolder = node.IsFolder,
                    Position = position
                };

                CopyNodes(node, newnode);

                db.Nodes.InsertOnSubmit(newnode);
                db.SubmitChanges();

                if (newnode.IsFolder)
                {
                    string path = GetNodePath(newnode.Id);
                    @Directory.CreateDirectory(path);
                }

                return newnode.Id;
            }
            catch
            {
                return null;
            }
        }

        public string GetNodeContents(int id)
        {
            string nodePath = GetNodePath(id);

            return System.IO.File.ReadAllText(nodePath);
        }

        protected string GetNodePath(int nodeId)
        {
            var node = GetDbContext().Nodes.SingleOrDefault(n => n.Id == nodeId);
            var parent = node.Node1;

            string path = node.Id + (!node.IsFolder ? ".html" : "");

            while (parent != null)
            {
                path = Path.Combine(parent.Id.ToString(), path);
                parent = parent.Node1;
            }

            path = Path.Combine(GetCoursePath(node.CourseId), path);

            return path;
        }

        protected string GetCoursePath(int courseId)
        {
            var path = HttpContext.Current == null ? Path.Combine(System.Environment.CurrentDirectory, "Site") : HttpContext.Current.Request.PhysicalApplicationPath;

            return Path.Combine(path, @"Data\Courses", courseId.ToString());
        }

        protected void CopyNodes(Node node, Node newnode)
        {
            foreach (var child in node.Nodes)
            {
                var newchild = new Node
                {
                    CourseId = child.CourseId,
                    Name = child.Name,
                    IsFolder = child.IsFolder,
                };

                newnode.Nodes.Add(newchild);

                if (child.Nodes.Count > 0)
                {
                    CopyNodes(child, newchild);
                }
            }
        }

        protected void CreateFolders(Node newnode)
        {
            foreach (var child in newnode.Nodes)
            {
                if (!child.IsFolder)
                {
                    continue;
                }

                var path = GetNodePath(child.Id);
                Directory.CreateDirectory(path);

                CreateFolders(child);
            }
        }

        #endregion

        #endregion
    }
}