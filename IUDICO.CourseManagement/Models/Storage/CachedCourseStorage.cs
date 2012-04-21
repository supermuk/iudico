using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Caching;

namespace IUDICO.CourseManagement.Models.Storage
{
    public class CachedCourseStorage: ICourseStorage
    {
        private readonly ICourseStorage _storage;
        private readonly ICacheProvider _cacheProvider;
        private readonly object lockObject = new object();

        public CachedCourseStorage(ICourseStorage storage, ICacheProvider cachePrvoider)
        {
            _storage = storage;
            _cacheProvider = cachePrvoider;
        }


        /*
        #region Course methods

        public override IEnumerable<Course> GetCourses()
        {
            var courses = _cacheProvider["courses-all"] as IEnumerable<Course>;

            if (courses == null)
            {
                courses = base.GetCourses();
                _cacheProvider["courses-all"] = courses;

                foreach (var course in courses)
                {
                    _cacheProvider.AddTag("course-" + course.Id, "courses-all");
                }
            }

            return courses;
        }

        public override IEnumerable<Course> GetCourses(Guid userId)
        {
            var courses = _cacheProvider["courses-" + userId.ToString()] as IEnumerable<Course>;

            if (courses == null)
            {
                courses = base.GetCourses(userId);
                _cacheProvider["courses-" + userId.ToString()] = courses;
                
                foreach (var course in courses)
                {
                    _cacheProvider.AddTag("course-" + course.Id, "courses-" + userId.ToString());
                }
            }

            return courses;
        }

        public override IEnumerable<Course> GetCourses(string owner)
        {
            var courses = _cacheProvider["courses-owner-" + owner] as IEnumerable<Course>;

            if (courses == null)
            {
                courses = base.GetCourses(owner);
                _cacheProvider["courses-owner-" + owner] = courses;

                foreach (var course in courses)
                {
                    _cacheProvider.AddTag("course-" + course.Id, "courses-owner-" + owner);
                }

            }

            return courses;
        }

        public override IEnumerable<Course> GetCourses(User owner)
        {
            var courses = _cacheProvider["courses-owner-" + owner.Username] as IEnumerable<Course>;

            if (courses == null)
            {
                courses = base.GetCourses(owner);
                _cacheProvider["courses-owner-" + owner.Username] = courses;
                _cacheProvider["courses-" + owner.Id.ToString()] = courses;

                foreach (var course in courses)
                {
                    _cacheProvider.AddTag("course-" + course.Id, "courses-owner-" + owner.Username);
                }
            }

            return courses;
        }

        public override Course GetCourse(int id)
        {
            var course = _cacheProvider["course-" + id] as Course;

            if (course == null)
            {
                course = base.GetCourse(id);
                _cacheProvider["course-" + id] = course;
            }

            return course;
        }

        public override IEnumerable<User> GetCourseUsers(int courseId)
        {
            var courseUsers = _cacheProvider["courseusers-" + courseId] as IEnumerable<User>;

            if (courseUsers == null)
            {
                courseUsers = base.GetCourseUsers(courseId);

                _cacheProvider["courseusers-" + courseId] = courseUsers;

                foreach (var courseUser in courseUsers)
                {
                    _cacheProvider.AddTag("courseuser-" + courseUser.Id, "courseusers-" + courseId);
                }
            }

            return courseUsers;
        }

        public override void UpdateCourseUsers(int courseId, IEnumerable<Guid> userIds)
        {
            base.UpdateCourseUsers(courseId, userIds);

            var oldUserIds = _cacheProvider["courseusers-" + courseId] as IEnumerable<Guid> ?? new List<Guid>();
            foreach (var userId in oldUserIds)
            {
                _cacheProvider.DeleteTag("courseuser-" + userId, "courseusers-" + courseId);
            }

            _cacheProvider.Expire("courseusers-" + courseId);
        }

        public override void DeleteCourseUsers(Guid userId)
        {
            _cacheProvider.ExpireTag("courseuser-" + userId);
            
            base.DeleteCourseUsers(userId);
        }

        public override int AddCourse(Course course)
        {
            // TODO: correct expire cache
            _cacheProvider.ExpireTag("course-" + course.Id);
            
            return base.AddCourse(course);
        }

        public override void UpdateCourse(int id, Course course)
        {
            _cacheProvider.ExpireTag("course-" + course.Id);

            base.UpdateCourse(id, course);
        }

        public override void DeleteCourse(int id)
        {
            _cacheProvider.ExpireTag("course-" + id);

            base.DeleteCourse(id);
        }

        public override void DeleteCourses(List<int> ids)
        {
            foreach (var id in ids)
            {
                _cacheProvider.ExpireTag("course-" + id);
            }

            base.DeleteCourses(ids);
        }

        #endregion

        #region Node methods

        public override IEnumerable<Node> GetNodes(int courseId)
        {
            var nodes = _cacheProvider["nodes-" + courseId] as IEnumerable<Node>;

            if (nodes == null)
            {
                nodes = base.GetNodes(courseId);
                _cacheProvider["nodes-" + courseId] = nodes;

                foreach (var node in nodes)
                {
                    _cacheProvider.AddTag("node-" + node.Id, "nodes-" + courseId);
                }
            }

            return nodes;
        }

        public override IEnumerable<Node> GetNodes(int courseId, int? parentId)
        {
            var nodes = _cacheProvider["nodes-" + courseId + "-" + (parentId.ToString() ?? "null")] as IEnumerable<Node>;

            if (nodes == null)
            {
                nodes = base.GetNodes(courseId, parentId);
                _cacheProvider["nodes-" + courseId + "-" + (parentId.ToString() ?? "null")] = nodes;

                foreach (var node in nodes)
                {
                    _cacheProvider.AddTag("node-" + node.Id, "nodes-" + courseId + "-" + (parentId.ToString() ?? "null"));
                }

                _cacheProvider.AddTag("node-new", "nodes-" + courseId + "-" + (parentId.ToString() ?? "null"));
            }

            return nodes;
        }

        public override Node GetNode(int id)
        {
            var node = _cacheProvider["node-" + id] as Node;

            if (node == null)
            {
                node = base.GetNode(id);
                _cacheProvider["node-" + id] = node;
                _cacheProvider.AddTag("node-" + id, "node-" + id);
            }

            return node;
        }

        public override int? AddNode(Node node)
        {            
            _cacheProvider.Expire("nodes-" + node.CourseId + "-null");
            _cacheProvider.Expire("nodes-" + node.CourseId + "-" + node.ParentId);
                
            // TODO: CHECK correct expire cache

            return base.AddNode(node);
        }

        public override void UpdateNode(int id, Node node)
        {
            base.UpdateNode(id, node);

            _cacheProvider.ExpireTag("node-" + id);
        }

        public override void DeleteNode(int id)
        {
            base.DeleteNode(id);

            _cacheProvider.ExpireTag("node-" + id);
        }

        public override void DeleteNodes(List<int> ids)
        {
            base.DeleteNodes(ids);

            foreach (var id in ids)
            {
                _cacheProvider.ExpireTag("node-" + id);
            }
        }

        public override int CreateCopy(Node node, int? parentId, int position)
        {
            return base.CreateCopy(node, parentId, position);
        }

        public override string GetPreviewNodePath(int id)
        {
            return base.GetPreviewNodePath(id);
        }

        public override string GetNodeContents(int id)
        {
            return GetNodeContents(id);
        }

        public override void UpdateNodeContents(int id, string data)
        {
            base.UpdateNodeContents(id, data);
        }

        public override string GetNodePath(int nodeId)
        {
            return GetNodePath(nodeId);
        }

        public override string GetCoursePath(int courseId)
        {
            return base.GetCoursePath(courseId);
        }

        public override string GetCourseTempPath(int courseId)
        {
            return base.GetCourseTempPath(courseId);
        }

        public override string GetTemplatesPath()
        {
            return base.GetTemplatesPath();
        }

        protected override string GetCoursesPath()
        {
            return GetCoursesPath();
        }

        protected override void CopyNodes(Node node, Node newnode)
        {
            base.CopyNodes(node, newnode);
        }

        protected override void CreateFolders(Node newnode)
        {
            base.CreateFolders(newnode);
        }

        #endregion

        #region NodeResource methods

        public override IEnumerable<NodeResource> GetResources(int nodeId)
        {
            var resources = _cacheProvider["resources-" + nodeId] as IEnumerable<NodeResource>;

            if (resources == null)
            {
                resources = base.GetResources(nodeId);
                _cacheProvider["resources-" + nodeId] = resources;

                foreach (var resource in resources)
                {
                    _cacheProvider.AddTag("resource-" + resource.Id, "resources-" + nodeId);
                }
            }

            return resources;
        }

        public override NodeResource GetResource(int id)
        {
            var resource = _cacheProvider["resource-" + id] as NodeResource;

            if (resource == null)
            {
                resource = base.GetResource(id);
                _cacheProvider["resource-" + id] = resource;
                _cacheProvider.AddTag("resource-" + id, "resource-" + id);
            }

            return resource;
        }

        public override int AddResource(NodeResource resource, HttpPostedFileBase file)
        {
            _cacheProvider.Expire("resources-" + resource.NodeId);

            return base.AddResource(resource, file);
        }

        public override string GetResourcePath(int resId)
        {
            return base.GetResourcePath(resId);
        }

        public override string GetResourcePath(int nodeId, string fileName)
        {
            return base.GetResourcePath(nodeId, fileName);
        }

        public override void UpdateResource(int id, NodeResource resource)
        {
            _cacheProvider.ExpireTag("resource-" + id);

            base.UpdateResource(id, resource);
        }

        public override void DeleteResource(int id)
        {
            _cacheProvider.ExpireTag("resource-" + id);

            base.DeleteResource(id);
        }

        public override void DeleteResources(List<int> ids)
        {
            foreach (var id in ids)
            {
                _cacheProvider.ExpireTag("resource-" + id);
            }

            base.DeleteResources(ids);
        }
        #endregion
        */

