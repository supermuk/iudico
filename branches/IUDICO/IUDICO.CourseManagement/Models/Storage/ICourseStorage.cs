using System;
using System.Collections.Generic;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.CourseManagement.Models.Storage
{
    public interface ICourseStorage
    {
        #region Course methods

        IEnumerable<Course> GetCourses();
        IEnumerable<Course> GetCourses(Guid userId);
        IEnumerable<Course> GetCourses(string owner);
        IEnumerable<Course> GetCourses(User owner);
        Course GetCourse(int id);
        int AddCourse(Course course);
        void UpdateCourseUsers(int courseId, IEnumerable<Guid> userIds);
        void DeleteCourseUsers(Guid userId);
        IEnumerable<User> GetCourseUsers(int courseId);
        void UpdateCourse(int id, Course course);
        void DeleteCourse(int id);
        void DeleteCourses(List<int> ids);
        string Export(int id);
        void Import(string path, string owner);
        void Parse(int id);
        string GetCoursePath(int id);
        string GetCourseTempPath(int id);

        #endregion

        #region Node methods

        IEnumerable<Node> GetNodes(int courseId);
        IEnumerable<Node> GetNodes(int courseId, int? parentId);
        Node GetNode(int id);
        int? AddNode(Node node);
        void UpdateNode(int id, Node node);
        void DeleteNode(int id);
        IEnumerable<Node> DeleteNodes(List<int> ids);
        int CreateCopy(Node node, int? parentId, int position);
        string GetNodePath(int id);
        string GetPreviewNodePath(int id);
        string GetNodeContents(int id);
        void UpdateNodeContents(int id, string data);

        #endregion
    }
}