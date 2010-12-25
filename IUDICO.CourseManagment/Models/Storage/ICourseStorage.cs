using System;
using System.Collections.Generic;
using IUDICO.Common.Models;

namespace IUDICO.CourseManagement.Models.Storage
{
    public interface ICourseStorage
    {
        #region Course methods

        IEnumerable<Course> GetCourses();
        Course GetCourse(int id);
        int AddCourse(Course course);
        void UpdateCourse(int id, Course course);
        void DeleteCourse(int id);
        void DeleteCourses(List<int> ids);
        string Export(int id);
        int Import(string path);

        #endregion

        #region Node methods

        IEnumerable<Node> GetNodes(int courseId);
        IEnumerable<Node> GetNodes(int courseId, int? parentId);
        Node GetNode(int id);
        int? AddNode(Node node);
        bool UpdateNode(int id, Node node);
        bool DeleteNode(int id);
        bool DeleteNodes(List<int> ids);
        int? CreateCopy(Node node, int? parentId, int position);
        String GetNodeContents(int id);

        #endregion
    }
}