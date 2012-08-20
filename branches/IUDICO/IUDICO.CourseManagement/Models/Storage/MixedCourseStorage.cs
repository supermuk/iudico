using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Shared;
using IUDICO.CourseManagement.Helpers;
using IUDICO.CourseManagement.Models.ManifestModels;
using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.ManifestModels.ResourceModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;
using File = System.IO.File;

namespace IUDICO.CourseManagement.Models.Storage
{
    using System.Globalization;

    public class MixedCourseStorage : ICourseStorage
    {
        protected readonly ILmsService LmsService;
        protected readonly LinqLogger Logger;

        private readonly string[] templateFiles = { "api.js", "checkplayer.js", "flensed.js", "flXHR.js", "flXHR.swf", "iudico.css", "iudico.js", "jquery-1.5.2.min.js", "jquery.flXHRproxy.js", "jquery.xhr.js", "questions.js", "sco.js", "swfobject.js", "updateplayer.swf", "sh_main.min.js", "sh_cpp.min.js", "sh_csharp.min.js", "sh_java.min.js", "sh_xml.min.js", "sh_style.css", "wait.gif" };

        private const string ResourceIdForTemplateFiles = "TemplateFiles";

        public MixedCourseStorage(ILmsService lmsService)
        {
            this.LmsService = lmsService;
        }

        public MixedCourseStorage(ILmsService lmsService, LinqLogger logger)
        {
            this.LmsService = lmsService;
            this.Logger = logger;
        }

        protected virtual IDataContext GetDbContext()
        {
            var db = new DBDataContext();

            #if DEBUG
                db.Log = this.Logger;
            #endif

            return db;
        }

        #region IStorage Members

        #region Course methods

        public virtual IEnumerable<Course> GetCourses()
        {

            return this.GetDbContext().Courses.Where(c => c.Deleted == false).AsEnumerable();
        }

        public virtual IEnumerable<Course> GetCourses(Guid userId)
        {
            var db = this.GetDbContext();
            var courses = from c in db.Courses
                          join u in db.CourseUsers on c.Id equals u.CourseRef
                          where u.UserRef == userId && c.Deleted == false
                          select c;
            return courses;
        }

        public virtual IEnumerable<Course> GetCourses(string owner)
        {
            return this.GetDbContext().Courses.Where(i => i.Owner == owner && i.Deleted == false);
        }

        public virtual IEnumerable<Course> GetCourses(User owner)
        {
            return this.GetDbContext().Courses.Where(i => i.Owner == owner.Username && i.Deleted == false);
        }

        public virtual Course GetCourse(int id)
        {
            return this.GetDbContext().Courses.Single(c => c.Id == id);
        }

        public virtual IEnumerable<User> GetCourseUsers(int courseId)
        {
            var db = this.GetDbContext();

            var userIds = db.CourseUsers.Where(i => i.CourseRef == courseId).Select(i => i.UserRef).ToList();
            var users = this.LmsService.FindService<IUserService>().GetUsers().Where(i => userIds.Contains(i.Id)).ToList();
            return users;
        }

        public virtual void UpdateCourseUsers(int courseId, IEnumerable<Guid> userIds)
        {
            var db = this.GetDbContext();
            
            var oldUsers = db.CourseUsers.Where(i => i.CourseRef == courseId);
            db.CourseUsers.DeleteAllOnSubmit(oldUsers);

            if (userIds != null)
            {
                var newUsers = userIds.Select(i => new CourseUser { CourseRef = courseId, UserRef = i });
                db.CourseUsers.InsertAllOnSubmit(newUsers);
            }

            db.SubmitChanges();
        }

        public virtual void DeleteCourseUsers(Guid userId)
        {
            var db = this.GetDbContext();
            
            var courseUsers = db.CourseUsers.Where(i => i.UserRef == userId);
            
            db.CourseUsers.DeleteAllOnSubmit(courseUsers);
            db.SubmitChanges();
        }

        public virtual int AddCourse(Course course)
        {
            course.Created = DateTime.Now;
            course.Updated = DateTime.Now;
            course.UpdatedBy = course.Owner;
            var db = this.GetDbContext();

            db.Courses.InsertOnSubmit(course);
            db.SubmitChanges();

            var path = this.GetCoursePath(course.Id);
            @Directory.CreateDirectory(path);

            var templatePath = this.GetTemplatesPath();

            foreach (var templateFile in this.templateFiles)
            {
                File.Copy(Path.Combine(templatePath, templateFile), Path.Combine(path, templateFile), true);
            }

            this.LmsService.Inform(CourseNotifications.CourseCreate, course);

            return course.Id;
        }