        public IEnumerable<Course> GetCourses()
        {
            return _cacheProvider.Get<IEnumerable<Course>>("courses", @lockObject, () => _storage.GetCourses(), DateTime.Now.AddDays(1), "courses");
        }

        public IEnumerable<Course> GetCourses(Guid userId)
        {
            return _cacheProvider.Get<IEnumerable<Course>>("courses-" + userId, @lockObject, () => _storage.GetCourses(userId), DateTime.Now.AddDays(1), "courses");
        }

        public IEnumerable<Course> GetCourses(string owner)
        {
            return _cacheProvider.Get<IEnumerable<Course>>("courses-" + owner, @lockObject, () => _storage.GetCourses(owner), DateTime.Now.AddDays(1), "courses");
        }

        public IEnumerable<Course> GetCourses(User owner)
        {
            return _cacheProvider.Get<IEnumerable<Course>>("courses-" + owner.Username, @lockObject, () => _storage.GetCourses(owner), DateTime.Now.AddDays(1), "courses");
        }

        public Course GetCourse(int id)
        {
            return _cacheProvider.Get<Course>("course-" + id, @lockObject, () => _storage.GetCourse(id), DateTime.Now.AddDays(1), "course-" + id);
        }

        public int AddCourse(Course course)
        {
            //_cacheProvider.Invalidate(
            throw new NotImplementedException();
        }

