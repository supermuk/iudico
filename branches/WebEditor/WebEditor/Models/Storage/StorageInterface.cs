using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebEditor.Models;

namespace WebEditor.Models.Storage
{
    public interface IStorageInterface
    {
        List<Course> GetCourses();
        Course GetCourse(int id);
        int AddCourse(Course course);
        void UpdateCourse(int id, Course course);
        void DeleteCourse(int id);
        void DeleteCourses(List<int> ids);

        List<Node> GetNodes(int courseId);
        List<Node> GetNodes(int courseId, int? parentId);
        Node GetNode(int id);
        int AddNode(Node node);
        void UpdateNode(int id, Node node);
        void DeleteNode(int id);
        void DeleteNodes(List<int> ids);
        int CreateCopy(Node node, int? parentId, int position);
    }
}