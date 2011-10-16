using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Notifications;
using IUDICO.CourseManagement.Helpers;
using IUDICO.CourseManagement.Models.ManifestModels;
using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.ManifestModels.ResourceModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;
using File = System.IO.File;
using System.Data.Entity;

namespace IUDICO.CourseManagement.Models.Storage
{
    internal class MixedCourseStorage : ICourseStorage
    {
        private readonly ILmsService _LmsService;
        private readonly string[] _TemplateFiles = { "api.js", "checkplayer.js", "flensed.js", "flXHR.js", "flXHR.swf", "iudico.css", "iudico.js", "jquery-1.5.2.min.js", "jquery.flXHRproxy.js", "jquery.xhr.js", "questions.js", "sco.js", "swfobject.js", "updateplayer.swf" };
        private readonly string _ResourceIdForTemplateFiles = "TemplateFiles";

        public MixedCourseStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
        }

        #region IStorage Members

        #region Course methods

        public IEnumerable<Course> GetCourses()
        {
            using (var db = new CourseManagementDbConext())
            {
                return db.Courses.Where(c => c.Deleted == false).AsEnumerable();
            }
        }

        public IEnumerable<Course> GetCourses(Guid userId)
        {
            using (var db = new CourseManagementDbConext())
            {
                return (from c in db.Courses
                            join u in db.CourseUsers on c.Id equals u.CourseRef
                            where u.UserRef == userId && c.Deleted == false
                            select c);
            }
        }

        public IEnumerable<Course> GetCourses(string owner)
        {
            using (var db = new CourseManagementDbConext())
            {
                return db.Courses.Where(i => i.Owner == owner && i.Deleted == false);
            }
        }

        public IEnumerable<Course> GetCourses(User owner)
        {
            using (var db = new CourseManagementDbConext())
            {
                return db.Courses.Where(i => i.Owner == owner.Username && i.Deleted == false);
            }
        }

        public Course GetCourse(int id)
        {
            using (var db = new CourseManagementDbConext())
            {
                return db.Courses.Single(c => c.Id == id);
            }
        }

        public IEnumerable<User> GetCourseUsers(int courseId)
        {
            using (var db = new CourseManagementDbConext())
            {
                var userIds = db.CourseUsers.Where(i => i.CourseRef == courseId);
                var users = _LmsService.FindService<IUserService>().GetUsersByExpression(i => userIds.Any(j => j.UserRef == i.Id));

                return users;
            }
        }

        public void UpdateCourseUsers(int courseId, IEnumerable<Guid> userIds)
        {
            using (var db = new CourseManagementDbConext())
            {
                var oldUsers = db.CourseUsers.Where(i => i.CourseRef == courseId);

                foreach (var oldUser in oldUsers)
                {
                    db.CourseUsers.Remove(oldUser);
                }

                if (userIds != null)
                {
                    var newUsers = userIds.Select(i => new CourseUser { CourseRef = courseId, UserRef = i });
                    foreach (var newUser in newUsers)
                    {
                        db.CourseUsers.Add(newUser);
                    }
                }

                db.SaveChanges();
            }
        }

        public void DeleteCourseUsers(Guid userId)
        {
            using (var db = new CourseManagementDbConext())
            {
                var courseUsers = db.CourseUsers.Where(i => i.UserRef == userId);

                foreach (var courseUser in courseUsers)
                {
                    db.CourseUsers.Remove(courseUser);
                }
                db.SaveChanges();
            }
        }

        public int AddCourse(Course course)
        {
            course.Created = DateTime.Now;
            course.Updated = DateTime.Now;

            using (var db = new CourseManagementDbConext())
            {

                db.Courses.Add(course);
                db.SaveChanges();

                var path = GetCoursePath(course.Id);
                @Directory.CreateDirectory(path);

                var templatePath = GetTemplatesPath();

                foreach (var templateFile in _TemplateFiles)
                {
                    File.Copy(Path.Combine(templatePath, templateFile), Path.Combine(path, templateFile), true);
                }

                _LmsService.Inform(CourseNotifications.CourseCreate, course);

                return course.Id;
            }
        }

        public void UpdateCourse(int id, Course course)
        {
            using (var db = new CourseManagementDbConext())
            {
                var oldCourse = db.Courses.Single(c => c.Id == id);

                oldCourse.Name = course.Name;
                oldCourse.Updated = DateTime.Now;
                oldCourse.Sequencing = course.Sequencing;

                db.SaveChanges();

                _LmsService.Inform(CourseNotifications.CourseEdit, course);
            }
        }

        public void DeleteCourse(int id)
        {
            using (var db = new CourseManagementDbConext())
            {
                var course = db.Courses.Single(c => c.Id == id);

                if (course.Owner != _LmsService.FindService<IUserService>().GetCurrentUser().Username)
                {
                    return;
                }

                course.Deleted = true;

                db.SaveChanges();

                _LmsService.Inform(CourseNotifications.CourseDelete, course);
            }
        }

