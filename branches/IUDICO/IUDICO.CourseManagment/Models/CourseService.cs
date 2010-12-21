using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.Common.Models.Services;

namespace IUDICO.CourseManagement.Models
{
    public class CourseService: ICourseService
    {
        private ICourseStorage _courseStorage;

        public CourseService(ICourseStorage courseStorage)
        {
            _courseStorage = courseStorage;
        }

        #region Implementation of ICourseService

        public IEnumerable<Course> GetCourses()
        {
            return _courseStorage.GetCourses();
        }

        public Course GetCourse(int id)
        {
            return _courseStorage.GetCourse(id);
        }

        public IEnumerable<Node> GetNodes(int courseId)
        {
            return _courseStorage.GetNodes(courseId);
        }

        public IEnumerable<Node> GetNodes(int courseId, int? parentId)
        {
            return _courseStorage.GetNodes(courseId, parentId);
        }

        public Node GetNode(int id)
        {
            return _courseStorage.GetNode(id);
        }

        public string GetNodeContents(int id)
        {
            return _courseStorage.GetNodeContents(id);
        }

        #endregion
    }
}