using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models;

namespace IUDICO.UnitTests.Fakes
{
    public class FakeCourseService : ICourseService
    {
        List<Course> courses { get; set; }

        public FakeCourseService()
        {
            courses = new List<Course>();
            courses.Add(new Course() { Name = "Course1", Id = 1, Owner="User1" });
            courses.Add(new Course() { Name = "Course2", Id = 2, Owner="User2" });
            courses.Add(new Course() { Name = "Course3", Id = 3, Owner="User1" });
        }

        public IEnumerable<Course> GetCourses()
        {
            return courses;
        }

        public IEnumerable<Course> GetCourses(User owner)
        {
            return courses.Where(item => item.Owner == owner.Username);
        }

        public Course GetCourse(int id)
        {
            return courses[id - 1];
        }

        public string Export(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Node> GetNodes(int courseId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Node> GetNodes(int courseId, int? parentId)
        {
            throw new NotImplementedException();
        }

        public Node GetNode(int id)
        {
            throw new NotImplementedException();
        }

        public string GetNodeContents(int id)
        {
            throw new NotImplementedException();
        }
    }
}