        public void UpdateCourseUsers(int courseId, IEnumerable<Guid> userIds)
        {
            _storage.UpdateCourseUsers(courseId, userIds);

            _cacheProvider.Invalidate("courseusers");
        }

        public void DeleteCourseUsers(Guid userId)
        {
            _storage.DeleteCourseUsers(userId);

            _cacheProvider.Invalidate("courseusers");
        }

        public IEnumerable<User> GetCourseUsers(int courseId)
        {
            return _cacheProvider.Get<IEnumerable<User>>("courseusers", @lockObject, () => _storage.GetCourseUsers(courseId), DateTime.Now.AddDays(1), "courseusers");
        }

        public void UpdateCourse(int id, Course course)
        {
            _storage.UpdateCourse(id, course);

            _cacheProvider.Invalidate("courses", "course-" + id);
        }

        public void DeleteCourse(int id)
        {
            _storage.DeleteCourse(id);

            _cacheProvider.Invalidate("courses", "course-" + id);
        }

        public void DeleteCourses(List<int> ids)
        {
            _storage.DeleteCourses(ids);

            var courses = ids.Select(c => "course-" + c).ToArray();

            _cacheProvider.Invalidate(courses);
            _cacheProvider.Invalidate("courses");
        }

        public string Export(int id)
        {
            return _cacheProvider.Get<string>("course-path", @lockObject, () => _storage.Export(id), DateTime.Now.AddDays(1), "course-" + id);
        }

        public void Import(string path, string owner)
        {
            _storage.Import(path, owner);

            _cacheProvider.Invalidate("courses");
        }

        public void Parse(int id)
        {
            _storage.Parse(id);

            _cacheProvider.Invalidate("course-" + id, "courses");
        }

        public string GetCoursePath(int id)
        {
            return _storage.GetCoursePath(id);
        }

        public string GetCourseTempPath(int id)
        {
            return _storage.GetCourseTempPath(id);
        }

