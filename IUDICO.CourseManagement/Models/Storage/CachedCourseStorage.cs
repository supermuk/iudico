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
            var id = _storage.AddCourse(course);

            _cacheProvider.Invalidate("courses");

            return id;
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