        public void DeleteCourses(List<int> ids)
        {
            using (var db = new CourseManagementDbConext())
            {
                var courses = (from n in db.Courses where ids.Contains(n.Id) select n);

                foreach (var course in courses)
                {
                    if (course.Owner != _LmsService.FindService<IUserService>().GetCurrentUser().Username)
                    {
                        continue;
                    }

                    course.Deleted = true;

                    _LmsService.Inform(CourseNotifications.CourseDelete, course);
                }

                db.SaveChanges();
            }
        }

        public string Export(int id)
        {
            var course = GetCourse(id);
            var path = GetCoursePath(id);

            if (course.Locked != null && course.Locked.Value)
            {
                return path + ".zip";
            }

            path = Path.Combine(path, Guid.NewGuid().ToString());
            path = Path.Combine(path, course.Name);

            Directory.CreateDirectory(path);

            var nodes = GetNodes(id).ToList();

            for (var i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].IsFolder == false)
                {
                    File.Copy(GetNodePath(nodes[i].Id) + ".html", Path.Combine(path, nodes[i].Id + ".html"));
                }
                else
                {
                    var subNodes = GetNodes(id, nodes[i].Id);
                    nodes.AddRange(subNodes);
                }
            }

            var coursePath = GetCoursePath(id);
            foreach (var file in _TemplateFiles)
            {
                File.Copy(Path.Combine(coursePath, file), Path.Combine(path, file));
            }

            var helper = new ManifestManager();

            var manifest = new Manifest();
            var sw = new StreamWriter(Path.Combine(path, SCORM.ImsManifset));
            var parentItem = new Item();

            parentItem = AddSubItems(parentItem, null, id, helper, ref manifest);
            manifest.Organizations = ManifestManager.AddOrganization(manifest.Organizations, helper.CreateOrganization());
            manifest.Organizations.Default = manifest.Organizations[0].Identifier;
            manifest.Organizations[0].Items = parentItem.Items;
            manifest.Organizations[0].Title = course.Name;

            if (course.Sequencing != null)
            {
                var xml = new XmlSerializer(typeof(Sequencing));
                manifest.Organizations[0].Sequencing = (Sequencing)xml.DeserializeXElement(course.Sequencing);
            }
            var resource = new Resource
                               {
                                   Identifier = _ResourceIdForTemplateFiles,
                                   Files = _TemplateFiles.Select(file => new ManifestModels.ResourceModels.File(file)).ToList(),
                                   ScormType = ScormType.Asset
                               };

            manifest.Resources = ManifestManager.AddResource(manifest.Resources, resource);

            ManifestManager.Serialize(manifest, sw);
            sw.Close();

            Zipper.CreateZip(path + ".zip", path);

            return path + ".zip";
        }

        private Item AddSubItems(Item parentItem, Node parentNode, int courseId, ManifestManager helper, ref Manifest manifest)
        {
            var nodes = parentNode == null ? GetNodes(courseId) : GetNodes(courseId, parentNode.Id);
            
            foreach (var node in nodes)
            {
                if (node.IsFolder)
                {
                    var item = helper.CreateItem();
                    item.Title = node.Name;

                    if (node.Sequencing != null)
                    {
                        var xml = new XmlSerializer(typeof (Sequencing));
                        item.Sequencing = (Sequencing) xml.DeserializeXElement(node.Sequencing);
                    }

                    item = AddSubItems(item, node, courseId, helper, ref manifest);

                    parentItem = ManifestManager.AddItem(parentItem, item);
                }
                else
                {
                    var files = new List<ManifestModels.ResourceModels.File>();
                    files.Add(new ManifestModels.ResourceModels.File(node.Id + ".html"));

                    var resource = helper.CreateResource(ScormType.SCO, files, new[] { _ResourceIdForTemplateFiles });
                    resource.Href = node.Id + ".html";

                    manifest.Resources = ManifestManager.AddResource(manifest.Resources, resource);
                 
                    var item = helper.CreateItem(resource.Identifier);
                    item.Title = node.Name;

                    if (node.Sequencing != null)
                    {
                        var xml = new XmlSerializer(typeof(Sequencing));
                        item.Sequencing = (Sequencing)xml.DeserializeXElement(node.Sequencing);
                    }

                    parentItem = ManifestManager.AddItem(parentItem, item);
                }
            }
            return parentItem;
        }

        public void Import(string path, string owner)
        {
            var zipName = Path.GetFileNameWithoutExtension(path);

            var course = new Course
                             {
                                 Name = zipName,
                                 Owner = owner,
                                 Locked = true
                             };

            AddCourse(course);
            
            File.Copy(path, GetCoursePath(course.Id) + ".zip");
        }

        public void Parse(int courseId)
        {
            using (var db = new CourseManagementDbConext())
            {
                var course = db.Courses.Single(c => c.Id == courseId);

                if (!course.Locked.Value)
                {
                    return;
                }

                var coursePath = GetCoursePath(course.Id);
                var courseTempPath = GetCourseTempPath(course.Id);
                var manifestPath = Path.Combine(courseTempPath, SCORM.ImsManifset);

                Zipper.ExtractZipFile(coursePath + ".zip", courseTempPath);

                var reader = new XmlTextReader(new FileStream(manifestPath, FileMode.Open));
                var manifest = Manifest.Deserialize(reader);

                var importer = new Importer(manifest, course, this);

                importer.Import();

                course.Locked = false;

                db.SaveChanges();
            }
        }

        #endregion

        #region Node methods

        public IEnumerable<Node> GetNodes(int courseId)
        {
            return GetNodes(courseId, null);
        }

        public IEnumerable<Node> GetNodes(int courseId, int? parentId)
        {
            using (var db = new CourseManagementDbConext())
            {
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
        }

        public Node GetNode(int id)
        {
            using (var db = new CourseManagementDbConext())
            {
                return db.Nodes.SingleOrDefault(n => n.Id == id);
            }
        }

        public int? AddNode(Node node)
        {
            using (var db = new CourseManagementDbConext())
            {
                if (node.Sequencing == null)
                {
                    var xs = new XmlSerializer(typeof(Sequencing));
                    node.Sequencing = xs.SerializeToXElemet(new Sequencing());
                }

                db.Nodes.Add(node);
                db.SaveChanges();

                if (!node.IsFolder)
                {
                    var template = Path.Combine(GetTemplatesPath(), "iudico.html");

                    File.Copy(template, GetNodePath(node.Id) + ".html", true);
                }

                return node.Id;
            }
        }

        public void UpdateNode(int id, Node node)
        {
            using (var db = new CourseManagementDbConext())
            {
                var oldNode = db.Nodes.SingleOrDefault(n => n.Id == id);

                oldNode.Name = node.Name;
                oldNode.ParentId = node.ParentId;
                oldNode.Position = node.Position;
                oldNode.Sequencing = node.Sequencing;

                db.SaveChanges();
            }
        }

        public void DeleteNode(int id)
        {
            using (var db = new CourseManagementDbConext())
            {
                var node = db.Nodes.SingleOrDefault(n => n.Id == id);

                if (!node.IsFolder)
                {
                    @File.Delete(GetNodePath(id));
                }

                db.Nodes.Remove(node);
                db.SaveChanges();
            }
        }

        public void DeleteNodes(List<int> ids)
        {
            using (var db = new CourseManagementDbConext())
            {
                var nodes = (from n in db.Nodes where ids.Contains(n.Id) select n);

                foreach (var node in nodes)
                {
                    if (!node.IsFolder)
                    {
                        @File.Delete(GetNodePath(node.Id));
                    }
                }

                foreach (var node in nodes)
                {
                    db.Nodes.Remove(node);
                }
                
                db.SaveChanges();
            }
        }

        public int CreateCopy(Node node, int? parentId, int position)
        {
            using (var db = new CourseManagementDbConext())
            {
                var newnode = new Node
                                  {
                                      CourseId = node.CourseId,
                                      Name = node.Name,
                                      ParentId = parentId,
                                      IsFolder = node.IsFolder,
                                      Position = position
                                  };

                CopyNodes(node, newnode);

                db.Nodes.Add(newnode);
                db.SaveChanges();

                return newnode.Id;
            }
        }

        public string GetPreviewNodePath(int id)
        {
            var node = GetNode(id);

            return Path.Combine(@"Data\Courses", node.CourseId.ToString(), node.Id.ToString()) + ".html";
        }

        public string GetNodeContents(int id)
        {
            string nodePath = GetNodePath(id) + ".html";

            if (!File.Exists(nodePath))
            {
                return string.Empty;
            }

            return File.ReadAllText(nodePath);
        }

        public void UpdateNodeContents(int id, string data)
        {
            string nodePath = GetNodePath(id) + ".html";

            File.WriteAllText(nodePath, data);

            //return System.IO.File.ReadAllText(nodePath);
        }

        public string GetNodePath(int nodeId)
        {
            var node = GetDbContext().Nodes.SingleOrDefault(n => n.Id == nodeId);
            var path = Path.Combine(GetCoursePath(node.CourseId), node.Id.ToString());

            return path;
        }

        public string GetCoursePath(int courseId)
        {
            var path = GetCoursesPath();

            return Path.Combine(path, courseId.ToString());
        }

        public string GetCourseTempPath(int courseId)
        {
            var path = HttpContext.Current == null ? Path.Combine(Environment.CurrentDirectory, "Site") : HttpContext.Current.Request.PhysicalApplicationPath;

            return Path.Combine(path, @"Data\WorkFolder", courseId.ToString());
        }

        public string GetTemplatesPath()
        {
            var path = HttpContext.Current == null ? Path.Combine(Environment.CurrentDirectory, "Site") : HttpContext.Current.Request.PhysicalApplicationPath;

            return Path.Combine(path, @"Data\CourseTemplate");
        }

        protected string GetCoursesPath()
        {
            var path = HttpContext.Current == null ? Path.Combine(Environment.CurrentDirectory, "Site") : HttpContext.Current.Request.PhysicalApplicationPath;

            return Path.Combine(path, @"Data\Courses");
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