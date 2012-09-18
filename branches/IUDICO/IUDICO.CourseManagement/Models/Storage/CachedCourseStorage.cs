using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models;

namespace IUDICO.CourseManagement.Models.Storage
{
    public class CachedCourseStorage : ICourseStorage
    {
        private readonly ICourseStorage storage;
        private readonly ICacheProvider cacheProvider;
        private readonly object lockObject = new object();

        public CachedCourseStorage(ICourseStorage storage, ICacheProvider cachePrvoider)
        {
            this.storage = storage;
            this.cacheProvider = cachePrvoider;
        }

        public IEnumerable<Course> GetCourses()
        {
            return this.cacheProvider.Get<IEnumerable<Course>>("courses", @lockObject, () => this.storage.GetCourses().ToList(), DateTime.Now.AddDays(1), "courses");
        }

        public IEnumerable<Course> GetCourses(Guid userId)
        {
            return this.cacheProvider.Get<IEnumerable<Course>>("courses-" + userId, @lockObject, () => this.storage.GetCourses(userId).ToList(), DateTime.Now.AddDays(1), "courses");
        }

        public IEnumerable<Course> GetCourses(string owner)
        {
            return this.cacheProvider.Get<IEnumerable<Course>>("courses-" + owner, @lockObject, () => this.storage.GetCourses(owner).ToList(), DateTime.Now.AddDays(1), "courses");
        }

        public IEnumerable<Course> GetCourses(User owner)
        {
            return this.cacheProvider.Get<IEnumerable<Course>>("courses-" + owner.Username, @lockObject, () => this.storage.GetCourses(owner).ToList(), DateTime.Now.AddDays(1), "courses");
        }

        public Course GetCourse(int id)
        {
            return this.cacheProvider.Get<Course>("course-" + id, @lockObject, () => this.storage.GetCourse(id), DateTime.Now.AddDays(1), "course-" + id);
        }

        public int AddCourse(Course course)
        {
            var id = this.storage.AddCourse(course);

            this.cacheProvider.Invalidate("courses");

            return id;
        }

        public int AddCourseInfo(IudicoCourseInfo courseInfo)
        {
            var id = this.storage.AddCourseInfo(courseInfo);

            this.cacheProvider.Invalidate("coursesInfo");

            return id;
        }

        public IudicoCourseInfo GetCourseInfo(int id)
        {
            return this.cacheProvider.Get<IudicoCourseInfo>("courseInfo-" + id, @lockObject, () => this.storage.GetCourseInfo(id), DateTime.Now.AddDays(1), "courseInfo-" + id);
        }

        public void UpdateCourseUsers(int courseId, IEnumerable<Guid> userIds)
        {
            this.storage.UpdateCourseUsers(courseId, userIds);

            this.cacheProvider.Invalidate("courseusers");
        }

        public void DeleteCourseUsers(Guid userId)
        {
            this.storage.DeleteCourseUsers(userId);

            this.cacheProvider.Invalidate("courseusers");
        }

        public IEnumerable<User> GetCourseUsers(int courseId)
        {
            return this.cacheProvider.Get("courseusers-" + courseId, @lockObject, () => this.storage.GetCourseUsers(courseId).ToList(), DateTime.Now.AddDays(1), "courseusers");
        }

        public void UpdateCourse(int id, Course course)
        {
            this.storage.UpdateCourse(id, course);

            this.cacheProvider.Invalidate("courses", "course-" + id);
        }

        public void DeleteCourse(int id)
        {
            this.storage.DeleteCourse(id);

            this.cacheProvider.Invalidate("courses", "course-" + id);
        }

        public void DeleteCourses(List<int> ids)
        {
            this.storage.DeleteCourses(ids);

            var courses = ids.Select(c => "course-" + c).ToArray();

            this.cacheProvider.Invalidate(courses);
            this.cacheProvider.Invalidate("courses");
        }

        public string Export(int id, bool exportForPlayCourse = false)
        {
            return this.cacheProvider.Get<string>("course-path", @lockObject, () => this.storage.Export(id, exportForPlayCourse), DateTime.Now.AddDays(1), "course-" + id);
        }

        public void Import(string path, string owner)
        {
            this.storage.Import(path, owner);

            this.cacheProvider.Invalidate("courses");
        }

        public void Import(string path, string courseName, string owner)
        {
           this.storage.Import(path, courseName, owner);

           this.cacheProvider.Invalidate("courses");
        }

        public void Parse(int id)
        {
            this.storage.Parse(id);

            this.cacheProvider.Invalidate("course-" + id, "courses");
        }

        public string GetCoursePath(int id)
        {
            return this.storage.GetCoursePath(id);
        }

        public string GetCourseTempPath(int id)
        {
            return this.storage.GetCourseTempPath(id);
        }

        public IEnumerable<Node> GetNodes(int courseId)
        {
            return this.storage.GetNodes(courseId);
        }

        public IEnumerable<Node> GetNodes(int courseId, int? parentId)
        {
            return this.cacheProvider.Get<IEnumerable<Node>>("nodes-" + courseId + "-" + parentId, @lockObject, () => this.storage.GetNodes(courseId).ToList(), DateTime.Now.AddDays(1), "nodes-" + courseId + "-" + parentId, "nodes-" + courseId);
        }

        public IEnumerable<Node> GetAllNodes(int courseId)
        {
            return this.cacheProvider.Get<IEnumerable<Node>>("nodes-" + courseId, @lockObject, () => this.storage.GetAllNodes(courseId).ToList(), DateTime.Now.AddDays(1), "nodes-" + courseId);
        }

        public Node GetNode(int id)
        {
            return this.cacheProvider.Get<Node>("node-" + id, @lockObject, () => this.storage.GetNode(id), DateTime.Now.AddDays(1), "node-" + id);
        }

        public int? AddNode(Node node)
        {
            this.cacheProvider.Invalidate("node-" + node.Id, "nodes", "course-" + node.CourseId, "courses");

            return this.storage.AddNode(node);
        }

        public void UpdateNode(int id, Node node)
        {
            this.storage.UpdateNode(id, node);

            this.cacheProvider.Invalidate("node-" + node.Id, "nodes", "course-" + node.CourseId, "courses");
        }

        public void DeleteNode(int id)
        {
            var node = this.GetNode(id);

            this.storage.DeleteNode(id);

            this.cacheProvider.Invalidate("node-" + id, "nodes", "course-" + node.CourseId, "courses");
        }

        public IEnumerable<Node> DeleteNodes(List<int> ids)
        {
            var nodes = this.storage.DeleteNodes(ids);

            this.cacheProvider.Invalidate(nodes.Select(n => "course-" + n.CourseId).ToArray());
            this.cacheProvider.Invalidate(ids.Select(s => "node-" + s).ToArray());
            this.cacheProvider.Invalidate("nodes", "courses");

            return nodes;
        }

        public int CreateCopy(Node node, int? parentId, int position)
        {
            var id = this.storage.CreateCopy(node, parentId, position);

            this.cacheProvider.Invalidate("nodes", "courses", "course-" + node.CourseId);

            return id;
        }

        public string GetNodePath(int id)
        {
            return this.storage.GetNodePath(id);
        }

        public string GetPreviewNodePath(int id)
        {
            return this.storage.GetPreviewNodePath(id);
        }

        public string GetNodeContents(int id)
        {
            return this.storage.GetNodeContents(id);
        }

        public void UpdateNodeContents(int id, string data)
        {
            this.storage.UpdateNodeContents(id, data);
        }

    }
}