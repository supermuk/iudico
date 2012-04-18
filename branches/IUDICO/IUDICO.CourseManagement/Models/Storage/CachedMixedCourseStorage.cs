using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Cache;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;

namespace IUDICO.CourseManagement.Models.Storage
{
    public class CachedMixedCourseStorage: MixedCourseStorage
    {
        protected ICacheProvider _cacheProvider;

        public CachedMixedCourseStorage(ILmsService lmsService, ICacheProvider cachePrvoider)
            : base(lmsService)
        {
            _cacheProvider = cachePrvoider;
        }

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
    }
}