        public virtual void UpdateCourse(int id, Course course)
        {
            var db = this.GetDbContext();

            var oldCourse = db.Courses.Single(c => c.Id == id);

            oldCourse.Name = course.Name;
            oldCourse.Updated = DateTime.Now;
            oldCourse.Sequencing = course.Sequencing;
            oldCourse.UpdatedBy = this.LmsService.FindService<IUserService>().GetCurrentUser().Username;

            db.SubmitChanges();

            course.Updated = oldCourse.Updated;
            course.Created = oldCourse.Created;
            course.UpdatedBy = oldCourse.UpdatedBy;
            course.Locked = oldCourse.Locked;
            course.Owner = oldCourse.Owner;
            course.Id = oldCourse.Id;

            this.LmsService.Inform(CourseNotifications.CourseEdit, course);
        }

        public virtual void DeleteCourse(int id)
        {
            var db = this.GetDbContext();
            var course = db.Courses.Single(c => c.Id == id);

            if (course.Owner != this.LmsService.FindService<IUserService>().GetCurrentUser().Username)
            {
                return;
            }

            course.Deleted = true;

            db.SubmitChanges();

            this.LmsService.Inform(CourseNotifications.CourseDelete, course);
        }

        public virtual void DeleteCourses(List<int> ids)
        {
            var db = this.GetDbContext();

            var courses = (from n in db.Courses where ids.Contains(n.Id) select n);

            foreach (var course in courses)
            {
                if (course.Owner != this.LmsService.FindService<IUserService>().GetCurrentUser().Username)
                {
                    continue;
                }

                course.Deleted = true;

                this.LmsService.Inform(CourseNotifications.CourseDelete, course);
            }

            db.SubmitChanges();
        }

        public virtual string Export(int id)
        {
            var course = this.GetCourse(id);

            var path = this.GetCoursePath(id);

            if (course.Locked != null && course.Locked.Value)
            {
                return path + ".zip";
            }

            path = Path.Combine(path, Guid.NewGuid().ToString());
            path = Path.Combine(path, course.Name);

            Directory.CreateDirectory(path);

            var nodes = this.GetNodes(id).ToList();

            for (var i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].IsFolder == false)
                {
                    File.Copy(this.GetNodePath(nodes[i].Id) + ".html", Path.Combine(path, nodes[i].Id + ".html"));
                }
                else
                {
                    var subNodes = GetNodes(id, nodes[i].Id);
                    nodes.AddRange(subNodes);
                }
            }

            var coursePath = this.GetCoursePath(id);

            FileHelper.DirectoryCopy(Path.Combine(coursePath, "Node"), Path.Combine(path, "Node"));

            foreach (var file in this.templateFiles)
            {
                File.Copy(Path.Combine(coursePath, file), Path.Combine(path, file));
            }

            var helper = new ManifestManager();

            var manifest = new Manifest();
            var sw = new StreamWriter(Path.Combine(path, SCORM.ImsManifset));
            var parentItem = new Item();

