using System.Collections.Generic;
using IUDICO.Common.Models;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.Common.Models.Services;

namespace IUDICO.CourseManagement.Models
{
    public class CourseService: ICourseService
    {
        private readonly ICourseStorage _CourseStorage;

        public CourseService(ICourseStorage courseStorage)
        {
            _CourseStorage = courseStorage;
        }

        #region Implementation of ICourseService

        public IEnumerable<Course> GetCourses()
        {
            return _CourseStorage.GetCourses();
        }

        public IEnumerable<Course> GetCourses(User owner)
        {
            return _CourseStorage.GetCourses(owner);
        }

        public Course GetCourse(int id)
        {
            return _CourseStorage.GetCourse(id);
        }

        public IEnumerable<Node> GetNodes(int courseId)
        {
            return _CourseStorage.GetNodes(courseId);
        }

        public IEnumerable<Node> GetNodes(int courseId, int? parentId)
        {
            return _CourseStorage.GetNodes(courseId, parentId);
        }

        public Node GetNode(int id)
        {
            return _CourseStorage.GetNode(id);
        }

        public string GetNodeContents(int id)
        {
            return _CourseStorage.GetNodeContents(id);
        }

        public string Export(int id)
        {
            return _CourseStorage.Export(id);
        }

        #endregion
    }
}