        public IEnumerable<Node> GetNodes(int courseId)
        {
            return _storage.GetNodes(courseId);
        }

        public IEnumerable<Node> GetNodes(int courseId, int? parentId)
        {
            return _cacheProvider.Get<IEnumerable<Node>>("nodes-" + courseId + "-" + parentId, @lockObject, () => _storage.GetNodes(courseId), DateTime.Now.AddDays(1), "nodes-" + courseId + "-" + parentId, "nodes-" + courseId);
        }

        public Node GetNode(int id)
        {
            return _cacheProvider.Get<Node>("node-" + id, @lockObject, () => _storage.GetNode(id), DateTime.Now.AddDays(1), "node-" + id);
        }

        public int? AddNode(Node node)
        {
            _cacheProvider.Invalidate("node-" + node.Id, "nodes", "course-" + node.CourseId, "courses");

            return _storage.AddNode(node);
        }

        public void UpdateNode(int id, Node node)
        {
            _storage.UpdateNode(id, node);

            _cacheProvider.Invalidate("node-" + node.Id, "nodes", "course-" + node.CourseId, "courses");
        }

        public void DeleteNode(int id)
        {
            var node = GetNode(id);

            _storage.DeleteNode(id);

            _cacheProvider.Invalidate("node-" + id, "nodes", "course-" + node.CourseId, "courses");
        }

        public IEnumerable<Node> DeleteNodes(List<int> ids)
        {
            var nodes = _storage.DeleteNodes(ids);

            _cacheProvider.Invalidate(nodes.Select(n => "course-" + n.CourseId).ToArray());
            _cacheProvider.Invalidate(ids.Select(s => "node-" + s).ToArray());
            _cacheProvider.Invalidate("nodes", "courses");

            return nodes;
        }

        public int CreateCopy(Node node, int? parentId, int position)
        {
            var id = _storage.CreateCopy(node, parentId, position);

            _cacheProvider.Invalidate("nodes", "courses", "course-" + node.CourseId);

            return id;
        }

        public string GetNodePath(int id)
        {
            return _storage.GetNodePath(id);
        }

        public string GetPreviewNodePath(int id)
        {
            return _storage.GetPreviewNodePath(id);
        }

        public string GetNodeContents(int id)
        {
            return _storage.GetNodeContents(id);
        }

        public void UpdateNodeContents(int id, string data)
        {
            _storage.UpdateNodeContents(id, data);
        }

        public IEnumerable<NodeResource> GetResources(int nodeId)
        {
            return _cacheProvider.Get<IEnumerable<NodeResource>>("noderesources-" + nodeId, @lockObject, () => _storage.GetResources(nodeId), DateTime.Now.AddDays(1), "noderesources");
        }

        public NodeResource GetResource(int id)
        {
            return _cacheProvider.Get<NodeResource>("noderesource-" + id, @lockObject, () => _storage.GetResource(id), DateTime.Now.AddDays(1), "noderesource-" + id);
        }

        public int AddResource(NodeResource resource, HttpPostedFileBase file)
        {
            var id = _storage.AddResource(resource, file);

            _cacheProvider.Invalidate("noderesource-" + resource.Id, "noderesources");

            return id;
        }

        public string GetResourcePath(int resId)
        {
            // TODO: check
            return _cacheProvider.Get<string>("noderesourcepath-" + resId, @lockObject, () => _storage.GetResourcePath(resId), DateTime.Now.AddDays(1), "noderesource-" + resId, "nodes", "noderesources");
        }

        public string GetResourcePath(int nodeId, string fileName)
        {
            return _cacheProvider.Get<string>("noderesourcepath-" + nodeId + "-" + fileName, lockObject, () => _storage.GetResourcePath(nodeId, fileName), DateTime.Now.AddDays(1), "node-" + nodeId, "nodes", "noderesources");
        }

        public void UpdateResource(int id, NodeResource resource)
        {
            _storage.UpdateResource(id, resource);

            _cacheProvider.Invalidate("noderesource-" + id, "noderesources");
        }

        public void DeleteResource(int id)
        {
            _storage.DeleteResource(id);

            _cacheProvider.Invalidate("noderesource-" + id, "noderesources");
        }

        public void DeleteResources(List<int> ids)
        {
            _storage.DeleteResources(ids);

            _cacheProvider.Invalidate(ids.Select(i => "noderesource-" + i).ToArray());
            _cacheProvider.Invalidate("noderesources");
        }
    }
}