            parentItem = this.AddSubItems(parentItem, null, id, helper, ref manifest);
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
                                   Identifier = ResourceIdForTemplateFiles,
                                   Files = this.templateFiles.Select(file => new ManifestModels.ResourceModels.File(file)).ToList(),
                                   ScormType = ScormType.Asset
                               };

            manifest.Resources = ManifestManager.AddResource(manifest.Resources, resource);

            ManifestManager.Serialize(manifest, sw);
            sw.Close();

            Zipper.CreateZip(path + ".zip", path);

            return path + ".zip";
        }

        protected Item AddSubItems(Item parentItem, Node parentNode, int courseId, ManifestManager helper, ref Manifest manifest)
        {
            var nodes = parentNode == null ? this.GetNodes(courseId) : this.GetNodes(courseId, parentNode.Id);
            
            foreach (var node in nodes)
            {
                if (node.IsFolder)
                {
                    var item = helper.CreateItem();
                    item.Title = node.Name;

                    if (node.Sequencing != null)
                    {
                        var xml = new XmlSerializer(typeof(Sequencing));
                        item.Sequencing = (Sequencing)xml.DeserializeXElement(node.Sequencing);
                    }

                    item = this.AddSubItems(item, node, courseId, helper, ref manifest);

                    parentItem = ManifestManager.AddItem(parentItem, item);
                }
                else
                {
                    var files = new List<ManifestModels.ResourceModels.File>();
                    files.Add(new ManifestModels.ResourceModels.File(node.Id + ".html"));

                    var resource = helper.CreateResource(ScormType.SCO, files, new[] { ResourceIdForTemplateFiles });
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

        public virtual void Import(string path, string owner)
        {
            var zipName = Path.GetFileNameWithoutExtension(path);

            var course = new Course
                             {
                                 Name = zipName,
                                 Owner = owner,
                                 Locked = true
                             };

            this.AddCourse(course);
            
            File.Copy(path, this.GetCoursePath(course.Id) + ".zip");
        }

        public virtual void Parse(int courseId)
        {
            var db = this.GetDbContext();
            var course = db.Courses.Single(c => c.Id == courseId);

            if (!course.Locked.Value)
            {
                return;
            }

            var coursePath = this.GetCoursePath(course.Id);
            var courseTempPath = this.GetCourseTempPath(course.Id);
            var manifestPath = Path.Combine(courseTempPath, SCORM.ImsManifset);

            Zipper.ExtractZipFile(coursePath + ".zip", courseTempPath);

            var reader = new XmlTextReader(new FileStream(manifestPath, FileMode.Open));
            var manifest = Manifest.Deserialize(reader);
            
            var importer = new Importer(manifest, course, this);

            importer.Import();

            // QUICK FIX for importing images
            var imagesPath = Path.Combine(courseTempPath, "Node");
            if (Directory.Exists(imagesPath))
            {
                FileHelper.DirectoryCopy(imagesPath, Path.Combine(coursePath, "Node"));
            }

            // QUICK FIX for "Row not found or changed." exception
            db = this.GetDbContext();
            course = db.Courses.Single(c => c.Id == courseId);
            course.Locked = false;

            db.SubmitChanges();
        }

        #endregion

        #region Node methods

        public virtual IEnumerable<Node> GetNodes(int courseId)
        {
            return GetNodes(courseId, null);
        }

        public virtual IEnumerable<Node> GetNodes(int courseId, int? parentId)
        {
            var db = this.GetDbContext();

            var course = this.GetCourse(courseId);
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

        public IEnumerable<Node> GetAllNodes(int courseId)
        {
            var db = this.GetDbContext();

            var course = this.GetCourse(courseId);
            var nodes = course.Nodes.OrderBy(n => n.Position).ToList();

            return nodes;
        }

        public virtual Node GetNode(int id)
        {
            return this.GetDbContext().Nodes.SingleOrDefault(n => n.Id == id);
        }

        public virtual int? AddNode(Node node)
        {
            var db = this.GetDbContext();

            if (node.Sequencing == null)
            {
                var xs = new XmlSerializer(typeof(Sequencing));
                node.Sequencing = xs.SerializeToXElemet(new Sequencing());
            }

            db.Nodes.InsertOnSubmit(node);
            db.SubmitChanges();

            if (!node.IsFolder)
            {
                var template = Path.Combine(this.GetTemplatesPath(), "iudico.html");

                File.Copy(template, this.GetNodePath(node.Id) + ".html", true);
            }

            this.LmsService.Inform(CourseNotifications.NodeCreate, node);

            var course = node.Course;
            this.UpdateCourse(course.Id, course);

            return node.Id;
        }

        public virtual void UpdateNode(int id, Node node)
        {
            var db = this.GetDbContext();
            object[] data = new object[2];

            var oldNode = db.Nodes.SingleOrDefault(n => n.Id == id);
            var newNode = oldNode;

            newNode.Name = node.Name;
            newNode.ParentId = node.ParentId;
            newNode.Position = node.Position;
            newNode.Sequencing = node.Sequencing;

            data[0] = oldNode;
            data[1] = newNode;

            db.SubmitChanges();
            this.LmsService.Inform(CourseNotifications.NodeEdit, data);

            var course = newNode.Course;
            this.UpdateCourse(course.Id, course);
        }

        public virtual void DeleteNode(int id)
        {
            var db = this.GetDbContext();

            var node = db.Nodes.SingleOrDefault(n => n.Id == id);

            if (!node.IsFolder)
            {
                @File.Delete(this.GetNodePath(id));
            }

            db.Nodes.DeleteOnSubmit(node);
            db.SubmitChanges();

            this.LmsService.Inform(CourseNotifications.NodeDelete, node);

            var course = node.Course;
            this.UpdateCourse(course.Id, course);
        }

        public virtual IEnumerable<Node> DeleteNodes(List<int> ids)
        {
            var db = this.GetDbContext();

            var nodes = (from n in db.Nodes where ids.Contains(n.Id) select n);

            foreach (var node in nodes)
            {
                if (!node.IsFolder)
                {
                    @File.Delete(this.GetNodePath(node.Id));
                }
            }

            db.Nodes.DeleteAllOnSubmit(nodes);
            db.SubmitChanges();

            return nodes;
        }

        public virtual int CreateCopy(Node node, int? parentId, int position)
        {
            var db = this.GetDbContext();

            var newnode = new Node
                              {
                                  CourseId = node.CourseId,
                                  Name = node.Name,
                                  ParentId = parentId,
                                  IsFolder = node.IsFolder,
                                  Position = position
                              };


            this.CopyNodes(node, newnode);

            this.AddNode(node);
/*
            db.Nodes.InsertOnSubmit(newnode);
            db.SubmitChanges();
*/
            return newnode.Id;
        }

        public virtual string GetPreviewNodePath(int id)
        {
            var node = this.GetNode(id);

            return Path.Combine(@"Data\Courses", node.CourseId.ToString(), node.Id.ToString()) + ".html";
        }

        public virtual string GetNodeContents(int id)
        {
            string nodePath = this.GetNodePath(id) + ".html";

            if (!File.Exists(nodePath))
            {
                return string.Empty;
            }

            return File.ReadAllText(nodePath);
        }

        public virtual void UpdateNodeContents(int id, string data)
        {
            string nodePath = this.GetNodePath(id) + ".html";

            File.WriteAllText(nodePath, data);

            var course = this.GetNode(id).Course;
            this.UpdateCourse(course.Id, course);

            this.LmsService.Inform(CourseNotifications.NodeContentEdit, this.GetNode(id));

            // return System.IO.File.ReadAllText(nodePath);
        }

        public virtual string GetNodePath(int nodeId)
        {
            var node = this.GetNode(nodeId);
            var path = Path.Combine(this.GetCoursePath(node.CourseId), node.Id.ToString());

            return path;
        }

        public virtual string GetCoursePath(int courseId)
        {
            var path = this.GetCoursesPath();

            return Path.Combine(path, courseId.ToString());
        }

        public virtual string GetCourseTempPath(int courseId)
        {
            var path = HttpContext.Current == null ? System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath : HttpContext.Current.Request.PhysicalApplicationPath;

            return Path.Combine(path, @"Data\WorkFolder", courseId.ToString());
        }

        public virtual string GetTemplatesPath()
        {
            var path = HttpContext.Current == null ? System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath : HttpContext.Current.Request.PhysicalApplicationPath;

            return Path.Combine(path, @"Data\CourseTemplate");
        }

        protected virtual string GetCoursesPath()
        {
			  string path = string.Empty;
			  try
			  {
				  path = HttpContext.Current == null ? System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath : HttpContext.Current.Request.PhysicalApplicationPath;
			  }
			  catch
			  {
				  path = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
				  path = path.Replace("Plugins/IUDICO.CourseManagement.DLL", "");
			  }

           return Path.Combine(path, @"Data\Courses");
        }

        protected virtual void CopyNodes(Node node, Node newnode)
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
                    this.CopyNodes(child, newchild);
                }
            }
            var course = node.Course;
            this.UpdateCourse(course.Id, course);
        }

        protected virtual void CreateFolders(Node newnode)
        {
            foreach (var child in newnode.Nodes)
            {
                if (!child.IsFolder)
                {
                    continue;
                }

                var path = this.GetNodePath(child.Id);
                Directory.CreateDirectory(path);

                this.CreateFolders(child);
            }
            var course = newnode.Course;
            this.UpdateCourse(course.Id, course);
        }

        #endregion

        #region NodeResource methods


        public virtual string GetResourcePath(int resId)
        {
            // var res = GetResource(resId);
            var res = this.GetDbContext().NodeResources.Single(n => n.Id == resId);
            var path = Path.Combine(this.GetNodePath(res.NodeId ?? -1), res.Path);

            return path;
        }

        public virtual string GetResourcePath(int nodeId, string fileName)
        {
            // var node = GetNode(nodeId);
            var node = this.GetDbContext().Nodes.SingleOrDefault(n => n.Id == nodeId);
            var path = Path.Combine(this.GetCoursePath(node.CourseId), "Node");
            path = Path.Combine(path, nodeId.ToString(CultureInfo.InvariantCulture));
            path = Path.Combine(path, "Images");
            path = Path.Combine(path, fileName);

            return path;
        }

        
        #endregion

        #endregion
    }
}