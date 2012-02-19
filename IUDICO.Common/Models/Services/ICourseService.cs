using System;
using System.Collections.Generic;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Common.Models.Services
{
    public interface ICourseService : IService
    {
        #region Course methods

        IEnumerable<Course> GetCourses();
        IEnumerable<Course> GetCourses(User owner);
        Course GetCourse(int id);
        string Export(int id);

        #endregion

        #region Nodes methods

        IEnumerable<Node> GetNodes(int courseId);
        IEnumerable<Node> GetNodes(int courseId, int? parentId);
        Node GetNode(int id);
        String GetNodeContents(int id);

        #endregion
    }
}
