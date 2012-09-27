using System.Collections.Generic;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.Common.Models.Services;

namespace IUDICO.CourseManagement.Models
{
    public class CourseService : ICourseService
    {
        private readonly ICourseStorage courseStorage;

        public CourseService(ICourseStorage courseStorage)
        {
            this.courseStorage = courseStorage;
        }

        #region Implementation of ICourseService

        public IEnumerable<Course> GetCourses()
        {
            return this.courseStorage.GetCourses();
        }

        public IEnumerable<Course> GetCourses(User owner)
        {
            return this.courseStorage.GetCourses(owner);
        }

        public Course GetCourse(int id)
        {
            return this.courseStorage.GetCourse(id);
        }

        public IudicoCourseInfo GetCourseInfo(int id)
        {
            return this.courseStorage.GetCourseInfo(id);
        }

        public IEnumerable<Node> GetNodes(int courseId)
        {
            return this.courseStorage.GetNodes(courseId);
        }

        public IEnumerable<Node> GetNodes(int courseId, int? parentId)
        {
            return this.courseStorage.GetNodes(courseId, parentId);
        }

        public IEnumerable<Node> GetAllNodes(int courseId)
        {
            return this.courseStorage.GetAllNodes(courseId);
        }

        public Node GetNode(int id)
        {
            return this.courseStorage.GetNode(id);
        }

        public string GetNodeContents(int id)
        {
            return this.courseStorage.GetNodeContents(id);
        }

        public string Export(int id, bool exportForPlayCourse = false)
        {
            return this.courseStorage.Export(id, exportForPlayCourse);
        }

        public void Import(string path, string owner)
        {
           this.courseStorage.Import(path, owner);
        }

        public void Import(string path, string name, string owner)
        {
           this.courseStorage.Import(path, name, owner);
        }

        public void Unlock(int id)
        {
           this.courseStorage.Parse(id);
        }

        #endregion
    }
}