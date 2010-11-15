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
        int? AddCourse(Course course);
        bool UpdateCourse(int id, Course course);
        bool DeleteCourse(int id);
        bool DeleteCourses(List<int> ids);
        string Export(int id);
        int? Import(string path);

        List<Node> GetNodes(int courseId);
        List<Node> GetNodes(int courseId, int? parentId);
        Node GetNode(int id);
        int? AddNode(Node node);
        bool UpdateNode(int id, Node node);
        bool DeleteNode(int id);
        bool DeleteNodes(List<int> ids);
        int? CreateCopy(Node node, int? parentId, int position);
        String GetNodeContents(int id);
    }
}